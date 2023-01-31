using static MysteriousKnives.Projectiles.MKchannel;

namespace MysteriousKnives.Projectiles
{
    public class MysteriousKnife : ModProjectile
    {
        public override string Texture => "MysteriousKnives/Pictures/Projectiles/Another/Projectile_873，长枪是919";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("诡秘飞刀");
            Main.projFrames[Projectile.type] = 8;
        }
        public override void SetDefaults()
        {
            Projectile.width = 32;//宽
            Projectile.height = 32;//高
            Projectile.scale = 0.5f;//体积倍率
            Projectile.DamageType = ModContent.GetInstance<Mysterious>();// 伤害类型
            Projectile.friendly = true;// 攻击敌方？
            Projectile.hostile = false;// 攻击友方？
            Projectile.ignoreWater = true;//忽视水？
            Projectile.tileCollide = false;//不穿墙？
            Projectile.penetrate = -1;//穿透数量 -1无限
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
            Projectile.alpha = 255;
            Projectile.extraUpdates = 4;
            Projectile.timeLeft = 600 * (1 + Projectile.extraUpdates);//存在时间60 = 1秒
            ProjectileID.Sets.TrailCacheLength[Type] = 15 * (1 + Projectile.extraUpdates);
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
            get { return (Projmode)Projectile.frameCounter; }
            set { Projectile.frameCounter = (int)value; }
        }
        public enum Projtype
        {
            RB, WV, SK, CS, AB, CB, ST, AS
        }
        public Projtype ProjType
        {
            get { return (Projtype)Projectile.frame; }
            set { Projectile.frame = (int)value; }
        }
        public static Color GetColor(int projType)
        {
            Color color = projType switch
            {
                0 => new(0, 255, 100, 0),//RB
                1 => new(225, 255, 0, 255),//WV
                2 => new(0, 155, 255, 0),//SK
                3 => new(255, 120, 220, 0),//CS
                4 => new(0, 0, 0, 255),//AB
                5 => new(255, 100, 0, 0),//CB
                6 => new(255, 253, 50, 50),//ST
                7 => new(135, 0, 255, 0),//AS
                _ => Color.White,
            };
            return color;
        }
        public float Alpha
        {
            get { return Projectile.ai[1]; }
            set { Projectile.ai[1] = value; }
        }
        public override void OnSpawn(IEntitySource source)
        {
            Projectile.ai[1] = 255;
            Projectile.velocity /= Exup;
            ProjType = Projectile.ai[0] switch
            {
                1 => (Projtype)Main.rand.Next(4),
                2 => (Projtype)Main.rand.Next(7),
                _ => (Projtype)Main.rand.Next(8),
            };
            base.OnSpawn(source);
        }
        public override void AI()
        {
            MysteriousKnives.draw2 = true;
            switch (State)
            {
                case Projmode.spawn:
                    Projectile.friendly = false;
                    if (Alpha > 0)
                    {
                        Alpha -= 255f / (25 * Exup);
                        Projectile.alpha = (int)Alpha;
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
                        State = Projmode.attack;
                    }
                    break;
                case Projmode.attack:
                    Projectile.friendly = true;
                    NPC target = ChooseTarget(Projectile, MaxDis: 5000);
                    if (target != null)
                    {
                        if (Projectile.localAI[0] < 20)
                        {
                            Projectile.localAI[0]++;
                        }
                        float lerp = Projectile.localAI[0] / 20 * (10 + 5 * Projectile.ai[0]);
                        Projectile.velocity = ChaseVel(Projectile, target, lerp);
                    }
                    else
                    {
                        Projectile.localAI[0] = 0;
                    }
                    if (Projectile.timeLeft <= 60 * Exup)
                    {
                        State = Projmode.disappear;
                    }
                    break;
                case Projmode.disappear:
                    Projectile.friendly = false;
                    if (Alpha < 255)
                    {
                        Alpha += 255f / (50 * Exup);
                        Projectile.alpha = (int)Alpha;
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
            Lighting.AddLight(Projectile.Center, ProjType != Projtype.AB ?
                GetColor((int)ProjType).ToVector3() : new(0.66f, 0.66f, 0.66f));//RGB
            base.AI();
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.timeLeft = 60 * Exup;
            Player player = Main.player[Projectile.owner];
            switch (ProjType)
            {
                case Projtype.RB:
                    Projectile.NewProjectile(Projectile.GetSource_OnHit(target), target.Center,
                        new Vector2(0), MKProjID.RB_Ray, 0, 0, player.whoAmI);
                    RBbuffs(player); break;
                case Projtype.WV: WVbuffs(target); break;
                case Projtype.SK: SKbuffs(target); break;
                case Projtype.CS: CSbuffs(target); break;
                case Projtype.AB: ABbuffs(target); break;
                case Projtype.CB: CBbuffs(target); break;
                case Projtype.ST: STbuffs(player); break;
                case Projtype.AS: ASbuffs(player); break;
            }
            base.OnHitNPC(target, damage, knockback, crit);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            SpriteBatch sb = Main.spriteBatch;
            /*sb.End();
            sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.None,
                RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);*/
            Texture2D tex = TextureAssets.Projectile[Type].Value;
            //tex = GetT2D("Projectiles/Another/Extra_998").Value;
            Vector2 origin = tex.Size() / 2f;
            Color drawcolor = GetColor((int)ProjType) * ((255 - Projectile.alpha) / 510f + 0.025f * Projectile.ai[0]);
            Vector2 pos = Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);
            float lerp = ((float)Math.Cos(Main.GameUpdateCount / 1.5f) * 0.2f + 1) * 0.8f;
            Vector2 scale = new Vector2(0.5f, 5f) * lerp * 0.8f * ((255 - Projectile.alpha) / 255f) * (Projectile.scale / 2 + 0.5f);
            float length = Projectile.oldPos.Length;

            /*Effect eff = ModContent.Request<Effect>("MysteriousKnives/Effects/Content/放大镜", AssetRequestMode.ImmediateLoad).Value;
            eff.CurrentTechnique.Passes[0].Apply();
            eff.Parameters["fix"].SetValue(new Vector2(Main.screenWidth / (float)Main.screenHeight, 1));
            eff.Parameters["pos"].SetValue(WorldPosToCoords(Projectile.Center));*/

            for (int i = 0; i < length; i++)
            {
                float dawl = (length - i) / length;
                Vector2 tral = new(1f);
                sb.Draw(tex, Projectile.oldPos[i] - Main.screenPosition, null, drawcolor * dawl,
                    Projectile.oldRot[i], origin, Projectile.scale * tral * 0.6f, 0, 0);
            }
            sb.Draw(tex, pos, null, drawcolor * lerp, (float)Math.PI / 2f, origin, scale, 0, 0);
            sb.Draw(tex, pos, null, drawcolor * lerp * 0.75f, 0f, origin, scale * 0.75f, 0, 0);
            sb.Draw(tex, pos, null, new Color(1, 1, 1, 0.5f) * ((255 - Projectile.alpha) / 255f),
                Projectile.rotation, origin, Projectile.scale, 0, 0);
            //ChangeSpb(BlendState.AlphaBlend);
            //Main.graphics.GraphicsDevice.SetRenderTarget(Main.screenTargetSwap);
            return false;
        }
        public void DrawSelf(SpriteBatch sb)
        {
            List<VertexData> bars = new();
            for (int i = 1; i < Projectile.oldPos.Length; i++)
            {
                Vector2[] data = CalculateVertex(Projectile.oldPos[i], Projectile.oldPos[i - 1], 30);
                var factor = i / (float)Projectile.oldPos.Length;
                var color = Color.Lerp(Color.White, Color.Red, factor);
                var alpha = /*MathHelper.Lerp(1f, 0.05f, factor)*/1;
                bars.Add(new VertexData(data[0], color, new Vector3((float)Math.Sqrt(factor), 1, alpha)));
                bars.Add(new VertexData(data[1], color, new Vector3((float)Math.Sqrt(factor), 1, alpha)));
            }
            List<VertexData> triangleList = new();

            if (bars.Count > 2)
            {

                // 按照顺序连接三角形
                triangleList.Add(bars[0]);
                var vertex = new VertexData((bars[0].Position + bars[1].Position) * 0.5f + Vector2.Normalize(Projectile.velocity) * 30, Color.White,
                    new Vector3(0, 0.5f, 1));
                triangleList.Add(bars[1]);
                triangleList.Add(vertex);
                for (int i = 0; i < bars.Count - 2; i += 2)
                {
                    triangleList.Add(bars[i]);
                    triangleList.Add(bars[i + 2]);
                    triangleList.Add(bars[i + 1]);

                    triangleList.Add(bars[i + 1]);
                    triangleList.Add(bars[i + 2]);
                    triangleList.Add(bars[i + 3]);
                }


                sb.End();
                sb.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);

                // 干掉注释掉就可以只显示三角形栅格
                //RasterizerState rasterizerState = new RasterizerState();
                //rasterizerState.CullMode = CullMode.None;
                //rasterizerState.FillMode = FillMode.WireFrame;
                //Main.graphics.GraphicsDevice.RasterizerState = rasterizerState;

                var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
                var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));

                // 把变换和所需信息丢给shader
                Effect DefaultEffect = ModContent.Request<Effect>("MysteriousKnives/Effects/Content/Trail", AssetRequestMode.ImmediateLoad).Value;
                Texture2D MainColor = ModContent.Request<Texture2D>("MysteriousKnives/Pictures/Projectiles/Another/heatmap").Value;
                Texture2D MainShape = ModContent.Request<Texture2D>("MysteriousKnives/Pictures/Projectiles/Another/Extra_197").Value;
                Texture2D MaskColor = ModContent.Request<Texture2D>("MysteriousKnives/Pictures/Projectiles/Another/Extra_189").Value;
                DefaultEffect.Parameters["uTransform"].SetValue(model * projection);
                DefaultEffect.Parameters["uTime"].SetValue(-(float)Main.time * 0.03f);
                Main.graphics.GraphicsDevice.Textures[0] = MainColor;
                Main.graphics.GraphicsDevice.Textures[1] = MainShape;
                Main.graphics.GraphicsDevice.Textures[2] = MaskColor;
                Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
                Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
                Main.graphics.GraphicsDevice.SamplerStates[2] = SamplerState.PointWrap;

                DefaultEffect.CurrentTechnique.Passes[0].Apply();


                Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList.ToArray(), 0, triangleList.Count / 3);

            }
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            writer.Write(Projectile.frame);
            writer.Write(Projectile.localAI[0]);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            Projectile.frame = reader.ReadInt32();
            Projectile.localAI[0] = reader.ReadSingle();
        }
        public static Vector2[] Smooth(Vector2[] vecs, int extraLength)//平滑处理，增加标记的坐标点
        {
            int l = vecs.Length;
            extraLength += l;

            Vector2[] scVecs = new Vector2[extraLength];
            for (int n = 0; n < extraLength; n++)
            {
                float t = n / (float)extraLength;
                float k = (l - 1) * t;
                int i = (int)k;
                float vk = k % 1;
                if (i == 0)
                {
                    scVecs[n] = Vector2.CatmullRom(2 * vecs[0] - vecs[1], vecs[0], vecs[1], vecs[2], vk);
                }
                else if (i == l - 2)
                {
                    scVecs[n] = Vector2.CatmullRom(vecs[l - 3], vecs[l - 2], vecs[l - 1], 2 * vecs[l - 1] - vecs[l - 2], vk);
                }
                else
                {
                    scVecs[n] = Vector2.CatmullRom(vecs[i - 1], vecs[i], vecs[i + 1], vecs[i + 2], vk);
                }
            }
            return scVecs;
        }
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
    }
}
