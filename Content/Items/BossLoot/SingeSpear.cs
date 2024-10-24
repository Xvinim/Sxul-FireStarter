using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sxul.Content.Items.BossLoot
{
    public class SingeSpear : ModProjectile
    {
        private const float ProjectileSpeed = 12f; // Speed of the projectile when attacking
        private const int DamageAmount = 104; // Adjust damage as needed
        private const int Knockback = 10; // Adjust knockback as needed
        private const int DetectionRadius = 600; // Radius to detect targets

        public override void SetDefaults()
        {
            Projectile.width = 40; // Width of the projectile
            Projectile.height = 40; // Height of the projectile
            Projectile.friendly = true; // Projectile will damage enemies
            Projectile.hostile = false; // Projectile is not hostile
            Projectile.penetrate = 10; // Number of penetrations before disappearing
            Projectile.timeLeft = 600; // Time before the projectile disappears, set high for indefinite duration
            Projectile.ignoreWater = true; // Ignore water
            Projectile.tileCollide = false; // Do not collide with tiles
        }

        public override void AI()
        {
            // Find the player and set initial position above the player
            Player player = Main.player[Projectile.owner];
            Projectile.position.X = player.Center.X - Projectile.width / 2;
            Projectile.position.Y = player.Center.Y - Projectile.height / 2 - 50; // Adjust vertical offset as needed

            // Check for nearby enemies within detection radius
            NPC targetNpc = null;
            float closestDistance = DetectionRadius;

            foreach (NPC npc in Main.npc)
            {
                if (npc.active && !npc.friendly && !npc.dontTakeDamage)
                {
                    float distance = Vector2.Distance(npc.Center, Projectile.Center);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        targetNpc = npc;
                    }
                }
            }

            if (targetNpc != null)
            {
                // Continuously move towards the closest target
                Vector2 direction = targetNpc.Center - Projectile.Center;
                direction.Normalize();
                Projectile.velocity = direction * ProjectileSpeed;
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            }
            else
            {
                // If no target is found, keep the projectile stationary and follow the player
                Projectile.velocity = Vector2.Zero;
                Projectile.rotation = 0f;
            }
        }

    }
}
