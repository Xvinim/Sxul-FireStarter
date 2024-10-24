using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Sxul.Content.Rarities
{
    public class HighAegis : ModRarity
    {
        public override Color RarityColor => new Color(0, 247, 255);

        public override int GetPrefixedRarity(int offset, float valueMult)
        {
            if (offset == +2)
            {
                return ModContent.RarityType<Legendary>(); // Make the rarity of items that have this rarity with a positive modifier the higher tier one.
            }
            else if (offset == +1)
            {
                return ModContent.RarityType<Mystic>(); // Make the rarity of items that have this rarity with a positive modifier the higher tier one.
            }
            else if (offset == -1)
            {
                return ModContent.RarityType<Aegis>();
            }
            else if (offset == -2)
            {
                return ModContent.RarityType<VoidV>();
            }

            return Type; // no 'lower' tier to go to, so return the type of this rarity.
        }
    }
}