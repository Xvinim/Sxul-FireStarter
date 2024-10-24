using Sxul.Content.Classes;
using Sxul.Content.Rarities;
using Sxul.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Sxul.Content.Armor.Vanilla
{
    [AutoloadEquip(EquipType.Head)]
    internal class GildedCopperHelmet : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 13;
            Item.height = 13;
            Item.value = Item.buyPrice(0, 0, 14, 83);
            Item.rare = ModContent.RarityType<Horrid>();
            Item.defense = 1;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemID.CopperChainmail && legs.type == ItemID.CopperGreaves;
        }

        public override void UpdateEquip(Player player)
        {
            Inflictor mp = player.GetModPlayer<Inflictor>();
            mp.iFlames += 0.05f;
        }

        public override void UpdateArmorSet(Player player)
        {
            Inflictor mp = player.GetModPlayer<Inflictor>();
            player.setBonus = "Set Bonus: 5% Base Debuff Chance, Gilded Dash";
            mp.iBaseDebuff += 0.05f;
            GlobalPlayer mps = player.GetModPlayer<GlobalPlayer>();
            mps.gildedCopper = true;
            mps.gildedSetDash = true;
            mps.dashType = GlobalPlayer.Dashes.gilded;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ItemID.CopperBar, 13);
            r.AddTile(TileID.Anvils);
            r.Register();
        }
    }
}
