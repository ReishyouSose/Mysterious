using Microsoft.Xna.Framework;
using MysteriousKnives.Buffs;
using MysteriousKnives.Projectiles;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace MysteriousKnives.Items
{
	public abstract class MKnives : ModItem
	{
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
		public abstract void GiveCsBuffs(NPC target);

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
			public override string Texture => "MysteriousKnives/Items/pictures/MK01";
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

			public override void GiveCsBuffs(NPC target)
			{
				target.AddBuff(ModContent.BuffType<Crystallization>(), 60);
			}
		}
		public class MK02 : MKnives
		{
			public override string Texture => "MysteriousKnives/Items/pictures/MK02";
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

			public override void GiveCsBuffs(NPC target)
			{
				target.AddBuff(ModContent.BuffType<Crystallization>(), 60);
			}
		}
        public class MK03 : MKnives
		{
			public override string Texture => "MysteriousKnives/Items/pictures/MK03";
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

			public override void GiveCsBuffs(NPC target)
			{
				target.AddBuff(ModContent.BuffType<Crystallization>(), 120);
			}
		}
		public class MK04 : MKnives
		{
			public override string Texture => "MysteriousKnives/Items/pictures/MK04";
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

			public override void GiveCsBuffs(NPC target)
			{
				target.AddBuff(ModContent.BuffType<Crystallization>(), 180);
			}
		}
		public class MK05 : MKnives
		{
			public override string Texture => "MysteriousKnives/Items/pictures/MK05";
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

			public override void GiveCsBuffs(NPC target)
			{
				target.AddBuff(ModContent.BuffType<Crystallization>(), 180);
			}
		}
		public class MK06 : MKnives
		{
			public override string Texture => "MysteriousKnives/Items/pictures/MK06";
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

			public override void GiveCsBuffs(NPC target)
			{
				target.AddBuff(ModContent.BuffType<Crystallization>(), 240);
			}
		}
		public class MK07 : MKnives
		{
			public override string Texture => "MysteriousKnives/Items/pictures/MK07";
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

			public override void GiveCsBuffs(NPC target)
			{
				target.AddBuff(ModContent.BuffType<Crystallization>(), 240);
			}
		}
		public class MK08 : MKnives
		{
			public override string Texture => "MysteriousKnives/Items/pictures/MK08";
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

			public override void GiveCsBuffs(NPC target)
			{
				target.AddBuff(ModContent.BuffType<Crystallization>(), 240);
			}
		}
		public class MK09 : MKnives
		{
			public override string Texture => "MysteriousKnives/Items/pictures/MK09";
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

			public override void GiveCsBuffs(NPC target)
			{
				target.AddBuff(ModContent.BuffType<Crystallization>(), 300);
			}
		}
		public class MK10 : MKnives
		{
			public override string Texture => "MysteriousKnives/Items/pictures/MK10";
			public override void SetStaticDefaults()
			{
				DisplayName.SetDefault("诡秘飞刀·终末X");
				Tooltip.SetDefault("终末撕裂时空");
			}

			public override void SetDefaults()
			{
				Item.autoReuse = true;
				Item.noMelee = true;
				Item.noUseGraphic = true;
				Item.DamageType = DamageClass.Melee;
				Item.UseSound = SoundID.Item1;
				Item.useStyle = ItemUseStyleID.Swing;
				Item.damage = 30;
				Item.crit = 100;
				Item.knockBack = 6;
				Item.useTime = 3;
				Item.useAnimation = 20;
				Item.value = Item.sellPrice(150, 0, 0, 0);
				Item.rare = ItemRarityID.Green;
				Item.shoot = ModContent.ProjectileType<MKboomX>();
				Item.shootSpeed = 10f;
			}
			
            public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, 
				ref int type, ref int damage, ref float knockback)
            {
				NPC target = null;
				var npclist = new List<(NPC npcwho, float distance)>();
				foreach (NPC npc in Main.npc )
				{
					if (npc.active && npc.life != 5 && !npc.friendly && npc.CanBeChasedBy())
					{
						float LNPC = Vector2.Distance(npc.Center, Main.MouseWorld);
						npclist.Add((npc, LNPC));
					}
				}
				target = npclist.MinBy(t => t.distance).npcwho;

				Projectile.NewProjectile(player.GetSource_ItemUse(Item),target.Center,
					(Main.rand.Next(360) * MathHelper.Pi / 180f).ToRotationVector2() * 30f, 
					Random(8), damage, knockback, 0);
            }

            public override void AddRecipes()
			{
				Recipe recipe = CreateRecipe();
				recipe.AddIngredient(ItemID.DirtBlock, 10);
				recipe.AddTile(TileID.WorkBenches);
				recipe.Register();
			}

			public override void GiveCsBuffs(NPC target)
			{
				target.AddBuff(ModContent.BuffType<Crystallization>(), 300);
			}
		}
	}

}
