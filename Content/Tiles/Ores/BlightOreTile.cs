using Sxul.Content.Items.Materials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Sxul.Systems;

namespace Sxul.Content.Tiles.Ores
{
    internal class BlightOreTile : ModTile
    {
        internal static FramedGlowMask GlowMask;

        public override void SetStaticDefaults()
        {
            GlowMask = new("Sxul/Content/Tiles/Ores/BlightOreTileGlowmask", 18, 18);

            Main.tileLighted[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileOreFinderPriority[Type] = 675;
            Main.tileMergeDirt[Type] = false;

            TileID.Sets.Ore[Type] = true;

            AddMapEntry(new Color(167, 216, 6), CreateMapEntryName());
            MineResist = 2f;
            MinPick = 45;
            HitSound = SoundID.Tink;
            Main.tileSpelunker[Type] = true;
        }

        public override IEnumerable<Item> GetItemDrops(int i, int j)
        {
            yield return new Item(ModContent.ItemType<BlightOre>());
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.03f;
            g = 0.06f;
            b = 0.01f;
        }
    }
}
