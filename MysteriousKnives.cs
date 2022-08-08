using Terraria.ID;

namespace MysteriousKnives
{
	public class MysteriousKnives : Mod
	{
		public override void Load()
		{
			On.Terraria.Main.GUIChatDrawInner += Main_GUIChatDrawInner;
			On.Terraria.Player.HasUnityPotion += Player_HasUnityPotion;
		}
		public override void Unload()
		{
			On.Terraria.Main.GUIChatDrawInner -= Main_GUIChatDrawInner;
			On.Terraria.Player.HasUnityPotion -= Player_HasUnityPotion;
		}
		public static bool Player_HasUnityPotion(On.Terraria.Player.orig_HasUnityPotion orig, Player self)
        {
			for (int i = 0; i < 58; i++)
			{
				if (self.inventory[i].type == ItemID.WormholePotion && self.inventory[i].stack > 0)
				{
					return true;
				}
				if (self.inventory[i].type == MKItemsID.MKOrigin) return true;
			}
			return false;
		}
		public override uint ExtraPlayerBuffSlots => 999;
        public static object TextDisplayCache => typeof(Main).GetField("_textDisplayCache",
		System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(Main.instance);
		public bool hover = false;
		public bool MouseLeft = false;
		public static float GetButtonPosition(int num, bool happy = false)
		{
			int i = 180;
			if (happy) i += 10;
			float dis = ChatManager.GetStringSize(FontAssets.MouseText.Value, Language.GetTextValue("LegacyInterface.64"), new(0.9f)).X;
			float postion = i + (Main.screenWidth - 800) / 2 + (num - 1) * (dis + 30);
			return postion;
		}
		public string text = Language.GetTextValue("LegacyInterface.28");
		public int num1;
		public void Main_GUIChatDrawInner(On.Terraria.Main.orig_GUIChatDrawInner orig, Main self)
		{
			orig(self);
			NPC npc = Main.npc[Main.player[Main.myPlayer].talkNPC];
			if (npc.type == NPCID.TownCat || npc.type == NPCID.TownDog || npc.type == NPCID.TownBunny)
			{
				DynamicSpriteFont font = FontAssets.MouseText.Value;
				string focusText = Language.GetTextValue("LegacyInterface.28");
				int numLines = (int)TextDisplayCache.GetType().GetProperty("AmountOfLines",
					System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public).GetValue(TextDisplayCache) + 1;
				Vector2 scale = new(0.9f);
				Vector2 stringSize = ChatManager.GetStringSize(font, Language.GetTextValue("LegacyInterface.51"), scale);
				Vector2 vector = new(1f);
				if (stringSize.X > 260f) vector.X *= 260f / stringSize.X;

				float button = GetButtonPosition(3, false);
				Vector2 position = new(button, 130 + numLines * 30);
				stringSize = ChatManager.GetStringSize(font, focusText, scale);
				if (Main.MouseScreen.Between(position, position + stringSize * scale * vector.X) && !PlayerInput.IgnoreMouseInterface)
				{
					Main.LocalPlayer.mouseInterface = true;
					Main.LocalPlayer.releaseUseItem = false;
					scale *= 1.2f;
					if (!hover) SoundEngine.PlaySound(SoundID.MenuTick);
					hover = true;
				}
				else
				{
					if (hover) SoundEngine.PlaySound(SoundID.MenuTick);
					hover = false;
				}

				int MTC = Main.mouseTextColor;
				Color unhoverColor = new(MTC, MTC, MTC / 2, MTC);
				Color hoveringColor = new(228, 206, 114, Main.mouseTextColor / 2);
				ChatManager.DrawColorCodedStringShadow(Main.spriteBatch, font, focusText, position + stringSize * vector * 0.5f,
					!hover ? Color.Black : Color.Brown, 0f, stringSize * 0.5f, scale * vector);
				ChatManager.DrawColorCodedString(Main.spriteBatch, font, focusText, position + stringSize * vector * 0.5f,
					!hover ? unhoverColor : hoveringColor, 0f, stringSize * 0.5f, scale * vector);

				if (Main.MouseScreen.Between(position, position + stringSize * scale * vector.X) && !PlayerInput.IgnoreMouseInterface)
				{
					if (Main.mouseLeft) MouseLeft = true;
					if (MouseLeft && !Main.mouseLeft)
					{
						Main.playerInventory = true;
						Main.npcChatText = "";
						SoundEngine.PlaySound(SoundID.MenuTick);
						Player player = Main.player[Main.myPlayer];
						Chest shop = Main.instance.shop[98];
						if (npc.type == NPCID.TownCat)
                        {
							Main.SetNPCShopIndex(98);
							int nextSlot = 0;
							bool sell = true;
							if (player.HasItem(ItemID.AngryBonesBanner))
                            {

                            }
							if (NPC.killCount[419] >= 5 || sell)//∫£µ¡¥¨
							{
								shop.item[nextSlot].SetDefaults(905);//«Æ±“«π
								nextSlot++;
								shop.item[nextSlot].SetDefaults(855);//–“‘À±“
								nextSlot++;
								shop.item[nextSlot].SetDefaults(2584);//∫£µ¡∑®’»
								nextSlot++;
							}
							if (NPC.killCount[50] >= 1 || sell)// ∑¿≥ƒ∑Õı
							{
								shop.item[nextSlot].SetDefaults(1309);// ∑¿≥ƒ∑∑®’»
								nextSlot++;
							}
							if (NPC.killCount[120] >= 50 || sell)//ªÏ„Áæ´
							{
								shop.item[nextSlot].SetDefaults(1326);//¡—Œª
								nextSlot++;
							}
							for (int i = 269; i <= 280; i++)
							{
								num1 += NPC.killCount[i];
							}
							if (num1 >= 50 || sell)//…˙–‚◊∞º◊˜º˜√
							{
								shop.item[nextSlot].SetDefaults(1517);//π«÷Æ”
								nextSlot++;
								shop.item[nextSlot].SetDefaults(1183);//—˝¡È∆ø
								nextSlot++;
							}
							if (NPC.killCount[253] >= 50 || sell)//À¿…Ò
							{
								shop.item[nextSlot].SetDefaults(1327);//À¿…Ò¡≠µ∂
								nextSlot++;
							}
						}
						else if (npc.type == NPCID.TownDog)
                        {
							Main.SetNPCShopIndex(98);
							int nextSlot = 0;
							shop.item[nextSlot].SetDefaults(ItemID.TerraBlade);
							shop.item[nextSlot].value = 1000;
						}
						else if (npc.type == NPCID.TownBunny)
                        {
							Main.SetNPCShopIndex(98);
							int nextSlot = 0;
							shop.item[nextSlot].SetDefaults(ItemID.LastPrism);
							shop.item[nextSlot].value = 1000;
						}
						MouseLeft = false;
					}
				}
			}
		}
	}
}