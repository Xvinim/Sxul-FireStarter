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

namespace Sxul.Content.Items.Accessories.InflictorClass.Fire
{
    internal class HeavenlyFire : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 17;
            Item.height = 17;
            Item.accessory = true;
            Item.rare = ModContent.RarityType<Good>();
            Item.value = Item.sellPrice(gold: 1, silver: 33, copper: 33);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            Inflictor mp = player.GetModPlayer<Inflictor>();

            mp.iBaseDebuff += 0.30f;
            mp.iFlames += 0.20f;
            mp.canFlame = true;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ModContent.ItemType<FlameTalisman>());
            r.AddIngredient(ModContent.ItemType<HeavenlyStone>());
            r.AddTile(TileID.SkyMill);
            r.Register();
        }
    }
}
