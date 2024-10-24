using Sxul.Content.Items.Accessories.Melee;
using Sxul.Content.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Sxul.Content.Items.BSummons;
using Terraria.Net;
using Sxul.Content.Items.Materials;

namespace Sxul.Content.Boss.Reworks
{
    internal class MoonLordRefresh : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            // First, we need to check the npc.type to see if the code is running for the vanilla NPCwe want to change
            if (npc.type == NPCID.MoonLordCore)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ForgottenRobes>(), 1, 1, 3));

                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<NG_>(), 1, 1, 1));
            }
        }
    }
}
