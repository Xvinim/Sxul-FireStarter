using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Sxul.Content.Projectiles.Player;
using Sxul.Content.Rarities;

namespace Sxul.Content.Items.Weapons
{
    public class SeldomHarbinger : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 43;
            Item.mana = 8;
            Item.DamageType = DamageClass.Magic;

            Item.width = 14;
            Item.height = 14;
            Item.UseSound = SoundID.Item2;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = 18;
            Item.useAnimation = 17;
            Item.value = Item.sellPrice(silver: 74);
            Item.rare = ModContent.RarityType<Mystic>();

            Item.shoot = ModContent.ProjectileType<Harbinger>();
            Item.shootSpeed = 8;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
    }
}