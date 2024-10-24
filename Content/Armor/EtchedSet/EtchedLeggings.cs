using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Sxul.Content.Rarities;
using Sxul.Content.Classes;
using Sxul.Content.Items.Materials;

namespace Sxul.Content.Armor.EtchedSet
{
    [AutoloadEquip(EquipType.Legs)]
    internal class EtchedLeggings : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 13;
            Item.height = 13;
            Item.value = Item.buyPrice(0, 0, 92, 28);
            Item.rare = ModContent.RarityType<Good>();
            Item.defense = 5;
        }

        public override void UpdateEquip(Player player)
        {
            Inflictor mp = player.GetModPlayer<Inflictor>();
            mp.iScorch += 0.08f; //8% Scorch Chance
            mp.canScorch = true;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ModContent.ItemType<EtchedSandBar>(), 15);
            r.AddTile(TileID.Anvils);
            r.Register();
        }
    }
}
