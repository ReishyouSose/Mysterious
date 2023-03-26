namespace MysteriousKnives.Projectiles
{
    public class Rainbow_Halberd : ModProjectile
    {
        public override string Texture => "MysteriousKnives/Pictures/Projectiles/Another/Rainbow_Halberd";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("彩虹长戟");
        }
        public override void SetDefaults()
        {
            Projectile.width = 16;//宽23
            Projectile.height = 16;//高130
            Projectile.scale = 1f;//体积倍率
            Projectile.extraUpdates = 5;
            Projectile.timeLeft = 240;//存在时间60 = 1秒
            Projectile.DamageType = ModContent.GetInstance<Mysterious>();
            Projectile.friendly = true;// 攻击敌方？
            Projectile.hostile = false;// 攻击友方？
            Projectile.ignoreWater = true;//忽视水？
            Projectile.tileCollide = false;//不穿墙？
            Projectile.aiStyle = -1;//附带原版弹幕AI ID
            Projectile.alpha = 255;
            Projectile.penetrate = 1;
            Main.projFrames[Projectile.type] = 1;//动画被分成几份
        }
        public Color color;
        public int d;
        public override void OnSpawn(IEntitySource source)
        {
            switch (Main.rand.Next(8))
            {
                case 0: color = new(0.9f, 0.63f, 1f); d = MKDustID.CSDust; break;
                case 1: color = new(0.33f, 0.33f, 0.33f); d = MKDustID.ABDust; break;
                case 2: color = new(0.45f, 0.04f, 0.75f); d = MKDustID.ASDust; break;
                case 3: color = new(1f, 0.39f, 0.22f); d = MKDustID.CBDust; break;
                case 4: color = new(0.2f, 0.95f, 0.13f); d = MKDustID.RBDust; break;
                case 5: color = new(0.29f, 0.37f, 0.88f); d = MKDustID.SKDust; break;
                case 6: color = new(1f, 0.95f, 0.75f); d = MKDustID.STDust; break;
                case 7: color = new(0.55f, 0.7f, 0.13f); d = MKDustID.WVDust; break;
            }
        }
        public override void AI()
        {
            Projectile.localAI[0]++;
            Projectile.rotation = Projectile.velocity.ToRotation();
            if (Projectile.localAI[0] % 5 == 0)
            {
                Dust dust = Dust.NewDustDirect(Projectile.Hitbox.TopLeft(), 16, 16, d);
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            SpriteBatch spb = Main.spriteBatch;
            Vector2 pos = Projectile.Center - Main.screenPosition;
            color.A = 0;
            {
                /*
                int num = 40;
                int num2 = 180 * num;
                num2 /= 2;
                Texture2D value2 = TextureAssets.Extra[178].Value;
                Vector2 origin = value2.Frame().Size() * new Vector2(0f, 0.5f);
                Vector2 scale = new(num2 / value2.Width, 2f);
                Vector2 scale2 = new(num2 / value2.Width / 2, 2f);
                Color color2 = color * Utils.GetLerpValue(60f, 55f, proj.localAI[0], clamped: true) * Utils.GetLerpValue(0f, 10f, proj.localAI[0], clamped: true);
                spb.Draw(value2, pos, null, color2, proj.rotation, origin, scale2, (SpriteEffects)0, 0f);
                spb.Draw(value2, pos, null, color2 * 0.3f, proj.rotation, origin, scale, (SpriteEffects)0, 0f);*/
            }
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            Vector2 origin2 = texture.Frame().Size() / 2f;
            Color color3 = Color.White * (Projectile.localAI[0] > 20 ? 1 : 0);
            color3.A = (byte)(color3.A / 2);
            if (Projectile.localAI[0] > 30)
            {
                for (float i = 1f; i > 0f; i -= 1f / 6f)
                {
                    Vector2 value4 = Projectile.rotation.ToRotationVector2() * -120f * i;
                    spb.Draw(texture, pos + value4, null, color * (1f - i), Projectile.rotation, origin2, 1, 0, 0f);
                    spb.Draw(texture, pos + value4, null, Color.White * 0.15f * (1f - i),
                        Projectile.rotation, origin2, 0.85f, 0, 0f);
                }
                for (float i = 0f; i < 1f; i += 0.25f)
                {
                    Vector2 value5 = (i * ((float)Math.PI * 2f) +
                        Projectile.rotation).ToRotationVector2() * 2f;
                    spb.Draw(texture, pos + value5, null, color, Projectile.rotation, origin2, 1, 0, 0f);
                }
                spb.Draw(texture, pos, null, color, Projectile.rotation, origin2, 1.1f, 0, 0f);
            }
            spb.Draw(texture, pos, null, color3, Projectile.rotation, origin2, 1, 0, 0f);
            return false;
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            writer.WriteRGB(color);
            writer.Write(d);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            color= reader.ReadRGB();
            d = reader.ReadInt32();
        }
    }
}