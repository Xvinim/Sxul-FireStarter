using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace Sxul.Content.Armor.XvinimSet
{
    [AutoloadEquip(EquipType.Head)]
    internal class Xvinim_sHelmet : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 21;
            Item.height = 21;
            Item.vanity = true;
        }
    }
}
