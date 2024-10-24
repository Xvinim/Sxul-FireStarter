using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
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
    internal class BlightOre : ModItem
    {
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

        public override void SetDefaults()
        {
            Item.width = 8;
            Item.height = 8;
            Item.rare = ModContent.RarityType<Material>();
            Item.useTime = 5;
            Item.useAnimation = 5;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.maxStack = 9999;
        }
    }
}
