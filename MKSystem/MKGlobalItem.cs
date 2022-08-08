namespace MysteriousKnives.MKSystem
{
    public class MKGlobalItem : GlobalItem
    {
        public static Item PlySelect(Player player)
        {
            return player.inventory[player.selectedItem];
        }
    }
}
