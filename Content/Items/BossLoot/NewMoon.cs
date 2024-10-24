using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Sxul.Content.Boss.Bosses.ForgottenAttacks;
using Microsoft.Xna.Framework;
using Sxul.Content.Rarities;

namespace Sxul.Content.Items.BossLoot
{
    public class NewMoon : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 200; // High damage
            Item.DamageType = DamageClass.Magic; // Magic weapon
            Item.mana = 230; // High mana cost
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true; // No melee hitbox
            Item.value = Item.sellPrice(gold: 10);
            Item.rare = ModContent.RarityType<Mystic>();
            Item.UseSound = SoundID.Item20;
            Item.shoot = ModContent.ProjectileType<MoonLordDrop>();
            Item.shootSpeed = 16f; // Speed of the projectile
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            LordOfTheMoon(player);

            return false; // Return false to prevent the game from firing the default projectile
        }

        public override bool CanUseItem(Player player)
        {
            // Optional: Prevent the player from using the weapon if they don't have enough mana
            return player.statMana >= Item.mana;
        }

        public override void OnConsumeMana(Player player, int manaConsumed)
        {
            player.AddBuff(ModContent.BuffType<NewMoonManaDebuff>(), 600); // 1-Minute debuff
        }

        private void LordOfTheMoon(Player player)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                // Ensure the player is valid
                if (player != null && player.active)
                {
                    // Get the mouse position in world coordinates
                    Vector2 mousePosition = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);

                    // Spawn position is above the mouse by 600 units
                    Vector2 spawnPosition = mousePosition - new Vector2(0, 1600);

                    int type = ModContent.ProjectileType<MoonLordDrop>();
                    int damage = 1647;
                    float knockBack = 100f;

                    // Use Item.GetSource_FromThis() to get the IEntitySource
                    int projID = Projectile.NewProjectile(Item.GetSource_FromThis(), spawnPosition, Vector2.Zero, type, damage, knockBack, player.whoAmI);

                    // Now modify the projectile's properties
                    Projectile projectile = Main.projectile[projID];
                    if (projectile.type == ModContent.ProjectileType<MoonLordDrop>())
                    {
                        projectile.friendly = true; // Make it friendly
                        projectile.hostile = false; // Ensure it won't harm the player or allies
                        projectile.owner = player.whoAmI; // Set the owner to the player
                    }

                    Main.NewText("With this Sacred Treasure I Summon", Color.Teal);
                }
            }
        }
    }
}
