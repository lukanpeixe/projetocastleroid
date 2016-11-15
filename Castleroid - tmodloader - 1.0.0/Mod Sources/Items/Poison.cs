using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Castleroid.Items
{
	public class Poison : ModItem
	{
		public override void SetDefaults()
		{
			item.name = "Poison";
			item.width = 20;
			item.height = 20;
			item.useStyle = 2;
			item.maxStack = 1;
			item.useAnimation = 17;
			item.useTime = 1;
			item.consumable = true;
			item.useTurn = true;
			AddTooltip("PRE-RI-GO");
			item.value = 60606;
			item.rare = 4;
			item.buffType = 1;
			item.buffTime = 36000;
			
			
		}


		public override bool UseItem(Player player)
		{
			player.statLifeMax = 40;
												
			return true;


		}
			
		
		
	}
}

