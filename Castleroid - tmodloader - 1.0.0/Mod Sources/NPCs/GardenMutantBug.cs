using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Castleroid.NPCs
{
	public class GardenMutantBug : ModNPC
	{
		
		public override void SetDefaults()
		{
			npc.name = "GardenMutantBug";
			npc.displayName = "Garden Mutant Bug";
			npc.width = 58;
			npc.height = 44;
			npc.damage = 15;
			npc.defense = 2;
			npc.lifeMax = 100;
			npc.soundHit = 5;
			npc.soundKilled = 12;
			npc.value = 60f;
			npc.knockBackResist = 0.0f;
			npc.aiStyle = 41;  //Preferencial, AI do inimigo clonado
			npc.scale = 2;
			Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.Derpling]; //Frames // Derpling é o nome do NPC
			aiType = NPCID.Derpling; // Inteligência Artificial
			animationType = NPCID.Derpling; //Animação
			music = MusicID.Ocean;
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
