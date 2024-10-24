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
    internal class HeavenlyStone : ModItem
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

            mp.iBaseDebuff += 0.30f;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ModContent.ItemType<EvilRock>());
            r.AddIngredient(ItemID.Cloud, 10);
            r.AddIngredient(ItemID.StoneBlock, 30);
            r.AddTile(TileID.SkyMill);
            r.Register();
        }
    }
}
