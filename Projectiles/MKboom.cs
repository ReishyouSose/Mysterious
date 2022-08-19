namespace MysteriousKnives.Projectiles
{
    public class MKboom : ModProjectile
    {
        public override string Texture => "MysteriousKnives/Pictures/Projectiles/Another/MKboom";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("诡秘飞刀");
        }
        public override void SetDefaults()
        {
            Projectile.width = 16;//宽
            Projectile.height = 16;//高
            Projectile.scale = 20f;//体积倍率
            Projectile.timeLeft = 2;//存在时间60 = 1秒
            Projectile.DamageType = ModContent.GetInstance<Mysterious>();// 伤害类型
            Projectile.friendly = true;// 攻击敌方？
            Projectile.hostile = false;// 攻击友方？
            Projectile.ignoreWater = true;//忽视水？
            Projectile.tileCollide = false;//不穿墙？
            Projectile.penetrate = -1;//穿透数量 -1无限
            Projectile.aiStyle = -1;//附带原版弹幕AI ID
            Projectile.alpha = 255;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 0;
            Main.projFrames[Projectile.type] = 1;//动画被分成几份
        }
    }
}