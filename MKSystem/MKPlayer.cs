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
                    new Item(MKItemsID.MKOrigin)
                };
            }
        }
        public override void OnEnterWorld(Player player)
        {
            MKPotionStationUI.enable = false;
            PotionButton.enable = false;
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
            if (Player.inventory[Player.selectedItem].type == MKItemsID.MKOrigin && Player.itemAnimation > 0)
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

        public override void SaveData(TagCompound tag)
        {
            var itemlist = new List<(int type, int stack)>();
            foreach (VanillaItemSlotWrapper slot in MKPotionStationUI.slots)
            {
                itemlist.Add((slot.item.type, slot.item.stack));
            }
            tag["itemtype"] = itemlist.Select(t => t.type).ToList();
            tag["itemstack"] = itemlist.Select(t => t.stack).ToList();
            var list = new List<(int type, int stack)>();
            foreach (VanillaItemSlotWrapper slot in KnifeVel.slots)
            {
                list.Add((slot.item.type, slot.item.stack));
            }
            tag["type"] = list.Select(t => t.type).ToList();
            tag["stack"] = list.Select(t => t.stack).ToList();
        }
        public override void LoadData(TagCompound tag)
        {
            var itemtype = tag.Get<List<int>>("itemtype");
            var itemstack = tag.Get<List<int>>("itemstack");
            List<(int type, int stack)> itemlist = itemtype.Zip(itemstack, (k, v) => (type: k, stack: v)).ToList();
            var type = tag.Get<List<int>>("type");
            var stack = tag.Get<List<int>>("stack");
            List<(int type, int stack)> list = type.Zip(stack, (k, v) => (type: k, stack: v)).ToList();
            for (int i = 0; i < list.Count; i++)
            {
                KnifeVel.slots[i].item.SetDefaults(list[i].type);
                KnifeVel.slots[i].item.stack = list[i].stack;
            }

            for (int i = 0; i < itemlist.Count; i++)
            {
                MKPotionStationUI.slots[i].item.SetDefaults(itemlist[i].type);
                MKPotionStationUI.slots[i].item.stack = itemlist[i].stack;
            }
            /*var bufftype = tag.Get<List<int>>("bufftype");
            var buffname = tag.Get<List<string>>("buffname");
            bufflist = bufftype.Zip(buffname, (k, v) => ( bufftype: k, buffname: v )).ToList();*/

        }
    }
}
