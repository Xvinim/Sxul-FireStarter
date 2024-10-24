using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Sxul.Content.Rarities
{
    public class Great : ModRarity
    {
        public override Color RarityColor => new Color(44, 58, 225);

        public override int GetPrefixedRarity(int offset, float valueMult)
        {
            if (offset == +2)
            {
                return ModContent.RarityType<Merf>(); // Make the rarity of items that have this rarity with a positive modifier the higher tier one.
            }
            else if (offset == +1)
            {
                return ModContent.RarityType<Good>(); // Make the rarity of items that have this rarity with a positive modifier the higher tier one.
            }
            else if (offset == -1)
            {
                return ModContent.RarityType<AtleastUse>();
            }
            else if (offset == -2)
            {
                return ModContent.RarityType<Material>();
            }

            return Type; // no 'lower' tier to go to, so return the type of this rarity.
        }
    }
}