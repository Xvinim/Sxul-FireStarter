using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Sxul.Content.Rarities;

namespace Sxul.Content.Items.Weapons.References
{
    internal class BladeOfJudgement : ModItem
    {
        private bool canJab;
        private bool canGrab;
        private int jabTimer;
        private int grabTimer;
        private NPC grabbedTarget; // To track the grabbed target
        private int lifeSapped;

        private enum ComboState { None, Jab, Grab }
        private ComboState currentComboState = ComboState.None;

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;
            Item.damage = 87;
            Item.knockBack = 1.3f;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = Item.buyPrice(1, 2, 99, 99);
            Item.rare = ModContent.RarityType<VoidV>();
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.DamageType = DamageClass.Melee;
        }

        // Modify damage for different combo states
        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            if (currentComboState == ComboState.Jab)
            {
                damage *= 2; // 2x damage for jab attack
            }
            else if (currentComboState == ComboState.Grab)
            {
                damage *= 3; // 3x damage for grab attack
            }
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            // Start jab window
            canJab = true;
            jabTimer = 30; // 0.5 seconds (30 ticks)

            // Create dust effect around target
            CreateComboDust(target);

            // Reset combo state to None after initial hit
            currentComboState = ComboState.None;
        }

        public override void UpdateInventory(Player player)
        {
            if (jabTimer > 0)
            {
                jabTimer--;
            }
            else
            {
                canJab = false; // Disable jab after timer ends
            }

            if (grabTimer > 0)
            {
                grabTimer--;
            }
            else
            {
                canGrab = false; // Disable grab after timer ends
            }

            // Check for right-click input for jab
            if (canJab && Main.mouseRight && jabTimer > 0)
            {
                PerformJab(player);
                canJab = false;
                jabTimer = 0;

                // After jab, set grab timer for 1.5 seconds
                canGrab = true;
                grabTimer = 90; // 1.5 seconds (90 ticks)
            }

            // Check for left-click input for grab
            if (canGrab && Main.mouseLeft && grabTimer > 0)
            {
                PerformGrab(player);
                canGrab = false;
                grabTimer = 0;
            }

            // Handle grab sapping life over time
            if (currentComboState == ComboState.Grab && grabbedTarget != null)
            {
                SapLife(player);
            }
        }

        private void PerformJab(Player player)
        {
            // Perform the jab action
            NPC target = FindClosestNPC(player);
            if (target != null)
            {
                target.velocity.Y = -10f; // Send NPC upwards
                currentComboState = ComboState.Jab; // Set combo state to Jab

                // Create dust effect
                CreateComboDust(target);
            }
        }

        private void PerformGrab(Player player)
        {
            NPC target = FindClosestNPC(player);
            if (target != null)
            {
                grabbedTarget = target;
                currentComboState = ComboState.Grab;

                // Freeze both the player and NPC in the air
                target.velocity = Vector2.Zero;
                player.velocity = Vector2.Zero;

                // Grant invincibility to the player during the grab
                player.immune = true;
                player.immuneTime = 60; // Player is invincible for 1 second (60 ticks)

                // Create dust effect
                CreateComboDust(target);
            }
        }

        private void SapLife(Player player)
        {
            // Gradually sap life from the target and heal the player
            if (grabbedTarget != null && grabbedTarget.active)
            {
                // Keep NPC frozen
                grabbedTarget.velocity = Vector2.Zero;
                player.velocity = Vector2.Zero;

                // Sap 5 life at a time
                if (Main.GameUpdateCount % 20 == 0) // Every 1/3 of a second (20 ticks)
                {
                    // Create HitInfo to deal damage
                    NPC.HitInfo hitInfo = new NPC.HitInfo()
                    {
                        Damage = 5,
                        Crit = false,
                        // You may want to set other properties here based on your needs
                    };

                    // Deal damage to the NPC and heal the player
                    grabbedTarget.StrikeNPC(hitInfo, false, false);
                    player.statLife += 5;
                    player.HealEffect(5);

                    // Stop sapping after 30 life is drained
                    if (player.statLife >= player.statLifeMax2 || lifeSapped >= 30)
                    {
                        grabbedTarget = null;
                        currentComboState = ComboState.None;
                    }
                    else
                    {
                        lifeSapped += 5;
                    }
                }
            }
        }

        private void CreateComboDust(NPC target)
        {
            // Create dust particles around the target during combo windows
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDust(target.position, target.width, target.height, DustID.MagicMirror, Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-2f, 2f), 150, default(Color), 1.5f);
            }
        }

        private NPC FindClosestNPC(Player player)
        {
            NPC closestNPC = null;
            float closestDistance = 100f; // Max range for jab/grab

            foreach (NPC npc in Main.npc)
            {
                if (npc.active && !npc.friendly && npc.Distance(player.Center) < closestDistance)
                {
                    closestDistance = npc.Distance(player.Center);
                    closestNPC = npc;
                }
            }

            return closestNPC;
        }
    }
}
