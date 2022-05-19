using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace MysteriousKnives.Items
{
    public class ABRare : ModRarity
    {
        public override Color RarityColor => new(0.33f, 0.33f, 0.33f);
    }
    public class ASRare : ModRarity
    {
        public override Color RarityColor => new(0.45f, 0.04f, 0.75f);
    }
    public class CBRare : ModRarity
    {
        public override Color RarityColor => new(1f, 0.39f, 0.22f);
    }
    public class RBRare : ModRarity
    {
        public override Color RarityColor => new(0.2f, 0.95f, 0.13f);
    }
    public class WVRare : ModRarity
    {
        public override Color RarityColor => new(0.55f, 0.7f, 0.13f);
    }
    public class CSRare : ModRarity
    {
        public override Color RarityColor => new(0.9f, 0.63f, 1f);
    }
    public class STRare : ModRarity
    {
        public override Color RarityColor => new(1f, 0.95f, 0.75f);
    }
    public class SKRare : ModRarity
    {
        public override Color RarityColor => new(0.29f, 0.37f, 0.88f);
    }


    public class Rare_Gray : ModRarity
    {
        public override Color RarityColor => new(0.66f, 0.66f, 0.66f);
    }
    public class Rare_White : ModRarity
    {
        public override Color RarityColor => new(0.9f, 0.9f, 0.9f);
    }
    public class Rare_Green : ModRarity
    {
        public override Color RarityColor => new(87, 231, 76);
    }
    public class Rare_Blue : ModRarity
    {
        public override Color RarityColor => new(50, 106, 233);
    }
    public class Rare_Purple : ModRarity
    {
        public override Color RarityColor => new(145, 50, 233);
    }
    public class Rare_Pink : ModRarity
    {
        public override Color RarityColor => new(240, 150, 246);
    }
    public class Rare_Orange : ModRarity
    {
        public override Color RarityColor => new(255, 150, 0);
    }
    public class Rare_Gold : ModRarity
    {
        public override Color RarityColor => new(239, 225, 64);
    }
    public class Rare_Red : ModRarity
    {
        public override Color RarityColor => new(255, 0, 0);
    }
    public class Rare_Ranbow : ModRarity
    {
        public override Color RarityColor => Main.DiscoColor;
    }
}
