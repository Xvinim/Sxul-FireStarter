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
using Steamworks;

namespace Sxul.Content.Armor.Vanilla.GildedSilver
{
    [AutoloadEquip(EquipType.Head)]
    internal class GildedSilverGreatHelm : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 13;
            Item.height = 13;
            Item.value = Item.buyPrice(0, 0, 53, 38);
            Item.rare = ModContent.RarityType<Meh>();
            Item.defense = 5;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemID.SilverChainmail && legs.type == ItemID.SilverGreaves;
        }

        public override void UpdateEquip(Player player)
        {
            Inflictor mp = player.GetModPlayer<Inflictor>();
            mp.iFrost += 0.05f;
        }

        public override void UpdateArmorSet(Player player)
        {
            Inflictor mp = player.GetModPlayer<Inflictor>();
            GlobalPlayer mps = player.GetModPlayer<GlobalPlayer>();
            player.setBonus = "Set Bonus: +3 Defense, Gilded Dash";
            player.statDefense += 3;
            mps.gildedSilver = true;
            mps.gildedSetDash = true;
            mps.dashType = GlobalPlayer.Dashes.gilded;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ItemID.SilverBar, 17);
            r.AddTile(TileID.Anvils);
            r.Register();
        }
    }
}
