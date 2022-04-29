using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using System;
using Microsoft.Xna.Framework;
using static MysteriousKnives.Dusts.MDust;

namespace MysteriousKnives.Buffs
{
    /// <summary>
    /// 收束爆破
    /// </summary>
    public class ConvergentBurst : MysteriousBuffs
    {
        public override string Texture => "MysteriousKnives/Buffs/pictures/ConvergentBurst";
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("收束爆破");
            Description.SetDefault("给火焰能量一些收束的时间\n" +
                "然后，欣赏艺术\n");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = false;
            Segment.NPCSegment = 180;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            Dust dust = Dust.NewDustDirect(npc.position, npc.width, npc.height
                  , ModContent.DustType<CBDust>(), 0f, 0f, 0, default, 1f);
            dust.alpha = 30;
            dust.noGravity = true;
            int i=0;
            if (npc.buffTime[buffIndex] > i) i = npc.buffTime[buffIndex];
            float multiple = 1 + i / 180f;
            if (npc.buffTime[buffIndex] % 300 == 0)
            {
                switch(Segment.GetNpcBuffSegment())
                {
                    case 0:
                        ConBurst(npc, multiple, 5, 50);
                        break;
                    case 1:
                        ConBurst(npc, multiple, 7, 200);
                        break;
                    case 2:
                        ConBurst(npc, multiple, 9, 250);
                        break;
                    case 3:
                        ConBurst(npc, multiple, 11, 500);
                        break;
                    case 4:
                        ConBurst(npc, multiple, 13, 1500);
                        break;
                    case 5:
                        ConBurst(npc, multiple, 15, 3000);
                        break;
                }
                npc.DelBuff(buffIndex);
            }
            base.Update(npc, ref buffIndex);
        }
        public override bool ReApply(NPC npc, int time, int buffIndex)
        {
            npc.buffTime[buffIndex] += time;
            if (npc.buffTime[buffIndex] > 1080) npc.buffTime[buffIndex] = 1080;
            return true;
        }
    }
}
