using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Sxul.Content.Rarities;
using Microsoft.Xna.Framework;
using Sxul.Content.Classes;
using Sxul.Systems;

namespace Sxul.Content.Items.Accessories.Melee
{
    public class CursedOilPot : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.rare = ModContent.RarityType<Aegis>();
            Item.accessory = true;
            Item.value = Item.buyPrice(gold: 1, silver: 32);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            SoulTracker mp = player.GetModPlayer<SoulTracker>();

            mp.cursedOilPot = true;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ItemID.CursedFlame, 10);
            r.AddIngredient(ItemID.Bottle);
            r.AddTile(TileID.MythrilAnvil);
            r.Register();
        }
    }
}
