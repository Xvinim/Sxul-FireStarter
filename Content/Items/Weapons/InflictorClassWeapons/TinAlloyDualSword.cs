using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Sxul.Content.Classes;
using Sxul.Content.Rarities;
using Microsoft.Xna.Framework;

namespace Sxul.Content.Items.Weapons.InflictorClassWeapons
{
    public class TinAlloyDualSword : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 35;
            Item.height = 45;
            Item.autoReuse = true;
            Item.damage = 13;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.DamageType = ModContent.GetInstance<InflictorClass>();
            Item.value = Item.sellPrice(silver: 62);
            Item.rare = ModContent.RarityType<Good>();
            Item.UseSound = SoundID.Item1;
            Item.knockBack = 8;
            Item.crit = 2;
            Item.useStyle = ItemUseStyleID.Swing;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ItemID.TinBar, 12);
            r.AddIngredient(ItemID.CopperBar, 3);
            r.AddTile(TileID.Anvils);
            r.Register();
        }
    }
}
