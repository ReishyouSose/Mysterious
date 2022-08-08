using log4net.Core;
using Terraria.ID;

namespace MysteriousKnives.Items
{
    public class MKOrigin : ModItem
    {
        public override string Texture => "MysteriousKnives/Pictures/Items/MKsOrigin";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("诡秘起源");
            Tooltip.SetDefault("放在背包里，获得随进度增强的增益");
        }
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = 0;
            Item.rare = ItemRarityID.Green;
            Item.useTime = 17;
            Item.useAnimation = 17;
            Item.autoReuse = false;
            Item.useStyle = ItemUseStyleID.HoldUp;
        }
        public override void UpdateInventory(Player player)
        {
            //特殊
            //战争药水,郑静药水

            //不增强
            player.ammoCost75 = true;
            player.archery = true;
            player.tileSpeed += 0.25f;
            player.wallSpeed += 0.25f;
            player.blockRange++;
            player.cratePotion = true;//宝匣
            player.dangerSense = true;
            //player.slowFall = true;//羽落
            player.ignoreWater = true;
            player.accFlipper = true;
            player.gills = true;
            //player.gravControl = true;//重力控制
            player.luckPotion = 3;
            player.lifeMagnet = true;
            player.detectCreature = true;//危险感
            //player.inferno = true;//火圈范围200f
            //player.invis = true;//隐身
            player.manaRegen += player.statManaMax2 / 2;
            player.nightVision = true;
            player.lavaImmune = true;
            player.fireWalk = true;
            player.buffImmune[24] = true;
            Lighting.AddLight(player.Center, 0.8f, 0.95f, 1f);
            player.sonarPotion = true;
            player.findTreasure = true;
            player.kbBuff = true;
            player.resistCold = true;
            player.waterWalk = true;
            player.fishingSkill += 15;
            player.pickSpeed -= 0.25f; //挖矿速度
            player.thorns += 1f;
            //player.lifeForce = true;

            //增强
            float level = 0;
            if (NPC.downedBoss1 && NPC.downedBoss2 && NPC.downedBoss3) level++;
            if (Main.hardMode) level++;
            if (NPC.downedPlantBoss) level++;
            if (NPC.downedMoonlord) level++;
            rate = PlayerDatePanel.mul;
            rate += level;
            player.endurance += 0.1f;
            player.statDefense += 8;
            player.statLifeMax2 += player.statLifeMax / 5;
            player.lifeRegen += player.statLifeMax2 / 10;
            player.maxMinions += 1;
            player.moveSpeed += 1f + rate * 0.15f;
            //player.maxRunSpeed += 1.25f * (1 + rate);
            player.maxFallSpeed += 1 + rate;
            player.accRunSpeed += 0.25f + rate * 0.15f;
            player.wingAccRunSpeed += 0.25f + rate * 0.15f;
            //player.stepSpeed += 2.25f * (1 + rate);
            player.GetCritChance(DamageClass.Generic) += 10 + 5 * rate;
            player.GetArmorPenetration(DamageClass.Generic) += 10 + 5 * rate;
            player.GetDamage(DamageClass.Generic) += 0.1f + 0.05f * rate;
        }
        internal static float rate = 0;
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            //Tooltip.SetDefault($"当前加成倍率{rate}");
            //TooltipLine tooltip = new TooltipLine(Mod, "ExampleMod: HotPatato", $"You have {timer / 60f:N1} seconds left!") { OverrideColor = Color.Red };
            TooltipLine tooltip = new TooltipLine(Mod, Name, $"当前加成等级：{rate}\n" +
                $"旧三王：{NPC.downedBoss1 && NPC.downedBoss2 && NPC.downedBoss3}\n" +
                $"肉山：{Main.hardMode}\n" +
                $"世花：{NPC.downedPlantBoss}\n" +
                $"月总：{NPC.downedMoonlord}");
            tooltips.Add(tooltip);
        }
    }
}
