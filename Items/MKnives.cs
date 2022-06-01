using MysteriousKnives.Projectiles;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using static MysteriousKnives.Items.QM.诡毒之水;
using static MysteriousKnives.Items.QM.结晶之水;
using static MysteriousKnives.Items.QM.沉沦之水;
using static MysteriousKnives.Items.QM.生命之水;
using static MysteriousKnives.Items.QM.凝爆之水;
using static MysteriousKnives.Items.QM.深渊之水;
using static MysteriousKnives.Items.QM.力量之水;
using static MysteriousKnives.Items.QM.星辉之水;
using static MysteriousKnives.Buffs.MysteriousBuffs;
using static MysteriousKnives.Projectiles.MysteriousKnife;
using Terraria.DataStructures;
using Terraria.Utilities;
using static MysteriousKnives.Items.MKprefix;

namespace MysteriousKnives.Items
{
	public abstract class MKnives : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.autoReuse = true;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.DamageType = DamageClass.Melee;
			Item.UseSound = SoundID.Item1;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.shoot = ModContent.ProjectileType<MysteriousCore>();
			Item.shootSpeed = 10f;
			Item.UseSound = SoundID.Item113;
			base.SetDefaults();
		}
        public class MK01 : MKnives
		{
			public override string Texture => "MysteriousKnives/Pictures/Items/MK01";
			public override void SetStaticDefaults()
			{
				DisplayName.SetDefault("诡秘飞刀I");
				Tooltip.SetDefault("射出一团不明物质，随后炸出数把诡秘飞刀\n" +
					"散射1+2");
				CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			}

			public override void SetDefaults()
			{
				Item.damage = 30;
				Item.crit = 10;
				Item.knockBack = 6;
				Item.value = Item.sellPrice(0, 1, 0, 0);
				Item.rare = ModContent.RarityType<Rare_Gray>();
				base.SetDefaults();
			}
			public override void AddRecipes()
			{
				Recipe recipe = CreateRecipe();
				recipe.AddIngredient(ModContent.ItemType<诡毒01>(), 1);
				recipe.AddIngredient(ModContent.ItemType<结晶01>(), 1);
				recipe.AddIngredient(ModContent.ItemType<沉沦01>(), 1);
				recipe.AddIngredient(ModContent.ItemType<生命01>(), 1);
				if (ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod))
				{
					recipe.AddIngredient(CalamityMod.Find<ModItem>("EnergyCore").Type, 3);
					recipe.AddIngredient(CalamityMod.Find<ModItem>("BloodOrb"), 100);
				}
				recipe.AddTile(TileID.Anvils);
				recipe.ReplaceResult(this, 1);
				recipe.Register();
			}
		}

		public class MK02 : MKnives
		{
			public override string Texture => "MysteriousKnives/Pictures/Items/MK02";
			public override void SetStaticDefaults()
			{
				DisplayName.SetDefault("诡秘飞刀II");
				Tooltip.SetDefault("射出一团不明物质，随后炸出数把诡秘飞刀\n" +
					"散射2+2");
				CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			}
			public override void SetDefaults()
			{
				Item.damage = 40;
				Item.crit = 20;
				Item.knockBack = 6;
				Item.value = Item.sellPrice(0, 10, 0, 0);
				Item.rare = ModContent.RarityType<Rare_White>();
				base.SetDefaults();
			}
            public override void AddRecipes()
			{
				Recipe recipe = CreateRecipe();
				recipe.AddIngredient(ItemID.TissueSample, 10);
				recipe.AddIngredient(ModContent.ItemType<诡毒02>(), 1);
				recipe.AddIngredient(ModContent.ItemType<生命02>(), 1);
				recipe.AddIngredient(ModContent.ItemType<力量01>(), 1);
				recipe.AddIngredient(ModContent.ItemType<凝爆01>(), 1);
				recipe.AddIngredient(ModContent.ItemType<深渊01>(), 1);
				recipe.AddIngredient(ModContent.ItemType<MK01>(), 1);
				recipe.AddTile(TileID.Anvils);
				recipe.ReplaceResult(this, 1);
				recipe.Register();
			}
		}

        public class MK03 : MKnives
		{
			public override string Texture => "MysteriousKnives/Pictures/Items/MK03";
			public override void SetStaticDefaults()
			{
				DisplayName.SetDefault("诡秘飞刀III");
				Tooltip.SetDefault("射出一团不明物质，随后炸出数把诡秘飞刀\n" +
					"散射3+3");
				CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			}
			public override void SetDefaults()
			{
				Item.damage = 50;
				Item.crit = 30;
				Item.knockBack = 6;
				Item.value = Item.sellPrice(0, 30, 0, 0);
				Item.rare = ModContent.RarityType<Rare_Green>();
				base.SetDefaults();
			}
			public override void AddRecipes()
			{
				Recipe recipe = CreateRecipe();
				recipe.AddIngredient(ModContent.ItemType<诡毒03>(), 1);
				recipe.AddIngredient(ModContent.ItemType<生命03>(), 1);
				recipe.AddIngredient(ModContent.ItemType<沉沦02>(), 1);
				recipe.AddIngredient(ModContent.ItemType<结晶02>(), 1);
				recipe.AddIngredient(ModContent.ItemType<力量02>(), 1);
				recipe.AddIngredient(ModContent.ItemType<凝爆02>(), 1);
				recipe.AddIngredient(ModContent.ItemType<深渊02>(), 1);
				recipe.AddIngredient(ModContent.ItemType<星辉01>(), 1);
				recipe.AddIngredient(ModContent.ItemType<MK02>(), 1);
				recipe.AddTile(TileID.Anvils);
				recipe.ReplaceResult(this, 1);
				recipe.Register();
			}
		}

		public class MK04 : MKnives
		{
			public override string Texture => "MysteriousKnives/Pictures/Items/MK04";
			public override void SetStaticDefaults()
			{
				DisplayName.SetDefault("诡秘飞刀IV");
				Tooltip.SetDefault("射出一团不明物质，随后炸出数把诡秘飞刀\n" +
					"散射4+3");
				CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			}
			public override void SetDefaults()
			{
				Item.damage = 75;
				Item.crit = 40;
				Item.knockBack = 6;
				Item.value = Item.sellPrice(0, 80, 0, 0);
				Item.rare = ModContent.RarityType<Rare_Blue>();
				base.SetDefaults();
			}
			public override void AddRecipes()
			{
				Recipe recipe = CreateRecipe();
				recipe.AddIngredient(ModContent.ItemType<生命04>(), 1);
				recipe.AddIngredient(ModContent.ItemType<结晶03>(), 1);
				recipe.AddIngredient(ModContent.ItemType<力量03>(), 1);
				recipe.AddIngredient(ModContent.ItemType<凝爆03>(), 1);
				recipe.AddIngredient(ModContent.ItemType<深渊03>(), 1);
				recipe.AddIngredient(ModContent.ItemType<星辉02>(), 1);
				recipe.AddIngredient(ModContent.ItemType<MK03>(), 1);
				recipe.AddTile(TileID.MythrilAnvil);
				recipe.ReplaceResult(this, 1);
				recipe.Register();
			}
		}

		public class MK05 : MKnives
		{
			public override string Texture => "MysteriousKnives/Pictures/Items/MK05";
			public override void SetStaticDefaults()
			{
				DisplayName.SetDefault("诡秘飞刀V");
				Tooltip.SetDefault("射出一团不明物质，随后炸出数把诡秘飞刀\n" +
					"散射5+4");
				CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			}
			public override void SetDefaults()
			{
				Item.damage = 100;
				Item.crit = 50;
				Item.knockBack = 6;
				Item.value = Item.sellPrice(1, 50, 0, 0);
				Item.rare = ModContent.RarityType<Rare_Purple>();
				base.SetDefaults();
			}
			public override void AddRecipes()
			{
				Recipe recipe = CreateRecipe();
				recipe.AddIngredient(ModContent.ItemType<诡毒04>(), 1);
				recipe.AddIngredient(ModContent.ItemType<生命05>(), 1);
				recipe.AddIngredient(ModContent.ItemType<力量04>(), 1);
				recipe.AddIngredient(ModContent.ItemType<凝爆04>(), 1);
				recipe.AddIngredient(ModContent.ItemType<深渊04>(), 1);
				recipe.AddIngredient(ModContent.ItemType<星辉03>(), 1);
				recipe.AddIngredient(ModContent.ItemType<MK04>(), 1);
				recipe.AddTile(TileID.LunarCraftingStation);
				recipe.ReplaceResult(this, 1);
				recipe.Register();
			}
		}

		public class MK06 : MKnives
		{
			public override string Texture => "MysteriousKnives/Pictures/Items/MK06";
			public override void SetStaticDefaults()
			{
				DisplayName.SetDefault("诡秘飞刀VI");
				Tooltip.SetDefault("射出一团不明物质，随后炸出数把诡秘飞刀\n" +
					"散射6+4");
				CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			}
			public override void SetDefaults()
			{
				Item.damage = 125;
				Item.crit = 60;
				Item.knockBack = 6;
				Item.value = Item.sellPrice(4, 0, 0, 0);
				Item.rare = ModContent.RarityType<Rare_Pink>();
				base.SetDefaults();
			}
			public override void AddRecipes()
			{
				{
					Recipe recipe = CreateRecipe();
					recipe.AddIngredient(ModContent.ItemType<生命06>(), 1);
					recipe.AddIngredient(ModContent.ItemType<结晶04>(), 1);
					recipe.AddIngredient(ModContent.ItemType<力量05>(), 1);
					recipe.AddIngredient(ModContent.ItemType<深渊05>(), 1);
					recipe.AddIngredient(ModContent.ItemType<MK05>(), 1);
					if (ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod))
						recipe.AddIngredient(CalamityMod.Find<ModItem>("BloodstoneCore"), 10);
					recipe.AddTile(TileID.LunarCraftingStation);
					recipe.ReplaceResult(this, 1);
					recipe.Register();
				}
			}
		}

		public class MK07 : MKnives
		{
			public override string Texture => "MysteriousKnives/Pictures/Items/MK07";
			public override void SetStaticDefaults()
			{
				DisplayName.SetDefault("诡秘飞刀VII");
				Tooltip.SetDefault("射出一团不明物质，随后炸出数把诡秘飞刀\n" +
					"散射7+5");
				CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			}
			public override void SetDefaults()
			{
				Item.damage = 150;
				Item.crit = 70;
				Item.knockBack = 6;
				Item.value = Item.sellPrice(10, 0, 0, 0);
				Item.rare = ModContent.RarityType<Rare_Orange>();
				base.SetDefaults();
			}

			public override void AddRecipes()
			{
				Recipe recipe = CreateRecipe();
				recipe.AddIngredient(ModContent.ItemType<沉沦03>(), 1);
				recipe.AddIngredient(ModContent.ItemType<深渊06>(), 1);
				recipe.AddIngredient(ModContent.ItemType<星辉04>(), 1);
				recipe.AddIngredient(ModContent.ItemType<MK06>(), 1);
				if (ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod))
					recipe.AddTile(CalamityMod.Find<ModTile>("CosmicAnvil"));
				else recipe.ReplaceResult(this, 1);
				recipe.Register();
			}
		}

		public class MK08 : MKnives
		{
			public override string Texture => "MysteriousKnives/Pictures/Items/MK08";
			public override void SetStaticDefaults()
			{
				DisplayName.SetDefault("诡秘飞刀VIII");
				Tooltip.SetDefault("射出一团不明物质，随后炸出数把诡秘飞刀\n" +
					"散射8+5");
				CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			}
			public override void SetDefaults()
			{
				Item.damage = 200;
				Item.crit = 80;
				Item.knockBack = 6;
				Item.value = Item.sellPrice(30, 0, 0, 0);
				Item.rare = ModContent.RarityType<Rare_Gold>();
				base.SetDefaults();
			}
			public override void AddRecipes()
			{
				Recipe recipe = CreateRecipe();
				recipe.AddIngredient(ModContent.ItemType<生命07>(), 1);
				recipe.AddIngredient(ModContent.ItemType<凝爆05>(), 1);
				recipe.AddIngredient(ModContent.ItemType<MK07>(), 1);
				if (ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod))
					recipe.AddTile(CalamityMod.Find<ModTile>("CosmicAnvil"));
				else recipe.ReplaceResult(this, 1);
				recipe.Register();
			}
		}

		public class MK09 : MKnives
		{
			public override string Texture => "MysteriousKnives/Pictures/Items/MK09";
			public override void SetStaticDefaults()
			{
				DisplayName.SetDefault("诡秘飞刀IX");
				Tooltip.SetDefault("射出一团不明物质，随后炸出数把诡秘飞刀\n" +
					"散射9+6");
				CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			}
			public override void SetDefaults()
			{
				Item.damage = 250;
				Item.crit = 90;
				Item.knockBack = 6;
				Item.value = Item.sellPrice(80, 0, 0, 0);
				Item.rare = ModContent.RarityType<Rare_Red>();
				base.SetDefaults();
			}
			public override void AddRecipes()
			{
				Recipe recipe = CreateRecipe();
				recipe.AddIngredient(ModContent.ItemType<结晶05>(), 1);
				recipe.AddIngredient(ModContent.ItemType<凝爆06>(), 1);
				recipe.AddIngredient(ModContent.ItemType<深渊07>(), 1);
				recipe.AddIngredient(ModContent.ItemType<MK08>(), 1);
				if (ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod))
					recipe.AddTile(CalamityMod.Find<ModTile>("DraedonsForge"));
				else recipe.ReplaceResult(this, 1);
				recipe.Register();
			}
		}

		public class MK10 : MKnives
		{
			public override string Texture => "MysteriousKnives/Pictures/Items/MK10";
			public override void SetStaticDefaults()
			{
				DisplayName.SetDefault("诡秘飞刀·终末X");
				Tooltip.SetDefault("终末撕裂时空");
			}
			public override void SetDefaults()
			{
				Item.width = 32;
				Item.height = 32;
				Item.autoReuse = false;
				Item.noMelee = true;
				Item.noUseGraphic = true;
				Item.DamageType = DamageClass.Melee;
				Item.useStyle = ItemUseStyleID.Shoot;
				Item.damage = 300;
				Item.knockBack = 20;
				Item.crit = 100;
				Item.useTime = 15;
				Item.useAnimation = 15;
				Item.value = Item.sellPrice(150, 0, 0, 0);
				Item.rare = ModContent.RarityType<Rare_Rainbow>();
				Item.shoot = ModContent.ProjectileType<MKchannel>();
				Item.shootSpeed = 10f;
				Item.channel = true;
			}
            /*public override void OnCreate(ItemCreationContext context)
			{
				Item.Prefix(ModContent.PrefixType<MKprefix01>());
			}*/
            public override bool CanUseItem(Player player)
            {
				NPC target = null;
				foreach (NPC npc in Main.npc)
				if (npc.CanBeChasedBy() && !npc.dontTakeDamage) 
					target = npc;
				if (target != null) return true;
				else return false;
            }
            public override void HoldItem(Player player)
            {
				player.AddBuff(ModContent.BuffType<RejuvenationBlessing>(), 8 * 180);
				player.AddBuff(ModContent.BuffType<StrengthEX>(), 6 * 180);
				player.AddBuff(ModContent.BuffType<AstralRay>(), 4 * 180);
			}
            public override void AddRecipes()
			{
				Recipe recipe = CreateRecipe();
				recipe.AddIngredient(ModContent.ItemType<MK09>(), 1);
				if (ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod))
				{
					recipe.AddIngredient(CalamityMod.Find<ModItem>("Rock"), 1);
					recipe.AddTile(CalamityMod.Find<ModTile>("DraedonsForge"));
				}
				else recipe.AddTile(TileID.LunarCraftingStation);
				recipe.ReplaceResult(this, 1);
				recipe.Register();
			}
		}
	}
}