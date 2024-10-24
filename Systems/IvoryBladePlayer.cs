using Microsoft.Xna.Framework;
using Sxul.Content.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Sxul.Systems
{
    internal class IvoryBladePlayer : ModPlayer
    {
        public bool radianceReady = false;
        public bool showRadianceMeter = false;

        public override void PostUpdate()
        {
            // Check if the player has the Ivory Blade in their inventory
            foreach (Item item in Player.inventory)
            {
                if (item.type == ModContent.ItemType<IvoryBlade>())
                {
                    // Add light to the player (adjust intensity and color as needed)
                    Lighting.AddLight(Player.position, 1f, 1f, 0.8f); // White-yellow light effect
                    break;
                }
            }

            if (showRadianceMeter)
            {
                ShowRadianceMeter();
            }
        }

        private void ShowRadianceMeter()
        {
            float radius = 70f; // Radius of the dust circle
            int dustCount = 10; // Number of dust particles
            float angleStep = MathHelper.TwoPi / dustCount; // Evenly spread dust

            for (int i = 0; i < dustCount; i++)
            {
                float angle = angleStep * i; // Angle for each dust particle
                Vector2 dustPosition = Player.Center + new Vector2(radius, 0).RotatedBy(angle); // Position the dust around the player

                // Create the dust
                Dust dust = Dust.NewDustPerfect(dustPosition, DustID.RainbowMk2, null, 100, Color.Red, 1.8f);
                dust.noGravity = true; // Ensure dust floats
                dust.velocity = Vector2.Zero; // Keep the dust in place

                if (radianceReady)
                {
                    dust.color = Color.White;
                }
            }
        }
    }
}
