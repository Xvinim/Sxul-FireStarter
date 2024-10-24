using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Sxul.Content.Rarities;

namespace Sxul.Content.Items.Materials
{
    public class IceFragment : ModItem
    {
        public override void SetDefaults()
        {
            Item.rare = ModContent.RarityType<Love>();
            Item.value = Item.buyPrice(gold: 3);
            Item.noMelee = true;
            Item.maxStack = 9999;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 1;
            Item.useTime = 1;

            Item.width = 25;
            Item.height = 25;
        }
    }
}