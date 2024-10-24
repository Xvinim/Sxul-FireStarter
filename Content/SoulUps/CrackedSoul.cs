using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Sxul.Content.Rarities;
using System;
using Steamworks;
using Sxul;
using Mono.Cecil.Cil;

namespace Sxul.Content.SoulUps
{
    public class CrackedSoul : ModItem
    {
        public override void SetDefaults()
        {
            Item.noMelee = true;
            Item.height = 15;
            Item.width = 15;
            Item.rare = ModContent.RarityType<VoidV>();
            Item.value = Item.sellPrice(0, 0, 38, 3);
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
            return modPlayer.crackedSoulUses < 5;
        }

        public override bool? UseItem(Player player)
        {
            SoulTracker modPlayer = player.GetModPlayer<SoulTracker>();

            modPlayer.crackedSoulUses++;

            modPlayer.crackedSoulUsed++;

            return true;
        }
    }
}
