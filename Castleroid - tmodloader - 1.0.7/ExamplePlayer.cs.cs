using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace Castleroid
{
	public class ExamplePlayer : ModPlayer
	{
		private const int saveVersion = 0;
		public int score = 0;
		public bool eFlames = false;
		public bool elementShield = false;
		public int elementShields = 0;
		private int elementShieldTimer = 0;
		public int elementShieldPos = 0;
		public int lockTime = 0;
		public bool voidMonolith = false;
		public int heroLives = 0;
		public int reviveTime = 0;
		public int constantDamage = 0;
		public float percentDamage = 0f;
		public float defenseEffect = -1f;
		public bool badHeal = false;
		public int healHurt = 0;
		public bool nullified = false;	
	
		public bool examplePet = false;
		public bool exampleLightPet = false;

		public bool ZoneExample = false;

		public override void ResetEffects()
		{
			eFlames = false;
			elementShield = false;
			constantDamage = 0;
			percentDamage = 0f;
			defenseEffect = -1f;
			badHeal = false;
			healHurt = 0;
			nullified = false;
			
			examplePet = false;
			exampleLightPet = false;
		}

		public override void UpdateDead()
		{
			eFlames = false;
			badHeal = false;
		}
/*
		public override void SaveCustomData(BinaryWriter writer)
		{
			writer.Write(saveVersion);
			writer.Write(score);
		}

		public override void LoadCustomData(BinaryReader reader)
		{
			int loadVersion = reader.ReadInt32();
			score = reader.ReadInt32();
		}

*/


		public override void SetupStartInventory(IList<Item> items)
		{
			items.Clear();   // Remove os 3 ítens iniciais de cobre		

			Item item = new Item();
			item.SetDefaults(mod.ItemType("Item")); // Seleciona o Item de exemplo
			item.stack = 5;
			items.Add(item);   // Adiciona o Item para o inventário

			Item obj1 = new Item();
			obj1.SetDefaults("Wooden Sword");  // Adiciona um ítem qualquer pelo nome
            obj1.Prefix(-1);   

			items.Add(obj1);   // Adiciona o Item para o inventário

                        player.statLifeMax = 50; // define a quantidade máxima de vida
                        player.statLife = 50; // define a quantidade atual de vida			 

		}




		public override void UpdateBadLifeRegen()
		{
			if (eFlames)
			{
				if (player.lifeRegen > 0)
				{
					player.lifeRegen = 0;
				}
				player.lifeRegenTime = 0;
				player.lifeRegen -= 16;
			}
			if (healHurt > 0)
			{
				if (player.lifeRegen > 0)
				{
					player.lifeRegen = 0;
				}
				player.lifeRegenTime = 0;
				player.lifeRegen -= 120 * healHurt;
			}
		}


		public override void PostUpdateBuffs()
		{
			if (nullified)
			{
				Nullify();
			}
		}

		public override void PostUpdateEquips()
		{
			if (nullified)
			{
				Nullify();
			}
			if (elementShield)
			{
				if (elementShields > 0)
				{
					elementShieldTimer--;
					if (elementShieldTimer < 0)
					{
						elementShields--;
						elementShieldTimer = 600;
					}
				}
			}
			else
			{
				elementShields = 0;
				elementShieldTimer = 0;
			}
			elementShieldPos++;
			elementShieldPos %= 300;
		}

		

		public override void FrameEffects()
		{
			if (nullified)
			{
				Nullify();
			}
		}

		private void Nullify()
		{
			player.ResetEffects();
			player.head = -1;
			player.body = -1;
			player.legs = -1;
			player.handon = -1;
			player.handoff = -1;
			player.back = -1;
			player.front = -1;
			player.shoe = -1;
			player.waist = -1;
			player.shield = -1;
			player.neck = -1;
			player.face = -1;
			player.balloon = -1;
			nullified = true;
		}
/*
		public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit,
			ref bool customDamage, ref bool playSound, ref bool genGore, ref string deathText)
		{
			if (constantDamage > 0 || percentDamage > 0f)
			{
				int damageFromPercent = (int)(player.statLifeMax2 * percentDamage);
				damage = Math.Max(constantDamage, damageFromPercent);
				customDamage = true;
			}
			else if (defenseEffect >= 0f)
			{
				if (Main.expertMode)
				{
					defenseEffect *= 1.5f;
				}
				damage -= (int)(player.statDefense * defenseEffect);
				if (damage < 0)
				{
					damage = 1;
				}
				customDamage = true;
			}
			constantDamage = 0;
			percentDamage = 0f;
			defenseEffect = -1f;
			return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref deathText);
		}		
*/

		public override void DrawEffects(PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
		{
			if (eFlames)
			{
				if (Main.rand.Next(4) == 0 && drawInfo.shadow == 0f)
				{
					int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, mod.DustType("EtherealFlame"), player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100, default(Color), 3f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 1.8f;
					Main.dust[dust].velocity.Y -= 0.5f;
					Main.playerDrawDust.Add(dust);
				}
				r *= 0.1f;
				g *= 0.2f;
				b *= 0.7f;
				fullBright = true;
			}
		}

		
	}
}