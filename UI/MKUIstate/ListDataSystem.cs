namespace MysteriousKnives.UI.UIStates
{
    public class ListDataSystem : ModSystem
    {
        public static List<Item> Itemlist = new();
        public static List<string> Funcslist = new();
        public static List<string> Funcsnonlist = new();
        public override void Load()
        {
            for (int i = 0; i < 100; i++)
            {
                Itemlist.Add(new Item(i, 1));
            }
            for (int i = 0; i < 10; i++)
            {
                Funcslist.Add($"功能键{i}");
            }
            for (int i = 0; i < 3; i++)
            {
                Funcsnonlist.Add($"未选键{i}");
            }
        }
        public override void Unload()
        {
            Itemlist = null;
        }
    }
}
