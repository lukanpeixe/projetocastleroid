using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Castleroid.NPCs
{
	public class IllusionPollen : ModNPC
	{
		public override void SetDefaults()
		{
			npc.name = "IllusionPollen";
			npc.displayName = "Illusion Pollen";
			npc.width = 24;
			npc.height = 24;
			npc.damage = 4;
			npc.defense = 0;
			npc.lifeMax = 6;
			npc.soundHit = 1;
			npc.soundKilled = 22;
			npc.value = 60f;
			npc.knockBackResist = 0.0f;
			npc.aiStyle = 5;
			npc.alpha = 130;
			npc.behindTiles = true;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.scale = 2;
			Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.Bee];
			aiType = NPCID.Bee;
			animationType = NPCID.Bee;
		}
	
	public override void HitEffect(int hitDirection, double damage)
		{
			for (int i = 0; i < 10; i++)
			{
				int dustType = Main.rand.Next(139, 143);
				int dustIndex = Dust.NewDust(npc.position, npc.width, npc.height, dustType);
				Dust dust = Main.dust[dustIndex];
				dust.velocity.X = dust.velocity.X + Main.rand.Next(-50, 51) * 0.01f;
				dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-50, 51) * 0.01f;
				dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
			}
		}
	
	}
}
