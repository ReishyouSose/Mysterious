using Microsoft.Xna.Framework;
using MysteriousKnives.NPCs;
using MysteriousKnives.Projectiles;
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
        public static void ConBurst(NPC npc, float multiple, int baseamount)
        {
            multiple = Math.Min(multiple, 20);
            npc.life -= (int)(baseamount * multiple);
            CombatText.NewText(new Rectangle((int)npc.Center.X, (int)npc.Center.Y - 20, npc.width, npc.height),
                    new Color(255, 100, 56), (int)(baseamount * multiple), false, false);
            //Main.NewText((int)(baseamount * multiple));
            if (npc.life <= 0)
            {
                npc.life = 1;
                npc.StrikeNPC(1, 0, 0);
            }
            for (int i = 0; i < 100; i++)
            {
                Dust dust = Dust.NewDustDirect(npc.position, npc.width, npc.height, ModContent.DustType<CBDust>());
                dust.scale *= 3f;
                dust.velocity *= 50;
                dust.noGravity = false;
            }
            SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot("MysteriousKnives/Sounds/Boom"));
        }



        /// <summary>
        /// 星辉射线
        /// </summary>
        public class AstralRay : MysteriousBuffs
        {
            public override string Texture => "MysteriousKnives/Pictures/Buffs/AstralRay";
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
                Dust.NewDustDirect(player.position, player.width, player.height, ModContent.DustType<ASDust>());
                base.Update(player, ref buffIndex);
            }
        }
        /// <summary>
        /// 收束爆破
        /// </summary>
        public abstract class ConvergentBurst : MysteriousBuffs
        {
            public override void SetStaticDefaults()
            {
                DisplayName.SetDefault("收束爆破");
                Description.SetDefault("给火焰能量一些收束的时间\n" +
                    "然后，欣赏艺术\n");
                Main.buffNoTimeDisplay[Type] = false;
                Main.debuff[Type] = true;
                Main.buffNoSave[Type] = false;
                base.SetStaticDefaults();
            }
            public int count = 0, x = 0;
            public float multiple;
            public override void Update(NPC npc, ref int buffIndex)
            {
                Dust.NewDustDirect(npc.position, npc.width, npc.height, ModContent.DustType<CBDust>());
                if (npc.buffTime[buffIndex] > 0) count++;
                else count = 0;
                multiple = 1 + npc.buffTime[buffIndex] / 180f;
                base.Update(npc, ref buffIndex);
            }
        }
        public class ConvergentBurst1 : ConvergentBurst
        {
            public override string Texture => "MysteriousKnives/Pictures/Buffs/ConvergentBurst1";
            public override void SetStaticDefaults()
            {
                base.SetStaticDefaults();
            }
            public override void Update(NPC npc, ref int buffIndex)
            {
                if (count > 300)
                {
                    ConBurst(npc, multiple, 50);
                    count = 0;
                    x = 0;
                    npc.DelBuff(buffIndex);
                    return;
                }
                base.Update(npc, ref buffIndex);
            }
            public override bool ReApply(NPC npc, int time, int buffIndex)
            {
                npc.buffTime[buffIndex] += time;
                return true;
            }
        }
        public class ConvergentBurst2 : ConvergentBurst
        {
            public override string Texture => "MysteriousKnives/Pictures/Buffs/ConvergentBurst2";
            public override void SetStaticDefaults()
            {
                base.SetStaticDefaults();
            }
            public override void Update(NPC npc, ref int buffIndex)
            {
                if (count > 300)
                {
                    ConBurst(npc, multiple, 200);
                    count = 0;
                    x = 0;
                    npc.DelBuff(buffIndex);
                }
                base.Update(npc, ref buffIndex);
            }
            public override bool ReApply(NPC npc, int time, int buffIndex)
            {
                npc.buffTime[buffIndex] += time;
                return true;
            }
        }
        public class ConvergentBurst3 : ConvergentBurst
        {
            public override string Texture => "MysteriousKnives/Pictures/Buffs/ConvergentBurst3";
            public override void SetStaticDefaults()
            {
                base.SetStaticDefaults();
            }
            public override void Update(NPC npc, ref int buffIndex)
            {
                if (count > 300)
                {
                    ConBurst(npc, multiple, 250);
                    count = 0;
                    x = 0;
                    npc.DelBuff(buffIndex);
                }
                base.Update(npc, ref buffIndex);
            }
            public override bool ReApply(NPC npc, int time, int buffIndex)
            {
                npc.buffTime[buffIndex] += time;
                return true;
            }
        }
        public class ConvergentBurst4 : ConvergentBurst
        {
            public override string Texture => "MysteriousKnives/Pictures/Buffs/ConvergentBurst4";
            public override void SetStaticDefaults()
            {
                base.SetStaticDefaults();
            }
            public override void Update(NPC npc, ref int buffIndex)
            {
                if (count > 300)
                {
                    ConBurst(npc, multiple, 500);
                    count = 0;
                    x = 0;
                    npc.DelBuff(buffIndex);
                }
                base.Update(npc, ref buffIndex);
            }
            public override bool ReApply(NPC npc, int time, int buffIndex)
            {
                npc.buffTime[buffIndex] += time;
                return true;
            }
        }
        public class ConvergentBurst5 : ConvergentBurst
        {
            public override string Texture => "MysteriousKnives/Pictures/Buffs/ConvergentBurst5";
            public override void SetStaticDefaults()
            {
                base.SetStaticDefaults();
            }
            public override void Update(NPC npc, ref int buffIndex)
            {
                if (count > 300)
                {
                    ConBurst(npc, multiple, 1500);
                    count = 0;
                    x = 0;
                    npc.DelBuff(buffIndex);
                }
                base.Update(npc, ref buffIndex);
            }
            public override bool ReApply(NPC npc, int time, int buffIndex)
            {
                npc.buffTime[buffIndex] += time;
                return true;
            }
        }
        public class ConvergentBurst6 : ConvergentBurst
        {
            public override string Texture => "MysteriousKnives/Pictures/Buffs/ConvergentBurst6";
            public override void SetStaticDefaults()
            {
                base.SetStaticDefaults();
            }
            public override void Update(NPC npc, ref int buffIndex)
            {
                if (count > 300)
                {
                    ConBurst(npc, multiple, 3000);
                    count = 0;
                    x = 0;
                    npc.DelBuff(buffIndex);
                }
                base.Update(npc, ref buffIndex);
            }
            public override bool ReApply(NPC npc, int time, int buffIndex)
            {
                npc.buffTime[buffIndex] += time;
                return true;
            }
        }
        /// <summary>
        /// 结晶化
        /// </summary>
        public class Crystallization : MysteriousBuffs
        {
            public override string Texture => "MysteriousKnives/Pictures/Buffs/Crystallization";
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
                Dust.NewDustDirect(npc.position, npc.width, npc.height, ModContent.DustType<CSDust>());
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
        /// <summary>
        /// 不可名状恐惧
        /// </summary>
        public class IndescribableFear : MysteriousBuffs
        {
            public override string Texture => "MysteriousKnives/Pictures/Buffs/IndescribableFear";
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
                switch (Segment.GetNpcBuffSegment())
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
                Dust.NewDustDirect(npc.position, npc.width, npc.height, ModContent.DustType<ABDust>());
                base.Update(npc, ref buffIndex);
            }
        }
        /// <summary>
        /// 回春祝福
        /// </summary>
        public class RejuvenationBlessing : MysteriousBuffs
        {
            public override string Texture => "MysteriousKnives/Pictures/Buffs/RejuvenationBlessing";
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
                switch (Segment.GetPlayerBuffSegment())
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
                Dust.NewDustDirect(player.position, player.width, player.height, ModContent.DustType<RBDust>());
                base.Update(player, ref buffIndex);
            }
        }
        /// <summary>
        /// 筋力EX
        /// </summary>
        public class StrengthEX : MysteriousBuffs
        {
            public override string Texture => "MysteriousKnives/Pictures/Buffs/StrengthEX";
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
                switch (Segment.GetPlayerBuffSegment())
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
                Dust.NewDustDirect(player.position, player.width, player.height, ModContent.DustType<STDust>());
                base.Update(player, ref buffIndex);
            }
        }
        /// <summary>
        /// 沉沦之癌
        /// </summary>
        public class SunkerCancer : MysteriousBuffs
        {
            public override string Texture => "MysteriousKnives/Pictures/Buffs/SunkerCancer";
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
                Dust.NewDustDirect(npc.position, npc.width, npc.height, ModContent.DustType<SKDust>());
                base.Update(npc, ref buffIndex);
            }
        }
        /// <summary>
        /// 诡异剧毒
        /// </summary>
        public class WeirdVemon : MysteriousBuffs
        {
            public override string Texture => "MysteriousKnives/Pictures/Buffs/WeirdVemon";
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
                if (npc.buffTime[buffIndex] % 6 == 0)
                    switch (Segment.GetNpcBuffSegment())
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
                Dust.NewDustDirect(npc.position, npc.width, npc.height, ModContent.DustType<WVDust>());
                base.Update(npc, ref buffIndex);
            }
        }
    }
}