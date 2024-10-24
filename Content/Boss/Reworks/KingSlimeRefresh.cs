using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Sxul.Content.Items.Weapons;
using Terraria.GameContent.ItemDropRules;
using Microsoft.Xna.Framework;
using Sxul.Assets.BossBars;
using Sxul.Config;

namespace Sxul.Content.Boss.Reworks
{
    public class KingSlimeRework : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            // First, we need to check the npc.type to see if the code is running for the vanilla NPCwe want to change
            if (npc.type == NPCID.KingSlime)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Naruka>(), 2, 1, 1));
            }
            // We can use other if statements here to adjust the drop rules of other vanilla NPC
        }

        public override void SetDefaults(NPC npc)
        {
            if (SxulServer.Instance.BossReworks)
            {
                if (npc.type == NPCID.KingSlime)
                {
                    if (Main.specialSeedWorld)
                    {
                        npc.lifeMax = 3500;
                        npc.damage = 40;
                        npc.defense = 11;
                        npc.scale = 3f;
                    }
                    else if (Main.masterMode)
                    {
                        npc.lifeMax = 2000;
                        npc.damage = 35;
                        npc.defense = 10;
                        npc.scale = 2f;
                    }
                    else if (Main.expertMode)
                    {
                        npc.lifeMax = 1900;
                        npc.damage = 30;
                        npc.defense = 9;
                        npc.scale = 1f;
                    }
                    else
                    {
                        npc.lifeMax = 1650;
                        npc.damage = 25;
                        npc.defense = 8;
                    }

                    npc.aiStyle = -1;
                }
            }
        }

        private int slimeRainTimer = 0; // Timer for new attack

        public override bool InstancePerEntity => true;

        public override bool AppliesToEntity(NPC npc, bool lateInstantiation)
        {
            // This will ensure that this logic only applies to King Slime
            return npc.type == NPCID.KingSlime;
        }

        public override void AI(NPC npc)
        {
            if (npc.type == NPCID.KingSlime && SxulServer.Instance.BossReworks)
            {
                if (npc.life <= npc.lifeMax * 0.7f) // First phase
                {
                    KingSlimePhaseOne(npc);
                }
                if (npc.life <= npc.lifeMax * 0.4f) // Second phase
                {
                    KingSlimePhaseTwo(npc);
                }
                }
                // Master mode unique behavior
                if (Main.masterMode)
                {
                    SummonSlimeRain(npc);
                }
                base.AI(npc); // Retain the original AI behavior
            }

        // Phase one - Slime jump and teleportation
        private void KingSlimePhaseOne(NPC npc)
        {
            if (npc.ai[0] % 100 == 0) // Jump every few frames
            {
                npc.velocity.Y = -30f; // Jump higher
            }
        }

        // Phase two - Slime rain attack
        private void KingSlimePhaseTwo(NPC npc)
        {
            slimeRainTimer++;
            if (slimeRainTimer >= 60 * 60) // Every 1 minute
            {
                for (int i = 0; i < 0.1; i++)
                {
                    int slimeType = NPCID.BlueSlime;
                    if (Main.rand.NextFloat() < 0.10f)
                    {
                        slimeType = NPCID.RedSlime; // Occasionally spawn Pink Slimes
                    }
                    Vector2 spawnPosition = new Vector2(npc.position.X + Main.rand.Next(-50, 50), npc.position.Y - 30);
                    NPC.NewNPC(null, (int)spawnPosition.X, (int)spawnPosition.Y, slimeType);
                }
                slimeRainTimer = 0;
            }
        }
        private void SummonSlimeRain(NPC npc)
        {
            // Check if the slime rain should occur
            if (Main.rand.NextBool(180)) 
            {
                for (int i = 0; i < 0.01f; i++) // Loop through max slime count
                {
                    // Determine the spawn position
                    Vector2 spawnPosition = new Vector2(npc.position.X + Main.rand.Next(-300, 300), npc.position.Y - 400);
                    // Spawn the slime
                    NPC.NewNPC(null, (int)spawnPosition.X, (int)spawnPosition.Y, NPCID.BlueSlime);
                }
            }
        }
    }
}