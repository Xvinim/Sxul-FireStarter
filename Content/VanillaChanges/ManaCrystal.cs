using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Sxul;
using Sxul.Content.SoulUps;
using Terraria;
using Terraria.ID;

namespace Sxul.Content.VanillaChanges
{
    public class ManaCrystal : GlobalItem
    {
        public override bool CanUseItem(Item item, Player player)
        {
            if(item.type == ItemID.ManaCrystal)
            {
                SoulTracker modPlayer = player.GetModPlayer<SoulTracker>();

                if (!modPlayer.hasUsedManaCore)
                {
                    return false;
                }
            }
            return base.CanUseItem(item, player);
        }
    }
}
