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
	public class estatuaAnjoJaB4 : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
			TileObjectData.addTile(Type);
			
		}

				
	}

	public class estatuaAnjoJaB4i : ModItem
	{
		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.ArmorStatue);
			item.name = "estatuaAnjoJaB4";
			item.createTile = mod.TileType("estatuaAnjoJaB4");
			item.placeStyle = 0;
		}
	}

	
}
