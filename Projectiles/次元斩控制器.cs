using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteriousKnives.Projectiles
{
    public class Proj1 : ModProjectile//次元斩
    {
        public override string Texture => "Terraria/Images/Projectile_0";
        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 1;
            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.friendly = true;
            Projectile.hide = true;//禁用原本的绘制
            Projectile.timeLeft = 80;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 1;
            base.SetDefaults();
        }
        public override bool ShouldUpdatePosition()
        {
            return false;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
            if (Projectile.timeLeft >= 75)
            {
                Projectile.ai[0] -= 0.2f;
            }
            if (Projectile.timeLeft == 80)
            {
                Projectile.ai[0] = 1f;//反色量
                Projectile.localAI[1] = 60;//宽度
            }
            if (Projectile.timeLeft < 60)
                Projectile.localAI[1] -= 1f;
            base.AI();
        }
        public void Draw(SpriteBatch sb)//自己的绘制
        {
            //绘制一个很长的方形
            float width = Projectile.localAI[1];
            sb.Draw(TextureAssets.MagicPixel.Value, Projectile.Center - Main.screenPosition, new Rectangle(0, 0, 5, 5), Color.White, Projectile.rotation, new Vector2(3, 3), new Vector2(1000, width / 5), SpriteEffects.None, 0); ;
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)//判定
        {
            float point = 0;
            if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center - Projectile.velocity * 2500, Projectile.Center + Projectile.velocity * 2500, Projectile.localAI[1], ref point))
                return true;
            return false;
        }
    }
    public class Proj2 : ModProjectile//次元斩 控制器弹幕&刀光弹幕
    {
        public override string Texture => "Terraria/Images/Projectile_0";
        public override void SetDefaults()
        {
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 90;
            Projectile.tileCollide = false;
            base.SetDefaults();
        }
        public override bool ShouldUpdatePosition()
        {
            return false;
        }
        int timer = 0;
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            if (Projectile.ai[0] == 0)//控制器
            {
                if (Projectile.timeLeft > 60)
                {
                    Vector2 r = Main.rand.NextVector2Unit();
                    Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), Projectile.Center + r * 1000,
                        -r.RotatedBy(Main.rand.NextFloat() * 0.6f) * 150, ModContent.ProjectileType<Proj2>(),
                        0, 0, Projectile.owner, 1).timeLeft = Projectile.timeLeft;
                }
                if (Projectile.timeLeft > 80)
                    Projectile.ai[1] += 0.1f;
                else if (Projectile.timeLeft < 50 && Projectile.timeLeft >= 40)
                    Projectile.ai[1] -= 0.1f;
            }
            else//黑色的刀光
            {
                if (timer < 14)
                {
                    Projectile.position += Projectile.velocity;
                    if (timer < 7)
                        Projectile.ai[1] += 5;
                    else
                        Projectile.ai[1] -= 5;
                }
                timer++;
            }
        }
        public override void Kill(int timeLeft)
        {
            if (Projectile.ai[0] == 1 && Main.rand.NextBool(2))
            {
                Projectile.NewProjectileDirect(Projectile.GetSource_Death(), Projectile.Center,
                    Vector2.Normalize(Projectile.velocity), ModContent.ProjectileType<Proj1>(),
                    1000, 0, Projectile.owner);
            }
            base.Kill(timeLeft);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            //Draw(spriteBatch);
            return false;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (Projectile.ai[0] == 1)
            {
                Texture2D tex = GetT2D("Effects/projTex").Value;
                spriteBatch.Draw(tex, Projectile.Center - Main.screenPosition, null, Color.Black, Projectile.rotation, tex.Size() / 2, new Vector2(0.5f, Projectile.ai[1]), SpriteEffects.None, 0);
            }
        }
    }
}
