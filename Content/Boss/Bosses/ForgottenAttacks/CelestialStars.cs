using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Sxul.Content.Boss.Bosses.ForgottenAttacks
{
    public class CelestialStars : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 60;
            Projectile.height = 60;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.tileCollide = false; // Don't collide with tiles
            Projectile.penetrate = 5; // Only one hit
            Projectile.timeLeft = 120; // Duration
        }

        public override void AI()
        {
            // Rotate the projectile to face the direction it's moving
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

            Lighting.AddLight(Projectile.Center, 2f, 2f, 2f);

            // Adding some dust for visual effect (optional)
            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 58, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default, 1f);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            // Inflict some debuffs on the player when hit (optional)
            target.AddBuff(BuffID.OnFire, 180); // Inflicts "On Fire!" debuff for 3 seconds
        }
    }
}
