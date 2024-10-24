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
    internal class EvilRock : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 17;
            Item.height = 17;
            Item.accessory = true;
            Item.rare = ModContent.RarityType<Great>();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            Inflictor mp = player.GetModPlayer<Inflictor>();

            mp.iBaseDebuff += 0.20f;
        }

        public override void AddRecipes()
        {
            Recipe crimson = CreateRecipe();
            crimson.AddIngredient(ModContent.ItemType<EngravedStone>());
            crimson.AddIngredient(ItemID.TissueSample, 5);
            crimson.AddTile(TileID.DemonAltar);
            crimson.Register();

            Recipe corruption = CreateRecipe();
            corruption.AddIngredient(ModContent.ItemType<EngravedStone>());
            corruption.AddIngredient(ItemID.ShadowScale, 5);
            corruption.AddTile(TileID.DemonAltar);
            corruption.Register();
        }
    }
}
