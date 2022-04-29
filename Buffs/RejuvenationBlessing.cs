using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using static MysteriousKnives.Dusts.MDust;

namespace MysteriousKnives.Buffs
{
    /// <summary>
    /// 回春祝福
    /// </summary>
    public class RejuvenationBlessing : MysteriousBuffs
    {
        public override string Texture => "MysteriousKnives/Buffs/pictures/RejuvenationBlessing";
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("回春祝福");
            Description.SetDefault("你的一切伤痛都会迅速消失");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false;
            Main.buffNoSave[Type] = false;
            Segment.PlayerSegment = 180;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            switch(Segment.GetPlayerBuffSegment())
            {
                case 0:
                    RejuvenationEffect(player, 10);
                    break;
                case 1:
                    RejuvenationEffect(player, 25);
                    break;
                case 2:
                    RejuvenationEffect(player, 40);
                    break;
                case 3:
                    RejuvenationEffect(player, 55);
                    break;
                case 4:
                    RejuvenationEffect(player, 70);
                    break;
                case 5:
                    RejuvenationEffect(player, 85);
                    break;
                case 6:
                    RejuvenationEffect(player, 100);
                    break;
                case 7:
                    RejuvenationEffect(player, 120);
                    break;
            }
            Dust dust = Dust.NewDustDirect(player.position, player.width, player.height,
                    ModContent.DustType<RBDust>(), 0f, 0f, 0, default, 1f);
            // 粒子特效不受重力
            dust.alpha = 30;
            base.Update(player, ref buffIndex);
        }
    }
}
