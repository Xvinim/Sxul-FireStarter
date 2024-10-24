using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace Sxul.Content.Armor.XvinimSet
{
    [AutoloadEquip(EquipType.Body)]
    internal class Xvinim_sPlating : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 27;
            Item.height = 27;
            Item.vanity = true;
        }
    }
}
