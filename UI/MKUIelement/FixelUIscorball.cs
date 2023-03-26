namespace MysteriousKnives.UI.MKUIelement
{

    public class FixedUIScrollbar : UIScrollbar
    {
        //public UserInterface Interface;

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            // 两端相同的代码，我还没搞清楚为什么要这么写
            //UserInterface temp = UserInterface.ActiveInstance;
            //UserInterface.ActiveInstance = Interface;
            base.DrawSelf(spriteBatch);
            //UserInterface.ActiveInstance = temp;
        }

        public override void LeftMouseDown(UIMouseEvent evt)
        {
            //UserInterface temp = UserInterface.ActiveInstance;
            //UserInterface.ActiveInstance = Interface;
            base.LeftMouseDown(evt);
            //UserInterface.ActiveInstance = temp;
            ScrollWheelValue = 0;
        }

        // 这是一个滚动动画
        private float ScrollWheelValue = 0;
        public void SetViewPosition(int ScrollWheelValue)
        {
            this.ScrollWheelValue -= ScrollWheelValue;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (ScrollWheelValue != 0)
            {
                ViewPosition += ScrollWheelValue * 0.2f;
                ScrollWheelValue *= 0.8f;
                if (MathF.Abs(ScrollWheelValue) < 0.001f)
                {
                    ViewPosition = MathF.Round(ViewPosition, 3);
                    ScrollWheelValue = 0;
                }
            }
        }

        /*private float ScrollWheelValue = -120;
        public void SetViewPosition(int ScrollWheelValue)
        {
            this.ScrollWheelValue = ViewPosition - ScrollWheelValue;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (ScrollWheelValue > -120)
            {
                ViewPosition += (ScrollWheelValue - ViewPosition) / 5f;
            }
        }*/
    }
}
