using ReLogic.Content;

namespace MysteriousKnives.UI.MKUIelement
{
	public class UIHoverImageButton : UIImageButton
	{
		internal string HoverText;
		public int id;
        public UIHoverImageButton(Asset<Texture2D> texture, string hoverText) : base(texture)
        {
			HoverText = hoverText;
        }
		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (ContainsPoint(Main.MouseScreen))
			{
				Main.LocalPlayer.mouseInterface = true;
			}
		}
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);

			if (IsMouseHovering)
			{
				Main.hoverItemName = HoverText;
			}
		}
	}
}
