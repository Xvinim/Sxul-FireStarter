using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Sxul.Content.Rarities
{
    public class Meh : ModRarity
    {
        public override Color RarityColor => new Color(43, 0, 129);

        public override int GetPrefixedRarity(int offset, float valueMult)
        {
            if (offset == +2)
            {
                return ModContent.RarityType<Material>(); // Make the rarity of items that have this rarity with a positive modifier the higher tier one.
            }
            else if (offset == +1)
            {
                return ModContent.RarityType<BossMaterial>(); // Make the rarity of items that have this rarity with a positive modifier the higher tier one.
            }
            else if (offset == -1)
            {
                return ModContent.RarityType<Horrid>();
            }
            else if (offset == -2)
            {
                return ModContent.RarityType<Basic>();
            }

            return Type; // no 'lower' tier to go to, so return the type of this rarity.
        }
    }
}