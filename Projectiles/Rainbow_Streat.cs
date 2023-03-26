namespace MysteriousKnives.Projectiles
{
    public class Rainbow_Streat : ModProjectile
    {
        public override string Texture => "MysteriousKnives/Pictures/Projectiles/Another/Projectile_873，长枪是919";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("七彩矢");
            Main.projFrames[Projectile.type] = 1;//动画被分成几份
            ProjectileID.Sets.TrailCacheLength[Type] = 30;
        }
        public override void SetDefaults()
        {
            Projectile.width = 20;//宽
            Projectile.height = 20;//高
            Projectile.scale = 1f;//体积倍率
            Projectile.timeLeft = 600;//存在时间60 = 1秒
            Projectile.friendly = true;// 攻击敌方？
            Projectile.hostile = false;// 攻击友方？
            Projectile.ignoreWater = true;//忽视水？
            Projectile.tileCollide = false;//不穿墙？
            Projectile.penetrate = 1;//穿透数量 -1无限
            Projectile.alpha = 0;
        }
        public Color color;
        public int d;
        public override void OnSpawn(IEntitySource source)
        {
            switch (Main.rand.Next(8))
            {
                case 0: color = new(0.9f, 0.63f, 1f, 0); d = MKDustID.CSDust; break;
                case 1: color = new(0.33f, 0.33f, 0.33f, 0); d = MKDustID.ABDust; break;
                case 2: color = new(0.45f, 0.04f, 0.75f, 0); d = MKDustID.ASDust; break;
                case 3: color = new(1f, 0.39f, 0.22f, 0); d = MKDustID.CBDust; break;
                case 4: color = new(0.2f, 0.95f, 0.13f, 0); d = MKDustID.RBDust; break;
                case 5: color = new(0.29f, 0.37f, 0.88f, 0); d = MKDustID.SKDust; break;
                case 6: color = new(1f, 0.9f, 0.27f, 0); d = MKDustID.STDust; break;
                case 7: color = new(0.55f, 0.7f, 0.13f, 0); d = MKDustID.WVDust; break;
            }
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
            float distanceMax = 5000f;
            NPC target = null;
            foreach (NPC npc in Main.npc)
            {
                if (npc.CanBeChasedBy())
                {
                    float targetD = Vector2.Distance(npc.Center, Projectile.Center);
                    if (targetD <= distanceMax)
                    {
                        distanceMax = targetD;
                        target = npc;
                    }
                }
            }
            if (target != null)
            {
                Vector2 deflection = Vector2.Normalize(target.Center - Projectile.Center) * (10f + 5 * Projectile.ai[0]);
                Projectile.velocity = (Projectile.velocity * 30f + deflection) / 31f;
                if (Projectile.timeLeft > 569) Projectile.timeLeft++;
            }

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
            SpriteBatch sb = Main.spriteBatch;
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            Vector2 origin = texture.Size() / 2f;
            Color drawcolor = color;
            drawcolor.A = 0;
            Vector2 pos = Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);
            float lerp = ((float)Math.Cos(Main.GameUpdateCount / 2) * 0.2f + 1) * 0.8f;
            Vector2 scale = new Vector2(0.5f, 5f) * lerp;
            for (int i = 0; i < Projectile.oldPos.Length; i++)
            {
                sb.Draw(texture, Projectile.oldPos[i] - Main.screenPosition, null, color * ((30 - i) / 30f),
                    Projectile.rotation, origin, Projectile.scale * 0.9f * (30 - i) / 30f, 0, 0);
            }
            sb.Draw(texture, pos, null, new Color(255, 255, 255, 0), Projectile.rotation, origin, Projectile.scale * 0.9f, 0, 0);
            sb.Draw(texture, pos, null, new Color(255, 255, 255, 0), Projectile.rotation, origin, Projectile.scale * 0.9f, 0, 0);
            sb.Draw(texture, pos, null, drawcolor * lerp, (float)Math.PI / 2f, origin, scale * 0.8f, 0, 0);
            sb.Draw(texture, pos, null, drawcolor * lerp * 0.75f, 0f, origin, scale * 0.6f, 0, 0);
            return false;
        }
    }
}