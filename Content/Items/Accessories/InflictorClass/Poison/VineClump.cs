using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Sxul.Content.Classes;
using Sxul.Content.Rarities;
using Terraria.ID;

namespace Sxul.Content.Items.Accessories.InflictorClass.Poison
{
    internal class VineClump : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 15;
            Item.height = 20;
            Item.rare = ModContent.RarityType<Great>();
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            Inflictor mp = player.GetModPlayer<Inflictor>();

            mp.iPoison += 0.40f;
            mp.canPoison = true;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ItemID.Vine, 8);
            r.AddTile(TileID.WorkBenches);
            r.Register();
        }
    }
}
