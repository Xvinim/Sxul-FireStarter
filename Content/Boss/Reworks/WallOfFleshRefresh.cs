using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Sxul.Content.Items.Weapons;
using Terraria.GameContent.ItemDropRules;
using Terraria.Social;
using Microsoft.Xna.Framework;
using Sxul.Content.Items.Accessories.Melee;
using Sxul.Config;

namespace Sxul.Content.Boss.Reworks
{
    public class WallofFlesh : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public bool specialMode = false;
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            // First, we need to check the npc.type to see if the code is running for the vanilla NPCwe want to change
            if (npc.type == NPCID.WallofFlesh)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SeldomHarbinger>(), 4, 1, 1));

                if (Main.expertMode)
                {
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DevilsBargain>()));
                }
            }
            // We can use other if statements here to adjust the drop rules of other vanilla NPC
        }

        //SetDefaults
        #region
        public override void SetDefaults(NPC npc)
        {
            if (SxulServer.Instance.BossReworks)
            {
                if (npc.type == NPCID.WallofFlesh)
                {
                    if (Main.specialSeedWorld)
                    {
                        npc.lifeMax = 17500;
                        npc.damage = 120;
                        npc.defense = 15;
                        specialMode = true;
                    }
                    else if (Main.masterMode)
                    {
                        npc.lifeMax = 15000;
                        npc.damage = 95;
                        npc.defense = 14;
                    }
                    else if (Main.expertMode)
                    {
                        npc.lifeMax = 12000;
                        npc.damage = 70;
                        npc.defense = 13;
                    }
                    else
                    {
                        npc.lifeMax = 8000;
                        npc.damage = 50;
                        npc.defense = 12;
                    }
                }
            }
        }
        #endregion

        //specialMode(i.e. Legendary Behaviors)
        #region
        public override void AI(NPC npc)
        {
            if (npc.type == NPCID.Deerclops)
            {
                base.AI(npc); // Ensure base behavior is retained

                // Special mode adjustments
                if (specialMode)
                {
                    // Example of additional special mode behavior
                    // Apply Frostburn debuff when the boss attacks

                    // For example, you can check if the NPC has a player target
                    if (npc.HasPlayerTarget)
                    {
                        Player player = Main.player[npc.target];
                        // Apply Frostburn debuff if the NPC is hitting the player
                        if (npc.Distance(player.Center) < 600f && player.active)
                        {
                            player.AddBuff(BuffID.Frostburn, 300); // Apply Frostburn debuff for 5 seconds (300 ticks)
                        }
                    }
                }
            }
        }
        #endregion
    }
}