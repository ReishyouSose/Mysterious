using static MysteriousKnives.Projectiles.MysteriousKnife;
using Terraria;

namespace MysteriousKnives.Projectiles
{
    public class MysteriousCore : ModProjectile
    {
        public override string Texture => "MysteriousKnives/Pictures/Projectiles/Another/MysteriousCore";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("诡秘飞刀");
            //ProjectileID.Sets.TrailCacheLength[Type] = 30;
        }
        public override void SetDefaults()
        {
            //原宽高15
            Projectile.width = 30;//宽
            Projectile.height = 30;//高
            Projectile.scale = 0.5f;//体积倍率
            Projectile.timeLeft = 180;//存在时间60 = 1秒
            Projectile.DamageType = ModContent.GetInstance<Mysterious>();// 伤害类型
            Projectile.friendly = true;// 攻击敌方？
            Projectile.hostile = false;// 攻击友方？
            Projectile.ignoreWater = true;//忽视水？
            Projectile.tileCollide = false;//不穿墙？
            Projectile.penetrate = 1;//穿透数量 -1无限
            Projectile.aiStyle = -1;//附带原版弹幕AI ID
            Projectile.extraUpdates = 1;//每帧额外更新次数
            Projectile.alpha = 0;
            Main.projFrames[Projectile.type] = 118;//动画被分成几份
        }
        public override void OnSpawn(IEntitySource source)
        {
            if (source is EntitySource_ItemUse use1)
            {
                Item item = use1.Item;
                if (item.type == MKItemID.MK01) Projectile.frame = 1;
                if (item.type == MKItemID.MK02) Projectile.frame = 2;
                if (item.type == MKItemID.MK03) Projectile.frame = 3;
                if (item.type == MKItemID.MK04) Projectile.frame = 4;
                if (item.type == MKItemID.MK05) Projectile.frame = 5;
                if (item.type == MKItemID.MK06) Projectile.frame = 6;
                if (item.type == MKItemID.MK07) Projectile.frame = 7;
                if (item.type == MKItemID.MK08) Projectile.frame = 8;
                if (item.type == MKItemID.MK09) Projectile.frame = 9;
                if (item.type == MKItemID.MK10) Projectile.frame = 10;
            }
            else if (source is EntitySource_ItemUse_WithAmmo use2)
            {
                Item item = use2.Item;
                if (item.type == MKItemID.MK01) Projectile.frame = 1;
                if (item.type == MKItemID.MK02) Projectile.frame = 2;
                if (item.type == MKItemID.MK03) Projectile.frame = 3;
                if (item.type == MKItemID.MK04) Projectile.frame = 4;
                if (item.type == MKItemID.MK05) Projectile.frame = 5;
                if (item.type == MKItemID.MK06) Projectile.frame = 6;
                if (item.type == MKItemID.MK07) Projectile.frame = 7;
                if (item.type == MKItemID.MK08) Projectile.frame = 8;
                if (item.type == MKItemID.MK09) Projectile.frame = 9;
                if (item.type == MKItemID.MK10) Projectile.frame = 10;
            }
            switch (Projectile.frame)
            {
                case 1: Projectile.ai[0] = 2; break;
                case 2: Projectile.ai[0] = 2; break;
                case 3: Projectile.ai[0] = 3; break;
                case 4: Projectile.ai[0] = 3; break;
                case 5: Projectile.ai[0] = 4; break;
                case 6: Projectile.ai[0] = 4; break;
                case 7: Projectile.ai[0] = 5; break;
                case 8: Projectile.ai[0] = 5; break;
                case 9: Projectile.ai[0] = 6; break;
                case 10: Projectile.ai[0] = 6; break;
            }
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Projectile.velocity *= 0.97f;
            Lighting.AddLight(Projectile.Center, Main.DiscoColor.ToVector3());
            Projectile.ai[1] = MathHelper.ToRadians((float)Math.Pow((180 - Projectile.timeLeft), 1.25));
            Projectile.rotation = Projectile.ai[1];

            int t = 10;
            if (Projectile.timeLeft > t)
            {
                if (Projectile.timeLeft < t + 1 + 6 * Projectile.frame && Projectile.timeLeft % 6 == 0)
                {
                    Projectile.scale += 0.05f;
                    RandomShoot(player, 1, 0);
                }
                if (Projectile.timeLeft < t + 1 + 9 * Projectile.ai[0] && Projectile.timeLeft % 9 == 0)
                {
                    if (Main.rand.NextBool(2)) RandomShoot(player, 1, 0);
                }
            }
            else
            {
                Projectile.scale -= 0.05f;
                Projectile.alpha += 25;
            }
            {/*
                if (Projectile.timeLeft % 2 == 0)
                {
                    for (int i = Projectile.oldPos.Length - 1; i > 0; i--)
                    {
                        Projectile.oldPos[i] = Projectile.oldPos[i - 1];
                    }
                }
                Projectile.oldPos[0] = Projectile.Center;*/
            }//拖尾
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[Projectile.owner];
            RandomShoot(player, Projectile.frame, (int)Projectile.ai[1]);
        }
        public override void Kill(int timeLeft)
        {
            Projectile proj = Projectile.NewProjectileDirect(Projectile.GetSource_Death(), Projectile.Center, Projectile.velocity,
                ModContent.ProjectileType<MKboom>(), Projectile.damage, 20, Projectile.owner);
            proj.CritChance = Projectile.CritChance;
            SoundEngine.PlaySound(SoundID.Item14);
            for (int i = 0; i < 100; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position - new Vector2(15, 15),
                    30, 30, ModContent.DustType<RainbowDust>());
                dust.scale *= 1.5f;
                dust.velocity *= 50;
                dust.noGravity = false;

            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            Color color = Main.DiscoColor;
            color.A = 0;
            return color;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            SpriteBatch sb = Main.spriteBatch;
            Texture2D tex1 = ModContent.Request<Texture2D>("MysteriousKnives/Pictures/Projectiles/Sphere/RBsphere_2").Value;
            Texture2D tex2 = ModContent.Request<Texture2D>("MysteriousKnives/Pictures/Projectiles/Another/Extra_98").Value;
            Texture2D tex3 = ModContent.Request<Texture2D>("MysteriousKnives/Pictures/Projectiles/Another/Projectile_873，长枪是919").Value;
            Vector2 pos = Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);
            Vector2 origin = new(36, 36);
            Color drawcolor = new(Main.DiscoR, Main.DiscoG, Main.DiscoB, 0);
            float lerp = ((float)Math.Cos(Main.GameUpdateCount / 1.5f) * 0.2f + 1) * 0.8f;
            Vector2 scale = new Vector2(0.5f, 5f) * lerp * 0.8f * ((255 - Projectile.alpha) / 255f)
                * Projectile.scale * Projectile.ai[1] / 12f;
            {
                /*float length = Projectile.oldPos.Length;
                for (int i = 0; i < length; i++)
                {
                    float dawl = (length - i) / length;
                    sb.Draw(tex3, Projectile.oldPos[i] - Main.screenPosition, null, drawcolor * dawl,
                        Projectile.velocity.ToRotation() + (float)Math.PI / 2f, origin, Projectile.scale * dawl * 0.8f, 0, 0);
                }*/
            }//拖尾
            sb.Draw(tex3, pos, null, drawcolor * lerp, (float)Math.PI / 2f + Projectile.rotation, origin, scale * 1.5f, 0, 0);
            sb.Draw(tex3, pos, null, drawcolor * lerp * 0.75f, Projectile.rotation, origin, scale * 1.5f, 0, 0);
            ChangeSpb(BlendState.Additive);
            sb.Draw(tex2, pos, null,
                color: new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, 255) * ((255 - Projectile.alpha) / 255f),
                rotation: (float)Math.PI / 100f * Main.GameUpdateCount,
                origin, Projectile.scale * 2, SpriteEffects.None, 0);
            ChangeSpb(BlendState.AlphaBlend);
            return false;
        }
        public void RandomShoot(Player player, int cn, int rn)
        {
            for (int j = 0; j < (Main.rand.NextBool(10) == true ? 2 : 1); j++)
            {
                for (int i = 0; i < cn + Main.rand.Next(rn + 1); i++)
                {
                    Projectile proj = Projectile.NewProjectileDirect(Projectile.GetSource_Death(), Projectile.Center,
                        (Main.rand.Next(360) * MathHelper.Pi / 180f).ToRotationVector2() * 20f,
                        MKProjID.MKnives, Projectile.damage, Projectile.knockBack, player.whoAmI, Projectile.frame);
                    proj.CritChance = Projectile.CritChance;
                }
            }
        }
    }
}