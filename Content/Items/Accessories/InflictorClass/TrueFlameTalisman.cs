using Sxul.Content.Classes;
using Sxul.Content.Items.Accessories.InflictorClass.CursedFire;
using Sxul.Content.Items.Accessories.InflictorClass.Fire;
using Sxul.Content.Items.Accessories.InflictorClass.Frost;
using Sxul.Content.Rarities;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Sxul.Content.Items.Accessories.InflictorClass
{
    internal class TrueFlameTalisman : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 17;
            Item.height = 17;
            Item.accessory = true;
            Item.rare = ModContent.RarityType<Godly>();
            Item.value = Item.sellPrice(silver: 74, gold: 6);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            Inflictor mp = player.GetModPlayer<Inflictor>();

            mp.iFlames += 0.20f;
            mp.iCursedFire += 0.15f;
            mp.iFrost += 0.10f;
            mp.canFlame = true;
            mp.canCurse = true;
            mp.canFrost = true;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ModContent.ItemType<FlameTalisman>());
            r.AddIngredient(ModContent.ItemType<IceStone>());
            r.AddIngredient(ModContent.ItemType<CursedCorruptionSeed>());
            r.AddTile(TileID.ObsidianBrick);
            r.Register();
        }
    }
}
