using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace Castleroid.NPCs
{
    public class elusive_butterfly : ModNPC
    {
        Player player = Main.player[Main.myPlayer];
        float x_jogador = 0;
        float x_npc = 0;
        float y_npc = 0;
        float aux_pos = 0;
        int contador = 0;
        int segundos = 0;
        int c_frame = 0;
        int c_segundos = 0;
        bool defesa = false;
        bool mostrar = true;
        int steps = 0;
        public override void SetDefaults()
        {
            npc.name = "Elusive Butterfly";
            npc.displayName = "Elusive Butterfly";
            npc.width = 80;
            npc.height = 50;
            npc.damage = 10;
            npc.defense = 2;
            npc.lifeMax = 500;
            npc.soundHit = 7;
            npc.soundKilled = 5;
            npc.noTileCollide = true;//colisões com pisos
            npc.value = 60f;
            npc.knockBackResist = 0.8f;//1.0f sem resistencia - 0.0000... quanto mais zeros mais resitente
            npc.aiStyle = 2;
            aiType = NPCID.Moth;
            Main.npcFrameCount[npc.type] = 3;
            npc.noGravity = true;
        }

        public override float CanSpawn(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.spawnTileY < Main.rockLayer && !Main.dayTime ? 0.5f : 0f;
        }  

        public override void FindFrame(int frameHeight)
        {
            x_npc = npc.position.X;
            y_npc = npc.position.Y;
            //----------------Animação ---------------------
            //Muda o quadro de acordo com a qtde de npcFrameCount juntamente com o tamanho da imagem
            npc.frameCounter += 0.09;// Velocidade do sprite
            npc.frameCounter %= Main.npcFrameCount[npc.type];
            int f = (int)npc.frameCounter;
            npc.frame.Y = f * frameHeight;
            npc.spriteDirection = npc.direction;
            //---------------------------------------------------------------------
            c_frame++;
            if(c_frame > 10){//60 numero de fps do jogo 60quadros por segundo
                c_segundos++;
                c_frame = 0;
            }

            if(c_segundos > 0 && c_segundos < 3){
                npc.alpha += 20;//0 = opacidade maxima e 255= transparente
                //Main.npc.DrawHealthBar(x_npc, y_npc, npc.life, npc.lifeMax, npc.alpha, 1);
                mostrar = false;
            }
            
            if(c_segundos > 2 && c_segundos < 4){
                npc.alpha = 50;//0 = opacidade maxima e 255= transparente
                mostrar = true;
            }
            

           if(c_segundos > 4){
                c_segundos = 0;
            }
        }

    }
}