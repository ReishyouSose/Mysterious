namespace MysteriousKnives.UI.UIStates
{
    public class OperationPanel : UIState
    {
        internal static bool enable = false;

        public DragableUIPanel bg;
        public ScrollView functionArea, ItemArea;

        public ButtonUI funcs;
        public VanillaItemSlotWrapper[] slots = new VanillaItemSlotWrapper[9999];//物品框上限
        //获取物品就是slost[?].item
        public override void OnInitialize()
        {
            bg = new();
            bg.Width.Pixels = 800f;
            bg.Height.Pixels = 600f;
            bg.HAlign = 0.5f;
            bg.VAlign = 0.3f;
            Append(bg);

            functionArea = new(200, 350);
            functionArea.Left.Pixels = 10;
            bg.Append(functionArea);

            ButtonUI function = new("打开功能选择菜单");
            function.Width.Pixels = 200;
            function.Height.Pixels = 30f;
            function.OnLeftClick += (evt, uie) =>
            {
                FunctionPanel.visible = !FunctionPanel.visible;
            };
            bg.Append(function);

            //这里45是物品栏的宽度，9是他自动弄出来的一个间隔（
            ItemArea = new(45*10*0.75f+9*9, 350);
            ItemArea.Left.Pixels = functionArea.Width() + functionArea.Left() + 10f;
            bg.Append(ItemArea);

            for (int i = 0; i < ListDataSystem.Funcslist.Count; i++)
            {
                funcs = new($"{ListDataSystem.Funcslist[i]}");
                funcs.Width.Pixels = functionArea.ScrollList.WidthInside();
                funcs.Height.Pixels = 50f;
                funcs.id[0] = i;
                funcs.OnLeftClick += (evt, uie) =>
                {
                    int x = (uie as ButtonUI).id[0];
                    FuncsOnClick(x);
                };
                functionArea.AppendElement(funcs);
            }

            for (int i = 0; i < ListDataSystem.Itemlist.Count; i++)
            {
                slots[i] = new(scale: 0.75f);
                slots[i].item = ListDataSystem.Itemlist[i];
                ItemArea.AppendElement(slots[i]);
            }

            bg.Width.Pixels = functionArea.Width() + ItemArea.Width() + functionArea.HPadding() + 20;
            bg.Height.Pixels = functionArea.Height() + functionArea.Top() + functionArea.VPadding() + 20;

            functionArea.VAlign = 0.5f;
            ItemArea.VAlign = 0.5f;

            function.Left.Pixels = 20f;
            function.Top.Pixels = -25f;
            function.Width.Pixels = funcs.Width();

            ButtonUI close = new("X");
            close.Height.Pixels = function.Height();
            close.Width.Pixels = close.Height();
            close.Left.Pixels = bg.Width() - close.Width() - 30f;
            close.Top.Pixels = function.Top();
            close.OnLeftClick += (evt, uie) =>
            {
                  enable = false;
            };
            bg.Append(close);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public void FuncsOnClick(int id)
        {
            switch (id) //id判断点的哪个功能键
            {
                case 0: slots[5].item = new(5, 5); break;
                case 1: slots[5].item = new(5, 15); break;
            }
        }
    }
}
