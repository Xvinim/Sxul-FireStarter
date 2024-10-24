using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Sxul.Content.Rarities;

namespace Sxul.Content.Items.Weapons
{
    public class WorldBane : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 43;
            Item.crit = 3;
            Item.DamageType = DamageClass.Melee;
            Item.width = 500;
            Item.height = 500;
            Item.useTime = 60;
            Item.useAnimation = 60;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 24;
            Item.value = Item.buyPrice(gold: 5);
            Item.rare = ModContent.RarityType<Merf>();
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;
        }
    }
}
