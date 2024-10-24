using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Sxul.Content.Buffs;
using Sxul.Content.Rarities;

namespace Sxul.Content.Items.Weapons.References
{
    internal class CrystallineSealedBladeMurakumo : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 50; // Width in pixels
            Item.height = 50; // Height in pixels
            Item.damage = 143; // Base damage
            Item.knockBack = 6f; // Knockback
            Item.useStyle = ItemUseStyleID.Swing; // Use style for swinging
            Item.useAnimation = 45; // Use animation duration
            Item.useTime = 45; // Use time
            Item.rare = ModContent.RarityType<RainV>(); // Rarity
            Item.value = Item.buyPrice(1, 13, 99, 99); // Sell price
            Item.autoReuse = true; // Allows the weapon to auto swing
            Item.crit = 10; // Critical hit chance
            Item.DamageType = DamageClass.Melee;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.rand.Next() > 10)
            {
                target.AddBuff(ModContent.BuffType<HitScan>(), 60 * 3); // 3 second duration
            }
        }
    }
}
