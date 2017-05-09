using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Castleroid.NPCs
{
	public class LadyRosarina : ModNPC
	{
		public override void SetDefaults()
		{
			npc.name = "LadyRosarina";
			npc.displayName = "Lady Rosarina";
			npc.width = 35;
			npc.height = 42;
			npc.scale = 0.75f;
			npc.damage = 15;
			npc.defense = 0;
			npc.lifeMax = 200;
			npc.soundHit = 5;
			npc.soundKilled = 9;
			npc.value = 60f;
			npc.knockBackResist = 0.0f;
			npc.aiStyle = 22;
			npc.noGravity = true;
			npc.noTileCollide = false;
			Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.GiantFlyingFox];
			//aiType = NPCID.CyanBeetle;
			animationType = NPCID.GiantFlyingFox;
			
		}
	
	public override void HitEffect(int hitDirection, double damage)
		{
			for (int i = 0; i < 100; i++)
			{
				int dustType = Main.rand.Next(208, 208);
				int dustIndex = Dust.NewDust(npc.position, npc.width, npc.height, dustType);
				Dust dust = Main.dust[dustIndex];
				dust.velocity.X = dust.velocity.X + Main.rand.Next(0, 0) * 0.01f;
				dust.velocity.Y = dust.velocity.Y + Main.rand.Next(0, 0) * 0.01f;
				dust.scale *= 1f + Main.rand.Next(0, 0) * 0.01f;
			}
		}
	
	}
}
