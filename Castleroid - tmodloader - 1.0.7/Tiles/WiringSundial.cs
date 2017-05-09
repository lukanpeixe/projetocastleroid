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
    public class WiringSundial : ModTile
    {
        public override void SetDefaults()
        {
            // For some reason, setting tileFrameImportant will cause world gen to fail. Stack Overflow
            //Main.tileFrameImportant[Type] = true;

            Main.tileObsidianKill[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
            TileObjectData.addTile(Type);
            AddMapEntry(new Color(190, 230, 190), "WiringSundial");
            dustType = 11;
            disableSmartCursor = true;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 48, mod.ItemType("WiringSundialItem"));
        }

        public override void HitWire(int i, int j)
        {
            // Find the coordinates of top left tile square through math
            int y = j - (Main.tile[i, j].frameY / 18);
            int x = i - (Main.tile[i, j].frameX / 18);

            Wiring.SkipWire(x, y);
            Wiring.SkipWire(x, y + 1);
            Wiring.SkipWire(x, y + 2);
            Wiring.SkipWire(x + 1, y);
            Wiring.SkipWire(x + 1, y + 1);
            Wiring.SkipWire(x + 1, y + 2);

            if (Wiring.CheckMech(x, y, 30))
            {
               
				Main.time = 32000.1;
				
            }
        }
    }

    public class WiringSundialItem : ModItem
    {
        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.Actuator);
            item.name = "WiringSundial";
            item.createTile = mod.TileType("WiringSundial");
            item.placeStyle = 0;
        }
    }

}
