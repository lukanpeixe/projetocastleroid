using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Castleroid.Items.Placeable
{
	public class TransparentShine : ModItem
	{
		public override void SetDefaults()
		{
			item.name = "Transparent Shine";
			item.width = 10;
			item.height = 12;
			item.maxStack = 99;
			AddTooltip("Luz Invisível");
			item.holdStyle = 1;
			item.noWet = true;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = 1;
			item.consumable = true;
			item.createTile = mod.TileType("TransparentShine");
			item.flame = true;
			item.value = 50;
		}


		
		
	}
}