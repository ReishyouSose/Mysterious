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
        public bool[] stars = new bool[42];
        public bool[] level = new bool[9];
        public bool PlyStar(int[] id)
        {
            bool light = true;
            for (int i = 0; i < id.Length; i++)
            {
                if (!Player.GetModPlayer<MKPlayer>().stars[id[i]])
                {
                    light = false;
                }
            }
            return light;
        }
        public override void ResetEffects()
        {
            int[] id = new int[1];
            bool front = true;
            for (int i = 0; i < 9; i++)
            {
                switch (i)
                {
                    case 0: id = new int[] { 0, 7, 10, 15 }; break;
                    case 1: id = new int[] { 1, 16, 19, 25, 32 }; break;
                    case 2: id = new int[] { 2, 8, 11, 17, 20, 26, 33, 38 }; break;
                    case 3: id = new int[] { 3, 12, 21, 27, 34, 39 }; break;
                    case 4: id = new int[] { 4, 18, 22, 28, 35, 40 }; break;
                    case 5: id = new int[] { 5, 13, 29, 36 }; break;
                    case 6: id = new int[] { 9, 30, 37, 41 }; break;
                    case 7: id = new int[] { 6, 23 }; break;
                    case 8: id = new int[] { 14, 24, 31 }; break;
                };
                if (i > 0)
                {
                    front = Player.GetModPlayer<MKPlayer>().level[i - 1];
                }
                Player.GetModPlayer<MKPlayer>().level[i] = PlyStar(id) && front;
            }
        }
        public override void UpdateVisibleAccessories()
        {
            int _level = 0;
            bool shoot = Main.rand.NextBool(100);
            Item item = null;
            for (int i = 0; i < 9; i++)
            {
                if (Player.GetModPlayer<MKPlayer>().level[i]) _level++;
            }
            switch (_level)
            {
                case 1: item = new Item(MKItemID.MK01).Clone(); break;
                case 2: item = new Item(MKItemID.MK02).Clone(); break;
                case 3: item = new Item(MKItemID.MK03).Clone(); break;
                case 4: item = new Item(MKItemID.MK04).Clone(); break;
                case 5: item = new Item(MKItemID.MK05).Clone(); break;
                case 6: item = new Item(MKItemID.MK06).Clone(); break;
                case 7: item = new Item(MKItemID.MK07).Clone(); break;
                case 8: item = new Item(MKItemID.MK08).Clone(); break;
                case 9: item = new Item(MKItemID.MK09).Clone(); break;
            }
            if (Player.ItemAnimationActive && shoot && _level > 0 && ModLoader.TryGetMod("CalamityMod", out Mod mod))
            {
                Projectile proj = Projectile.NewProjectileDirect(Entity.GetSource_ItemUse(item), Player.Center,
                   Vector2.One.RotatedBy(ManyPI(1 / 180f) * Main.rand.Next(0, 360)), MKProjID.Core,
                   (int)Player.GetDamage(ModContent.GetInstance<Mysterious>()).ApplyTo(item.damage),
                   0, Player.whoAmI);
            }
        }
        public override void SaveData(TagCompound tag)
        {
            for (int i = 0; i < stars.Length; i++)
            {
                tag[$"stars{i}"] = stars[i];
            }
        }
        public override void LoadData(TagCompound tag)
        {
            for (int i = 0; i < stars.Length; i++)
            {
                stars[i] = tag.GetBool($"stars{i}");
            }
        }
    }
}
