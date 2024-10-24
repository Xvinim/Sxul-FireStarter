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
    internal class DecayingSoul : ModItem
    {
        public override void SetDefaults()
        {
            Item.noMelee = true;
            Item.height = 15;
            Item.width = 15;
            Item.rare = ModContent.RarityType<VoidV>();
            Item.value = Item.sellPrice(0, 4, 20, 0);
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
            return modPlayer.decaySoulUses < 2;
        }

        public override bool? UseItem(Player player)
        {
            SoulTracker modPlayer = player.GetModPlayer<SoulTracker>();

            modPlayer.decaySoulUses++;

            modPlayer.decaySoulUsed++;

            return true;
        }
    }
}
