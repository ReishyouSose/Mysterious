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
        public override string Texture => "MysteriousKnives/Pictures/Items/MKpuppetSummon";
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
			Item.useTime = 4;
			Item.useAnimation = 4;
			Item.value = Item.sellPrice(0, 1, 0, 0);
			Item.rare = ItemRarityID.Green;
			base.SetDefaults();
        }
        public bool i = true;
        public override bool? UseItem(Player player)
        {
            if (i)
            {
                NPC.NewNPC(player.GetSource_ItemUse(Item), (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y,
                ModContent.NPCType<MKpuppet>());
            }
            if (!i)
            {
                foreach (NPC npc in Main.npc)
                {
                    if (npc.type == ModContent.NPCType<MKpuppet>())
                        npc.life = 0;
                }
            }
            i = !i;
            foreach (NPC npc in Main.npc)
            {
                if (npc.boss && npc.CanBeChasedBy())
                {
                    i = true;
                    return i;
                }
            }
            return true;
        }
    }
}
