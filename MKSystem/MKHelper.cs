namespace MysteriousKnives.MKSystem
{
    public class MKHelper
    {
        public static void ChangeSpb(BlendState blendState)
        {
            SpriteBatch sb = Main.spriteBatch;
            sb.End();
            sb.Begin(SpriteSortMode.Deferred, blendState, SamplerState.AnisotropicClamp, DepthStencilState.None,
                RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
        }
        public static NPC ChooseTarget(Projectile proj, bool NearMouse = false, float MaxDis = 2000)
        {
            NPC target = null;
            var list = new List<(NPC npc, float dis)>();
            foreach (NPC npc in Main.npc)
            {
                if (NearMouse)
                {
                    if (npc.CanBeChasedBy())
                    {
                        list.Add((npc, Vector2.Distance(npc.Center, Main.MouseWorld)));
                    }
                    target = list.MinBy(t => t.dis).npc;
                }
                else
                {
                    float dis = Vector2.Distance(proj.Center, npc.Center);
                    if (npc.CanBeChasedBy() && dis <= MaxDis)
                    {
                        MaxDis = dis;
                        target = npc;
                    }
                }
            }
            if (target != null)
            {
                target = RealTarget(target);
            }
            return target;
        }
        public static NPC RealTarget(NPC npc)
        {
            return npc.realLife != -1 ? Main.npc[npc.realLife] : npc;
        }
        public static void NoSoundStrike(NPC target, int damage, Color color)
        {
            if (target.life > damage)
            {
                target.life -= damage;
                if (target.realLife == -1)
                {
                    CombatText.NewText(new Rectangle((int)target.position.X + Main.rand.Next(-32, 32),
                    (int)target.Center.Y - Main.rand.Next(16, 32), target.width, target.height),
                    color, damage, true);
                }
            }
            else target.StrikeNPC(damage + 1, 0, 0);
        }
        public static Item PlySelect(Player player)
        {
            return player.inventory[player.selectedItem];
        }
    }
}
