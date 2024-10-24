using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace Sxul.Content.Items.BossLoot
{
    public class VoidArrow : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.damage = 8;
            Projectile.penetrate = 1; // Set this to the number of enemies it can hit
            Projectile.arrow = true; // This makes it behave like an arrow
        }

        public override void AI()
        {
            // Code to make the projectile home in on bosses
            NPC target = FindClosestBoss();
            if (target != null)
            {
                Vector2 direction = target.Center - Projectile.Center;
                direction.Normalize();
                Projectile.velocity = direction * Projectile.velocity.Length();
            }
        }

        private NPC FindClosestBoss()
        {
            NPC closestBoss = null;
            float maxDistance = 50000f; // Adjust the distance as needed

            foreach (NPC npc in Main.npc)
            {
                if (npc.active && npc.boss && Vector2.Distance(Projectile.Center, npc.Center) < maxDistance)
                {
                    closestBoss = npc;
                    maxDistance = Vector2.Distance(Projectile.Center, npc.Center);
                }
            }

            return closestBoss;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            for (int b = 0; b < 7; b++)// Create red dust effect on miss or collision
            {
                int No = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.RainbowMk2, 0f, 0f, 0, Color.Red, 3f);
                Main.dust[No].position = Projectile.Center + Vector2.One.RotatedByRandom(MathHelper.TwoPi) * Projectile.width / 2f;
                Main.dust[No].noGravity = true; // Makes the dust not affected by gravity
            }
            return true; // Return true to allow default collision behavior
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            for (int a = 0; a < 7; a++)// Create green dust effect on hit
            {
                int Yes = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.RainbowMk2, 0f, 0f, 0, Color.Green, 3f);
                Main.dust[Yes].position = Projectile.Center + Vector2.One.RotatedByRandom(MathHelper.TwoPi) * Projectile.width / 2f;
                Main.dust[Yes].noGravity = true; // Makes the dust not affected by gravity
            }
        }

        private void CreateDust()
        {
            // Create dust around the projectile
            for (int c = 0; c < 2; c++) // Number of dust particles
            {
                int dustIndex = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.RainbowMk2, 0f, 0f, 0, Color.White, 1.5f);
                Main.dust[dustIndex].position = Projectile.Center + Vector2.One.RotatedByRandom(MathHelper.TwoPi) * Projectile.width / 2f;
                Main.dust[dustIndex].noGravity = true; // Makes the dust not affected by gravity
            }
        }
    }
}
