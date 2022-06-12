namespace MysteriousKnives.UI
{
	//初始化UI的，还有绘制什么的
    public class MKModSystem : ModSystem
    {
		internal UserInterface MKInterface;
		internal MKPotionStationUI MKpsUI;
		public override void Load()
		{
			if (!Main.dedServ)
			{
				//实例化
				MKpsUI = new MKPotionStationUI();
				//初始化
				MKpsUI.Activate();
				MKInterface = new UserInterface();
				////让MKInterface代理MKpsUI的事件触发
				MKInterface.SetState(MKpsUI);
			}
		}
		public override void Unload()
		{
			//MKpsUI?.; // If you hold data that needs to be unloaded, call it in OO-fashion
			MKpsUI = null;
		}
		public GameTime gametime;
		public override void UpdateUI(GameTime gameTime)
		{
			gametime = gameTime;
			MKInterface?.Update(gameTime);
			if (Main.LocalPlayer.controlInv)
            {
				MKPotionStationUI.enable = false;
            }
			//当enable为true时（当UI开启时）
			/*if (MKPotionStationUI.enable)
			{
				ShowUI();
			}
			else HideUI();*/
		}
		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
			//寻找一个名字为Vanilla: Mouse Text的绘制层，也就是绘制鼠标字体的那一层，并且返回那一层的索引
			int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
			if (mouseTextIndex != -1)
			{
				//往绘制层集合插入一个成员，第一个参数是插入的地方的索引，第二个参数是绘制层
				layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(name: "MKInterface",
					drawMethod: delegate
					{
						if (MKPotionStationUI.enable)
						{
							//绘制UI（运行exampleUI的Draw方法）
							MKInterface.Draw(Main.spriteBatch, gametime);
						}
						return true;
					},
				//绘制层的类型，可以被设置缩放啥的
				InterfaceScaleType.UI));
			}
		}
		internal void ShowUI()
		{
			MKInterface?.SetState(MKpsUI);
		}

		internal void HideUI()
		{
			MKInterface?.SetState(null);
		}
	}
	
}
