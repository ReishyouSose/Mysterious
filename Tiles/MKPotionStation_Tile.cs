using MysteriousKnives.Tiles._TileEntity;
using Terraria.ID;

namespace MysteriousKnives.Tiles
{
	public class MKPotionStation_Tile : ModTile
	{
		public override string Texture => "MysteriousKnives/Pictures/Tiles/灌注站";
        public override string HighlightTexture => "MysteriousKnives/Pictures/Tiles/灌注站高亮";
        public override void SetStaticDefaults()
		{
			// Properties
			Main.tileSolid[Type] = false;//物块是否实体
			Main.tileSolidTop[Type] = false;//物块顶部实体
			Main.tileSpelunker[Type] = true;//是否被洞穴探险荧光棒捕捉
			Main.tileContainer[Type] = true;//是否是容器
			Main.tileNoAttach[Type] = true;//不可在旁边放物块
			Main.tileTable[Type] = false;//是否视为桌子
			Main.tileLavaDeath[Type] = false;//是否被岩浆破坏
			Main.tileFrameImportant[Type] = true;//是否帧对齐
			Main.tileBlockLight[Type] = false;//是否阻挡光传播
			TileID.Sets.HasOutlines[Type] = true;//是否含有边缘高亮线
			Main.tileShine2[Type] = true;
			Main.tileShine[Type] = 1200;
			Main.tileNoAttach[Type] = true;
			Main.tileOreFinderPriority[Type] = 500;
			//TileID.Sets.BasicChest[Type] = true;
			//TileID.Sets.DisableSmartCursor[Type] = true;

			DustType = MKDustID.RainbowDust;
			AdjTiles = new int[] { TileID.Containers };
			ChestDrop = MKItemsID.Station;

			//给物块命名
			ContainerName.SetDefault("诡秘灌注站");

			ModTranslation name = CreateMapEntryName();
			name.SetDefault("诡秘灌注站");
			AddMapEntry(new Color(200, 200, 200), name);

			// Placement
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);//模板
			TileObjectData.newTile.StyleHorizontal = true;//横向读取贴图
			TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 16 };//高度数组
			TileObjectData.newTile.Origin = new Point16(0, 1);//放置相对鼠标
			//TileObjectData.newTile.HookCheckIfCanPlace = new PlacementHook(Chest.FindEmptyChest, -1, 0, true);
			//TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(Chest.AfterPlacement_Hook, -1, 0, false);
			//TileObjectData.newTile.AnchorInvalidTiles = new int[] { TileID.MagicalIceBlock };
			TileObjectData.newTile.HookPostPlaceMyPlayer = 
				new PlacementHook(ModContent.GetInstance<Potion>().Hook_AfterPlacement, -1, 0, false);
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop 
				| AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
			TileObjectData.addTile(Type);
		}
        public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
        {
            return true;
        }
        public override ushort GetMapOption(int i, int j) => (ushort)(Main.tile[i, j].TileFrameX / 54);
		public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) => true;
		
		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = 1;
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Point16 origin = GetTileOrigin(i, j);
			ModContent.GetInstance<Potion>().Kill(origin.X, origin.Y);
			Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 54, 54, ChestDrop);
			//Chest.DestroyChest(i, j);
		}

		public override bool RightClick(int i, int j)
		{
			Player player = Main.LocalPlayer;
			Main.mouseRightRelease = false;

			if (player.sign >= 0)
			{
				SoundEngine.PlaySound(SoundID.MenuClose);
				player.sign = -1;
				Main.editSign = false;
				Main.npcChatText = "";
			}

			if (Main.editChest)
			{
				SoundEngine.PlaySound(SoundID.MenuTick);
				Main.editChest = false;
				Main.npcChatText = "";
			}
			
			if (player.editedChestName)
			{
				NetMessage.SendData(MessageID.SyncPlayerChest, -1, -1, NetworkText.FromLiteral(Main.chest[player.chest].name), player.chest, 1f);
				player.editedChestName = false;
			}
			//右键这个物块打开UI
			MKPotionStationUI.enable = !MKPotionStationUI.enable;
			Main.playerInventory = true;
			if (!MKPotionStationUI.enable) SoundEngine.PlaySound(SoundID.MenuOpen);
			return true;
		}
		public override void MouseOver(int i, int j)
		{
			Player player = Main.LocalPlayer;
			player.noThrow = 2;
			player.cursorItemIconEnabled = true;
			player.cursorItemIconID = MKItemsID.Station;
		}
        public override void NearbyEffects(int i, int j, bool closer)
        {
			Player player = Main.LocalPlayer;
            if (Vector2.Distance(player.position / 16, new Vector2(i,j)) > 18)
            {
                if (MKPotionStationUI.enable)
                {
					MKPotionStationUI.enable = false;
					SoundEngine.PlaySound(SoundID.MenuClose);
				}
            }
        }
    }
}
