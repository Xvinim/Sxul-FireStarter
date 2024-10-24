using SubworldLibrary;
using Sxul.Content.Rarities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Sxul.Content.Items.Materials
{
    internal class WorldReturn : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 13;
            Item.height = 13;
            Item.consumable = true;
            Item.rare = ModContent.RarityType<Legendary>();
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useAnimation = 9;
            Item.useTime = 9;
        }

        public override bool? UseItem(Player player)
        {
            // Trigger the subworld entry here
            if (ModContent.GetInstance<InverseReality>() != null)
            {
                SubworldSystem.Exit();
            }
            return true;
        }

        public override bool ConsumeItem(Player player)
        {
            return false; // This prevents the item from being consumed.
        }
    }
}
