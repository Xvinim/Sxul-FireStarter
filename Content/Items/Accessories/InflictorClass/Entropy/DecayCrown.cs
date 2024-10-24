using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Sxul.Content.Classes;
using Sxul.Content.Rarities;
using Sxul.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace Sxul.Content.Items.Accessories.InflictorClass.Entropy
{
    [AutoloadEquip(EquipType.Face)]
    internal class DecayCrown : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 15;
            Item.height = 15;
            Item.accessory = true;
            Item.rare = ModContent.RarityType<Good>();
            Item.value = 6442;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            Inflictor mp = player.GetModPlayer<Inflictor>();

            mp.iEntropy = 0.10f;
            mp.canEntropy = true;
        }

        public static Asset<Texture2D> GlowTexture { get; private set; }

        public override void Load()
        {
            if (!Main.dedServ)
            {
                GlowTexture = ModContent.Request<Texture2D>($"{Texture}Glowmask");
            }
        }

        public override void Unload()
        {
            GlowTexture = null;
        }
    }
}
