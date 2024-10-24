using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using System;
using System.Collections;

namespace Sxul.Content.Boss.Bosses.ForgottenAttacks
{
    public class SolarMeld : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 20;  // Adjust to match your sprite
            Projectile.height = 20; // Adjust to match your sprite
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 300;
            Projectile.damage = 27;
            Projectile.light = 0.5f;
            Projectile.ignoreWater = true;
            Projectile.extraUpdates = 1;
            Projectile.rotation = MathHelper.PiOver2; // Start with 90-degree rotation
        }

        public override void AI()
        {
            Player player = Main.player[(int)Projectile.ai[0]];

            // Calculate direction toward the player
            Vector2 direction = player.Center - Projectile.Center;
            direction.Normalize();

            // Set a fixed speed for the projectile
            float speed = 2.5f; // Adjust speed as needed
            Projectile.velocity = direction * speed;

            // Rotate the projectile to face its movement direction
            Projectile.rotation = Projectile.velocity.ToRotation(); // Ensure the projectile is aligned with its direction

            // Add solar-themed light
            Lighting.AddLight(Projectile.position, 1f, 0.5f, 0.2f);

            // Spawn custom dust for visual effects
            if (Main.rand.NextBool(3))
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.SolarFlare);
            }
        }
    }
}
