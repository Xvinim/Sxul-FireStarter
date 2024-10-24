using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Sxul.Content.Rarities;

namespace Sxul.Content.Items.Accessories.Mixed
{
    public class StimShot : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 25;
            Item.height = 25;
            Item.value = Item.buyPrice(silver: 94);
            Item.rare = ModContent.RarityType<Good>();
            Item.UseSound = SoundID.Item3;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.maxMinions += 2;
            player.maxTurrets += 1;
            player.GetDamage(DamageClass.Melee) += 0.05f;
            player.GetDamage(DamageClass.Magic) += 0.05f;
        }
    }
}
