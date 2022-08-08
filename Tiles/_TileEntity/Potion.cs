namespace MysteriousKnives.Tiles._TileEntity
{
    public class Potion : ModTileEntity
	{
		public Item[] items = new Item[10];
		public Potion()
        {
			for (int i = 0; i < 10; i++)
            {
				items[i] = new Item();
            }
        }
		public override bool IsTileValidForEntity(int x, int y)
		{
			Tile tile = Main.tile[x, y];
			return tile.HasTile && tile.TileType == MKTileID.PotionStation && tile.TileFrameX == 0 && tile.TileFrameY == 0;
		}
		public override int Hook_AfterPlacement(int i, int j, int type, int style, int direction, int alternate)
		{ 
			if (Main.netMode == NetmodeID.MultiplayerClient)
			{
				//Sync the entire multitile's area.  Modify "width" and "height" to the size of your multitile in tiles
				int width = 3;
				int height = 3;
				NetMessage.SendTileSquare(Main.myPlayer, i, j, width, height);

				//Sync the placement of the tile entity with other clients
				//The "type" parameter refers to the tile type which placed the tile entity, so "Type" (the type of the tile entity) needs to be used here instead
				NetMessage.SendData(MessageID.TileEntityPlacement, -1, -1, null, i, j, Type);
			}
			//ModTileEntity.Place() handles checking if the entity can be placed, then places it for you
			//Set "tileOrigin" to the same value you set TileObjectData.newTile.Origin to in the ModTile
			Point16 tileOrigin = new(1, 1);
			int placedEntity = Place(i - tileOrigin.X, j - tileOrigin.Y);
			return placedEntity;
			#if COMPILE_ERROR
									
			#endif
		}

        public override void OnNetPlace()
		{
			if (Main.netMode == NetmodeID.Server)
			{
				NetMessage.SendData(MessageID.TileEntitySharing, -1, -1, null, ID, Position.X, Position.Y);
			}
		}
        public override void SaveData(TagCompound tag)
        {
            //tag["Item"] = VanillaItemSlotWrapper.Item;
        }
        public override void LoadData(TagCompound tag)
        {
			//VanillaItemSlotWrapper.Item = (Item)tag["Item"];
        }
    }
}
