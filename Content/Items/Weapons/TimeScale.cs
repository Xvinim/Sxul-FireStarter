using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Sxul.Content.Rarities;

namespace Sxul.Content.Items.Weapons
{
    public class TimeScale : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 46;
            Item.crit = 5;
            Item.knockBack = 2.5f;
            Item.DamageType = DamageClass.Melee;
            Item.width = 15;
            Item.height = 15;
            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = Item.buyPrice(gold: 2);
            Item.rare = ModContent.RarityType<Great>();
            Item.UseSound = SoundID.Item1;
        }
    }
}
