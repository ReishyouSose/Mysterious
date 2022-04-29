using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using static MysteriousKnives.Dusts.MDust;

namespace MysteriousKnives.Buffs
{
    /// <summary>
    /// 筋力EX
    /// </summary>
    public class StrengthEX : MysteriousBuffs
    {
        public override string Texture => "MysteriousKnives/Buffs/pictures/StrengthEX";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("筋力EX+++");//StrengthEX
            Description.SetDefault("你被强化了！快上！");
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
                    StrengthEffect(player, 0.1f);
                    break;
                case 1:
                    StrengthEffect(player, 0.2f);
                    break;
                case 2:
                    StrengthEffect(player, 0.3f);
                    break;
                case 3:
                    StrengthEffect(player, 0.4f);
                    break;
                case 4:
                    StrengthEffect(player, 0.5f);
                    break;
                case 5:
                    StrengthEffect(player, 0.6f);
                    break;
            }
            Dust dust = Dust.NewDustDirect(player.position, player.width, player.height,
                    ModContent.DustType<STDust>(), 0f, 0f, 0, default, 1f);
            // 粒子特效不受重力
            dust.alpha = 30;
            base.Update(player, ref buffIndex);
        }
    }
}
