using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Castleroid.NPCs
{
	public class gosma_atirador : ModNPC
	{
		int contador = 0;//canta os frams
		int segundos = 0;//conta os segundos
		int steps = 0;// conta os passos
		public override void SetDefaults()
		{
			npc.name = "Gosma Atirador";
			npc.displayName = "Gosma Atirador";
			npc.width = 32;
			npc.height = 4;
			npc.damage = 20;
			npc.defense = 6;
			npc.lifeMax = 100;
			npc.soundHit = 7;
			npc.soundKilled = 4;
			npc.noTileCollide = false;//colisões com pisos
			npc.value = 60f;
			npc.knockBackResist = 0f;//1.0f sem resistencia - 0.0000... quanto mais zeros mais resitente
			npc.aiStyle = -1;//Zerada a ai
		}

		public override float CanSpawn(NPCSpawnInfo spawnInfo)
		{
			return spawnInfo.spawnTileY < Main.rockLayer && Main.dayTime ? 0.1f : 0f;//Vou testar e melhorar como ele spawnna
		}

		public override void AI(){
			int damage = 5;//Adiciona o dano ao projetio
            Player P = Main.player[npc.target];// Priorisa o target do personagem em testes ainda
               
	
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
					
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                npc.TargetClosest(true);
            }
            npc.netUpdate = true;

                float Speed = 20f;  //projectile speed
                Vector2 vector8 = new Vector2(npc.position.X, npc.position.Y + -10);//Posição de onde vai sair o projetio
                int type = mod.ProjectileType("gosma_shot");  //nome do projetio
                Main.PlaySound(23, (int)npc.position.X, (int)npc.position.Y, 17);//O som emitido
                float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));//faz o sprite do projetio rodar
				int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
					//não sei ao certo mas acredito que instancia o projetio
					//npc.velocity.Y = -7;//Força do pulo
					segundos = 0;
				}
				
		}

	}
}

/*//Gosma atira
				 Player P = Main.player[npc.target];
           		 if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active){
               		npc.TargetClosest(true);
            	}
            	npc.netUpdate = true;
				float Speed = 20f;  //projectile speed
                Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                int damage = 10;  //projectile damage
                int type = mod.ProjectileType("gosma_shot");  //put your projectile
                Main.PlaySound(9, (int)npc.position.X, (int)npc.position.Y, 17);
                float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
   

*/

/*
	-----------------Logica/objetivo------------------
	O objetivo é fazer com que o npc simule a gravidade.
	vou olhar na documentação do XNA, pois se for similar a do Game Maker 
	teremos npcs totalmente customizados.
	O npc salta após 3 s porem não se aplica a gravidade corretamente, poderia fazer manual, mas deixaria de ser terraria
	e a gravidade não ficaraia como a de alguns npcs. Enfim estoru trabalhando nisso se alguem puder me ajudar, tipo um código 
	de aiStyle seria ótimo, mas vou trabalhar mais.

*/