namespace MysteriousKnives.Projectiles
{
    public class SpaceWarpProj : ModProjectile
    {
        public override string Texture => "MysteriousKnives/Pictures/Projectiles/Another/Arcaea";
        public override void SetDefaults()
        {
            Projectile.width = 15;//宽
            Projectile.height = 15;//高
            Projectile.friendly = false;
            Projectile.hostile = false;// 攻击友方？
            Projectile.ignoreWater = true;//忽视水？
            Projectile.tileCollide = false;//不穿墙？
            Projectile.penetrate = -1;//穿透数量 -1无限
            Projectile.aiStyle = -1;//附带原版弹幕AI ID
            Projectile.hide = true;
        }
        public override void AI()
        {
            MysteriousKnives.draw = true;
            Projectile.timeLeft = 2;
            if (Projectile.ai[0] <= Projectile.ai[1]) Projectile.ai[0]++;
            else Projectile.Kill();
        }
        public void DrawSelf(SpriteBatch sb)
        {
            if (Projectile.ai[0] <= Projectile.ai[1])
            {
                Texture2D tex0 = GetT2D("Effects/空间扭曲").Value;
                sb.Draw(tex0, Projectile.Center - Main.screenPosition, null, Color.White * (1 - (float)Math.Sqrt(Projectile.ai[0] / Projectile.ai[1])),
                    0, tex0.Size() / 2f, (float)Math.Sqrt(Projectile.ai[0] / 2f) * 2, 0, 0);
            }
        }
    }
}
