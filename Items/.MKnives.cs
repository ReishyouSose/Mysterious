using Microsoft.Xna.Framework;
using MysteriousKnives.Buffs;
using MysteriousKnives.Projectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static MysteriousKnives.Buffs.MysteriousBuffs;
using static MysteriousKnives.Dusts.MDust;
using static MysteriousKnives.Projectiles.MysteriousKnife;

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
			Item.shoot = ModContent.ProjectileType<Knife_Mysterious>();
			Item.shootSpeed = 10f;
			Item.UseSound = SoundID.Item113;
			base.SetDefaults();
		}

		/*
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, 
			ref int type, ref int damage, ref float knockback)
        {
			int cn = 2, rn = Main.rand.Next(2);
			float fx = (cn + rn) / 2f;
			Vector2 vec = Main.MouseWorld - player.Center;
			for (int num = cn + rn; num >= 0; num--)
            {
				Vector2 shootVec = (vec.ToRotation() + (num - fx) * MathHelper.Pi / 60f).ToRotationVector2() * 20f;
				Projectile.NewProjectile(player.GetSource_ItemUse(Item), position, shootVec, 
					type, damage, knockback);
            }
        }*/

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
				Item.rare = ItemRarityID.Green;
				base.SetDefaults();
			}


			public override void AddRecipes()
			{
				Recipe recipe = CreateRecipe();
				recipe.AddIngredient(ItemID.DirtBlock, 10);
				recipe.AddTile(TileID.WorkBenches);
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
					"散射1+2");
				CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			}

			public override void SetDefaults()
			{
				Item.damage = 40;
				Item.crit = 20;
				Item.knockBack = 6;
				Item.value = Item.sellPrice(0, 10, 0, 0);
				Item.rare = ItemRarityID.Green;
				base.SetDefaults();
			}
	
            public override void AddRecipes()
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ItemID.DirtBlock, 10);
                recipe.AddTile(TileID.WorkBenches);
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
					"散射1+2");
				CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			}

			public override void SetDefaults()
			{
				Item.damage = 50;
				Item.crit = 30;
				Item.knockBack = 6;
				Item.value = Item.sellPrice(0, 30, 0, 0);
				Item.rare = ItemRarityID.Green;
				base.SetDefaults();
			}


			public override void AddRecipes()
			{
				Recipe recipe = CreateRecipe();
				recipe.AddIngredient(ItemID.DirtBlock, 10);
				recipe.AddTile(TileID.WorkBenches);
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
					"散射1+2");
				CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			}

			public override void SetDefaults()
			{
				Item.damage = 75;
				Item.crit = 40;
				Item.knockBack = 6;
				Item.value = Item.sellPrice(0, 80, 0, 0);
				Item.rare = ItemRarityID.Green;
				base.SetDefaults();
			}


			public override void AddRecipes()
			{
				Recipe recipe = CreateRecipe();
				recipe.AddIngredient(ItemID.DirtBlock, 10);
				recipe.AddTile(TileID.WorkBenches);
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
					"散射1+2");
				CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			}

			public override void SetDefaults()
			{
				Item.damage = 100;
				Item.crit = 50;
				Item.knockBack = 6;
				Item.value = Item.sellPrice(1, 50, 0, 0);
				Item.rare = ItemRarityID.Green;
				base.SetDefaults();
			}


			public override void AddRecipes()
			{
				Recipe recipe = CreateRecipe();
				recipe.AddIngredient(ItemID.DirtBlock, 10);
				recipe.AddTile(TileID.WorkBenches);
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
					"散射1+2");
				CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			}

			public override void SetDefaults()
			{
				Item.damage = 125;
				Item.crit = 60;
				Item.knockBack = 6;
				Item.value = Item.sellPrice(4, 0, 0, 0);
				Item.rare = ItemRarityID.Green;
				base.SetDefaults();
			}


			public override void AddRecipes()
			{
				Recipe recipe = CreateRecipe();
				recipe.AddIngredient(ItemID.DirtBlock, 10);
				recipe.AddTile(TileID.WorkBenches);
				recipe.Register();
			}
		}
		public class MK07 : MKnives
		{
			public override string Texture => "MysteriousKnives/Pictures/Items/MK07";
			public override void SetStaticDefaults()
			{
				DisplayName.SetDefault("诡秘飞刀VII");
				Tooltip.SetDefault("射出一团不明物质，随后炸出数把诡秘飞刀\n" +
					"散射1+2");
				CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			}

			public override void SetDefaults()
			{
				Item.damage = 150;
				Item.crit = 70;
				Item.knockBack = 6;
				Item.value = Item.sellPrice(10, 0, 0, 0);
				Item.rare = ItemRarityID.Green;
				base.SetDefaults();
			}


			public override void AddRecipes()
			{
				Recipe recipe = CreateRecipe();
				recipe.AddIngredient(ItemID.DirtBlock, 10);
				recipe.AddTile(TileID.WorkBenches);
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
					"散射1+2");
				CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			}

			public override void SetDefaults()
			{
				Item.damage = 200;
				Item.crit = 80;
				Item.knockBack = 6;
				Item.value = Item.sellPrice(30, 0, 0, 0);
				Item.rare = ItemRarityID.Green;
				base.SetDefaults();
			}


			public override void AddRecipes()
			{
				Recipe recipe = CreateRecipe();
				recipe.AddIngredient(ItemID.DirtBlock, 10);
				recipe.AddTile(TileID.WorkBenches);
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
					"散射1+2");
				CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			}

			public override void SetDefaults()
			{
				Item.damage = 250;
				Item.crit = 90;
				Item.knockBack = 6;
				Item.value = Item.sellPrice(80, 0, 0, 0);
				Item.rare = ItemRarityID.Green;
				base.SetDefaults();
			}


			public override void AddRecipes()
			{
				Recipe recipe = CreateRecipe();
				recipe.AddIngredient(ItemID.DirtBlock, 10);
				recipe.AddTile(TileID.WorkBenches);
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
				Item.autoReuse = false;
				Item.noMelee = true;
				Item.noUseGraphic = true;
				Item.DamageType = DamageClass.Melee;
				Item.useStyle = ItemUseStyleID.Shoot;
				Item.damage = 300;
				Item.crit = 100;
				Item.knockBack = 6;
				Item.useTime = 2;
				Item.useAnimation = 2;
				Item.value = Item.sellPrice(150, 0, 0, 0);
				Item.rare = ItemRarityID.Green;
				Item.shoot = ModContent.ProjectileType<MKchannel>();
				Item.shootSpeed = 10f;
				Item.channel = true;
			}
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
				recipe.AddIngredient(ItemID.DirtBlock, 10);
				recipe.AddTile(TileID.WorkBenches);
				recipe.Register();
			}
		}
	}

}
