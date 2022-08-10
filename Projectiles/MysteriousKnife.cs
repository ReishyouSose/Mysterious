namespace MysteriousKnives.Projectiles
{
    public abstract class MysteriousKnife : ModProjectile
    {
        public void LessDust(int type)
        {
            /*if (Projectile.timeLeft < 597)//弹幕粒子效果
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, type);
                dust.noGravity = true;
                dust.scale *= 0.7f;
                dust.position = Projectile.Center;
            }*/
        }
        public override void SetDefaults()
        {
            Projectile.width = 16;//宽
            Projectile.height = 16;//高
            Projectile.scale = 0.8f;//体积倍率
            Projectile.DamageType = ModContent.GetInstance<Mysterious>();// 伤害类型
            Projectile.friendly = true;// 攻击敌方？
            Projectile.hostile = false;// 攻击友方？
            Projectile.ignoreWater = true;//忽视水？
            Projectile.tileCollide = false;//不穿墙？
            Projectile.penetrate = -1;//穿透数量 -1无限
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
            Projectile.aiStyle = -1;//附带原版弹幕AI ID
            Projectile.alpha = 255;
            Projectile.extraUpdates = 2;
            Projectile.timeLeft = 600 * (1 + Projectile.extraUpdates);//存在时间60 = 1秒
            Main.projFrames[Projectile.type] = 1;//动画被分成几份
            ProjectileID.Sets.TrailCacheLength[Type] = 20 * (1 + Projectile.extraUpdates);
            base.SetDefaults();
        }
        public int Exup
        {
            get { return 1 + Projectile.extraUpdates; }
            set
            {
                if (value < 1) value = 1;
                Projectile.extraUpdates = value - 1;
            }
        }
        public enum Projmode
        {
            spawn,
            attack,
            disappear
        }
        public Projmode State
        {
            get { return (Projmode)(int)Projectile.ai[1]; }
            set { Projectile.ai[1] = (int)value; }
        }
        public void SwitchTo(Projmode state)
        {
            State = state;
        }
        public float alpha = 255;
        public override void AI()
        {
            switch (State)
            {
                case Projmode.spawn:
                    Projectile.friendly = false;
                    if (Projectile.localAI[0] == 0)
                    {
                        Projectile.velocity /= Exup;
                        Projectile.localAI[0]++;
                    }
                    if (alpha > 0)
                    {
                        alpha -= 255f / (25 * Exup);
                        Projectile.alpha = (int)alpha;
                        if (Projectile.alpha < 0)
                        {
                            Projectile.alpha = 0;
                        }
                    }
                    if (Projectile.timeLeft % Exup == 0)
                    {
                        Projectile.velocity *= 0.97f;
                        Projectile.scale += 0.0067f;
                        if (Projectile.scale > 1f)
                        {
                            Projectile.scale = 1f;
                        }
                    }
                    if (Projectile.timeLeft < 570 * Exup)
                    {
                        SwitchTo(Projmode.attack);
                    }
                    break;
                case Projmode.attack:
                    Projectile.friendly = true;
                    float distanceMax = 5000f;
                    NPC target = null;
                    foreach (NPC npc in Main.npc)
                    {
                        if (npc.CanBeChasedBy())
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
                        Vector2 tarVel = Vector2.Normalize(target.Center - Projectile.Center)
                            * (10f + 5f * Projectile.ai[0]) / Exup;
                        Projectile.velocity = (Projectile.velocity * 30 * Exup + tarVel) / (30 * Exup + 1);
                        Projectile.timeLeft++;
                    }
                    if (Projectile.timeLeft <= 60 * Exup)
                    {
                        SwitchTo(Projmode.disappear);
                    }
                    break;
                case Projmode.disappear:
                    Projectile.friendly = false;
                    if (alpha < 255)
                    {
                        alpha += 255f / (50 * Exup);
                        Projectile.alpha = (int)alpha;
                        if (Projectile.alpha > 255)
                        {
                            Projectile.alpha = 255;
                        }
                    }
                    if (Projectile.timeLeft % Exup == 0)
                    {
                        Projectile.velocity *= 0.9f;
                        if (Projectile.scale > 0.0167f)
                        {
                            Projectile.scale -= 0.0167f;
                        }
                    }
                    break;
            }
            Projectile.rotation = MathHelper.Pi / 2 + Projectile.velocity.ToRotation();
            for (int i = Projectile.oldPos.Length - 1; i > 0; i--)
            {
                Projectile.oldPos[i] = Projectile.oldPos[i - 1];
                Projectile.oldRot[i] = Projectile.oldRot[i - 1];
            }
            Projectile.oldPos[0] = Projectile.Center;
            Projectile.oldRot[0] = Projectile.rotation;
            base.AI();
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.timeLeft = 60 * Exup;
            base.OnHitNPC(target, damage, knockback, crit);
        }
        public Color color;
        public int d;
        public override void OnSpawn(IEntitySource source)
        {
            if (Type == MKProjID.ABKnife)
            {
                color = new(0, 0, 0, 255); d = MKDustID.ABDust;
            }
            else if (Type == MKProjID.ASKnife)
            {
                color = new(135, 0, 255, 0); d = MKDustID.ASDust;
            }
            else if (Type == MKProjID.CBKnife)
            {
                color = new(255, 100, 0, 0); d = MKDustID.CBDust;
            }
            else if (Type == MKProjID.CSKnife)
            {
                color = new(255, 120, 220, 0); d = MKDustID.CSDust;
            }
            else if (Type == MKProjID.SKKnife)
            {
                color = new(0, 155, 255, 0); d = MKDustID.SKDust;
            }
            else if (Type == MKProjID.STKnife)
            {
                color = new(255, 255, 0, 0); d = MKDustID.STDust;
            }
            else if (Type == MKProjID.WVKnife)
            {
                color = new(225, 255, 0, 255); d = MKDustID.WVDust;
            }
            else if (Type == MKProjID.RBKnife)
            {
                color = new(0, 255, 100, 0); d = MKDustID.RBDust;
            }
            base.OnSpawn(source);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            SpriteBatch sb = Main.spriteBatch;
            Texture2D tex = ModContent.Request<Texture2D>("MysteriousKnives/Pictures/Projectiles/Another/Projectile_873，长枪是919").Value;
            Vector2 origin = tex.Size() / 2f;
            Color drawcolor = color * ((255 - Projectile.alpha) / 510f + 0.5f);
            Vector2 pos = Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);
            float lerp = ((float)Math.Cos(Main.GameUpdateCount / 1.5f) * 0.2f + 1) * 0.8f;
            Vector2 scale = new Vector2(0.5f, 5f) * lerp * 0.8f * ((255 - Projectile.alpha) / 255f) * Projectile.scale;
            float length = Projectile.oldPos.Length;
            for (int i = 0; i < length; i++)
            {
                float dawl = (length - i) / length;
                sb.Draw(tex, Projectile.oldPos[i] - Main.screenPosition, null, drawcolor * dawl,
                    Projectile.oldRot[i], origin, Projectile.scale * dawl * 0.8f, 0, 0);
            }
            sb.Draw(tex, pos, null, drawcolor * lerp, (float)Math.PI / 2f, origin, scale, 0, 0);
            sb.Draw(tex, pos, null, drawcolor * lerp * 0.75f, 0f, origin, scale * 0.75f, 0, 0);
            sb.Draw(tex, pos, null, new Color(1, 1, 1, 0f) * ((255 - Projectile.alpha) / 255f),
                Projectile.rotation, origin, Projectile.scale * 0.8f, 0, 0);
            return false;
        }
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
                case 2: target.AddBuff(ModContent.BuffType<WeirdVemon>(), i * 2); break;
                case 3: target.AddBuff(ModContent.BuffType<WeirdVemon>(), i * 3); break;
                case 4: target.AddBuff(ModContent.BuffType<WeirdVemon>(), i * 3); break;
                case 5: target.AddBuff(ModContent.BuffType<WeirdVemon>(), i * 4); break;
                case 6: target.AddBuff(ModContent.BuffType<WeirdVemon>(), i * 4); break;
                case 7: target.AddBuff(ModContent.BuffType<WeirdVemon>(), i * 4); break;
                case 8: target.AddBuff(ModContent.BuffType<WeirdVemon>(), i * 4); break;
                case 9: target.AddBuff(ModContent.BuffType<WeirdVemon>(), i * 4); break;
                case 10: target.AddBuff(ModContent.BuffType<WeirdVemon>(), i * 4); break;
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
                base.OnHitNPC(target, damage, knockback, crit);
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
                base.OnHitNPC(target, damage, knockback, crit);
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
                base.OnHitNPC(target, damage, knockback, crit);
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
                base.OnHitNPC(target, damage, knockback, crit);
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
                base.OnHitNPC(target, damage, knockback, crit);
                Player player = Main.player[Projectile.owner];
                Projectile.NewProjectile(Projectile.GetSource_OnHit(target), target.Center, new Vector2(0), MKProjID.RB_Ray,
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
                base.OnHitNPC(target, damage, knockback, crit);
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
                base.OnHitNPC(target, damage, knockback, crit);
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
                base.OnHitNPC(target, damage, knockback, crit);
                WVbuffs(target);
            }
        }
    }
}
