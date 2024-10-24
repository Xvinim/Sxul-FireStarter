using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Sxul.Content.Rarities;

namespace Sxul.Content.Items.Weapons
{
	// This is a basic item template.
	// Please see tModLoader's ExampleMod for every other example:
	// https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
	public class Yulah : ModItem
	{
		// The Display Name and Tooltip of this item can be edited in the 'Localization/en-US_Mods.TutorialMod.hjson' file.
		public override void SetDefaults()
		{
			Item.damage = 52;
			Item.crit = 3;
			Item.DamageType = DamageClass.Melee;
			Item.useTime = 6;
			Item.useAnimation = 6;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 0;

			Item.width = 45;
			Item.height = 45;
			Item.value = Item.buyPrice(gold: 3);
			Item.rare = ModContent.RarityType<AtleastUse>();
			Item.UseSound = SoundID.Item1;
		}
	}
}
