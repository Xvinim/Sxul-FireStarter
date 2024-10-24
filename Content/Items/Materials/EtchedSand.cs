using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Sxul.Content.Rarities;

namespace Sxul.Content.Items.Materials
{
    public class EtchedSand : ModItem
    {
        public override void SetDefaults()
        {
            Item.height = 13;
            Item.width = 13;
            Item.rare = ModContent.RarityType<Material>();
            Item.value = Item.sellPrice(silver: 20);
            Item.useTime = 5;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.maxStack = 9999;
        }
    }
}