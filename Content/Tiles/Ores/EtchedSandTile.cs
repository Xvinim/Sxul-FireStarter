using Microsoft.Xna.Framework;
using Sxul.Content.Items.Materials;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Sxul.Content.Tiles.Ores
{
    public class EtchedSandTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileLighted[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileOreFinderPriority[Type] = 675;
            Main.tileMergeDirt[Type] = false;

            TileID.Sets.Ore[Type] = true;

            AddMapEntry(new Color(215, 181, 11), CreateMapEntryName());
            MineResist = 2f;
            MinPick = 45;
            HitSound = SoundID.Tink;
            Main.tileSpelunker[Type] = true;
        }

        public override IEnumerable<Item> GetItemDrops(int i, int j)
        {
            yield return new Item(ModContent.ItemType<EtchedSand>());
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.04f;
            g = 0.03f;
            b = 0.01f;
        }
    }
}