using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace Sxul.Content.Projectiles.Player
{
    public class Harbinger : ModProjectile
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
            Projectile.penetrate = -1; // Can hit multiple enemies
            Projectile.timeLeft = 60 * 10; // How long the projectile lasts
            Projectile.light = 0.5f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.aiStyle = 0; // Custom AI
            AIType = ProjectileID.None;

            // Store the initial speed of the projectile
            initialSpeed = Projectile.velocity.Length();
        }

        public override void AI()
        {
            if (targetNPC != -1)
            {
                NPC target = Main.npc[targetNPC];
                if (target.active)
                {
                    // Calculate the direction to the target
                    Vector2 direction = target.Center - Projectile.Center;
                    direction.Normalize();
                    // Update the projectile's velocity to move towards the target with increased speed
                    Projectile.velocity = direction * (initialSpeed * 1.05f); // Increase speed by 5%
                }
                else
                {
                    // Target is no longer active; reset targetNPC
                    targetNPC = -1;
                }
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            // Lock onto the first enemy hit
            if (targetNPC == -1)
            {
                targetNPC = target.whoAmI;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            // Reflect the projectile's velocity upon collision
            Projectile.velocity = Vector2.Reflect(oldVelocity, Projectile.velocity);
            // Continue tracking the target
            return false; // Return false to continue moving
        }
    }
}
