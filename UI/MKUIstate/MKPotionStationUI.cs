using Humanizer;
using MysteriousKnives.UI.MKUIPanel;
using System.Collections.Generic;

namespace MysteriousKnives.UI.MKUIstate
{
    public class MKPotionStationUI : UIState
	{
		internal static bool enable = false;
		public DragableUIPanel MainPanel;
		public ScrollView Menuview;
		public const int maxslot = 60;
		internal static VanillaItemSlotWrapper[] slots = new VanillaItemSlotWrapper[maxslot];
		public UIHoverImageButton stack, sort;
		public override void OnInitialize()
		{
			MainPanel = new();
			MainPanel.Width.Set(620, 0);
			MainPanel.Height.Set(250, 0);
			MainPanel.HAlign = 0.5f;
			MainPanel.VAlign = 0.2f;
			Append(MainPanel);

			Menuview = new(520, 210);
			Menuview.VAlign = 0.5f;
			MainPanel.Append(Menuview);

			Menuview.RemoveAllElement();
			int x = 0, y = -1;
			for (int i = 0; i < maxslot; i++)
            {
				x++;
                if (i % 10 == 0) { x = 0; y++; }
				slots[i] = new(scale: 0.75f);
				slots[i].Left.Set(Menuview.ScrollList.WidthInside() * x, 0f);
				slots[i].Top.Set(45 * y, 0f);
				Menuview.AppendElement(slots[i]);
			}

			sort = new(ModContent.Request<Texture2D>("MysteriousKnives/Pictures/UI/Sort_0"), "整理药水");
			sort.Width.Set(30, 0);
			sort.Height.Set(30, 0);
			sort.Left.Pixels = 560f;
			sort.VAlign = 0.5f;
			sort.OnClick += Sort_OnClick;
			MainPanel.Append(sort);
		}
		public static void Sort_OnClick(UIMouseEvent evt, UIElement listeningElement)
        {
			Item item;
			SortedDictionary<int, int> sortlist = new();
			List<Item> sortlist1 = new();
			for(int i = 0; i < maxslot; i++)
            {
				if (!slots[i].item.IsAir)
                {
					item = slots[i].item;
					if (sortlist.ContainsKey(item.type))
					{
						sortlist[item.type] += item.stack;
					}
					else
					{
						sortlist[item.type] = item.stack;
					}
					slots[i].item = new();
                }
            }
			for (int i = 0; i < 50; i++)
            {
				item = Main.LocalPlayer.inventory[i];
				if (item.buffType > 0)
                {
					if (sortlist.ContainsKey(item.type))
                    {
						sortlist[item.type]+= item.stack;
                    }
					else
                    {
						sortlist[item.type] = item.stack;
                    }
					Main.LocalPlayer.inventory[i].type = new();
                }
            }
			var list = new List<(int itemtype, int itemstack)>();
			foreach (KeyValuePair<int, int> pair in sortlist)
            {
				list.Add((pair.Key, pair.Value));
			}
            list = list.SelectMany(t =>
            {
                var collect = new List<(int itemType, int itemStack)>();
                var count = t.itemstack;
                var maxStack = new Item(t.itemtype).maxStack;
                while (count > maxStack)
                {
                    count -= maxStack;
                    collect.Add((t.itemtype, maxStack));
                }
                collect.Add((t.itemtype, count));
                return collect;
            }).ToList();
			foreach ((int, int) ele in list)
            {
				if (ele == (0, 0))
                {
					list.Remove(ele);
                }
            }
			for (int i = 0; i < list.Count; i++)
            {
				slots[i].item.SetDefaults(list[i].itemtype);
				slots[i].item.stack = list[i].itemstack;
            }
			SoundEngine.PlaySound(SoundID.Grab);
		}
		public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
			if (sort.IsMouseHovering)
			{
				sort.SetImage(ModContent.Request<Texture2D>("MysteriousKnives/Pictures/UI/Sort_1"));
			}
			else sort.SetImage(ModContent.Request<Texture2D>("MysteriousKnives/Pictures/UI/Sort_0"));
			foreach (VanillaItemSlotWrapper slot in slots)
            {
				if (slot.item.buffType != 0)
                {
					Main.player[Main.myPlayer].AddBuff(slot.item.buffType, 2);
                }
            }
			/*
			if (dragging)
			{
				bg.SetPos(Main.mouseX - offset.X, Main.mouseY - offset.Y);
			}

			// 判断鼠标是否在面板内
			if (bg.IsMouseHovering)
			{
				Main.LocalPlayer.mouseInterface = true;
			}

			// 当屏幕大小发生改变时候刷新面板
			if (screenWidth != Main.screenWidth || screenHeight != Main.screenHeight)
			{
				screenWidth = Main.screenWidth;
				screenHeight = Main.screenHeight;
				bg.SetPos(screenWidth / 2f - bg.Width.Pixels / 2f, screenHeight / 2f - bg.Height.Pixels / 2f);
			}
			/*if (!addslot.Item.IsAir && addslot.Item.buffType != -1)
            {
				MKPlayer.bufflist.Add((addslot.Item.buffType, Lang.GetBuffName(addslot.Item.buffType)));
				addslot.Item.TurnToAir();
			}
			if (MKPlayer.bufflist.Count > 0)
			{
				if (!delslot.Item.IsAir)
				{
					for (int i = 0; i < MKPlayer.bufflist.Count; i++)
                    {
						if (delslot.Item.buffType == MKPlayer.bufflist[i].bufftype)
                        {
							MKPlayer.bufflist.RemoveAt(i);
							delslot.Item.TurnToAir();
							break;
                        }
                    }
				}
				MKPlayer.bufflist = MKPlayer.bufflist.DistinctBy(t => t.bufftype).ToList();
				var result = string.Join("\n", MKPlayer.bufflist.Select(t => t.buffname));
				list.SetText($"{result}");
			}*/
		}
	}
}
