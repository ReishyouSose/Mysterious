using MysteriousKnives.UI.MKUIPanel;

namespace MysteriousKnives.UI.MKUIstate
{
	
	//这块就是注册UI啥的，顺便在这里怼什么委托，不过我没弄

    public class MKPotionStationUI : UIState
	{
		internal static bool enable = false;
		public override void OnInitialize()
		{
			DragableUIPanel panel = new();
			//设置面板的宽度
			panel.Width.Set(468f, 0f);
			//设置面板的高度
			panel.Height.Set(198f, 0f);
			panel.HAlign = 0.6f;
			panel.VAlign = 0.1f;
			//设置面板距离屏幕最左边的距离
			//panel.Left.Set(-244f, 0.5f);
			//设置面板距离屏幕最上端的距离
			//panel.Top.Set(-284f, 0.5f);
			//将这个面板注册到UIState
			Append(panel);

			UIPanel panel2 = new();
			panel2.Width.Set(270f, 0f);
			panel2.Height.Set(30f, 0f);
			panel2.Top.Set(-40f, 0f);
			panel2.HAlign = 0.5f;
			panel.Append(panel2);

			UIText header = new("放入的增益药剂将持续生效");
			header.HAlign = 0.5f;
			header.VAlign = 0.5f;
			panel2.Append(header);
			{ 
			/*
			
			
			//用tr原版图片实例化一个文字按钮
			UIText text = new("阿巴阿巴");
			//设置按钮距宽度
			text.Width.Set(22f, 0f);
			//设置按钮高度
			text.Height.Set(22f, 0f);
			//设置按钮距离所属ui部件的最左端的距离
			text.Left.Set(20f, 0.5f);
			//设置按钮距离所属ui部件的最顶端的距离
			text.Top.Set(15f, 0.5f);
			//设置按钮的水平居中
			text.HAlign = 0.5f;
			//设置按钮的垂直居中
			text.VAlign = 0.5f;
			text.OnClick += OntextClick;
			//将按钮注册入面板中，这个按钮的坐标将以面板的坐标为基础计算
			panel.Append(text);
			
			UIText text = new("Click me!");
			text.HAlign = text.VAlign = 0.5f;
			button.Append(text);*/
		}
			int x = 0;
			int y = -1;
			for (int i = 0; i < 40; i++)
            {
				x++;
				if (i % 10 == 0)
                {
					x = 0;
					y++;
                }
				VanillaItemSlotWrapper slot = new(ItemSlot.Context.BankItem, 0.75f);
				slot.Left.Set(x * 45, 0);
				slot.Top.Set(y * 45, 0);
				panel.Append(slot);
            }
		}
        public void OntextClick(UIMouseEvent evt, UIElement listeningElement)
		{
			// We can do stuff in here!
			Main.NewText("giao");
		}
	}
}
