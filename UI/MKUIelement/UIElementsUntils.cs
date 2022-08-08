namespace MysteriousKnives.UI.MKUIelement
{

    public static class UIElementUtils
    {
        public static void SetPos(this UIElement uie, Vector2 position, float precentX = 0, float precentY = 0)
        {
            uie.SetPos(position.X, position.Y, precentX, precentY);
        }

        public static void SetPos(this UIElement uie, float x, float y, float precentX = 0, float precentY = 0)
        {
            uie.Left.Set(x, precentX);
            uie.Top.Set(y, precentY);
            uie.Recalculate();
        }

        public static Vector2 Size(this UIElement uie)
        {
            return new Vector2(uie.Width.Pixels, uie.Height.Pixels);
        }

        public static void SetSize(this UIElement uie, float width, float height, float precentWidth = 0, float precentHeight = 0)
        {
            uie.Width.Set(width, precentWidth);
            uie.Height.Set(height, precentHeight);
            uie.Recalculate();
        }

        public static float Left(this UIElement uie)
        {
            return uie.Left.Pixels;
        }

        public static float Top(this UIElement uie)
        {
            return uie.Top.Pixels;
        }

        public static float Width(this UIElement uie)
        {
            return uie.Width.Pixels;
        }

        public static float Height(this UIElement uie)
        {
            return uie.Height.Pixels;
        }

        public static float WidthInside(this UIElement uie)
        {
            return uie.Width.Pixels - uie.PaddingLeft - uie.PaddingRight;
        }

        public static float HeightInside(this UIElement uie)
        {
            return uie.Height.Pixels - uie.PaddingTop - uie.PaddingBottom;
        }

        public static float HPadding(this UIElement uie)
        {
            return uie.PaddingLeft + uie.PaddingRight;
        }

        public static float VPadding(this UIElement uie)
        {
            return uie.PaddingTop + uie.PaddingBottom;
        }
    }
}
