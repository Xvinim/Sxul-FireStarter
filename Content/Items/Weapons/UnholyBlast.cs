using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Sxul.Content.Projectiles.Player;
using Sxul.Content.Rarities;
using Sxul.Content.Items.Materials;

namespace Sxul.Content.Items.Weapons
{
    public class UnholyBlast : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 45;
            Item.crit = 3;
            Item.knockBack = 6;
            Item.DamageType = DamageClass.Magic;
            Item.width = 55;
            Item.height = 55;
            Item.useTime = 23;
            Item.useAnimation = 23;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = Item.buyPrice(gold: 1);
            Item.rare = ModContent.RarityType<Great>();
            Item.UseSound = SoundID.Item1;

            Item.shoot = ModContent.ProjectileType<BlastOfUnholiness>();
            Item.shootSpeed = 8;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<BrainChunk>(), 50);
            recipe.AddIngredient(ItemID.TissueSample, 25);
            recipe.AddIngredient(ItemID.CrimtaneBar, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ModContent.ItemType<BrainChunk>(), 50);
            recipe2.AddIngredient(ItemID.ShadowScale, 25);
            recipe2.AddIngredient(ItemID.DemoniteBar, 10);
            recipe2.AddTile(TileID.Anvils);
            recipe2.Register();
        }
    }
}