using Sxul.Content.Items.Weapons.References;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace Sxul.Content.Boss.Reworks
{
    internal class DestroyerRework : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            // Check if the NPC is The Twins (Retinazer and Spazmatism are considered together)
            if (npc.type == NPCID.TheDestroyer)
            {
                // Add the drop rule for Crescent Rose in expert mode with a 0.4% chance
                npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), ModContent.ItemType<BladeOfJudgement>(), chanceNumerator: 4, chanceDenominator: 1000));
            }
        }
    }
}
