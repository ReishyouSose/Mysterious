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
            Main.projFrames[Projectile.type] = 1;//动画被分成几份
            base.SetDefaults();
        }
        public override void OnSpawn(IEntitySource source)
        {
            SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot("MysteriousKnives/Sounds/Channel"));
        }
        public override void AI()
        {
            NPC target = null;
            var npclist = new List<(NPC npcwho, float distance)>();
            foreach (NPC npc in Main.npc)
            {
                if (npc.CanBeChasedBy() && !npc.dontTakeDamage)
                {
                    float LNPC = Vector2.Distance(npc.Center, Main.MouseWorld);
                    npclist.Add((npc, LNPC));
                }
            }
            target = npclist.MinBy(t => t.distance).npcwho;
            Player player = Main.player[Projectile.owner];
            player.heldProj = Projectile.whoAmI;
            if (player.dead) Projectile.Kill();
            if (player.channel)
            {
                player.itemTime = 2;
                player.itemAnimation = 2;
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), target.Center,
                (Main.rand.Next(360) * MathHelper.Pi / 180f).ToRotationVector2() * 30f,
                Random(8), Projectile.damage, Projectile.knockBack, player.whoAmI);
                float t = Main.GameUpdateCount * 0.1f;
                for (int i = 0; i < 36; i++)
                {
                    Dust dust = Dust.NewDustDirect(target.Center, target.width, target.height,
                        ModContent.DustType<RanbowDust>());
                    dust.position = target.Center + new Vector2((float)Math.Cos(Math.PI / 18 * (i + t)),
                        (float)Math.Sin(Math.PI / 18 * (i + t))) * ((float)Math.Cos(t / 2) / 10 + 1) * 100f;
                    dust.velocity *= 0;
                }
                for (int i = 0; i < 36; i++)
                {
                    Dust dust = Dust.NewDustDirect(target.Center, target.width, target.height,
                        ModContent.DustType<RanbowDust>());
                    dust.position = target.Center + new Vector2((float)-Math.Cos(Math.PI / 18 * (i + t)),
                        (float)Math.Sin(Math.PI / 18 * (i + t))) * ((float)Math.Cos(t / 2 + Math.PI) / 10 + 1) * 100f;
                    dust.velocity *= 0;
                }
            }
            else
            {
                return;
            }
        }
    }
}