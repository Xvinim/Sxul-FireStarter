using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Sxul.Content.Rarities;

namespace Sxul.Content.Items.BossLoot
{
    public class BlackHole : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 436; // Adjust as needed
            Item.DamageType = DamageClass.Ranged;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.value = Item.sellPrice(gold: 12);
            Item.rare = ModContent.RarityType<Merf>();
            Item.UseSound = SoundID.Item5;
            Item.shoot = ModContent.ProjectileType<VoidArrow>();
            Item.shootSpeed = 16f;
            Item.autoReuse = true;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true; // Allows the right-click functionality
        }

        public override bool Shoot(Player player, Terraria.DataStructures.EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2) // Right-click functionality
            {
                ShootBehindBoss(player);
                return false; // Prevent default shooting
            }

            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }

        private void ShootBehindBoss(Player player)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC target = FindClosestBoss(player);

                if (target != null)
                {
                    Vector2 direction = target.Center - player.Center;
                    Vector2 behindPosition = target.Center - direction * 2f; // Position behind the boss
                    int type = ModContent.ProjectileType<VoidArrow>();
                    int damage = Item.damage;
                    float knockBack = Item.knockBack;

                    if (Main.rand.NextFloat() >= 0.75f) // 25% chance to hit
                    {
                        Projectile.NewProjectile(Item.GetSource_FromThis(), behindPosition, direction, type, damage, knockBack, player.whoAmI);
                    }
                    else
                    {
                        // 75% chance to miss (you might want to add some visual or audio feedback here)
                        Main.NewText("The shot missed!", Color.Teal);
                    }
                }
            }
        }

        private NPC FindClosestBoss(Player player)
        {
            NPC closestBoss = null;
            float maxDistance = 5000f; // Adjust the distance as needed

            foreach (NPC npc in Main.npc)
            {
                if (npc.active && npc.boss && Vector2.Distance(player.Center, npc.Center) < maxDistance)
                {
                    closestBoss = npc;
                    maxDistance = Vector2.Distance(player.Center, npc.Center);
                }
            }

            return closestBoss;
        }
    }
}
