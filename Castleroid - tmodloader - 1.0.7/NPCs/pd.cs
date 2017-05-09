using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Castleroid.NPCs
{
	public class pd : ModNPC
	{
		public override void SetDefaults()
		{
			//Caso hardmod 
			npc.name = "pd";
			npc.displayName = "Programação e Diversão";
			npc.width = 18;
			npc.height = 40;
			npc.damage = 14;//x2
			npc.defense = 6;//x2
			npc.lifeMax = 200;//x2
			npc.soundHit = 2;
			npc.soundKilled = 5;
			npc.value = 60f;
			npc.knockBackResist = 0.5f;//x2
			npc.aiStyle = 2;
			Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.Pixie];//ids dos npcs, são literalmente seus nomes no Hero-mod
			aiType = NPCID.Pixie;
			animationType = NPCID.Pixie;
		}

		public override float CanSpawn(NPCSpawnInfo spawnInfo)
		{
			return spawnInfo.spawnTileY < Main.rockLayer && !Main.dayTime ? 0.5f : 0f;
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
//Uma observação que a principio me deixou louco, no hardmod os npcs/monstros tudo que for relacionado a status considere o multiplicador x2
//Não adicionei nenhum drop caso necessário basta solicitar e serão implementados

