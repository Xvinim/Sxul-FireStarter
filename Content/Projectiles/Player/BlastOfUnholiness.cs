using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System;
using System.Collections;
using Microsoft.Xna.Framework;

namespace Sxul.Content.Projectiles.Player
{
    public class BlastOfUnholiness : ModProjectile
    {
        // Variable to track the target NPC
        private int targetNPC = -1;
        // Variable to store the initial speed of the projectile
        private float initialSpeed;

        public override void SetDefaults()
        {
            Projectile.width = 7;
            Projectile.height = 7;
            Projectile.friendly = true;
            Projectile.penetrate = 3; // Can hit multiple enemies
            Projectile.timeLeft = 60 * 15; // How long the projectile lasts
            Projectile.light = 2.5f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.aiStyle = 27;
        }

        public override void AI()
        {
            // Make the projectile face the right direction
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.Pi;
        }
    }
}
