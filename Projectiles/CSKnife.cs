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
    public class CSKnife : MysteriousKnife
    {
        public override string Texture => "MysteriousKnives/Projectiles/pictures/CSKnife";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("结晶飞刀");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
        }
        public override void AI()
        {
            base.AI();
            //Lighting.AddLight(Projectile.position, 0.9f, 0.63f, 1f);//RGB
            if (Projectile.timeLeft < 594)//弹幕粒子效果
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height
                    , ModContent.DustType<CSDust>());
                // 粒子特效不受重力
                dust.alpha = 30;
                dust.noGravity = true;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)//弹幕命中时
        {
            CSbuffs(target);
        }
    }
}