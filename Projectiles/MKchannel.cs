namespace MysteriousKnives.Projectiles
{
    public class MKchannel : ModProjectile
    {
        public override string Texture => "MysteriousKnives/Pictures/Projectiles/Another/Arcaea";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("诡秘充能");
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
        }
        public float dis = 0;
        public int Damage;
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
                    Lighting.AddLight(drawpos, Main.DiscoColor.ToVector3() * 2);
                    //MysteriousKnives.draw = true;
                    MysteriousKnives.draw2 = true;
                    if (Projectile.ai[1] < 80)
                    {
                        Projectile.ai[1]++;
                    }
                    if (Projectile.ai[0] == 0)
                    {
                        Projectile.ai[0] = 1;
                    }
                    else if (Projectile.ai[0] < 60)
                    {
                        Projectile.ai[0]++;
                    }
                    else if (Projectile.ai[0] == 60)
                    {
                        Projectile wrap = Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), drawpos,
                            Vector2.Zero, MKProjID.SpaceWrap, 0, 0, Projectile.owner, 0, 50);
                        SoundEngine.PlaySound(new("MysteriousKnives/Sounds/Channel"));
                        Projectile.ai[0] = 60.1f;
                    }
                    //if(!target.active)
                    if (Projectile.ai[0] > 60)
                    {
                        // 帧伤
                        int space = 1;
                        if (Main.GameUpdateCount % space == 0)
                        {
                            float random = 1 + 0.01f * Main.rand.Next(-15, 15);
                            int damage = (int)(root * target.takenDamageMultiplier * 1.5f * random * space);
                            Damage += damage;
                            target.life -= damage;
                            if (target.life < 0)
                            {
                                target.checkDead();
                                Damage = 0;
                            }
                            //NoSoundStrike(target, damage, new(Main.DiscoColor.ToVector3()) { A = 0 });
                            player.addDPS(damage);
                        }
                        // 3帧一剑
                        /*space = 3;
                        if (Main.GameUpdateCount % space == 0)
                        {
                            float rand = Main.rand.NextFloat(MathHelper.TwoPi);
                            Vector2 pos = target.Center + new Vector2((float)Math.Cos(rand), (float)Math.Sin(rand)) * 1500;
                            Projectile proj = Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), pos,
                                Vector2.Normalize(target.Center - pos) * 10f, MKProjID.Rainbow_Halberd,
                                (int)root * space, 0, player.whoAmI);
                            proj.extraUpdates += Main.rand.Next(-3, 3);
                        }*/

                        /*switch (Main.rand.Next(8))
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
                        }*/

                        float t = Main.GameUpdateCount * 0.1f;
                        if (dis < 10) dis++;
                        else dis = 10 * ((float)-Math.Cos(t / 1.25f) / 10 + 1.2f);
                        /*for (int i = 0; i < 72; i++)
                        {
                            Dust dust = Dust.NewDustDirect(target.Center, target.width, target.height,
                                Main.dayTime ? ModContent.DustType<HalloweenDust>() : ModContent.DustType<RainbowDust>());
                            dust.position = Circle(ManyPI(1 / 36f) * (i + Math.Pow(-1, i) * t), 10f * dis) + target.Center;
                            dust.velocity *= 0;
                        }*/
                        int step = 72;
                        int width = 20;
                        float unit = 1f / step;
                        bars = new();
                        for (int i = 1; i < step + 2; i++)
                        {
                            Vector2 pos = Circle(ManyPI(1 / 36f) * (i + t), 10f * dis) + target.Center;
                            Vector2 oldpos = Circle(ManyPI(1 / 36f) * (i - 1 + t), 10f * dis) + target.Center;
                            Vector2 normal = oldpos - pos;
                            normal = Vector2.Normalize(new Vector2(-normal.Y, normal.X));
                            float alpha = MathHelper.Lerp(1f, 0.05f, i * unit / 2 + 0.5f);
                            for (int j = -1; j < 2; j += 2)
                            {
                                Vector2 point = pos + j * normal * width;
                                bars.Add(new VertexData(point, Color.White, new Vector3(unit * i, j == -1 ? 1 : 0, /*alpha*/1)));
                            }
                        }
                    }
                }
            }
        }
        public struct VertexData : IVertexType
        {
            public static VertexDeclaration data = new(new VertexElement[3]
            {
                new VertexElement(0, VertexElementFormat.Vector2, VertexElementUsage.Position, 0),
                new VertexElement(8, VertexElementFormat.Color, VertexElementUsage.Color, 0),
                new VertexElement(12, VertexElementFormat.Vector3, VertexElementUsage.TextureCoordinate, 0)
            });
            public Vector2 Position;
            public Color Color;
            public Vector3 TexCoord;

            public VertexData(Vector2 position, Color color, Vector3 texCoord)
            {
                this.Position = position;
                this.Color = color;
                this.TexCoord = texCoord;
            }

            public VertexDeclaration VertexDeclaration => data;
        }
        public RenderTarget2D render;
        public List<VertexData> bars;
        public Vector2 drawpos;
        public bool down = true;
        public override bool PreDraw(ref Color lightColor)
        {
            var sb = Main.spriteBatch;
            //if (Damage > 0)
            {
                var font = FontAssets.MouseText.Value;
                Vector2 size = ChatManager.GetStringSize(font, Damage.ToString(), Vector2.One);
                ChatManager.DrawColorCodedString(sb, font, Damage.ToString(),
                    Main.player[Projectile.owner].Center + new Vector2(0, -40) - Main.screenPosition,
                    new Color(Main.DiscoColor.ToVector3()) { A = 0 }, 0, size / 2f, new Vector2(1.5f));
            }
            sb.End();
            sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            //Effect wrap = ModContent.Request<Effect>("MysteriousKnives/Effects/Content/SpaceWrap", AssetRequestMode.ImmediateLoad).Value;
            //Main.graphics.GraphicsDevice.Textures[0] = render;
            /*wrap.Parameters["center"].SetValue(WorldPosToCoords(Main.MouseWorld));
            wrap.Parameters["fix"].SetValue(new Vector2(Main.screenWidth / (float)Main.screenHeight, 1));
            wrap.Parameters["r_in"].SetValue(0.4f);
            wrap.Parameters["r_out"].SetValue(0.6f);
            wrap.Parameters["mult"].SetValue(0.02f);
            wrap.CurrentTechnique.Passes[0].Apply();*/
            ChangeSpb(BlendState.AlphaBlend);
            //VertexDraw(sb);

            return false;
        }
        public void VertexDraw(SpriteBatch sb)
        {
            List<VertexData> triangle = new();
            if (bars != null && bars.Count > 2)
            {
                for (int i = 0; i < bars.Count - 2; i += 2)
                {
                    triangle.Add(bars[i]);
                    triangle.Add(bars[i + 1]);
                    triangle.Add(bars[i + 3]);
                    triangle.Add(bars[i]);
                    triangle.Add(bars[i + 2]);
                    triangle.Add(bars[i + 3]);
                }
            }
            if (triangle.Count > 3)
            {
                sb.End();
                sb.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap,
                    DepthStencilState.Default, RasterizerState.CullNone);

                var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
                var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));

                Effect effect = ModContent.Request<Effect>("MysteriousKnives/Effects/Content/Trail", AssetRequestMode.ImmediateLoad).Value;
                Texture2D MainColor = ModContent.Request<Texture2D>("MysteriousKnives/Pictures/Projectiles/Another/heatmap2", AssetRequestMode.ImmediateLoad).Value;
                Texture2D MainShape = ModContent.Request<Texture2D>("MysteriousKnives/Pictures/Projectiles/Another/Extra_197", AssetRequestMode.ImmediateLoad).Value;
                Texture2D MaskColor = ModContent.Request<Texture2D>("MysteriousKnives/Pictures/Projectiles/Another/Extra_189", AssetRequestMode.ImmediateLoad).Value;
                effect.Parameters["uTransform"].SetValue(model * projection);
                effect.Parameters["uTime"].SetValue(Main.GameUpdateCount * 0.01f);
                effect.Parameters["mult"].SetValue(3);
                Main.graphics.GraphicsDevice.Textures[0] = MainColor;
                Main.graphics.GraphicsDevice.Textures[1] = /*MainShape*/MaskColor;
                Main.graphics.GraphicsDevice.Textures[2] = MaskColor;
                Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
                Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
                Main.graphics.GraphicsDevice.SamplerStates[2] = SamplerState.PointWrap;

                effect.CurrentTechnique.Passes["ColorBar"].Apply();
                Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangle.ToArray(), 0, triangle.Count / 3);
                sb.End();
                sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);
            }
        }
        public Vector2 random = new();
        public void DrawSelf(SpriteBatch sb)
        {
            Texture2D tex = GetT2D("Effects/玫瑰绿").Value;
            Rectangle rec = new(0, 0, 210, 195);
            Vector2 origin = new Vector2(210, 195) / 2f;
            float rot = (float)Math.Pow(-(50 - Projectile.ai[1]) / 50, 2) * ManyPI(2);
            if (Projectile.ai[1] > 50) rot = 0;
            float scale;
            if (Projectile.ai[1] < 50)
            {
                scale = (50 - Projectile.ai[1]) / 50f;
            }
            else if (Projectile.ai[1] < 60)
            {
                scale = 0;
            }
            else if (Projectile.ai[1] < 70)
            {
                scale = (Projectile.ai[1] - 60) / 30f;
            }
            else
            {
                scale = (80 - Projectile.ai[1]) / 80f;
            }
            for (int i = 0; i < 13; i++)
            {
                rec.Y = i * 195;
                int dir = (Between(i, 0, 3) || Between(i, 7, 9)) ? 1 : -1;
                rot *= (int)Math.Pow(-1, dir);
                sb.Draw(tex, drawpos - Main.screenPosition, rec, Color.White,
                    rot + MathHelper.ToRadians(Main.GameUpdateCount / 2f), origin, 1 + scale, 0, 0);
            }

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