namespace MysteriousKnives.UI.UIStates
{
    public class FunctionPanel : UIState
    {
        internal static bool visible = false;
        public DragableUIPanel bgx;
        public ScrollView selected, unselect;
        public ButtonUI funcsx, nonf;
        public override void OnInitialize()
        {
            bgx = new();
            bgx.Width.Pixels = 250f;
            bgx.Height.Pixels = 300f;
            bgx.HAlign = 0.5f;
            bgx.VAlign = 0.1f;
            Append(bgx);

            selected = new(100, 180);
            selected.Left.Pixels = 10f;
            bgx.Append(selected);

            unselect = new(100, 180);
            unselect.Left.Pixels = selected.Width() + selected.Left() + 40f;
            bgx.Append(unselect);

            ButtonUI add = new("←");
            add.Width.Pixels = 60f;
            add.Height.Pixels = 30f;
            add.Left.Pixels = selected.Width() + selected.Left() + 5;
            add.VAlign = 0.35f;
            add.OnLeftClick += (evt, uie) =>
            {

            };
            bgx.Append(add);

            ButtonUI remove = new("→");
            remove.Width.Pixels = 60f;
            remove.Height.Pixels = 30f;
            remove.Left.Pixels = selected.Width() + selected.Left() + 5;
            remove.VAlign = 0.65f;
            remove.OnLeftClick += (evt, uie) =>
            {

            };
            bgx.Append(remove);

            for (int i = 0; i < ListDataSystem.Funcslist.Count; i++)
            {
                funcsx = new($"{ListDataSystem.Funcslist[i]}");
                funcsx.Width.Pixels = selected.ScrollList.WidthInside();
                funcsx.Height.Pixels = 50f;
                funcsx.id[0] = i;
                funcsx.OnLeftClick += (evt, uie) =>
                {
                    int x = (uie as ButtonUI).id[0];
                    FuncsOnClick(x);
                };
                selected.AppendElement(funcsx);
            }

            for (int i = 0; i < ListDataSystem.Funcsnonlist.Count; i++)
            {
                nonf = new($"{ListDataSystem.Funcsnonlist[i]}");
                nonf.Width.Pixels = unselect.ScrollList.WidthInside();
                nonf.Height.Pixels = 50;
                nonf.id[0] = i;
                nonf.OnLeftClick += (evt, uie) =>
                {
                    int x = (uie as ButtonUI).id[0];
                    NonfOnClick(x);
                };
                unselect.AppendElement(nonf);
            }

            bgx.Width.Pixels =unselect.Width() + unselect.Left() + 20f;
            bgx.Height.Pixels = selected.Height() + selected.VPadding() + 20f;
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public void FuncsOnClick(int id)
        {
            switch (id) //id判断点的哪个功能键
            {
                case 0: break;
            }
        }
        public void NonfOnClick(int id)
        {
            switch (id) //id判断点的哪个功能键
            {
                case 0: break;
            }
        }
    }
}
