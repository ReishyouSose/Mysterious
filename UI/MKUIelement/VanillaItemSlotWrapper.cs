﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteriousKnives.UI.MKUIelement
{
	internal class VanillaItemSlotWrapper : UIElement
	{
		internal Item Item;
		private readonly int _context;
		private readonly float _scale;
		internal Func<Item, bool> ValidItemFunc;

		public VanillaItemSlotWrapper(int context = ItemSlot.Context.BankItem, float scale = 1f)
		{
			_context = context;
			_scale = scale;
			Item = new Item();
			Item.SetDefaults(0);

			Width.Set(TextureAssets.InventoryBack9.Width() * scale, 0f);
			Height.Set(TextureAssets.InventoryBack9.Height() * scale, 0f);
		}
        protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			float oldScale = Main.inventoryScale;
			Main.inventoryScale = _scale;
			Rectangle rectangle = GetDimensions().ToRectangle();

			if (ContainsPoint(Main.MouseScreen) && !PlayerInput.IgnoreMouseInterface)
			{
				Main.LocalPlayer.mouseInterface = true;
				if (ValidItemFunc == null || ValidItemFunc(Main.mouseItem))
				{
					// Handle handles all the click and hover actions based on the context.
					ItemSlot.Handle(ref Item, _context);
				}
			}
			ItemSlot.Draw(spriteBatch, ref Item, _context, rectangle.TopLeft());
			Main.inventoryScale = oldScale;
		}
        public override void Update(GameTime gameTime)
        {
			UpdateGiveBuff(Item);
            base.Update(gameTime);
        }
		public static void DrawName(Vector2 position, Item item)
        {
			SpriteBatch spriteBatch = Main.spriteBatch;
			ChatManager.DrawColorCodedString(spriteBatch, FontAssets.MouseText.Value, item.Name, position, Color.White,
				0f, new Vector2(0, 0), new Vector2(1f));

        }
        public static void UpdateGiveBuff(Item item)
        {
			Player player = Main.LocalPlayer;
			if (item.buffType != -1 && Main.debuff[item.buffType] == false)
            {
				player.AddBuff(item.buffType, 2);
            }
			else
            {
				Rectangle rec = player.getRect();
				item.TurnToAir();
				Item.NewItem(item.GetSource_Loot(), rec, item.type, item.stack);
            }
        }
    }
}
