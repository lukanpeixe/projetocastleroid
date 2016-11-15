using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Castleroid.Items.Placeable
{
	public class bloco_falso : ModItem
	{
		public override void SetDefaults()
		{
			item.name = "Bloco Falso";
			item.width = 16;
			item.height = 16;
			item.maxStack = 99;
			AddTooltip("Bloco Falso");
			item.noWet = true;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = 1;
			item.consumable = true;
			item.createTile = mod.TileType("bloco_falso_tile");
			item.value = 50;
		}

		
		
	}
}