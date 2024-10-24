using Terraria;
using Terraria.ModLoader;
using Sxul.Content.Classes;
using Sxul.Content.Rarities;
using Terraria.ID;

namespace Sxul.Content.Items.Accessories.InflictorClass.Frost
{
    internal class IceRock : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 17;
            Item.height = 17;
            Item.accessory = true;
            Item.rare = ModContent.RarityType<Meh>();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            Inflictor mp = player.GetModPlayer<Inflictor>();

            mp.iBaseDebuff += 0.20f;
            mp.iFrost += 0.10f;
            mp.canFrost = true;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ModContent.ItemType<EvilRock>());
            r.AddIngredient(ModContent.ItemType<IceStone>());
            r.AddTile(TileID.TinkerersWorkbench);
            r.Register();
        }
    }
}
