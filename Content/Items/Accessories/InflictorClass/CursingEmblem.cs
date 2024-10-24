using Sxul.Content.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace Sxul.Content.Items.Accessories.InflictorClass
{
    internal class CursingEmblem : ModItem
    {
        public override void SetDefaults()
        {
            var item = Item;
            item.width = 13;
            item.height = 13;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            Inflictor mp = player.GetModPlayer<Inflictor>();
            mp.iDamage += 0.15f;
        }
    }
}
