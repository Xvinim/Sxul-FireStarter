using Sxul.Content.Classes;
using Sxul.Content.Rarities;
using Sxul.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace Sxul.Content.Armor.Vanilla.SilverMask
{
    [AutoloadEquip(EquipType.Head)]
    internal class SilverMask : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 13;
            Item.height = 13;
            Item.value = Item.buyPrice(0, 0, 53, 38);
            Item.rare = ModContent.RarityType<Meh>();
            Item.defense = 4;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemID.SilverChainmail && legs.type == ItemID.SilverGreaves;
        }

        public override void UpdateEquip(Player player)
        {
            player.aggro += 2;
            player.GetDamage<MeleeDamageClass>() += 0.02f;
        }

        public override void UpdateArmorSet(Player player)
        {
            Inflictor mp = player.GetModPlayer<Inflictor>();
            GlobalPlayer mps = player.GetModPlayer<GlobalPlayer>();
            player.setBonus = "Set Bonus: Boosted Speed and Acceleration";
            player.moveSpeed += 0.02f;
            player.runAcceleration += 0.50f;
            mps.dashType = GlobalPlayer.Dashes.invalid;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ItemID.SilverBar, 14);
            r.AddTile(TileID.Anvils);
            r.Register();
        }
    }
}
