using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using MysteriousKnives.Buffs;
using MysteriousKnives.Items;
using static MysteriousKnives.Dusts.MDust;

namespace MysteriousKnives.Projectiles
{ 
    public class ABKnife : MysteriousKnife
    {
        public override string Texture => "MysteriousKnives/Projectiles/pictures/ABKnife";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("深渊飞刀");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            
        }
        public override void AI()
        {
            base.AI();
            Lighting.AddLight(Projectile.position, 0f, 0f, 0f);//RGB
            if (Projectile.timeLeft < 597)//弹幕粒子效果
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height
                    , ModContent.DustType<ABDust>(), 1f, 1f, 100, default, 1f);
                // 粒子特效不受重力
                dust.alpha = 30;
                dust.noGravity = true;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)//弹幕命中时
        {
            Lighting.AddLight(Projectile.position, 0f, 0f, 0f);//RGB
            ABbuffs(target);
            for (int i = 0; i < 30; i++)
            {
                // 生成粒子效果
                Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height,
                    ModContent.DustType<CSDust>(), 0, 0, 0, default, 0.5f);

                // 粒子效果无重力
                d.noGravity = false;
                // 粒子效果初速度乘以二
                d.velocity *= 10;
            }//粒子效果
        }
    }
}