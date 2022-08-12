namespace MysteriousKnives.Projectiles
{
    public class RB_Ray : ModProjectile
    {
        public override string Texture => "MysteriousKnives/Pictures/Projectiles/Another/RB_Ray";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("回春光束");
        }
        public override void SetDefaults()
        {
            Projectile.width = 4;//宽
            Projectile.height = 4;//高
            Projectile.scale = 1f;//体积倍率
            Projectile.extraUpdates = 1;
            Projectile.timeLeft = 2;//存在时间60 = 1秒
            Projectile.friendly = false;// 攻击敌方？
            Projectile.hostile = false;// 攻击友方？
            Projectile.ignoreWater = true;//忽视水？
            Projectile.tileCollide = false;//不穿墙？
            Projectile.penetrate = 1;//穿透数量 -1无限
            Projectile.aiStyle = -1;//附带原版弹幕AI ID
            Projectile.alpha = 255;
            Main.projFrames[Projectile.type] = 1;//动画被分成几份
        }
        public override void AI()
        {
            Projectile.timeLeft = 2;
            Player player = Main.player[Projectile.owner];
            Vector2 deflection = Vector2.Normalize(player.Center - Projectile.Center);
            Projectile.velocity = (Projectile.velocity + deflection) * 30 / 31f;
            if (Projectile.Hitbox.Intersects(player.Hitbox))
            {
                int i = (player.statLifeMax2 - player.statLife) / 20;
                player.statLife += i;
                player.HealEffect(i);
                Projectile.Kill();
            }
            for (int i = 0; i < 3; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height,
                    ModContent.DustType<RBDust>());
                dust.position = Projectile.Center - Projectile.velocity * i / 3f;
                dust.velocity *= 0;
            }
        }
    }
}