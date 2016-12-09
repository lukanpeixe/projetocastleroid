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

namespace Castleroid.Items
{
	public class regen_sword : ModItem
	{
		int aux_chance = 0;
		Player player = Main.player[Main.myPlayer];
		double aux_damage = 0;
		Random chance = new Random();
		bool regen = false;
		
		public override void SetDefaults()
		{
			item.name = "Espada da vida";
			item.damage = 20;
			item.melee = true;
			item.width = 40;
			item.height = 40;
			item.toolTip = "Possui propriedades que podem curar";
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = 1;
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
		//Funcao criada para ativar e desativar o regen
		public void Regen(){
			//Vou opmitizar o código estava com um pouco de preguiça XD

			//Caso o jogador possua o status do buff ele ativa ou nao o regen
			if(player.HasBuff(BuffID.Regeneration) == -1){
				regen = false;
			}else{
				regen = true;
			}
			if(regen){
				player.lifeRegen += 1;//Quantidade que ira regenerar
				//player.lifeRegenCount = 1;//Não achei necessario usar mas tambem influencia no heal
			}else{
				player.lifeRegen = 0;
				player.lifeRegenCount = 0;
			}
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			int t = Chance();
			int v = player.HasBuff(BuffID.Regeneration);
			string teste = v.ToString();
			if(t > 7){
					regen = true;
					player.AddBuff(BuffID.Regeneration, 600);
			}

			//Main.NewText(teste, 255, 240, 20, false);
			/*if (player.HasBuff(BuffID.Regeneration) == -1){
				player.lifeRegen = 0;
				player.lifeRegenCount = 0;
				regen = false;
				
			}
			*/
			
		}
	}
}
/* 
	Basta setar o buff que deseja e o percentual de chance na funçao Chance()
	Nota: ligar no arquivo Castleroid.cs a regeneração de vida
	ou simplesmente comente a linha 
*/
