using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Sxul.Content.Rarities
{
    public class Basic : ModRarity
    {
        public override Color RarityColor => new Color(238, 143, 0);

        public override int GetPrefixedRarity(int offset, float valueMult)
        {
            if (offset == +2)
            {
                return ModContent.RarityType<Meh>(); // Make the rarity of items that have this rarity with a positive modifier the higher tier one.
            }
            else if (offset == +1)
            {
                return ModContent.RarityType<Horrid>(); // Make the rarity of items that have this rarity with a positive modifier the higher tier one.
            }
            else if (offset == -1)
            {
                return ModContent.RarityType<Basic>();
            }
            else if (offset == -2)
            {
                return ModContent.RarityType<Basic>();
            }

            return Type; // no 'lower' tier to go to, so return the type of this rarity.
        }
    }
}