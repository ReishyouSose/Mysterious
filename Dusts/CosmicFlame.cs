namespace MysteriousKnives.Dusts
{
    public class CosmicFlame : ModDust
    {
        public override string Texture => "MysteriousKnives/Pictures/Dusts/CosmicFlame";
        public override void OnSpawn(Dust dust)
        {
            dust.scale = 1.2f;
            dust.alpha = 255;
            dust.noLight = true;
            dust.noGravity = true;
            base.OnSpawn(dust);
        }
        public override bool Update(Dust dust)
        {
            dust.position += dust.velocity;
            Main.NewText(dust.scale);
            dust.velocity *= 0.99f;
            if (dust.scale <= 0)
                dust.active = false;
            return false;
        }
        public static void DrawAll(SpriteBatch sb)
        {
            foreach (Dust d in Main.dust)
            {
                if (d.type == MKDustID.CosmicFlame && d.active)
                {
                    d.scale += 0.04f;
                    Texture2D tex = GetT2D("Dusts/CosmicFlame").Value;
                    sb.Draw(tex, d.position - Main.screenPosition, null, Color.White, 0, tex.Size() / 2, d.scale, SpriteEffects.None, 0);
                }
            }
        }
    }
}
