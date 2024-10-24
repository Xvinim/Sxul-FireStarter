using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Sxul.Content.Projectiles.Player;
using Sxul.Content.Rarities;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Sxul.Systems;

namespace Sxul.Content.Items.Weapons
{
    internal class IvoryBlade : ModItem
    {
        public int critCounter = 0;
        public const int maxCrits = 9;
        private const int ChargeTime = 60;
        private bool isCharging = false; 
        private int chargeTimer = 0;

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 32;
            Item.rare = ModContent.RarityType<Aegis>();
            Item.damage = 42;
            Item.knockBack = 4;
            Item.value = Item.sellPrice(0, 2, 74, 26);
            Item.useTurn = true;
            Item.useTime = 22;
            Item.useAnimation = 22;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.crit = 12;
            Item.DamageType = DamageClass.Melee;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            // Check if it's a critical hit
            if (hit.Crit)
            {
                critCounter++;
                isCharging = true;
                chargeTimer = 0; // Reset charge timer

                // Add the dust around the player for each crit hit
                player.GetModPlayer<IvoryBladePlayer>().showRadianceMeter = true;

                // If the circle is complete, prepare for double damage
                if (critCounter >= maxCrits)
                {
                    critCounter = 0; // Reset crit counter
                    isCharging = false; // Reset charging status
                    player.GetModPlayer<IvoryBladePlayer>().radianceReady = true; // Radiance ready for double damage
                }
            }
        }

        public override void ModifyHitNPC(Player player, NPC target, ref NPC.HitModifiers modifiers)
        {
            // If Radiance is ready, apply double damage and reset the state
            if (player.GetModPlayer<IvoryBladePlayer>().radianceReady)
            {
                modifiers.FinalDamage *= 3; // Apply 2x damage multiplier
                player.GetModPlayer<IvoryBladePlayer>().radianceReady = false; // Reset Radiance
            }
        }
    }
}
