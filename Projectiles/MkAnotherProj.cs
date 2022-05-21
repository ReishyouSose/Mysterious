using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.PlayerDrawLayer;
using static MysteriousKnives.Dusts.MDust;
using static MysteriousKnives.Items.MKnives;
using Terraria.DataStructures;
using Terraria.Audio;
using System.Collections.Generic;
using System.Linq;
using static MysteriousKnives.Buffs.MysteriousBuffs;
using static MysteriousKnives.NPCs.MKGlobalNPC;

namespace MysteriousKnives.Projectiles
{
    public class MKboom : ModProjectile
    {
        public override string Texture => "MysteriousKnives/Pictures/Projectiles/MKboom";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("诡秘飞刀");
        }
        public override void SetDefaults()
        {
            Projectile.width = 16;//宽
            Projectile.height = 16;//高
            Projectile.scale = 20f;//体积倍率
            Projectile.timeLeft = 1;//存在时间60 = 1秒
            Projectile.DamageType = DamageClass.Generic;// 伤害类型
            Projectile.friendly = true;// 攻击敌方？
            Projectile.hostile = false;// 攻击友方？
            Projectile.ignoreWater = true;//忽视水？
            Projectile.tileCollide = false;//不穿墙？
            Projectile.penetrate = -1;//穿透数量 -1无限
            Projectile.aiStyle = -1;//附带原版弹幕AI ID
            Projectile.alpha = 255;
            Main.projFrames[Projectile.type] = 1;//动画被分成几份
            base.SetDefaults();
        }
    }

    public class MKchannel : ModProjectile
    {
        public override string Texture => "MysteriousKnives/Pictures/Projectiles/MKchannel";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("诡秘飞刀");
        }
        public override void SetDefaults()
        {
            Projectile.width = 15;//宽
            Projectile.height = 15;//高
            Projectile.friendly = false;
            Projectile.hostile = false;// 攻击友方？
            Projectile.ignoreWater = true;//忽视水？
            Projectile.tileCollide = false;//不穿墙？
            Projectile.penetrate = -1;//穿透数量 -1无限
            Projectile.aiStyle = -1;//附带原版弹幕AI ID
            Projectile.alpha = 255;
        }
        public override void OnSpawn(IEntitySource source)
        {
            SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot("MysteriousKnives/Sounds/Channel"));
        }
        public float dis = 0;
        public override void AI()
        {

            Player player = Main.player[Projectile.owner];
            NPC target = null;
            var npclist = new List<(NPC npcwho, float distance)>();
            foreach (NPC npc in Main.npc)
            {
                if (npc.CanBeChasedBy())
                {
                    float LNPC = Vector2.Distance(npc.Center, Main.MouseWorld);
                    npclist.Add((npc, LNPC));
                }
            }
            target = npclist.MinBy(t => t.distance).npcwho;

            float t = Main.GameUpdateCount * 0.1f;
            if (dis <= 10) dis++;
            if (dis > 10) dis = 10 * ((float)-Math.Cos(t / 1.25) / 10 + 1f);
            if (player.dead || !player.channel) Projectile.Kill();
            if (player.channel)
            {
                Projectile.timeLeft = 2;
                player.itemTime = 2;
                player.itemAnimation = 2;
                if (Main.GameUpdateCount % 3 == 0)
                {
                    int damage = (int)(Projectile.damage * (float)(3 + 0.3f * Main.rand.Next(-10, 10)) 
                                 * target.takenDamageMultiplier * 1.5f
                                 * (1 + (player.GetTotalCritChance(DamageClass.Melee)) / 100)
                                    + player.GetTotalAttackSpeed(DamageClass.Melee));
                    target.life -= damage;
                    if (target.realLife != -1)
                        Main.npc[target.realLife].life -= damage;
                    player.addDPS(damage);
                    CombatText.NewText(new Rectangle((int)target.position.X + Main.rand.Next(-32, 32),
                        (int)target.Center.Y - Main.rand.Next(16, 32), target.width, target.height), Main.DiscoColor, damage);
                }
                NPCnormalDead(target);
                switch (Main.rand.Next(8))
                {
                    case 0: target.AddBuff(ModContent.BuffType<Crystallization>(), 300); break;
                    case 1:
                        if (target.rarity != 0 || target.boss)
                        {
                            target.AddBuff(ModContent.BuffType<ConvergentBurst6>(), 180);
                        } break;
                    case 2: target.AddBuff(ModContent.BuffType<IndescribableFear>(), 7 * 180); break;
                    case 3: target.AddBuff(ModContent.BuffType<SunkerCancer>(), 720); break;
                    case 4: target.AddBuff(ModContent.BuffType<WeirdVemon>(), 720); break;
                    case 5:
                        if (player.statLife < player.statLifeMax2)
                        {
                            Vector2 targetVec = player.Center - Projectile.Center;
                            targetVec.Normalize();
                            Projectile.NewProjectile(Projectile.GetSource_FromAI(), target.Center, new Vector2(0, 0),
                                ModContent.ProjectileType<RB_Ray>(), 0, 0, player.whoAmI);
                        } break;
                }
                for (int i = 0; i < 72; i++)
                {
                    Dust dust = Dust.NewDustDirect(target.Center, target.width, target.height,
                        ModContent.DustType<RanbowDust>());
                    dust.position = target.Center + new Vector2((int)(Math.Pow(-1, i)) * (float)Math.Cos(Math.PI / 36 * (i + t)),
                        (float)Math.Sin(Math.PI / 36 * (i + t))) * 10f * dis;
                    dust.velocity *= 0;
                }
            }
        }
    }

    public class RB_Ray : ModProjectile
    {
        public override string Texture => "MysteriousKnives/Pictures/Projectiles/RB_Ray";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("回春光束");
        }
        public override void SetDefaults()
        {
            Projectile.width = 2;//宽
            Projectile.height = 2;//高
            Projectile.scale = 1f;//体积倍率
            Projectile.extraUpdates = 1;
            Projectile.timeLeft = 60000;//存在时间60 = 1秒
            Projectile.friendly = false;// 攻击敌方？
            Projectile.hostile = false;// 攻击友方？
            Projectile.ignoreWater = true;//忽视水？
            Projectile.tileCollide = false;//不穿墙？
            Projectile.penetrate = 1;//穿透数量 -1无限
            Projectile.aiStyle = -1;//附带原版弹幕AI ID
            Projectile.alpha = 255;
            Main.projFrames[Projectile.type] = 1;//动画被分成几份
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 deflection = Vector2.Normalize(player.Center - Projectile.Center);
            Projectile.velocity = (Projectile.velocity + deflection) * 30 / 31f;
            if (Vector2.Distance(player.Center, Projectile.Center) < 16)
            {
                int i = (player.statLifeMax2 - player.statLife) / 20;
                player.statLife += i;
                player.HealEffect(i);
                Projectile.Kill();
            }
            for (int i = 0; i < 30; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.Center, Projectile.width, Projectile.height,
                    ModContent.DustType<RBDust>());
                dust.velocity *= 0;
            }
        }
    }
}