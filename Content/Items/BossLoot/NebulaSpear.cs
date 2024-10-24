using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using System.Collections;

namespace Sxul.Content.Items.BossLoot
{
    public class NebulaSpear : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.minion = true;
            Projectile.penetrate = 1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 600;
            Projectile.ignoreWater = true;
        }

        public override void AI()
        {
            // Logic for homing onto enemies
            NPC target = FindTarget();
            if (target != null)
            {
                Vector2 direction = target.Center - Projectile.Center;
                direction.Normalize();
                direction *= 10f;
                Projectile.velocity = (Projectile.velocity * 20f + direction) / 21f;
            }
        }

        private NPC FindTarget()
        {
            NPC closestNPC = null;
            float maxDistance = 400f; // Adjust as needed
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
    }
}
