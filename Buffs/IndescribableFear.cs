using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using MysteriousKnives.NPCs;
using static MysteriousKnives.Dusts.MDust;

namespace MysteriousKnives.Buffs
{
    /// <summary>
    /// 不可名状恐惧
    /// </summary>
    public class IndescribableFear : MysteriousBuffs
    {
        public override string Texture => "MysteriousKnives/Buffs/pictures/IndescribableFear";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("不可名状恐惧");//IndescribableFear
            Description.SetDefault("啊——大脑在颤抖——");
            Main.pvpBuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = false;
            Segment.NPCSegment = 180;
            Segment.PlayerSegment = 180;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            switch(Segment.GetNpcBuffSegment())
            {
                case 0:
                    npc.GetGlobalNPC<MKGlobalNPC>().AB1 = true;
                    break;
                case 1:
                    npc.GetGlobalNPC<MKGlobalNPC>().AB2 = true;
                    break;
                case 2:
                    npc.GetGlobalNPC<MKGlobalNPC>().AB3 = true;
                    break;
                case 3:
                    npc.GetGlobalNPC<MKGlobalNPC>().AB4 = true;
                    break;
                case 4:
                    npc.GetGlobalNPC<MKGlobalNPC>().AB5 = true;
                    break;
                case 5:
                    npc.GetGlobalNPC<MKGlobalNPC>().AB6 = true;
                    break;
                case 6:
                    npc.GetGlobalNPC<MKGlobalNPC>().AB7 = true;
                    break;
            }
            Dust dust = Dust.NewDustDirect(npc.position, npc.width, npc.height,
                    ModContent.DustType<ABDust>(), 0f, 0f, 0, default, 1f);
            // 粒子特效不受重力
            dust.alpha = 30;
            base.Update(npc, ref buffIndex);
        }
    }
}
