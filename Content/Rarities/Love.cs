using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Sxul.Content.Rarities
{
    public class Love : ModRarity
    {
        private const float CycleDuration = 0.5f; // Duration in seconds for a full cycle through one color pair

        public override Color RarityColor
        {
            get
            {
                float time = (Main.GameUpdateCount / 60f) % (ColorHelper.LoveCustomColors.Length * CycleDuration);
                int currentIndex = (int)(time / CycleDuration) % ColorHelper.LoveCustomColors.Length;
                int nextIndex = (currentIndex + 1) % ColorHelper.LoveCustomColors.Length;

                float lerpAmount = (time % CycleDuration) / CycleDuration; // Calculate interpolation factor
                return Color.Lerp(ColorHelper.LoveCustomColors[currentIndex], ColorHelper.LoveCustomColors[nextIndex], lerpAmount);
            }
        }

        public override int GetPrefixedRarity(int offset, float valueMult)
        {
            if (offset > 0)
            {
                return ModContent.RarityType<RainV>(); // Make the rarity of items that have this rarity with a positive modifier the higher tier one.
            }
            else if (offset > 0)
            {
                return ModContent.RarityType<HighAegis>(); // Make the rarity of items that have this rarity with a positive modifier the higher tier one.
            }

            return Type; // no 'lower' tier to go to, so return the type of this rarity.
        }
    }
}