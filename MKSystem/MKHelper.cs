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
                    if (list.Count > 0)
                    {
                        target = list.MinBy(t => t.dis).npc;
                    }
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
        public static NPC RealTarget(NPC target)
        {
            return target.realLife != -1 ? Main.npc[target.realLife] : target;
        }
        public static void NoSoundStrike(NPC target, int damage, Color color, bool largeText = true, bool dot = false)
        {
            if (target.life > damage)
            {
                target.life -= damage;
                if (target.realLife == -1)
                {
                    CombatText.NewText(new Rectangle((int)target.position.X + Main.rand.Next(-32, 32),
                    (int)target.Center.Y - Main.rand.Next(16, 32), target.width, target.height),
                    color, damage, largeText, dot);
                }
            }
            else
            {
                target.life = 1;
                target.StrikeNPC(damage, 0, 0);
            }
        }
        public static Item PlySelect(Player player)
        {
            return player.inventory[player.selectedItem];
        }
        public static Vector2 ChaseVel(Projectile proj, NPC target, float M)
        {
            Vector2 tarVel = Vector2.Normalize(target.Center - proj.Center) * M;
            float dis = Vector2.Distance(target.Center, proj.Center);
            float lerp = dis < 1000 ? dis / 1000 * 20 + 10 : 30;
            return (proj.velocity * lerp * (proj.MaxUpdates) + tarVel / proj.MaxUpdates)
                / (lerp * proj.MaxUpdates + 1);
        }
        public static float Sin(double t)
        {
            return (float)Math.Sin(t);
        }
        public static float Cos(double t)
        {
            return (float)Math.Cos(t);
        }
        /// <summary>
        /// 注意是一个PI
        /// </summary>
        /// <param name="precent"></param>
        /// <returns></returns>
        public static float ManyPI(float precent)
        {
            return (float)(Math.PI * precent);
        }
        public static Vector2 Circle(double value, double r)
        {
            return new Vector2(Cos(value), Sin(value)) * (float)r;
        }
        public static Asset<Texture2D> GetT2D(string fullName)
        {
            return ModContent.Request<Texture2D>($"MysteriousKnives/Pictures/{fullName}", AssetRequestMode.ImmediateLoad);
        }
        /// <summary>
        /// 左闭右开
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static bool Between(double value, double min, double max)
        {
            if (min > max)
            {
                throw new ArgumentException("min must be <= max");
            }
            return value >= min && value <= max;
        }
        /// <summary>
        /// Mod Item Type
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static int MIT(Mod mod, string name)
        {
            return mod.Find<ModItem>(name).Type;
        }
    }
}
