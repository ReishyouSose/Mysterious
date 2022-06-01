using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static MysteriousKnives.Dusts.MDust;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace MysteriousKnives.Projectiles
{
    public abstract class MKSphere : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("光球");
            Main.projFrames[Projectile.type] = 118;
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            Projectile.width = 30;//宽
            Projectile.height = 30;//高
            Projectile.scale = 2f;//体积倍率
            Projectile.timeLeft = 600;
            Projectile.DamageType = DamageClass.Generic;
            Projectile.ignoreWater = true;//忽视水？
            Projectile.tileCollide = false;//不穿墙？
            Projectile.friendly = false;// 攻击敌方？
            Projectile.hostile = false;// 攻击友方？
            Projectile.aiStyle = -1;//附带原版弹幕AI ID
            Projectile.alpha = 0;
            Main.projFrames[Projectile.type] = 118;
            base.SetDefaults();
        }
        public override string Texture => "MysteriousKnives/Pictures/Projectiles/Sphere/RBsphere_1";
        public int type;
        public float t, r;
        public Vector2 pos;
        public Color color;
        public override void AI()
        {
            t = Main.GameUpdateCount * 0.025f;
            Projectile.timeLeft = 2; 
            Projectile.frameCounter++;
            if (Projectile.frameCounter % 2 == 0)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;
                if (Projectile.frame > 118)
                    Projectile.frame = 0;
            }
            Player player = Main.player[Projectile.owner];
            Projectile.Center = player.Center + pos * 100;
            base.AI();
        }
        public override bool PreDraw(ref Color lightColor)
        {
            SpriteBatch spriteBatch = Main.spriteBatch;
            Texture2D texture = ModContent.Request<Texture2D>("MysteriousKnives/Pictures/Projectiles/Sphere/RBsphere_1").Value;
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.AnisotropicClamp,
                DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            spriteBatch.Draw(texture: texture,
                position: Projectile.position - Main.screenPosition,
                sourceRectangle: new Rectangle(0, Projectile.frame * 30, 30, 30),
                color: color,
                //rotation: 0,
                rotation: (float)Math.PI / 100f * Main.GameUpdateCount + r,
                origin: new Vector2(15, 15),
                scale: (float)Math.Sin(Main.GameUpdateCount * 0.1f) / 4 + 1.2f,
                effects: SpriteEffects.None,
                layerDepth: 0); ;
            return false;
        }
        public override void PostDraw(Color lightColor)
        {
            SpriteBatch spriteBatch = Main.spriteBatch;
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.AnisotropicClamp,
                DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
        }
        public class RBsphere : MKSphere
        {
            public override void AI()
            {
                type = ModContent.DustType<RBDust>();
                color = new(0.2f, 0.95f, 0.13f);
                r = 0;
                pos = new Vector2((float)Math.Cos(t), (float)Math.Sin(t));
                Dust dust = Dust.NewDustDirect(Projectile.position, 0, 0, type);
                dust.velocity *= 0;
                Lighting.AddLight(Projectile.Center, 0.2f, 0.95f, 0.13f);
                base.AI();
            }
        }
        public class STsphere : MKSphere
        {
            public override void AI()
            {
                type = ModContent.DustType<STDust>();
                color = new(1f, 0.95f, 0.75f);
                r = (float)(-Math.PI / 1.5f);
                pos = new Vector2((float)Math.Cos(t - Math.PI / 1.5f), (float)Math.Sin(t - Math.PI / 1.5f));
                Dust dust = Dust.NewDustDirect(Projectile.position, 0, 0, type);
                dust.velocity *= 0;
                Lighting.AddLight(Projectile.Center, 1f, 0.95f, 0.75f);

                base.AI();
            }
        }
        public class ASsphere : MKSphere
        {
            public override void AI()
            {
                type = ModContent.DustType<ASDust>();
                color = new(0.45f, 0.04f, 0.75f);
                r = (float)(Math.PI / 1.5f);
                pos = new Vector2((float)Math.Cos(t + Math.PI / 1.5f), (float)Math.Sin(t + Math.PI / 1.5f));
                Dust dust = Dust.NewDustDirect(Projectile.position, 0, 0, type);
                dust.velocity *= 0;
                Lighting.AddLight(Projectile.Center, 0.45f, 0.04f, 0.75f);
                base.AI();
            }
        }
    }
}
