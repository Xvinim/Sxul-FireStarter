//usings
#region
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using Sxul.Content.Tiles.Ores;
using Microsoft.Xna.Framework;
using Sxul.Content.Items.Materials;
using Terraria.UI;
using Sxul.Content.Items.Accessories.Melee;
using Sxul.Content.Items.Accessories.InflictorClass;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Sxul.Content.Items.Accessories.InflictorClass.Fire;
using Sxul.Content.Items.Weapons;
using Sxul.Content.Items.Accessories.InflictorClass.Poison;
using Sxul.Content.Items.Accessories.InflictorClass.Frost;
using Sxul.Content.SoulUps;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Generation;
#endregion

namespace Sxul
{
    internal class SxulWorldGen : ModSystem
    {
        //WorldGen
        #region
        private List<Vector2> oreLocations = new List<Vector2>();

        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
        {
            int genIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));

            int genIndex1 = tasks.FindIndex(genpass => genpass.Name.Equals("Final Cleanup"));

            if (genIndex != -1)
            {
                tasks.Insert(genIndex + 1, new PassLegacy("Etched Sand Generation", EtchedSandGen));

                tasks.Insert(genIndex + 2, new PassLegacy("SecondPassEnsureOreSpread", EnsureOreSpread));
            }

            if (genIndex1 != -1)
            {
                tasks.Insert(genIndex1 + 1, new PassLegacy("Pre-Hardmode Soul Gen", CrackedSoul));

                tasks.Insert(genIndex + 2, new PassLegacy("Blighted Soul Gen", BlightedSoul));

                tasks.Insert(genIndex + 3, new PassLegacy("Ensnared Soul Gen", EnsnaredSoul));
            }
        }

        private void EtchedSandGen(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = "Etching The Sand...";

            // Define the generation bounds (Underground Desert)
            int maxX = Main.maxTilesX;
            int maxY = (int)(Main.rockLayer); // Start at rock layer (underground)
            int minY = (int)(Main.rockLayer - 100); // Underground desert area typically

            for (int i = 0; i < maxX * 0.6; i++) // Adjust this multiplier to change ore frequency
            {
                // Randomly choose a position inside the Underground Desert
                int x = WorldGen.genRand.Next(200, maxX - 200); // Desert typically starts a bit inward from the edge
                int y = WorldGen.genRand.Next((int)(Main.rockLayer - 200), (int)(Main.rockLayer + 700));

                // Ensure this position is in the Underground Desert biome
                if (Main.tile[x, y].TileType == TileID.Sandstone || Main.tile[x, y].TileType == TileID.HardenedSand)
                {
                    // Generate a cluster of ore
                    WorldGen.TileRunner(x, y, WorldGen.genRand.Next(2, 10), WorldGen.genRand.Next(5, 9), ModContent.TileType<EtchedSandTile>());
                }
            }
        }

        private void EnsureOreSpread(GenerationProgress progress, GameConfiguration config)
        {
            progress.Message = "Extra Etching of Sand...";

            int maxX = Main.maxTilesX;
            int minY = (int)(Main.rockLayer - 100);
            int maxY = (int)(Main.rockLayer);

            // Second pass: Ensure there are no gaps larger than 25 tiles between clusters
            for (int x = 200; x < maxX - 200; x += 25) // Step through the world in 25-tile increments
            {
                for (int y = minY; y < maxY; y += 25)
                {
                    bool clusterNearby = false;

                    // Check if there's a cluster within 25 tiles
                    foreach (Vector2 orePos in oreLocations)
                    {
                        if (Vector2.Distance(new Vector2(x, y), orePos) < 25)
                        {
                            clusterNearby = true;
                            break;
                        }
                    }

                    // If no cluster is nearby, add one
                    if (!clusterNearby)
                    {
                        // Ensure the new cluster is within the Underground Desert biome
                        if (Main.tile[x, y].TileType == TileID.Sandstone || Main.tile[x, y].TileType == TileID.HardenedSand)
                        {
                            WorldGen.TileRunner(x, y, WorldGen.genRand.Next(4, 9), WorldGen.genRand.Next(5, 10), ModContent.TileType<EtchedSandTile>());
                            oreLocations.Add(new Vector2(x, y)); // Add the new cluster's location
                        }
                    }
                }
            }
        }

        private void CrackedSoul(GenerationProgress progress, GameConfiguration config)
        {
            progress.Message = "Souls Being Scattered";

            // Define the generation bounds (Underground Desert)
            int maxX = Main.maxTilesX;
            int maxY = (int)(Main.rockLayer); // Start at rock layer (underground)
            int minY = (int)(Main.rockLayer - 100); // Underground desert area typically

            float chance = 0.09f; //9% chance of Spawning in Valid Positions

            for (int i = 0; i < maxX * 0.6; i++) // Adjust this multiplier to change ore frequency
            {
                if (WorldGen.genRand.NextFloat() < chance)
                {
                    int x = WorldGen.genRand.Next(200, maxX - 200);
                    int y = WorldGen.genRand.Next((int)(Main.rockLayer - 200), (int)(Main.rockLayer + 700));

                    if (Main.tile[x, y].TileType == TileID.Stone)
                    {
                        WorldGen.TileRunner(x, y, WorldGen.genRand.Next(2, 2), WorldGen.genRand.Next(1, 1), ModContent.TileType<CrackedSoulTile>());
                    }
                }
            }
        }

        private void BlightedSoul(GenerationProgress progress, GameConfiguration config)
        {
            progress.Message = "Souls Being Scattered";

            // Define the generation bounds (Underground Desert)
            int maxX = Main.maxTilesX;
            int maxY = (int)(Main.rockLayer); // Start at rock layer (underground)
            int minY = (int)(Main.rockLayer - 100); // Underground desert area typically

            float chance = 0.13f; //13% chance of Spawning in Valid Positions

            for (int i = 0; i < maxX * 0.6; i++) // Adjust this multiplier to change ore frequency
            {
                if (WorldGen.genRand.NextFloat() < chance)
                {
                    int x = WorldGen.genRand.Next(200, maxX - 200);
                    int y = WorldGen.genRand.Next((int)(Main.rockLayer - 200), (int)(Main.rockLayer + 700));

                    if (Main.tile[x, y].TileType == TileID.Stone)
                    {
                        WorldGen.TileRunner(x, y, WorldGen.genRand.Next(2, 2), WorldGen.genRand.Next(1, 1), ModContent.TileType<BlightedSoulTile>());
                    }
                }
            }
        }

        private void EnsnaredSoul(GenerationProgress progress, GameConfiguration config)
        {
            progress.Message = "Souls Being Scattered";

            // Define the generation bounds (Underground Desert)
            int maxX = Main.maxTilesX;
            int maxY = (int)(Main.rockLayer); // Start at rock layer (underground)
            int minY = (int)(Main.rockLayer - 100); // Underground desert area typically

            float chance = 0.13f; //13% chance of Spawning in Valid Positions

            for (int i = 0; i < maxX * 0.6; i++) // Adjust this multiplier to change ore frequency
            {
                if (WorldGen.genRand.NextFloat() < chance)
                {
                    int x = WorldGen.genRand.Next(200, maxX - 200);
                    int y = WorldGen.genRand.Next((int)(Main.rockLayer - 200), (int)(Main.rockLayer + 700));

                    if (Main.tile[x, y].TileType == TileID.Stone)
                    {
                        WorldGen.TileRunner(x, y, WorldGen.genRand.Next(2, 2), WorldGen.genRand.Next(1, 1), ModContent.TileType<EnsnaredSoulTile>());
                    }
                }
            }
        }


        #endregion

        //Chest Editing(Adding Loot)
        #region
        public override void PostWorldGen()
        {
            // Loop through all chests in the world
            for (int i = 0; i < Main.maxChests; i++)
            {
                Chest chest = Main.chest[i];

                // If chest exists
                if (chest != null)
                {
                    // Check if the chest is a Gold Chest (Dungeon Chest)
                    if (Main.tile[chest.x, chest.y].TileType == TileID.Containers)
                    {
                        // Check if the chest is surrounded by Dungeon Bricks
                        if (IsDungeonChest(chest.x, chest.y))
                        {
                            // Find the index of the first empty slot in the chest
                            int slotIndex = FindEmptySlotIndex(chest);

                            if (slotIndex != -1)
                            {
                                //Rinstride
                                int item1 = ModContent.ItemType<Rinstride>(); // Replace with your custom item
                                AddItemToChest(chest, item1, 7, 26, 100);
                                //Ivory Blade
                                int item2 = ModContent.ItemType<IvoryBlade>();
                                AddItemToChest(chest, item2, 1, 1, 10);
                            }
                        }
                        // Check if it's a normal chest
                        else if (IsCaveChest(chest.x, chest.y))
                        {
                            int slotIndex = FindEmptySlotIndex(chest);

                            if (slotIndex != -1)
                            {
                                //Engraved Stone
                                int item1 = ModContent.ItemType<EngravedStone>();
                                AddItemToChest(chest, item1, 1, 1, 65);
                            }
                        }
                        else if (IsJungleChest(chest.x, chest.y))
                        {
                            int slotIndex = FindEmptySlotIndex(chest);

                            if (slotIndex != -1)
                            {
                                int item1 = ModContent.ItemType<VineClump>();
                                AddItemToChest(chest, item1, 1, 1, 45);
                            }
                        }
                        else if (IsShadowChest(chest.x, chest.y))
                        {
                            int slotIndex = FindEmptySlotIndex(chest);

                            if(slotIndex != -1)
                            {
                                int item1 = ModContent.ItemType<FlameTalisman>();
                                AddItemToChest(chest, item1, 1, 1, 25);
                            }
                        }
                        else if (IsSnowChest(chest.x, chest.y))
                        {
                            int slotIndex = FindEmptySlotIndex(chest);

                            if (slotIndex != -1)
                            {
                                int item1 = ModContent.ItemType<IceStone>();
                                AddItemToChest(chest, item1, 1, 1, 75);
                            }
                        }
                    }
                }
            }
        }
        #endregion

        //Chest Checking Functions
        #region
        private bool IsDungeonChest(int x, int y)
        {
            // Loop through a 1-tile radius around the chest
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int tileType = Main.tile[x + i, y + j].TileType;

                    // Check if the tile is any type of Dungeon Brick
                    if (tileType == TileID.BlueDungeonBrick || tileType == TileID.GreenDungeonBrick || tileType == TileID.PinkDungeonBrick)
                    {
                        return true; // Dungeon Brick found near the chest
                    }
                }
            }

            return false; // No Dungeon Bricks nearby
        }

        private bool IsCaveChest(int x, int y)
        {
            // Loop through a 1-tile radius around the chest
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int tileType = Main.tile[x + i, y + j].TileType;

                    // Check if the tile is any type of Stone\Dirt
                    if (tileType == TileID.Stone || tileType == TileID.Dirt || tileType == TileID.WoodBlock)
                    {
                        return true; // Dirt or Stone found near the chest
                    }
                }
            }

            return false; //No Stone or Dirt nearby
        }

        private bool IsWaterChest(int x, int y)
        {
            // Loop through a 1-tile radius around the chest
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    Tile tile = Main.tile[x + i, y + j];
                    int tileType = Main.tile[x + i, y + j].TileType;

                    // Check if the tile contains water (LiquidType 0 means water)
                    if (tile.LiquidAmount > 0 && tile.LiquidType == 0 || tileType != TileID.Sand)
                    {
                        return true; // Water found near the chest
                    }
                }
            }

            return false; // No water nearby
        }

        private bool IsOceanChest(int x, int y)
        {
            // Loop through a 1-tile radius around the chest
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    Tile tile = Main.tile[x + i, y + j];
                    int tileType = Main.tile[x + i, y + j].TileType;

                    // Check if the tile contains water (LiquidType 0 means water)
                    if (tile.LiquidAmount > 0 && tile.LiquidType == 0 || tileType == TileID.Sand)
                    {
                        return true; // Water found near the chest
                    }
                }
            }

            return false; // No water nearby
        }

        private bool IsMushroomBiome(int x, int y)
        {
            // Loop through a 1-tile radius around the chest
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int tileType = Main.tile[x + i, y + j].TileType;

                    // Check if the tile is any type of Mushroom biome
                    if (tileType == TileID.MushroomGrass || tileType == TileID.MushroomBlock || tileType == TileID.MushroomTrees || tileType == TileID.MushroomPlants)
                    {
                        return true; //Mushroom found near the chest
                    }
                }
            }

            return false; //No Mushroom nearby
        }

        private bool IsJungleChest(int x, int y)
        {
            // Loop through a 1-tile radius around the chest
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int tileType = Main.tile[x + i, y + j].TileType;

                    // Check if the tile is any type of Mushroom biome
                    if (tileType == TileID.Mud || tileType == TileID.IridescentBrick || tileType == TileID.RichMahogany || tileType == TileID.TinBrick || tileType == TileID.GoldBrick || tileType == TileID.Mudstone || tileType == TileID.JungleGrass || tileType == TileID.JunglePlants || tileType == TileID.JunglePlants2 || tileType == TileID.JungleThorns || tileType == TileID.JungleVines)
                    {
                        return true; //Mushroom found near the chest
                    }
                }
            }

            return false; //No Mushroom nearby
        }

        private bool IsSnowChest(int x, int y)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int tileType = Main.tile[x + i, y + j].TileType;

                    // Check for Ice or Snow tiles
                    if (tileType == TileID.SnowBlock || tileType == TileID.IceBlock || tileType == TileID.BorealWood)
                    {
                        return true; // Snow biome found near the chest
                    }
                }
            }

            return false;
        }

        private bool IsShadowChest(int x, int y)
        {
            // Check if the chest is a Shadow Chest based on its TileFrameX
            if (Main.tile[x, y].TileType == TileID.Containers && Main.tile[x, y].TileFrameX == 4 * 36) // Shadow Chest FrameX is 144 (4 * 36)
            {
                // Loop through a 1-tile radius around the chest to check for Underworld-related blocks
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        int tileType = Main.tile[x + i, y + j].TileType;

                        // Check for Underworld-related tiles
                        if (tileType == TileID.Ash || tileType == TileID.Hellstone || tileType == TileID.Obsidian || tileType == TileID.HellstoneBrick)
                        {
                            return true; // Shadow Chest in the Underworld found
                        }
                    }
                }
            }

            return false; // Not a Shadow Chest or not in the Underworld
        }
        #endregion


        // Find the index of the first empty slot in the chest
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
