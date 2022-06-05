using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static MysteriousKnives.Dusts.MDust;
using static MysteriousKnives.Projectiles.MysteriousKnife;
using Terraria.DataStructures;
using Terraria.Audio;
using System.Collections.Generic;
using System.Linq;
using static MysteriousKnives.Buffs.MysteriousBuffs;
using static MysteriousKnives.NPCs.MKGlobalNPC;
using Microsoft.Xna.Framework.Graphics;

namespace MysteriousKnives.Projectiles
{
    public class MysteriousCore : MysteriousKnife
    {
        public override string Texture => "MysteriousKnives/Pictures/Projectiles/Another/MysteriousCore";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("诡秘飞刀");
        }
        public override void SetDefaults()
        {
            Projectile.width = 15;//宽
            Projectile.height = 15;//高
            Projectile.scale = 1f;//体积倍率
            Projectile.timeLeft = 180;//存在时间60 = 1秒
            Projectile.DamageType = DamageClass.Generic;// 伤害类型
            Projectile.friendly = true;// 攻击敌方？
            Projectile.hostile = false;// 攻击友方？
            Projectile.ignoreWater = true;//忽视水？
            Projectile.tileCollide = false;//不穿墙？
            Projectile.penetrate = 1;//穿透数量 -1无限
            Projectile.aiStyle = -1;//附带原版弹幕AI ID
            Projectile.extraUpdates = 1;//每帧额外更新次数
            Projectile.alpha = 0;
            //AIType = ProjectileID.RainbowCrystal;
            Main.projFrames[Projectile.type] = 118;//动画被分成几份
        }
        public int d;
        public override void AI()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter % 5 == 0)
            {
                Projectile.frame++;
                Projectile.frame %= 118;
            }
            if (Projectile.timeLeft % 5 == 0) Projectile.velocity *= 0.9f;
            Lighting.AddLight(Projectile.Center, Main.DiscoR / 255f, Main.DiscoG / 255f, Main.DiscoB / 255f);
            int rand = Projectile.timeLeft / 10;
            if (Projectile.timeLeft < 177 && Main.rand.Next(rand + 6) >= 5)//弹幕粒子效果
            {
                Dust.NewDust(Projectile.position - new Vector2(15, 15), 30, 30, ModContent.DustType<RainbowDust>());
            }
        }
        public override void Kill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];
            Projectile proj = Projectile.NewProjectileDirect(Projectile.GetSource_Death(), Projectile.Center, Projectile.velocity,
                ModContent.ProjectileType<MKboom>(),
                (int)(Projectile.damage * (float)(1 + (int)player.GetCritChance(DamageClass.Melee) / 100)),
                20, player.whoAmI);
            proj.CritChance = Projectile.CritChance;
            SoundEngine.PlaySound(SoundID.Item14);
            for (int i = 0; i < 100; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position - new Vector2(15, 15), 
                    30, 30, ModContent.DustType<RainbowDust>());
                dust.scale *= 1.5f;
                dust.velocity *= 50;
                dust.noGravity = false;
            }
            switch (MKID)
            {
                case 1: RandomShoot(player, 1, 2, 4); break;
                case 2: RandomShoot(player, 2, 2, 7); break;
                case 3: RandomShoot(player, 3, 3, 8); break;
                case 4: RandomShoot(player, 4, 3, 8); break;
                case 5: RandomShoot(player, 5, 4, 8); break;
                case 6: RandomShoot(player, 6, 4, 8); break;
                case 7: RandomShoot(player, 7, 5, 8); break;
                case 8: RandomShoot(player, 8, 5, 8); break;
                case 9: RandomShoot(player, 9, 6, 8); break;
                case 10: RandomShoot(player, 10, 6, 8); break;
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            SpriteBatch spriteBatch = Main.spriteBatch;
            Texture2D texture = ModContent.Request<Texture2D>("MysteriousKnives/Pictures/Projectiles/Sphere/RBsphere_1").Value;
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.AnisotropicClamp,
                DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            spriteBatch.Draw(texture: texture,
                position: Projectile.Center - Main.screenPosition,
                sourceRectangle: new Rectangle(0, Projectile.frame * 30, 30, 30),
                color: Main.DiscoColor,
                rotation: (float)Math.PI / 100f * Main.GameUpdateCount,
                origin: new Vector2(15, 15),
                scale: 1f,
                effects: SpriteEffects.None,
                layerDepth: 0); ;
            return false;
        }
        public static int Random(int rand)
        {
            int i = Main.rand.Next(rand);
            if (i == 0) return ModContent.ProjectileType<RBKnife>();
            else if (i == 1) return ModContent.ProjectileType<WVKnife>();
            else if (i == 2) return ModContent.ProjectileType<SKKnife>();
            else if (i == 3) return ModContent.ProjectileType<CSKnife>();
            else if (i == 4) return ModContent.ProjectileType<ABKnife>();
            else if (i == 5) return ModContent.ProjectileType<CBKnife>();
            else if (i == 6) return ModContent.ProjectileType<STKnife>();
            else return ModContent.ProjectileType<ASKnife>();
        }
        public void  RandomShoot(Player player, int cn, int rn, int lv)
        {
            for (int i = 0; i <= cn + Main.rand.Next(rn); i++)
            {
                Projectile proj =  Projectile.NewProjectileDirect(Projectile.GetSource_Death(), Projectile.Center,
                    (Main.rand.Next(360) * MathHelper.Pi / 180f).ToRotationVector2() * 20f,
                    Random(lv), (int)(Projectile.damage * (float)(1 + (int)player.GetCritChance(DamageClass.Melee) / 100)),
                    Projectile.knockBack, player.whoAmI, MKID);
                proj.CritChance = Projectile.CritChance;
            }
        }
    }
    public class MKboom : ModProjectile
    {
        public override string Texture => "MysteriousKnives/Pictures/Projectiles/Another/MKboom";
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
            Projectile.penetrate = 1;//穿透数量 -1无限
            Projectile.aiStyle = -1;//附带原版弹幕AI ID
            Projectile.alpha = 255;
            Main.projFrames[Projectile.type] = 1;//动画被分成几份
            base.SetDefaults();
        }
    }

    public class MKchannel : ModProjectile
    {
        public override string Texture => "MysteriousKnives/Pictures/Projectiles/Another/MKchannel";
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
            SoundEngine.PlaySound(new("MysteriousKnives/Sounds/Channel"));
        }
        public float dis = 0;
        public int d;
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
            if (dis > 10) dis = 10 * ((float)-Math.Cos(t / 1.25) / 10 + 1);
            if (!player.channel) Projectile.Kill();
            else
            {
                Projectile.timeLeft = 2;
                player.itemTime = 2;
                player.itemAnimation = 2;
                if (Main.GameUpdateCount % 3 == 0)
                {
                    float random = 1 + 0.01f * Main.rand.Next(-15, 15);
                    int damage = (int)(Projectile.damage * target.takenDamageMultiplier * 4.5f * random);
                    if (target.realLife != -1)
                        Main.npc[target.realLife].life -= damage;
                    else target.life -= damage;
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
                if (Main.dayTime) d = ModContent.DustType<HalloweenDust>();
                else if(!Main.dayTime) d = ModContent.DustType<RainbowDust>();
                for (int i = 0; i < 72; i++)
                {
                    Dust dust = Dust.NewDustDirect(target.Center, target.width, target.height, d);
                    dust.position = target.Center + new Vector2((int)(Math.Pow(-1, i)) * (float)Math.Cos(Math.PI / 36 * (i + t)),
                        (float)Math.Sin(Math.PI / 36 * (i + t))) * 10f * dis;
                    dust.velocity *= 0;
                }
            }
        }
    }

    public class RB_Ray : ModProjectile
    {
        public override string Texture => "MysteriousKnives/Pictures/Projectiles/Another/RB_Ray";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("回春光束");
        }
        public override void SetDefaults()
        {
            Projectile.width = 4;//宽
            Projectile.height = 4;//高
            Projectile.scale = 1f;//体积倍率
            Projectile.extraUpdates = 1;
            Projectile.timeLeft = 2;//存在时间60 = 1秒
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
            Projectile.timeLeft = 2;
            Player player = Main.player[Projectile.owner];
            Vector2 deflection = Vector2.Normalize(player.Center - Projectile.Center);
            Projectile.velocity = (Projectile.velocity + deflection) * 30 / 31f;
            if (Projectile.Hitbox.Intersects(player.Hitbox))
            {
                int i = (player.statLifeMax2 - player.statLife) / 20;
                player.statLife += i;
                player.HealEffect(i);
                Projectile.Kill();
            }
            for (int i = 0; i < 3; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height,
                    ModContent.DustType<RBDust>());
                dust.position = Projectile.Center - Projectile.velocity * i / 3f;
                dust.velocity *= 0;
            }
        }
    }
} 