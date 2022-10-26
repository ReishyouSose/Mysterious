

namespace MysteriousKnives.Effects.ShaderSystem
{
    public class MKScreenShader : ScreenShaderData
    {
        public MKScreenShader(Ref<Effect> shader, string passName) : base(shader, passName)
        {
            shader.Value.Parameters["color"].SetValue(new Vector4(Color.Red.ToVector3(), 1));
        }
        public override void Apply()
        {
            //Shader.Parameters["color"].SetValue(new Vector4(Main.DiscoColor.ToVector3(), 0.5f));

            base.Apply();
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Shader.Parameters["add"].SetValue(true);
            Shader.Parameters["color"].SetValue(new Vector4(/*Main.DiscoColor*/Color.White.ToVector3()*1/*0.15f*/, 1f));
        }
    }
}
