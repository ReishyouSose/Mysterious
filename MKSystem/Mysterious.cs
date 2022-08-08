namespace MysteriousKnives.MKSystem
{
    public class Mysterious : DamageClass
    {
        public override void SetDefaultStats(Player player)
        {
            ClassName.SetDefault("诡异伤害");
        }
        public override StatInheritanceData GetModifierInheritance(DamageClass damageClass)
        {
            return StatInheritanceData.Full;
        }
    }
}
