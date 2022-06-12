namespace MysteriousKnives.Items
{
    public abstract class MKprefix : ModPrefix
    {
        public class MKprefix01 : MKprefix
        {
            public override void SetStaticDefaults()
            {
                DisplayName.SetDefault("未知I");
            }
            public override void Apply(Item item)
            {
                item.value += (int)(item.value * 0.1f);
            }
            public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, 
                ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
            {
                damageMult += 0.15f;
                useTimeMult -= 0.1f;
                shootSpeedMult += 0.1f;
                critBonus += 10;
            }
        }
    }
}
