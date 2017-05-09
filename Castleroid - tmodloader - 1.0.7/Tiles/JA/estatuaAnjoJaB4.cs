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
	public class estatuaAnjoJaB3 : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
			TileObjectData.addTile(Type);
			
		}

				
	}

	public class estatuaAnjoJaB3i : ModItem
	{
		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.ArmorStatue);
			item.name = "estatuaAnjoJaB3";
			item.createTile = mod.TileType("estatuaAnjoJaB3");
			item.placeStyle = 0;
		}
	}

	
}
