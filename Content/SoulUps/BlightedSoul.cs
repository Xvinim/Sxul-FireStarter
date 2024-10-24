using Sxul.Content.Rarities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace Sxul.Content.SoulUps
{
    internal class BlightedSoul : ModItem
    {
        public override void SetDefaults()
        {
            Item.noMelee = true;
            Item.height = 15;
            Item.width = 15;
            Item.rare = ModContent.RarityType<VoidV>();
            Item.value = Item.sellPrice(0, 1, 83, 93);
            Item.mana = 20;
            Item.consumable = true;
            Item.maxStack = 10;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = 20;
            Item.useAnimation = 20;
        }

        public override bool CanUseItem(Player player)
        {
            SoulTracker modPlayer = player.GetModPlayer<SoulTracker>();
            return modPlayer.blightSoulUses < 2;
        }

        public override bool? UseItem(Player player)
        {
            SoulTracker modPlayer = player.GetModPlayer<SoulTracker>();

            modPlayer.blightSoulUses++;

            modPlayer.blightSoulUsed++;

            return true;
        }
    }
}
