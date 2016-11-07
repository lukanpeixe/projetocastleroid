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

	public class gosma_shot : ModProjectile{


		public override void SetDefaults(){
		projectile.name = "Tiro de Gosma";//Nome
		projectile.width = 16;
		projectile.height = 16;
		//projectile.magic = true;
		projectile.ranged = true;
		projectile.penetrate = 1;
		projectile.hostile = true;
		projectile.friendly = false;
		projectile.tileCollide = true;//Importante
		projectile.ignoreWater = true;
		projectile.alpha = 255;
		projectile.timeLeft = 128;//Tempo que o projetio fica no jogo
		projectile.aiStyle = 1;//Tipo de projetio olhar escala de ids de projetios
		}

	

		//Copiei e colei xd
		//Mas cria um rastro do projetio
	public override void AI()
	{
		int num666 = 5;//não sei como esta sendo o calculo mas o numero tem influencia
		int num667 = Dust.NewDust(new Vector2(projectile.position.X + (float)num666 + 10,projectile.position.Y + (float)num666),projectile.width - num666 *2, projectile.height - num666 *2,66,0f,0f,0, new Color(0,255,0),1.5f);//Cor da poeira
		
			Main.PlaySound(2,(int)projectile.position.X,(int)projectile.position.Y,20);
		
		
		Main.dust[num667].velocity *= 0.2f;
		Main.dust[num667].velocity += projectile.velocity * 0.2f;
		Main.dust[num667].noGravity = true;
		Main.dust[num667].noLight = true;
		Main.dust[num667].scale = 1.4f;
		
		
	}

}
}
/*
----------------------Logica--------------------
A idéia é fazer um projétio que funcione como um "cuspe" ele vai subir e depois cair,
como o famoso gunbound. Porém o sprite não esta aparecendo não sei o motivo.
Se alguém conseguir por favor entre em contato. Ou melhore o código agradeço...

*/