namespace MysteriousKnives.Items
{
    public class Test : ModItem
    {
        public override string Texture => "MysteriousKnives/Pictures/Items/MK01";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("测试武器");
            Tooltip.SetDefault("用于测试新加的东西");
        }
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.DamageType = DamageClass.Melee;
            Item.damage = 100;
            Item.UseSound = SoundID.Item1;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.shoot = ModContent.ProjectileType<RBsphere>();
            Item.shootSpeed = 10f;
            Item.UseSound = SoundID.Item113;
        }
    }
}
