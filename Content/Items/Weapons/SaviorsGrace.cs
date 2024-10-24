using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Sxul.Content.Rarities;

namespace Sxul.Content.Items.Weapons
{
    public class SaviorsGrace : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 82;
            Item.crit = 27;
            Item.knockBack = 4;
            Item.DamageType = DamageClass.Melee;
            Item.width = 55;
            Item.height = 55;
            Item.useTime = 16;
            Item.useAnimation = 16;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = Item.buyPrice(gold: 3);
            Item.rare = ModContent.RarityType<Aegis>();
            Item.UseSound = SoundID.Item1;
        }
    }
}
