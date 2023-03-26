
namespace MysteriousKnives.Items
{
    public class MKPotionStation_Item : ModItem
    {
        public override string Texture => "MysteriousKnives/Pictures/Items/灌注站";
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("诡秘药箱");
			// Tooltip.SetDefault("放在其内的药水效果会持续施加给所有玩家");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 36;
			Item.maxStack = 9999;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.value = Item.buyPrice(0, 1, 0, 0);
			Item.createTile = MKTileID.PotionStation;
			Item.placeStyle = 0;//放置的物块处于哪个帧
		}
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
}
