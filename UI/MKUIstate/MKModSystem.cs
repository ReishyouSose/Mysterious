using Terraria.ID;

namespace MysteriousKnives.UI.MKUIstate
{
    //初始化UI的，还有绘制什么的
    public class MKModSystem : ModSystem
    {
        internal UserInterface MKInterface1, MKInterface2, MKInterface3;
        internal MKPotionStationUI MKpsUI;
        internal PotionButton potionButton;
        internal PlayerDatePanel datePanel;
        internal KnifeVel knifeVel;
        public override void Load()
        {
            if (!Main.dedServ)
            {
                //实例化
                MKpsUI = new MKPotionStationUI();
                potionButton = new PotionButton();
                datePanel = new PlayerDatePanel();
                knifeVel = new KnifeVel();
                //初始化
                MKpsUI.Activate();
                potionButton.Activate();
                datePanel.Activate();
                knifeVel.Activate();

                MKInterface1 = new();
                MKInterface2 = new();
                MKInterface3 = new();
            }
        }
        public override void Unload()
        {
            //MKpsUI?.; // If you hold data that needs to be unloaded, call it in OO-fashion
            MKpsUI = null;
            potionButton = null;
            datePanel = null;
            knifeVel = null;
        }
        public GameTime gametime;
        public override void UpdateUI(GameTime gameTime)
        {
            gametime = gameTime;
            MKInterface1?.Update(gameTime);
            MKInterface2?.Update(gameTime);
            MKInterface3?.Update(gameTime);
            /*if (Main.LocalPlayer.controlUseTile)
            {
                KnifeVel.enable = !KnifeVel.enable;
            }*/
            if (KnifeVel.enable)
            {
                MKInterface3.SetState(knifeVel);
            }
            else MKInterface3.SetState(null);
            if (Main.playerInventory == true)
            {
                PotionButton.enable = true;
            }
            else
            {
                PotionButton.enable = false;
            }
            if (Main.LocalPlayer.controlInv) MKPotionStationUI.enable = false;
            //当enable为true时（当UI开启时）
            if (PotionButton.enable) MKInterface1.SetState(potionButton);
            else MKInterface1.SetState(null);
            if (MKPotionStationUI.enable) MKInterface2.SetState(MKpsUI);
            else MKInterface2.SetState(null);
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
                        //if (PotionButton.enable)
                        {
                            //绘制UI（运行exampleUI的Draw方法）
                            MKInterface1.Draw(Main.spriteBatch, gametime);
                        }
                        //if (MKPotionStationUI.enable)
                        {
                            //绘制UI（运行exampleUI的Draw方法）
                            MKInterface2.Draw(Main.spriteBatch, gametime);
                        }
                        MKInterface3.Draw(Main.spriteBatch, gametime);
                        return true;
                    },
                //绘制层的类型，可以被设置缩放啥的
                InterfaceScaleType.UI));
            }
        }
    }

}
