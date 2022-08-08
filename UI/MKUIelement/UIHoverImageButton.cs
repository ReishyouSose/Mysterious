using ReLogic.Content;

namespace MysteriousKnives.UI.MKUIelement
{
	public class UIHoverImageButton : UIImageButton
	{
		internal string HoverText;

        public UIHoverImageButton(Asset<Texture2D> texture, string hoverText) : base(texture)
        {
			HoverText = hoverText;
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
