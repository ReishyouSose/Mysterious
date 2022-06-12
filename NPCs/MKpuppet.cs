namespace MysteriousKnives.NPCs
{
    public class MKpuppet : ModNPC
    {
        public override string Texture => "MysteriousKnives/Pictures/NPCs/MKpuppet";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("诡秘侍者");
            Main.npcFrameCount[NPC.type] = 3;
        }
        public override void SetDefaults()
        {
            NPC.width = 34;
            NPC.height = 162 / 3;
            NPC.lifeMax = 50000000;
            NPC.defense = 0;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = -1;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath6;
            NPC.value = Item.buyPrice(0, 0, 0, 0);
            Main.npcFrameCount[NPC.type] = 3;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            AIType = -3;//这边为了方便直接写了ID（绝对不是懒
            AnimationType = -3;
            NPC.rarity = -1;
            NPC.boss = false;
        }
        public int i = 0;
        public override void FindFrame(int frameHeight)
        {
            {
                NPC.frameCounter++;
                if (NPC.frameCounter >= 5)
                {
                    NPC.frameCounter = 0;
                    i++;
                    i %= 3;
                    NPC.frame.Y = frameHeight * i;
                }
            }
        }
        public override void AI()
        {
            NPC.velocity = new Vector2((float)Math.Sin(Main.GameUpdateCount * 0.05f), 0);
            if (NPC.velocity.X > 0) NPC.spriteDirection = -1;
            if (NPC.velocity.X < 0) NPC.spriteDirection = 1;
            Lighting.AddLight(NPC.Center, 1f, 1f, 1f);
            foreach (NPC boss in Main.npc)
            if (boss.boss && boss.active)
            {
                foreach (NPC npc in Main.npc)
                    if (npc.type == ModContent.NPCType<MKpuppet>())
                        npc.life = 0;
                break;
            }
        }/*
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule);
        }*/
    }
}
