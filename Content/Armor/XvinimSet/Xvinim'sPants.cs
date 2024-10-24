using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace Sxul.Content.Armor.XvinimSet
{
    [AutoloadEquip(EquipType.Legs)]
    internal class Xvinim_sPants : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 12;
            Item.vanity = true;
        }
    }
}
