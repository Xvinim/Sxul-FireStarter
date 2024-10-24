using Sxul.Content.Blocks;
using Sxul.Content.Boss.Bosses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Achievements;
using Terraria.ModLoader;

namespace Sxul.Systems
{
    internal class AchievementHandler : ModSystem
    {
        public override void PostSetupContent()
        {
            // Check if the TMLAchievements mod is loaded
            if (ModLoader.TryGetMod("TMLAchievements", out Mod mod))
            {
                // Call to add the custom achievement
                mod.Call(
                    "AddAchievement",          // Method identifier
                    this,                      // Reference to your mod
                    "KingOfTheStars",       // Achievement name (no spaces)
                    AchievementCategory.Slayer, // Achievement category
                    "Sxul/Assets/Achievements/LunaticCultistKill", // Path to the texture (128x64)
                    null,                      // Custom border texture path (null for default)
                    false,                     // Show progress bar (false)
                    true,                      // Show achievement card (true)
                    37.0f,                     // Achievement card order (somewhere in the middle of progression)
                    new string[] { "Kill_" + ModContent.NPCType<ForgottenCultist>() } 
                );

                mod.Call(
                    "AddAchievement",          // Method identifier
                    this,                      // Reference to your mod
                    "SoulUser",       // Achievement name (no spaces)
                    AchievementCategory.Slayer, // Achievement category
                    "Sxul/Assets/Achievements/SoulWeaving", // Path to the texture (128x64)
                    null,                      // Custom border texture path (null for default)
                    false,                     // Show progress bar (false)
                    true,                      // Show achievement card (true)
                    22.0f,                     // Achievement card order (somewhere in the middle of progression)
                    new string[] { "Craft_" + ModContent.ItemType<SoulWeaverBlock>() }
                );
            }
        }
    }
}
