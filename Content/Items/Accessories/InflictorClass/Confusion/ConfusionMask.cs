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

namespace Sxul.Content.Items.Accessories.InflictorClass.Confusion
{
    internal class ConfusionMask : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 17;
            Item.height = 17;
            Item.accessory = true;
            Item.rare = ModContent.RarityType<Mystic>();
            Item.value = Item.sellPrice(gold: 1, silver: 17, copper: 99);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            Inflictor mp = player.GetModPlayer<Inflictor>();

            mp.iBaseDebuff += 0.10f;
            mp.iConfusion += 0.25f;
            mp.canConfuse = true;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ModContent.ItemType<EngravedStone>());
            r.AddIngredient(ModContent.ItemType<BrainCell>());
            r.AddTile(TileID.DemonAltar);
            r.Register();
        }
    }
}
