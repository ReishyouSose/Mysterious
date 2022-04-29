using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MysteriousKnives.Dusts
{
	public abstract class MDust : ModDust
    {
		public override void OnSpawn(Dust dust)
		{
			dust.noLight = false;
			dust.scale = 1.1f;
			dust.noGravity = true;
			dust.velocity /= 5f;
			dust.alpha = 0;
		}
        public override bool Update(Dust dust)
        {
			dust.position += dust.velocity;
			dust.rotation += dust.velocity.X;
			dust.scale -= 0.01f;
			if (dust.scale < 0.5f)
			{
				dust.active = false;
			}
			return base.Update(dust);
        }

		/// <summary>
		/// 结晶粒子0.9f, 0.63f, 1f
		/// </summary>
		public class CSDust : MDust
		{
			public override string Texture => "MysteriousKnives/Dusts/pictures/CSDust";
			public override void OnSpawn(Dust dust)
            {
                base.OnSpawn(dust);
            }
            public override bool Update(Dust dust)
			{
				base.Update(dust);
				Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 0.9f, 0.63f, 1f);
				return false;
			}
        }
		/// <summary>
		/// 沉沦粒子0.29f, 0.37f, 0.88f
		/// </summary>
		public class SKDust : MDust
		{
			public override string Texture => "MysteriousKnives/Dusts/pictures/SKDust";
			public override void OnSpawn(Dust dust)
			{
				base.OnSpawn(dust);
			}
			public override bool Update(Dust dust)
			{
				base.Update(dust);
				Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 0.29f, 0.37f, 0.88f);
				return false;
			}
		}
		/// <summary>
		/// 凝爆粒子1f, 0.39f, 0.22f
		/// </summary>
		public class CBDust : MDust
		{
			public override string Texture => "MysteriousKnives/Dusts/pictures/CBDust";
			public override bool Update(Dust dust)
			{
				base.Update(dust);
				Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 1f, 0.39f, 0.22f);
				return false;
			}
		}
		/// <summary>
		/// 深渊粒子0,0,0
		/// </summary>
		public class ABDust : MDust
		{
			public override string Texture => "MysteriousKnives/Dusts/pictures/ABDust";
			public override bool Update(Dust dust)
			{
				base.Update(dust);
				Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 0f, 0f, 0f);
				return false;
			}
		}
		/// <summary>
		/// 星辉粒子0.45f, 0.04f, 0.75f
		/// </summary>
		public class ASDust : MDust
		{
			public override string Texture => "MysteriousKnives/Dusts/pictures/ASDust";
			public override bool Update(Dust dust)
			{
				base.Update(dust);
				Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 0.45f, 0.04f, 0.75f);
				return false;
			}
		}
		/// <summary>
		/// 筋力粒子1f, 0.95f, 0.75f
		/// </summary>
		public class STDust : MDust
		{
			public override string Texture => "MysteriousKnives/Dusts/pictures/STDust";
			public override bool Update(Dust dust)
			{
				base.Update(dust);
				Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 1f, 0.95f, 0.75f);
				return false;
			}
		}
		/// <summary>
		/// 回春粒子0.2f, 0.95f, 0.13f
		/// </summary>
		public class RBDust : MDust
		{
			public override string Texture => "MysteriousKnives/Dusts/pictures/RBDust";
			public override bool Update(Dust dust)
			{
				base.Update(dust);
				Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 0.2f, 0.95f, 0.13f);
				return false;
			}
		}
		/// <summary>
		/// 诡毒粒子0.55f, 0.7f, 0.13f
		/// </summary>
		public class WVDust : MDust
		{
			public override string Texture => "MysteriousKnives/Dusts/pictures/WVDust";
			public override bool Update(Dust dust)
			{
				base.Update(dust);
				Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 0.55f, 0.7f, 0.13f);
				return false;
			}
		}
		/// <summary>
		/// 彩色粒子
		/// </summary>
		public class RanbowDust : MDust
		{
			public override string Texture => "MysteriousKnives/Dusts/pictures/RanbowDust";
			public override void OnSpawn(Dust dust)
			{
				dust.noGravity = true;
				dust.color = Main.DiscoColor;
				base.OnSpawn(dust);
			}
			public override bool Update(Dust dust)
			{
				float i = 0.001f; i += i;
				if(dust.noGravity == false )dust.velocity.Y += 0.2f + i;
				dust.rotation += dust.velocity.Y;
				dust.velocity *= 0.9f;
				base.Update(dust);
				Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 
					Main.DiscoR / 255f, Main.DiscoG / 255f, Main.DiscoB / 255f);
				//Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 0.9f, 0.63f, 1f);
				return false;
			}
		}
	}
}

