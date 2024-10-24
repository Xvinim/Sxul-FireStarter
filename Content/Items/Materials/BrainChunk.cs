using Terraria;
using Terraria.Localization;
using Terraria.ID;
using Terraria.ModLoader;
using Sxul.Content.Rarities;
using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Sxul.Content.Items.Materials
{
    public class BrainChunk : ModItem
    {
        public override void SetDefaults()
        {
            Item.rare = ModContent.RarityType<BossMaterial>();
            Item.value = Item.buyPrice(silver: 62);
            Item.noMelee = true;
            Item.maxStack = 9999;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 3;
            Item.useTime = 3;

            Item.width = 27;
            Item.height = 27;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Determine the correct tooltip text based on the world type
            string tooltipText = WorldGen.crimson
                ? "'gross...'\nMaterial"
                : "'a piece of a brain..? must've eaten something weird..'\nMaterial";

            // Add the new tooltip line to the list
            TooltipLine newLine = new TooltipLine(Mod, "MyCustomTooltip", tooltipText);
            tooltips.Add(newLine);
        }
    }
}