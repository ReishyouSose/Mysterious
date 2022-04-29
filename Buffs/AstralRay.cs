using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework.Graphics;
using static MysteriousKnives.Dusts.MDust;

namespace MysteriousKnives.Buffs
{ 
    /// <summary>
    /// 星辉射线
    /// </summary>
    public class AstralRay : MysteriousBuffs
    {
        public override string Texture => "MysteriousKnives/Buffs/pictures/AstralRay";
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("星辉射线");
            Description.SetDefault("星辉会瓦解一切");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false;
            Main.buffNoSave[Type] = false;
            Segment.PlayerSegment = 180;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            switch (Segment.GetPlayerBuffSegment())
            {
                case 0:
                    ArstalEffect(player, 20);
                    break;
                case 1:
                    ArstalEffect(player, 40);
                    break;
                case 2:
                    ArstalEffect(player, 60);
                    break;
                case 3:
                    ArstalEffect(player, 100);
                    break;
            }
            Dust dust = Dust.NewDustDirect(player.position, player.width, player.height,
                    ModContent.DustType<ASDust>(), 0f, 0f, 0, default, 1f);
            // 粒子特效不受重力
            dust.alpha = 30;
            base.Update(player, ref buffIndex);
        }
    }
}
