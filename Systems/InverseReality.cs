using SubworldLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.WorldBuilding;
using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameInput;
using Microsoft.Build.Tasks;
using static InverseReality.TerrainRaisingPass;

public class InverseReality : Subworld
{
    public override int Width => 3600;
    public override int Height => 2100;

    public override bool ShouldSave => false;
    public override bool NoPlayerSaving => false;

    public override bool GetLight(Tile tile, int x, int y, ref FastRandom rand, ref Vector3 color)
    {
        if (tile.LiquidType == LiquidID.Water && tile.LiquidAmount > 0)
        {
            color.Z = tile.LiquidAmount / 235f;
        }
        return false;
    }

    public override List<GenPass> Tasks => new List<GenPass>()
    {
        new TerrainGenPass(),
        new TerrainRaisingPass(),
        new CSpikesPass(),
        new AddNaturalLakesPass()
    };

    // Sets the time to the middle of the day whenever the subworld loads
    public override void OnLoad()
    {
        Main.dayTime = true;
        Main.time = 27000;
    }

    public override void DrawSetup(GameTime gameTime)
    {
        PlayerInput.SetZoom_Unscaled();
        Main.instance.GraphicsDevice.Clear(Color.Black);
        Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, Main.Rasterizer, null, Main.UIScaleMatrix);
        DrawMenu(gameTime);
        Main.DrawCursor(Main.DrawThickCursor());
        Main.spriteBatch.End();
    }

    //public TerrainGenPass() : base("Terrain", 100) { }

    #region GenPasses
    public class TerrainGenPass : GenPass
    {
        public TerrainGenPass() : base("Terrain", 100) { }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = "Generating Terrain";

            int worldWidth = Main.maxTilesX;
            int worldHeight = Main.maxTilesY;

            // Step 1: Fill the world with dirt.
            FillWorldWithDirt(worldWidth, worldHeight);

            // Step 2: Shape the surface terrain.
            ShapeSurfaceTerrain(worldWidth, worldHeight);
        }

        private void FillWorldWithDirt(int worldWidth, int worldHeight)
        {
            // Fill the entire world with dirt from the surface down to a specified depth
            for (int x = 0; x < worldWidth; x++)
            {
                for (int y = 0; y < worldHeight; y++)
                {
                    if (y < worldHeight * 0.90f) // Filling up to around 90% of the world height???
                    {
                        Tile tile = Main.tile[x, y];
                        tile.HasTile = true; // Set the tile as active
                        tile.TileType = TileID.Dirt; // Set the tile type to dirt
                    }
                }
            }
        }

        private void ShapeSurfaceTerrain(int worldWidth, int worldHeight)
        {
            int minSurfaceHeight = (int)(worldHeight * 0.2f);
            int maxSurfaceHeight = (int)(worldHeight * 0.3f);

            // Starting height for the terrain
            int lastSurfaceHeight = (minSurfaceHeight + maxSurfaceHeight) / 2;

            for (int x = 0; x < worldWidth; x++)
            {
                // Adjust height with a smoothing factor
                int randomValue = WorldGen.genRand.Next(10); // Random value between 0 and 9
                int shift = 0;

                // Smoothing factor to reduce drastic changes
                if (randomValue < 3) // 30% chance to shift down
                {
                    shift = -1;
                }
                else if (randomValue < 6) // 30% chance to shift up
                {
                    shift = 1;
                }
                // 40% chance that shift stays 0, causing no change

                // Clamp the lastSurfaceHeight with a tighter range
                lastSurfaceHeight = Math.Clamp(lastSurfaceHeight + shift, minSurfaceHeight, maxSurfaceHeight);

                // Clear tiles above the current surface height to form the terrain
                for (int y = 0; y < lastSurfaceHeight; y++)
                {
                    Tile tile = Main.tile[x, y];
                    tile.HasTile = false; // Deactivate the tile
                    tile.ClearTile(); // Clear the tile, making it air
                }

                // Ensure dirt tiles at and below the surface height remain active
                for (int y = lastSurfaceHeight; y < worldHeight * 0.6f; y++)
                {
                    Tile tile = Main.tile[x, y];
                    tile.HasTile = true;
                    tile.TileType = TileID.Dirt;
                }
            }

            // Additional pass to smooth the terrain
            SmoothTerrain(worldWidth, worldHeight);
        }

        private void SmoothTerrain(int worldWidth, int worldHeight)
        {
            // Create a temporary list to store changes for batch processing
            List<(int x, int y, bool shouldBeSolid)> changes = new List<(int, int, bool)>();

            // Loop through the terrain to analyze and smooth it
            for (int x = 1; x < worldWidth - 1; x++)
            {
                for (int y = 1; y < worldHeight - 1; y++)
                {
                    // Check the current tile's state
                    bool isSolid = Main.tile[x, y].HasTile;

                    // Count solid neighboring tiles
                    int adjacentSolidTiles = 0;
                    if (Main.tile[x - 1, y].HasTile) adjacentSolidTiles++; // Left
                    if (Main.tile[x + 1, y].HasTile) adjacentSolidTiles++; // Right
                    if (Main.tile[x, y - 1].HasTile) adjacentSolidTiles++; // Up
                    if (Main.tile[x, y + 1].HasTile) adjacentSolidTiles++; // Down
                    if (Main.tile[x - 1, y - 1].HasTile) adjacentSolidTiles++; // Top-Left
                    if (Main.tile[x + 1, y - 1].HasTile) adjacentSolidTiles++; // Top-Right
                    if (Main.tile[x - 1, y + 1].HasTile) adjacentSolidTiles++; // Bottom-Left
                    if (Main.tile[x + 1, y + 1].HasTile) adjacentSolidTiles++; // Bottom-Right

                    // Isolated block logic
                    if (isSolid && adjacentSolidTiles < 3) // If the block is solid but not supported enough
                    {
                        changes.Add((x, y, false)); // Mark for deletion
                    }
                    else if (!isSolid && adjacentSolidTiles >= 4) // If it's air and has enough solid neighbors
                    {
                        changes.Add((x, y, true)); // Mark for addition
                    }
                }
            }

            // Apply changes
            foreach (var (x, y, shouldBeSolid) in changes)
            {
                Tile tile = Main.tile[x, y];
                if (shouldBeSolid)
                {
                    tile.HasTile = true;
                    tile.TileType = TileID.Dirt; // Set to dirt for visible terrain
                }
                else
                {
                    tile.HasTile = false; // Remove isolated blocks
                    tile.ClearTile(); // Make it air
                }
            }

            // Optional: Run another smoothing pass to ensure better results
            for (int i = 0; i < 2; i++)
            {
                FurtherSmoothTerrain(worldWidth, worldHeight);
            }
        }

        private void FurtherSmoothTerrain(int worldWidth, int worldHeight)
        {
            // Loop through the terrain again to apply additional smoothing
            for (int x = 1; x < worldWidth - 1; x++)
            {
                for (int y = 1; y < worldHeight - 1; y++)
                {
                    // Count solid neighboring tiles
                    int adjacentSolidTiles = 0;
                    if (Main.tile[x - 1, y].HasTile) adjacentSolidTiles++; // Left
                    if (Main.tile[x + 1, y].HasTile) adjacentSolidTiles++; // Right
                    if (Main.tile[x, y - 1].HasTile) adjacentSolidTiles++; // Up
                    if (Main.tile[x, y + 1].HasTile) adjacentSolidTiles++; // Down

                    // If there are too many solid tiles in a cluster
                    if (adjacentSolidTiles >= 4)
                    {
                        // Add some air around the cluster to break it up a little
                        if (Main.tile[x, y - 1].HasTile && !Main.tile[x, y + 1].HasTile) // If there's a solid tile above and no tile below
                        {
                            Tile tile = Main.tile[x, y + 1];
                            tile.HasTile = false; // Clear the tile below
                            tile.ClearTile();
                        }
                    }

                    // New logic: Check the two tiles directly next to the solid tile
                    if (Main.tile[x, y].HasTile) // Check if current tile is solid
                    {
                        bool leftTileAir = !Main.tile[x - 1, y].HasTile; // Check left
                        bool rightTileAir = !Main.tile[x + 1, y].HasTile; // Check right

                        // If both left and right tiles are air, remove the solid tile
                        if (leftTileAir && rightTileAir)
                        {
                            Tile tile = Main.tile[x, y];
                            tile.HasTile = false; // Remove the tile
                            tile.ClearTile();
                        }
                    }
                }
            }
        }
    }

    public class TerrainRaisingPass : GenPass
    {
        public TerrainRaisingPass() : base("Terrain Raise", 100) { }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = "Raising Terrain";

            int worldWidth = Main.maxTilesX;
            int worldHeight = Main.maxTilesY;

            GenerateHillsMountainsAndCliffs(worldWidth, worldHeight);
        }

        private void GenerateHillsMountainsAndCliffs(int worldWidth, int worldHeight)
        {
            int hillHeight = 30;
            int minDistance = 200; // Ensure enough spacing between formations

            for (int x = 0; x < worldWidth; x++)
            {
                int surfaceHeight = GetSurfaceHeight(x);
                int randomValue = WorldGen.genRand.Next(100);

                if (randomValue < 10 && x > minDistance) // Hill generation chance
                {
                    CreateHill(x, surfaceHeight + 10, hillHeight);
                    x += minDistance;
                }
            }
        }

        private void CreateHill(int x, int surfaceHeight, int hillHeight)
        {
            for (int offset = 0; offset <= hillHeight; offset++)
            {
                int hillWidth = (int)(Math.Sqrt(hillHeight * hillHeight - offset * offset) * 1.5); // Make hills wider
                int currentHeight = surfaceHeight - offset;

                for (int i = -hillWidth; i <= hillWidth; i++)
                {
                    int adjustedX = x + i;
                    if (adjustedX >= 0 && adjustedX < Main.maxTilesX && currentHeight >= 0)
                    {
                        if (CanPlaceTile(adjustedX, currentHeight))
                        {
                            Tile tile = Main.tile[adjustedX, currentHeight];
                            tile.HasTile = true;
                            tile.TileType = TileID.Dirt;
                        }
                    }
                }
            }
        }

        public class CSpikesPass : GenPass
        {
            public CSpikesPass() : base("Spiking the Terrain", 120) { }

            protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
            {
                int surface = GetSurfaceHeight(); // Ensure this method exists and returns a valid height
                int numberOfSpikes = Main.rand.Next(50, 100); // Set the number of spikes you want

                Main.NewText($"Surface Height: {surface}, Number of Spikes: {numberOfSpikes}");

                for (int i = 0; i < numberOfSpikes; i++)
                {
                    int spikeX = Main.rand.Next(0, Main.maxTilesX);
                    int spikeY = Main.rand.Next(surface - 10, surface); // Adjust this as needed

                    // Debugging: Log spike positions
                    Main.NewText($"Spike Position: ({spikeX}, {spikeY})");

                    GenerateCurvedSpikes(spikeX, spikeY);
                }
            }

            private void GenerateCurvedSpikes(int x, int y)
            {
                int spikeHeight = Main.rand.Next(3, 8); // Random height for the spike
                int baseWidth = Main.rand.Next(3, 6); // Random base width

                for (int i = 0; i < spikeHeight; i++)
                {
                    // Tapering effect: Calculate width for the current height
                    int width = (int)(baseWidth * (1f - (float)i / spikeHeight));

                    // Create walls to form the spike
                    for (int j = -width; j <= width; j++)
                    {
                        if (j != 0) // Skip the middle for a sharp point
                        {
                            // Add walls at the current height
                            if (y - i >= 0 && x + j >= 0 && x + j < Main.maxTilesX) // Ensure it doesn't go out of bounds
                            {
                                Main.tile[x + j, y - i].WallType = WallID.Stone; // Use a wall ID that fits your theme
                            }
                        }
                    }
                }

                // Add some curve to the spike's appearance by offsetting
                for (int i = 0; i < spikeHeight; i++)
                {
                    int width = (int)(baseWidth * (1f - (float)i / spikeHeight)); // Recalculate width for offsetting
                    int offset = (int)(Math.Sin((float)i / spikeHeight * Math.PI) * baseWidth / 2);

                    for (int j = -width; j <= width; j++)
                    {
                        if (j != 0 && y - i >= 0 && x + j + offset >= 0 && x + j + offset < Main.maxTilesX) // Check bounds
                        {
                            Main.tile[x + j + offset, y - i].WallType = WallID.Stone; // Curve the wall to the side
                        }
                    }
                }
            }

            private int GetSurfaceHeight()
            {
                // Implement this method to return the current surface height
                // You may want to return the highest tile in your terrain
                int surface = 0;
                for (int x = 0; x < Main.maxTilesX; x++)
                {
                    for (int y = Main.maxTilesY - 1; y >= 0; y--)
                    {
                        if (Main.tile[x, y].HasTile)
                        {
                            surface = y; // The highest active tile is considered the surface
                            break;
                        }
                    }
                }
                return surface;
            }
        }


        public class AddNaturalLakesPass : GenPass
        {
            public AddNaturalLakesPass() : base("Adding Natural Lakes", 50) { }

            protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
            {
                progress.Message = "Carving Lakes and Ponds";

                int numberOfLakes = 5; // Adjust the number of lakes

                for (int i = 0; i < numberOfLakes; i++)
                {
                    // Randomly select a location for the lake
                    int lakeX = WorldGen.genRand.Next(100, Main.maxTilesX - 100);
                    int surfaceY = GetSurfaceHeight(lakeX);

                    // Ensure lakes only spawn above a certain depth
                    if (surfaceY < Main.worldSurface && surfaceY > 50)
                    {
                        // Create the lake by carving a depression into the terrain
                        HashSet<Point> carvedTiles = CreateNaturalLake(lakeX, surfaceY);

                        // Fill the carved tiles with water
                        FillLakeWithWater(carvedTiles);
                    }
                }
            }

            private int GetSurfaceHeight(int x)
            {
                for (int y = 0; y < Main.maxTilesY; y++)
                {
                    if (Main.tile[x, y].HasTile)
                    {
                        return y - 1; // Return surface height just above the first solid tile
                    }
                }
                return Main.maxTilesY;
            }

            private HashSet<Point> CreateNaturalLake(int x, int surfaceY)
            {
                // Determine the radius of the lake
                int radius = WorldGen.genRand.Next(10, 20); // Increase radius for larger lakes
                HashSet<Point> carvedTiles = new HashSet<Point>();

                // Create a larger bounding box to define the lake area
                for (int offsetX = -radius; offsetX <= radius; offsetX++)
                {
                    for (int offsetY = -radius; offsetY <= radius; offsetY++)
                    {
                        int distanceFromCenter = (int)Math.Sqrt(offsetX * offsetX + offsetY * offsetY);
                        int chanceToClear = 1; // Base chance to clear the tile

                        // Increase the chance to clear tiles closer to the edge
                        if (distanceFromCenter < radius - 2)
                        {
                            chanceToClear = 0; // Inner area stays solid
                        }
                        else if (distanceFromCenter < radius)
                        {
                            chanceToClear = WorldGen.genRand.Next(0, 3); // Random chance to create some irregularity
                        }

                        // Only proceed if clearing
                        if (chanceToClear == 0)
                        {
                            continue; // Skip clearing for this tile
                        }

                        int xPos = x + offsetX + WorldGen.genRand.Next(-2, 3); // Randomly adjust position
                        int yPos = surfaceY + offsetY + WorldGen.genRand.Next(-2, 3); // Randomly adjust position

                        // Check if within bounds
                        if (xPos >= 0 && xPos < Main.maxTilesX && yPos >= 0 && yPos < Main.maxTilesY)
                        {
                            Tile tile = Main.tile[xPos, yPos];

                            // Only carve if there's a tile and we haven't already carved here
                            if (tile.HasTile && !carvedTiles.Contains(new Point(xPos, yPos)))
                            {
                                // Carve the tile and mark it
                                tile.ClearTile();
                                carvedTiles.Add(new Point(xPos, yPos));
                            }
                        }
                    }
                }

                return carvedTiles; // Return the carved tiles for filling water
            }


            private void FillLakeWithWater(HashSet<Point> carvedTiles)
            {
                foreach (Point tilePos in carvedTiles)
                {
                    Tile tile = Main.tile[tilePos.X, tilePos.Y];

                    // Fill the carved tiles with water
                    if (tile.LiquidAmount == 0) // Check if it's empty
                    {
                        tile.LiquidType = LiquidID.Water; // Set liquid type to water
                        tile.LiquidAmount = 255; // Fill the tile with water

                        // Refresh the tile to reflect the changes in the game
                        WorldGen.SquareTileFrame(tilePos.X, tilePos.Y); // Refresh the tile frame
                    }
                }
            }
        }

        private bool CanPlaceTile(int x, int y)
        {
            if (y <= 0 || y >= Main.maxTilesY - 1) return false;

            bool hasSupport = Main.tile[x, y + 1].HasTile;

            int surroundingEmptySpaces = 0;
            if (!Main.tile[x - 1, y + 1].HasTile) surroundingEmptySpaces++;
            if (!Main.tile[x + 1, y + 1].HasTile) surroundingEmptySpaces++;
            if (!Main.tile[x - 2, y + 1].HasTile) surroundingEmptySpaces++;
            if (!Main.tile[x + 2, y + 1].HasTile) surroundingEmptySpaces++;

            return hasSupport && surroundingEmptySpaces < 3; // Allow more space around for smoother transitions
        }

        private int GetSurfaceHeight(int x)
        {
            for (int y = 0; y < Main.maxTilesY; y++)
            {
                if (Main.tile[x, y].HasTile)
                {
                    return y - 1; // Return surface height just above the first solid tile
                }
            }
            return Main.maxTilesY;
        }
    }
    #endregion

    public class UpdateSubworldSystem : ModSystem
    {
        public override void PreUpdateWorld()
        {
            if (SubworldSystem.IsActive<InverseReality>())
            {
                // Update mechanisms
                Wiring.UpdateMech();

                // Update tile entities
                TileEntity.UpdateStart();
                foreach (TileEntity te in TileEntity.ByID.Values)
                {
                    te.Update();
                }
                TileEntity.UpdateEnd();

                // Update liquid
                if (++Liquid.skipCount > 1)
                {
                    Liquid.UpdateLiquid();
                    Liquid.skipCount = 0;
                }
            }
        }
    }
}
