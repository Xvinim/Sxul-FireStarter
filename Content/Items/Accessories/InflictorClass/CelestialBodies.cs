using Sxul.Content.Classes;
using Sxul.Content.Items.Accessories.InflictorClass.Confusion;
using Sxul.Content.Items.Accessories.InflictorClass.Poison;
using Sxul.Content.Rarities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Sxul.Content.Items.Accessories.InflictorClass
{
    internal class CelestialBodies : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 17;
            Item.height = 17;
            Item.accessory = true;
            Item.rare = ModContent.RarityType<VoidV>();
            Item.value = Item.sellPrice(gold: 6, silver: 99, copper: 99);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            Inflictor mp = player.GetModPlayer<Inflictor>();

            mp.iBaseDebuff += 0.30f;
            mp.iConfusion += 0.15f;
            mp.iCursedFire += 0.15f;
            mp.iFlames += 0.15f;
            mp.iFrost += 0.15f;
            mp.iPoison += 0.15f;
            mp.canFlame = true;
            mp.canFrost = true;
            mp.canPoison = true;
            mp.canConfuse = true;
            mp.canCurse = true;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ModContent.ItemType<TrueFlameTalisman>());
            r.AddIngredient(ModContent.ItemType<EvilCell>());
            r.AddIngredient(ModContent.ItemType<PoisonStone>());
            r.AddTile(TileID.MythrilAnvil);
            r.Register();
        }
    }
}
