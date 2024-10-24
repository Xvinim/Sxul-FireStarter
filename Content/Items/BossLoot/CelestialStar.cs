using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using System.Collections;
using Sxul.Content.Rarities;

namespace Sxul.Content.Items.BossLoot
{
    public class CelestialStar : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 50;  // Adjust damage as needed.
            Item.DamageType = DamageClass.Summon;
            Item.knockBack = 2f;
            Item.mana = 10;  // Mana cost
            Item.width = 28;
            Item.height = 28;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = Item.sellPrice(gold: 10);
            Item.rare = ModContent.RarityType<Aegis>();
            Item.UseSound = SoundID.Item44;
            Item.noMelee = true; // Summon weapon, no melee hitbox
            Item.buffType = ModContent.BuffType<NewNebulaMoonBuff>();  // Buff applied when used
            Item.shoot = ModContent.ProjectileType<NewNebulaMoon>(); // The minion projectile
        }

        public override bool AltFunctionUse(Player player)
        {
            // If you want to add a right-click function in the future
            return false;
        }
    }
}
