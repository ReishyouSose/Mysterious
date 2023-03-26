namespace MysteriousKnives.UI.MKUIstate
{
    public class SkillButton : UIState
    {
        internal static bool enable;
        public UIHoverImageButton button;
        public override void OnInitialize()
        {
            button = new(GetT2D("UI/SkillTree/彩"), "技能树");
            button.Width.Set(24, 0);
            button.Height.Set(24, 0);
            button.HAlign = 0.895f;
            button.VAlign = 0.377f;
            button.OnLeftClick += Button_OnClick;
            Append(button);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);/*
            Asset<Texture2D> t1, t2, t3, t4;
            t1 = GetT2D("MysteriousKnives/Pictures/UI/SkillTree/高亮彩");
            t2 = GetT2D("MysteriousKnives/Pictures/UI/SkillTree/高亮灰");
            t3 = GetT2D("MysteriousKnives/Pictures/UI/SkillTree/彩");
            t4 = GetT2D("MysteriousKnives/Pictures/UI/SkillTree/灰");
            button.SetImage(IsMouseHovering ? (enable ? t1 : t2) : (enable ? t3 : t4));*/
        }
        public static void Button_OnClick(UIMouseEvent evt, UIElement listeningElement)
        {
            SkillTree.enable = !SkillTree.enable;
            SoundEngine.PlaySound(SoundID.Item4);
        }
    }
}
