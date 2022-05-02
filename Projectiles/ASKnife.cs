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
    public class ASKnife : MysteriousKnife
    {
        public override string Texture => "MysteriousKnives/Projectiles/pictures/ASKnife";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("星辉飞刀");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
        }
        public override void AI()
        {
            base.AI();
            Lighting.AddLight(Projectile.position, 0.45f, 0.04f, 0.75f);//RGB
            if (Projectile.timeLeft < 597)//弹幕粒子效果
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height
                    , ModContent.DustType<ASDust>(), 1f, 1f, 100, default, 1f);
                // 粒子特效不受重力
                dust.alpha = 30;
                dust.noGravity = true;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)//弹幕命中时
        {
            Player player = Main.player[Projectile.owner];
            ASbuffs(player);
        }
        
    }
}