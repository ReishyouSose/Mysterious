using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using MysteriousKnives.Projectiles;

namespace MysteriousKnives.NPCs
{
    public class MKpuppet : ModNPC
    {
        public override string Texture => "MysteriousKnives/Pictures/NPCs/MKpuppet";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("诡秘侍者");
            Main.npcFrameCount[Type] = 3;
        }
        public override void SetDefaults()
        {
            NPC.width = 34;
            NPC.height = 162 / 3;
            NPC.damage = 100;
            NPC.lifeMax = 21000000;
            NPC.defense = 0;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = -1;
            NPC.friendly = false;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath6;
            NPC.value = Item.buyPrice(0, 0, 0, 0);
            Main.npcFrameCount[NPC.type] = 3;
            NPC.noGravity = true;
            NPC.noTileCollide = false;
            AIType = 3;//这边为了方便直接写了ID（绝对不是懒
            AnimationType = -1;
            NPC.rarity = -1;
            NPC.boss = false;
        }
        public override void AI()
        {
            NPC.velocity *= 0;
            Lighting.AddLight(NPC.Center, 1f, 1f, 1f);
            foreach (NPC boss in Main.npc)
            if (boss.boss && boss.active)
            foreach (NPC npc in Main.npc)
            if (npc.type == ModContent.NPCType<MKpuppet>())
                npc.life = 0;
        }
    }
}
