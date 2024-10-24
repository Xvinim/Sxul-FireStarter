using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Sxul.Content.Rarities
{
    public class Legendary : ModRarity
    {
        public override Color RarityColor => new Color(219, 175, 30);

        public override int GetPrefixedRarity(int offset, float valueMult)
        {
            if (offset > 0)
            {
                return ModContent.RarityType<Godly>(); // Make the rarity of items that have this rarity with a positive modifier the higher tier one.
            }
            else if (offset == -1)
            {
                return ModContent.RarityType<Mystic>();
            }
            else if (offset == -2)
            {
                return ModContent.RarityType<HighAegis>();
            }

            return Type; // no 'lower' tier to go to, so return the type of this rarity.
        }
    }
}