using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using System;
using System.Collections;

namespace Sxul.Content.Items.BossLoot
{
    public class SingeSlicer : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 80;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 30; // Duration of the slash
            Projectile.light = 1f;
            Projectile.alpha = 255; // Start invisible
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
        }

        public override void AI()
        {
            // Make the projectile visible
            if (Projectile.timeLeft == 30)
            {
                Projectile.alpha = 0;

                // Number of slashes
                int slashCount = 2;

                // Randomly rotate and position the projectile
                Projectile.rotation = Main.rand.NextFloat(MathHelper.TwoPi);
                Projectile.position += new Vector2(Main.rand.NextFloat(-50f, 50f), Main.rand.NextFloat(-50f, 50f));

                // Create the dust effect forming a line across the projectile
                for (int i = -20; i <= 20; i++)
                {
                    // Position of the dust along the line
                    Vector2 offset = Vector2.UnitX.RotatedBy(Projectile.rotation) * i;
                    Vector2 dustPosition = Projectile.Center + offset;

                    // Create the dust at the calculated position
                    Dust dust = Dust.NewDustPerfect(dustPosition, DustID.FireworkFountain_Red, null, 100, new Color(255, 69, 0), 1.5f);
                    dust.noGravity = true;
                    dust.velocity = Vector2.Zero; // Ensure dust does not move away
                }
            }

            // Fade out and remove the projectile
            Projectile.alpha += 5;
            if (Projectile.alpha >= 255)
            {
                Projectile.Kill();
            }
        }
    }
}
