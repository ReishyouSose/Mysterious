namespace MysteriousKnives
{
    public class MysteriousKnives : Mod
    {
        public Effect Lens;
        public RenderTarget2D render;
        public static bool draw = false;
        public static bool draw2 = false;
        public static bool draw3 = false;
        public static bool draw4 = true;
        //public static Effect scshader;
        public override void Load()
        {
            Lens = ModContent.Request<Effect>("MysteriousKnives/Effects/Content/Lens", AssetRequestMode.ImmediateLoad).Value;
            On.Terraria.Main.GUIChatDrawInner += Main_GUIChatDrawInner;
            On.Terraria.Player.HasUnityPotion += Player_HasUnityPotion;
            On.Terraria.Graphics.Effects.FilterManager.EndCapture += FilterManager_EndCapture;
            Main.OnResolutionChanged += (obj) => { render = new RenderTarget2D(Main.graphics.GraphicsDevice, Main.screenWidth, Main.screenHeight); };
            // 注意设置正确的Pass名字，Scene的名字可以随便填，不和别的Mod以及原版冲突即可
            //Effect scshader = ModContent.Request<Effect>("MysteriousKnives/Effects/Content/ScreenShader", AssetRequestMode.ImmediateLoad).Value;
            Filters.Scene["MKScreenShader"] = new Filter(
                new MKScreenShader(new Ref<Effect>(ModContent.Request<Effect>("MysteriousKnives/Effects/Content/ScreenShader", AssetRequestMode.ImmediateLoad).Value), "screenShader"), EffectPriority.Medium);
            Filters.Scene["MKScreenShader"].Load();
        }
        public override void Unload()
        {
            On.Terraria.Main.GUIChatDrawInner -= Main_GUIChatDrawInner;
            On.Terraria.Player.HasUnityPotion -= Player_HasUnityPotion;
            On.Terraria.Graphics.Effects.FilterManager.EndCapture -= FilterManager_EndCapture;
        }
        public void FilterManager_EndCapture(On.Terraria.Graphics.Effects.FilterManager.orig_EndCapture orig,
            Terraria.Graphics.Effects.FilterManager self, RenderTarget2D finalTexture, RenderTarget2D screenTarget1,
            RenderTarget2D screenTarget2, Color clearColor)
        {
            GraphicsDevice gd = Main.graphics.GraphicsDevice;
            SpriteBatch sb = Main.spriteBatch;
            if (render == null) render = new(gd, Main.screenWidth, Main.screenHeight);
            draw3 = true;

            if (draw3)
            {

                foreach (Projectile proj in Main.projectile)
                    if (proj.type == ModContent.ProjectileType<Proj2>() && proj.active && proj.ai[0] == 0)//白屏
                    {
                        sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
                        sb.Draw(TextureAssets.MagicPixel.Value, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.White * proj.ai[1]);
                        sb.End();
                        sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
                        Player player = Main.player[Main.myPlayer];
                        Main.PlayerRenderer.DrawPlayer(Main.Camera, player, player.position, 0, Vector2.Zero);
                        sb.End();
                    }
                foreach (Projectile proj in Main.projectile)
                {
                    if (proj.type == ModContent.ProjectileType<Proj2>() && proj.active)//在白屏前面绘制黑色的刀光弹幕(无render使用)
                    {
                        if (proj.ai[0] == 1)
                        {
                            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
                            ((Proj2)proj.ModProjectile).Draw(sb);
                            sb.End();
                        }
                    }
                    if (proj.type == ModContent.ProjectileType<Proj1>() && proj.active)//然后是斩击本体的处理   每个弹幕都会进行一次操作，所以弹幕多了会很卡很卡
                    {
                        //先在自己的render上画这个弹幕
                        gd.SetRenderTarget(render);
                        gd.Clear(Color.Transparent);
                        sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
                        ((Proj1)proj.ModProjectile).Draw(sb);
                        sb.End();
                        //然后在随便一个render里绘制屏幕，并把上面那个带弹幕的render传进shader里对屏幕进行处理
                        //原版自带的screenTargetSwap就是一个可以使用的render，（原版用来连续上滤镜）
                        gd.SetRenderTarget(Main.screenTargetSwap);
                        gd.Clear(Color.Transparent);
                        sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                        Effect offsetEff = ModContent.Request<Effect>("MysteriousKnives/Effects/Content/Offset", AssetRequestMode.ImmediateLoad).Value;
                        offsetEff.CurrentTechnique.Passes[0].Apply();
                        offsetEff.Parameters["tex0"].SetValue(render);//render可以当成贴图使用或者绘制。（前提是当前gd.SetRenderTarget的不是这个render，否则会报错）
                        offsetEff.Parameters["offset"].SetValue(proj.velocity / 10);
                        offsetEff.Parameters["invAlpha"].SetValue(proj.ai[0]);
                        sb.Draw(Main.screenTarget, Vector2.Zero, Color.White);
                        sb.End();

                        //最后在screenTarget上把刚刚的结果画上
                        gd.SetRenderTarget(Main.screenTarget);
                        gd.Clear(Color.Transparent);
                        sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
                        sb.Draw(Main.screenTargetSwap, Vector2.Zero, Color.White);
                        sb.End();
                    }
                }
            }
            if (draw2)
            {
                gd.SetRenderTarget(Main.screenTargetSwap);
                gd.Clear(Color.Transparent);
                sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
                sb.Draw(Main.screenTarget, Vector2.Zero, Color.White);
                sb.End();

                gd.SetRenderTarget(render);
                gd.Clear(Color.Transparent);
                sb.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointWrap, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
                DrawAdd2(sb, out var pos);
                sb.End();

                gd.SetRenderTarget(Main.screenTarget);
                gd.Clear(Color.Transparent);
                sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
                sb.Draw(Main.screenTargetSwap, Vector2.Zero, Color.White);
                sb.End();
                sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                //gd.Textures[0] = render;
                gd.Textures[1] = ModContent.Request<Texture2D>("MysteriousKnives/Pictures/Effects/Cosmic", AssetRequestMode.ImmediateLoad).Value;
                Effect BigTentacle = ModContent.Request<Effect>("MysteriousKnives/Effects/Content/BigTentacle", AssetRequestMode.ImmediateLoad).Value;
                BigTentacle.CurrentTechnique.Passes[0].Apply();
                BigTentacle.Parameters["m"].SetValue(0.9f);
                BigTentacle.Parameters["n"].SetValue(0.6f);
                BigTentacle.Parameters["color"].SetValue(Main.DiscoColor.ToVector3());
                /*Effect enlarge = ModContent.Request<Effect>("MysteriousKnives/Effects/Content/放大镜", AssetRequestMode.ImmediateLoad).Value;
                gd.Textures[1] = render;
                enlarge.Parameters["fix"].SetValue(new Vector2(Main.screenWidth / (float)Main.screenHeight, 1));
                enlarge.Parameters["pos"].SetValue(WorldPosToCoords(pos));
                enlarge.CurrentTechnique.Passes[0].Apply();*/
                sb.Draw(render, Vector2.Zero, Color.White);
                sb.End();
            }
            if (draw)
            {

                gd.SetRenderTarget(render);
                gd.Clear(Color.Transparent);
                sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
                sb.Draw(Main.screenTarget, Vector2.Zero, Color.White);
                sb.End();

                gd.SetRenderTarget(Main.screenTargetSwap);
                gd.Clear(Color.Transparent);
                sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default,
                    RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
                DrawAdd(sb, out Vector2 pos);
                sb.End();

                gd.SetRenderTarget(Main.screenTarget);
                gd.Clear(Color.Transparent);
                sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                Effect eff_mag = ModContent.Request<Effect>("MysteriousKnives/Effects/Content/Magnification", AssetRequestMode.ImmediateLoad).Value;
                eff_mag.CurrentTechnique.Passes[1].Apply();
                eff_mag.Parameters["tex0"].SetValue(Main.screenTargetSwap);
                eff_mag.Parameters["i"].SetValue(0.1f);
                /*Effect enlarge = ModContent.Request<Effect>("MysteriousKnives/Effects/Content/放大镜", AssetRequestMode.ImmediateLoad).Value;
                gd.Textures[0] = render;
                gd.Textures[1] = Main.screenTargetSwap;
                enlarge.Parameters["fix"].SetValue(new Vector2(Main.screenWidth / (float)Main.screenHeight, 1));
                enlarge.Parameters["pos"].SetValue(WorldPosToCoords(pos));
                enlarge.Parameters["mult"].SetValue(/*Main.rand.NextFloat(-0.2f, 0.2f)2f);
                enlarge.Parameters["followdis"].SetValue(true);
                enlarge.CurrentTechnique.Passes[0].Apply();*/
                sb.Draw(render, Vector2.Zero, Color.White);
                sb.End();
            }

            /*gd.SetRenderTarget(render);
            gd.Clear(Color.Transparent);
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            sb.Draw(Main.screenTarget, Vector2.Zero, Color.White);
            sb.End();

            gd.SetRenderTarget(Main.screenTargetSwap);
            gd.Clear(Color.Transparent);
            sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default,
                    RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            foreach (Projectile p in Main.projectile)
            {
                if (p.active && p.ModProjectile is MKchannel channel)
                {
                    channel.VertexDraw(sb);
                }
                if (p.active && p.ModProjectile is Gray gray)
                {
                    //Main.NewText("go");
                    gray.Drawself(sb);
                }
            }
            sb.End();

            gd.SetRenderTarget(Main.screenTarget);
            gd.Clear(Color.Transparent);
            sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            Effect gr = ModContent.Request<Effect>("MysteriousKnives/Effects/Content/灰度替换", AssetRequestMode.ImmediateLoad).Value;
            gr.CurrentTechnique.Passes[0].Apply();
            gd.Textures[0] = Main.screenTargetSwap;
            gd.Textures[1] = ModContent.Request<Texture2D>("MysteriousKnives/Pictures/Effects/Cosmic", AssetRequestMode.ImmediateLoad).Value;
            gd.Textures[2] = render;
            sb.Draw(Main.screenTargetSwap/*render, Vector2.Zero, Color.White);
            sb.End();*/
            orig(self, finalTexture, screenTarget1, screenTarget2, clearColor);
        }
        public static void DrawAdd(SpriteBatch sb, out Vector2 pos)
        {
            //Texture2D tex = GetT2D("Effects/空间扭曲角度").Value;
            //sb.Draw(tex, channel.drawpos - Main.screenPosition, null, Color.White, 0, tex.Size() / 2f, 1f, 0, 0);
            pos = Vector2.One;
            foreach (Projectile proj in Main.projectile)
            {
                if (proj.active && proj.ModProjectile is SpaceWarpProj wrap)
                {
                    wrap.DrawSelf(sb);
                }
            }

            foreach (Projectile proj in Main.projectile)
            {
                if (proj.active && proj.ModProjectile is MKchannel channel)
                {
                    //Texture2D tex = GetT2D("Effects/空间扭曲角度").Value;
                    //sb.Draw(tex, channel.drawpos - Main.screenPosition, null, Color.White, 0, tex.Size() / 2f, 1f, 0, 0);
                    //channel.VertexDraw(sb);
                    //scshader.Parameters["color"].SetValue(new Vector4(Color.Red.ToVector3(), 1));
                    channel.DrawSelf(sb);
                    pos = channel.drawpos;
                }
            }
        }
        public static void DrawAdd2(SpriteBatch sb, out Vector2 pos)
        {
            pos = Vector2.Zero;
            foreach (Projectile proj in Main.projectile)
            {
                if (proj.active && proj.ModProjectile is MKchannel channel)
                {
                    //Texture2D tex = GetT2D("Effects/空间扭曲角度").Value;
                    //sb.Draw(tex, channel.drawpos - Main.screenPosition, null, Color.White, 0, tex.Size() / 2f, 1f, 0, 0);
                    channel.DrawSelf(sb);
                    pos = channel.drawpos;
                }
            }
            foreach (Projectile proj in Main.projectile)
            {
                if (proj.active && proj.ModProjectile is MysteriousKnife knife)
                {
                    //knife.DrawSelf(sb);
                }
            }
        }
        public static bool Player_HasUnityPotion(On.Terraria.Player.orig_HasUnityPotion orig, Player self)
        {
            for (int i = 0; i < 58; i++)
            {
                if (self.inventory[i].type == ItemID.WormholePotion && self.inventory[i].stack > 0)
                {
                    return true;
                }
                if (self.inventory[i].type == MKItemID.MKOrigin) return true;
            }
            return false;
        }
        public override uint ExtraPlayerBuffSlots => 999;
        public static object TextDisplayCache => typeof(Main).GetField("_textDisplayCache",
        System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(Main.instance);
        public bool hover = false;
        public bool MouseLeft = false;
        public static float GetButtonPosition(int num, bool happy = false)
        {
            int i = 180;
            if (happy) i += 10;
            float dis = ChatManager.GetStringSize(FontAssets.MouseText.Value, Language.GetTextValue("LegacyInterface.64"), new(0.9f)).X;
            float postion = i + (Main.screenWidth - 800) / 2 + (num - 1) * (dis + 30);
            return postion;
        }
        public string text = Language.GetTextValue("LegacyInterface.28");
        public int num1;
        public void Main_GUIChatDrawInner(On.Terraria.Main.orig_GUIChatDrawInner orig, Main self)
        {
            orig(self);
            NPC npc = Main.npc[Main.player[Main.myPlayer].talkNPC];
            if (npc.type == NPCID.TownCat || npc.type == NPCID.TownDog || npc.type == NPCID.TownBunny)
            {
                DynamicSpriteFont font = FontAssets.MouseText.Value;
                string focusText = Language.GetTextValue("LegacyInterface.28");
                int numLines = (int)TextDisplayCache.GetType().GetProperty("AmountOfLines",
                    System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public).GetValue(TextDisplayCache) + 1;
                Vector2 scale = new(0.9f);
                Vector2 stringSize = ChatManager.GetStringSize(font, Language.GetTextValue("LegacyInterface.51"), scale);
                Vector2 vector = new(1f);
                if (stringSize.X > 260f) vector.X *= 260f / stringSize.X;

                float button = GetButtonPosition(3, false);
                Vector2 position = new(button, 130 + numLines * 30);
                stringSize = ChatManager.GetStringSize(font, focusText, scale);
                if (Main.MouseScreen.Between(position, position + stringSize * scale * vector.X) && !PlayerInput.IgnoreMouseInterface)
                {
                    Main.LocalPlayer.mouseInterface = true;
                    Main.LocalPlayer.releaseUseItem = false;
                    scale *= 1.2f;
                    if (!hover) SoundEngine.PlaySound(SoundID.MenuTick);
                    hover = true;
                }
                else
                {
                    if (hover) SoundEngine.PlaySound(SoundID.MenuTick);
                    hover = false;
                }

                int MTC = Main.mouseTextColor;
                Color unhoverColor = new(MTC, MTC, MTC / 2, MTC);
                Color hoveringColor = new(228, 206, 114, Main.mouseTextColor / 2);
                ChatManager.DrawColorCodedStringShadow(Main.spriteBatch, font, focusText, position + stringSize * vector * 0.5f,
                    !hover ? Color.Black : Color.Brown, 0f, stringSize * 0.5f, scale * vector);
                ChatManager.DrawColorCodedString(Main.spriteBatch, font, focusText, position + stringSize * vector * 0.5f,
                    !hover ? unhoverColor : hoveringColor, 0f, stringSize * 0.5f, scale * vector);

                if (Main.MouseScreen.Between(position, position + stringSize * scale * vector.X) && !PlayerInput.IgnoreMouseInterface)
                {
                    if (Main.mouseLeft) MouseLeft = true;
                    if (MouseLeft && !Main.mouseLeft)
                    {
                        Main.playerInventory = true;
                        Main.npcChatText = "";
                        SoundEngine.PlaySound(SoundID.MenuTick);
                        Player player = Main.player[Main.myPlayer];
                        Chest shop = Main.instance.shop[98];
                        if (npc.type == NPCID.TownCat)
                        {
                            Main.SetNPCShopIndex(98);
                            int nextSlot = 0;
                            bool sell = true;
                            if (player.HasItem(ItemID.AngryBonesBanner))
                            {

                            }
                            if (NPC.killCount[419] >= 5 || sell)//海盗船
                            {
                                shop.item[nextSlot].SetDefaults(905);//钱币枪
                                nextSlot++;
                                shop.item[nextSlot].SetDefaults(855);//幸运币
                                nextSlot++;
                                shop.item[nextSlot].SetDefaults(2584);//海盗法杖
                                nextSlot++;
                            }
                            if (NPC.killCount[50] >= 1 || sell)//史莱姆王
                            {
                                shop.item[nextSlot].SetDefaults(1309);//史莱姆法杖
                                nextSlot++;
                            }
                            if (NPC.killCount[120] >= 50 || sell)//混沌精
                            {
                                shop.item[nextSlot].SetDefaults(1326);//裂位
                                nextSlot++;
                            }
                            for (int i = 269; i <= 280; i++)
                            {
                                num1 += NPC.killCount[i];
                            }
                            if (num1 >= 50 || sell)//生锈装甲骷髅
                            {
                                shop.item[nextSlot].SetDefaults(1517);//骨之羽
                                nextSlot++;
                                shop.item[nextSlot].SetDefaults(1183);//妖灵瓶
                                nextSlot++;
                            }
                            if (NPC.killCount[253] >= 50 || sell)//死神
                            {
                                shop.item[nextSlot].SetDefaults(1327);//死神镰刀
                                nextSlot++;
                            }
                        }
                        else if (npc.type == NPCID.TownDog)
                        {
                            Main.SetNPCShopIndex(98);
                            int nextSlot = 0;
                            shop.item[nextSlot].SetDefaults(ItemID.TerraBlade);
                            shop.item[nextSlot].value = 1000;
                        }
                        else if (npc.type == NPCID.TownBunny)
                        {
                            Main.SetNPCShopIndex(98);
                            int nextSlot = 0;
                            shop.item[nextSlot].SetDefaults(ItemID.LastPrism);
                            shop.item[nextSlot].value = 1000;
                        }
                        MouseLeft = false;
                    }
                }
            }
        }
    }
}