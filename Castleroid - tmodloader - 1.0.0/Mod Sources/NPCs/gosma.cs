using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Microsoft.Xna.Framework;

namespace Castleroid.NPCs
{
	public class gosma : ModNPC
	{
		int contador = 0;
		int segundos = 0;
		public override void SetDefaults()
		{
			npc.name = "Gosma";
			npc.displayName = "Gosma";
			npc.width = 18;
			npc.height = 40;
			npc.damage = 14;
			npc.defense = 6;
			npc.lifeMax = 200;
			npc.soundHit = 1;
			npc.soundKilled = 2;
			npc.value = 60f;
			npc.knockBackResist = 50f;
			npc.aiStyle = -1;//Zerada a ai
		}

		public override float CanSpawn(NPCSpawnInfo spawnInfo)
		{
			return spawnInfo.spawnTileY < Main.rockLayer && !Main.dayTime ? 0.5f : 0f;
		}

		public override void AI()
		{
			npc.collideX = true;
			contador += 1;

			float vel_x = npc.position.X;
			float vel_y = npc.position.Y;
			if(contador > 60){//60 numero de fps do jogo 60quadros por segundo
				segundos++;
				contador = 0;
			}
				if(segundos > 3){
					//vel_x += 16;
					vel_y -= 64;//plano cartesiano o ponto de horigem é diferente - 4 blocos altura
					segundos = 0;
				}
			string s = segundos.ToString();// converte variavel segundos para o formato de texto
			npc.position = new Vector2(vel_x,vel_y);
			Main.NewText(s, 255, 240, 20, false);// Texto com posicao x
			

		}

	}
}
/*
	-----------------Logica/objetivo------------------
	O objetivo é fazer com que o npc simule a gravidade.
	vou olhar na documentação do XNA, pois se for similar a do Game Maker 
	teremos npcs totalmente customizados.
	O npc salta após 3 s porem não se aplica a gravidade corretamente, poderia fazer manual, mas deixaria de ser terraria
	e a gravidade não ficaraia como a de alguns npcs. Enfim estoru trabalhando nisso se alguem puder me ajudar, tipo um código 
	de aiStyle seria ótimo, mas vou trabalhar mais.

*/