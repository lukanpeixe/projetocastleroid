using Terraria.ID;
using Terraria.ModLoader;

namespace Castleroid.Items
{
	public class pd_quadro_item : ModItem
	{
		public override void SetDefaults()
		{
			item.name = "PD Quadro";
			item.width = 30;
			item.height = 30;
			item.maxStack = 99;
			AddTooltip("Quadro para invocar monstros");
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 5;
			item.useStyle = 1;
			item.consumable = true;
			item.value = 100000;
			item.createTile = mod.TileType("pd_quadro");
			item.rare = 4;
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