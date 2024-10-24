using Sxul.Content.Classes;
using Sxul.Content.Items.Accessories.InflictorClass.Confusion;
using Sxul.Content.Items.Accessories.InflictorClass.CursedFire;
using Sxul.Content.Rarities;
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
    internal class CorruptedCellStone : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 17;
            Item.height = 17;
            Item.accessory = true;
            Item.rare = ModContent.RarityType<Good>();
            Item.value = Item.sellPrice(gold: 4, silver: 99, copper: 99);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            Inflictor mp = player.GetModPlayer<Inflictor>();

            mp.iBaseDebuff += 0.30f;
            mp.iConfusion += 0.50f;
            mp.iCursedFire += 0.15f;
            mp.canConfuse = true;
            mp.canCurse = true;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ModContent.ItemType<HeavenlyStone>());
            r.AddIngredient(ModContent.ItemType<Neuron>());
            r.AddIngredient(ModContent.ItemType<CursedCorruptionSeed>());
            r.AddTile(TileID.SkyMill);
            r.AddTile(TileID.Anvils);
            r.Register();
        }
    }
}
