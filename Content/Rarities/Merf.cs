using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Sxul.Content.Rarities
{
    public class Merf : ModRarity
    {
        public override Color RarityColor => new Color(78, 50, 9);

        public override int GetPrefixedRarity(int offset, float valueMult)
        {
            if (offset == +2)
            {
                return ModContent.RarityType<VoidV>(); // Make the rarity of items that have this rarity with a positive modifier the higher tier one.
            }
            else if (offset == +1)
            {
                return ModContent.RarityType<RainV>(); // Make the rarity of items that have this rarity with a positive modifier the higher tier one.
            }
            else if (offset == -1)
            {
                return ModContent.RarityType<Good>();
            }
            else if (offset == -2)
            {
                return ModContent.RarityType<Great>();
            }

            return Type; // no 'lower' tier to go to, so return the type of this rarity.
        }
    }
}