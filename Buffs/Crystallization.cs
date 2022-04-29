using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using static MysteriousKnives.Dusts.MDust;

namespace MysteriousKnives.Buffs
{
    /// <summary>
    /// 结晶化
    /// </summary>
    public class Crystallization : MysteriousBuffs
    {
        public override string Texture => "MysteriousKnives/Buffs/pictures/Crystallization";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("结晶化");//Crystallization
            Description.SetDefault("你是个易碎品\n" +
                "再次被施加这个效果时会受到大额伤害");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = false;
            Segment.NPCSegment = 60;
            Segment.PlayerSegment = 60;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            Dust dust = Dust.NewDustDirect(npc.position, npc.width, npc.height,
                    ModContent.DustType<CSDust>(), 0f, 0f, 0, default, 1f);
            // 粒子特效不受重力
            dust.alpha = 30;
            base.Update(npc, ref buffIndex);
        }
        public override bool ReApply(NPC npc, int time, int buffIndex)
        {
            switch (Segment.GetNpcBuffSegment())
            {
                case 0:
                    CrystalDamage(npc, 50);
                    break;
                case 1:
                    CrystalDamage(npc, 150);
                    break;
                case 2:
                    CrystalDamage(npc, 350);
                    break;
                case 3:
                    CrystalDamage(npc, 500);
                    break;
                case 4:
                    CrystalDamage(npc, 1000);
                    break;
            }
            return base.ReApply(npc, time, buffIndex);
        }
    }
}
