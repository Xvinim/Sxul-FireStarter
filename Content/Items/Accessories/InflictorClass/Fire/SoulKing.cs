using Sxul.Content.Classes;
using Sxul.Content.Rarities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace Sxul.Content.Items.Accessories.InflictorClass.Fire
{
    internal class SoulKing : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 17;
            Item.height = 17;
            Item.accessory = true;
            Item.rare = ModContent.RarityType<Legendary>();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            Inflictor mp = player.GetModPlayer<Inflictor>();

            mp.iBaseDebuff += 0.50f;
            mp.iFlames += 0.20f;
            mp.iCursedFire += 0.20f;
            mp.iFrost += 0.20f;
            mp.canFlame = true;
            mp.canFrost = true;
            mp.canCurse = true;
        }
    }
}
