using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Sxul.Content.Items.Weapons;
using Sxul.Content.Items.Materials;
using Terraria.GameContent.ItemDropRules;
using Sxul.Content.Items.Accessories.InflictorClass.Confusion;
using Sxul.Config;

namespace Sxul.Content.Boss.Reworks
{
	public class BrainofCthulhu : GlobalNPC
	{
		public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
		{
			// First, we need to check the npc.type to see if the code is running for the vanilla NPCwe want to change
			if (npc.type == NPCID.BrainofCthulhu)
			{
				// This is where we add item drop rules for VampireBat, here is a simple example:
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Yulah>(), 3, 1, 1));
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BrainChunk>(), 1, 6, 20));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BrainOfTrueConfusion>(), 5, 1, 1));
			}
			// We can use other if statements here to adjust the drop rules of other vanilla NPC
		}

        public override void SetDefaults(NPC npc)
        {
            if (SxulServer.Instance.BossReworks)
			{
                if (npc.type == NPCID.BrainofCthulhu)
                {
                    if (Main.specialSeedWorld)
                    {
                        npc.lifeMax = 3100;
                        npc.damage = 116;
                        npc.defense = 17;
                    }
                    else if (Main.masterMode)
                    {
                        npc.lifeMax = 2709;
                        npc.damage = 81;
                        npc.defense = 16;
                    }
                    else if (Main.expertMode)
                    {
                        npc.lifeMax = 2125;
                        npc.damage = 54;
                        npc.defense = 15;
                    }
                    else
                    {
                        npc.lifeMax = 1250;
                        npc.damage = 40;
                        npc.defense = 14;
                    }
                }
            }
        }
    }
}