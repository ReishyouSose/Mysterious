namespace MysteriousKnives.UI.MKUIelement
{
    public class AddiImage : UIImageButton
    {
        public Texture2D _tex;
        public string _text;
        public int id;
        public Color _color;
        public bool active;
        public AddiImage(Asset<Texture2D> tex, string text, Color color) : base(tex)
        {
            _tex = tex.Value;
            _text = text;
            _color = color;
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (ContainsPoint(Main.MouseScreen))
            {
                Main.LocalPlayer.mouseInterface = true;
            }
            Recalculate();
        }
        protected override void DrawSelf(SpriteBatch sb)
        {
            _color *= Main.LocalPlayer.GetModPlayer<MKPlayer>().stars[id] ? 1 : 0.7f;
            for (int i = 0; i < 2; i++)
            {
                var pos = GetDimensions().Position();
                Vector2 origin = _tex.Size() / 2f;
                float scale = 1.3f;
                float lerp = Sin(Main.GameUpdateCount / 20f) / 15 + 1;
                if (IsMouseHovering)
                {
                    sb.Draw(_tex, pos + origin, null, _color, 0, origin, scale * 1.3f * lerp, 0, 0);
                    sb.Draw(_tex, pos + origin, null, Color.White, 0, origin, scale * 1.3f * 0.8f * lerp, 0, 0);
                    Main.hoverItemName = _text;
                }
                else
                {
                    sb.Draw(_tex, pos + origin, null, _color * 0.7f, 0, origin, scale * 1.1f, 0, 0);
                    sb.Draw(_tex, pos + origin, null, Color.White, 0, origin, scale * 0.8f, 0, 0);
                }
            }
        }
    }
}
