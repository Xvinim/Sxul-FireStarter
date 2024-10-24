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

namespace Sxul.Content.Armor.Vanilla.GildedTungsten
{
    [AutoloadEquip(EquipType.Head)]
    internal class GildedTungstenGreatHelm : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 13;
            Item.height = 13;
            Item.value = Item.buyPrice(0, 0, 58, 39);
            Item.rare = ModContent.RarityType<Meh>();
            Item.defense = 4;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemID.TungstenChainmail && legs.type == ItemID.TungstenGreaves;
        }

        public override void UpdateEquip(Player player)
        {
            Inflictor mp = player.GetModPlayer<Inflictor>();
            mp.iFrost += 0.06f;
        }

        public override void UpdateArmorSet(Player player)
        {
            Inflictor mp = player.GetModPlayer<Inflictor>();
            GlobalPlayer mps = player.GetModPlayer<GlobalPlayer>();
            player.setBonus = "Set Bonus: +4 Defense, Gilded Dash";
            player.statDefense += 4;
            mps.gildedSilver = true;
            mps.gildedSetDash = true;
            mps.dashType = GlobalPlayer.Dashes.gilded;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ItemID.TungstenBar, 17);
            r.AddTile(TileID.Anvils);
            r.Register();
        }
    }
}
