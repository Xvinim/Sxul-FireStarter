using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Sxul.Content.Items.Weapons;
using Terraria.GameContent.ItemDropRules;
using Sxul.Config;

namespace Sxul.Content.Boss.Reworks
{
	public class Skeletron : GlobalNPC
	{
		public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
		{
			// First, we need to check the npc.type to see if the code is running for the vanilla NPCwe want to change
			if (npc.type == NPCID.SkeletronHead)
			{
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<TimeScale>(), 3, 1, 1));
				//npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<123thisIsNull>()), 1, 1, 2);
			}
			// We can use other if statements here to adjust the drop rules of other vanilla NPC
		}

        public override void SetDefaults(NPC entity)
        {
            if (SxulServer.Instance.BossReworks)
            {
                if (entity.type == NPCID.SkeletronHead)
                {
                    if (Main.masterMode)
                    {
                        entity.lifeMax = 12000;
                        entity.defense = 12;
                        entity.damage = 105;
                    }
                    else if (Main.expertMode)
                    {
                        entity.lifeMax = 9000;
                        entity.defense = 11;
                        entity.damage = 70;
                    }
                    else
                    {
                        entity.lifeMax = 6500;
                        entity.defense = 10;
                        entity.damage = 32;
                    }
                }
                else if(entity.type == NPCID.SkeletronHand)
                {
                    if (Main.masterMode)
                    {
                        entity.lifeMax = 2000;
                        entity.defense = 16;
                        entity.damage = 20;
                    }
                    else if (Main.expertMode)
                    {
                        entity.lifeMax = 1500;
                        entity.defense = 15;
                        entity.damage = 15;
                    }
                    else
                    {
                        entity.lifeMax = 1000;
                        entity.defense = 14;
                        entity.damage = 10;
                    }
                }
            }
        }
    }
}