using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;

namespace MysteriousKnives.Items
{
    public class ExampleChest_Item : ModItem
    {
        public override string Texture => "MysteriousKnives/Pictures/Items/Chest_Item";
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("诡秘药箱");
			Tooltip.SetDefault("放在其内的药水效果会持续施加给所有玩家");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 22;
			Item.maxStack = 1;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.value = Item.buyPrice(0, 1, 0, 0);
			Item.createTile = ModContent.TileType<ExampleChest>();
			// Item.placeStyle = 1; // Use this to place the chest in its locked style
		}/*
        public override bool CanUseItem(Player player)
        {
            for (int i = 0; i < 8000; i++)
            {
				if (Main.chest[i].name == "诡秘药箱")
                {
					Main.NewText("每个世界只能放置一个诡秘药箱！");
					return false;
                }
            }
			return true;
        }*/
        public override void AddRecipes()
		{
			if (ModLoader.TryGetMod("CalamityMod", out Mod calamitymod))
			{
				Recipe recipe = CreateRecipe();
				recipe.AddIngredient(ItemID.Wood, 999);
				recipe.AddIngredient(ItemID.StoneBlock, 999);
				recipe.AddIngredient(ItemID.GuideVoodooDoll, 1);
				recipe.AddTile(TileID.WorkBenches);
				recipe.ReplaceResult(this, 1);
				recipe.Register();
			}
		}
	}

	public class ExampleChestKey : ModItem
	{
		public override string Texture => "MysteriousKnives/Pictures/Items/Key";
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3; // Biome keys usually take 1 item to research instead.
		}

		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.GoldenKey);
			Item.width = 14;
			Item.height = 20;
			Item.maxStack = 99;
		}
	}
}
