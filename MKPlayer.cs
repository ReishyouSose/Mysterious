namespace MysteriousKnives
{
    public class MKPlayer : ModPlayer
    {
        public override void OnEnterWorld(Player player)
        {
            MKPotionStationUI.enable = false;
        }
    }
}
