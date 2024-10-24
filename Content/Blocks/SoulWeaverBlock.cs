using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Sxul.Content.Tiles;
using Sxul.Content.SoulUps;

namespace Sxul.Content.Blocks
{
    internal class SoulWeaverBlock : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 24; // Dimensions of the item
            Item.height = 24;
            Item.maxStack = 99; // Max stack size
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing; // Use style like swinging
            Item.consumable = true; // It will be consumed when placed
            Item.value = 150;
            Item.createTile = ModContent.TileType<SoulWeaverTile>(); // Reference to the custom tile
            Item.useStyle = ItemUseStyleID.Swing; // Style of item usage
        }

        // Recipe for crafting the Soul Weaver (optional, modify as needed)
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Obsidian, 5);
            recipe.AddIngredient(ModContent.ItemType<CrackedSoul>(), 3);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
