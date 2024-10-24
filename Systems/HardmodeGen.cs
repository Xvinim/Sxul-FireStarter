using Sxul.Content.Tiles.Ores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.WorldBuilding;
using Microsoft.Xna.Framework;
using Sxul.Content.Items.Accessories.InflictorClass.Fire;
using Sxul.Content.Items.Accessories.InflictorClass.Frost;
using Sxul.Content.Items.Accessories.InflictorClass.Poison;
using Sxul.Content.Items.Accessories.InflictorClass;
using Sxul.Content.Items.Materials;
using Sxul.Content.Items.Weapons;
using Terraria.ModLoader.IO;

namespace Sxul.Systems
{
    internal class HardmodeGen : ModSystem
    {
        private bool blightOreGenerated = false;
        private bool decaySoulGenerated = false;

        public override void LoadWorldData(TagCompound tag)
        {
            if (tag.ContainsKey("blightOreGenerated"))
            {
                blightOreGenerated = tag.GetBool("blightOreGenerated");
            }

            if (tag.ContainsKey("decaySoulGenerated"))
            {
                decaySoulGenerated = tag.GetBool("decaySoulGen");
            }
        }

        public override void SaveWorldData(TagCompound tag)
        {
            tag["blightOreGenerated"] = blightOreGenerated;

            tag["decaySoulGen"] = decaySoulGenerated;
        }

        public override void PostUpdateWorld()
        {
            // Check if it's hardmode and the ore hasn't been generated yet
            if (Main.hardMode && !blightOreGenerated)
            {
                GenerateBlightOre();
                blightOreGenerated = true; // Set the flag to prevent further generation
            }

            if (Main.hardMode && !decaySoulGenerated)
            {
                GenerateDecaying();
                decaySoulGenerated = true;
            }
        }

        private void GenerateBlightOre()
        {
            int maxX = Main.maxTilesX;
            int maxY = Main.maxTilesY;

            // Generate ore in random stone tiles
            for (int i = 0; i < maxX * 0.05; i++) // 5% of world width
            {
                int x = WorldGen.genRand.Next(0, maxX);
                int y = WorldGen.genRand.Next((int)Main.worldSurface + 50, maxY - 200); // Choose a random underground location

                // Only replace stone tiles with blight ore
                if (Main.tile[x, y].TileType == TileID.Stone)
                {
                    WorldGen.TileRunner(x, y, WorldGen.genRand.Next(7, 10), WorldGen.genRand.Next(9, 14), ModContent.TileType<BlightOreTile>());
                }
            }
        }

        private void GenerateDecaying()
        {
            int maxX = Main.maxTilesX;
            int maxY = (int)(Main.rockLayer); // Start at rock layer (underground)
            int minY = (int)(Main.rockLayer - 100); // Underground desert area typically

            float chance = 0.18f; //18% chance of Spawning in Valid Positions

            for (int i = 0; i < maxX * 0.6; i++) // Adjust this multiplier to change ore frequency
            {
                if (WorldGen.genRand.NextFloat() < chance)
                {
                    int x = WorldGen.genRand.Next(200, maxX - 200);
                    int y = WorldGen.genRand.Next((int)(Main.rockLayer - 200), (int)(Main.rockLayer + 700));

                    if (Main.tile[x, y].TileType == TileID.Stone)
                    {
                        WorldGen.TileRunner(x, y, WorldGen.genRand.Next(2, 2), WorldGen.genRand.Next(1, 1), ModContent.TileType<DecayingSoulTile>());
                    }
                }
            }
        }

        private int FindEmptySlotIndex(Chest chest)
        {
            for (int i = 0; i < chest.item.Length; i++)
            {
                if (chest.item[i].type == ItemID.None)
                {
                    return i; // Return the index of the first empty slot
                }
            }
            return -1; // No empty slot found
        }
        //Quicker Item Adding + To Make it Look Clean
        private void AddItemToChest(Chest chest, int itemType, int minStack, int maxStack, int rarityPercentage)
        {
            {
                // Check if the item should be added based on rarity
                if (WorldGen.genRand.Next(0, 100) < rarityPercentage)
                {
                    int slotIndex = FindEmptySlotIndex(chest);

                    if (slotIndex != -1)
                    {
                        chest.item[slotIndex].SetDefaults(itemType);
                        chest.item[slotIndex].stack = WorldGen.genRand.Next(minStack, maxStack + 1); // maxStack is inclusive
                    }
                }
            }
        }
    }
}
