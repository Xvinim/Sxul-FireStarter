using Sxul.Content.Classes;
using Sxul.Content.Rarities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Sxul.Content.Items.Accessories.InflictorClass.Fire
{
    internal class HeavenlyBlaze : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 17;
            Item.height = 17;
            Item.accessory = true;
            Item.rare = ModContent.RarityType<HighAegis>();
            Item.value = Item.sellPrice(gold: 4, silver: 63, copper: 64);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            Inflictor mp = player.GetModPlayer<Inflictor>();

            mp.iBaseDebuff += 0.30f;
            mp.iFrost += 0.20f;
            mp.iCursedFire += 0.20f;
            mp.iFlames += 0.20f;
            mp.canCurse = true;
            mp.canFlame = true;
            mp.canFrost = true;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ModContent.ItemType<HeavenlyStone>());
            r.AddIngredient(ModContent.ItemType<TrueFlameTalisman>());
            r.AddTile(TileID.SkyMill);
            r.Register();
        }
    }
}
