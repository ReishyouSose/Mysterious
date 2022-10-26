namespace MysteriousKnives.Pictures.Projectiles
{
    public class MKGproj : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public override bool PreAI(Projectile projectile)
        {
            //MysteriousKnives.draw = false;
            return base.PreAI(projectile);
        }
    }
}
