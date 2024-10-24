#region Usings
using Terraria;
using Terraria.ID;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using System;
using System.Collections;
using Sxul;
using Sxul.Content;
using Sxul.Content.Boss;
using Sxul.Content.Boss.TreasureBags;
using Sxul.Content.Boss.Bosses.ForgottenAttacks;
using Sxul.Assets.BossBars;
using Terraria.Achievements;
using Sxul.Content.Boss.Reworks;
#endregion

namespace Sxul.Content.Boss.Bosses
{
    [AutoloadBossHead]
    public class ForgottenCultist : ModNPC
    {
        #region Defaults
        private enum AIStates
        {
            Floating,
            Dashing,
            Teleporting,
            Passive
        }

        private AIStates state;
        private int stateTimer;

        public override void SetDefaults()
        {
            if (Main.masterMode)
            {
                NPC.lifeMax = 61000;
            }
            else if (Main.expertMode)
            {
                NPC.lifeMax = 48000;
            }
            else
            {
                NPC.lifeMax = 32000;
            }
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.CultistBoss];

            NPC.width = 34;
            NPC.height = 34;
            NPC.damage = 100;
            NPC.defense = 37;
            NPC.knockBackResist = 0f;
            NPC.value = Item.buyPrice(7, 77, 77, 77);
            NPC.npcSlots = 10f;
            NPC.boss = true;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath5;
            NPC.aiStyle = -1;
            state = AIStates.Floating;
            stateTimer = 0;

            NPC.BossBar = ModContent.GetInstance<ForgottenCultistBossBar>();

            CustomBossBar cb = ModContent.GetInstance<CustomBossBar>();

            cb.notOverrided = true;

            if (!Main.dedServ)
            {
                Music = MusicLoader.GetMusicSlot(Mod, "Assets/Music/OverlordDescendance");
            }
        }
        #endregion

        #region AI
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
                NPC.velocity.Y -= 0.3f; // Move up if the player is dead or inactive
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
                    if (stateTimer >= 250) // Time for dashing
                    {
                        state = AIStates.Teleporting;
                        stateTimer = 0;
                    }
                    break;

                case AIStates.Teleporting:
                    TeleportAroundPlayer(player);
                    state = AIStates.Passive;
                    stateTimer = 0;
                    break;

                case AIStates.Passive:
                    HoverAroundPlayer(player);
                    if (stateTimer >= 1800) // Time for passive state
                    {
                        state = AIStates.Floating;
                        stateTimer = 0;
                    }
                    break;
            }

            NPC.ai[0]++; // Increment AI counter
            stateTimer++; // Increment state timer

            if (NPC.life < NPC.lifeMax * 0.5f && !NPC.HasBuff(BuffID.MoonLeech))
            {
                LordOfTheMoon();
                NPC.AddBuff(BuffID.MoonLeech, 600); // Cooldown before the attack can be used again
                NPC.defense = 56; // Increase defense when below 50% health
            }

            CelestialRage();

            // Use Savior of the Stars less frequently
            if (Main.rand.Next(600) == 0)
            {
                SaviorOfTheStars();
            }

            if (Main.rand.Next(400) == 0)
            {
                SummonNebulaMoon();
                SummonSolarOrb(); // Summon the minion
                NPC.ai[0] = 1; // Prevent repeated summons
            }

        }

        private void FloatAroundPlayer(Player player)
        {
            Vector2 center = player.Center;
            float radius = 150f;
            float angle = (NPC.ai[0] % 360) * (float)Math.PI / 180f;
            Vector2 targetPosition = center + new Vector2(radius * (float)Math.Cos(angle), radius * (float)Math.Sin(angle));
            Vector2 direction = targetPosition - NPC.Center;
            float speed = 10f;
            direction.Normalize();
            NPC.velocity = (NPC.velocity * (30f - 1f) + direction * speed) / 30f;
        }

        private void HoverAroundPlayer(Player player)
        {
            Vector2 targetPosition = player.Center + new Vector2(0, -200); // Hover further away
            Vector2 direction = targetPosition - NPC.Center;
            float speed = 3f;
            direction.Normalize();
            NPC.velocity = (NPC.velocity * (30f - 1f) + direction * speed) / 30f;
        }

        private void DashTowardsPlayer(Player player)
        {
            Vector2 dashDirection = player.Center - NPC.Center;
            dashDirection.Normalize();
            float dashSpeed = 15f;
            NPC.velocity = dashDirection * dashSpeed;
        }

        private void TeleportAroundPlayer(Player player)
        {
            int TPTimer = 0;
            bool Teleporting;

            // Create dust at the current position before teleporting
            CreateDustEffect(NPC.Center);

            // Determine the new teleport position near the player
            Vector2 teleportPosition = player.Center + new Vector2(Main.rand.Next(-200, 200), Main.rand.Next(-200, 200));

            Teleporting = true;

            // Create dust at the new position after teleporting
            CreateDustEffect(teleportPosition);

            TPTimer += 1;
            
            if (TPTimer <= 3) // Teleport the NPC to the new position
            {
                NPC.Center = teleportPosition;
                TPTimer = 0;
                Teleporting = false;
            }
        }

        private void CreateDustEffect(Vector2 position)
        {
            for (int i = 0; i < 30; i++) // Create 30 dust particles\\
            {
                int dustType = DustID.RainbowMk2; // The dust that does it all lol\\
                Dust dust = Dust.NewDustDirect(position, NPC.width, NPC.height, dustType); //spawns dust on him\\
                dust.velocity *= 2f; //adds velocity\\
                dust.scale = 1.5f; //adds some scale to make it a certain size\\
                dust.noGravity = true; //makes it so it floats fr\\
                dust.color = Color.Red; //makes it a red color cause that's badass for a teleport\\
            }
        }
        #endregion

        #region Attacks
        private void LordOfTheMoon()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 spawnPosition = Main.player[NPC.target].Center - new Vector2(0, 3200);
                int type = ModContent.ProjectileType<MoonLordDrop>();
                int damage = 99999;
                float knockBack = 1000f;

                // Ensure the spawn position is above the player and not colliding with solids
                if (spawnPosition.Y < Main.worldSurface * 16.0)
                {
                    // Use NPC.GetSource_FromAI() to get the IEntitySource
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), spawnPosition, Vector2.Zero, type, damage, knockBack, Main.myPlayer);

                    // Darkening effect
                    Lighting.AddLight(NPC.Center, 1f, 1f, 1f);

                    Main.NewText("Above.. the.. sky is falling.. run!!", Color.Red);
                }
            }
        }

        private int projectileTimer = 0; // Timer variable

        private void CelestialRage()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Player player = Main.player[NPC.target];
                Vector2 startPosition = NPC.Center;
                Vector2 endPosition = player.Center;

                // Update the projectile timer
                projectileTimer++;

                // Fire projectiles at intervals
                if (projectileTimer >= 30) // Adjust the number (60) for the delay in ticks (1 tick = 1/60th of a second)
                {
                    // Create the line projectile
                    Vector2 direction = endPosition - startPosition;
                    direction.Normalize();
                    int lineProjectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), startPosition, direction * 5f, ModContent.ProjectileType<CelestialStars>(), 0, 0, Main.myPlayer);

                    // Create the star projectile after the line has been drawn
                    float delay = 180f; // Time to wait before shooting the star
                    float speed = 10f; // Speed of the star
                    float angle = (endPosition - startPosition).ToRotation();
                    Vector2 starVelocity = new Vector2(speed, 0f).RotatedBy(angle);

                    // Create the star projectile
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), endPosition, starVelocity, ModContent.ProjectileType<CelestialStars>(), 80, 5f, Main.myPlayer);

                    // Reset the timer
                    projectileTimer = 0;
                }
            }
        }

        private void SummonNebulaMoon()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 spawnPosition = NPC.Center + new Vector2(Main.rand.Next(10, 10), Main.rand.Next(-10, -10));
                int type = ModContent.NPCType<NebulaMoon>();
                NPC.NewNPC(NPC.GetSource_FromAI(), (int)spawnPosition.X, (int)spawnPosition.Y, type);
            }
        }

        private void SummonSolarOrb()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 spawnPosition = NPC.Center + new Vector2(Main.rand.Next(10, 10), Main.rand.Next(-10, -10));
                int type = ModContent.NPCType<SolarOrb>();
                NPC.NewNPC(NPC.GetSource_FromAI(), (int)spawnPosition.X, (int)spawnPosition.Y, type);
            }
        }


        private void SaviorOfTheStars()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 spawnPosition = Main.player[NPC.target].Center;
                int type = ProjectileID.FallingStar; // Placeholder for larger stars
                int damage = 80;
                float knockBack = 5f;

                for (int i = 0; i < 18; i++) // Spawn 18 stars
                {
                    Vector2 offset = new Vector2(Main.rand.Next(-400, 400), Main.rand.Next(-600, -200));
                    Vector2 velocity = new Vector2(Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(5f, 15f)); // Random angles and speed
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), spawnPosition + offset, velocity, type, damage, knockBack, Main.myPlayer);
                }

                // Add visual effect
                NPC.color = new Color(255, 0, 255, 128); // Brighter purple with some transparency
                NPC.immune[Main.myPlayer] = 60; // 1 seconds of I-Frames

                // Increase light emitted by the NPC
                Lighting.AddLight(NPC.Center, 0.5f, 0.1f, 0.5f); // Stronger purple light
            }
        }


        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.rand.Next(200) == 0)
            {
                SaviorOfTheStars();
            }
        }
        #endregion

        public override void OnSpawn(IEntitySource source)
        {
            Main.NewText("The Lunatic Cultist returns, seeking revenge...", 156, 0, 0);
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            // Don't need to use an if statement for npcType because it's inside the npc's script already.\\
            if (Main.expertMode)
            {
                npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<ForgottenBossBag>()));
            }
        }

        #region Animation
        public override void FindFrame(int frameHeight)
        {
            // Use frames 4-7 for idle animation
            if (state == AIStates.Floating || state == AIStates.Passive)
            {
                NPC.frameCounter += 1.0; // Controls the speed of the animation

                if (NPC.frameCounter >= 6) // Change frame every 6 ticks (adjust for desired speed)
                {
                    NPC.frame.Y += frameHeight;
                    NPC.frameCounter = 0;
                }

                // Loop the animation back to frame 4 when it reaches frame 7
                if (NPC.frame.Y < 4 * frameHeight || NPC.frame.Y > 7 * frameHeight)
                {
                    {
                        NPC.frame.Y = 4 * frameHeight;
                    }
                }
                // Use frames 8-10 for star-related attacks
                else if (IsUsingStarAttack())
                {
                    NPC.frameCounter += 0.6f; // Controls the speed of the animation

                    if (NPC.frameCounter >= 6) // Change frame every 6 ticks (adjust for desired speed)
                    {
                        NPC.frame.Y += frameHeight;
                        NPC.frameCounter = 0;
                    }

                    // Loop the animation back to frame 8 when it reaches frame 10
                    if (NPC.frame.Y < 8 * frameHeight || NPC.frame.Y > 10 * frameHeight)
                    {
                        NPC.frame.Y = 8 * frameHeight;
                    }
                }
                // Use frames 1-3 for LordOfTheMoon attack
                else if (IsUsingLordOfTheMoon())
                {
                    NPC.frameCounter += 0.1f; // Controls the speed of the animation

                    if (NPC.frameCounter >= 6) // Change frame every 6 ticks (adjust for desired speed)
                    {
                        NPC.frame.Y += frameHeight;
                        NPC.frameCounter = 0;
                    }

                    // Loop the animation back to frame 1 when it reaches frame 3
                    if (NPC.frame.Y < 1 * frameHeight || NPC.frame.Y > 3 * frameHeight)
                    {
                        NPC.frame.Y = 1 * frameHeight;
                    }
                }
                // Use frames 14-16 for teleportation
                else if (state == AIStates.Teleporting)
                {
                    NPC.frameCounter += 0.4f; // Controls the speed of the animation

                    if (NPC.frameCounter >= 6) // Change frame every 6 ticks (adjust for desired speed)
                    {
                        NPC.frame.Y += frameHeight;
                        NPC.frameCounter = 0;
                    }

                    // Loop the animation back to frame 14 when it reaches frame 16
                    if (NPC.frame.Y < 14 * frameHeight || NPC.frame.Y > 16 * frameHeight)
                    {
                        NPC.frame.Y = 14 * frameHeight;
                    }
                    else if (NPC.ai[0] == 0) // If he is summoning
                    {
                        NPC.frame.Y = (int)(NPC.frameCounter / 10 * frameHeight);
                    }
                    else
                    {
                        // Default to frame 0 if not in specific states
                        NPC.frame.Y = 0;
                    }
                }
            }
        }

        // Helper method to check if the NPC is using a star-related attack
        public bool IsUsingStarAttack()
        {
            return NPC.ai[0] == ModContent.ProjectileType<CelestialStars>() ||
            NPC.ai[0] == ProjectileID.FallingStar;
        }

        // Helper method to check if the NPC is using the LordOfTheMoon attack
        public bool IsUsingLordOfTheMoon()
        {
            return NPC.ai[0] == ModContent.ProjectileType<MoonLordDrop>();
        }
        #endregion
    }
}
