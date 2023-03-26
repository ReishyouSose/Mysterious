namespace MysteriousKnives.Items
{
    public abstract class QM : ModItem
    {
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 26;
			Item.maxStack = 9999;
			base.SetDefaults();
		}
        public abstract class 诡毒之水 : QM
		{
			public override string Texture => "MysteriousKnives/Pictures/Items/诡毒";
			public override void SetStaticDefaults()
			{
				// Tooltip.SetDefault("众多毒性物质提取的精华浓缩在这一小瓶水中");
				base.SetStaticDefaults();
			}
            public override void SetDefaults()
            {
				Item.rare = ModContent.RarityType<WVRare>();
                base.SetDefaults();
            }
            public class 诡毒01 : 诡毒之水
			{
				public override void SetStaticDefaults()
				{
					// DisplayName.SetDefault("诡毒之水I");
					base.SetStaticDefaults();
				}
				public override void AddRecipes()
				{
					if (ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod))
					{
						Recipe recipe = CreateRecipe();
						recipe.AddIngredient(ItemID.WormTooth, 10);
						recipe.AddIngredient(ItemID.JungleSpores, 10);
						recipe.AddIngredient(ItemID.Stinger, 10);
						recipe.AddIngredient(ItemID.BottledWater, 10);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("EbonianGel"), 10);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("SulphurousSand"), 20);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("SulphurousSandstone"), 20);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("HardenedSulphurousSandstone"), 20);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("SulfuricScale"), 10);
						recipe.AddTile(TileID.Bottles);
						recipe.ReplaceResult(this, 1);
						recipe.Register();
					}
				}
			}

			public class 诡毒02 : 诡毒之水
			{
				public override void SetStaticDefaults()
				{
					// DisplayName.SetDefault("诡毒之水II");
					base.SetStaticDefaults();
				}
				public override void SetDefaults()
				{
					base.SetDefaults();
				}
				public override void AddRecipes()
				{
					if (ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod))
					{
						Recipe recipe = CreateRecipe();
						recipe.AddIngredient(ItemID.BottledWater, 10);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("TrueShadowScale"), 10);
						recipe.AddTile(TileID.AlchemyTable);
						recipe.ReplaceResult(this, 1);
						recipe.Register();
					}
				}
			}

			public class 诡毒03 : 诡毒之水
			{
				public override void SetStaticDefaults()
				{
					// DisplayName.SetDefault("诡毒之水III");
					base.SetStaticDefaults();
				}
				public override void SetDefaults()
				{
					base.SetDefaults();
				}
				public override void AddRecipes()
				{
					if (ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod))
					{
						Recipe recipe = CreateRecipe();
						recipe.AddIngredient(ItemID.BottledWater, 10);
						recipe.AddIngredient(ItemID.SpiderFang, 10);
						recipe.AddIngredient(ItemID.Ichor, 10);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("MolluskHusk"), 10);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("BlightedGel"), 10);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("CorrodedFossil"), 10);
						recipe.AddTile(TileID.AlchemyTable);
						recipe.ReplaceResult(this, 1);
						recipe.Register();
					}
				}
			}
			public class 诡毒04 : 诡毒之水
			{
				public override void SetStaticDefaults()
				{
					// DisplayName.SetDefault("诡毒之水IV");
					base.SetStaticDefaults();
				}
				public override void SetDefaults()
				{
					base.SetDefaults();
				}
				public override void AddRecipes()
				{
					if (ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod))
					{
						Recipe recipe = CreateRecipe();
						recipe.AddIngredient(ItemID.BottledWater, 10);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("PlagueCellCluster"), 10);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("InfectedArmorPlating"), 10);
						recipe.AddTile(TileID.AlchemyTable);
						recipe.ReplaceResult(this, 1);
						recipe.Register();
					}
				}
			}
		}

		public abstract class 结晶之水 : QM
		{
			public override string Texture => "MysteriousKnives/Pictures/Items/结晶";
			public override void SetStaticDefaults()
			{
				// Tooltip.SetDefault("晶体化后可是很脆的~");
				base.SetStaticDefaults();
			}
			public override void SetDefaults()
			{
				ModContent.RarityType<CSRare>();
				base.SetDefaults();
			}
			public class 结晶01 : 结晶之水
			{
				public override void SetStaticDefaults()
				{
					// DisplayName.SetDefault("结晶之水I");
					base.SetStaticDefaults();
				}
				public override void AddRecipes()
				{
					if (ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod))
					{
						Recipe recipe = CreateRecipe();
						recipe.AddIngredient(ItemID.LargeRuby, 1);
						recipe.AddIngredient(ItemID.LargeAmber, 1);
						recipe.AddIngredient(ItemID.LargeTopaz, 1);
						recipe.AddIngredient(ItemID.LargeEmerald, 1);
						recipe.AddIngredient(ItemID.LargeSapphire, 1);
						recipe.AddIngredient(ItemID.LargeAmethyst, 1);
						recipe.AddIngredient(ItemID.LargeDiamond, 1);
						recipe.AddIngredient(ItemID.BottledWater, 10);
						recipe.AddIngredient(ItemID.LavaBucket, 10);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("PrismaticGuppy"), 1);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("SeaPrism"), 10);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("PrismShard"), 10);
						recipe.AddTile(TileID.Furnaces);
						recipe.ReplaceResult(this, 1);
						recipe.Register();
					}
				}
			}

			public class 结晶02 : 结晶之水
			{
				public override void SetStaticDefaults()
				{
					// DisplayName.SetDefault("结晶之水II");
					base.SetStaticDefaults();
				}
				public override void AddRecipes()
				{
					if (ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod))
					{
						Recipe recipe = CreateRecipe();
						recipe.AddIngredient(ItemID.AncientBattleArmorMaterial, 3);
						recipe.AddIngredient(ItemID.BottledWater, 10);
						recipe.AddIngredient(ItemID.LavaBucket, 10);
						recipe.AddIngredient(ItemID.CrystalShard, 20);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("EssenceofEleum"), 10);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("VerstaltiteBar"), 10);
						recipe.AddTile(TileID.Hellforge);
						recipe.ReplaceResult(this, 1);
						recipe.Register();
					}
				}
			}

			public class 结晶03 : 结晶之水
			{
				public override void SetStaticDefaults()
				{
					// DisplayName.SetDefault("结晶之水III");
					base.SetStaticDefaults();
				}
				public override void AddRecipes()
				{
					if (ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod))
					{
						Recipe recipe = CreateRecipe();
						recipe.AddIngredient(ItemID.BottledWater, 10);
						recipe.AddIngredient(ItemID.LavaBucket, 10);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("Lumenite"), 10);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("CoreofEleum"), 10);
						recipe.AddTile(TileID.AdamantiteForge);
						recipe.ReplaceResult(this, 1);
						recipe.Register();
					}
				}
			}

			public class 结晶04 : 结晶之水
			{
				public override void SetStaticDefaults()
				{
					// DisplayName.SetDefault("结晶之水IV");
					base.SetStaticDefaults();
				}
				public override void AddRecipes()
				{
					if (ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod))
					{
						Recipe recipe = CreateRecipe();
						recipe.AddIngredient(ItemID.BottledWater, 10);
						recipe.AddIngredient(ItemID.LavaBucket, 10);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("DivineGeode"), 10);
						recipe.AddTile(TileID.AdamantiteForge);
						recipe.ReplaceResult(this, 1);
						recipe.Register();
					}
				}
			}

			public class 结晶05 : 结晶之水
			{
				public override void SetStaticDefaults()
				{
					// DisplayName.SetDefault("结晶之水V");
					base.SetStaticDefaults();
				}
				public override void AddRecipes()
				{
					if (ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod))
					{
						Recipe recipe = CreateRecipe();
						recipe.AddIngredient(ItemID.BottledWater, 10);
						recipe.AddIngredient(ItemID.LavaBucket, 10);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("ExoPrism"), 10);
						recipe.AddTile(TileID.AdamantiteForge);
						recipe.ReplaceResult(this, 1);
						recipe.Register();
					}
				}
			}

		}

		//S沉沦
		public abstract class 沉沦之水 : QM
		{
			public override string Texture => "MysteriousKnives/Pictures/Items/沉沦";
			public override void SetStaticDefaults()
			{
				/* Tooltip.SetDefault("周围的物质都会逐渐沉静\n" +
					"喝下以查看淬炼后的效果（危）\n" +
					"不消耗"); */
				base.SetStaticDefaults();
			}
			public override void SetDefaults()
			{
				Item.rare = ModContent.RarityType<SKRare>();
				base.SetDefaults();
			}
			public class 沉沦01 : 沉沦之水
			{
				public override void SetStaticDefaults()
				{
					// DisplayName.SetDefault("沉沦之水I");
					base.SetStaticDefaults();
				}
				public override void AddRecipes()
				{
					if (ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod))
					{
						Recipe recipe = CreateRecipe();
						recipe.AddIngredient(ItemID.BottledWater, 10);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("SunkenSailfish"), 1);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("VictideBar"), 10);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("Navystone"), 20);
						recipe.AddTile(TileID.Bottles);
						recipe.ReplaceResult(this, 1);
						recipe.Register();
					}
				}
			}

			public class 沉沦02 : 沉沦之水
			{
				public override void SetStaticDefaults()
				{
					// DisplayName.SetDefault("沉沦之水II");
					base.SetStaticDefaults();
				}
				public override void AddRecipes()
				{
					if (ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod))
					{
						Recipe recipe = CreateRecipe();
						recipe.AddIngredient(ItemID.BottledWater, 10);
						//recipe.AddIngredient(CalamityMod.Find<ModItem>("ScarredAngelfish"), 1);
						recipe.AddTile(TileID.Bottles);
						recipe.ReplaceResult(this, 1);
						recipe.Register();
					}
				}
			}

			public class 沉沦03 : 沉沦之水
			{
                public override void SetStaticDefaults()
				{
					// DisplayName.SetDefault("沉沦之水III");
					base.SetStaticDefaults();
				}
				public override void AddRecipes()
				{
					if (ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod))
					{
						Recipe recipe = CreateRecipe();
						recipe.AddIngredient(ItemID.BottledWater, 10);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("EndothermicEnergy"), 10);
						recipe.AddTile(TileID.Bottles);
						recipe.ReplaceResult(this, 1);
						recipe.Register();
					}
				}
			}

		}

		//L生命
		public abstract class 生命之水 : QM
		{
			public override string Texture => "MysteriousKnives/Pictures/Items/生命";
			public override void SetStaticDefaults()
			{
				// Tooltip.SetDefault("充盈的生命能量！");
				base.SetStaticDefaults();
			}
			public override void SetDefaults()
			{
				ModContent.RarityType<RBRare>();
				base.SetDefaults();
			}
			public class 生命01 : 生命之水
			{
				public override void SetStaticDefaults()
				{
					// DisplayName.SetDefault("生命之水I");
					base.SetStaticDefaults();
				}
				public override void AddRecipes()
				{
					if (ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod))
					{
						Recipe recipe = CreateRecipe();
						recipe.AddIngredient(ItemID.LifeCrystal, 10);
						recipe.AddIngredient(ItemID.BottledWater, 10);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("PlantyMush"), 20);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("CoralskinFoolfish"), 1);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("EutrophicSand"), 20);
						recipe.AddTile(TileID.Bottles);
						recipe.ReplaceResult(this, 1);
						recipe.Register();
					}
				}
			}

			public class 生命02 : 生命之水
			{
				public override void SetStaticDefaults()
				{
					// DisplayName.SetDefault("生命之水II");
					base.SetStaticDefaults();
				}
				public override void AddRecipes()
				{
					if (ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod))
					{
						Recipe recipe = CreateRecipe();
						recipe.AddIngredient(ItemID.BottledWater, 10);
						recipe.AddIngredient(ItemID.BeeWax, 10);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("PurifiedGel"), 10);
						recipe.AddTile(TileID.AlchemyTable);
						recipe.ReplaceResult(this, 1);
						recipe.Register();
					}
				}
			}

			public class 生命03 : 生命之水
			{
				public override void SetStaticDefaults()
				{
					// DisplayName.SetDefault("生命之水III");
					base.SetStaticDefaults();
				}
				public override void AddRecipes()
				{
					if (ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod))
					{
						Recipe recipe = CreateRecipe();
						recipe.AddIngredient(ItemID.BottledWater, 10);
						recipe.AddIngredient(ItemID.TurtleShell, 10);
						recipe.AddIngredient(ItemID.AncientCloth, 3);
						recipe.AddIngredient(ItemID.SoulofFlight, 10);
						recipe.AddIngredient(ItemID.ButterflyDust, 3);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("TitanHeart"), 3);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("TrapperBulb"), 3);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("BeetleJuice"), 10);
						recipe.AddTile(TileID.AlchemyTable);
						recipe.ReplaceResult(this, 1);
						recipe.Register();
					}
				}
			}

			public class 生命04 : 生命之水
			{
				public override void SetStaticDefaults()
				{
					// DisplayName.SetDefault("生命之水IV");
					base.SetStaticDefaults();
				}
				public override void AddRecipes()
				{
					if (ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod))
					{
						Recipe recipe = CreateRecipe();
						recipe.AddIngredient(ItemID.BottledWater, 10);
						recipe.AddIngredient(ItemID.SoulofSight, 10);
						recipe.AddIngredient(ItemID.LifeFruit, 10);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("LivingShard"), 10);
						recipe.AddTile(TileID.AlchemyTable);
						recipe.ReplaceResult(this, 1);
						recipe.Register();
					}
				}
			}

			public class 生命05 : 生命之水
			{
				public override void SetStaticDefaults()
				{
					// DisplayName.SetDefault("生命之水V");
					base.SetStaticDefaults();
				}
				public override void AddRecipes()
				{
					if (ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod))
					{
						Recipe recipe = CreateRecipe();
						recipe.AddIngredient(ItemID.BottledWater, 10);
						recipe.AddIngredient(ItemID.BeetleHusk, 10);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("BarofLife"), 10);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("ExodiumCluster"), 20);
						recipe.AddTile(TileID.AlchemyTable);
						recipe.ReplaceResult(this, 1);
						recipe.Register();
					}
				}
			}

			public class 生命06 : 生命之水
			{
				public override void SetStaticDefaults()
				{
					// DisplayName.SetDefault("生命之水VI");
					base.SetStaticDefaults();
				}
				public override void AddRecipes()
				{
					if (ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod))
					{
						Recipe recipe = CreateRecipe();
						recipe.AddIngredient(ItemID.BottledWater, 10);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("ArmoredShell"), 20);
						recipe.AddTile(TileID.AlchemyTable);
						recipe.ReplaceResult(this, 1);
						recipe.Register();
					}
				}
			}

			public class 生命07 : 生命之水
			{
				public override void SetStaticDefaults()
				{
					// DisplayName.SetDefault("生命之水VII");
					base.SetStaticDefaults();
				}
				public override void AddRecipes()
				{
					if (ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod))
					{
						Recipe recipe = CreateRecipe();
						recipe.AddIngredient(ItemID.BottledWater, 10);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("AuricBar"), 10);
						recipe.AddTile(TileID.AlchemyTable);
						recipe.ReplaceResult(this, 1);
						recipe.Register();
					}
				}
			}

			public class 生命08 : 生命之水
			{
				public override void SetStaticDefaults()
				{
					// DisplayName.SetDefault("生命之水VIII");
					base.SetStaticDefaults();
				}
				public override void AddRecipes()
				{
					if (ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod))
					{
						Recipe recipe = CreateRecipe();
						recipe.AddIngredient(ItemID.BottledWater, 10);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("MiracleMatter"), 3); 
						recipe.AddTile(TileID.AlchemyTable);
						recipe.ReplaceResult(this, 1);
						recipe.Register();
					}
				}
			}
		}

		//P力量
		public abstract class 力量之水 : QM
		{
			public override string Texture => "MysteriousKnives/Pictures/Items/力量";
			public override void SetStaticDefaults()
			{
				// Tooltip.SetDefault("好！很有精神！");
				base.SetStaticDefaults();
			}
			public override void SetDefaults()
			{
				ModContent.RarityType<STRare>();
				base.SetDefaults();
			}
			public class 力量01 : 力量之水
			{
				public override void SetStaticDefaults()
				{
					// DisplayName.SetDefault("力量之水I");
					base.SetStaticDefaults();
				}
				public override void AddRecipes()
				{
					if (ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod))
					{
						Recipe recipe = CreateRecipe();
						recipe.AddIngredient(ItemID.BottledWater, 10);
						recipe.AddIngredient(ItemID.Bone, 10);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("AncientBoneDust"), 10);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("DemonicBoneAsh"), 10);
						recipe.AddTile(TileID.AlchemyTable);
						recipe.ReplaceResult(this, 1);
						recipe.Register();
					}
				}
			}

			public class 力量02 : 力量之水
			{
				public override void SetStaticDefaults()
				{
					// DisplayName.SetDefault("力量之水II");
					base.SetStaticDefaults();
				}
				public override void AddRecipes()
				{
					if (ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod))
					{
						Recipe recipe = CreateRecipe();
						recipe.AddIngredient(ItemID.BottledWater, 10);
						recipe.AddIngredient(ItemID.SoulofLight, 10);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("EssenceofSunlight"), 10);
						recipe.AddTile(TileID.AlchemyTable);
						recipe.ReplaceResult(this, 1);
						recipe.Register();
					}
				}
			}

			public class 力量03 : 力量之水
			{
				public override void SetStaticDefaults()
				{
					// DisplayName.SetDefault("力量之水III");
					base.SetStaticDefaults();
				}
				public override void AddRecipes()
				{
					if (ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod))
					{
						Recipe recipe = CreateRecipe();
						recipe.AddIngredient(ItemID.BottledWater, 10);
						recipe.AddIngredient(ItemID.SoulofMight, 10);
						recipe.AddIngredient(ItemID.BlackFairyDust, 3);
						recipe.AddIngredient(ItemID.BrokenHeroSword, 3);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("SolarVeil"), 10);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("GrandScale"), 3);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("CoreofSunlight"), 10);
						recipe.AddTile(TileID.AlchemyTable);
						recipe.ReplaceResult(this, 1);
						recipe.Register();
					}
				}
			}

			public class 力量04 : 力量之水
			{
				public override void SetStaticDefaults()
				{
					// DisplayName.SetDefault("力量之水IV");
					base.SetStaticDefaults();
				}
				public override void AddRecipes()
				{
					if (ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod))
					{
						Recipe recipe = CreateRecipe();
						recipe.AddIngredient(ItemID.BottledWater, 10);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("MeldiateBar"), 10);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("EffulgentFeather"), 10);
						recipe.AddTile(TileID.AlchemyTable);
						recipe.ReplaceResult(this, 1);
						recipe.Register();
					}
				}
			}

			public class 力量05 : 力量之水
			{
				public override void SetStaticDefaults()
				{
					// DisplayName.SetDefault("力量之水V");
					base.SetStaticDefaults();
				}
				public override void AddRecipes()
				{
					if (ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod))
					{
						Recipe recipe = CreateRecipe();
						recipe.AddIngredient(ItemID.BottledWater, 10);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("TwistingNether"), 1);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("RuinousSoul"), 10);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("ReaperTooth"), 3);
						recipe.AddTile(TileID.AlchemyTable);
						recipe.ReplaceResult(this, 1);
						recipe.Register();
					}
				}
			}

			public class 力量06 : 力量之水
			{
				public override void SetStaticDefaults()
				{
					// DisplayName.SetDefault("力量之水VI");
					base.SetStaticDefaults();
				}
				public override void AddRecipes()
				{
					if (ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod))
					{
						Recipe recipe = CreateRecipe();
						recipe.AddIngredient(ItemID.BottledWater, 10);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("AscendantSpiritEssence"), 10);
						recipe.AddTile(TileID.AlchemyTable);
						recipe.ReplaceResult(this, 1);
						recipe.Register();
					}
				}
			}
		}

		public abstract class 凝爆之水 : QM
		{
			public override string Texture => "MysteriousKnives/Pictures/Items/凝爆";
			public override void SetStaticDefaults()
			{
				// Tooltip.SetDefault("烫烫烫烫烫烫烫烫烫烫！");
				base.SetStaticDefaults();
			}
            public override void SetDefaults()
			{
				ModContent.RarityType<CBRare>();
				base.SetDefaults();
            }
            public class 凝爆01 : 凝爆之水
			{
				public override void SetStaticDefaults()
				{
					// DisplayName.SetDefault("凝爆之水I");
					base.SetStaticDefaults();
				}
				public override void AddRecipes()
				{
					if (ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod))
					{
						Recipe recipe = CreateRecipe();
						recipe.AddIngredient(ItemID.BottledWater, 10);
						recipe.AddIngredient(ItemID.MeteoriteBar, 10);
						recipe.AddIngredient(ItemID.HellstoneBar, 10);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("BrimstoneFish"), 1);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("CragBullhead"), 1);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("BrimstoneSlag"), 20);
						recipe.AddTile(TileID.Furnaces);
						recipe.ReplaceResult(this, 1);
						recipe.Register();
					}
				}
			}

			public class 凝爆02 : 凝爆之水
			{
				public override void SetStaticDefaults()
				{
					// DisplayName.SetDefault("凝爆之水II");
					base.SetStaticDefaults();
				}
				public override void AddRecipes()
				{
					if (ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod))
					{
						Recipe recipe = CreateRecipe();
						recipe.AddIngredient(ItemID.BottledWater, 10);
						recipe.AddIngredient(ItemID.CursedFlame, 10);
						recipe.AddTile(TileID.Hellforge);
						recipe.ReplaceResult(this, 1);
						recipe.Register();
					}
				}
			}

			public class 凝爆03 : 凝爆之水
			{
				public override void SetStaticDefaults()
				{
					// DisplayName.SetDefault("凝爆之水III");
					base.SetStaticDefaults();
				}
				public override void AddRecipes()
				{
					if (ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod))
					{
						Recipe recipe = CreateRecipe();
						recipe.AddIngredient(ItemID.BottledWater, 10);
						recipe.AddIngredient(ItemID.LunarTabletFragment, 10);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("UnholyCore"), 10);
						recipe.AddTile(TileID.AdamantiteForge);
						recipe.ReplaceResult(this, 1);
						recipe.Register();
					}
				}
			}

			public class 凝爆04 : 凝爆之水
			{
				public override void SetStaticDefaults()
				{
					// DisplayName.SetDefault("凝爆之水IV");
					base.SetStaticDefaults();
				}
				public override void AddRecipes()
				{
					if (ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod))
					{
						Recipe recipe = CreateRecipe();
						recipe.AddIngredient(ItemID.BottledWater, 10);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("CruptixBar"), 10);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("UnholyEssence"), 10);
						recipe.AddTile(TileID.AdamantiteForge);
						recipe.ReplaceResult(this, 1);
						recipe.Register();
					}
				}
			}

			public class 凝爆05 : 凝爆之水
			{
				public override void SetStaticDefaults()
				{
					// DisplayName.SetDefault("凝爆之水V");
					base.SetStaticDefaults();
				}
				public override void AddRecipes()
				{
					if (ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod))
					{
						Recipe recipe = CreateRecipe();
						recipe.AddIngredient(ItemID.BottledWater, 10);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("HellcasterFragment"), 10);
						recipe.AddTile(TileID.AdamantiteForge);
						recipe.ReplaceResult(this, 1);
						recipe.Register();
					}
				}
			}
			public class 凝爆06 : 凝爆之水
			{
				public override void SetStaticDefaults()
				{
					// DisplayName.SetDefault("凝爆之水VI");
					base.SetStaticDefaults();
				}
				public override void AddRecipes()
				{
					if (ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod))
					{
						Recipe recipe = CreateRecipe();
						recipe.AddIngredient(ItemID.BottledWater, 10);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("CalamitousEssence"), 10);
						recipe.AddTile(TileID.AdamantiteForge);
						recipe.ReplaceResult(this, 1);
						recipe.Register();
					}
				}
			}
		}

		public abstract class 深渊之水 : QM
		{
			public override string Texture => "MysteriousKnives/Pictures/Items/深渊";
			public override void SetStaticDefaults()
			{
				// Tooltip.SetDefault("▊▊▊▊在凝视你");
				base.SetStaticDefaults();
			}
			public override void SetDefaults()
			{
				ModContent.RarityType<ABRare>();
				base.SetDefaults();
			}
			public class 深渊01 : 深渊之水
			{
				public override void SetStaticDefaults()
				{
					// DisplayName.SetDefault("深渊之水I");
					base.SetStaticDefaults();
				}
				public override void AddRecipes()
				{
					if (ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod))
					{
						Recipe recipe = CreateRecipe();
						recipe.AddIngredient(ItemID.BottledWater, 10);
						recipe.AddIngredient(ItemID.DemoniteBar, 10);
						recipe.AddIngredient(ItemID.ShadowScale, 10);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("CoastalDemonfish"), 1);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("Shadowfish"), 1);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("AbyssGravel"), 20);
						recipe.AddTile(TileID.Hellforge);
						recipe.ReplaceResult(this, 1);
						recipe.Register();
					}
				}
			}

			public class 深渊02 : 深渊之水
			{
				public override void SetStaticDefaults()
				{
					// DisplayName.SetDefault("深渊之水II");
					base.SetStaticDefaults();
				}
				public override void AddRecipes()
				{
					if (ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod))
					{
						Recipe recipe = CreateRecipe();
						recipe.AddIngredient(ItemID.BottledWater, 10);
						recipe.AddIngredient(ItemID.SoulofNight, 10);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("EssenceofChaos"), 10);
						recipe.AddTile(TileID.Hellforge);
						recipe.ReplaceResult(this, 1);
						recipe.Register();
					}
				}
			}

			public class 深渊03 : 深渊之水
			{
				public override void SetStaticDefaults()
				{
					// DisplayName.SetDefault("深渊之水III");
					base.SetStaticDefaults();
				}
				public override void AddRecipes()
				{
					if (ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod))
					{
						Recipe recipe = CreateRecipe();
						recipe.AddIngredient(ItemID.BottledWater, 10);
						recipe.AddIngredient(ItemID.SoulofFright, 10);
						recipe.AddIngredient(ItemID.Ectoplasm, 10);
						recipe.AddIngredient(ItemID.SpookyTwig, 10);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("Voidstone"), 20);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("Tenebris"), 20);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("CalamityDust"), 10);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("DepthCells"), 10);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("CoreofChaos"), 10);
						recipe.AddTile(TileID.AdamantiteForge);
						recipe.ReplaceResult(this, 1);
						recipe.Register();
					}
				}
			}

			public class 深渊04 : 深渊之水
			{
				public override void SetStaticDefaults()
				{
					// DisplayName.SetDefault("深渊之水IV");
					base.SetStaticDefaults();
				}
				public override void AddRecipes()
				{
					if (ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod))
					{
						Recipe recipe = CreateRecipe();
						recipe.AddIngredient(ItemID.BottledWater, 10);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("Phantoplasm"), 10);
						recipe.AddTile(TileID.AdamantiteForge);
						recipe.ReplaceResult(this, 1);
						recipe.Register();
					}
				}
			}

			public class 深渊05 : 深渊之水
			{
				public override void SetStaticDefaults()
				{
					// DisplayName.SetDefault("深渊之水V");
					base.SetStaticDefaults();
				}
				public override void AddRecipes()
				{
					if (ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod))
					{
						Recipe recipe = CreateRecipe();
						recipe.AddIngredient(ItemID.BottledWater, 10);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("DarkPlasma"), 1);
						recipe.AddTile(TileID.AdamantiteForge);
						recipe.ReplaceResult(this, 1);
						recipe.Register();
					}
				}
			}

			public class 深渊06 : 深渊之水
			{
				public override void SetStaticDefaults()
				{
					// DisplayName.SetDefault("深渊之水VI");
					base.SetStaticDefaults();
				}
				public override void AddRecipes()
				{
					if (ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod))
					{
						Recipe recipe = CreateRecipe();
						recipe.AddIngredient(ItemID.BottledWater, 10);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("NightmareFuel"), 10);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("DarksunFragment"), 10);
						recipe.AddTile(TileID.AdamantiteForge);
						recipe.ReplaceResult(this, 1);
						recipe.Register();
					}
				}
			}

			public class 深渊07 : 深渊之水
			{
				public override void SetStaticDefaults()
				{
					// DisplayName.SetDefault("深渊之水VII");
					base.SetStaticDefaults();
				}
				public override void AddRecipes()
				{
					if (ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod))
					{
						Recipe recipe = CreateRecipe();
						recipe.AddIngredient(ItemID.BottledWater, 10);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("ShadowspecBar"), 10);
						recipe.AddTile(TileID.AdamantiteForge);
						recipe.ReplaceResult(this, 1);
						recipe.Register();
					}
				}
			}
		}

		public abstract class 星辉之水 : QM
		{
			public override string Texture => "MysteriousKnives/Pictures/Items/星辉";
			public override void SetStaticDefaults()
			{
				// Tooltip.SetDefault("星辉宙域的光辉洒落于此");
				base.SetStaticDefaults();
			}
			public override void SetDefaults()
			{
				ModContent.RarityType<ASRare>();
				base.SetDefaults();
			}
			public class 星辉01 : 星辉之水
			{
				public override void SetStaticDefaults()
				{
					// DisplayName.SetDefault("星辉之水I");
					base.SetStaticDefaults();
				}
				public override void AddRecipes()
				{
					if (ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod))
					{
						Recipe recipe = CreateRecipe();
						recipe.AddIngredient(ItemID.BottledWater, 10);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("Stardust"), 10);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("AldebaranAlewife"), 1);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("AstralFossil"), 20);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("AstralSilt"), 20);
						recipe.AddTile(TileID.Hellforge);
						recipe.ReplaceResult(this, 1);
						recipe.Register();
					}
				}
			}

			public class 星辉02 : 星辉之水
			{
				public override void SetStaticDefaults()
				{
					// DisplayName.SetDefault("星辉之水II");
					base.SetStaticDefaults();
				}
				public override void AddRecipes()
				{
					if (ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod))
					{
						Recipe recipe = CreateRecipe();
						recipe.AddIngredient(ItemID.BottledWater, 10);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("AstralJelly"), 10);
						recipe.AddTile(TileID.AdamantiteForge);
						recipe.ReplaceResult(this, 1);
						recipe.Register();
					}
				}
			}

			public class 星辉03 : 星辉之水
			{
				public override void SetStaticDefaults()
				{
					// DisplayName.SetDefault("星辉之水III");
					base.SetStaticDefaults();
				}
				public override void AddRecipes()
				{
					if (ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod))
					{
						Recipe recipe = CreateRecipe();
						recipe.AddIngredient(ItemID.BottledWater, 10);
						recipe.AddIngredient(ItemID.LunarBar, 10);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("GalacticaSingularity"), 10);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("AstralBar"), 10);
						recipe.AddTile(TileID.AdamantiteForge);
						recipe.ReplaceResult(this, 1);
						recipe.Register();
					}
				}
			}

			public class 星辉04 : 星辉之水
			{
				public override void SetStaticDefaults()
				{
					// DisplayName.SetDefault("星辉之水IV");
					base.SetStaticDefaults();
				}
				public override void AddRecipes()
				{
					if (ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod))
					{
						Recipe recipe = CreateRecipe();
						recipe.AddIngredient(ItemID.BottledWater, 10);
						recipe.AddIngredient(CalamityMod.Find<ModItem>("CosmiliteBar"), 10);
						recipe.AddTile(TileID.AdamantiteForge);
						recipe.ReplaceResult(this, 1);
						recipe.Register();
					}
				}
			}
		}
	}
}