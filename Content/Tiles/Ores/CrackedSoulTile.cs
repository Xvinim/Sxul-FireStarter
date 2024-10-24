using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Sxul.Content.Items.Materials;
using Sxul.Content.SoulUps;

namespace Sxul.Content.Tiles.Ores
{
    internal class CrackedSoulTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileMergeDirt[Type] = false;
            Main.tileLighted[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;

            TileID.Sets.Ore[Type] = true;

            AddMapEntry(new Color(225, 247, 0), CreateMapEntryName());
            MineResist = 4f;
            MinPick = 35;
            HitSound = SoundID.AbigailAttack;
            Main.tileSpelunker[Type] = true;
        }

        public override IEnumerable<Item> GetItemDrops(int i, int j)
        {
            yield return new Item(ModContent.ItemType<CrackedSoul>());
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.9f;
            g = 0.4f;
            b = 0.6f;
        }
    }
}
