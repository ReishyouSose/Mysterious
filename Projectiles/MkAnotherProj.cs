using Microsoft.Xna.Framework.Graphics;

namespace MysteriousKnives.Projectiles
{
    public class MysteriousCore : MysteriousKnife
    {
        public override string Texture => "MysteriousKnives/Pictures/Projectiles/Another/MysteriousCore";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("诡秘飞刀");
        }
        public override void SetDefaults()
        {
            //原宽高15
            Projectile.width = 30;//宽
            Projectile.height = 30;//高
            Projectile.scale = 1f;//体积倍率
            Projectile.timeLeft = 180;//存在时间60 = 1秒
            Projectile.DamageType = ModContent.GetInstance<Mysterious>();// 伤害类型
            Projectile.friendly = true;// 攻击敌方？
            Projectile.hostile = false;// 攻击友方？
            Projectile.ignoreWater = true;//忽视水？
            Projectile.tileCollide = false;//不穿墙？
            Projectile.penetrate = 1;//穿透数量 -1无限
            Projectile.aiStyle = -1;//附带原版弹幕AI ID
            Projectile.extraUpdates = 1;//每帧额外更新次数
            Projectile.alpha = 0;
            //AIType = ProjectileID.RainbowCrystal;
            Main.projFrames[Projectile.type] = 118;//动画被分成几份
        }
        public int d;
        public override void AI()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter % 5 == 0)
            {
                Projectile.frame++;
                Projectile.frame %= 118;
            }
            if (Projectile.timeLeft % 5 == 0) Projectile.velocity *= 0.9f;
            Lighting.AddLight(Projectile.Center, Main.DiscoR / 255f, Main.DiscoG / 255f, Main.DiscoB / 255f);
            int rand = Projectile.timeLeft / 10;
            if (Projectile.timeLeft < 177 && Main.rand.Next(rand + 6) >= 5)//弹幕粒子效果
            {
                Dust.NewDust(Projectile.position, 30, 30, ModContent.DustType<RainbowDust>());
            }
        }
        public int MKID;
        public override void OnSpawn(IEntitySource source)
        {
            if (source is EntitySource_ItemUse_WithAmmo use)
            {
                Item item = use.Item;
                if (item.type == ModContent.ItemType<MK01>()) MKID = 1;
                else if (item.type == ModContent.ItemType<MK02>()) MKID = 2;
                else if (item.type == ModContent.ItemType<MK03>()) MKID = 3;
                else if (item.type == ModContent.ItemType<MK04>()) MKID = 4;
                else if (item.type == ModContent.ItemType<MK05>()) MKID = 5;
                else if (item.type == ModContent.ItemType<MK06>()) MKID = 6;
                else if (item.type == ModContent.ItemType<MK07>()) MKID = 7;
                else if (item.type == ModContent.ItemType<MK08>()) MKID = 8;
                else if (item.type == ModContent.ItemType<MK09>()) MKID = 9;
                else if (item.type == ModContent.ItemType<MK10>()) MKID = 10;
            }
            else MKID = 0;
        }
        public override void Kill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];
            Projectile proj = Projectile.NewProjectileDirect(Projectile.GetSource_Death(), Projectile.Center, Projectile.velocity,
                ModContent.ProjectileType<MKboom>(), Projectile.damage, 20, player.whoAmI);
            proj.CritChance = Projectile.CritChance;
            SoundEngine.PlaySound(SoundID.Item14);
            for (int i = 0; i < 100; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position - new Vector2(15, 15), 
                    30, 30, ModContent.DustType<RainbowDust>());
                dust.scale *= 1.5f;
                dust.velocity *= 50;
                dust.noGravity = false;
            }
            switch (MKID)
            {
                case 1: RandomShoot(player, 1, 2, 4); break;
                case 2: RandomShoot(player, 2, 2, 7); break;
                case 3: RandomShoot(player, 3, 3, 8); break;
                case 4: RandomShoot(player, 4, 3, 8); break;
                case 5: RandomShoot(player, 5, 4, 8); break;
                case 6: RandomShoot(player, 6, 4, 8); break;
                case 7: RandomShoot(player, 7, 5, 8); break;
                case 8: RandomShoot(player, 8, 5, 8); break;
                case 9: RandomShoot(player, 9, 6, 8); break;
                case 10: RandomShoot(player, 10, 6, 8); break;
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            SpriteBatch spriteBatch = Main.spriteBatch;
            Texture2D texture = ModContent.Request<Texture2D>("MysteriousKnives/Pictures/Projectiles/Sphere/RBsphere_2").Value;
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.AnisotropicClamp,
                DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            spriteBatch.Draw(texture: texture,
                position: Projectile.Center - Main.screenPosition,
                sourceRectangle: new Rectangle(0, Projectile.frame * 30, 30, 30),
                color: Main.DiscoColor,
                rotation: (float)Math.PI / 100f * Main.GameUpdateCount,
                origin: new Vector2(15, 15),
                scale: 1f,
                effects: SpriteEffects.None,
                layerDepth: 0); ;
            return false;
        }
        public override void PostDraw(Color lightColor)
        {
            SpriteBatch spriteBatch = Main.spriteBatch;
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp,
                DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
        }
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
        public int l;
        public void  RandomShoot(Player player, int cn, int rn, int lv)
        {
            if (Main.rand.NextBool(10)) l = 2;
            else l = 1;
            for (int j = 0; j < l; j++)
            for (int i = 0; i <= cn + Main.rand.Next(rn); i++)
            {
                Projectile proj =  Projectile.NewProjectileDirect(Projectile.GetSource_Death(), Projectile.Center,
                    (Main.rand.Next(360) * MathHelper.Pi / 180f).ToRotationVector2() * 20f,
                    Random(lv), Projectile.damage, Projectile.knockBack, player.whoAmI, MKID);
                proj.CritChance = Projectile.CritChance;
            }
        }
    }
    public class MKboom : ModProjectile
    {
        public override string Texture => "MysteriousKnives/Pictures/Projectiles/Another/MKboom";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("诡秘飞刀");
        }
        public override void SetDefaults()
        {
            Projectile.width = 16;//宽
            Projectile.height = 16;//高
            Projectile.scale = 20f;//体积倍率
            Projectile.timeLeft = 1;//存在时间60 = 1秒
            Projectile.DamageType = ModContent.GetInstance<Mysterious>();// 伤害类型
            Projectile.friendly = true;// 攻击敌方？
            Projectile.hostile = false;// 攻击友方？
            Projectile.ignoreWater = true;//忽视水？
            Projectile.tileCollide = false;//不穿墙？
            Projectile.penetrate = 1;//穿透数量 -1无限
            Projectile.aiStyle = -1;//附带原版弹幕AI ID
            Projectile.alpha = 255;
            Main.projFrames[Projectile.type] = 1;//动画被分成几份
            base.SetDefaults();
        }
    }

    public class MKchannel : ModProjectile
    {
        public override string Texture => "MysteriousKnives/Pictures/Projectiles/Another/Arcaea";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("诡秘充能");
            Main.projFrames[Type] = 30;
        }
        public override void SetDefaults()
        {
            Projectile.width = 15;//宽
            Projectile.height = 15;//高
            Projectile.friendly = false;
            Projectile.hostile = false;// 攻击友方？
            Projectile.ignoreWater = true;//忽视水？
            Projectile.tileCollide = false;//不穿墙？
            Projectile.penetrate = -1;//穿透数量 -1无限
            Projectile.aiStyle = -1;//附带原版弹幕AI ID
            Projectile.alpha = 255;
        }
        public override void OnSpawn(IEntitySource source)
        {
            SoundEngine.PlaySound(new("MysteriousKnives/Sounds/Channel"));
        }
        public float dis = 0;
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            NPC target = null;
            var npclist = new List<(NPC npcwho, float distance)>();
            foreach (NPC npc in Main.npc)
            {
                if (npc.CanBeChasedBy())
                {
                    float LNPC = Vector2.Distance(npc.Center, Main.MouseWorld);
                    npclist.Add((npc, LNPC));
                }
            }
            target = npclist.MinBy(t => t.distance).npcwho;
            if(target.realLife != -1) target = Main.npc[target.realLife];
            drawpos = target.Center;

            if (!player.channel) Projectile.Kill();
            else
            {
                Projectile.Center = player.Center;
                Projectile.timeLeft = 2;
                player.itemTime = 2;
                player.itemAnimation = 2;
                float root = player.GetTotalDamage(ModContent.GetInstance<Mysterious>()).ApplyTo(PlySelect(player).damage);

                if (Main.GameUpdateCount % 3 == 0)
                {
                    float random = 1 + 0.01f * Main.rand.Next(-15, 15);
                    int damage = (int)(root * target.takenDamageMultiplier * 4.5f * random);
                    if (target.life > damage)
                    {
                        target.life -= damage;
                        CombatText.NewText(new Rectangle((int)target.position.X + Main.rand.Next(-32, 32),
                        (int)target.Center.Y - Main.rand.Next(16, 32), target.width, target.height),
                        Main.DiscoColor, damage, true);
                    }
                    else target.StrikeNPC(damage + 1, 0, 0);
                    player.addDPS(damage);
                }

                Projectile.localAI[0]++;
                if (Projectile.localAI[0] % 1 == 0)
                {
                    float rand = Main.rand.NextFloat(MathHelper.TwoPi);
                    Vector2 pos = target.Center + new Vector2((float)Math.Cos(rand), (float)Math.Sin(rand)) * 1500;
                    Projectile proj = Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), pos,
                        Vector2.Normalize(target.Center - pos) * 10f, MKProjID.Rainbow_Streat,
                        (int)root, 0, player.whoAmI);
                    //proj.extraUpdates += Main.rand.Next(-3, 3);
                }

                switch (Main.rand.Next(8))
                {
                    case 0: target.AddBuff(ModContent.BuffType<Crystallization>(), 300); break;
                    case 1:
                        if (target.rarity != 0 || target.boss)
                        {
                            target.AddBuff(ModContent.BuffType<ConvergentBurst6>(), 180);
                        } break;
                    case 2: target.AddBuff(ModContent.BuffType<IndescribableFear>(), 7 * 180); break;
                    case 3: target.AddBuff(ModContent.BuffType<SunkerCancer>(), 720); break;
                    case 4: target.AddBuff(ModContent.BuffType<WeirdVemon>(), 720); break;
                    case 5:
                        if (player.statLife < player.statLifeMax2)
                        {
                            Projectile.NewProjectile(Projectile.GetSource_FromAI(), target.Center, new Vector2(0, 0),
                                ModContent.ProjectileType<RB_Ray>(), 0, 0, player.whoAmI);
                        } break;
                }
                /*
                float t = Main.GameUpdateCount * 0.1f;
                if (dis <= 10)
                {
                    dis++;
                    for (int i = 0; i < 72; i++)
                    {
                        Dust dust = Dust.NewDustDirect(target.Center, target.width, target.height,
                            Main.dayTime ? ModContent.DustType<HalloweenDust>() : ModContent.DustType<RainbowDust>());
                        dust.position = new Vector2((int)(Math.Pow(-1, i)) * (float)Math.Cos(Math.PI / 36 * (i + t)),
                            (float)Math.Sin(Math.PI / 36 * (i + t))) * 10f * dis + target.Center;
                        dust.velocity *= 0;
                    }
                }
                else dis = 10 * ((float)-Math.Cos(t / 1.25) / 10 + 1);*/
            }
        }
        public Vector2 drawpos;
        public bool down = true;
        public override bool PreDraw(ref Color lightColor)
        {
            if (Projectile.ai[0] < 10) Projectile.ai[0]++;

            int f = 20, t = 3;
            if (Projectile.frameCounter == 0 || Projectile.frameCounter == f * t) down = !down;
            if(!down) Projectile.frameCounter++;
            else Projectile.frameCounter--;
            Projectile.frame = Projectile.frameCounter / t;
            Projectile.frame %= f;
            
            int x = Projectile.frame;
            int y = Projectile.frame / 5;
            x %= 6;
            SpriteBatch sb = Main.spriteBatch;
            sb.End();
            sb.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.AnisotropicClamp,
                DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            sb.Draw(
                texture: TextureAssets.Projectile[Type].Value,
                position: drawpos - Main.screenPosition,
                sourceRectangle: new Rectangle(x * 144, y * 144, 144, 144),
                color: Main.DiscoColor,
                rotation: (float)(Math.Cos(MathHelper.Lerp(1, 10, Main.GameUpdateCount*0.1f))),
                origin: new Vector2(72, 72),
                scale: Projectile.ai[0] < 10 ?  1.5f * Projectile.ai[0] / 10
                    : (float)Math.Sin(Main.GameUpdateCount * 0.2f) * 0.15f + 1.5f,
                effects: 0,
                layerDepth: 0);
            return false;
        }
        public override void PostDraw(Color lightColor)
        {
            SpriteBatch sb = Main.spriteBatch;
            sb.End();
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp,
                DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
        }
    }
    public class Rainbow_Halberd : ModProjectile
    {
        public override string Texture => "MysteriousKnives/Pictures/Projectiles/Another/Rainbow_Halberd";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("彩虹长戟");
        }
        public override void SetDefaults()
        {
            Projectile.width = 16;//宽23
            Projectile.height = 16;//高130
            Projectile.scale = 1f;//体积倍率
            Projectile.extraUpdates = 5;
            Projectile.timeLeft = 240;//存在时间60 = 1秒
            Projectile.DamageType = ModContent.GetInstance<Mysterious>();
            Projectile.friendly = true;// 攻击敌方？
            Projectile.hostile = false;// 攻击友方？
            Projectile.ignoreWater = true;//忽视水？
            Projectile.tileCollide = false;//不穿墙？
            Projectile.aiStyle = -1;//附带原版弹幕AI ID
            Projectile.alpha = 255;
            Projectile.penetrate = 1;
            Main.projFrames[Projectile.type] = 1;//动画被分成几份
        }
        public Color color;
        public int d;
        public override void OnSpawn(IEntitySource source)
        {
            switch (Main.rand.Next(8))
            {
                case 0: color = new(0.9f, 0.63f, 1f); d = MKDustID.CSDust; break;
                case 1: color = new(0.33f, 0.33f, 0.33f); d = MKDustID.ABDust; break;
                case 2: color = new(0.45f, 0.04f, 0.75f); d = MKDustID.ASDust; break;
                case 3: color = new(1f, 0.39f, 0.22f); d = MKDustID.CBDust; break;
                case 4: color = new(0.2f, 0.95f, 0.13f); d = MKDustID.RBDust; break;
                case 5: color = new(0.29f, 0.37f, 0.88f); d = MKDustID.SKDust; break;
                case 6: color = new(1f, 0.95f, 0.75f); d = MKDustID.STDust; break;
                case 7: color = new(0.55f, 0.7f, 0.13f); d = MKDustID.WVDust; break;
            }
        }
        public override void AI()
        {
            Projectile.localAI[0]++;
            Projectile.rotation = Projectile.velocity.ToRotation();
            if (Projectile.localAI[0] % 5 == 0)
            {
                Dust dust = Dust.NewDustDirect(Projectile.Hitbox.TopLeft(), 16, 16, d);
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            SpriteBatch spb = Main.spriteBatch;
            Vector2 pos = Projectile.Center - Main.screenPosition;
            color.A = 0;
            {
                /*
                int num = 40;
                int num2 = 180 * num;
                num2 /= 2;
                Texture2D value2 = TextureAssets.Extra[178].Value;
                Vector2 origin = value2.Frame().Size() * new Vector2(0f, 0.5f);
                Vector2 scale = new(num2 / value2.Width, 2f);
                Vector2 scale2 = new(num2 / value2.Width / 2, 2f);
                Color color2 = color * Utils.GetLerpValue(60f, 55f, proj.localAI[0], clamped: true) * Utils.GetLerpValue(0f, 10f, proj.localAI[0], clamped: true);
                spb.Draw(value2, pos, null, color2, proj.rotation, origin, scale2, (SpriteEffects)0, 0f);
                spb.Draw(value2, pos, null, color2 * 0.3f, proj.rotation, origin, scale, (SpriteEffects)0, 0f);*/
            }
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            Vector2 origin2 = texture.Frame().Size() / 2f;
            Color color3 = Color.White * (Projectile.localAI[0] > 20 ? 1 : 0);
            color3.A = (byte)(color3.A / 2);
            if (Projectile.localAI[0] > 30)
            {
                for (float i = 1f; i > 0f; i -= 1f / 6f)
                {
                    Vector2 value4 = Projectile.rotation.ToRotationVector2() * -120f * i;
                    spb.Draw(texture, pos + value4, null, color * (1f - i), Projectile.rotation, origin2, 1, 0, 0f);
                    spb.Draw(texture, pos + value4, null, Color.White * 0.15f * (1f - i), 
                        Projectile.rotation, origin2, 0.85f, 0, 0f);
                }
                for (float i = 0f; i < 1f; i += 0.25f)
                {
                    Vector2 value5 = (i * ((float)Math.PI * 2f) + 
                        Projectile.rotation).ToRotationVector2() * 2f;
                    spb.Draw(texture, pos + value5, null, color, Projectile.rotation, origin2, 1, 0, 0f);
                }
                spb.Draw(texture, pos, null, color, Projectile.rotation, origin2, 1.1f, 0, 0f);
            }
            spb.Draw(texture, pos, null, color3, Projectile.rotation, origin2, 1, 0, 0f);
            return false;
        }
    }

    public class RB_Ray : ModProjectile
    {
        public override string Texture => "MysteriousKnives/Pictures/Projectiles/Another/RB_Ray";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("回春光束");
        }
        public override void SetDefaults()
        {
            Projectile.width = 4;//宽
            Projectile.height = 4;//高
            Projectile.scale = 1f;//体积倍率
            Projectile.extraUpdates = 1;
            Projectile.timeLeft = 2;//存在时间60 = 1秒
            Projectile.friendly = false;// 攻击敌方？
            Projectile.hostile = false;// 攻击友方？
            Projectile.ignoreWater = true;//忽视水？
            Projectile.tileCollide = false;//不穿墙？
            Projectile.penetrate = 1;//穿透数量 -1无限
            Projectile.aiStyle = -1;//附带原版弹幕AI ID
            Projectile.alpha = 255;
            Main.projFrames[Projectile.type] = 1;//动画被分成几份
        }
        public override void AI()
        {
            Projectile.timeLeft = 2;
            Player player = Main.player[Projectile.owner];
            Vector2 deflection = Vector2.Normalize(player.Center - Projectile.Center);
            Projectile.velocity = (Projectile.velocity + deflection) * 30 / 31f;
            if (Projectile.Hitbox.Intersects(player.Hitbox))
            {
                int i = (player.statLifeMax2 - player.statLife) / 20;
                player.statLife += i;
                player.HealEffect(i);
                Projectile.Kill();
            }
            for (int i = 0; i < 3; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height,
                    ModContent.DustType<RBDust>());
                dust.position = Projectile.Center - Projectile.velocity * i / 3f;
                dust.velocity *= 0;
            }
        }
    }
    public class Rainbow_Streat : ModProjectile
    {
        public override string Texture => "MysteriousKnives/Pictures/Projectiles/Another/Projectile_873，长枪是919";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("七彩矢");
            Main.projFrames[Projectile.type] = 1;//动画被分成几份
        }
        public override void SetDefaults()
        {
            Projectile.width = 20;//宽
            Projectile.height = 20;//高
            Projectile.scale = 1f;//体积倍率
            Projectile.timeLeft = 600;//存在时间60 = 1秒
            Projectile.friendly = true;// 攻击敌方？
            Projectile.hostile = false;// 攻击友方？
            Projectile.ignoreWater = true;//忽视水？
            Projectile.tileCollide = false;//不穿墙？
            Projectile.penetrate = 1;//穿透数量 -1无限
            Projectile.alpha = 0;
        }
        public Color color;
        public int d;
        public override void OnSpawn(IEntitySource source)
        {
            switch (Main.rand.Next(8))
            {
                case 0: color = new(0.9f, 0.63f, 1f); d = MKDustID.CSDust; break;
                case 1: color = new(0.33f, 0.33f, 0.33f); d = MKDustID.ABDust; break;
                case 2: color = new(0.45f, 0.04f, 0.75f); d = MKDustID.ASDust; break;
                case 3: color = new(1f, 0.39f, 0.22f); d = MKDustID.CBDust; break;
                case 4: color = new(0.2f, 0.95f, 0.13f); d = MKDustID.RBDust; break;
                case 5: color = new(0.29f, 0.37f, 0.88f); d = MKDustID.SKDust; break;
                case 6: color = new(1f, 0.9f, 0.27f); d = MKDustID.STDust; break;
                case 7: color = new(0.55f, 0.7f, 0.13f); d = MKDustID.WVDust; break;
            }
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
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
                if (Projectile.timeLeft > 569) Projectile.timeLeft++;
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            SpriteBatch sb = Main.spriteBatch;
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            Vector2 origin = texture.Size() / 2f;
            Color drawcolor = color;
            drawcolor.A = 0;
            Vector2 pos = Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);
            float lerp = ((float)Math.Cos(Main.GameUpdateCount / 2) * 0.2f + 1) * 0.8f;
            Vector2 scale = new Vector2(0.5f, 5f) * lerp;
            sb.Draw(texture, pos, null, drawcolor, Projectile.rotation, origin, Projectile.scale * 0.9f, 0, 0);
            sb.Draw(texture, pos, null, drawcolor * lerp, (float)Math.PI / 2f, origin, scale * 0.8f, 0, 0);
            sb.Draw(texture, pos, null, drawcolor * lerp * 0.5f, 0f, origin, scale * 0.6f, 0, 0);
            return false;
        }
    }
} 