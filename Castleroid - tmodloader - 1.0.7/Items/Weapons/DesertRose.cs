using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Castleroid;

namespace Castleroid.Items.Weapons
{
	public class DesertRose : ModItem
	{
		int aux_chance = 0;
		Player player = Main.player[Main.myPlayer];
		double aux_damage = 0;
		Random chance = new Random();
		bool regen = false;
		
		public override void SetDefaults()
		{
			item.name = "Desert Rose";
			item.damage = 20;
			item.melee = true;
			item.width = 18;
			item.height = 18;
			item.toolTip = "Espada de Rosa amarela e arenosa";
			item.toolTip2 = "Chance de cegar o usuario";
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = 3;
			item.knockBack = 6;
			item.value = 1000000;
			item.rare = 2;
			item.useSound = 1;
			item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.DirtBlock, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public int Chance(){
			aux_chance = chance.Next(0,10);//Define o percentual de chance com base nos dois numeros de entrada
			return aux_chance;//Valor retornado para utilizar na função OnHitNPC
			
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{

			int t = Chance();
			int c = 1;//10%, 2 = 20%, 3 = 30% etc...
			if(t > c){
				Main.NewText("Acertou..!", 255, 240, 20, false);//Linha caso acerte o dbuff
				player.AddBuff(BuffID.Darkness, 360);
			}
			
		}
	}
}
/* 
	O codigo a chance é de 0 a 10, t recebe o valor aleatorio para chance.
	e c é o comparativo para chance exemplo se t for maior que c
	c = 4 - 
	t = 6 - 60%
	tudo que tiver entre c e 10 neste caso c vale 4 é aceitavel
*/