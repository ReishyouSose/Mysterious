using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using MysteriousKnives.Buffs;
using MysteriousKnives.Items;
using static MysteriousKnives.Items.MKnives;
using Terraria.DataStructures;
using MysteriousKnives.Projectiles;
using static MysteriousKnives.Buffs.MysteriousBuffs;
using static MysteriousKnives.Dusts.MDust;
using Terraria.Audio;

namespace MysteriousKnives.Projectiles
{
    
    public abstract class MysteriousKnife : ModProjectile
    {
        public static int Random(int rand)
        {
            int i = Main.rand.Next(rand);
            if (i == 0) return ModContent.ProjectileType<RBKnife>();
            else if (i == 1) return ModContent.ProjectileType<WVKnife>();
            else if (i == 2) return ModContent.ProjectileType<SKKnife>();
            else if (i == 3) return ModContent.ProjectileType<CSKnife>();
            else if (i == 4) return ModContent.ProjectileType<ABKnife>();
            else if (i == 5) return ModContent.ProjectileType<CBKnife>();
            else if (i == 6) return ModContent.ProjectileType<STKnife>();
            else return ModContent.ProjectileType<ASKnife>();
        }
        public override void SetDefaults()
        {
            Projectile.width = 14;//宽
            Projectile.height = 30;//高
            Projectile.scale = 1f;//体积倍率
            Projectile.timeLeft = 600;//存在时间60 = 1秒
            Projectile.DamageType = DamageClass.Melee;// 伤害类型
            Projectile.friendly = true;// 攻击敌方？
            Projectile.hostile = false;// 攻击友方？
            Projectile.ignoreWater = true;//忽视水？
            Projectile.tileCollide = false;//不穿墙？
            Projectile.penetrate = 1;//穿透数量 -1无限
            Projectile.aiStyle = -1;//附带原版弹幕AI ID
            Projectile.alpha = 0;
            Main.projFrames[Projectile.type] = 1;//动画被分成几份
            { 
            //Projectile.extraUpdates = 1;
             /*
        Projectile.aiStyle = 304;//附带原版弹幕AI ID
        aiType = ProjectileID.VampireKnife;//让此弹幕继承某种弹幕AI

        // 累加帧计时器
        Projectile.frameCounter++;
        // 当计时器经过了7帧
        if (Projectile.frameCounter % 7 == 0)
        {
            // 重置计时器
            Projectile.frameCounter = 0;
            // 选择下一帧动画
            // 让弹幕的帧与等于与5进行模运算，也就是除以5的余数
            Projectile.frame++;
            Projectile.frame %= 5;
        }
        */
            }
            base.SetDefaults();
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK10>())
            {
                Projectile.extraUpdates = 1;
                if (Projectile.timeLeft > 540) Projectile.friendly = false;
                else Projectile.friendly = true;
            }
            else
            {
                Projectile.extraUpdates = 0;
                if (Projectile.timeLeft > 570) Projectile.friendly = false;
                else Projectile.friendly = true;
            }
            //弹幕发射角度（朝向弹幕[proj]速度[v]的方向[tor]）
            Projectile.rotation = MathHelper.Pi / 2 + Projectile.velocity.ToRotation();
            if (Projectile.timeLeft %3 == 0 && Projectile.timeLeft >= 590) Projectile.velocity *= 0.9f;
            
            //追踪
            {
                NPC target = null;
                float distanceMax = 1000f;
                foreach (NPC npc in Main.npc)
                {
                    if (npc.CanBeChasedBy() && !npc.dontTakeDamage)
                    {
                        float currentDistance = Vector2.Distance(npc.Center, Projectile.Center);
                        if (currentDistance < distanceMax)
                        {
                            distanceMax = currentDistance;
                            target = npc;
                        }
                    }
                }
                if (target != null)
                {
                    // 计算朝向目标的向量
                    Vector2 targetVec = target.Center - Projectile.Center;
                    targetVec.Normalize();
                    // 目标向量是朝向目标的大小为20的向量(追踪速度）
                    targetVec *= 30f;
                    // 朝向npc的单位向量*20 + 3.33%偏移量（最总便宜）
                    Projectile.velocity = (Projectile.velocity * 30f + targetVec) / 31f;
                }
            }

            if (Projectile.timeLeft < 60) Projectile.alpha += (255 - Projectile.alpha) / 60;
            base.AI();
        }//发射角 追踪 淡出

        public void RandomShoot(int cn, int rn,int lv)
        {
            for (int i = 0; i <= cn + Main.rand.Next(rn); i++)
            {
                Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.position,
                    (Main.rand.Next(360) * MathHelper.Pi / 180f).ToRotationVector2() * 20f,
                    Random(lv), Projectile.damage, Projectile.knockBack, 0);
            }
        }
        /// <summary>
        /// 施加结晶
        /// </summary>
        /// <param name="target"></param>
        public void CSbuffs(NPC target)
        {
            target.buffImmune[ModContent.BuffType<Crystallization>()] = false;
            Player player = Main.player[Projectile.owner];
            var item = player.inventory[player.selectedItem].ModItem;
            if (item is MKnives mk)
            {
                mk.GiveCsBuffs(target);
            }
        }
        /// <summary>
        /// 施加凝爆
        /// </summary>
        /// <param name="target"></param>
        public void CBbuffs(NPC target)
        {
            target.buffImmune[ModContent.BuffType<ConvergentBurst>()] = false;
            Player player = Main.player[Projectile.owner];
            int i = 180;
            target.AddBuff(ModContent.BuffType<ConvergentBurst>(), i);
             {
                 if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK02>())
                 {
                     target.AddBuff(ModContent.BuffType<ConvergentBurst1>(), i);
                 }
                 if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK03>())
                 {
                     target.AddBuff(ModContent.BuffType<ConvergentBurst2>(), i);
                 }
                 if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK04>())
                 {
                     target.AddBuff(ModContent.BuffType<ConvergentBurst3>(), i);
                 }
                 if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK05>())
                 {
                     target.AddBuff(ModContent.BuffType<ConvergentBurst4>(), i);
                 }
                 if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK06>())
                 {
                     target.AddBuff(ModContent.BuffType<ConvergentBurst4>(), i);
                 }
                 if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK07>())
                 {
                     target.AddBuff(ModContent.BuffType<ConvergentBurst4>(), i);
                 }
                 if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK08>())
                 {
                     target.AddBuff(ModContent.BuffType<ConvergentBurst5>(), i);
                 }
                 if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK09>())
                 {
                     target.AddBuff(ModContent.BuffType<ConvergentBurst6>(), i);
                 }
                 if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK10>())
                 {
                     target.AddBuff(ModContent.BuffType<ConvergentBurst6>(), i);
                 }
             }
        }
        /// <summary>
        /// 施加诡毒
        /// </summary>
        /// <param name="target"></param>
        public void WVbuffs(NPC target)
        {
            target.buffImmune[ModContent.BuffType<WeirdVemon>()] = false;
            if (target.FullName == "WITCH") target.buffImmune[ModContent.BuffType<WeirdVemon>()] = true;
            Player player = Main.player[Projectile.owner];
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK01>())
            {
                target.AddBuff(ModContent.BuffType<WeirdVemon>(), 180);
            }
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK02>())
            {
                target.AddBuff(ModContent.BuffType<WeirdVemon>(), 360);
            }
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK03>())
            {
                target.AddBuff(ModContent.BuffType<WeirdVemon>(), 540);
            }
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK04>())
            {
                target.AddBuff(ModContent.BuffType<WeirdVemon>(), 540);
            }
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK05>())
            {
                target.AddBuff(ModContent.BuffType<WeirdVemon>(), 720);
            }
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK06>())
            {
                target.AddBuff(ModContent.BuffType<WeirdVemon>(), 720);
            }
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK07>())
            {
                target.AddBuff(ModContent.BuffType<WeirdVemon>(), 720);
            }
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK08>())
            {
                target.AddBuff(ModContent.BuffType<WeirdVemon>(), 720);
            }
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK09>())
            {
                target.AddBuff(ModContent.BuffType<WeirdVemon>(), 720);
            }
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK10>())
            {
                target.AddBuff(ModContent.BuffType<WeirdVemon>(), 720);
            }
        }
        /// <summary>
        /// 施加沉沦
        /// </summary>
        /// <param name="target"></param>
        public void SKbuffs(NPC target)
        {
            target.buffImmune[ModContent.BuffType<SunkerCancer>()] = false;
            if (target.FullName == "WITCH") target.buffImmune[ModContent.BuffType<SunkerCancer>()] = true;
            Player player = Main.player[Projectile.owner];
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK01>())
            {
                target.AddBuff(ModContent.BuffType<SunkerCancer>(), 180);
            }
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK02>())
            {
                target.AddBuff(ModContent.BuffType<SunkerCancer>(), 180);
            }
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK03>())
            {
                target.AddBuff(ModContent.BuffType<SunkerCancer>(), 360);
            }
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK04>())
            {
                target.AddBuff(ModContent.BuffType<SunkerCancer>(), 360);
            }
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK05>())
            {
                target.AddBuff(ModContent.BuffType<SunkerCancer>(), 360);
            }
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK06>())
            {
                target.AddBuff(ModContent.BuffType<SunkerCancer>(), 360);
            }
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK07>())
            {
                target.AddBuff(ModContent.BuffType<SunkerCancer>(), 540);
            }
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK08>())
            {
                target.AddBuff(ModContent.BuffType<SunkerCancer>(), 540);
            }
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK09>())
            {
                target.AddBuff(ModContent.BuffType<SunkerCancer>(), 540);
            }
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK10>())
            {
                target.AddBuff(ModContent.BuffType<SunkerCancer>(), 540);
            }
        }
        /// <summary>
        /// 施加深渊
        /// </summary>
        /// <param name="target"></param>
        public void ABbuffs(NPC target)
        {
            target.buffImmune[ModContent.BuffType<IndescribableFear>()] = false;
            if (target.FullName == "WITCH") target.buffImmune[ModContent.BuffType<IndescribableFear>()] = true;
            Player player = Main.player[Projectile.owner];
            int i = 180;
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK02>())
            {
                target.AddBuff(ModContent.BuffType<IndescribableFear>(), i * 1);
            }
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK03>())
            {
                target.AddBuff(ModContent.BuffType<IndescribableFear>(), i * 2);
            }
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK04>())
            {
                target.AddBuff(ModContent.BuffType<IndescribableFear>(), i * 3);
            }
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK05>())
            {
                target.AddBuff(ModContent.BuffType<IndescribableFear>(), i * 4);
            }
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK06>())
            {
                target.AddBuff(ModContent.BuffType<IndescribableFear>(), i * 5);
            }
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK07>())
            {
                target.AddBuff(ModContent.BuffType<IndescribableFear>(), i * 6);
            }
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK08>())
            {
                target.AddBuff(ModContent.BuffType<IndescribableFear>(), i * 6);
            }
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK09>())
            {
                target.AddBuff(ModContent.BuffType<IndescribableFear>(), i * 7);
            }
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK10>())
            {
                target.AddBuff(ModContent.BuffType<IndescribableFear>(), i * 7);
            }
        }
        /// <summary>
        /// 施加星辉
        /// </summary>
        /// <param name="target"></param>
        public static void ASbuffs(Player player)
        {
            int i = 180;
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK03>())
            {
                player.AddBuff(ModContent.BuffType<AstralRay>(), i * 1);
            }
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK04>())
            {
                player.AddBuff(ModContent.BuffType<AstralRay>(), i * 2);
            }
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK05>())
            {
                player.AddBuff(ModContent.BuffType<AstralRay>(), i * 3);
            }
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK06>())
            {
                player.AddBuff(ModContent.BuffType<AstralRay>(), i * 3);
            }
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK07>())
            {
                player.AddBuff(ModContent.BuffType<AstralRay>(), i * 4);
            }
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK08>())
            {
                player.AddBuff(ModContent.BuffType<AstralRay>(), i * 4);
            }
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK09>())
            {
                player.AddBuff(ModContent.BuffType<AstralRay>(), i * 4);
            }
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK10>())
            {
                player.AddBuff(ModContent.BuffType<AstralRay>(), i * 4);
            }
        }
        /// <summary>
        /// 施加回春
        /// </summary>
        public void RBbuffs()
        {
            Player player = Main.player[Projectile.owner];
            int i = 180;
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK01>())
            {
                player.AddBuff(ModContent.BuffType<RejuvenationBlessing>(), i * 1);
            }
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK02>())
            {
                player.AddBuff(ModContent.BuffType<RejuvenationBlessing>(), i * 2);
            }
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK03>())
            {
                player.AddBuff(ModContent.BuffType<RejuvenationBlessing>(), i * 3);
            }
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK04>())
            {
                player.AddBuff(ModContent.BuffType<RejuvenationBlessing>(), i * 4);
            }
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK05>())
            {
                player.AddBuff(ModContent.BuffType<RejuvenationBlessing>(), i * 5);
            }
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK06>())
            {
                player.AddBuff(ModContent.BuffType<RejuvenationBlessing>(), i * 6);
            }
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK07>())
            {
                player.AddBuff(ModContent.BuffType<RejuvenationBlessing>(), i * 6);
            }
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK08>())
            {
                player.AddBuff(ModContent.BuffType<RejuvenationBlessing>(), i * 7);
            }
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK09>())
            {
                player.AddBuff(ModContent.BuffType<RejuvenationBlessing>(), i * 8);
            }
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK10>())
            {
                player.AddBuff(ModContent.BuffType<RejuvenationBlessing>(), i * 8);
            }
        }
        /// <summary>
        /// 施加筋力
        /// </summary>
        public void STbuffs()
        {
            Player player = Main.player[Projectile.owner];
            int i = 180;
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK02>())
            {
                player.AddBuff(ModContent.BuffType<StrengthEX>(), i * 1);
            }
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK03>())
            {
                player.AddBuff(ModContent.BuffType<StrengthEX>(), i * 2);
            }
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK04>())
            {
                player.AddBuff(ModContent.BuffType<StrengthEX>(), i * 3);
            }
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK05>())
            {
                player.AddBuff(ModContent.BuffType<StrengthEX>(), i * 4);
            }
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK06>())
            {
                player.AddBuff(ModContent.BuffType<StrengthEX>(), i * 5);
            }
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK07>())
            {
                player.AddBuff(ModContent.BuffType<StrengthEX>(), i * 6);
            }
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK08>())
            {
                player.AddBuff(ModContent.BuffType<StrengthEX>(), i * 6);
            }
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK09>())
            {
                player.AddBuff(ModContent.BuffType<StrengthEX>(), i * 6);
            }
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK10>())
            {
                player.AddBuff(ModContent.BuffType<StrengthEX>(), i * 6);
            }
        }
    }

    public class ABKnife : MysteriousKnife
    {
        public override string Texture => "MysteriousKnives/Pictures/Projectiles/ABKnife";
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
            ABbuffs(target);
        }
    }
    public class ASKnife : MysteriousKnife
    {
        public override string Texture => "MysteriousKnives/Pictures/Projectiles/ASKnife";
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
    public class CBKnife : MysteriousKnife
    {
        public override string Texture => "MysteriousKnives/Pictures/Projectiles/CBKnife";
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
    public class CSKnife : MysteriousKnife
    {
        public override string Texture => "MysteriousKnives/Pictures/Projectiles/CSKnife";
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
    public class Knife_Mysterious : MysteriousKnife
    {
        public override string Texture => "MysteriousKnives/Pictures/Projectiles/Knife_Mysterious";
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
            if (Projectile.timeLeft % 5 == 0) Projectile.velocity *= 0.9f;
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
    public class RBKnife : MysteriousKnife
    {
        public override string Texture => "MysteriousKnives/Pictures/Projectiles/RBKnife";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("回春飞刀");
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
            Player player = Main.player[Projectile.owner];
            if (player.statLife < player.statLifeMax2)
            {
                player.statLife += (player.statLifeMax2 - player.statLife) / 20;
                player.HealEffect((player.statLifeMax2 - player.statLife) / 20);
                //CombatText.NewText(new Rectangle((int)player.Center.X, (int)player.Center.Y - 20, player.width, player.height), new Color(51, 245, 35), (player.statLifeMax2 - player.statLife) / 20, false, false);
            }
            RBbuffs();
        }
    }
    public class SKKnife : MysteriousKnife
    {
        public override string Texture => "MysteriousKnives/Pictures/Projectiles/SKKnife";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("沉沦飞刀");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
        }
        public override void AI()
        {
            base.AI();
            Lighting.AddLight(Projectile.position, 0.29f, 0.37f, 0.88f);//RGB
            if (Projectile.timeLeft < 597)//弹幕粒子效果
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height
                    , ModContent.DustType<SKDust>(), 1f, 1f, 100, default, 1f);
                // 粒子特效不受重力
                dust.alpha = 30;
                dust.noGravity = true;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)//弹幕命中时
        {
            SKbuffs(target);
        }
    }
    public class STKnife : MysteriousKnife
    {
        public override string Texture => "MysteriousKnives/Pictures/Projectiles/STKnife";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("力量飞刀");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
        }
        public override void AI()
        {
            base.AI();
            Lighting.AddLight(Projectile.position, 1f, 0.95f, 0.7f);//RGB
            if (Projectile.timeLeft < 597)//弹幕粒子效果
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height
                    , ModContent.DustType<STDust>(), 1f, 1f, 100, default, 1f);
                // 粒子特效不受重力
                dust.alpha = 30;
                dust.noGravity = true;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)//弹幕命中时
        {
            STbuffs();
        }
    }
    public class WVKnife : MysteriousKnife
    {
        public override string Texture => "MysteriousKnives/Pictures/Projectiles/WVKnife";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("诡毒飞刀");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
        }
        public override void AI()
        {
            base.AI();
            Lighting.AddLight(Projectile.position, 0.55f, 0.7f, 0.13f);//RGB
            if (Projectile.timeLeft < 597)//弹幕粒子效果
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height
                    , ModContent.DustType<WVDust>(), 1f, 1f, 100, default, 1f);
                // 粒子特效不受重力
                dust.alpha = 30;
                dust.noGravity = true;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)//弹幕命中时
        {
            WVbuffs(target);
        }
    }
}
