using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.PlayerDrawLayer;
using static MysteriousKnives.Dusts.MDust;
using MysteriousKnives.Items;
using MysteriousKnives.Buffs;
using static MysteriousKnives.Items.MKnives;
using MysteriousKnives.Projectiles;
using Terraria.Audio;

namespace MysteriousKnives.Projectiles
{ 
    public class Knife_Mysterious : MysteriousKnife
    {
        public override string Texture => "MysteriousKnives/Projectiles/pictures/Knife_Mysterious";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("诡秘飞刀");
        }
        public override void SetDefaults()
        {
            Projectile.width = 15;//宽
            Projectile.height = 15;//高
            Projectile.scale = 1f;//体积倍率
            Projectile.timeLeft = 180;//存在时间60 = 1秒
            Projectile.DamageType = DamageClass.Melee;// 伤害类型
            Projectile.friendly = true;// 攻击敌方？
            Projectile.hostile = false;// 攻击友方？
            Projectile.ignoreWater = true;//忽视水？
            Projectile.tileCollide = false;//不穿墙？
            Projectile.penetrate = 1;//穿透数量 -1无限
            Projectile.aiStyle = -1;//附带原版弹幕AI ID
            Projectile.extraUpdates = 1;//每帧额外更新次数
            Projectile.alpha = 255;
            Main.projFrames[Projectile.type] = 1;//动画被分成几份
        }
        public override void AI()
        {
            //弹幕贴图角度（朝向弹幕[proj]速度[v]的方向[tor]）
            Projectile.rotation = MathHelper.Pi / 2 + Projectile.velocity.ToRotation();
            if(Projectile.timeLeft % 5 == 0) Projectile.velocity *= 0.9f;
            Lighting.AddLight(Projectile.position, 
                    Main.DiscoR / 255f, Main.DiscoG / 255f, Main.DiscoB / 255f);
            if (Projectile.timeLeft < 177)//弹幕粒子效果
            {
                Dust dust = Dust.NewDustDirect(Projectile.position,
                    Projectile.width + Main.rand.Next(-5, 5), Projectile.height + Main.rand.Next(-5, 5), 
                    ModContent.DustType<RanbowDust>(), 0f, 0f, 0, default, 1f);
                // 粒子特效不受重力
                dust.alpha = 30;
                dust.scale *= 2f;
            }
            
        }
        public override void Kill(int timeLeft)
        {
            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center, Projectile.velocity,
                ModContent.ProjectileType<MKboom>(), 10, 20, 0);
            SoundEngine.PlaySound(SoundID.Item14);
            for (int i = 0; i < 100; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 
                    ModContent.DustType<RanbowDust>(), 0f, 0f, 0, default, 1f);
                // 粒子特效不受重力
                dust.alpha = 30;
                dust.scale *= 1.5f;
                dust.velocity *= 50;
                dust.noGravity = false;
            }
            
            {
                Player player = Main.player[Projectile.owner];
                if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK01>()) RandomShoot(1, 2, 4);
                if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK02>()) RandomShoot(2, 2, 7);
                if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK03>()) RandomShoot(3, 3, 8);
                if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK04>()) RandomShoot(4, 3, 8);
                if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK05>()) RandomShoot(5, 4, 8);
                if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK06>()) RandomShoot(6, 4, 8);
                if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK07>()) RandomShoot(7, 5, 8);
                if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK08>()) RandomShoot(8, 5, 8);
                if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK09>()) RandomShoot(9, 6, 8);
                if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK10>()) RandomShoot(10, 6, 8);
            }//按等级散射

            base.Kill(timeLeft);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)//弹幕命中时
        {
            for (int i = 0; i < 100; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height
                   , ModContent.DustType<RanbowDust>(), 0f, 0f, 0, default, 1f);
                dust.alpha = 30;
                dust.scale *= 2f;
                dust.velocity *= 50;
                dust.noGravity = false;
            }
            base.OnHitNPC(target, damage, knockback, crit);
        }
    }
}