using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Castleroid.Items
{
	public class SugarPetal : ModItem
	{
		public override void SetDefaults()
		{
			item.name = "Sugar Petal";
			item.width = 14;
			item.height = 24;
			item.useStyle = 2;
			item.maxStack = 5;
			item.useAnimation = 30;
			item.useTime = 30;
			item.consumable = true;
			item.useTurn = false;
			AddTooltip("Restore 5 HP");
			AddTooltip2("Small, but delicius!");
			item.healLife = 5;
			item.rare = 6;
			//item.buffType = 1;
			//item.buffTime = 36000;
			
			
		}
	}
}

