using Microsoft.Xna.Framework;
using MysteriousKnives.Buffs;
using MysteriousKnives.NPCs;
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
	public class MKpuppetSummon : ModItem
    {
        public override string Texture => "MysteriousKnives/Items/pictures/MKpuppetSummon";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("傀儡召唤器");
            Tooltip.SetDefault("于鼠标处召唤一个无敌傀儡\n" +
                "只能存在一个\n" +
				"右键使用召回");
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
			Item.autoReuse = false;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.useStyle = ItemUseStyleID.Thrust;
			Item.useTime = 30;
			Item.useAnimation = 1;
			Item.value = Item.sellPrice(0, 1, 0, 0);
			Item.rare = ItemRarityID.Green;
			base.SetDefaults();
        }
        public override bool? UseItem(Player player)
        {
            foreach (NPC npc in Main.npc)
            {
                if (npc.type == ModContent.NPCType<MKpuppet>())
                {
                    npc.life = 0;
                }
            }
            NPC.NewNPC(player.GetSource_ItemUse(Item), (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y,
                ModContent.NPCType<MKpuppet>());
            if (player.altFunctionUse == 2)
            {
                foreach (NPC npc in Main.npc)
                {
                    if (npc.type == ModContent.NPCType<MKpuppet>()) npc.life = 0;
                }
            }
            return base.UseItem(player);
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
    }
}
