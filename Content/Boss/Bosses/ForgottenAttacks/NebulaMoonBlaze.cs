using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Sxul.Content.Boss.Bosses.ForgottenAttacks
{
    public class NebulaMoonBlaze : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 25;
            Projectile.height = 25;
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.penetrate = 3;
            Projectile.timeLeft = 600;
            Projectile.light = 0.7f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            if (Main.rand.NextBool(3)) // This ensures dust is created only 1/3 of the time
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.FireworkFountain_Blue, 0f, 0f, 100, default(Color), 1.5f);
                dust.noGravity = true; // Makes the dust float
                dust.velocity *= 0.5f; // Slows down the dust's velocity
            }

            // If the projectile is in homing mode, track the player
            if (Projectile.aiStyle == 1)
            {
                Player player = Main.player[Projectile.owner];
                Vector2 move = player.Center - Projectile.Center;
                float speed = 10f;
                move.Normalize();
                move *= speed;
                Projectile.velocity = (Projectile.velocity * 20f + move) / 21f;
            }
        }
    }
}
