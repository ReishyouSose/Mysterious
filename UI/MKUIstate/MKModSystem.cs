namespace MysteriousKnives.UI.MKUIstate
{
    //初始化UI的，还有绘制什么的
    public class MKModSystem : ModSystem
    {
        internal UserInterface UI1, UI2;
        internal SkillButton button;
        internal SkillTree tree;
        public override void Load()
        {
            if (!Main.dedServ)
            {
                //实例化
                button = new();
                tree = new();
                //初始化
                button.Activate();
                tree.Activate();

                UI1 = new();
                UI2 = new();
            }
        }
        public override void Unload()
        {
            tree = null;
            button = null;
        }
        public GameTime gametime;
        public override void UpdateUI(GameTime gameTime)
        {
            gametime = gameTime;
            UI1?.Update(gameTime);
            UI2?.Update(gameTime);
            if (ModLoader.TryGetMod("CalamityMod", out Mod mod))
            {
                SkillButton.enable = Main.playerInventory;
                UI1.SetState(SkillButton.enable ? button : null);
                UI2.SetState(Main.playerInventory ? (SkillTree.enable ? tree : null) : null);
                if (Main.playerInventory && Main.LocalPlayer.controlInv) SkillTree.enable = false;
            }
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
                        UI1.Draw(Main.spriteBatch, gametime);
                        UI2.Draw(Main.spriteBatch, gametime);
                        return true;
                    },
                //绘制层的类型，可以被设置缩放啥的
                InterfaceScaleType.UI));
            }
        }
    }

}
