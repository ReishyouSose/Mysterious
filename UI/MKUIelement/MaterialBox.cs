namespace MysteriousKnives.UI.MKUIelement
{
    public class MaterialBox : UIElement
    {
        public float _unit;
        public float _height;
        public int id;
        public Item[] list = new Item[12];
        /// <summary>
        /// 最大12个物品
        /// </summary>
        /// <param name="items"></param>
        public MaterialBox(float width = 60, float height = 54, float unit = 5)
        {
            Width.Pixels = width * unit;
            Height.Pixels = height * unit;
            _unit = unit;
            _height = height;
        }
        public void SetList(List<Item> itemlist)
        {
            Item[] materials = new Item[12];
            for (int i = 0; i < itemlist.Count; i++)
            {
                materials[i] = itemlist[i].Clone();
            }
            list[0] = new Item(ItemID.FallenStar, 10).Clone();
            for (int i = 1; i < 12; i++)
            {
                if (materials[i - 1] != null)
                {
                    list[i] = materials[i - 1].Clone();
                }
                else list[i] = null; ;
            }
        }
        internal static bool Enough = true;
        protected override void DrawSelf(SpriteBatch sb)
        {
            Texture2D bg = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/PanelGrayscale").Value;
            var rec = GetDimensions().ToRectangle();
            {//框体
                Utils.DrawSplicedPanel(sb, bg, rec.X, rec.Y, rec.Width,
                    rec.Height, 10, 10, 10, 10, Colors.InventoryDefaultColor);
                for (int i = 1; i < 4; i++)
                {
                    float X = rec.X + i * 0.25f * rec.Width;
                    Utils.DrawLine(sb, new Vector2(X, rec.Y) + Main.screenPosition,
                        new Vector2(X, rec.Y + rec.Height) + Main.screenPosition, Color.Black, Color.Black, 1f);
                }
                for (int i = 1; i < 3; i++)
                {
                    float Y = rec.Y + i * 1 / 3f * rec.Height;
                    Utils.DrawLine(sb, new Vector2(rec.X, Y) + Main.screenPosition,
                        new Vector2(rec.X + rec.Width, Y) + Main.screenPosition, Color.Black, Color.Black, 1f);
                }
            }
            {//内容
                float n = 1f / _height * rec.Height;
                bool enough = true;
                for (int i = 0; i < list.Length; i++)
                {
                    float X = rec.X + ((i % 4) * 0.25f + 0.125f) * rec.Width;
                    float Y = rec.Y + (i / 4) * 1 / 3f * rec.Height;
                    DynamicSpriteFont font = FontAssets.MouseText.Value;
                    if (list[i] != null)
                    {
                        Item item = new();
                        item.SetDefaults(list[i].type);
                        Main.instance.LoadItem(item.type);
                        float oldscale = Main.inventoryScale;
                        Main.inventoryScale = 0.75f;
                        ItemSlot.Draw(sb, ref item, 4, new Vector2(X - 54 * 0.75f / 2f,
                            Y - 45 * 0.75f / 2f + n * 5));
                        Main.inventoryScale = oldscale;
                        item = list[i].Clone();
                        int have = 0;
                        Player player = Main.LocalPlayer;
                        foreach (Item request in player.inventory)
                        {
                            if (request.type == item.type)
                            {
                                have += request.stack;
                            }
                        }
                        if (player.chest != -1)
                        {
                            foreach (Item request in Main.chest[player.chest].item)
                            {
                                if (request.type == item.type)
                                {
                                    have += request.stack;
                                }
                            }
                        }
                        if (have < item.stack) enough = false;
                        string text = $"{(have < item.stack ? have : item.stack)}/{item.stack}";
                        Vector2 size = ChatManager.GetStringSize(font, text, Vector2.One);
                        ChatManager.DrawColorCodedString(sb, font, text,
                            new Vector2(X, Y + ((_height / 3f - 10) * 0.75f + 10) * n),
                            have < item.stack ? Color.Red : Color.Green, 0, size / 2f, Vector2.One);
                        text = item.Name;
                        size = ChatManager.GetStringSize(font, text, Vector2.One);
                        ChatManager.DrawColorCodedString(sb, font, text,
                            new Vector2(X, Y + ((_height / 3f - 10) * 0.3f + 10) * n),
                            Color.White, 0, size / 2f, new Vector2(0.8f - size.X / 800f));
                    }
                    else
                    {
                        string text = "None";
                        Vector2 size = ChatManager.GetStringSize(font, text, Vector2.One);
                        ChatManager.DrawColorCodedString(sb, font, text, new Vector2(X, Y + (0.4f + _height / 6) * n),
                            Color.Gray, 0, size / 2f, Vector2.One);
                    }
                }
                Enough = enough;
            }
        }
        public void Light()
        {
            if (Enough)
            {
                SoundEngine.PlaySound(SoundID.Item4);
                Player player = Main.LocalPlayer;
                player.GetModPlayer<MKPlayer>().stars[id] = true;
                for (int i = 0; i < list.Length; i++)
                {
                    if (list[i] != null)
                    {
                        Item item = list[i].Clone();
                        int stack = item.stack;
                        for (int j = stack; j > 0; j--)
                        {
                            foreach (Item request in player.inventory)
                            {
                                if (request.stack > 0 && request.type == item.type)
                                {
                                    request.stack--;
                                }
                                else continue;
                            }
                            if (player.chest != -1)
                            {
                                foreach (Item request in Main.chest[player.chest].item)
                                {
                                    if (request.stack > 0 && request.type == item.type)
                                    {
                                        request.stack--;
                                    }
                                }
                            }
                        }
                    }
                    else break;
                }
            }
        }
    }
}
