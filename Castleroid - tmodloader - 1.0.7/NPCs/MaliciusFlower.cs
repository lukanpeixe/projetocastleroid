using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Castleroid.NPCs
{
	public class MaliciusFlower : ModNPC
	{
		int contador = 0;//canta os frams
		int segundos = 0;//conta os segundos
		int steps = 0;// conta os passos
		
		public override void SetDefaults()
		{
			npc.name = "MaliciusFlower";
			npc.displayName = "malicius Flower";
			npc.width = 18;
			npc.height = 40;
			npc.damage = 2;
			npc.defense = 0;
			npc.lifeMax = 4;
			npc.soundHit = 1;
			npc.soundKilled = 16;
			npc.knockBackResist = 0.0f;
			npc.aiStyle = -1;
			//Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.Zombie];
			//aiType = NPCID.CyanBeetle;
			animationType = NPCID.GiantFlyingFox;
			Main.npcFrameCount[npc.type] = 4;
			NPCID.Sets.MustAlwaysDraw[npc.type] = true;
		}
	
	public override void HitEffect(int hitDirection, double damage)
		{
			for (int i = 0; i < 10; i++)
			{
				int dustType = Main.rand.Next(139, 143);
				int dustIndex = Dust.NewDust(npc.position, npc.width, npc.height, dustType);
				Dust dust = Main.dust[dustIndex];
				dust.velocity.X = dust.velocity.X + Main.rand.Next(-50, 51) * 0.01f;
				dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-50, 51) * 0.01f;
				dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
			}
		}
	
	public override float CanSpawn(NPCSpawnInfo spawnInfo)
		{
			return spawnInfo.spawnTileY < Main.rockLayer && Main.dayTime ? 0.1f : 0f;//Vou testar e melhorar como ele spawnna
		}

		public override void AI(){
			int damage = 5;//Adiciona o dano ao projetio
            //Player P = Main.player[npc.target];// Priorisa o target do personagem em testes ainda
               
	
			if(steps == 0){
				npc.velocity.X = 0;//move para traz
			}else if(steps == 1){
				npc.velocity.X = 0;//move para frente
			}else{
				npc.velocity.X = 0;
				steps = 0;
			}
			npc.collideX = true;
			npc.collideY = true;
			contador += 1;
			if(contador > 60){//60 numero de fps do jogo 60quadros por segundo
				steps++;
				segundos++;
				contador = 0;
			}
				if(segundos > 3){
					
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                npc.TargetClosest(true);
            }
            //npc.netUpdate = true; // para que serve?

                float Speed = 20f;  //projectile speed
                Vector2 vector8 = new Vector2(npc.position.X, npc.position.Y + 0);//Posição de onde vai sair o projetio
                int type = mod.ProjectileType("gosma_shot");  //nome do projetio
                Main.PlaySound(23, (int)npc.position.X, (int)npc.position.Y, 17);//O som emitido
                float rotation = (float)Math.Atan2(vector8.Y - (npc.position.Y + (npc.height * 0.0f)), vector8.X - (npc.position.X + (npc.width * 0.0f)));//1º +alto -chão //2º dire +esq -dir sobrepoem o 1ºd baixo, quanto maior mais fecha o arco
				int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -0.6), (float)((Math.Sin(rotation) * Speed) * 0.1), type, damage, 0f, 0); //1º dire +esq -dir força// 2º +alto -chão ?
					
					
					//npc.velocity.Y = -7;//Força do pulo
					segundos = 0;
				}
				if(segundos > 3){
					
            if (npc.target < 2 || npc.target == 2 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                npc.TargetClosest(true);
            }
            

                float Speed = 2f;  //projectile speed
                Vector2 vector8 = new Vector2(npc.position.X, npc.position.Y + 2);//Posição de onde vai sair o projetio
                int type = mod.ProjectileType("gosma_shot");  //nome do projetio
                Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 2);//O som emitido
                float rotation = (float)Math.Atan2(vector8.Y - (npc.position.Y + (npc.height * 0.0f)), vector8.X - (npc.position.X + (npc.width * 0.1f)));//1º +alto -chão //2º dire +esq -dir sobrepoem o 1ºd baixo, quanto maior mais fecha o arco
				int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * 0.6), (float)((Math.Sin(rotation) * Speed) * 0.1), type, damage, 0f, 0); //1º dire +esq -dir força// 2º +alto -chão ?
					
					
					//npc.velocity.Y = -7;//Força do pulo
					segundos = 0;
				}
				
				
		}
	
	}
	
	
	}

