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
using Sxul.Content.Items.Materials;

namespace Sxul.Content.Armor.EtchedSet
{
    [AutoloadEquip(EquipType.Head)]
    internal class EtchedGreatHelm : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 13;
            Item.height = 13;
            Item.value = Item.buyPrice(0, 0, 73, 99);
            Item.rare = ModContent.RarityType<Good>();
            Item.defense = 6;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<EtchedChestplate>() && legs.type == ModContent.ItemType<EtchedLeggings>();
        }

        public override void UpdateEquip(Player player)
        {
            Inflictor mp = player.GetModPlayer<Inflictor>();
            mp.iBaseDebuff += 0.10f;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Set Bonus: 3% Magic Damage Boost, Gilded Dash";
            player.GetDamage<MagicDamageClass>() += 0.03f;
            GlobalPlayer mps = player.GetModPlayer<GlobalPlayer>();
            mps.etchedGreatHelm = true;
            mps.gildedSetDash = true;
            mps.dashType = GlobalPlayer.Dashes.gilded;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ModContent.ItemType<EtchedSandBar>(), 16);
            r.AddTile(TileID.Anvils);
            r.Register();
        }
    }
}
