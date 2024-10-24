using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sxul.Content.Boss.Bosses.ForgottenAttacks
{
    public class MoonLordDrop : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 1000; // Adjust size as needed
            Projectile.height = 1000;
            Projectile.aiStyle = 1; // Use default AI style
            Projectile.friendly = false; // It can hit players
            Projectile.hostile = true; // It's not hostile
            Projectile.penetrate = 9999999; // It will disappear after hitting a target
            Projectile.timeLeft = 1000000; // How long the projectile will exist
            Projectile.damage = 100000; // Adjust damage as needed
            Projectile.knockBack = 5f; // Adjust knockback as needed
            Projectile.light = 2.5f; // Add a light effect to the projectile
            Projectile.tileCollide = true; // The projectile will collide with tiles
        }

        public override void AI()
        {
            // Example: Add a glowing effect
            Lighting.AddLight(Projectile.Center, 0.9f, 0.9f, 0.9f); // White light

            // Example: Make the projectile fall from space
            Projectile.velocity.Y += 0.2f; // Gravity effect
        }

        public override void OnKill(int timeLeft)
        {
            // Add an explosion effect when the projectile is destroyed
            for (int i = 0; i < 20; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Lava, 0f, 0f, 0, default, 1f);
            }
        }
    }
}
