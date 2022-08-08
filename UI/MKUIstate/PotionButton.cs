using Humanizer;
using MysteriousKnives.UI.MKUIPanel;

namespace MysteriousKnives.UI.MKUIstate
{
    public class PotionButton : UIState
    {
        internal static bool enable;
        internal static DragableUIPanel panel;
        public override void OnInitialize()
        {
            /*panel = new DragableUIPanel();
            panel.Width.Set(46, 0);
            panel.Height.Set(46, 0);
            panel.Left.Set(399, 0);
            panel.Top.Set(259, 0);
            Append(panel);*/
            
            button = new(ModContent.Request<Texture2D>("MysteriousKnives/Pictures/Items/灌注站"), "点击打开");
            button.Width.Set(30, 0);
            button.Height.Set(36, 0);
            button.Left.Set(570, 0);
            button.Top.Set(274, 0);
            //button.HAlign = 0.5f;
            //button.VAlign = 0.2f;
            button.OnClick += Button_OnClick;
            Append(button);
        }
        public UIHoverImageButton button;
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public static void Button_OnClick(UIMouseEvent evt, UIElement listeningElement)
        {
            MKPotionStationUI.enable = !MKPotionStationUI.enable;
            SoundEngine.PlaySound(SoundID.MenuTick);
        }
    }
}
