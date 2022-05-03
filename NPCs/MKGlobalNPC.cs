using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace MysteriousKnives.NPCs
{
    public class MKGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public bool SK1;
        public bool SK2;
        public bool SK3;
        public bool AB1;
        public bool AB2;
        public bool AB3;
        public bool AB4;
        public bool AB5;
        public bool AB6;
        public bool AB7;

        public override void ResetEffects(NPC npc)
        {
            SK1 = false;
            SK2 = false;
            SK3 = false;
            AB1 = false;
            AB2 = false;
            AB3 = false;
            AB4 = false;
            AB5 = false;
            AB6 = false;
            AB7 = false;
            base.ResetEffects(npc);
        }
        public override void ModifyHitPlayer(NPC npc, Player target, ref int damage, ref bool crit)
        {
            if (SK1) damage -= (int)(damage * 0.1f);
            if (SK2) damage -= (int)(damage * 0.2f);
            if (SK3) damage -= (int)(damage * 0.4f);
            base.ModifyHitPlayer(npc, target, ref damage, ref crit);
        }
        public override void ModifyHitNPC(NPC npc, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            if (SK1) damage -= (int)(damage * 0.1f);
            if (SK2) damage -= (int)(damage * 0.2f);
            if (SK3) damage -= (int)(damage * 0.4f);
            base.ModifyHitNPC(npc, target, ref damage, ref knockback, ref crit);
        }
        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            if (AB1) damage += (int)(damage * 0.05f);
            if (AB2) damage += (int)(damage * 0.1f);
            if (AB3) damage += (int)(damage * 0.15f);
            if (AB4) damage += (int)(damage * 0.2f);
            if (AB5) damage += (int)(damage * 0.25f);
            if (AB6) damage += (int)(damage * 0.3f);
            if (AB7) damage += (int)(damage * 0.5f);
            base.ModifyHitByItem(npc, player, item, ref damage, ref knockback, ref crit);
        }
        public override void ModifyHitByProjectile(NPC npc, Projectile Projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (AB1) damage += (int)(damage * 0.05f);
            if (AB2) damage += (int)(damage * 0.1f);
            if (AB3) damage += (int)(damage * 0.15f);
            if (AB4) damage += (int)(damage * 0.2f);
            if (AB5) damage += (int)(damage * 0.25f);
            if (AB6) damage += (int)(damage * 0.3f);
            if (AB7) damage += (int)(damage * 0.5f);
            base.ModifyHitByProjectile(npc, Projectile, ref damage, ref knockback, ref crit, ref hitDirection);
        }
    }
}
