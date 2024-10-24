using Terraria.ModLoader;
using Terraria;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.IO;
using Terraria.WorldBuilding;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Sxul.Content.Armor.XvinimSet;
using Sxul.Config;
using Terraria.DataStructures;

public class SxulFireSeed : ModPlayer
{
    public bool isSxulFire = false;
    private int acidRainTimer = 180;

    public override void OnEnterWorld()
    {
        // Check if the world seed is "SxulFire" and notify the player
        if (Main.ActiveWorldFileData.SeedText == "SxulFire" && SxulServer.Instance.SpecialSeedFromFire)
        {
            isSxulFire = true;
        }
    }

    public override void PostUpdate()
    {
        // Acid Rain Damage
        if (Main.raining && Player.ZoneOverworldHeight && SxulServer.Instance.SpecialSeedFromFire)
        {
            int tileX = (int)(Player.position.X / 16);
            int tileY = (int)(Player.position.Y / 16);
            bool exposedToRain = true;

            // Check if there are solid tiles above the player
            for (int y = tileY - 1; y >= 0; y--)
            {
                if (WorldGen.SolidTile(tileX, y))
                {
                    exposedToRain = false;
                    break;
                }
            }

            if (exposedToRain)
            {
                // Reduce the timer
                acidRainTimer--;

                // Apply damage every 3 seconds (180 ticks)
                if (acidRainTimer <= 0)
                {
                    Player.Hurt(PlayerDeathReason.ByCustomReason($"{Player.name} was melted by acid rain."), 41, 0);
                    acidRainTimer = 180; // Reset the timer
                }
            }
            else
            {
                acidRainTimer = 180; // Reset the timer if not exposed to rain
            }
        }
    }
}

public class SxulFireNPC : GlobalNPC
{
    public override void SetDefaults(NPC npc)
    {
        SxulFireSeed SFS = ModContent.GetInstance<SxulFireSeed>();
        // Check if the world seed is "SxulFire"
        if (Main.ActiveWorldFileData.SeedText == "SxulFire" && SxulServer.Instance.SpecialSeedFromFire)
        {
            // Increase health and damage of all NPCs
            npc.lifeMax = (int)(npc.lifeMax * 3f); // 150% more health
            npc.damage = (int)(npc.damage * 3f);
        }
    }

    public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
    {
        SxulFireSeed SFS = ModContent.GetInstance<SxulFireSeed>();

        if (Main.ActiveWorldFileData.SeedText == "SxulFire" && SxulServer.Instance.SpecialSeedFromFire)
        {
            spawnRate = (int)(spawnRate * 0.2f);
            maxSpawns = (int)(maxSpawns * 10f);
        }
    }
}

public class SxulFireBiomeConversion : ModSystem
{
    public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
    {
        if (Main.ActiveWorldFileData.SeedText == "SxulFire" &&  SxulServer.Instance.SpecialSeedFromFire)
        {
            // Locate the 'Terrain' generation pass where most of the biomes are created
            int index = tasks.FindIndex(genpass => genpass.Name.Equals("Final Cleanup"));

            if (index != -1)
            {
                // Insert a new generation pass after terrain generation to convert Forest into Mushroom biome
                tasks.Insert(index + 1, new PassLegacy("Convert Forest to Mushroom", ConvertForestToMushroom));
            }
        }
    }

    private void ConvertForestToMushroom(GenerationProgress progress, GameConfiguration configuration)
    {
        progress.Message = "Converting Forest to Mushroom Biome";

        // Iterate through all tiles in the world
        for (int x = 0; x < Main.maxTilesX; x++)
        {
            for (int y = 0; y < Main.maxTilesY; y++)
            {
                Tile tile = Main.tile[x, y];

                // Check if this tile is a grass or dirt tile in the surface (Forest Biome)
                if (tile.TileType == TileID.Grass || tile.TileType == TileID.Dirt)
                {
                    // Replace grass and dirt with Glowing Mushroom biome blocks
                    tile.TileType = TileID.Mud; // Change to Mud
                    WorldGen.PlaceTile(x, y, TileID.MushroomGrass, mute: true, forced: false); // Add Mushroom Grass

                    // Optionally, place Mushroom biome objects like tall mushrooms or trees
                    if (WorldGen.genRand.NextBool(50)) // 1 in 50 chance to place a mushroom tree
                    {
                        WorldGen.PlaceObject(x, y - 1, TileID.MushroomTrees);
                    }
                }
            }
        }
    }
}

public class SxulFirePlayer : ModPlayer
{
    private int rewardTimer = 0;  // Tracks the time spent in the world

    // List of junk items to give
    private List<int> junkItems = new List<int>
    {
        ItemID.DirtBlock,
        ItemID.StoneBlock,
        ItemID.Acorn,
        ItemID.Ale,
        ItemID.Anchor,
        ItemID.OldShoe,
        ItemID.RottenEgg,
        ItemID.Rope,
        ItemID.Gel,
        ItemID.GardenGnome
    };

    // List of rare items
    private List<int> rareItems = new List<int>
    {
        ItemID.TungstenBar,
        ItemID.GoldBar,
        ItemID.FloatingIslandFishingCrate,
        ItemID.GoldenCrate,
        ItemID.IronBar,
        ItemID.IvyGuitar
    };

    public override void Initialize()
    {
        rewardTimer = 0;
    }

    public bool HMChecked = false;

    public override void PreUpdate()
    {
        // Check if the player is in the "SxulFire" seed
        if (Main.ActiveWorldFileData.SeedText == "SxulFire")
        {
            if (SxulServer.Instance.SpecialSeedFromFire)
            {
                rewardTimer++;

                if (rewardTimer >= 60 * 120)  // 5 minutes in-game
                {
                    rewardTimer = 0; // Reset timer

                    if (Main.myPlayer == Player.whoAmI) // Only run on the local player
                    {
                        if (Main.hardMode && !HMChecked)
                        {
                            // Add Hardmode exclusive items
                            rareItems.Add(ItemID.AdamantiteBar);
                            rareItems.Add(ItemID.CobaltBar);
                            rareItems.Add(ItemID.MythrilBar);
                            rareItems.Add(ItemID.OrichalcumBar);
                            rareItems.Add(ItemID.FieryGreatsword);
                            rareItems.Add(ItemID.BladeofGrass);
                            rareItems.Add(ModContent.ItemType<Xvinim_sHelmet>());
                            rareItems.Add(ModContent.ItemType<Xvinim_sPlating>());
                            rareItems.Add(ModContent.ItemType<Xvinim_sPants>());
                            HMChecked = true;
                        }

                        // 95% chance for a junk item, 5% for a rare item
                        if (Main.rand.NextFloat() < 0.95f)
                        {
                            GiveItem(junkItems[Main.rand.Next(junkItems.Count)]);
                        }
                        else
                        {
                            GiveItem(rareItems[Main.rand.Next(rareItems.Count)]);
                        }
                    }
                }
            }
        }
    }

    private void GiveItem(int itemType)
    {
        // Add item to the player's inventory
        Player.QuickSpawnItem(null, itemType);
        Main.NewText($"You received: {Lang.GetItemNameValue(itemType)}", 255, 255, 0); // Message in chat
    }
}
