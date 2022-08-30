namespace MysteriousKnives.UI.MKUIelement
{
    public class ButtonUI : UIElement
    {
        private static Asset<Texture2D> Background;
        private static Asset<Texture2D> BackgroundBorder;

        public bool _playSound;
        public int[] id;

        public UIImage UIImage;
        public UIText UIText;

        public ButtonUI(string text)
        {
            id ??= new int[1];
            _playSound = true;
            Background ??= Main.Assets.Request<Texture2D>("Images/UI/CharCreation/PanelGrayscale");
            BackgroundBorder ??= Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelBorder");

            UIText = new(text)
            {
                VAlign = 0.5f,
                HAlign = 0.5f
            };
            Append(UIText);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (ContainsPoint(Main.MouseScreen))
            {
                Main.LocalPlayer.mouseInterface = true;
            }
            if (IsMouseHovering)
            {
                if (_playSound)
                {
                    _playSound = false;
                    SoundEngine.PlaySound(SoundID.MenuTick);
                }
            }
            else
            {
                _playSound = true;
            }
        }

        protected override void DrawSelf(SpriteBatch sb)
        {
            var rectangle = GetDimensions().ToRectangle();
            if (IsMouseHovering)
            {
                Utils.DrawSplicedPanel(sb, BackgroundBorder.Value, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, 10, 10, 10, 10, Color.White);
            }
            else Utils.DrawSplicedPanel(sb, Background.Value, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, 10, 10, 10, 10, Colors.InventoryDefaultColor);
        }

        public void SetText(string text)
        {
            UIText.SetText(text);
        }
    }
}
