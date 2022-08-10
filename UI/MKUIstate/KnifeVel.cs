namespace MysteriousKnives.UI.MKUIstate
{
    public class KnifeVel : UIState
    {
        internal static bool enable = false;
        public static VanillaItemSlotWrapper[] slots = new VanillaItemSlotWrapper[3];
        public override void OnInitialize()
        {
            DragableUIPanel bg = new();
            bg.Width.Pixels = 200f;
            bg.Height.Pixels = 300f;
            bg.HAlign = 0.5f;
            bg.VAlign = 0.3f;
            Append(bg);

            UIText exup = new("Exup:")
            {
                VAlign = 0.25f,
                HAlign = 0.3f
            };
            bg.Append(exup);

            slots[0] = new()
            {
                VAlign = 0.25f,
                HAlign = 0.7f
            };
            bg.Append(slots[0]);

            UIText oril = new("oril:")
            {
                VAlign = 0.5f,
                HAlign = 0.3f
            };
            bg.Append(oril);

            slots[1] = new()
            {
                VAlign = 0.5f,
                HAlign = 0.7f
            };
            bg.Append(slots[1]);

            UIText tarl = new("tarl:")
            {
                VAlign = 0.75f,
                HAlign = 0.3f
            };
            bg.Append(tarl);

            slots[2] = new()
            {
                VAlign = 0.75f,
                HAlign = 0.7f
            };
            bg.Append(slots[2]);
        }
        internal static int exup, oril, tarl;
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            exup = slots[0].item.stack;
            oril = slots[1].item.stack;
            tarl = slots[2].item.stack;
        }
    }
}
