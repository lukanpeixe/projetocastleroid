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
    public class boss_1 : ModNPC
    {
        Player player = Main.player[Main.myPlayer];

        float x_jogador = 0;
        float x_npc = 0;
        float y_npc = 0;
        float aux_pos = 0;
        float colisao = 0;
        int contador = 0;
        int segundos = 0;
        int c_frame = 0;
        int c_segundos = 0;
        bool defesa = false;
        bool andar = true;
        bool existe = true;
        bool aux_contador = false;
        int steps = 0;
        bool vivo = false;
        int velocidade = 2;//Velocidade do boss


        public override void SetDefaults()
        {

            npc.name = "boss_1";
            npc.displayName = "Boss";
            npc.width = 64;
            npc.height = 64;
            npc.damage = 64;
            npc.defense = 16;
            npc.lifeMax = 1000;
            npc.soundHit = 7;
            npc.soundKilled = 32;
            npc.noTileCollide = false;//colisões com pisos
            npc.value = 60f;
            npc.knockBackResist = 0f;//1.0f sem resistencia - 0.0000... quanto mais zeros mais resitente
            npc.aiStyle = -1;//Zerada a ai
            Main.npcFrameCount[npc.type] = 1;//Quantidade de frames no sprite que o boss possui
            npc.noGravity = true;//Gravidade do boss
        }

        public override float CanSpawn(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.spawnTileY < Main.rockLayer && !Main.dayTime ? 0.5f : 0f;
        }

        public override void FindFrame(int frameHeight)
        {

            /*

            c_frame++;
            if(c_frame > 60){//60 numero de fps do jogo 60quadros por segundo
                c_segundos++;
                c_frame = 0;
            }
            if(defesa == true){
                npc.frame.Y = 0 *frameHeight;
                npc.defense = 9999;//muda a defesa, vou criar um buff que torna o dano em 0
                npc.velocity.X = 0;
                andar = false;
            }else{
                npc.frame.Y = 1 *frameHeight;
                npc.defense = 16;
                andar = true;
            }

            if(c_segundos > 2 && c_segundos < 6){
                defesa = true;
            }else{
                defesa = false;
            }
            if(c_segundos > 6){
                c_segundos = 0;
            }

            */
        }
        public virtual bool CheckActive(){
            return true;
        }

        public override void AI()
        {

            npc.noGravity = true;//REmove a gravidade do boss

            //npc.TargetClosest(true); //Seleciona o alvo do npc

//------------------------Temporisador de colisão-------------------------------------------
/*
    Logica do tempo - O tempo é ativado quando o aux_contador for falso
    O contador limita a colisão com o bloco à um frame, pois sem essa verificação ele colide
    e continua a colisão nos proximos frames
*/
            if( c_segundos > 1){
                aux_contador = true;
                c_segundos = 0;
            }
            if(aux_contador == false){
                c_frame++;
            if(c_frame > 60){//60 numero de fps do jogo 60quadros por segundo
                c_segundos++;
                c_frame = 0;
            }
        }
//------------------------------------------------------------------------------------------


//------------------------Verifica colisão no eixo x----------------------------------------
/*
    Logica da movimentação - Se o npc colidir no eixo x e o contador for verdadeiro ele muda
    a direçao do sprite e a direção que o npc se movimenta
*/
        if(npc.collideX && aux_contador){
            velocidade *= -1;//Multiplica o valor da velocidade: 1*(-1)=-1   -1*(-1)=1
            npc.spriteDirection *= -1;//Muda o sprite de direção: 1*(-1)=-1   -1*(-1)=1
            aux_contador = false;//desativa o contador
        }
        npc.velocity.X = velocidade;//atualiza a velocidade

        //string teste = npc.direction.ToString();

    }

}
}