using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Sxul.Content.Classes;
using Sxul.Content.Rarities;

namespace Sxul.Content.Items.Accessories.InflictorClass
{
    internal class EngravedStone : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 17;
            Item.height = 17;
            Item.accessory = true;
            Item.rare = ModContent.RarityType<Meh>();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            Inflictor mp = player.GetModPlayer<Inflictor>();

            mp.iBaseDebuff += 0.10f;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ItemID.TinBroadsword);
            r.AddIngredient(ItemID.StoneBlock, 25);
            r.AddTile(TileID.Anvils);
            r.Register();
        }
    }
}
