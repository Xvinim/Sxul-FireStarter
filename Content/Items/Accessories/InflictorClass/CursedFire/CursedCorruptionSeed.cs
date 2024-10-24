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

namespace Sxul.Content.Items.Accessories.InflictorClass.CursedFire
{
    internal class CursedCorruptionSeed : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 17;
            Item.height = 17;
            Item.accessory = true;
            Item.rare = ModContent.RarityType<Good>();
            Item.value = Item.sellPrice(gold: 2, silver: 63, copper: 64);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            Inflictor mp = player.GetModPlayer<Inflictor>();

            mp.iCursedFire += 0.15f;
            mp.canCurse = true;
        }

        public override void AddRecipes()
        {
            Recipe r1 = CreateRecipe();
            r1.AddIngredient(ItemID.RottenChunk, 35);
            r1.AddIngredient(ItemID.CursedFlame, 5);
            r1.AddTile(TileID.DemonAltar);
            r1.Register();

            Recipe r2 = CreateRecipe();
            r2.AddIngredient(ItemID.Vertebrae, 35);
            r2.AddIngredient(ItemID.Ichor, 5);
            r2.AddTile(TileID.DemonAltar);
            r2.Register();
        }
    }
}
