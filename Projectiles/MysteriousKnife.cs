namespace MysteriousKnives.Projectiles
{
    public abstract class MysteriousKnife : ModProjectile
    {
        public void LessDust(int type)
        {
            if (Projectile.timeLeft < 597)//弹幕粒子效果
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, type);
                dust.noGravity = true;
                dust.scale *= 0.7f;
                dust.position = Projectile.Center;
            }
        }
        public override void SetDefaults()
        {
            Projectile.width = 14;//宽
            Projectile.height = 14;//高
            Projectile.scale = 1f;//体积倍率
            Projectile.timeLeft = 600;//存在时间60 = 1秒
            Projectile.DamageType = ModContent.GetInstance<Mysterious>();// 伤害类型
            Projectile.friendly = true;// 攻击敌方？
            Projectile.hostile = false;// 攻击友方？
            Projectile.ignoreWater = true;//忽视水？
            Projectile.tileCollide = false;//不穿墙？
            Projectile.penetrate = 1;//穿透数量 -1无限
            Projectile.aiStyle = -1;//附带原版弹幕AI ID
            Projectile.alpha = 0;
            Main.projFrames[Projectile.type] = 1;//动画被分成几份
            base.SetDefaults();
        }
        public override void AI()
        {
            if (Projectile.timeLeft %3 == 0 && Projectile.timeLeft >= 590) Projectile.velocity *= 0.9f;
            //弹幕发射角度（朝向弹幕[proj]速度[v]的方向[tor]）
            Projectile.rotation = MathHelper.Pi / 2 + Projectile.velocity.ToRotation();
            if (Projectile.timeLeft > 570) Projectile.friendly = false;
            else 
            {
                Projectile.friendly = true;
                //追踪
                float distanceMax = 5000f;
                NPC target = null;
                foreach (NPC npc in Main.npc)
                {
                    if (npc.CanBeChasedBy(default, true))
                    {
                        float targetD = Vector2.Distance(npc.Center, Projectile.Center);
                        if (targetD <= distanceMax)
                        {
                            distanceMax = targetD;
                            target = npc;
                        }
                    }
                }
                if (target != null)
                {
                    Vector2 deflection = Vector2.Normalize(target.Center - Projectile.Center) * (10f + 5 * Projectile.ai[0]);
                    Projectile.velocity = (Projectile.velocity * 30f + deflection) / 31f;
                    if(Projectile.timeLeft > 569) Projectile.timeLeft++;
                }
            }
            if (Projectile.timeLeft < 60 && Projectile.alpha < 255) Projectile.alpha += 255 / 60;
            else if (Projectile.alpha > 255) Projectile.alpha = 255;
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
            switch (Projectile.ai[0])
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
            int i = 180;
            if (target.rarity != 0 || target.boss)
            {
                switch (Projectile.ai[0])
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
        }
        /// <summary>
        /// 施加诡毒
        /// </summary>
        /// <param name="target"></param>
        public void WVbuffs(NPC target)
        {
            if (target.realLife != -1)
                target = Main.npc[target.realLife];
            int i = 180;
            switch (Projectile.ai[0])
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
            int i = 180;
            switch (Projectile.ai[0])
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
            int i = 180;
            switch (Projectile.ai[0])
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
        public void ASbuffs(Player player)
        {
            int i = 180;
            switch (Projectile.ai[0])
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
        public void RBbuffs(Player player)
        {
            int i = 180;
            switch (Projectile.ai[0])
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
        public void STbuffs(Player player)
        {
            int i = 180;
            switch (Projectile.ai[0])
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
            public override string Texture => "MysteriousKnives/Pictures/Projectiles/Knife/ABKnife";
            public override void SetStaticDefaults()
            {
                DisplayName.SetDefault("深渊飞刀");
            }
            public override void AI()
            {
                base.AI();
                Lighting.AddLight(Projectile.Center, 0.33f, 0.33f, 0.33f);//RGB
                LessDust(ModContent.DustType<ABDust>());
            }
            public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)//弹幕命中时
            {
                ABbuffs(target);
            }
        }
        public class ASKnife : MysteriousKnife
        {
            public override string Texture => "MysteriousKnives/Pictures/Projectiles/Knife/ASKnife";
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
            public override string Texture => "MysteriousKnives/Pictures/Projectiles/Knife/CBKnife";
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
            public override string Texture => "MysteriousKnives/Pictures/Projectiles/Knife/CSKnife";
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
            public override string Texture => "MysteriousKnives/Pictures/Projectiles/Knife/RBKnife";
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
                Projectile.NewProjectile(Projectile.GetSource_OnHit(target), target.Center, new Vector2(0),MKProjID.RB_Ray,
                    0, 0, player.whoAmI);
                RBbuffs(player);
            }
        }
        public class SKKnife : MysteriousKnife
        {
            public override string Texture => "MysteriousKnives/Pictures/Projectiles/Knife/SKKnife";
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
            public override string Texture => "MysteriousKnives/Pictures/Projectiles/Knife/STKnife";
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
            public override string Texture => "MysteriousKnives/Pictures/Projectiles/Knife/WVKnife";
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
}
