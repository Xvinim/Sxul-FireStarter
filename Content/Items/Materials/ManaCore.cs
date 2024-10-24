using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Sxul.Content.Rarities;
using Terraria.ID;

namespace Sxul.Content.Items.Materials
{
    public class ManaCore : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<SoulCore>();
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.rare = ModContent.RarityType<RainV>();
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = 120;
            Item.useAnimation = 120;
            Item.consumable = true;
        }

        public override bool? UseItem(Player player)
        {
            SoulTracker modPlayer = player.GetModPlayer<SoulTracker>();
            if (!modPlayer.hasUsedManaCore)
            {
                if (modPlayer.hasUsedManaCore)
                {
                    return false;
                }
                else
                {
                    modPlayer.hasUsedManaCore = true;
                    player.statManaMax2 += 40;
                    return true;
                }
            }
            modPlayer.usingSoul = false;

            return true;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ItemID.FallenStar, 10);
            r.Register();
        }
    }
}
