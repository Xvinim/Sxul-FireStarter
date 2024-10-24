using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Sxul.Content.Rarities;
using Microsoft.Xna.Framework;
using Sxul.Assets;
using Terraria.Audio;

namespace Sxul.Content.Items.Weapons
{
	// This is a basic item template.
	// Please see tModLoader's ExampleMod for every other example:
	// https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
	public class Xenith : ModItem
	{
		// The Display Name and Tooltip of this item can be edited in the 'Localization/en-US_Mods.TutorialMod.hjson' file.
		public override void SetDefaults()
		{
			Item.damage = 842;
			Item.crit = 6;
			Item.DamageType = DamageClass.Melee;
			Item.width = 35;
			Item.height = 35;
			Item.useTime = 15;
			Item.useAnimation = 35;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 100;
			Item.value = Item.buyPrice(platinum: 50);
			Item.rare = ModContent.RarityType<Godly>();
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;
		}

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            hitbox.X -= 11; // Adjust the X position
            hitbox.Y -= 8; // Adjust the Y position
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-17, 4);
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Zenith, 5);
			recipe.AddTile(TileID.BlueFairyJar);
			recipe.AddTile(TileID.GreenFairyJar);
			recipe.AddTile(TileID.PinkFairyJar);
			recipe.Register();
		}
	}
}
