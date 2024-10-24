using Sxul.Content.SoulUps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Sxul.Content.Tiles.Ores
{
    internal class EnsnaredSoulTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileMergeDirt[Type] = false;
            Main.tileLighted[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;

            TileID.Sets.Ore[Type] = true;

            AddMapEntry(new Color(215, 133, 10), CreateMapEntryName());
            MineResist = 4f;
            MinPick = 50;
            HitSound = SoundID.AbigailAttack;
            Main.tileSpelunker[Type] = true;
        }

        public override IEnumerable<Item> GetItemDrops(int i, int j)
        {
            yield return new Item(ModContent.ItemType<EnsnaredSoul>());
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 2.2f;
            g = 1.5f;
            b = 0f;
        }
    }
}
