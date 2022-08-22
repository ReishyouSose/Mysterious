namespace MysteriousKnives.Projectiles
{
    public class SwordLight : ModProjectile
    {
        public override string Texture => "MysteriousKnives/Pictures/Projectiles/Another/Projectile_873，长枪是919";
        public override void SetDefaults()
        {
            Projectile.width = 30;//宽
            Projectile.height = 30;//高
            Projectile.scale = 1f;//体积倍率
            Projectile.timeLeft = 90;//存在时间60 = 1秒
            Projectile.DamageType = ModContent.GetInstance<Mysterious>();// 伤害类型
            Projectile.friendly = true;// 攻击敌方？
            Projectile.hostile = false;// 攻击友方？
            Projectile.ignoreWater = true;//忽视水？
            Projectile.tileCollide = false;//不穿墙？
            Projectile.penetrate = -1;//穿透数量 -1无限
            Projectile.extraUpdates = 10;
            Projectile.alpha = 0;
            ProjectileID.Sets.TrailCacheLength[Type] = 30;
        }
        public override void OnSpawn(IEntitySource source)
        {
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Projectile.ai[0]++;
            float t = Projectile.ai[0] * ManyPI(1 / 180f);
            Vector2 pos = new Vector2(Sin(t + ManyPI(0.25f)) * 2, -Cos(t + ManyPI(0.25f))) * 100f;
            Projectile.Center = pos + player.Center;
            Projectile.rotation = Projectile.velocity.ToRotation();
            for (int i = Projectile.oldPos.Length - 1; i > 0; i--)
            {
                Projectile.oldPos[i] = Projectile.oldPos[i - 1];
                Projectile.oldRot[i] = Projectile.oldRot[i - 1];
            }
            Projectile.oldPos[0] = Projectile.Center;
            Projectile.oldRot[0] = Projectile.rotation;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Type].Value;
            SpriteBatch sb = Main.spriteBatch;
            float t = Projectile.ai[0] * ManyPI(1 / 180f);
            float scale = t < ManyPI(0.25f) ? t / ManyPI(0.25f) : (ManyPI(0.75f) - t) / ManyPI(0.5f);
            for (int i = 0; i < Projectile.oldPos.Length; i++)
            {
                sb.Draw(tex, Projectile.oldPos[i] - Main.screenPosition, null,
                    new(255, 120, 255, 0), 0, tex.Size() / 2f, scale, 0, 0);
            }
            return false;
        }
    }
}
