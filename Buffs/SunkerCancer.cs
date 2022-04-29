using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using MysteriousKnives.NPCs;
using static MysteriousKnives.Dusts.MDust;

namespace MysteriousKnives.Buffs
{
    /// <summary>
    /// 沉沦之癌
    /// </summary>
    public class SunkerCancer : MysteriousBuffs
    {
        public override string Texture => "MysteriousKnives/Buffs/pictures/SunkerCancer";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("沉沦之癌");
            Description.SetDefault("你正逐渐摆烂");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = false;
            Segment.NPCSegment = 180;
            Segment.PlayerSegment = 180;
        }
        
        public override void Update(NPC npc, ref int buffIndex)
        {
            switch (Segment.GetNpcBuffSegment())
            {
                case 0:
                    npc.GetGlobalNPC<MKGlobalNPC>().SK1 = true;
                    break;
                case 1:
                    npc.GetGlobalNPC<MKGlobalNPC>().SK2 = true;
                    break;
                case 3:
                    npc.GetGlobalNPC<MKGlobalNPC>().SK3 = true;
                    break;
            }
            Dust dust = Dust.NewDustDirect(npc.position, npc.width, npc.height,
                    ModContent.DustType<SKDust>(), 0f, 0f, 0, default, 1f);
            // 粒子特效不受重力
            dust.alpha = 30;
            base.Update(npc, ref buffIndex);
        }
    }
}
