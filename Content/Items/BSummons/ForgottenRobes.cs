using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using System;
using System.Collections;
using Sxul.Content.Rarities;
using Sxul.Content.Boss.Bosses;

namespace Sxul.Content.Items.BSummons
{
    public class ForgottenRobes : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 9999;
            Item.rare = ModContent.RarityType<RainV>();
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = true;
        }

        public override bool CanUseItem(Player player)
        {
            // Ensure boss isn't already spawned
            return !NPC.AnyNPCs(ModContent.NPCType<ForgottenCultist>());
        }

        public override bool? UseItem(Player player)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<ForgottenCultist>());
            }
            return true;
        }
    }
}