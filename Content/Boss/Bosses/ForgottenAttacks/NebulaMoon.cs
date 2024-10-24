using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace Sxul.Content.Boss.Bosses.ForgottenAttacks
{
    public class NebulaMoon : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 1; // Set the number of frames if you plan to animate the minion
        }

        public override void SetDefaults()
        {
            NPC.width = 16;
            NPC.height = 16;
            NPC.damage = 0;
            NPC.defense = 10;
            NPC.lifeMax = 500;
            NPC.knockBackResist = 0.5f;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.aiStyle = -1; // Custom AI
            NPC.npcSlots = 0.5f;
            NPC.netAlways = true;
            NPC.DeathSound = SoundID.NPCDeath14;
        }

        public override void AI()
        {
            NPC.TargetClosest(true);
            Player player = Main.player[NPC.target];

            // Rotate and fire projectiles outward
            float rotationSpeed = 0.05f; // Adjust for rotation speed
            NPC.rotation += rotationSpeed;

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (NPC.ai[0] % 15 == 0) // Fire every 15 ticks (1/4 second)
                {
                    for (int i = 0; i < 17; i++) // Fire 10 projectiles in all directions
                    {
                        Vector2 velocity = new Vector2(0, -5).RotatedBy(MathHelper.ToRadians(45 * i)); // 45-degree intervals
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity, ModContent.ProjectileType<NebulaMoonBlaze>(), 20, 1f, Main.myPlayer);
                    }
                }
                NPC.ai[0]++;
            }

            // Slowly move towards the player
            Vector2 moveTo = player.Center - NPC.Center;
            float speed = 2f;
            if (moveTo.Length() > speed)
            {
                moveTo.Normalize();
                moveTo *= speed;
            }
            NPC.velocity = (NPC.velocity * 20f + moveTo) / 21f;

            // Check if the NPC's life is below zero, if so trigger projectile homing
            if (NPC.life <= 0)
            {
                TriggerProjectileHoming();
            }
        }

        private void TriggerProjectileHoming()
        {
            foreach (Projectile proj in Main.projectile)
            {
                if (proj.active && proj.type == ModContent.ProjectileType<NebulaMoonBlaze>())
                {
                    proj.aiStyle = 1; // Change to the homing AI
                    proj.hostile = true;
                    proj.friendly = false;
                    proj.netUpdate = true;
                }
            }
        }

        public override void OnKill()
        {
            TriggerProjectileHoming(); // Ensure projectiles home in on the player when the NPC dies
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                // Add a visual effect when the minion dies
                for (int i = 0; i < 20; i++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.PurpleCrystalShard);
                }
            }
        }
    }
}
