using Sxul.Content.Rarities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Sxul.Content.Items.Beginning
{
    internal class ElfRace : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = true;
            Item.rare = ModContent.RarityType<Material>();
        }

        public override bool? UseItem(Player player)
        {
            SoulTracker mp = player.GetModPlayer<SoulTracker>();

            if (mp.isHuman || mp.isLizhard || mp.isVampire || mp.isDryad)
            {
                return false;
            }
            else
            {
                mp.isElf = true;
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
            r3.AddIngredient(ModContent.ItemType<HumanRace>());
            r3.Register();
        }
    }
}
