//Esse exampleNPC foi pego como o BogenKrampus do necropolis, nele há a AI de tal mob.

using System.Text;
using System.Diagnostics;
using TAPI;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Necro
{
    public class BogenKrampus : ModNPC
    {

        float customAi1;
        float customAi2;
        int drownTimerMax = 2400;
        int drownTimer = 2400;
        int drowningRisk = 2000;

        #region AI // code by GrtAndPwrflTrtl (http://www.terrariaonline.com/members/grtandpwrfltrtl.86018/)
        public override void AI()  //  warrior ai
        {

            npc.TargetClosest(true);

            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                int dustDeath = 0;
                for (int num36 = 0; num36 < 20; num36++)
                {
                    dustDeath = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 5, Main.rand.Next(-10, 10), Main.rand.Next(-10, 10), 200, Color.White, 2.2f);
                    Main.dust[dustDeath].noGravity = true;
                }
                npc.position.X = -1000;
                npc.position.Y = -1000;
                npc.velocity.X = 0;
                npc.velocity.Y = 0;
                npc.timeLeft = 0;
            }

            #region set up NPC's attributes & behaviors
            // set parameters
            //  is_archer OR can_pass_doors OR shoot_and_walk, pick only 1.  They use the same ai[] vars (1&2)
            bool is_archer = false; // stops and shoots when target sighted; skel archer & gob archer are the archers
            bool shoot_and_walk = true;  //  can shoot while walking like clown; uses ai[2] so cannot be used with is_archer or can_pass_doors

            //  can_teleport==true code uses boredom_time and ai[3] (boredom), but not mutually exclusive
            bool can_teleport = false;  //  tp around like chaos ele
            int boredom_time = 60; // time until it stops targeting player if blocked etc, 60 for anything but chaos ele, 20 for chaos ele
            int boredom_cooldown = 10 * boredom_time; // boredom level where boredom wears off; usually 10*boredom_time

            bool hates_light = false;  //  flees in daylight like: Zombie, Skeleton, Undead Miner, Doctor Bones, The Groom, Werewolf, Clown, Bald Zombie, Possessed Armor

            int sound_type = 14; // Parameter for Main.PlaySound().  14 for Zombie, Skeleton, Angry Bones, Heavy Skeleton, Skeleton Archer, Bald Zombie.  26 for Mummy, Light & Dark Mummy. 0 means no sounds
            int sound_frequency = 500;  //  chance to play sound every frame, 1000 for zombie/skel, 500 for mummies

            float acceleration = 2f;  //  how fast it can speed up
            float top_speed = 3.6f;  //  max walking speed, also affects jump length
            float braking_power = .2f;  //  %of speed that can be shed every tick when above max walking speed
            double bored_speed = .9;  //  above this speed boredom decreases(if not already bored); usually .9

            float enrage_percentage = .5f;  //  double movement speed below this life fraction. 0 for no enrage. Mummies enrage below .5
            float enrage_acceleration = 4.6f;  //  faster when enraged, usually 2*acceleration
            float enrage_top_speed = 2;  //  faster when enraged, usually 2*top_speed

            // is_archer & clown bombs only
            int shot_rate = 70;  //  rate at which archers/bombers fire; 70 for skeleton archer, 180 for goblin archer, 450 for clown; atm must be an even # or won't fire at shot_rate/2
            int projectile_damage = 35;  //  projectile dmg: 35 for Skeleton Archer, 11 for Goblin Archer
            int projectile_id = ProjDef.byName["Necro:EArcherBolt"].type; // projectile id: 82(Flaming Arrow) for Skeleton Archer, 81(Wooden Arrow) for Goblin Archer, 75(Happy Bomb) for Clown
            float projectile_velocity = 11; // initial velocity? 11 for Skeleton Archers, 9 for Goblin Archers, bombs have fixed speed & direction atm

            // Omnirs creature sorts
            bool tooBig = false; // force bigger creatures to jump
            bool lavaJumping = false; // Enemies jump on lava.
            bool canDrown = true; // They will drown if in the water for too long

            // calculated parameters
            bool moonwalking = false;  //  not jump/fall and moving backwards to facing
            if (npc.velocity.Y == 0f && ((npc.velocity.X > 0f && npc.direction < 0) || (npc.velocity.X < 0f && npc.direction > 0)))
                moonwalking = true;
            #endregion
            //-------------------------------------------------------------------


            #region Too Big and Lava Jumping
            if (tooBig)
            {
                if (npc.velocity.Y == 0f && (npc.velocity.X == 0f && npc.direction < 0))
                {
                    npc.velocity.Y -= 16f;
                    npc.velocity.X -= 2f;
                }
                else if (npc.velocity.Y == 0f && (npc.velocity.X == 0f && npc.direction > 0))
                {
                    npc.velocity.Y -= 16f;
                    npc.velocity.X += 2f;
                }
            }
            if (lavaJumping)
            {
                if (npc.lavaWet)
                {
                    npc.velocity.Y -= 2;
                }
            }
            #endregion
            //-------------------------------------------------------------------


            #region teleportation particle effects
            if (can_teleport)  //  chaos elemental type teleporter
            {
                if (npc.ai[3] == -120f)  //  boredom goes negative? I think this makes disappear/arrival effects after it just teleported
                {
                    npc.velocity *= 0f; // stop moving
                    npc.ai[3] = 0f; // reset boredom to 0
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 8);
                    Vector2 vector = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f); // current location
                    float num6 = npc.oldPos[2].X + (float)npc.width * 0.5f - vector.X; // direction to where it was 3 frames ago?
                    float num7 = npc.oldPos[2].Y + (float)npc.height * 0.5f - vector.Y; // direction to where it was 3 frames ago?
                    float num8 = (float)((double)(num6 * num6 + num7 * num7)); // distance to where it was 3 frames ago?
                    num8 = 2f / num8; // to normalize to 2 unit long vector
                    num6 *= num8; // direction to where it was 3 frames ago, vector normalized
                    num7 *= num8; // direction to where it was 3 frames ago, vector normalized
                    for (int j = 0; j < 20; j++) // make 20 dusts at current position
                    {
                        int num9 = Dust.NewDust(npc.position, npc.width, npc.height, 71, num6, num7, 200, default(Color), 2f);
                        Main.dust[num9].noGravity = true; // floating
                        Dust expr_19EE_cp_0 = Main.dust[num9]; // make a dust handle?
                        expr_19EE_cp_0.velocity.X = expr_19EE_cp_0.velocity.X * 2f; // faster in x direction
                    }
                    for (int k = 0; k < 20; k++) // more dust effects at old position
                    {
                        int num10 = Dust.NewDust(npc.oldPos[2], npc.width, npc.height, 71, -num6, -num7, 200, default(Color), 2f);
                        Main.dust[num10].noGravity = true;
                        Dust expr_1A6F_cp_0 = Main.dust[num10];
                        expr_1A6F_cp_0.velocity.X = expr_1A6F_cp_0.velocity.X * 2f;
                    }
                } // END just teleported
            } // END can teleport
            #endregion
            //-------------------------------------------------------------------


            #region adjust boredom level
            if (!is_archer || npc.ai[2] <= 0f)  //  loop to set ai[3] (boredom)
            {
                if (npc.position.X == npc.oldPosition.X || npc.ai[3] >= (float)boredom_time || moonwalking)  //  stopped or bored or moonwalking
                    npc.ai[3] += 1f; // increase boredom
                else if ((double)(npc.velocity.X) > bored_speed && npc.ai[3] > 0f)  //  moving fast and not bored
                    npc.ai[3] -= 1f; // decrease boredom

                if (npc.justHit || npc.ai[3] > boredom_cooldown)
                    npc.ai[3] = 0f; // boredom wears off if enough time passes, or if hit

                if (npc.ai[3] == (float)boredom_time)
                    npc.netUpdate = true; // netupdate when state changes to bored
            }
            #endregion
            //-------------------------------------------------------------------


            #region play creature sounds, target/face player, respond to boredom
            if ((!hates_light || !Main.dayTime || (double)npc.position.Y > Main.worldSurface * 16.0) && npc.ai[3] < (float)boredom_time)
            {   // not fleeing light & not bored
                if (sound_type > 0 && Main.rand.Next(sound_frequency) <= 0)
                    Main.PlaySound(sound_type, (int)npc.position.X, (int)npc.position.Y, 1); // random creature sounds
                if (!canDrown)
                {
                    npc.TargetClosest(true); //  Target the closest player & face him (If passed as a parameter, a bool will determine whether it should face the target or not)
                }
                if (canDrown && !npc.wet)
                {
                    npc.TargetClosest(true); //  Target the closest player & face him (If passed as a parameter, a bool will determine whether it should face the target or not)
                }
            }
            else if (!is_archer || npc.ai[2] <= 0f) //  fleeing light or bored (& not aiming)
            {
                if (hates_light && Main.dayTime && (double)(npc.position.Y / 16f) < Main.worldSurface && npc.timeLeft > 10)
                    npc.timeLeft = 10;  //  if hates light & in light, hasten despawn

                if (npc.velocity.X == 0f)
                {
                    if (npc.velocity.Y == 0f)
                    { // not moving
                        if (npc.ai[0] == 0f)
                            npc.ai[0] = 1f; // facing change delay
                        else
                        { // change movement and facing direction, reset delay
                            npc.direction *= -1;
                            npc.spriteDirection = npc.direction;
                            npc.ai[0] = 0f;
                        }
                    }
                }
                else // moving in x direction,
                    npc.ai[0] = 0f; // reset facing change delay

                if (npc.direction == 0) // what does it mean if direction is 0?
                    npc.direction = 1; // flee right if direction not set? or is initial direction?
            } // END fleeing light or bored (& not aiming)
            #endregion
            //-------------------------------------------------------------------


            #region enrage
            bool enraged = false; // angry from damage; not stored from tick to tick
            if ((enrage_percentage > 0) && (npc.life < (float)npc.lifeMax * enrage_percentage))  //  speed up at low life
                enraged = true;
            if (enraged)
            { // speed up movement if enraged
                acceleration = enrage_acceleration;
                top_speed = enrage_top_speed;
            }
            #endregion
            //-------------------------------------------------------------------


            #region melee movement
            if (!is_archer || (npc.ai[2] <= 0f && !npc.confused))  //  meelee attack/movement. archers only use while not aiming
            {
                if ((npc.velocity.X) > top_speed)  //  running/flying faster than top speed
                {
                    if (npc.velocity.Y == 0f)  //  and not jump/fall
                        npc.velocity *= (1f - braking_power);  //  decelerate
                }
                else if ((npc.velocity.X < top_speed && npc.direction == 1) || (npc.velocity.X > -top_speed && npc.direction == -1))
                {  //  running slower than top speed (forward), can be jump/fall
                    if (can_teleport && moonwalking)
                        npc.velocity.X = npc.velocity.X * 0.99f;  //  ? small decelerate for teleporters

                    npc.velocity.X = npc.velocity.X + (float)npc.direction * acceleration;  //  accellerate fwd; can happen midair
                    if ((float)npc.direction * npc.velocity.X > top_speed)
                        npc.velocity.X = (float)npc.direction * top_speed;  //  but cap at top speed
                }  //  END running slower than top speed (forward), can be jump/fall
            } // END non archer or not aiming*/
            #endregion
            //-------------------------------------------------------------------


            #region archer projectile code (stops moving to shoot)
            if (is_archer)
            {
                if (npc.confused)
                    npc.ai[2] = 0f; // won't try to stop & aim if confused
                else // not confused
                {
                    if (npc.ai[1] > 0f)
                        npc.ai[1] -= 1f; // decrement fire & reload counter

                    if (npc.justHit) // was just hit?
                    {
                        npc.ai[1] = 30f; // shot on .5 sec cooldown
                        npc.ai[2] = 0f; // not aiming
                    }
                    if (npc.ai[2] > 0f) // if aiming: adjust aim and fire if needed
                    {
                        npc.TargetClosest(true); // target and face closest player
                        if (npc.ai[1] == (float)(shot_rate / 2))  //  fire at halfway through; first half of delay is aim, 2nd half is cooldown
                        { // firing:
                            Vector2 npc_center = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f); // npc position
                            float npc_to_target_x = Main.player[npc.target].position.X + (float)Main.player[npc.target].width * 0.5f - npc_center.X; // x vector to target
                            float num16 = (npc_to_target_x) * 0.1f; // 10% of x distance to target: to aim high if farther?
                            float npc_to_target_y = Main.player[npc.target].position.Y + (float)Main.player[npc.target].height * 0.5f - npc_center.Y - num16; // y vector to target (aiming high at distant targets)
                            npc_to_target_x += (float)Main.rand.Next(-40, 41); //  targeting error: 40 pix=2.5 blocks
                            npc_to_target_y += (float)Main.rand.Next(-40, 41); //  targeting error: 40 pix=2.5 blocks
                            float target_dist = (float)((double)(npc_to_target_x * npc_to_target_x + npc_to_target_y * npc_to_target_y)); // distance to target
                            npc.netUpdate = true; // ??
                            target_dist = projectile_velocity / target_dist; // to normalize by projectile_velocity
                            npc_to_target_x *= target_dist; // normalize by projectile_velocity
                            npc_to_target_y *= target_dist; // normalize by projectile_velocity
                            npc_center.X += npc_to_target_x;  //  initial projectile position includes one tick of initial movement
                            npc_center.Y += npc_to_target_y;  //  initial projectile position includes one tick of initial movement
                            if (Main.netMode != 1)  //  is server
                                Projectile.NewProjectile(npc_center.X, npc_center.Y, npc_to_target_x, npc_to_target_y, projectile_id, projectile_damage, 0f, Main.myPlayer);

                            if ((npc_to_target_y) > (npc_to_target_x) * 2f) // target steeply above/below NPC
                            {
                                if (npc_to_target_y > 0f)
                                    npc.ai[2] = 1f; // aim downward
                                else
                                    npc.ai[2] = 5f; // aim upward
                            }
                            else if ((npc_to_target_x) > (npc_to_target_y) * 2f) // target on level with NPC
                                npc.ai[2] = 3f;  //  aim straight ahead
                            else if (npc_to_target_y > 0f) // target is below NPC
                                npc.ai[2] = 2f;  //  aim slight downward
                            else // target is not below NPC
                                npc.ai[2] = 4f;  //  aim slight upward
                        } // END firing
                        if (npc.velocity.Y != 0f || npc.ai[1] <= 0f) // jump/fall or firing reload
                        {
                            npc.ai[2] = 0f; // not aiming
                            npc.ai[1] = 0f; // reset firing/reload counter (necessary? nonzero maybe)
                        }
                        else // no jump/fall and no firing reload
                        {
                            npc.velocity.X = npc.velocity.X * 0.9f; // decelerate to stop & shoot
                            npc.spriteDirection = npc.direction; // match animation to facing
                        }
                    } // END if aiming: adjust aim and fire if needed
                    if (npc.ai[2] <= 0f && npc.velocity.Y == 0f && npc.ai[1] <= 0f && !Main.player[npc.target].dead && Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
                    { // not aiming & no jump/fall & fire/reload ctr is 0 & target is alive and LOS to target: start aiming
                        float num21 = 10f; // dummy vector length in place of initial velocity? not sure why this is needed
                        Vector2 npc_center = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                        float npc_to_target_x = Main.player[npc.target].position.X + (float)Main.player[npc.target].width * 0.5f - npc_center.X;
                        float num23 = (npc_to_target_x) * 0.1f; // 10% of x distance to target: to aim high if farther?
                        float npc_to_target_y = Main.player[npc.target].position.Y + (float)Main.player[npc.target].height * 0.5f - npc_center.Y - num23; // y vector to target (aiming high at distant targets)
                        npc_to_target_x += (float)Main.rand.Next(-40, 41);
                        npc_to_target_y += (float)Main.rand.Next(-40, 41);
                        float target_dist = (float)((double)(npc_to_target_x * npc_to_target_x + npc_to_target_y * npc_to_target_y));
                        if (target_dist < 700f) // 700 pix = 43.75 blocks
                        { // target is in range
                            npc.netUpdate = true; // ??
                            npc.velocity.X = npc.velocity.X * 0.5f; // hard brake
                            target_dist = num21 / target_dist; // to normalize by num21
                            npc_to_target_x *= target_dist; // normalize by num21
                            npc_to_target_y *= target_dist; // normalize by num21
                            npc.ai[2] = 3f; // aim straight ahead
                            npc.ai[1] = (float)shot_rate; // start fire & reload counter
                            if ((npc_to_target_y) > (npc_to_target_x) * 2f) // target steeply above/below NPC
                            {
                                if (npc_to_target_y > 0f)
                                    npc.ai[2] = 1f; // aim downward
                                else
                                    npc.ai[2] = 5f; // aim upward
                            }
                            else if ((npc_to_target_x) > (npc_to_target_y) * 2f) // target on level with NPC
                                npc.ai[2] = 3f; // aim straight ahead
                            else if (npc_to_target_y > 0f)
                                npc.ai[2] = 2f; // aim slight downward
                            else
                                npc.ai[2] = 4f; // aim slight upward
                        } // END target is in range
                    } // END start aiming
                } // END not confused
            }  //  END is archer
            #endregion
            //-------------------------------------------------------------------


            #region shoot and walk
            if (shoot_and_walk && Main.netMode != 1 && !Main.player[npc.target].dead) // can generalize this section to moving+projectile code
            {
                if (npc.justHit)
                    npc.ai[2] = 0f; // reset throw countdown when hit

                #region Projectiles
                customAi1 += (Main.rand.Next(2, 5) * 0.1f) * npc.scale;
                if (customAi1 >= 10f)
                {
                    float num48 = 8f;
                    Vector2 vector8 = new Vector2(npc.position.X + (npc.width * 0.5f), npc.position.Y + (npc.height / 2));
                    float speedX = ((Main.player[npc.target].position.X + (Main.player[npc.target].width * 0.5f)) - vector8.X) + Main.rand.Next(-20, 0x15);
                    float speedY = ((Main.player[npc.target].position.Y + (Main.player[npc.target].height * 0.5f)) - vector8.Y) + Main.rand.Next(-20, 0x15);
                    if (((speedX < 0f) && (npc.velocity.X < 0f)) || ((speedX > 0f) && (npc.velocity.X > 0f)))
                    {
                        if (Main.rand.Next(460) == 1)
                        {
                            Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 5);
                            float num51 = (float)((double)((speedX * speedX) + (speedY * speedY)));
                            num51 = num48 / num51;
                            speedX *= num51;
                            speedY *= num51;
                            int damage = 50,
                            type = ProjDef.byName["Necro:ENaughtyGift"].type;
                            int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, speedX, speedY, type, damage, 0f, Main.myPlayer);
                            Main.projectile[num54].timeLeft = 300;
                            customAi1 = 1f;
                        }
                        npc.netUpdate = true;
                    }
                }

                customAi2 += (Main.rand.Next(2, 5) * 0.1f) * npc.scale;
                if (npc.life >= 800 && customAi2 >= 10f)
                {
                    npc.TargetClosest(true);
                    if (Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
                    {
                        float num48 = 8f;
                        Vector2 vector8 = new Vector2(npc.position.X + (npc.width * 0.5f), npc.position.Y + (npc.height / 2));
                        float speedX = ((Main.player[npc.target].position.X + (Main.player[npc.target].width * 0.5f)) - vector8.X) + Main.rand.Next(-20, 0x15);
                        float speedY = ((Main.player[npc.target].position.Y + (Main.player[npc.target].height * 0.5f)) - vector8.Y) + Main.rand.Next(-20, 0x15);
                        if (((speedX < 0f) && (npc.velocity.X < 0f)) || ((speedX > 0f) && (npc.velocity.X > 0f)))
                        {
                            if (Main.rand.Next(360) == 1)
                            {
                                Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 5);
                                float num51 = (float)((double)((speedX * speedX) + (speedY * speedY)));
                                num51 = num48 / num51;
                                speedX *= num51;
                                speedY *= num51;
                                int damage = 15,
                                type = ProjDef.byName["Necro:ENaughtyGift"].type;
                                int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, speedX, speedY, type, damage, 0f, Main.myPlayer);
                                Main.projectile[num54].timeLeft = 60;
                                customAi1 = 1f;
                            }
                            npc.netUpdate = true;
                        }
                    }
                }
                #endregion
            }
            #endregion
            //-------------------------------------------------------------------

            #region drown // code by Omnir
            if (canDrown)
            {
                if (!npc.wet)
                {
                    npc.TargetClosest(true);
                    drownTimer = drownTimerMax;
                }
                if (npc.wet)
                {
                    drownTimer--;
                }
                if (npc.wet && drownTimer > drowningRisk)
                {
                    npc.TargetClosest(true);
                }
                else if (npc.wet && drownTimer <= drowningRisk)
                {
                    npc.TargetClosest(false);
                    if (npc.timeLeft > 10)
                    {
                        npc.timeLeft = 10;
                    }
                    npc.directionY = -1;
                    if (npc.velocity.Y > 0f)
                    {
                        npc.direction = 1;
                    }
                    npc.direction = -1;
                    if (npc.velocity.X > 0f)
                    {
                        npc.direction = 1;
                    }
                }
                if (drownTimer <= 0)
                {
                    npc.life--;
                    if (npc.life <= 0)
                    {
                        Main.PlaySound(4, (int)npc.position.X, (int)npc.position.Y, 1);
                        npc.NPCLoot();
                        npc.netUpdate = true;
                    }
                }
            }
            #endregion
            //-------------------------------------------------------------------*/
        }
        #endregion

        public override void PostNPCLoot()
        {
            //generate particle effect
            Color color = new Color();
            Rectangle rectangle = new Rectangle((int)npc.position.X, (int)(npc.position.Y + ((npc.height - npc.width) / 2)), npc.width, npc.width);//npc.frame;
            int count = 50;
            float vectorReduce = .4f;
            for (int i = 1; i <= count; i++)
            {
                //int dust = Dust.NewDust(new Vector2((float) rectangle.X, (float) rectangle.Y), rectangle.Width, rectangle.Height, 6, (npc.velocity.X * 0.2f) + (npc.direction * 3), npc.velocity.Y * 0.2f, 100, color, 1.9f);
                int dust = Dust.NewDust(npc.position, rectangle.Width, rectangle.Height, 6, 0, 0, 100, color, 1.6f);
                Main.dust[dust].noGravity = false;
                Main.dust[dust].velocity.X = vectorReduce * (Main.dust[dust].position.X - (npc.position.X + (npc.width / 2)));
                Main.dust[dust].velocity.Y = vectorReduce * (Main.dust[dust].position.Y - (npc.position.Y + (npc.height / 2)));
            }
            if (Main.netMode != 2)
            {
                Gore.NewGore(npc.position, npc.velocity, GoreDef.gores["Necro:BogenKrampusGore1"], 1f);
                Gore.NewGore(npc.position, npc.velocity, GoreDef.gores["Necro:BogenKrampusGore2"], 1f);
                Gore.NewGore(npc.position, npc.velocity, GoreDef.gores["Necro:BogenKrampusGore2"], 1f);
                Gore.NewGore(npc.position, npc.velocity, GoreDef.gores["Necro:BogenKrampusGore3"], 1f);
                Gore.NewGore(npc.position, npc.velocity, GoreDef.gores["Necro:BogenKrampusGore3"], 1f);
            }
            NPC.NewNPC((int)npc.position.X + (npc.width / 2), (int)npc.position.Y + (npc.height / 2), "Necro:KrampusBloatedHead", 0);
        }
    }
}
