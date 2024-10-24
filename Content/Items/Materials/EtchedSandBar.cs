using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Sxul.Content.Rarities;

namespace Sxul.Content.Items.Materials
{
	public class EtchedSandBar : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 50;
			Item.height = 50;
			Item.useTime = 5;
			Item.useAnimation = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.value = Item.buyPrice(silver: 58);
			Item.rare = ModContent.RarityType<Material>();
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.maxStack = 9999;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<EtchedSand>(), 5);
			recipe.AddTile(17);
			recipe.Register();
		}
	}
}
