using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static MysteriousKnives.Dusts.MDust;

namespace MysteriousKnives.Buffs
{
    /// <summary>
    /// 诡秘Buffs
    /// </summary>
    public abstract class MysteriousBuffs : ModBuff
    {
        public BuffSegment Segment;//定义数据
        public override void SetStaticDefaults()
        {
            Segment = new BuffSegment
            {
                Time = 0
            };
            base.SetStaticDefaults();
        }//初始化
        public override void Update(NPC npc, ref int buffIndex)
        {
            Segment.Time = npc.buffTime[buffIndex];//被分段时间为npc的buff剩余时间
            base.Update(npc, ref buffIndex);
        }
        public override void Update(Player player, ref int buffIndex)
        {
            Segment.Time = player.buffTime[buffIndex];
            base.Update(player, ref buffIndex);
        }


        //定义方法
        public static void VemonDamage(NPC npc, int damage)
        {
            npc.life -= damage;
            if (npc.life <= 0)
            {
                npc.life = 1;
                npc.StrikeNPC(1, 0, 0);
            }
            CombatText.NewText(new Rectangle((int)npc.Center.X, (int)npc.Center.Y - 20, npc.width, npc.height),
                    new Color(180, 230, 50), damage, false, false);
        }
        public static void CrystalDamage(NPC npc, int damage)
        {
            npc.life -= damage;
            if (npc.life <= 0)
            {
                npc.life = 1;
                npc.StrikeNPC(1, 0, 0);
            }
            CombatText.NewText(new Rectangle((int)npc.Center.X, (int)npc.Center.Y - 20, npc.width, npc.height),
                   new Color(230, 161, 255), damage, false, false);
        }
        public static void RejuvenationEffect(Player player, int boost)
        {
            player.lifeRegen += boost;
        }
        public static void StrengthEffect(Player player, float boost)
        {
            player.GetDamage<MeleeDamageClass>() += boost;
        }
        public static void ArstalEffect(Player player, int boost)
        {
            player.GetArmorPenetration<GenericDamageClass>() += boost;
        }
        public static void ConBurst(NPC npc, float multiple, float uplimit, int baseamount)
        {
            if(multiple > uplimit) multiple = uplimit;
            npc.life -= (int)(baseamount * multiple);
            CombatText.NewText(new Rectangle((int)npc.Center.X, (int)npc.Center.Y - 20, npc.width, npc.height),
                    new Color(255, 100, 56), (int)(baseamount * multiple), false, false);
            if (npc.life <= 0)
            {
                npc.life = 1;
                npc.StrikeNPC(1, 0, 0);
            }

            for (int i = 0; i < 100; i++)
            {
                Dust dust = Dust.NewDustDirect(npc.position, npc.width, npc.height,
                    ModContent.DustType<CBDust>(), 0f, 0f, 0, default, 1f);
                // 粒子特效不受重力
                dust.alpha = 30;
                dust.scale *= 1.5f;
                dust.velocity *= 50;
                dust.noGravity = false;
            }
            SoundEngine.PlaySound(SoundID.Item14);
        }
    }
}