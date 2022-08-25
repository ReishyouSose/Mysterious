namespace MysteriousKnives.UI.MKUIstate
{
    public class SkillTree : UIState
    {
        internal static bool enable = false;
        public DragableUIPanel bg;
        public ScrollView view;
        public override void OnInitialize()
        {
            bg = new();
            bg.Width.Pixels = 600f;
            bg.Height.Pixels = 600f;
            bg.HAlign = 0.5f;
            bg.VAlign = 0.5f;
            Append(bg);

            UIPanel side = new();
            side.Width.Pixels = 300f;
            side.Height.Pixels = 400f;
            side.Top.Pixels = 200f;
            side.Left.Pixels = 600f;

            view = new(270, 250);
            view.Top.Pixels = 10f;
            view.HAlign = 0.5f;
            bg.Append(side);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
