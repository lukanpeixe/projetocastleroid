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
	public class VasoA1 : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.addTile(Type);
			
		}

				
	}

	public class VasoA1i : ModItem
	{
		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.ArmorStatue);
			item.name = "VasoA1";
			item.createTile = mod.TileType("VasoA1");
			item.placeStyle = 0;
		}
	}

	
}
