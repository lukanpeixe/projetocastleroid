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
		int steps = 0;
		public override void SetDefaults()
		{
			npc.name = "Gosma";
			npc.displayName = "Gosma";
			npc.width = 32;
			npc.height = 4;
			npc.damage = 14;
			npc.defense = 6;
			npc.lifeMax = 200;
			npc.soundHit = 7;
			npc.soundKilled = 5;
			npc.noTileCollide = false;//colisões com pisos
			npc.value = 60f;
			npc.knockBackResist = 0f;//1.0f sem resistencia - 0.0000... quanto mais zeros mais resitente
			npc.aiStyle = -1;//Zerada a ai
		}

		public override float CanSpawn(NPCSpawnInfo spawnInfo)
		{
			return spawnInfo.spawnTileY < Main.rockLayer && Main.dayTime ? 0.1f : 0f;
		}

		public override void AI()
		{
			if(steps == 0){
				npc.velocity.X = -1;//move para traz
			}else if(steps == 1){
				npc.velocity.X = 1;//move para frente
			}else{
				npc.velocity.X = 0;
				steps = 0;
			}
			npc.collideX = true;
			npc.collideY = true;
			contador += 1;
			if(contador > 60){//60 numero de fps do jogo 60quadros por segundo
				steps++;
				segundos++;
				contador = 0;
			}
				if(segundos > 3){
					npc.velocity.Y = -7;//Força do pulo
					segundos = 0;
				}
				
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