using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Microsoft.Xna.Framework;

namespace Castleroid.NPCs
{
    public class rosaria : ModNPC
    {
        int contador = 0;
        int segundos = 0;
        int c_frame = 0;
        int c_segundos = 0;
        bool defesa = false;
        bool andar = true;
        int steps = 0;
        public override void SetDefaults()
        {
            npc.name = "Rosaria";
            npc.displayName = "Rosaria";
            npc.width = 32;
            npc.height = 58;
            npc.damage = 25;
            npc.defense = 16;
            npc.lifeMax = 500;
            npc.soundHit = 7;
            npc.soundKilled = 32;
            npc.noTileCollide = false;//colisões com pisos
            npc.value = 60f;
            npc.knockBackResist = 0f;//1.0f sem resistencia - 0.0000... quanto mais zeros mais resitente
            npc.aiStyle = -1;//Zerada a ai
            Main.npcFrameCount[npc.type] = 2;
        }

        public override float CanSpawn(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.spawnTileY < Main.rockLayer && !Main.dayTime ? 0.5f : 0f;
        }

        public override void FindFrame(int frameHeight)
        {
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
            if(andar == true){
               if(steps == 0){
                    npc.velocity.X = -1;//move para traz
                }else if(steps == 1){
                    npc.velocity.X = 1;//move para frente
                }else{
                    npc.velocity.X = 0;
                    steps = 0;
                }
            }else{
                andar = false;
                npc.velocity.X = 0;
                
            }

            npc.collideX = true;
            npc.collideY = true;
            contador += 1;
            if(contador > 60){//60 numero de fps do jogo 60quadros por segundo
                segundos++;
                contador = 0;
            }
            if(segundos > 2){
                steps++;
                segundos = 0;
            }
        }

    }
}