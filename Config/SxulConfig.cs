using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.Config;

namespace Sxul.Config
{
    internal class SxulConfig : ModConfig
    {
        public static SxulConfig Instance;

        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Label("Enable Custom Boss Bar")]
        [Tooltip("Toggle the custom boss bar for vanilla bosses on or off.  (This Will Override Modded Bosses)")]
        public bool EnableCustomBossBar = true;
    }

    internal class SxulServer : ModConfig
    {
        public static SxulServer Instance;

        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Label("Disable Boss Reworks")]
        [Tooltip("Disables the Adjustments of Vanilla Bosses")]
        public bool BossReworks = true;

        [Label("SxulFire Seed")]
        [Tooltip("Enables/Disables the 'SxulFire' Seed")]
        public bool SpecialSeedFromFire = false;
    }

    internal class BackgroundStyle : ModConfig
    {
        public static BackgroundStyle Instance;

        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Label("Christmas Style Backgrounds")]
        [Tooltip("Change the Main Menu Backgrounds to Christmas Versions.")]
        public bool Christmas = false;

        [Label("Halloween Style Backgrounds")]
        [Tooltip("Change the Main Menu Backgrounds to Halloween Versions.")]
        public bool Halloweener = false;
    }
}
