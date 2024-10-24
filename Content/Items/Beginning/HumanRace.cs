using Sxul.Content.Rarities;
using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Terraria;
using Terraria.ModLoader;
using Sxul.Content.Rarities;
using Terraria.ID;

namespace Sxul.Content.Items.Beginning
{
    public class HumanRace : ModItem
    {
        public override void SetDefaults()
        {
            Item.consumable = true;
            Item.width = 20;
            Item.height = 20;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.rare = ModContent.RarityType<Material>();
        }

        public override bool? UseItem(Player player)
        {
            SoulTracker modPlayer = player.GetModPlayer<SoulTracker>();
            
            if (modPlayer.isDryad || modPlayer.isLizhard || modPlayer.isVampire || modPlayer.isElf)
            {
                return false;
            }
            else
            {
                modPlayer.isHuman = true;
                return true;
            }
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ModContent.ItemType<VampireRace>());
            r.Register();

            Recipe r1 = CreateRecipe();
            r1.AddIngredient(ModContent.ItemType<LizhardRace>());
            r1.Register();

            Recipe r2 = CreateRecipe();
            r2.AddIngredient(ModContent.ItemType<DryadRace>());
            r2.Register();

            Recipe r3 = CreateRecipe();
            r3.AddIngredient(ModContent.ItemType<ElfRace>());
            r3.Register();
        }
    }
}
