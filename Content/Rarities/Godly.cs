using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Sxul.Content.Rarities
{
    public class Godly : ModRarity
    {
        public override Color RarityColor => new Color(225, 220, 74);

        public override int GetPrefixedRarity(int offset, float valueMult)
        {
            if (offset == -1)
            {
                return ModContent.RarityType<Legendary>(); // Make the rarity of items that have this rarity with a positive modifier the higher tier one.
            }
            else if (offset == -2)
            {
                return ModContent.RarityType<Mystic>();
            }

            return Type; // no 'lower' tier to go to, so return the type of this rarity.
        }
    }
}