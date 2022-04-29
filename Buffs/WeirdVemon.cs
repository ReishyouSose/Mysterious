using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using static MysteriousKnives.Dusts.MDust;

namespace MysteriousKnives.Buffs
{
    /// <summary>
    /// 诡异剧毒
    /// </summary>
    public class WeirdVemon : MysteriousBuffs
    {
        public override string Texture => "MysteriousKnives/Buffs/pictures/WeirdVemon";
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("诡异剧毒");//WeirdVemon
            Description.SetDefault("怎么就掉血了呢?");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = false;
            Segment.NPCSegment = 180;
            Segment.PlayerSegment = 180;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            if(npc.buffTime[buffIndex] %6 ==0)
            switch(Segment.GetNpcBuffSegment())
            {
                case 0:
                    VemonDamage(npc, 2);
                    break;
                case 1:
                    VemonDamage(npc, 5);
                    break;
                case 2:
                    VemonDamage(npc, 25);
                    break;
                case 3:
                    VemonDamage(npc, 50);
                    break;
            }
            Dust dust = Dust.NewDustDirect(npc.position, npc.width, npc.height,
                    ModContent.DustType<WVDust>(), 0f, 0f, 0, default, 1f);
            // 粒子特效不受重力
            dust.alpha = 30;
            base.Update(npc, ref buffIndex);
        }
    }
}
