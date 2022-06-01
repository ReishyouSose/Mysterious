﻿using Microsoft.Xna.Framework;
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
using static MysteriousKnives.NPCs.MKGlobalNPC;
using static MysteriousKnives.Projectiles.MKSphere;

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
            Segment.Time = npc.buffTime[buffIndex];
        }
        public override void Update(Player player, ref int buffIndex)
        {
            Segment.Time = player.buffTime[buffIndex];
            base.Update(player, ref buffIndex);
        }
        public static void VemonDamage(NPC npc, int damage)
        {
            npc.life -= damage;
            if(npc.realLife != -1)
                Main.npc[npc.realLife].life -= damage;
            NPCnormalDead(npc);
            if(npc.realLife == -1)
            CombatText.NewText(new Rectangle((int)npc.position.X, (int)npc.Center.Y - Main.rand.Next(10, 30), 
                npc.width, npc.height), new Color(180, 230, 50), damage, false, true);
        }
        public static void CrystalDamage(NPC npc, int damage)
        {
            npc.life -= damage;
            NPCnormalDead(npc);
            CombatText.NewText(new Rectangle((int)npc.position.X, (int)npc.Center.Y - Main.rand.Next(10, 30), 
                npc.width, npc.height), new Color(230, 161, 255), damage, false, true);
        }
        public static void RejuvenationEffect(Player player, int regen)
        {
            player.statLife += regen;
        }
        public static void StrengthEffect(Player player, float boost)
        {
            player.GetDamage<MeleeDamageClass>() += boost;
        }
        public static void ArstalEffect(Player player, int boost)
        {
            player.GetArmorPenetration<GenericDamageClass>() += boost;
        }
        public static void ConBurst(NPC npc, float amend, float multiple, float baseamount)
        {
            multiple = Math.Min(multiple, 20f * amend);
            int damage = (int)(baseamount * multiple / amend);
            npc.life -= damage;
            CombatText.NewText(new Rectangle((int)npc.position.X, (int)npc.Center.Y - Main.rand.Next(10, 30), 
                npc.width, npc.height), new Color(255, 100, 56), damage, false, false);
            NPCnormalDead(npc);
            for (int i = 0; i < 200; i++)
            {
                Dust dust = Dust.NewDustDirect(npc.position, npc.width, npc.height, ModContent.DustType<CBDust>());
                dust.scale *= 3f;
                dust.velocity *= 300;
                dust.noGravity = false;
            }
            SoundEngine.PlaySound(new("MysteriousKnives/Sounds/Boom"));
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
            public bool i = true;
            public int ply;
            public override void Update(Player player, ref int buffIndex)
            {
                if (i) Projectile.NewProjectile(Entity.GetSource_NaturalSpawn(), player.position, new Vector2(0, 0),
                     ModContent.ProjectileType<ASsphere>(), 0, 0, player.whoAmI);
                i = false;
                if (player.buffTime[buffIndex] == 0)
                {
                    foreach (Projectile proj in Main.projectile)
                    {
                        if (proj.type == ModContent.ProjectileType<ASsphere>() && proj.ai[0] == player.whoAmI)
                        {
                            proj.Kill();
                            i = true;
                            ply = player.whoAmI;

                        }
                    }
                }
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
                base.Update(player, ref buffIndex);
            }
            public override bool RightClick(int buffIndex)
            {
                foreach (Projectile proj in Main.projectile)
                {
                    if (proj.type == ModContent.ProjectileType<ASsphere>() && proj.ai[0] == ply)
                        proj.Kill();
                }
                return true;
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
            public float multiple, ament;
            public override void Update(NPC npc, ref int buffIndex)
            {
                if (!npc.CanBeChasedBy())
                    npc.DelBuff(buffIndex);
                if (npc.buffTime[buffIndex] > 0) count++;
                else count = 0;
                x = Math.Max(x, npc.buffTime[buffIndex]);
                multiple = 1 + x / 180f;
                float t = Main.GameUpdateCount * 0.1f * (1 + count / 150);
                if (count < 270)
                    for (int i = 0; i < 12; i++)
                    {
                        Dust dust = Dust.NewDustDirect(npc.position, npc.width, npc.height, ModContent.DustType<CBDust>());
                        dust.position = npc.Center + new Vector2((float)Math.Cos(t + Math.PI / 6 * i) * (int)Math.Pow(-1, i),
                            (float)(Math.Sin(t + Math.PI / 6 * i)))* 200 * (float)Math.Sin(Math.PI * (1.5 + 0.5 / 270 * count));
                        dust.velocity *= 0;
                    }
                if (270 <= count)
                    for(float i = 0; i < multiple; i += 1 * ament)
                    {
                        Dust dust = Dust.NewDustDirect(npc.position, npc.width, npc.height, ModContent.DustType<CBDust>());
                        dust.position = npc.Center + new Vector2(
                            -(float)Math.Sin(Math.PI / (10 * ament) * i * (count - 270) / 30),
                             (float)Math.Cos(Math.PI / (10 * ament) * i * (count - 270) / 30)
                            ) * 70;
                        dust.velocity *= 0;
                    }
                base.Update(npc, ref buffIndex);
            }
            public override bool ReApply(NPC npc, int time, int buffIndex)
            {
                npc.buffTime[buffIndex] += time;
                return true;
            }
        }
        public class ConvergentBurst1 : ConvergentBurst
        {
            public override string Texture => "MysteriousKnives/Pictures/Buffs/ConvergentBurst1";
            public override void Update(NPC npc, ref int buffIndex)
            {
                ament = 0.25f;
                if (count >= 300)
                {
                    ConBurst(npc, ament, multiple, 50);
                    count = 0;
                    x = 0;
                    npc.DelBuff(buffIndex);
                    return;
                }
                base.Update(npc, ref buffIndex);
            }
        }
        public class ConvergentBurst2 : ConvergentBurst
        {
            public override string Texture => "MysteriousKnives/Pictures/Buffs/ConvergentBurst2";
            public override void Update(NPC npc, ref int buffIndex)
            {
                ament = 0.33f;
                if (count >= 300)
                {
                    ConBurst(npc, ament, multiple, 200);
                    count = 0;
                    x = 0;
                    npc.DelBuff(buffIndex);
                }
                base.Update(npc, ref buffIndex);
            }
        }
        public class ConvergentBurst3 : ConvergentBurst
        {
            public override string Texture => "MysteriousKnives/Pictures/Buffs/ConvergentBurst3";
            public override void Update(NPC npc, ref int buffIndex)
            {
                ament = 0.4f;
                if (count >= 300)
                {
                    ConBurst(npc, ament, multiple, 250);
                    count = 0;
                    x = 0;
                    npc.DelBuff(buffIndex);
                }
                base.Update(npc, ref buffIndex);
            }
        }
        public class ConvergentBurst4 : ConvergentBurst
        {
            public override string Texture => "MysteriousKnives/Pictures/Buffs/ConvergentBurst4";
            public override void Update(NPC npc, ref int buffIndex)
            {
                ament = 0.5f;
                if (count >= 300)
                {
                    ConBurst(npc, ament, multiple, 500);
                    count = 0;
                    x = 0;
                    npc.DelBuff(buffIndex);
                }
                base.Update(npc, ref buffIndex);
            }
        }
        public class ConvergentBurst5 : ConvergentBurst
        {
            public override string Texture => "MysteriousKnives/Pictures/Buffs/ConvergentBurst5";
            public override void Update(NPC npc, ref int buffIndex)
            {
                ament = 0.75f;
                if (count >= 300)
                {
                    ConBurst(npc, ament, multiple, 1500);
                    count = 0;
                    x = 0;
                    npc.DelBuff(buffIndex);
                }
                base.Update(npc, ref buffIndex);
            }
        }
        public class ConvergentBurst6 : ConvergentBurst
        {
            public override string Texture => "MysteriousKnives/Pictures/Buffs/ConvergentBurst6";
            public override void Update(NPC npc, ref int buffIndex)
            {
                ament = 0.9f;
                if (count >= 300)
                {
                    ConBurst(npc, ament, multiple, 3000);
                    count = 0;
                    x = 0;
                    npc.DelBuff(buffIndex);
                }
                base.Update(npc, ref buffIndex);
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
            }
            public override void Update(NPC npc, ref int buffIndex)
            {
                if (npc.realLife == -1)
                {
                    Dust dust = Dust.NewDustDirect(npc.position, npc.width, npc.height, ModContent.DustType<CSDust>());
                    float t = Main.GameUpdateCount * 0.05f;
                    //dust.scale *= 1.5f;
                    dust.position = npc.Center + new Vector2((float)(-Math.Cos(t) / 2f), (float)Math.Sin(t) / 5f) * 100;
                    dust.velocity *= 0;
                }
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
                if (npc.realLife == -1)
                {
                    Dust dust = Dust.NewDustDirect(npc.position, npc.width, npc.height, ModContent.DustType<ABDust>());
                    float t = Main.GameUpdateCount * 0.05f;
                    dust.scale *= 1.5f;
                    dust.position = npc.Center + new Vector2((float)-Math.Sin(t) / 10f, (float)Math.Cos(t) / 20f - 0.5f) * 100f;
                    dust.noGravity = false;
                }
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
            public bool i = true;
            public int ply;
            public override void Update(Player player, ref int buffIndex)
            {
                if (i) Projectile.NewProjectile(Entity.GetSource_NaturalSpawn(), player.position, new Vector2(0, 0),
                     ModContent.ProjectileType<RBsphere>(), 0, 0, player.whoAmI);
                i = false;
                if (player.buffTime[buffIndex] == 0)
                {
                    foreach (Projectile proj in Main.projectile)
                    {
                        if (proj.type == ModContent.ProjectileType<RBsphere>() && proj.ai[0] == player.whoAmI)
                        {
                            proj.Kill();
                            i = true;
                            ply = player.whoAmI;
                        }
                            
                    }
                }
                if (player.statLife < player.statLifeMax)
                switch (Segment.GetPlayerBuffSegment())
                {
                    case 0://10*1
                        if(player.buffTime[buffIndex] % 6 == 0)
                            RejuvenationEffect(player, 1);
                        break;
                    case 1://20*1+5*1
                        if(player.buffTime[buffIndex] % 3 == 0)
                            RejuvenationEffect(player, 1);
                        if(player.buffTime[buffIndex] % 12 == 0)
                            RejuvenationEffect(player, 1);
                        break;
                    case 2://20*2
                        if(player.buffTime[buffIndex] % 3 == 0)
                            RejuvenationEffect(player, 2);
                        break;
                    case 3://20*2+15*1
                        if (player.buffTime[buffIndex] % 3 == 0)
                            RejuvenationEffect(player, 2);
                        if (player.buffTime[buffIndex] % 4 == 0)
                            RejuvenationEffect(player, 1);
                        break;
                    case 4://60*1+10*1
                        if (player.buffTime[buffIndex] % 1 == 0)
                            RejuvenationEffect(player, 1);
                        if (player.buffTime[buffIndex] % 6 == 0)
                            RejuvenationEffect(player, 1);
                        break;
                    case 5://60*1+20*1+5*1
                        if (player.buffTime[buffIndex] % 1 == 0)
                            RejuvenationEffect(player, 1);
                        if (player.buffTime[buffIndex] % 3 == 0)
                            RejuvenationEffect(player, 1);
                        if (player.buffTime[buffIndex] % 12 == 0)
                            RejuvenationEffect(player, 1);
                        break;
                    case 6://60*1+20*2
                        if (player.buffTime[buffIndex] % 1 == 0)
                            RejuvenationEffect(player, 1);
                        if (player.buffTime[buffIndex] % 3 == 0)
                            RejuvenationEffect(player, 2);
                        break;
                    case 7://60*2
                        if (player.buffTime[buffIndex] % 1 == 0)
                            RejuvenationEffect(player, 2);
                        break;
                }
                base.Update(player, ref buffIndex);
            }
            public override bool RightClick(int buffIndex)
            {
                foreach (Projectile proj in Main.projectile)
                {
                    if (proj.type == ModContent.ProjectileType<RBsphere>() && proj.ai[0] == ply)
                        proj.Kill();
                }
                return true;
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
            public bool i = true;
            public int ply;
            public override void Update(Player player, ref int buffIndex)
            {
                if (i) Projectile.NewProjectileDirect(player.GetSource_Buff(buffIndex), player.position, new Vector2(0, 0),
                         ModContent.ProjectileType<STsphere>(), 0, 0, player.whoAmI, player.whoAmI);
                i = false;
                if (player.buffTime[buffIndex] == 0)
                {
                    foreach (Projectile proj in Main.projectile)
                    {
                        if (proj.type == ModContent.ProjectileType<STsphere>() && proj.ai[0] == player.whoAmI)
                        {
                            proj.Kill();
                            i = true;
                            ply = player.whoAmI;
                        }
                    }
                } 
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
                base.Update(player, ref buffIndex);
            }
            public override bool RightClick(int buffIndex)
            {
                foreach (Projectile proj in Main.projectile)
                {
                    if (proj.type == ModContent.ProjectileType<STsphere>() && proj.ai[0] == ply)
                        proj.Kill();
                }
                return true;
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
                if (npc.realLife == -1)
                {
                    Dust dust = Dust.NewDustDirect(npc.position, npc.width, npc.height, ModContent.DustType<SKDust>());
                    float t = Main.GameUpdateCount * 0.05f;
                    dust.scale *= 1.5f;
                    dust.position = npc.Center + new Vector2((float)Math.Sin(t) / 10f, (float)Math.Cos(t) / 20f + 0.5f) * 100f;
                    dust.noGravity = false;
                }
            }
        }
        /// <summary>
        /// 诡异诡毒
        /// </summary>
        public class WeirdVemon : MysteriousBuffs
        {
            public override string Texture => "MysteriousKnives/Pictures/Buffs/WeirdVemon";
            public override void SetStaticDefaults()
            {
                base.SetStaticDefaults();
                DisplayName.SetDefault("诡异诡毒");//WeirdVemon
                Description.SetDefault("怎么就掉血了呢?");
                Main.buffNoTimeDisplay[Type] = false;
                Main.debuff[Type] = true;
                Main.buffNoSave[Type] = false;
                Segment.NPCSegment = 180;
                Segment.PlayerSegment = 180;
            }

            public override void Update(NPC npc, ref int buffIndex)
            {
                if (npc.buffTime[buffIndex] % 6 == 0 && npc.CanBeChasedBy())
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
                if (npc.realLife == -1)
                {
                    Dust dust = Dust.NewDustDirect(npc.position, npc.width, npc.height, ModContent.DustType<WVDust>());
                    float t = Main.GameUpdateCount * 0.05f;
                    //dust.scale *= 1.5f;
                    dust.position = npc.Center + new Vector2((float)(-Math.Cos(t + Math.PI) / 2f),
                        (float)(Math.Sin(t + Math.PI) / 5f)) * 100;
                    dust.velocity *= 0;
                }
            }
        }
    }
}