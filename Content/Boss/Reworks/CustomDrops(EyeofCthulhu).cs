using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Sxul.Content.Items.Weapons;
using Terraria.GameContent.ItemDropRules;
using Microsoft.Xna.Framework;
using Sxul.Config;

namespace Sxul.Content.Boss.Reworks
{
    public class EyeofCthulhu : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.EyeofCthulhu)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<TheRedDevil>(), 4, 1, 1));
            }
        }

        public override void SetDefaults(NPC npc)
        {
            if (SxulServer.Instance.BossReworks)
            {
                if (npc.type == NPCID.EyeofCthulhu)
                {
                    if (Main.masterMode)
                    {
                        npc.lifeMax = 5500;
                        npc.damage = 30;
                        npc.defense = 17;
                    }
                    else if (Main.expertMode)
                    {
                        npc.lifeMax = 4000;
                        npc.damage = 20;
                        npc.defense = 14;
                    }
                    else
                    {
                        npc.lifeMax = 2800;
                        npc.damage = 15;
                        npc.defense = 12;
                    }
                }
            }
        }
    }
}
