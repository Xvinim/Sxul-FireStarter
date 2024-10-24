using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using System.Collections;
using Sxul.Content.Rarities;

namespace Sxul.Content.Items.BossLoot
{
    public class Singe : ModItem
    {
        private const int ChargeTime = 300;
        private int chargeTimer = 0;
        private bool isCharging = false;
        private bool isMagicState = true; // Default to magic state
        private bool mouseMiddleJustPressed = false;

        public override void SetDefaults()
        {
            Item.damage = 100;
            Item.DamageType = DamageClass.Melee;
            Item.width = 40;
            Item.height = 45;
            Item.useTime = 20;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6;
            Item.value = 10000;
            Item.rare = ModContent.RarityType<Aegis>();
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<SingeSlicer>();
            Item.shootSpeed = 0f;
            Item.noUseGraphic = true;
            Item.noMelee = true;
        }

        public override bool? UseItem(Player player)
        {
            if (isMagicState)
            {
                if (!isCharging)
                {
                    int slashCount = 5;
                    for (int i = 0; i < slashCount; i++)
                    {
                        float angle = MathHelper.ToRadians(360f / slashCount * i);
                        Vector2 position = player.Center + Vector2.One.RotatedBy(angle) * 100f;
                        Projectile.NewProjectile(player.GetSource_ItemUse(Item), position, Vector2.Zero, Item.shoot, Item.damage, Item.knockBack, player.whoAmI);
                    }
                }
            }

            return true;
        }

        public override bool AltFunctionUse(Player player)
        {
            if (isMagicState && !isCharging)
            {
                isCharging = true;
                chargeTimer = 0;
                return true;
            }
            else if (!isMagicState)
            {
                // Melee state right-click (RMB) functionality
                ThrowSingeSpear(player);
            }
            return false;
        }

        private void ThrowSingeSpear(Player player)
        {
            // Throw the SingeSpear projectile
            Vector2 position = player.Center + new Vector2(0, -40); // Slightly in front of the player
            Vector2 velocity = Vector2.Normalize(Main.MouseWorld - player.Center) * 10f; // Adjust speed as needed
            Projectile.NewProjectile(player.GetSource_ItemUse(Item), position, velocity, ModContent.ProjectileType<SingeSpear>(), Item.damage, Item.knockBack, player.whoAmI);
        }

        public override void UpdateInventory(Player player)
        {
            if (isCharging)
            {
                chargeTimer++;
                if (chargeTimer >= ChargeTime)
                {
                    // Release the circle effect
                    ReleaseCircleEffect(player);
                    chargeTimer = 0;
                    isCharging = false;
                }
                ShowChargingMeter(player);
            }

            // Toggle state with middle mouse button
            if (Main.mouseMiddle && !mouseMiddleJustPressed)
            {
                isMagicState = !isMagicState;
                mouseMiddleJustPressed = true; // Prevent rapid toggling
            }
            else if (!Main.mouseMiddle)
            {
                mouseMiddleJustPressed = false; // Reset control when the button is released
            }

            if (!isMagicState)
            {
                Item.noUseGraphic = false;
                Item.noMelee = false;
                Item.damage = 634;
                Item.DamageType = DamageClass.Melee;
                Item.rare = ModContent.RarityType<RainV>();
                Item.width = 40;
                Item.height = 45;
                Item.useTime = 25;
                Item.useAnimation = 25;
                Item.useStyle = ItemUseStyleID.Swing;
                Item.knockBack = 8;
            }

            if (isMagicState)
            {
                Item.damage = 100;
                Item.DamageType = DamageClass.Melee;
                Item.width = 40;
                Item.height = 45;
                Item.useTime = 20;
                Item.useAnimation = 10;
                Item.useStyle = ItemUseStyleID.Swing;
                Item.knockBack = 6;
                Item.rare = ModContent.RarityType<Aegis>();
                Item.UseSound = SoundID.Item1;
                Item.autoReuse = true;
                Item.shoot = ModContent.ProjectileType<SingeSlicer>();
                Item.shootSpeed = 0f;
                Item.noUseGraphic = true;
                Item.noMelee = true;
            }
        }

        private void ShowChargingMeter(Player player)
        {
            if (isCharging)
            {
                float chargeProgress = chargeTimer / (float)ChargeTime;
                Color meterColor = Color.Lerp(Color.Red, Color.Green, chargeProgress);

                float radius = 100f;
                int dustCount = 30;
                float angleStep = MathHelper.TwoPi / dustCount;

                for (int i = 0; i < dustCount; i++)
                {
                    float angle = angleStep * i + chargeTimer * 0.05f;
                    Vector2 dustPosition = player.Center + new Vector2(radius, 0).RotatedBy(angle);
                    Dust dust = Dust.NewDustPerfect(dustPosition, DustID.RainbowMk2, null, 100, meterColor, 1.5f);
                    dust.noGravity = true;
                    dust.velocity = Vector2.Zero;
                }

                if (chargeTimer >= ChargeTime)
                {
                    for (int i = 0; i < dustCount; i++)
                    {
                        float angle = angleStep * i + chargeTimer * 0.05f;
                        Vector2 dustPosition = player.Center + new Vector2(radius, 0).RotatedBy(angle);
                        Dust dust = Dust.NewDustPerfect(dustPosition, DustID.RainbowMk2, null, 100, Color.Blue, 2.0f);
                        dust.noGravity = true;
                        dust.velocity = Vector2.Zero;
                    }
                }
            }
        }

        private void ReleaseCircleEffect(Player player)
        {
            Vector2 spawnPosition = player.Center;
            Projectile.NewProjectile(player.GetSource_ItemUse(Item), spawnPosition, Vector2.Zero, ModContent.ProjectileType<CircleEffect>(), 0, 0, player.whoAmI);
        }
    }
}
