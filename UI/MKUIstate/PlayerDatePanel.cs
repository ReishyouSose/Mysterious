namespace MysteriousKnives.UI.MKUIstate
{
    public class PlayerDatePanel : UIState
    {
        internal static bool enable = false;
        public override void OnInitialize()
        {
            Vector2 size = ChatManager.GetStringSize(FontAssets.MouseText.Value, "点", new Vector2(1f));
            DragableUIPanel panel = new();
            panel.Width.Set(size.X * 20, 0f);
            panel.Height.Set(size.Y + 10, 0f);
            panel.HAlign = 0.5f;
            panel.VAlign = 0.2f;
            Append(panel);

            mult = new UIText("");
            mult.Left.Set(10f, 0f);
            mult.VAlign = 0.5f;
            panel.Append(mult);

            UIText boost = new("增加");
            boost.Left.Set(size.X * 14, 0f);
            boost.VAlign = 0.5f;
            boost.OnClick += Boost_Click;
            panel.Append(boost);

            UIText reduce = new("减少");
            reduce.Left.Set(size.X *17, 0);
            reduce.VAlign = 0.5f;
            reduce.OnClick += Reduce_Click;
            panel.Append(reduce);
        }
        public UIText mult;
        internal static int mul = 0;
        public static void Boost_Click(UIMouseEvent evt, UIElement listeningElement)
        {
            mul++;
            SoundEngine.PlaySound(SoundID.MenuTick);
        }
        public static void Reduce_Click(UIMouseEvent evt, UIElement listeningElement)
        {
            if (mul > 0)
            {
                mul--;
                SoundEngine.PlaySound(SoundID.MenuTick);
            }
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            mult.SetText($"加成倍率：{mul}");
        }
    }
}
