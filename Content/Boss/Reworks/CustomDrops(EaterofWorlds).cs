using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Sxul.Content.Items.Weapons;
using Sxul.Content.Items.Materials;
using Terraria.GameContent.ItemDropRules;
using Sxul.Config;

namespace Sxul.Content.Boss.Reworks
{
	public class EaterofWorlds : GlobalNPC
	{
		public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
		{
			// First, we need to check the npc.type to see if the code is running for the vanilla NPCwe want to change
			if (npc.type == NPCID.EaterofWorldsHead)
			{
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WorldBane>(), 3, 1, 1));
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BrainChunk>(), 1, 1, 10));
			}
			// We can use other if statements here to adjust the drop rules of other vanilla NPC
		}

        public override void SetDefaults(NPC entity)
        {
            if (SxulServer.Instance.BossReworks)
			{
				if (entity.type == NPCID.EaterofWorldsHead)
				{
					if (Main.masterMode)
					{
						entity.lifeMax = 300;
						entity.defense = 4;
						entity.damage = 72;
					}
					else if (Main.expertMode)
					{
                        entity.lifeMax = 250;
                        entity.defense = 3;
                        entity.damage = 48;
                    }
					else
					{
                        entity.lifeMax = 150;
                        entity.defense = 2;
                        entity.damage = 22;
                    }
				}
				else if (entity.type == NPCID.EaterofWorldsBody)
				{
                    if (Main.masterMode)
                    {
                        entity.lifeMax = 300;
                        entity.defense = 6;
                        entity.damage = 31;
                    }
                    else if (Main.expertMode)
                    {
                        entity.lifeMax = 250;
                        entity.defense = 5;
                        entity.damage = 20;
                    }
                    else
                    {
                        entity.lifeMax = 150;
                        entity.defense = 4;
                        entity.damage = 13;
                    }
                }
				else if (entity.type == NPCID.EaterofWorldsTail)
				{
                    if (Main.masterMode)
                    {
                        entity.lifeMax = 300;
                        entity.defense = 10;
                        entity.damage = 26;
                    }
                    else if (Main.expertMode)
                    {
                        entity.lifeMax = 250;
                        entity.defense = 9;
                        entity.damage = 17;
                    }
                    else
                    {
                        entity.lifeMax = 150;
                        entity.defense = 8;
                        entity.damage = 11;
                    }
                }
			}
        }
    }
}