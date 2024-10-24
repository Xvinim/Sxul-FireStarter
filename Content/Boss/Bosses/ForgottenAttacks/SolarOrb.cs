using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using System;
using System.Collections;

namespace Sxul.Content.Boss.Bosses.ForgottenAttacks
{
    public class SolarOrb : ModNPC
    {
        public override void SetDefaults()
        {
            NPC.width = 16; // Adjust size as needed
            NPC.height = 16;
            NPC.damage = 7;
            NPC.defense = 3;
            NPC.lifeMax = 300;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.noGravity = true; // To make the orb float
            NPC.noTileCollide = true; // To avoid collision with tiles
            NPC.knockBackResist = 0.2f;
            NPC.aiStyle = -1; // Custom AI
            NPC.value = Item.buyPrice(0, 0, 49, 57); // Adjust value as needed
        }

        public override void AI()
        {
            NPC.TargetClosest(true); // Target the closest player
            Player player = Main.player[NPC.target];

            NPC.rotation += 0.1f; // Spin the orb

            Vector2 direction = player.Center - NPC.Center;
            direction.Normalize();

            // Slowly move toward the player
            NPC.velocity = direction * 1f;

            // Shoot Terra Blade-like slashes
            if (Main.rand.NextBool(25)) // Adjust frequency as needed
            {
                // Fire in the direction of the player
                Vector2 projectileDirection = direction * 10f; // Adjust speed as needed
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, projectileDirection, ModContent.ProjectileType<SolarMeld>(), NPC.damage, 2f, Main.myPlayer, NPC.target);
            }
        }
    }
}
