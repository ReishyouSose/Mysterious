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
            base.OnHitNPC(target, damage, knockback, crit); 
        }
    }
}