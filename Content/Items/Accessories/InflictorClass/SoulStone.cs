using Sxul.Content.Classes;
using Sxul.Content.Rarities;
using Sxul.Content.SoulUps;
using Sxul.Content.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Sxul.Content.Items.Accessories.InflictorClass
{
    internal class SoulStone : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 17;
            Item.height = 17;
            Item.accessory = true;
            Item.rare = ModContent.RarityType<Mystic>();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            Inflictor mp = player.GetModPlayer<Inflictor>();

            mp.iBaseDebuff += 0.50f;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ModContent.ItemType<CrackedSoul>());
            r.AddIngredient(ItemID.HellstoneBar, 3);
            r.AddTile(ModContent.TileType<SoulWeaverTile>());
            r.Register();
        }
    }
}
