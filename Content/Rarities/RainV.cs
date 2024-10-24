using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;

namespace Sxul.Content.Rarities
{
    public class RainV : ModRarity
    {
        private const float CycleDuration = 0.5f; // Duration in seconds for a full cycle through one color pair

        public override Color RarityColor
        {
            get
            {
                float time = (Main.GameUpdateCount / 60f) % (ColorHelper.RainVCustomColors.Length * CycleDuration);
                int currentIndex = (int)(time / CycleDuration) % ColorHelper.RainVCustomColors.Length;
                int nextIndex = (currentIndex + 1) % ColorHelper.RainVCustomColors.Length;

                float lerpAmount = (time % CycleDuration) / CycleDuration; // Calculate interpolation factor
                return Color.Lerp(ColorHelper.RainVCustomColors[currentIndex], ColorHelper.RainVCustomColors[nextIndex], lerpAmount);
            }
        }

        public override int GetPrefixedRarity(int offset, float valueMult)
        {
            if (offset == +2)
            {
                return ModContent.RarityType<Aegis>(); // Make the rarity of items that have this rarity with a positive modifier the higher tier one.
            }
            else if (offset == +1)
            {
                return ModContent.RarityType<VoidV>(); // Make the rarity of items that have this rarity with a positive modifier the higher tier one.
            }
            else if (offset == -1)
            {
                return ModContent.RarityType<Merf>();
            }
            else if (offset == -2)
            {
                return ModContent.RarityType<Good>();
            }
            return Type; // no 'lower' tier to go to, so return the type of this rarity.
        }
    }
}