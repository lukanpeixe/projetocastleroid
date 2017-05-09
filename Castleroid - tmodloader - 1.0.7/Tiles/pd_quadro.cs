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
	public class pd_quadro : ModTile
	{
		public override void SetDefaults()
		{

			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);//Este é o segredo vou tabelar os estilos de objectData
			//desta forma poderão ser criados vários tipos de itens seguindo este modelo.
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.StyleWrapLimit = 36;
			TileObjectData.addTile(Type);
			dustType = 7;
			disableSmartCursor = true;
			AddMapEntry(new Color(200, 200, 200), "Pd Quadro");
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 32, 48, mod.ItemType("pd_quadro"));
		}
		/*
			Abaixo uma função do tipo void que significa que nao precisa de retorno
			O código referece ao sistema de wireless
			Nota: não alterei esse código do mod de exemplo, porém não achei necessário
		*/
		public override void HitWire(int i, int j)
		{
			int y = j - (Main.tile[i, j].frameY / 18);
			int x = i - (Main.tile[i, j].frameX / 18);

			Wiring.SkipWire(x, y);
			Wiring.SkipWire(x, y + 1);
			Wiring.SkipWire(x, y + 2);
			Wiring.SkipWire(x + 1, y);
			Wiring.SkipWire(x + 1, y + 1);
			Wiring.SkipWire(x + 1, y + 2);

			int spawnX = x * 16 + 16;
			int spawnY = (y + 3) * 16;
			int npcIndex = -1;
			if (Wiring.CheckMech(x, y, 30) && NPC.MechSpawn((float)spawnX, (float)spawnY, mod.NPCType("pd")))
			{
				npcIndex = NPC.NewNPC(spawnX, spawnY - 12, mod.NPCType("pd"));//Pd- é um npc/monstro no qual eu criei, peço desculpas pela falta de criatividade
			}
			if (npcIndex >= 0)
			{
				Main.npc[npcIndex].value = 0f;
				Main.npc[npcIndex].npcSlots = 0f;
			}
		}
	}

}
