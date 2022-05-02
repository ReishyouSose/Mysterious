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
            
    }
    public class ConvergentBurst1 : MysteriousBuffs
    {
        public override string Texture => "MysteriousKnives/Buffs/pictures/ConvergentBurst1";
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
        private int count = 0, x = 0;
        public override void Update(NPC npc, ref int buffIndex)
        {
            Dust dust = Dust.NewDustDirect(npc.position, npc.width, npc.height
                  , ModContent.DustType<CBDust>(), 0f, 0f, 0, default, 1f);
            dust.alpha = 30;
            dust.noGravity = true;
            if (npc.buffTime[buffIndex] > 0) count++;
            else count = 0;
            if (npc.buffTime[buffIndex] == 179)
            {
                x += 180;
                Main.NewText("x=" + x);
            }
            if (x > 0) x--;
            float multiple = 1 + x / 180f;
            if (count > 300)
            {
                ConBurst(npc, multiple, 5, 50);
                count = 0;
                x = 0;
                npc.DelBuff(buffIndex);
            }
            base.Update(npc, ref buffIndex);
        }
    }
    public class ConvergentBurst2 : MysteriousBuffs
    {
        public override string Texture => "MysteriousKnives/Buffs/pictures/ConvergentBurst2";
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
        private int count = 0, x = 0;
        public override void Update(NPC npc, ref int buffIndex)
        {
            Dust dust = Dust.NewDustDirect(npc.position, npc.width, npc.height
                  , ModContent.DustType<CBDust>(), 0f, 0f, 0, default, 1f);
            dust.alpha = 30;
            dust.noGravity = true;
            if (npc.buffTime[buffIndex] > 0) count++;
            else count = 0;
            if (npc.buffTime[buffIndex] == 179)
            {
                x += 180;
                Main.NewText("x=" + x);
            }
            if (x > 0) x--;
            float multiple = 1 + x / 180f;
            if (count > 300)
            {
                ConBurst(npc, multiple, 7, 200);
                count = 0;
                x = 0;
                npc.DelBuff(buffIndex);
            }
            base.Update(npc, ref buffIndex);
        }
        public override bool ReApply(NPC npc, int time, int buffIndex)
        {
            npc.buffTime[buffIndex] += time;
            return base.ReApply(npc, time, buffIndex);
        }
    }
    public class ConvergentBurst3 : MysteriousBuffs
    {
        public override string Texture => "MysteriousKnives/Buffs/pictures/ConvergentBurst3";
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
        private int count = 0, x = 0;
        public override void Update(NPC npc, ref int buffIndex)
        {
            Dust dust = Dust.NewDustDirect(npc.position, npc.width, npc.height
                  , ModContent.DustType<CBDust>(), 0f, 0f, 0, default, 1f);
            dust.alpha = 30;
            dust.noGravity = true;
            if (npc.buffTime[buffIndex] > 0) count++;
            else count = 0;
            if (npc.buffTime[buffIndex] == 179)
            {
                x += 180;
                Main.NewText("x=" + x);
            }
            if (x > 0) x--;
            float multiple = 1 + x / 180f;
            if (count > 300)
            {
                ConBurst(npc, multiple, 9, 250);
                count = 0;
                x = 0;
                npc.DelBuff(buffIndex);
            }
            base.Update(npc, ref buffIndex);
        }
        public override bool ReApply(NPC npc, int time, int buffIndex)
        {
            npc.buffTime[buffIndex] += time;
            return base.ReApply(npc, time, buffIndex);
        }
    }
    public class ConvergentBurst4 : MysteriousBuffs
    {
        public override string Texture => "MysteriousKnives/Buffs/pictures/ConvergentBurst4";
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
        private int count = 0, x = 0;
        public override void Update(NPC npc, ref int buffIndex)
        {
            Dust dust = Dust.NewDustDirect(npc.position, npc.width, npc.height
                  , ModContent.DustType<CBDust>(), 0f, 0f, 0, default, 1f);
            dust.alpha = 30;
            dust.noGravity = true;
            if (npc.buffTime[buffIndex] > 0) count++;
            else count = 0;
            if (npc.buffTime[buffIndex] == 179)
            {
                x += 180;
                Main.NewText("x=" + x);
            }
            if (x > 0) x--;
            float multiple = 1 + x / 180f;
            if (count > 300)
            {
                ConBurst(npc, multiple, 11, 500);
                count = 0;
                x = 0;
                npc.DelBuff(buffIndex);
            }
            base.Update(npc, ref buffIndex);
        }
        public override bool ReApply(NPC npc, int time, int buffIndex)
        {
            npc.buffTime[buffIndex] += time;
            return base.ReApply(npc, time, buffIndex);
        }
    }
    public class ConvergentBurst5 : MysteriousBuffs
    {
        public override string Texture => "MysteriousKnives/Buffs/pictures/ConvergentBurst5";
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
        private int count = 0, x = 0;
        public override void Update(NPC npc, ref int buffIndex)
        {
            Dust dust = Dust.NewDustDirect(npc.position, npc.width, npc.height
                  , ModContent.DustType<CBDust>(), 0f, 0f, 0, default, 1f);
            dust.alpha = 30;
            dust.noGravity = true;
            if (npc.buffTime[buffIndex] > 0) count++;
            else count = 0;
            if (npc.buffTime[buffIndex] == 179)
            {
                x += 180;
                Main.NewText("x=" + x);
            }
            if (x > 0) x--;
            float multiple = 1 + x / 180f;
            if (count > 300)
            {
                ConBurst(npc, multiple, 13, 1500);
                count = 0;
                x = 0;
                npc.DelBuff(buffIndex);
            }
            base.Update(npc, ref buffIndex);
        }
        public override bool ReApply(NPC npc, int time, int buffIndex)
        {
            npc.buffTime[buffIndex] += time;
            return base.ReApply(npc, time, buffIndex);
        }
    }
    public class ConvergentBurst6 : MysteriousBuffs
    {
        public override string Texture => "MysteriousKnives/Buffs/pictures/ConvergentBurst6";
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
        private int count = 0, x = 0;
        public override void Update(NPC npc, ref int buffIndex)
        {
            Dust dust = Dust.NewDustDirect(npc.position, npc.width, npc.height
                  , ModContent.DustType<CBDust>(), 0f, 0f, 0, default, 1f);
            dust.alpha = 30;
            dust.noGravity = true;
            if (npc.buffTime[buffIndex] > 0) count++;
            else count = 0;
            if (npc.buffTime[buffIndex] == 179)
            {
                x += 180;
                Main.NewText("x=" + x);
            }
            if (x > 0) x--;
            float multiple = 1 + x / 180f;
            if (count > 300)
            {
                ConBurst(npc, multiple, 15, 3000);
                count = 0;
                x = 0;
                npc.DelBuff(buffIndex);
            }
            base.Update(npc, ref buffIndex);
        }
        public override bool ReApply(NPC npc, int time, int buffIndex)
        {
            npc.buffTime[buffIndex] += time;
            return base.ReApply(npc, time, buffIndex);
        }
    }
}
