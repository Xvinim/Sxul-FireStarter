using Terraria;
using Terraria.ModLoader;

namespace Sxul.Content.Items.BossLoot
{
    public class NewMoonManaDebuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true; // Indicates this is a debuff
            Main.buffNoTimeDisplay[Type] = false; // Show the remaining time of the debuff
        }

        public override void Update(Player player, ref int buffIndex)
        {
            // Slow down mana regeneration
            if (player.statMana < player.statManaMax2 * 0.5f)
            {
                player.manaRegen = 0; // Stop mana regeneration until 50%
                player.manaRegenDelay = 10;
            }
            else
            {
                player.manaRegen -= 3; // Slow down regeneration after 50%
                player.manaRegenDelay = 30; // Increase the delay between regen ticks
            }

            // Disable mana regeneration accessories
            player.manaRegenBonus = 0;
            player.manaFlower = false;
            player.magicCuffs = false;
        }
    }
}
