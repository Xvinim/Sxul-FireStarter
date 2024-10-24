using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Sxul.Content.Rarities;
using Sxul.Content.Items.BossLoot;

namespace Sxul.Content.Boss.TreasureBags
{
	public class ForgottenBossBag : ModItem
	{
		//Defaults
		#region
		public override void SetStaticDefaults()
		{
			// This set is one that every boss bag should have.
			// It will create a glowing effect around the item when dropped in the world.
			// It will also let our boss bag drop dev armor..
			ItemID.Sets.BossBag[Type] = true;
		}

		public override void SetDefaults()
		{
			Item.maxStack = Item.CommonMaxStack;
			Item.consumable = true;
			Item.width = 24;
			Item.height = 24;
			Item.expert = true; // This makes "Expert" be in the tooltip and makes it rainbow rarity, essentially makes it have the demon heart fx
		}

		public override bool CanRightClick()
		{
			return true;
		}
        #endregion

        public override void ModifyItemLoot(ItemLoot itemLoot)
		{
			itemLoot.Add(ItemDropRule.OneFromOptions(1, ModContent.ItemType<Singe>(), ModContent.ItemType<BlackHole>(), ModContent.ItemType<CelestialStar>(), ModContent.ItemType<NewMoon>()));
			itemLoot.Add(ItemDropRule.Common(ItemID.LunarBar, 1, 10, 28));
			itemLoot.Add(ItemDropRule.Common(ItemID.AncientCultistTrophy, 10, 1, 1));
		}
	}
}