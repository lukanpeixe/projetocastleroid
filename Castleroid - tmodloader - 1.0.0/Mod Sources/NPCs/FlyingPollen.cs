using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Castleroid.NPCs
{
	public class FlyingPollen : ModNPC
	{
		public override void SetDefaults()
		{
			npc.name = "FlyingPollen";
			npc.displayName = "Flying Pollen";
			npc.width = 12;
			npc.height = 12;
			npc.damage = 2;
			npc.defense = 0;
			npc.lifeMax = 2;
			npc.soundHit = 1;
			npc.soundKilled = 22;
			npc.value = 60f;
			npc.knockBackResist = 0.0f;
			npc.aiStyle = 5;
			npc.noGravity = true;
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
