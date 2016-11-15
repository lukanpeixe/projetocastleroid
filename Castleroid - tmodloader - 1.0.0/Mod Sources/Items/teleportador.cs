using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Castleroid.Items
{
	/*
	Mudar o item de teleportador para o nome desejado
	Ex-> public class novo_nome : ModItem
	------------------Importante---------------
	OS ARQUIVOS DEVEM POSSUIR NOMES EQUIVALENTES
	Ex-> teleportador.cs e teleportador.png
	*/
	public class teleportador : ModItem
	{
		public override void SetDefaults()
		{
			item.name = "Teleportador";//Nome do item
			item.width = 20;//Largura do item
			item.height = 20;//Altura do item
			item.useStyle = 2;//Irei descobrir sua função XD
			item.maxStack = 1;//Máximo de itens que o personagem pode obter
			item.useAnimation = 17;//Animação que será usada pelo personagem
			item.useTime = 1;//Quantidade de vezes que o iten será usado por vez
			item.consumable = true;//aplica se o item vai ser consumivel ou não
			AddTooltip("Este item pode teleportar o usuário para a coordenada desejada");//Informação do item no jogo
			item.value = 1;//Valor em coppers
			item.rare = 4;//Raridade do item
		}


		public override bool UseItem(Player player)
		{
			int x = 5000; // coordenada x de teleporte
			int y = 1500; // coordenada y de teleporte

			string posicao_x = x.ToString();// converte variavel x para o formato de texto
			string posicao_y = y.ToString();// converte variavel y para o formato de texto
			Player jogador = Main.player[Main.myPlayer];//atributos do jogador

					
			jogador.position = new Vector2(x,y);//Faz o jogar ir a opção desejada
			Main.NewText("Teleportou..!", 255, 240, 20, false);
			Main.NewText(posicao_x, 255, 240, 20, false);// Texto com posicao x
			Main.NewText(posicao_y, 255, 240, 20, false);// Texto com posicao y

			return true;

		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.DirtBlock, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();	

		}
	}
}

/*
	-----------Duvidas------------
	jefferson-l-bernardo@hotmail.com
	https://twitter.com/@Kemadoloco
	https://www.youtube.com/channel/UCv5TZmOl8eh0l1mmGVK3_Lw
	https://github.com/Kemado
*/