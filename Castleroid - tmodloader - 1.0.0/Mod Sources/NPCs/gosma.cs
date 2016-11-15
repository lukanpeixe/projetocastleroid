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
            npc.width = 32;
            npc.height = 144;
            npc.damage = 14;
            npc.defense = 6;
            npc.lifeMax = 200;
            npc.soundHit = 7;
            npc.soundKilled = 5;
            npc.noTileCollide = false;//colisões com pisos
            npc.value = 60f;
            npc.knockBackResist = 0f;//1.0f sem resistencia - 0.0000... quanto mais zeros mais resitente
            npc.aiStyle = -1;//Zerada a ai
            Main.npcFrameCount[npc.type] = 3;
        }

        public override float CanSpawn(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.spawnTileY < Main.rockLayer && !Main.dayTime ? 0.5f : 0f;
        }

        public override void FindFrame(int frameHeight)
        {
           npc.frameCounter -= 1F;// Velocidade do sprite
           npc.frameCounter %= Main.npcFrameCount[npc.type];
           int frame = (int)npc.frameCounter;
           npc.frame.Y = frame * frameHeight;

           npc.spriteDirection = npc.direction;
        }

        public override void AI()
        {
        //Virar sprites----------------------------------------------------------------------
            if (npc.velocity.X < 0)
            {
                npc.spriteDirection = -1;//para esquerda ou -x
            }else{
                npc.spriteDirection = 1;//para direita ou +x
            }
        //---------------------------------------------------------------------------------------
        
            npc.collideX = true;
            npc.collideY = true;
            contador += 1;
            if(contador > 60){//60 numero de fps do jogo 60quadros por segundo
                segundos++;
                contador = 0;
            }
                if(segundos > 3){
                    npc.velocity.Y = -10;//Força do pulo
                    segundos = 0;
                }

        }

    }
}