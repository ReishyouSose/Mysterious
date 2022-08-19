namespace MysteriousKnives.Projectiles
{
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
            if (!player.channel) Projectile.Kill();
            else
            {
                NPC target = ChooseTarget(Projectile, true);
                float root = player.GetTotalDamage(ModContent.GetInstance<Mysterious>()).ApplyTo(PlySelect(player).damage);
                Projectile.Center = player.Center;
                Projectile.timeLeft = 2;
                player.itemTime = 2;
                player.itemAnimation = 2;
                if (target != null)
                {
                    player.direction = target.position.X > player.position.X ? 1 : -1;
                    drawpos = target.Center;
                    int space = 1;
                    if (Main.GameUpdateCount % space == 0)
                    {
                        float random = 1 + 0.01f * Main.rand.Next(-15, 15);
                        int damage = (int)(root * target.takenDamageMultiplier * 1.5f * random * space);
                        NoSoundStrike(target, damage, new(Main.DiscoColor.ToVector3()) { A = 0 });
                        player.addDPS(damage);
                    }

                    int space2 = 3;
                    if (Main.GameUpdateCount % space2 == 0)
                    {
                        float rand = Main.rand.NextFloat(MathHelper.TwoPi);
                        Vector2 pos = target.Center + new Vector2((float)Math.Cos(rand), (float)Math.Sin(rand)) * 1500;
                        Projectile proj = Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), pos,
                            Vector2.Normalize(target.Center - pos) * 10f, MKProjID.Rainbow_Halberd,
                            (int)root * space2, 0, player.whoAmI);
                        proj.extraUpdates += Main.rand.Next(-3, 3);
                    }

                    switch (Main.rand.Next(8))
                    {
                        case 0: target.AddBuff(ModContent.BuffType<Crystallization>(), 300); break;
                        case 1:
                            if (target.rarity != 0 || target.boss)
                            {
                                target.AddBuff(ModContent.BuffType<ConvergentBurst6>(), 180);
                            }
                            break;
                        case 2: target.AddBuff(ModContent.BuffType<IndescribableFear>(), 7 * 180); break;
                        case 3: target.AddBuff(ModContent.BuffType<SunkerCancer>(), 720); break;
                        case 4: target.AddBuff(ModContent.BuffType<WeirdVemon>(), 720); break;
                        case 5:
                            if (player.statLife < player.statLifeMax2)
                            {
                                Projectile.NewProjectile(Projectile.GetSource_FromAI(), target.Center, new Vector2(0, 0),
                                    ModContent.ProjectileType<RB_Ray>(), 0, 0, player.whoAmI);
                            }
                            break;
                    }

                    float t = Main.GameUpdateCount * 0.1f;
                    if (dis < 10) dis++;
                    else dis = 10 * ((float)-Math.Cos(t / 1.25f) / 10 + 1.2f);
                    for (int i = 0; i < 72; i++)
                    {
                        Dust dust = Dust.NewDustDirect(target.Center, target.width, target.height,
                            Main.dayTime ? ModContent.DustType<HalloweenDust>() : ModContent.DustType<RainbowDust>());
                        dust.position = new Vector2((int)(Math.Pow(-1, i)) * (float)Math.Cos(Math.PI / 36 * (i + t)),
                            (float)Math.Sin(Math.PI / 36 * (i + t))) * 10f * dis + target.Center;
                        dust.velocity *= 0;
                    }
                }
            }
        }
        public Vector2 drawpos;
        public bool down = true;
        public override bool PreDraw(ref Color lightColor)
        {/*
            if (Projectile.ai[0] < 10) Projectile.ai[0]++;

            int f = 20, t = 3;
            if (Projectile.frameCounter == 0 || Projectile.frameCounter == f * t) down = !down;
            if (!down) Projectile.frameCounter++;
            else Projectile.frameCounter--;
            Projectile.frame = Projectile.frameCounter / t;
            Projectile.frame %= f;

            int x = Projectile.frame;
            int y = Projectile.frame / 5;
            x %= 6;
            SpriteBatch sb = Main.spriteBatch;
            ChangeSpb(BlendState.Additive);
            if (drawpos != Vector2.Zero)
            {
                sb.Draw(
                    texture: TextureAssets.Projectile[Type].Value,
                    position: drawpos - Main.screenPosition,
                    sourceRectangle: new Rectangle(x * 144, y * 144, 144, 144),
                    color: Main.DiscoColor,
                    rotation: (float)(Math.Cos(MathHelper.Lerp(1, 10, Main.GameUpdateCount * 0.1f))),
                    origin: new Vector2(72, 72),
                    scale: Projectile.ai[0] < 10 ? 1.5f * Projectile.ai[0] / 10
                        : (float)Math.Sin(Main.GameUpdateCount * 0.2f) * 0.15f + 1.5f,
                    effects: 0,
                    layerDepth: 0);
            }
            ChangeSpb(BlendState.AlphaBlend);*/
            return false;
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            writer.WriteVector2(drawpos);
            writer.Write(down);
            writer.Write(dis);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            drawpos = reader.ReadVector2();
            down = reader.ReadBoolean();
            dis = reader.ReadSingle();
        }
    }
}