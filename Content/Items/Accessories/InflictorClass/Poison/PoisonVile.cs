using Sxul.Content.Classes;
using Sxul.Content.Rarities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Sxul.Content.Items.Accessories.InflictorClass.Poison
{
    internal class PoisonVile : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 17;
            Item.height = 17;
            Item.accessory = true;
            Item.rare = ModContent.RarityType<Good>();
            Item.value = Item.sellPrice(gold: 4, silver: 44, copper: 4);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            Inflictor mp = player.GetModPlayer<Inflictor>();

            mp.iPoison += 0.80f;
            mp.canPoison = true;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ItemID.Bottle);
            r.AddIngredient(ItemID.Stinger, 15);
            r.AddIngredient(ItemID.JungleSpores, 18);
            r.AddIngredient(ItemID.Vine, 6);
            r.AddTile(TileID.TinkerersWorkbench);
            r.Register();
        }
    }
}
