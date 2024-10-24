using Sxul.Content.Classes;
using Sxul.Content.Items.Materials;
using Sxul.Content.Rarities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace Sxul.Content.Armor.EtchedSet
{
    [AutoloadEquip(EquipType.Body)]
    internal class EtchedChestplate : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 13;
            Item.height = 13;
            Item.value = Item.buyPrice(0, 1, 9, 99);
            Item.rare = ModContent.RarityType<Good>();
            Item.defense = 8;
        }

        public override void UpdateEquip(Player player)
        {
            Inflictor mp = player.GetModPlayer<Inflictor>();
            mp.iScorch += 0.04f; //4% Scorch Chance
            mp.canScorch = true;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ModContent.ItemType<EtchedSandBar>(), 23);
            r.AddTile(TileID.Anvils);
            r.Register();
        }
    }
}
