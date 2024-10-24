//Devil’s Bargain, increases damage at the cost of defense(glass cannon.)
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Sxul.Content.Rarities;
using Terraria.ID;

namespace Sxul.Content.Items.Accessories.Melee
{
    public class DevilsBargain : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28; // The item texture's width
            Item.height = 28; // The item texture's height
            Item.accessory = true; // Makes the item an accessory
            Item.value = Item.sellPrice(gold: 8); // The price when selling the item
            Item.rare = ModContent.RarityType<VoidV>(); // The item's rarity
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            // Increases damage by 15% for all damage types
            player.GetDamage(DamageClass.Generic) += 0.15f;

            // Decreases defense by 20
            player.statDefense -= 20;

            if (!hideVisual)
            {
                for (int i = 0; i < 8; i++) // Increase or decrease the loop count for more or fewer particles
                {
                    // Create a red dust effect
                    int dustIndex = Dust.NewDust(player.position, player.width, player.height, DustID.Smoke, Scale: 0.6f);
                    Main.dust[dustIndex].velocity *= 0.2f; // Slow-moving dust for a wavy effect
                    Main.dust[dustIndex].noGravity = true; // Dust floats in the air
                    Main.dust[dustIndex].color = new Color(255, 0, 0, 200); // Pure red color
                }

                // Add a faint red glow effect around the player
                Lighting.AddLight(player.position, 0.4f, 0.0f, 0.0f); // Red light to enhance the aura
            }
        }
    }
}