using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Sxul.Content.Rarities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace Sxul.Content.Items.Materials
{
    internal class RadianceBar : ModItem
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
            Item.rare = ModContent.RarityType<Godly>();
            Item.useTime = 5;
            Item.useAnimation = 5;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.maxStack = 9999;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ModContent.ItemType<BlightOre>(), 10);
            r.AddIngredient(ItemID.HallowedBar, 5);
            r.AddTile(TileID.MythrilAnvil);
            r.AddCustomShimmerResult(ModContent.ItemType<Rinstride>(), 3);
            r.AddCustomShimmerResult(ItemID.HallowedBar, 5);
            r.AddCustomShimmerResult(ModContent.ItemType<BlightOre>(), 7);
            r.Register();
        }
    }
}
