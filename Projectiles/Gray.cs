
using static MysteriousKnives.Projectiles.MKchannel;

namespace MysteriousKnives.Projectiles
{
    public class Gray : ModProjectile
    {
        public override string Texture => "Terraria/Images/Extra_98";
        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 16;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 300;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.hide = false;
            Projectile.alpha = 255;
            ProjectileID.Sets.TrailCacheLength[Type] = 30;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            //Drawself(Main.spriteBatch);
            return false;
        }
        public void Drawself(SpriteBatch sb)
        {
            if (Projectile.alpha > 0) Projectile.alpha -= 2;
            else Projectile.alpha = 0;
            for (int i = Projectile.oldPos.Length - 1; i > 0; --i)
                Projectile.oldPos[i] = Projectile.oldPos[i - 1];
            Projectile.oldPos[0] = Projectile.Center;
            //var sb = Main.spriteBatch;
            var gd = Main.graphics.GraphicsDevice;

            List<VertexData> bars = new();
            for (int i = 1; i < Projectile.oldPos.Length; i++)
            {
                float width = Math.Abs(Sin(i / ManyPI(1))) * 30 + 10;
                Vector2 pos = Projectile.oldPos[i - 1];
                Vector2 oldpos = Projectile.oldPos[i];
                if (oldpos == Vector2.Zero) break;
                Vector2 normalDir = pos - oldpos;
                normalDir = Vector2.Normalize(new Vector2(-normalDir.Y, normalDir.X));
                Vector2[] data = new Vector2[2];
                data[0] = pos + normalDir * width;
                data[1] = pos + normalDir * -width;
                float lerp = i / (float)Projectile.oldPos.Length;
                var w = MathHelper.Lerp(1f, -0.25f, lerp) * (255 - Projectile.alpha) / 255f;
                if (w < 0) w = 0;
                bars.Add(new VertexData(data[0], Color.White, new Vector3(lerp, 1, w)));
                bars.Add(new VertexData(data[1], Color.White, new Vector3(lerp, 0, w)));
            }

            List<VertexData> triangleList = new();

            if (bars.Count > 2)
            {

                // 按照顺序连接三角形
                triangleList.Add(bars[0]);
                var vertex = new VertexData((bars[0].Position + bars[1].Position) * 0.5f + Vector2.Normalize(Projectile.velocity) * 30, Color.White,
                    new Vector3(0, 0.5f, 1));
                triangleList.Add(bars[1]);
                triangleList.Add(vertex);
                for (int i = 0; i < bars.Count - 2; i += 2)
                {
                    triangleList.Add(bars[i]);
                    triangleList.Add(bars[i + 2]);
                    triangleList.Add(bars[i + 1]);

                    triangleList.Add(bars[i + 1]);
                    triangleList.Add(bars[i + 2]);
                    triangleList.Add(bars[i + 3]);
                }


                sb.End();
                sb.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);
                //RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;
                // 干掉注释掉就可以只显示三角形栅格
                //RasterizerState rasterizerState = new RasterizerState();
                //rasterizerState.CullMode = CullMode.None;
                //rasterizerState.FillMode = FillMode.WireFrame;
                //Main.graphics.GraphicsDevice.RasterizerState = rasterizerState;

                var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
                var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));

                // 把变换和所需信息丢给shader
                Effect DefaultEffect = ModContent.Request<Effect>("MysteriousKnives/Effects/Content/Trail", AssetRequestMode.ImmediateLoad).Value;
                Texture2D MainColor = ModContent.Request<Texture2D>("MysteriousKnives/Pictures/Projectiles/Another/heatmap").Value;
                Texture2D MainShape = ModContent.Request<Texture2D>("MysteriousKnives/Pictures/Projectiles/Another/Extra_197").Value;
                Texture2D MaskColor = ModContent.Request<Texture2D>("MysteriousKnives/Pictures/Projectiles/Another/Extra_189").Value;
                DefaultEffect.Parameters["uTransform"].SetValue(model * projection);
                Projectile.ai[0]++;
                DefaultEffect.Parameters["uTime"].SetValue(-Projectile.ai[0] * 0.02f);
                DefaultEffect.Parameters["mult"].SetValue(5);
                gd.Textures[0] = MainColor;
                gd.Textures[1] = MainShape;
                gd.Textures[2] = MaskColor;
                gd.SamplerStates[0] = SamplerState.PointWrap;
                gd.SamplerStates[1] = SamplerState.PointWrap;
                gd.SamplerStates[2] = SamplerState.PointWrap;
                //Main.graphics.GraphicsDevice.Textures[0] = Main.magicPixel;
                //Main.graphics.GraphicsDevice.Textures[1] = Main.magicPixel;
                //Main.graphics.GraphicsDevice.Textures[2] = Main.magicPixel;

                DefaultEffect.CurrentTechnique.Passes[0].Apply();


                gd.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList.ToArray(), 0, triangleList.Count / 3);

                //Main.graphics.GraphicsDevice.RasterizerState = originalState;
                //sb.End();
                //sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);
            }
        }
    }
}
