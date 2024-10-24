﻿using Sxul.Content.Classes;
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
    internal class FlameKingIfrit : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 17;
            Item.height = 17;
            Item.accessory = true;
            Item.rare = ModContent.RarityType<Good>();
            Item.value = Item.sellPrice(gold: 3, silver: 64, copper: 18);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            Inflictor mp = player.GetModPlayer<Inflictor>();

            mp.iFlames += 0.50f;
            mp.canFlame = true;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ModContent.ItemType<TrueFlameTalisman>());
            r.AddIngredient(ModContent.ItemType<HeavenlyFire>());
            r.AddTile(TileID.TreeAsh);
            r.Register();
        }
    }
}