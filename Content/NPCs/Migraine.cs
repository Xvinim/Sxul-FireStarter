using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.Utilities;
using Terraria.Enums;
using System;
using Microsoft.Xna.Framework;

namespace Sxul.Content.NPCs
{
    public class Migraine : ModNPC
    {
        private enum AIStates
        {
            Floating,
            Dashing
        }

        private AIStates state;
        private int stateTimer;

        public override void SetDefaults()
        {
            NPC.width = 18;  // Size similar to a zombie
            NPC.height = 40;
            NPC.damage = 32;  // Regular damage (walking)
            NPC.defense = 9;
            NPC.lifeMax = 600;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = 2348f;
            NPC.knockBackResist = 0.7f;
            NPC.aiStyle = -1;  // Custom AI
            state = AIStates.Floating;
            stateTimer = 0;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
        }

        public override void AI()
        {
            NPC.TargetClosest(true); // Target the closest player
            Player player = Main.player[NPC.target];

            // Make the NPC face the player
            if (player.Center.X < NPC.Center.X)
            {
                NPC.direction = -1; // Face left
                NPC.spriteDirection = -1;
            }
            else
            {
                NPC.direction = 1; // Face right
                NPC.spriteDirection = 1;
            }

            if (!player.active || player.dead)
            {
                NPC.TargetClosest(false);
                NPC.velocity.Y -= 0.1f; // Move up if the player is dead or inactive
                if (NPC.timeLeft > 10)
                {
                    NPC.timeLeft = 10;
                }
                return;
            }

            switch (state)
            {
                case AIStates.Floating:
                    FloatAroundPlayer(player);
                    if (stateTimer >= 1200) // 20 seconds
                    {
                        state = AIStates.Dashing;
                        stateTimer = 0;
                    }
                    break;

                case AIStates.Dashing:
                    if (NPC.ai[0] % 200 == 0)
                    {
                        DashTowardsPlayer(player);
                    }
                    if (stateTimer >= 450) // Time for dashing
                    {
                        state = AIStates.Floating;
                        stateTimer = 0;
                    }
                    break;
            }
        }

        private void FloatAroundPlayer(Player player)
        {
            Vector2 center = player.Center;
            float radius = 55f;
            float angle = (NPC.ai[0] % 360) * (float)Math.PI / 180f;
            Vector2 targetPosition = center;
            Vector2 direction = targetPosition - NPC.Center;
            float speed = 10f;
            direction.Normalize();
            NPC.velocity = (NPC.velocity * (30f - 1f) + direction * speed) / 30f;
        }

        private void DashTowardsPlayer(Player player)
        {
            Vector2 dashDirection = player.Center - NPC.Center;
            dashDirection.Normalize();
            float dashSpeed = 5f;
            NPC.velocity = dashDirection * dashSpeed;
        }
    }
}
