using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using MysteriousKnives.Projectiles;
using static MysteriousKnives.Dusts.MDust;
using MysteriousKnives.Buffs;

namespace MysteriousKnives.Projectiles
{ 
    public class CBKnife : MysteriousKnife
    {
        public override string Texture => "MysteriousKnives/Projectiles/pictures/CBKnife";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("凝爆飞刀");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
        }
        public override void AI()
        {
            Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height
                   , ModContent.DustType<CBDust>(), 0f, 0f, 0, default, 1f);
            dust.alpha = 30;
            dust.noGravity = true;
            base.AI();
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)//弹幕命中时
        {
            target.buffImmune[ModContent.BuffType<ConvergentBurst>()] = false;
            CBbuffs(target);
            Lighting.AddLight(Projectile.position, 1f, 0.39f, 0.22f);//RGB
            /*for (int i = 0; i < 30; i++)
            {
                // 生成粒子效果
                Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height,
                    ModContent.DustType<CSDust>(), 0, 0, 0, default, 0.5f);

                // 粒子效果无重力
                d.noGravity = false;
                // 粒子效果初速度乘以二
                d.velocity *= 10;
            }*/
            base.OnHitNPC(target, damage, knockback, crit); 
        }
    }
}