using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Sxul.Content.Rarities;

namespace Sxul.Content.Items.Weapons.References
{
    internal class CrescentRose : ModItem
    {
        private bool isDashing = false;
        private int dashTimer = 0;
        private const int dashDuration = 40; // Duration of the dashType in frames
        private const float dashSpeed = 23f; // Speed of the dashType
        public override void SetDefaults()
        {
            Item.width = 65;
            Item.height = 65;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 23;
            Item.useAnimation = 23;
            Item.value = Item.buyPrice(1, 34, 99, 99); // Sell price
            Item.UseSound = SoundID.Item101;
            Item.damage = 162;
            Item.knockBack = 0.4f;
            Item.crit = 36;
            Item.autoReuse = true;
            Item.rare = ModContent.RarityType<RainV>();
            Item.DamageType = DamageClass.Melee;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            // Spawn rose petals around the player during melee attacks
            for (int i = 0; i < 4; i++)
            {
                Dust petal = Dust.NewDustDirect(player.position, player.width, player.height, DustID.Firework_Red);
                petal.noGravity = true;
                petal.velocity = new Vector2(Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f));
            }
        }

        private int dashCooldown = 60; // Cooldown time in frames (e.g., 60 frames = 1 second)
        private int currentCooldown = 0; // Timer to track current cooldown

        public override bool AltFunctionUse(Player player)
        {
            // Start the dashType only if not already dashing and cooldown is over
            if (!isDashing && currentCooldown <= 0)
            {
                isDashing = true; // Start dashing
                dashTimer = 0; // Reset the dashType timer
                player.invis = true; // Make the player invisible
            }
            return true; // Indicate that the alternate use action was performed
        }

        public override void UpdateInventory(Player player)
        {
            // Handle the dashType effect
            if (isDashing)
            {
                dashTimer++;
                if (dashTimer < dashDuration)
                {
                    // Dash logic: move player relative to the mouse cursor
                    Vector2 cursorPosition = Main.MouseWorld; // Get the mouse position
                    Vector2 direction = cursorPosition - player.Center; // Calculate direction
                    direction.Normalize(); // Normalize to get unit vector
                    player.velocity = direction * dashSpeed; // Set dashType speed

                    // Make player immune during the dashType
                    player.immune = true; // Set immunity
                    player.immuneTime = dashDuration; // Set immunity time
                    Item.damage = 237;
                }
                else
                {
                    // End the dashType
                    isDashing = false; // Reset dashing state
                    player.invis = false; // Make the player visible again
                    player.immune = false; // Disable immunity

                    // Retain some velocity in the direction of the dashType
                    Vector2 retainedVelocity = player.velocity; // Save the velocity before stopping
                    player.velocity = retainedVelocity * 0.5f; // Retain 50% of the velocity after the dashType ends
                    Item.damage = 162;

                    // Start cooldown
                    currentCooldown = dashCooldown; // Set cooldown
                }
            }
            else
            {
                // The player is visible after the dashType ends
                player.invis = false; // Ensure the player is visible if not dashing
            }

            // Handle cooldown timer
            if (currentCooldown > 0)
            {
                currentCooldown--; // Decrease the cooldown timer
            }
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (hit.Crit)
            {
                // Spawn rose petal explosion
                for (int i = 0; i < 30; i++)
                {
                    Dust petal = Dust.NewDustDirect(target.position, target.width, target.height, DustID.Firework_Red);
                    petal.noGravity = true;
                    petal.scale = 1.8f;
                    petal.velocity = new Vector2(Main.rand.NextFloat(-4f, 4f), Main.rand.NextFloat(-4f, 4f));
                }
            }
        }
    }
}
