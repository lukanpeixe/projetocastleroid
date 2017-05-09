using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace Castleroid.NPCs
{
	public class GuardianHornetX : ModNPC
	{
		int contador = 0;//canta os frams
		int segundos = 0;//conta os segundos
		int steps = 0;// conta os passos
				
		public override void SetDefaults()
		{
			npc.name = "GuardianHornetX";
			npc.displayName = "Guardian Hornet";
			Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.Hornet];
			// aiType = NPCID.Hornet;
			animationType = NPCID.Hornet;
			npc.lifeMax = 8;
			npc.width = 34;
			npc.height = 32;
			npc.damage = 8;
			npc.defense = 0;
			npc.soundHit = 1;
			npc.soundKilled = 16;
			npc.value = 60f;
			npc.knockBackResist = 0.0f;
			npc.aiStyle = -1;
			npc.noGravity = true;
			npc.noTileCollide = false;
			
		
		}
	/*
	Modifica a escala de HP/Dano de acordo com o tipo de mapa (EXPERT)
	Desisti de mudar esta questão, deixando o mapa como Normal Pré hardmode, devido problemas bizarros de multiplicação caso o HP e Dano seja muito baixo
	
	public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.lifeMax = (int)(npc.lifeMax / 2);
			npc.damage = (int)(npc.damage / 2);
		}
	*/
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
	
	public override void AI()
	{
			if(steps == 0)
			{
				npc.velocity.X = -1;//move para traz
			}
			else if(steps == 1)
			{
				npc.velocity.X = 1;//move para frente
			}
		
			else
			{
				npc.velocity.X = 0;
				steps = 0;
			}
			npc.collideX = true;
			npc.collideY = true;
			contador += 1;
			if(contador > 240)
			{//60 numero de fps do jogo 60quadros por segundo
				steps++;
				segundos++;
				contador = 0;
			}
				if(segundos > 3)
			{
					
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
			
            {
                npc.TargetClosest(true);
            }
           




		   npc.netUpdate = true;				

	
		}
		
	}
	
	public override void FindFrame(int frameHeight)
{
    if ((double) npc.velocity.X > 0.0)
    {
        npc.spriteDirection = 1;
        npc.rotation = (float) Math.Atan2((double) npc.velocity.Y, (double) npc.velocity.X);
    }
    if ((double) npc.velocity.X < 0.0)
    {
        npc.spriteDirection = -1;
        npc.rotation = (float) Math.Atan2((double) npc.velocity.Y, (double) npc.velocity.X) + 3.14f;
    }
}
	
}	}