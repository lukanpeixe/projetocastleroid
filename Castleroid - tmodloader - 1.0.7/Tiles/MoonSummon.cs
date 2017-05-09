using Microsoft.Xna.Framework;
using System;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Castleroid.Tiles
{
    public class MoonSummon : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileObsidianKill[Type] = true;
			AddMapEntry(new Color(0, 255, 255), "MoonSummon");
            dustType = 80;
			disableSmartCursor = true;
			Main.tileSolidTop[Type] = true;
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = false;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x1);
			TileObjectData.newTile.CoordinateHeights = new int[]{ 18 };
			TileObjectData.addTile(Type);
		}
		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 32, 16, mod.ItemType("MoonSummon"));
		}
        public override void HitWire(int i, int j)
        {
            // Find the coordinates of top left tile square through math
            int y = j - (Main.tile[i, j].frameY / 16);
            int x = i - (Main.tile[i, j].frameX / 16);

            Wiring.SkipWire(x, y);
            Wiring.SkipWire(x, y + 1);
            Wiring.SkipWire(x, y + 2);
            Wiring.SkipWire(x + 1, y);
            Wiring.SkipWire(x + 1, y + 1);
            Wiring.SkipWire(x + 1, y + 2);

            if (Wiring.CheckMech(x, y, 30))
            {
               
				Main.time = 0;
				
            }
        }
    }

    public class MoonSummonItem : ModItem
    {
        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.Actuator);
            item.name = "MoonSummonItem";
            item.createTile = mod.TileType("MoonSummon");
            item.placeStyle = 0;
        }
    }

}
