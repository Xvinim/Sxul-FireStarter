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
    internal class MurderousSoulTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileMergeDirt[Type] = false;
            Main.tileLighted[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;

            TileID.Sets.Ore[Type] = true;

            AddMapEntry(new Color(119, 225, 40), CreateMapEntryName());
            MineResist = 4f;
            MinPick = 50;
            HitSound = SoundID.AbigailAttack;
            Main.tileSpelunker[Type] = true;
        }

        public override IEnumerable<Item> GetItemDrops(int i, int j)
        {
            yield return new Item(ModContent.ItemType<BlightedSoul>());
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0f;
            g = 2f;
            b = 0.3f;
        }
    }
}
