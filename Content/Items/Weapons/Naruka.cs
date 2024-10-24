using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Sxul.Content.Rarities;

namespace Sxul.Content.Items.Weapons
{
    public class Naruka : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 22;
            Item.crit = 4;
            Item.DamageType = DamageClass.Melee;
            Item.useTime = 10;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 100;

            Item.width = 80;
            Item.height = 80;
            Item.value = Item.buyPrice(silver: 61);
            Item.rare = ModContent.RarityType<Great>();
            Item.UseSound = SoundID.Item1;
        }
    }
}
