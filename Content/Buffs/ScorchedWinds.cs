using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Sxul.Content.Buffs
{
    public class ScorchedWinds : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true; // Debuff cannot be canceled by right-clicking.
            Main.buffNoSave[Type] = true; // Doesn't persist on save.
            Main.pvpBuff[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            // Check if near sand to reverse effects
            bool nearSand = CheckProximityToSand(npc);

            // Every 120 ticks (2 seconds)
            if (npc.buffTime[buffIndex] % 25 == 0)
            {
                int damage = RollDamage(nearSand);

                if (damage > 0)
                {
                    // Manually apply damage to the NPC
                    npc.life -= damage;

                    if (npc.life <= 0)
                    {
                        npc.checkDead(); // Checks if the NPC should die from this damage
                    }

                    // Create sandy beige-colored damage text
                    Color sandyColor = new Color(194, 178, 128); // Sandy beige color
                    CombatText.NewText(npc.Hitbox, sandyColor, damage, false, false);
                }
            }
        }

        public override void Update(Player player, ref int buffIndex)
        {
            // Check if near sand to reverse effects
            bool nearSand = CheckProximityToSand(player);

            // Every 120 ticks (2 seconds)
            if (player.buffTime[buffIndex] % 120 == 0)
            {
                int damage = RollDamage(nearSand);

                if (damage > 0)
                {
                    player.Hurt(Terraria.DataStructures.PlayerDeathReason.ByCustomReason("was scorched by the desert winds."), damage, 0);
                }
            }
        }

        // Function to calculate proximity to sand
        private bool CheckProximityToSand(Entity entity)
        {
            int tileRange = 10; // Range to check for sand tiles

            int entityTileX = (int)(entity.position.X / 16);
            int entityTileY = (int)(entity.position.Y / 16);

            for (int x = entityTileX - tileRange; x <= entityTileX + tileRange; x++)
            {
                for (int y = entityTileY - tileRange; y <= entityTileY + tileRange; y++)
                {
                    Tile tile = Main.tile[x, y];
                    if (tile != null && (tile.TileType == TileID.Sand || tile.TileType == TileID.HardenedSand || tile.TileType == TileID.Sandstone))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        // Function to calculate damage based on proximity to sand
        private int RollDamage(bool nearSand)
        {
            int maxDamage = 30;

            // Roll a random damage value between 1 and 60
            int damage = Main.rand.Next(1, maxDamage + 1);

            // Chance for damage: Higher numbers should have lower chances.
            float chance = nearSand ? (damage / (float)maxDamage) : ((maxDamage - damage) / (float)maxDamage);

            // If the random value is lower than the chance, apply damage
            return Main.rand.NextFloat() <= chance ? damage : 0;
        }
    }
}
