using Sxul.Content.Rarities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace Sxul.Content.Items.Materials
{
    internal class SoulCore : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<ManaCore>();
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
            modPlayer.usingSoul = true;
            return true;
        }
    }
}
