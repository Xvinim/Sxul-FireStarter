using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using System.Collections;

namespace Sxul.Content.Items.BossLoot
{
    public class NewNebulaMoon : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 34;
            Projectile.height = 34;
            Projectile.friendly = true;
            Projectile.minion = true;
            Projectile.minionSlots = 1;
            Projectile.penetrate = -1; // Infinite penetration (doesn't disappear on hit)
            Projectile.tileCollide = false; // Does not collide with tiles
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 18000; // Minion lasts for 5 minutes
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            // Make sure the minion follows the player
            if (player.dead || !player.active)
            {
                player.ClearBuff(ModContent.BuffType<NewNebulaMoonBuff>());
            }
            if (player.HasBuff(ModContent.BuffType<NewNebulaMoonBuff>()))
            {
                Projectile.timeLeft = 2;
            }

            FollowPlayer(player);

            // Check for nearby enemies and shoot if one is found
            NPC target = FindTarget();
            if (target != null)
            {
                ShootAtTarget(target);
            }
        }

        private void FollowPlayer(Player player)
        {
            Vector2 idlePosition = player.Center;
            idlePosition.Y -= 48f; // Position above the player

            Vector2 vectorToIdlePosition = idlePosition - Projectile.Center;
            float distanceToIdlePosition = vectorToIdlePosition.Length();

            if (distanceToIdlePosition > 600f)
            {
                // Teleport to player if too far away
                Projectile.position = idlePosition;
                Projectile.velocity *= 0.1f;
            }
            else if (distanceToIdlePosition > 200f)
            {
                // Move towards player
                vectorToIdlePosition.Normalize();
                Projectile.velocity = vectorToIdlePosition * 8f;
            }
            else
            {
                // Stay near the player
                Projectile.velocity *= 0.95f;
            }
        }

        private NPC FindTarget()
        {
            NPC closestNPC = null;
            float maxDistance = 800f; // Adjust as needed for detection range
            foreach (NPC npc in Main.npc)
            {
                if (npc.CanBeChasedBy() && Vector2.Distance(Projectile.Center, npc.Center) < maxDistance)
                {
                    maxDistance = Vector2.Distance(Projectile.Center, npc.Center);
                    closestNPC = npc;
                }
            }
            return closestNPC;
        }

        private void ShootAtTarget(NPC target)
        {
            // Fire spears at the target
            if (Projectile.ai[0]++ % 60 == 0) // Fires every second
            {
                Vector2 directionToTarget = target.Center - Projectile.Center;
                directionToTarget.Normalize();
                directionToTarget *= 10f; // Speed of the projectile

                for (int i = 0; i < 360; i += 180) // Adjust to fire in different directions
                {
                    Vector2 direction = directionToTarget.RotatedBy(MathHelper.ToRadians(i));
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, direction, ModContent.ProjectileType<NebulaSpear>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
                }
            }
        }
    }
}
