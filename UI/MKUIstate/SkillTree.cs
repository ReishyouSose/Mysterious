namespace MysteriousKnives.UI.MKUIstate
{
    public class SkillTree : UIState
    {
        internal static bool enable = false;
        public DragableUIPanel bg;
        public MaterialBox view;
        public ButtonUI button;
        public UIPanel side;
        public AddiImage[] skill = new AddiImage[42];
        public UIImage star;
        public enum Skill
        {
            RB1, RB2, RB3, RB4, RB5, RB6, RB7,//0-6
            SK1, SK2, SK3,//7-9
            CS1, CS2, CS3, CS4, CS5,//10-14
            WV1, WV2, WV3, WV4,//15-18
            CB1, CB2, CB3, CB4, CB5, CB6,//19-24
            AB1, AB2, AB3, AB4, AB5, AB6, AB7,//25-31
            ST1, ST2, ST3, ST4, ST5, ST6,//32-37
            AS1, AS2, AS3, AS4//38-41
        }

        public override void OnInitialize()
        {
            bg = new();
            bg.Width.Pixels = 600f;
            bg.Height.Pixels = 600f;
            bg.HAlign = 0.5f;
            bg.VAlign = 0.5f;
            Append(bg);

            star = new(GetT2D("UI/SkillTree/星图"));
            star.Width.Pixels = star.Height.Pixels = 600;
            star.HAlign = star.VAlign = 0.5f;
            star.Top.Pixels = star.Left.Pixels = -10;
            bg.Append(star);

            side = new();
            side.Width.Pixels = 350f;
            side.Height.Pixels = 500f;
            Append(side);

            view = new(280)
            {
                HAlign = 0.5f
            };
            view.Top.Pixels = 155;
            side.Append(view);

            button = new("");
            button.Height.Pixels = 50;
            button.HAlign = 0.5f;
            button.Top.Pixels = view.Height.Pixels + view.Top.Pixels + 5;
            button.OnClick += (evt, uie) => { if (!Lighting) view.Light(); };
            side.Append(button);

            for (int i = 0; i < skill.Length; i++)
            {
                Color color = new();
                if (Between(i, 0, 6))//RB
                    color = new(0, 255, 100, 0);
                else if (Between(i, 7, 9))//SK
                    color = new(0, 155, 255, 0);
                else if (Between(i, 10, 14))//CS
                    color = new(255, 120, 220, 0);
                else if (Between(i, 15, 18))//WV
                    color = new(225, 255, 0, 0);
                else if (Between(i, 19, 24))//CB
                    color = new(255, 100, 0, 0);
                else if (Between(i, 25, 31))//AB
                    color = Color.White;
                else if (Between(i, 32, 37))//ST
                    color = new(255, 255, 100, 0);
                else if (Between(i, 38, 41))//AS
                    color = new(135, 0, 255, 0);
                skill[i] = new(GetT2D("UI/SkillTree/AB"), "", color);
                skill[i].Width.Pixels = 24;
                skill[i].Height.Pixels = 24;
                skill[i].HAlign = skill[i].VAlign = 0.5f;
                skill[i].id = i;
                skill[i].OnClick += (evt, uie) => { int id = (uie as AddiImage).id; if (!Main.dayTime) ModifyView(id); };
                switch ((Skill)i)
                {
                    case Skill.RB1:
                        SetPos(i, 1 / 8f, 75);
                        break;
                    case Skill.RB2:
                        SetPos(i, 1 / 8f - 1 / 32f, 125);
                        break;
                    case Skill.RB3:
                        SetPos(i, 1 / 8f + 1 / 12f, 150);
                        break;
                    case Skill.RB4:
                        SetPos(i, 1 / 8f + 1 / 16f, 200);
                        break;
                    case Skill.RB5:
                        SetPos(i, 1 / 8f + 1 / 12f, 250);
                        break;
                    case Skill.RB6:
                        SetPos(i, 1 / 8f + 1 / 12f, 300);
                        break;
                    case Skill.RB7:
                        SetPos(i, 1 / 8f + 1 / 8f, 350);
                        break;
                    case Skill.SK1:
                        SetPos(i, 3 / 8f, 75);
                        break;
                    case Skill.SK2:
                        SetPos(i, 3 / 8f + 1 / 32f, 175);
                        break;
                    case Skill.SK3:
                        SetPos(i, 3 / 8f - 1 / 16f, 250);
                        break;
                    case Skill.CS1:
                        SetPos(i, 5 / 8f, 75);
                        break;
                    case Skill.CS2:
                        SetPos(i, 5 / 8f + 1 / 32f, 135);
                        break;
                    case Skill.CS3:
                        SetPos(i, 5 / 8f - 1 / 16f, 190);
                        break;
                    case Skill.CS4:
                        SetPos(i, 5 / 8f - 1 / 64f, 260);
                        break;
                    case Skill.CS5:
                        SetPos(i, 5 / 8f + 1 / 16f, 300);
                        break;
                    case Skill.WV1:
                        SetPos(i, 7 / 8f, 75);
                        break;
                    case Skill.WV2:
                        SetPos(i, 7 / 8f + 1 / 32f, 150);
                        break;
                    case Skill.WV3:
                        SetPos(i, 7 / 8f - 1 / 8f, 220);
                        break;
                    case Skill.WV4:
                        SetPos(i, 7 / 8f - 1 / 16f, 300);
                        break;
                    case Skill.CB1:
                        SetPos(i, 9 / 8f, 75);
                        break;
                    case Skill.CB2:
                        SetPos(i, 9 / 8f - 1 / 32f, 150);
                        break;
                    case Skill.CB3:
                        SetPos(i, 9 / 8f + 1 / 16f, 210);
                        break;
                    case Skill.CB4:
                        SetPos(i, 9 / 8f - 1 / 32f, 235);
                        break;
                    case Skill.CB5:
                        SetPos(i, 9 / 8f + 1 / 16f, 300);
                        break;
                    case Skill.CB6:
                        SetPos(i, 9 / 8f + 1 / 8f, 350);
                        break;
                    case Skill.AB1:
                        SetPos(i, 11 / 8f, 75);
                        break;
                    case Skill.AB2:
                        SetPos(i, 11 / 8f, 200);
                        break;
                    case Skill.AB3:
                        SetPos(i, 11 / 8f + 1 / 12f, 220);
                        break;
                    case Skill.AB4:
                        SetPos(i, 11 / 8f + 1 / 24f, 160);
                        break;
                    case Skill.AB5:
                        SetPos(i, 11 / 8f - 1 / 12f, 160);
                        break;
                    case Skill.AB6:
                        SetPos(i, 11 / 8f - 1 / 16f, 220);
                        break;
                    case Skill.AB7:
                        SetPos(i, 11 / 8f + 1 / 32f, 270);
                        break;
                    case Skill.ST1:
                        SetPos(i, 13 / 8f, 75);
                        break;
                    case Skill.ST2:
                        SetPos(i, 13 / 8f + 1 / 16f, 150);
                        break;
                    case Skill.ST3:
                        SetPos(i, 13 / 8f - 1 / 32f, 200);
                        break;
                    case Skill.ST4:
                        SetPos(i, 13 / 8f + 1 / 32f, 250);
                        break;
                    case Skill.ST5:
                        SetPos(i, 13 / 8f + 1 / 8f, 270);
                        break;
                    case Skill.ST6:
                        SetPos(i, 13 / 8f + 1 / 8f, 350);
                        break;
                    case Skill.AS1:
                        SetPos(i, 15 / 8f, 75);
                        break;
                    case Skill.AS2:
                        SetPos(i, 15 / 8f + 1 / 16f, 150);
                        break;
                    case Skill.AS3:
                        SetPos(i, 15 / 8f - 1 / 36f, 225);
                        break;
                    case Skill.AS4:
                        SetPos(i, 15 / 8f + 1 / 9f, 270);
                        break;
                }
                bg.Append(skill[i]);
            }
        }
        internal static bool Lighting = false;
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            string text = Lighting ? "已点亮" : MaterialBox.Enough ? "点亮" : "材料不足";
            button.SetText(text);
            button.Width.Pixels = ChatManager.GetStringSize(FontAssets.MouseText.Value, text, Vector2.One).X + 20;
            var rec = bg.GetDimensions().Position();
            side.Left.Pixels = rec.X + bg.Width.Pixels + 10;
            side.Top.Pixels = rec.Y + bg.Height.Pixels / 2 - side.Height.Pixels / 2 + 30;
        }
        public void SetPos(int id, float manyPI, float dis)
        {
            skill[id].Left.Pixels = Sin(ManyPI(manyPI)) * dis;
            skill[id].Top.Pixels = -Cos(ManyPI(manyPI)) * dis;
        }
        public void ModifyView(int id)
        {
            ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod);
            SoundEngine.PlaySound(SoundID.Item4);
            Lighting = Main.LocalPlayer.GetModPlayer<MKPlayer>().stars[id];
            view.id = id;
            List<Item> list = new();
            switch ((Skill)id)
            {
                case Skill.RB1:
                    list.Add(new Item(ItemID.LifeCrystal, 10));
                    list.Add(new Item(CalamityMod.Find<ModItem>("PlantyMush").Type, 20));
                    list.Add(new Item(CalamityMod.Find<ModItem>("CoralskinFoolfish").Type, 1));
                    list.Add(new Item(CalamityMod.Find<ModItem>("EutrophicSand").Type, 20));
                    break;
                case Skill.RB2:
                    list.Add(new Item(ItemID.BeeWax, 10));
                    list.Add(new Item(CalamityMod.Find<ModItem>("PurifiedGel").Type, 10));
                    break;
                case Skill.RB3:
                    list.Add(new Item(ItemID.TurtleShell, 10));
                    list.Add(new Item(ItemID.AncientCloth, 3));
                    list.Add(new Item(ItemID.SoulofFlight, 10));
                    list.Add(new Item(ItemID.ButterflyDust, 3));
                    list.Add(new Item(CalamityMod.Find<ModItem>("TitanHeart").Type, 3));
                    list.Add(new Item(CalamityMod.Find<ModItem>("TrapperBulb").Type, 3));
                    list.Add(new Item(CalamityMod.Find<ModItem>("BeetleJuice").Type, 10));
                    break;
                case Skill.RB4:
                    list.Add(new Item(ItemID.SoulofSight, 10));
                    list.Add(new Item(ItemID.LifeFruit, 10));
                    list.Add(new Item(CalamityMod.Find<ModItem>("LivingShard").Type, 10));
                    break;
                case Skill.RB5:
                    list.Add(new Item(ItemID.BeetleHusk, 10));
                    list.Add(new Item(CalamityMod.Find<ModItem>("BarofLife").Type, 10));
                    list.Add(new Item(CalamityMod.Find<ModItem>("ExodiumCluster").Type, 20));
                    break;
                case Skill.RB6:
                    list.Add(new Item(CalamityMod.Find<ModItem>("ArmoredShell").Type, 20));
                    break;
                case Skill.RB7:
                    list.Add(new Item(CalamityMod.Find<ModItem>("AuricBar").Type, 10));
                    break;
                case Skill.SK1:
                    list.Add(new Item(CalamityMod.Find<ModItem>("SunkenSailfish").Type, 1));
                    list.Add(new Item(CalamityMod.Find<ModItem>("VictideBar").Type, 10));
                    list.Add(new Item(CalamityMod.Find<ModItem>("Navystone").Type, 20));
                    break;
                case Skill.SK2:
                    //list.Add(new Item(CalamityMod.Find<ModItem>("ScarredAngelfish").Type, 1));
                    break;
                case Skill.SK3:
                    list.Add(new Item(CalamityMod.Find<ModItem>("EndothermicEnergy").Type, 10));
                    break;
                case Skill.CS1:
                    list.Add(new Item(ItemID.LargeRuby, 1));
                    list.Add(new Item(ItemID.LargeAmber, 1));
                    list.Add(new Item(ItemID.LargeTopaz, 1));
                    list.Add(new Item(ItemID.LargeEmerald, 1));
                    list.Add(new Item(ItemID.LargeSapphire, 1));
                    list.Add(new Item(ItemID.LargeAmethyst, 1));
                    list.Add(new Item(ItemID.LargeDiamond, 1));
                    list.Add(new Item(CalamityMod.Find<ModItem>("PrismaticGuppy").Type, 1));
                    list.Add(new Item(CalamityMod.Find<ModItem>("SeaPrism").Type, 10));
                    list.Add(new Item(CalamityMod.Find<ModItem>("PrismShard").Type, 10));
                    break;
                case Skill.CS2:
                    list.Add(new Item(ItemID.AncientBattleArmorMaterial, 3));
                    list.Add(new Item(ItemID.CrystalShard, 20));
                    list.Add(new Item(CalamityMod.Find<ModItem>("EssenceofEleum").Type, 10));
                    list.Add(new Item(CalamityMod.Find<ModItem>("VerstaltiteBar").Type, 10));
                    break;
                case Skill.CS3:
                    list.Add(new Item(CalamityMod.Find<ModItem>("Lumenite").Type, 10));
                    list.Add(new Item(CalamityMod.Find<ModItem>("CoreofEleum").Type, 10));
                    break;
                case Skill.CS4:
                    list.Add(new Item(CalamityMod.Find<ModItem>("DivineGeode").Type, 10));
                    break;
                case Skill.CS5:
                    list.Add(new Item(CalamityMod.Find<ModItem>("ExoPrism").Type, 10));
                    break;
                case Skill.WV1:
                    list.Add(new Item(ItemID.WormTooth, 10));
                    list.Add(new Item(ItemID.JungleSpores, 10));
                    list.Add(new Item(ItemID.Stinger, 10));
                    list.Add(new Item(CalamityMod.Find<ModItem>("EbonianGel").Type, 10));
                    list.Add(new Item(CalamityMod.Find<ModItem>("SulphurousSand").Type, 20));
                    list.Add(new Item(CalamityMod.Find<ModItem>("SulphurousSandstone").Type, 20));
                    list.Add(new Item(CalamityMod.Find<ModItem>("HardenedSulphurousSandstone").Type, 20));
                    list.Add(new Item(CalamityMod.Find<ModItem>("SulfuricScale").Type, 10));
                    break;
                case Skill.WV2:
                    list.Add(new Item(CalamityMod.Find<ModItem>("TrueShadowScale").Type, 10));
                    break;
                case Skill.WV3:
                    list.Add(new Item(ItemID.SpiderFang, 10));
                    list.Add(new Item(ItemID.Ichor, 10));
                    list.Add(new Item(CalamityMod.Find<ModItem>("MolluskHusk").Type, 10));
                    list.Add(new Item(CalamityMod.Find<ModItem>("BlightedGel").Type, 10));
                    list.Add(new Item(CalamityMod.Find<ModItem>("CorrodedFossil").Type, 10));
                    break;
                case Skill.WV4:
                    list.Add(new Item(CalamityMod.Find<ModItem>("PlagueCellCluster").Type, 10));
                    list.Add(new Item(CalamityMod.Find<ModItem>("InfectedArmorPlating").Type, 10));
                    break;
                case Skill.CB1:
                    list.Add(new Item(ItemID.MeteoriteBar, 10));
                    list.Add(new Item(ItemID.HellstoneBar, 10));
                    list.Add(new Item(CalamityMod.Find<ModItem>("BrimstoneFish").Type, 1));
                    list.Add(new Item(CalamityMod.Find<ModItem>("CragBullhead").Type, 1));
                    list.Add(new Item(CalamityMod.Find<ModItem>("BrimstoneSlag").Type, 20));
                    break;
                case Skill.CB2:
                    list.Add(new Item(ItemID.CursedFlame, 10));
                    break;
                case Skill.CB3:
                    list.Add(new Item(ItemID.LunarTabletFragment, 10));
                    list.Add(new Item(CalamityMod.Find<ModItem>("UnholyCore").Type, 10));
                    break;
                case Skill.CB4:
                    list.Add(new Item(CalamityMod.Find<ModItem>("CruptixBar").Type, 10));
                    list.Add(new Item(CalamityMod.Find<ModItem>("UnholyEssence").Type, 10));
                    break;
                case Skill.CB5:
                    list.Add(new Item(CalamityMod.Find<ModItem>("HellcasterFragment").Type, 10));
                    break;
                case Skill.CB6:
                    list.Add(new Item(CalamityMod.Find<ModItem>("CalamitousEssence").Type, 10));
                    break;
                case Skill.AB1:
                    list.Add(new Item(ItemID.DemoniteBar, 10));
                    list.Add(new Item(ItemID.ShadowScale, 10));
                    list.Add(new Item(CalamityMod.Find<ModItem>("CoastalDemonfish").Type, 1));
                    list.Add(new Item(CalamityMod.Find<ModItem>("Shadowfish").Type, 1));
                    list.Add(new Item(CalamityMod.Find<ModItem>("AbyssGravel").Type, 20));
                    break;
                case Skill.AB2:
                    list.Add(new Item(ItemID.SoulofNight, 10));
                    list.Add(new Item(CalamityMod.Find<ModItem>("EssenceofChaos").Type, 10));
                    break;
                case Skill.AB3:
                    list.Add(new Item(ItemID.SoulofFright, 10));
                    list.Add(new Item(ItemID.Ectoplasm, 10));
                    list.Add(new Item(ItemID.SpookyTwig, 10));
                    list.Add(new Item(CalamityMod.Find<ModItem>("Voidstone").Type, 20));
                    list.Add(new Item(CalamityMod.Find<ModItem>("Tenebris").Type, 20));
                    list.Add(new Item(CalamityMod.Find<ModItem>("CalamityDust").Type, 10));
                    list.Add(new Item(CalamityMod.Find<ModItem>("DepthCells").Type, 10));
                    list.Add(new Item(CalamityMod.Find<ModItem>("CoreofChaos").Type, 10));
                    break;
                case Skill.AB4:
                    list.Add(new Item(CalamityMod.Find<ModItem>("Phantoplasm").Type, 10));
                    break;
                case Skill.AB5:
                    list.Add(new Item(CalamityMod.Find<ModItem>("DarkPlasma").Type, 1));
                    break;
                case Skill.AB6:
                    list.Add(new Item(CalamityMod.Find<ModItem>("NightmareFuel").Type, 10));
                    list.Add(new Item(CalamityMod.Find<ModItem>("DarksunFragment").Type, 10));
                    break;
                case Skill.AB7:
                    list.Add(new Item(CalamityMod.Find<ModItem>("ShadowspecBar").Type, 10));
                    break;
                case Skill.ST1:
                    list.Add(new Item(ItemID.Bone, 10));
                    list.Add(new Item(CalamityMod.Find<ModItem>("AncientBoneDust").Type, 10));
                    list.Add(new Item(CalamityMod.Find<ModItem>("DemonicBoneAsh").Type, 10));
                    break;
                case Skill.ST2:
                    list.Add(new Item(ItemID.SoulofLight, 10));
                    list.Add(new Item(CalamityMod.Find<ModItem>("EssenceofSunlight").Type, 10));
                    break;
                case Skill.ST3:
                    list.Add(new Item(ItemID.SoulofMight, 10));
                    list.Add(new Item(ItemID.BlackFairyDust, 3));
                    list.Add(new Item(ItemID.BrokenHeroSword, 3));
                    list.Add(new Item(CalamityMod.Find<ModItem>("SolarVeil").Type, 10));
                    list.Add(new Item(CalamityMod.Find<ModItem>("GrandScale").Type, 3));
                    list.Add(new Item(CalamityMod.Find<ModItem>("CoreofSunlight").Type, 10));
                    break;
                case Skill.ST4:
                    list.Add(new Item(CalamityMod.Find<ModItem>("MeldiateBar").Type, 10));
                    list.Add(new Item(CalamityMod.Find<ModItem>("EffulgentFeather").Type, 10));
                    break;
                case Skill.ST5:
                    list.Add(new Item(CalamityMod.Find<ModItem>("TwistingNether").Type, 1));
                    list.Add(new Item(CalamityMod.Find<ModItem>("RuinousSoul").Type, 10));
                    list.Add(new Item(CalamityMod.Find<ModItem>("ReaperTooth").Type, 3));
                    break;
                case Skill.ST6:
                    list.Add(new Item(CalamityMod.Find<ModItem>("AscendantSpiritEssence").Type, 10));
                    break;
                case Skill.AS1:
                    list.Add(new Item(CalamityMod.Find<ModItem>("Stardust").Type, 10));
                    list.Add(new Item(CalamityMod.Find<ModItem>("AldebaranAlewife").Type, 1));
                    list.Add(new Item(CalamityMod.Find<ModItem>("AstralFossil").Type, 20));
                    list.Add(new Item(CalamityMod.Find<ModItem>("AstralSilt").Type, 20));
                    break;
                case Skill.AS2:
                    list.Add(new Item(CalamityMod.Find<ModItem>("AstralJelly").Type, 10));
                    break;
                case Skill.AS3:
                    list.Add(new Item(ItemID.LunarBar, 10));
                    list.Add(new Item(CalamityMod.Find<ModItem>("GalacticaSingularity").Type, 10));
                    list.Add(new Item(CalamityMod.Find<ModItem>("AstralBar").Type, 10));
                    break;
                case Skill.AS4:
                    list.Add(new Item(CalamityMod.Find<ModItem>("CosmiliteBar").Type, 10));
                    break;
            };
            view.SetList(list);
        }
    }
}
