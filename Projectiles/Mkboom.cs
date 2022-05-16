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
            Projectile.width = 15;//宽
            Projectile.height = 15;//高
            Projectile.scale = 20f;//体积倍率
            Projectile.timeLeft = 1;//存在时间60 = 1秒
            Projectile.DamageType = DamageClass.Melee;// 伤害类型
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
        public int dis = 0;
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

            if (dis < 10) dis++;
            int fluctuationDamage = (int)(Projectile.damage * (float)(1 + 0.1 * Main.rand.Next(-10, 10)));
            if (player.dead) Projectile.Kill();
            if (player.channel)
            {
                player.itemTime = 2;
                player.itemAnimation = 2;
                //target.StrikeNPC(fluctuationDamage, 0, 0, true, true);
                target.life -= fluctuationDamage;
                player.addDPS(fluctuationDamage);
                if (target.life <= 0)
                {
                    target.life = 1;
                    target.StrikeNPC(1, 0, 0, true);
                }
                CombatText.NewText(new Rectangle((int)target.position.X, (int)target.Center.Y - Main.rand.Next(10,30), 
                    target.width, target.height),new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), fluctuationDamage);
                switch (Main.rand.Next(8))
                {
                    case 0: target.AddBuff(ModContent.BuffType<Crystallization>(), 300); break;
                    case 1: target.AddBuff(ModContent.BuffType<ConvergentBurst6>(), 180); break;
                    case 2: target.AddBuff(ModContent.BuffType<IndescribableFear>(), 7 * 180); break;
                    case 3: target.AddBuff(ModContent.BuffType<SunkerCancer>(), 720); break;
                    case 4: target.AddBuff(ModContent.BuffType<WeirdVemon>(), 720); break;
                    case 5:
                        if (player.statLife < player.statLifeMax2)
                        {
                            int i = (player.statLifeMax2 - player.statLife) / 20;
                            player.statLife += i;
                            player.HealEffect(i);
                        } break;
                }
                float t = Main.GameUpdateCount * 0.1f;
                for (int i = 0; i < 18; i++)
                {
                    Dust dust = Dust.NewDustDirect(target.Center, target.width, target.height,
                        ModContent.DustType<RanbowDust>());
                    dust.position = target.Center + new Vector2((float)Math.Cos(Math.PI / 9 * (i + t)),
                        (float)Math.Sin(Math.PI / 9 * (i + t))) * ((float)Math.Cos(t / 2) / 10 + 1)
                        * 10f * dis;
                    dust.velocity *= 0;
                }
                for (int i = 0; i < 18; i++)
                {
                    Dust dust = Dust.NewDustDirect(target.Center, target.width, target.height,
                        ModContent.DustType<RanbowDust>());
                    dust.position = target.Center + new Vector2((float)-Math.Cos(Math.PI / 9 * (i + t)),
                        (float)Math.Sin(Math.PI / 9 * (i + t))) * ((float)Math.Cos(t / 2 + Math.PI) / 10 + 1)
                        * 10f * dis;
                    dust.velocity *= 0;
                }
            }
        }
    }
}