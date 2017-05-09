using Terraria.ID;
using Terraria.ModLoader;

namespace Castleroid.Items.Weapons
{
	public class Hanabira : ModItem
	{
		public override void SetDefaults()
		{
			item.name = "Hanabira";
			item.useTurn = false;
			
			
			item.damage = 8;
			item.melee = true;
			item.width = 24;
			item.height = 24;
			item.toolTip = "ASD";
			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = 3;
			item.knockBack = 0;
			item.value = 0;
			item.rare = 2;
			item.useSound = 1;
			item.autoReuse = false;
			//item.shoot = 405;
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
