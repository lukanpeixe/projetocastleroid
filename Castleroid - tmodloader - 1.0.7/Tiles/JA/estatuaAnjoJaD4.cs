using Microsoft.Xna.Framework;
using System;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Castleroid.Tiles.JA
{
	public class estatuaAnjoJaD4 : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
			TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 18};
			TileObjectData.addTile(Type);
			
		}

				
	}

	public class estatuaAnjoJaD4i : ModItem
	{
		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.ArmorStatue);
			item.name = "estatuaAnjoJaD4";
			item.createTile = mod.TileType("estatuaAnjoJaD4");
			item.placeStyle = 0;
		}
	}

	
}
