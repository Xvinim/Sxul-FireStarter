using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Sxul.Content.Rarities
{
    public class Material : ModRarity
    {
        public override Color RarityColor => new Color(225, 225, 225);

        public override int GetPrefixedRarity(int offset, float valueMult)
        {
            if (offset == +2)
            {
                return ModContent.RarityType<Great>(); // Make the rarity of items that have this rarity with a positive modifier the higher tier one.
            }
            else if (offset == +1)
            {
                return ModContent.RarityType<AtleastUse>(); // Make the rarity of items that have this rarity with a positive modifier the higher tier one.
            }
            else if (offset == -1)
            {
                return ModContent.RarityType<BossMaterial>();
            }
            else if (offset == -2)
            {
                return ModContent.RarityType<Meh>();
            }

            return Type; // no 'lower' tier to go to, so return the type of this rarity.
        }
    }
}