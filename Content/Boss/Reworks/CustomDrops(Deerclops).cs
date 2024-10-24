using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Sxul.Content.Items.Materials;
using Terraria.GameContent.ItemDropRules;
using Microsoft.Xna.Framework;
using Sxul.Config;

namespace Sxul.Content.Boss.Reworks
{
	public class Deerclops : GlobalNPC
	{
        public override bool InstancePerEntity => true;
        public bool specialMode = false;

		public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
		{
			// First, we need to check the npc.type to see if the code is running for the vanilla NPCwe want to change
			if (npc.type == NPCID.Deerclops)
			{
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<IceFragment>(), 1, 30, 50));
			}
			// We can use other if statements here to adjust the drop rules of other vanilla NPC
		}

		//SetDefaults
		#region
		public override void SetDefaults(NPC npc)
        {
			if (SxulServer.Instance.BossReworks)
            {
                if (npc.type == NPCID.Deerclops)
                {
                    if (Main.specialSeedWorld)
                    {
                        npc.lifeMax = 3000;
                        npc.damage = 35;
                        npc.defense = 8;
                        specialMode = true;
                    }
                    else if (Main.masterMode)
                    {
                        npc.lifeMax = 2280;
                        npc.damage = 29;
                        npc.defense = 7;
                    }
                    else if (Main.expertMode)
                    {
                        npc.lifeMax = 1250;
                        npc.damage = 22;
                        npc.defense = 6;
                    }
                    else
                    {
                        npc.lifeMax = 800;
                        npc.damage = 18;
                        npc.defense = 5;
                    }
                }
            }
        }
		#endregion

		//specialMode(i.e. Legendary Mode Behavior)
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