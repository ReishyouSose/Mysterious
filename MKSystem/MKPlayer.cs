namespace MysteriousKnives.MKSystem
{
    public class MKPlayer : ModPlayer
    {
        public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
        {
            if (ModLoader.TryGetMod("miningcracks_take_on_luiafk", out Mod luiafk))
            {
                return new[]
                {
                    new Item(luiafk.Find<ModItem>("UnlimitedEverything").Type)
                };
            }
            else
            {
                return new[]
                {
                    new Item(MKItemID.MKOrigin)
                };
            }
        }
        public float d = 0;
        public override void PreUpdateBuffs()
        {
            if (Main.rand.NextBool(10))
            {
                d = Main.rand.NextFloat(-1, 1);
            }
            Player.GetDamage(ModContent.GetInstance<Mysterious>()) += d;
        }
        public override bool PreItemCheck()
        {
            if (Player.inventory[Player.selectedItem].type == MKItemID.MKOrigin && Player.itemAnimation > 0)
            {
                if (Player.ItemTimeIsZero)
                {
                    Player.ApplyItemTime(Player.inventory[Player.selectedItem]);
                }
                else if (Player.itemTime == 10)
                {
                    SoundEngine.PlaySound(SoundID.Item6);
                    if (Main.netMode == NetmodeID.SinglePlayer)
                    {
                        Player.TeleportationPotion();
                    }
                    else if (Main.netMode == NetmodeID.MultiplayerClient && Player.whoAmI == Main.myPlayer)
                    {
                        NetMessage.SendData(MessageID.RequestTeleportationByServer);
                    }
                }
            }
            return true;
        }
    }
}
