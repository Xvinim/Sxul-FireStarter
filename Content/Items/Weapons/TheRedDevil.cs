using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Sxul.Content.Rarities;

namespace Sxul.Content.Items.Weapons
{
    public class TheRedDevil : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 38;
            Item.crit = 12;
            Item.DamageType = DamageClass.Melee;
            Item.width = 200;
            Item.height = 200;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 14;
            Item.value = Item.buyPrice(gold: 2);
            Item.rare = ModContent.RarityType<Good>();
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;
        }
    }
}
