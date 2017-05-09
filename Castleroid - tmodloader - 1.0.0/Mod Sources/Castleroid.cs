using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using System.Text;

namespace Castleroid
{
 
    public class Castleroid : Mod
    {
        internal bool instantRespawn = false;  
            

        public Castleroid()
        {
        	
            Properties = new ModProperties()
            {
                Autoload = true, 
            };

        }      
	    
	        public class Geral : ModPlayer
	        {	 
	        int num;
	        int onLoad = 0;

	        public void addExamplePerson() 
	     //adiciona no mundo o NPC que foi criado
	        {	     

	                    if (onLoad == 0){
							onLoad =  1;

							num = NPC.NewNPC((Main.spawnTileX + 5) * 16, Main.spawnTileY * 16, mod.NPCType("Example Person"), 0, 0f, 0f, 0f, 0f, 255);
							Main.npc[num].homeTileX = Main.spawnTileX + 5;
							Main.npc[num].homeTileY = Main.spawnTileY;
							Main.npc[num].direction = 1;
							Main.npc[num].homeless = true;													
												
						}  					
						
	        }

	        public override void Kill(double damage, int hitDirection, bool pvp, string deathText)
	        {	           
	                player.respawnTimer = 0;  // Quando o player morre ele renasce instantaneamente	                

	                // player.QuickSpawnItem(2106, 1);	// spaw de um item por ID 	                
	                	                
	                return;	           
	        }

	        class SpawnRateMultiplierGlobalNPC : GlobalNPC
			{
			        float multiplier = 5f;
			        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
			        {
			            spawnRate = (int)(spawnRate / multiplier);
			            maxSpawns = (int)(maxSpawns * multiplier);
			            spawnRate = 0; // impede spawn de animais e monstros
			            maxSpawns = 0; // define número máximo de spawn de animais e monstros			            
			        }

			}		

			public override void ResetEffects()
			{
			//Ativa a regeneração de vida
			int id_buff = BuffID.Regeneration;
			if(player.buffTime[id_buff] > 0)
			{
				//string teste = player.lifeRegen.ToString();
				Main.NewText("Ativado", 255, 240, 20, false);
				player.lifeRegen = 0;
			}else{
				Main.NewText("Desativado", 255, 240, 20, false);
				//player.lifeRegen = 0;
				player.lifeRegenCount = 0;
			}			
				       
				        
                        // quando o personagem der o primeiro pulo, o NPC é adicionado no mapa
						if (player.controlJump == true){
							
							addExamplePerson(); // chama a função para adicionar o NPC no mundo.


						}
						Main.npc[num].position.X = Main.npc[num].position.X;						
			

						Player.tileRangeX = 50; // distancia que pode adicionar blocos
						Player.tileRangeY = 50; // altura que pode adicionar blocos
						player.tileSpeed = 5f; // velocidade de posicionamento de tiles

						//player.lifeRegenCount = 0; // regeneração de life

						//Main.time = 32400.1; // permanece sempre dia
                        Main.time = Main.time-1;// mantem o tempo em looping, travado em um horário estipulado por outros itens

                //Main.time = 0;  // fica noite
                //Outra opção
                //Main.dayTime = true; // fica dia

                // player.statLifeMax = 500; // define a quantidade máxima de vida
                // player.statLife = 50; // define a quantidade atual de vida
                // player.statDefense = 10; define a defesa do jogador.

            }
	    } 
    }
}

