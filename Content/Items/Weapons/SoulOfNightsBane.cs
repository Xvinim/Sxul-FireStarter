using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Sxul.Content.Rarities;
using Sxul.Content.Items.Materials;

namespace Sxul.Content.Items.Weapons
{
	public class SoulOfNightsBane : ModItem
	{
		public override void SetDefaults()
		{
			Item.damage = 42;
			Item.DamageType = DamageClass.Melee;
			Item.width = 100;
			Item.height = 110;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 4;
			Item.value = Item.buyPrice(silver: 1);
			Item.rare = ModContent.RarityType<VoidV>();
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}
	}
}
