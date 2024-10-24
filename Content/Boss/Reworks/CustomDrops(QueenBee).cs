using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using Sxul.Content.Items.Accessories.Mixed;
using Sxul.Config;

namespace Sxul.Content.Boss.Reworks
{
    public class QueenBee : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            // First, we need to check the npc.type to see if the code is running for the vanilla NPCwe want to change
            if (npc.type == NPCID.QueenBee)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<StimShot>(), 5, 1, 1));
                //npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<123thisIsNull>()), 1, 1, 2);
            }
            // We can use other if statements here to adjust the drop rules of other vanilla NPC
        }

        public override void SetDefaults(NPC entity)
        {
            if (SxulServer.Instance.BossReworks)
            {
                if(entity.type == NPCID.QueenBee)
                {
                    if (Main.masterMode)
                    {
                        entity.lifeMax = 6369;
                        entity.defense = 10;
                        entity.damage += 7;
                    }
                    else if (Main.expertMode)
                    {
                        entity.lifeMax = 5000;
                        entity.defense = 9;
                        entity.damage = 54;
                    }
                    else
                    {
                        entity.lifeMax = 3500;
                        entity.defense = 8;
                        entity.damage = 30;
                    }
                }
            }
        }
    }
}