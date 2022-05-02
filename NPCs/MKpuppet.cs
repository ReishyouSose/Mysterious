using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace MysteriousKnives.NPCs
{
    public class MKpuppet : ModNPC
    {
        public override string Texture => "MysteriousKnives/NPCs/pictures/MKpuppet";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("僵尸傀儡");
        }
        public override void SetDefaults()
        {
            NPC.width = 34;
            NPC.height = 162 / 3;
            NPC.damage = 0;
            NPC.lifeMax = 2100000000;
            NPC.defense = 0;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = -3;
            NPC.friendly = false;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath6;
            NPC.value = Item.buyPrice(0, 0, 15, 0);
            Main.npcFrameCount[NPC.type] = 3;
            NPC.noGravity = true;
            NPC.noTileCollide = false;
            AIType = 0;//这边为了方便直接写了ID（绝对不是懒
            AnimationType = 0;
            NPC.boss = false;
            base.SetDefaults();
        }

        public override void AI()
        {
            NPC.velocity = new Vector2(0,0);
            Lighting.AddLight(NPC.Center, 1f, 1f, 1f);
            base.AI();
        }
    }
}
