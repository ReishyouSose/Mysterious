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
    public class RBKnife : MysteriousKnife
    {
        public override string Texture => "MysteriousKnives/Projectiles/pictures/RBKnife";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("泰拉飞刀");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
        }
        public override void AI()
        {
            base.AI();
            Lighting.AddLight(Projectile.position, 0.2f, 0.95f, 0.13f);//RGB
            if (Projectile.timeLeft < 597)//弹幕粒子效果
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height
                    , ModContent.DustType<RBDust>(), 1f, 1f, 100, default, 1f);
                // 粒子特效不受重力
                dust.alpha = 30;
                dust.noGravity = true;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)//弹幕命中时
        {
            Lighting.AddLight(Projectile.position, 0.2f, 0.95f, 0.13f);//RGB
            Player player = Main.player[Projectile.owner];
            if (player.statLife < player.statLifeMax2)
            {
                player.statLife += (player.statLifeMax2 - player.statLife) / 20;
                CombatText.NewText(new Rectangle((int)player.Center.X, (int)player.Center.Y - 20, player.width, player.height),
                        new Color(51, 245, 35), (player.statLifeMax2 - player.statLife) / 20, false, false);
            }
            RBbuffs();
            for (int i = 0; i < 30; i++)
            {
                // 生成粒子效果
                Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height,
                    ModContent.DustType<RBDust>(), 0, 0, 0, default, 0.5f);

                // 粒子效果无重力
                d.noGravity = false;
                // 粒子效果初速度乘以二
                d.velocity *= 10;
            }//粒子效果
        }
    }
}