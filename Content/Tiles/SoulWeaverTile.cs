using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Sxul.Content.Blocks;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace Sxul.Content.Tiles
{
    public class SoulWeaverTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolidTop[Type] = true; // Acts as a solid tile at the top
            Main.tileFrameImportant[Type] = true; // The frame matters (e.g. for furniture)
            Main.tileNoAttach[Type] = true; // Can't attach other tiles to this one
            Main.tileTable[Type] = true; // Can be used like a table for crafting

            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2); // Dimensions of the tile
            TileObjectData.addTile(Type);

            AdjTiles = new int[] { TileID.WorkBenches }; // Acts as a workbench for crafting
        }

        // Optional: Dropping the item when the tile is broken
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Vector2 position = new Vector2(i * 16, j * 16);

            Vector2 randomBox = new Vector2(32, 32);

            var source = new EntitySource_TileBreak(i, j);

            Item.NewItem(source, position, randomBox, ModContent.ItemType<SoulWeaverBlock>());
        }
    }
}