using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Castleroid.NPCs
{
	public class ai8 : ModNPC
	{
		public override void SetDefaults()
		{
			npc.name = "ai8";
			npc.displayName = "ai8";
			npc.width = 28;
			npc.height = 20;
			npc.damage = 2;
			npc.defense = 0;
			npc.lifeMax = 2;
			npc.soundHit = 1;
			npc.soundKilled = 16;
			npc.value = 60f;
			npc.knockBackResist = 0.0f;
			npc.aiStyle = 8;
			Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.CyanBeetle];
			aiType = NPCID.CyanBeetle;
			animationType = NPCID.CyanBeetle;
			npc.noGravity = false;
			npc.noTileCollide = true;
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
