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
    internal class CursedRock : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 17;
            Item.height = 17;
            Item.accessory = true;
            Item.rare = ModContent.RarityType<Great>();
            Item.value = Item.sellPrice(gold: 2, silver: 63, copper: 64);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            Inflictor mp = player.GetModPlayer<Inflictor>();

            mp.iBaseDebuff += 0.20f;
            mp.iCursedFire += 0.15f;
            mp.canCurse = true;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddTile(TileID.TinkerersWorkbench);
            r.AddIngredient(ModContent.ItemType<CursedCorruptionSeed>());
            r.AddIngredient(ModContent.ItemType<EvilRock>());
            r.Register();
        }
    }
}
