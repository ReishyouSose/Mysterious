namespace MysteriousKnives.Items
{
	public class MKpuppetSummon : ModItem
    {
        public override string Texture => "MysteriousKnives/Pictures/Items/MKpuppetSummon";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("傀儡召唤器");
            Tooltip.SetDefault("于鼠标处召唤一个傀儡\n" +
                "再次使用召回");
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
			Item.autoReuse = false;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.useStyle = ItemUseStyleID.Thrust;
			Item.useTime = 4;
			Item.useAnimation = 4;
			Item.value = Item.sellPrice(0, 1, 0, 0);
			Item.rare = ItemRarityID.Green;
        }
        public override bool CanUseItem(Player player)
        {
            foreach (NPC npc in Main.npc)
            {
                if (npc.boss && npc.active)
                    return false;
                if (npc.type == ModContent.NPCType<MKpuppet>() && npc.active)
                {
                    if (npc.ai[0] == player.whoAmI)
                        npc.life = 0;
                    return false;
                }
            }
            NPC.NewNPCDirect(player.GetSource_ItemUse(Item), (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y,
                ModContent.NPCType<MKpuppet>(), 0, player.whoAmI);
            return true;
        }
    }
}
