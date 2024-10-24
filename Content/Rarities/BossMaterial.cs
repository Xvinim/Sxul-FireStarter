using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Sxul.Content.Rarities
{
    public class BossMaterial : ModRarity
    {
        public override Color RarityColor => new Color(148, 148, 148);

        public override int GetPrefixedRarity(int offset, float valueMult)
        {
            if (offset == +2)
            {
                return ModContent.RarityType<AtleastUse>(); // Make the rarity of items that have this rarity with a positive modifier the higher tier one.
            }
            else if (offset == +1)
            {
                return ModContent.RarityType<Material>(); // Make the rarity of items that have this rarity with a positive modifier the higher tier one.
            }
            else if (offset == -1)
            {
                return ModContent.RarityType<Meh>();
            }
            else if (offset == -2)
            {
                return ModContent.RarityType<Horrid>();
            }

            return Type; // no 'lower' tier to go to, so return the type of this rarity.
        }
    }
}