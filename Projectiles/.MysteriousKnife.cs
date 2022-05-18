using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static MysteriousKnives.Items.MKnives;
using static MysteriousKnives.Buffs.MysteriousBuffs;
using static MysteriousKnives.Dusts.MDust;
using Terraria.Audio;

namespace MysteriousKnives.Projectiles
{
    
    public abstract class MysteriousKnife : ModProjectile
    {
        public static int Random(int rand)
        {
            int i = Main.rand.Next(rand );
            if (i == 0) return ModContent.ProjectileType<RBKnife>();
            else if (i == 1) return ModContent.ProjectileType<WVKnife>();
            else if (i == 2) return ModContent.ProjectileType<SKKnife>();
            else if (i == 3) return ModContent.ProjectileType<CSKnife>();
            else if (i == 4) return ModContent.ProjectileType<ABKnife>();
            else if (i == 5) return ModContent.ProjectileType<CBKnife>();
            else if (i == 6) return ModContent.ProjectileType<STKnife>();
            else return ModContent.ProjectileType<ASKnife>();
        }
        public void RandomShoot(Player player, int cn, int rn, int lv)
        {
            for (int i = 0; i <= cn + Main.rand.Next(rn); i++)
            {
                Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center,
                    (Main.rand.Next(360) * MathHelper.Pi / 180f).ToRotationVector2() * 20f,
                    Random(lv), Projectile.damage, Projectile.knockBack, player.whoAmI);
            }
        }
        public void LessDust(int type)
        {
            if (Projectile.timeLeft < 597)//弹幕粒子效果
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, type);
                dust.alpha = 30;
                dust.noGravity = true;
                dust.scale *= 0.5f;
                dust.position = Projectile.Center;
            }
        }
        public static int GetMKID(Player player)
        {
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK01>())
                return 1;
            else if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK02>())
                return 2;
            else if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK03>())
                return 3;
            else if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK04>())
                return 4;
            else if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK05>())
                return 5;
            else if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK06>())
                return 6;
            else if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK07>())
                return 7;
            else if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK08>())
                return 8;
            else if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK09>())
                return 9;
            else if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK10>())
                return 10;
            else return 0;
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
                    Vector2 deflection = Vector2.Normalize(target.Center - Projectile.Center) * 30f;
                    Projectile.velocity = (Projectile.velocity * 30f + deflection) / 31f;
                }
            }

            if (Projectile.timeLeft < 60) Projectile.alpha += (255 - Projectile.alpha) / 60;
            base.AI();
        }//发射角 追踪 淡出
        /// <summary>
        /// 施加结晶
        /// </summary>
        /// <param name="target"></param>
        public void CSbuffs(NPC target)
        {
            if (target.realLife != -1)
                target = Main.npc[target.realLife];
            int i = 60;
            Player player = Main.player[Projectile.owner];
            switch (GetMKID(player))
            {
                case 1: target.AddBuff(ModContent.BuffType<Crystallization>(), i); break;
                case 2: target.AddBuff(ModContent.BuffType<Crystallization>(), i); break;
                case 3: target.AddBuff(ModContent.BuffType<Crystallization>(), i * 2); break;
                case 4: target.AddBuff(ModContent.BuffType<Crystallization>(), i * 3); break;
                case 5: target.AddBuff(ModContent.BuffType<Crystallization>(), i * 3); break;
                case 6: target.AddBuff(ModContent.BuffType<Crystallization>(), i * 4); break;
                case 7: target.AddBuff(ModContent.BuffType<Crystallization>(), i * 4); break;
                case 8: target.AddBuff(ModContent.BuffType<Crystallization>(), i * 4); break;
                case 9: target.AddBuff(ModContent.BuffType<Crystallization>(), i * 5); break;
                case 10: target.AddBuff(ModContent.BuffType<Crystallization>(), i * 5); break;
            }
        }
        /// <summary>
        /// 施加凝爆
        /// </summary>
        /// <param name="target"></param>
        public void CBbuffs(NPC target)
        {
            if (target.realLife != -1)
                target = Main.npc[target.realLife];
            Player player = Main.player[Projectile.owner];
            int i = 180;
            switch (GetMKID(player))
            {
                case 2: target.AddBuff(ModContent.BuffType<ConvergentBurst1>(), i); break;
                case 3: target.AddBuff(ModContent.BuffType<ConvergentBurst2>(), i); break;
                case 4: target.AddBuff(ModContent.BuffType<ConvergentBurst3>(), i); break;
                case 5: target.AddBuff(ModContent.BuffType<ConvergentBurst4>(), i); break;
                case 6: target.AddBuff(ModContent.BuffType<ConvergentBurst4>(), i); break;
                case 7: target.AddBuff(ModContent.BuffType<ConvergentBurst4>(), i); break;
                case 8: target.AddBuff(ModContent.BuffType<ConvergentBurst5>(), i); break;
                case 9: target.AddBuff(ModContent.BuffType<ConvergentBurst6>(), i); break;
                case 10: target.AddBuff(ModContent.BuffType<ConvergentBurst6>(), i); break;
            }
        }
        /// <summary>
        /// 施加诡毒
        /// </summary>
        /// <param name="target"></param>
        public void WVbuffs(NPC target)
        {
            if (target.realLife != -1)
                target = Main.npc[target.realLife];
            Player player = Main.player[Projectile.owner];
            int i = 180;
            switch (GetMKID(player))
            {
                case 1: target.AddBuff(ModContent.BuffType<WeirdVemon>(), i); break;
                case 2: target.AddBuff(ModContent.BuffType<WeirdVemon>(), i*2); break;
                case 3: target.AddBuff(ModContent.BuffType<WeirdVemon>(), i*3); break;
                case 4: target.AddBuff(ModContent.BuffType<WeirdVemon>(), i*3); break;
                case 5: target.AddBuff(ModContent.BuffType<WeirdVemon>(), i*4); break;
                case 6: target.AddBuff(ModContent.BuffType<WeirdVemon>(), i*4); break;
                case 7: target.AddBuff(ModContent.BuffType<WeirdVemon>(), i*4); break;
                case 8: target.AddBuff(ModContent.BuffType<WeirdVemon>(), i*4); break;
                case 9: target.AddBuff(ModContent.BuffType<WeirdVemon>(), i*4); break;
                case 10: target.AddBuff(ModContent.BuffType<WeirdVemon>(), i*4); break;
            }
        }
        /// <summary>
        /// 施加沉沦
        /// </summary>
        /// <param name="target"></param>
        public void SKbuffs(NPC target)
        {
            if (target.realLife != -1)
                target = Main.npc[target.realLife];
            Player player = Main.player[Projectile.owner];
            int i = 180;
            switch (GetMKID(player))
            {
                case 1: target.AddBuff(ModContent.BuffType<SunkerCancer>(), i); break;
                case 2: target.AddBuff(ModContent.BuffType<SunkerCancer>(), i); break;
                case 3: target.AddBuff(ModContent.BuffType<SunkerCancer>(), i * 2); break;
                case 4: target.AddBuff(ModContent.BuffType<SunkerCancer>(), i * 2); break;
                case 5: target.AddBuff(ModContent.BuffType<SunkerCancer>(), i * 2); break;
                case 6: target.AddBuff(ModContent.BuffType<SunkerCancer>(), i * 2); break;
                case 7: target.AddBuff(ModContent.BuffType<SunkerCancer>(), i * 3); break;
                case 8: target.AddBuff(ModContent.BuffType<SunkerCancer>(), i * 3); break;
                case 9: target.AddBuff(ModContent.BuffType<SunkerCancer>(), i * 3); break;
                case 10: target.AddBuff(ModContent.BuffType<SunkerCancer>(), i * 3); break;
            }
        }
        /// <summary>
        /// 施加深渊
        /// </summary>
        /// <param name="target"></param>
        public void ABbuffs(NPC target)
        {
            if (target.realLife != -1)
                target = Main.npc[target.realLife];
            Player player = Main.player[Projectile.owner];
            int i = 180;
            switch (GetMKID(player))
            {
                case 2: target.AddBuff(ModContent.BuffType<IndescribableFear>(), i); break;
                case 3: target.AddBuff(ModContent.BuffType<IndescribableFear>(), i * 2); break;
                case 4: target.AddBuff(ModContent.BuffType<IndescribableFear>(), i * 3); break;
                case 5: target.AddBuff(ModContent.BuffType<IndescribableFear>(), i * 4); break;
                case 6: target.AddBuff(ModContent.BuffType<IndescribableFear>(), i * 5); break;
                case 7: target.AddBuff(ModContent.BuffType<IndescribableFear>(), i * 6); break;
                case 8: target.AddBuff(ModContent.BuffType<IndescribableFear>(), i * 6); break;
                case 9: target.AddBuff(ModContent.BuffType<IndescribableFear>(), i * 7); break;
                case 10: target.AddBuff(ModContent.BuffType<IndescribableFear>(), i * 7); break;
            }
        }
        /// <summary>
        /// 施加星辉
        /// </summary>
        /// <param name="target"></param>
        public static void ASbuffs(Player player)
        {
            int i = 180;
            switch (GetMKID(player))
            {
                case 3: player.AddBuff(ModContent.BuffType<AstralRay>(), i); break;
                case 4: player.AddBuff(ModContent.BuffType<AstralRay>(), i * 2); break;
                case 5: player.AddBuff(ModContent.BuffType<AstralRay>(), i * 3); break;
                case 6: player.AddBuff(ModContent.BuffType<AstralRay>(), i * 3); break;
                case 7: player.AddBuff(ModContent.BuffType<AstralRay>(), i * 4); break;
                case 8: player.AddBuff(ModContent.BuffType<AstralRay>(), i * 4); break;
                case 9: player.AddBuff(ModContent.BuffType<AstralRay>(), i * 4); break;
                case 10: player.AddBuff(ModContent.BuffType<AstralRay>(), i * 4); break;
            }
        }
        /// <summary>
        /// 施加回春
        /// </summary>
        public static void RBbuffs(Player player)
        {
            int i = 180;
            switch (GetMKID(player))
            {
                case 1: player.AddBuff(ModContent.BuffType<RejuvenationBlessing>(), i); break;
                case 2: player.AddBuff(ModContent.BuffType<RejuvenationBlessing>(), i * 2); break;
                case 3: player.AddBuff(ModContent.BuffType<RejuvenationBlessing>(), i * 3); break;
                case 4: player.AddBuff(ModContent.BuffType<RejuvenationBlessing>(), i * 4); break;
                case 5: player.AddBuff(ModContent.BuffType<RejuvenationBlessing>(), i * 5); break;
                case 6: player.AddBuff(ModContent.BuffType<RejuvenationBlessing>(), i * 6); break;
                case 7: player.AddBuff(ModContent.BuffType<RejuvenationBlessing>(), i * 6); break;
                case 8: player.AddBuff(ModContent.BuffType<RejuvenationBlessing>(), i * 7); break;
                case 9: player.AddBuff(ModContent.BuffType<RejuvenationBlessing>(), i * 7); break;
                case 10: player.AddBuff(ModContent.BuffType<RejuvenationBlessing>(), i * 7); break;
            }
        }
        /// <summary>
        /// 施加筋力
        /// </summary>
        public static void STbuffs(Player player)
        {
            int i = 180;
            switch (GetMKID(player))
            {
                case 1: player.AddBuff(ModContent.BuffType<StrengthEX>(), i); break;
                case 2: player.AddBuff(ModContent.BuffType<StrengthEX>(), i * 2); break;
                case 3: player.AddBuff(ModContent.BuffType<StrengthEX>(), i * 3); break;
                case 4: player.AddBuff(ModContent.BuffType<StrengthEX>(), i * 4); break;
                case 5: player.AddBuff(ModContent.BuffType<StrengthEX>(), i * 5); break;
                case 6: player.AddBuff(ModContent.BuffType<StrengthEX>(), i * 6); break;
                case 7: player.AddBuff(ModContent.BuffType<StrengthEX>(), i * 6); break;
                case 8: player.AddBuff(ModContent.BuffType<StrengthEX>(), i * 7); break;
                case 9: player.AddBuff(ModContent.BuffType<StrengthEX>(), i * 7); break;
                case 10: player.AddBuff(ModContent.BuffType<StrengthEX>(), i * 7); break;
            }
        }
        public class ABKnife : MysteriousKnife
        {
            public override string Texture => "MysteriousKnives/Pictures/Projectiles/ABKnife";
            public override void SetStaticDefaults()
            {
                DisplayName.SetDefault("深渊飞刀");
            }
            public override void AI()
            {
                base.AI();
                Lighting.AddLight(Projectile.Center, 0f, 0f, 0f);//RGB
                LessDust(ModContent.DustType<ABDust>());
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
            public override void AI()
            {
                base.AI();
                Lighting.AddLight(Projectile.Center, 0.45f, 0.04f, 0.75f);//RGB
                LessDust(ModContent.DustType<ASDust>());
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
            public override void AI()
            {
                base.AI();
                Lighting.AddLight(Projectile.Center, 1f, 0.39f, 0.22f);
                LessDust(ModContent.DustType<CBDust>());
            }
            public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)//弹幕命中时
            {
                CBbuffs(target);
            }
        }
        public class CSKnife : MysteriousKnife
        {
            public override string Texture => "MysteriousKnives/Pictures/Projectiles/CSKnife";
            public override void SetStaticDefaults()
            {
                DisplayName.SetDefault("结晶飞刀");
            }
            public override void AI()
            {
                base.AI();
                Lighting.AddLight(Projectile.Center, 0.9f, 0.63f, 1f);//RGB
                LessDust(ModContent.DustType<CSDust>());
            }
            public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)//弹幕命中时
            {
                CSbuffs(target);
            }
        }
        public class RBKnife : MysteriousKnife
        {
            public override string Texture => "MysteriousKnives/Pictures/Projectiles/RBKnife";
            public override void SetStaticDefaults()
            {
                DisplayName.SetDefault("回春飞刀");
            }
            public override void AI()
            {
                base.AI();
                Lighting.AddLight(Projectile.Center, 0.2f, 0.95f, 0.13f);//RGB
                LessDust(ModContent.DustType<RBDust>());
            }
            public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)//弹幕命中时
            {
                Player player = Main.player[Projectile.owner];
                if (player.statLife < player.statLifeMax2)
                {
                    int i = (player.statLifeMax2 - player.statLife) / 20;
                    player.statLife += i;
                    player.HealEffect(i);
                }
                RBbuffs(player);
            }
        }
        public class SKKnife : MysteriousKnife
        {
            public override string Texture => "MysteriousKnives/Pictures/Projectiles/SKKnife";
            public override void SetStaticDefaults()
            {
                DisplayName.SetDefault("沉沦飞刀");
            }
            public override void AI()
            {
                base.AI();
                Lighting.AddLight(Projectile.Center, 0.29f, 0.37f, 0.88f);//RGB
                LessDust(ModContent.DustType<SKDust>());
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
            public override void AI()
            {
                base.AI();
                Lighting.AddLight(Projectile.Center, 1f, 0.95f, 0.7f);//RGB
                LessDust(ModContent.DustType<STDust>());
            }
            public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)//弹幕命中时
            {
                Player player = Main.player[Projectile.owner];
                STbuffs(player);
            }
        }
        public class WVKnife : MysteriousKnife
        {
            public override string Texture => "MysteriousKnives/Pictures/Projectiles/WVKnife";
            public override void SetStaticDefaults()
            {
                DisplayName.SetDefault("诡毒飞刀");
            }
            public override void AI()
            {
                base.AI();
                Lighting.AddLight(Projectile.Center, 0.55f, 0.7f, 0.13f);//RGB
                LessDust(ModContent.DustType<WVDust>());
            }
            public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)//弹幕命中时
            {
                WVbuffs(target);
            }
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
                Lighting.AddLight(Projectile.Center,
                        Main.DiscoR / 255f, Main.DiscoG / 255f, Main.DiscoB / 255f);
                if (Projectile.timeLeft < 177)//弹幕粒子效果
                {
                    Dust dust = Dust.NewDustDirect(Projectile.Center, Projectile.width, Projectile.height,
                        ModContent.DustType<RanbowDust>(), 0f, 0f, 0, default, 1f);
                    dust.scale *= 2f;
                }

            }
            public override void Kill(int timeLeft)
            {
                Player player = Main.player[Projectile.owner];
                Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center, Projectile.velocity,
                    ModContent.ProjectileType<MKboom>(), Projectile.damage, 20, player.whoAmI);
                SoundEngine.PlaySound(SoundID.Item14);
                for (int i = 0; i < 100; i++)
                {
                    Dust dust = Dust.NewDustDirect(Projectile.Center, Projectile.width, Projectile.height,
                        ModContent.DustType<RanbowDust>(), 0f, 0f, 0, default, 1f);
                    dust.scale *= 1.5f;
                    dust.velocity *= 50;
                    dust.noGravity = false;
                }

                {
                    if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK01>()) RandomShoot(player, 1, 2, 4);
                    if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK02>()) RandomShoot(player, 2, 2, 7);
                    if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK03>()) RandomShoot(player, 3, 3, 8);
                    if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK04>()) RandomShoot(player, 4, 3, 8);
                    if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK05>()) RandomShoot(player, 5, 4, 8);
                    if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK06>()) RandomShoot(player, 6, 4, 8);
                    if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK07>()) RandomShoot(player, 7, 5, 8);
                    if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK08>()) RandomShoot(player, 8, 5, 8);
                    if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK09>()) RandomShoot(player, 9, 6, 8);
                    if (player.inventory[player.selectedItem].type == ModContent.ItemType<MK10>()) RandomShoot(player, 10, 6, 8);
                }//按等级散射
                base.Kill(timeLeft);
            }
        }
}
