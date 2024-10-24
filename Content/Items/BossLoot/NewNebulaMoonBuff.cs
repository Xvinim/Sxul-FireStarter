using Terraria;
using Terraria.ModLoader;

namespace Sxul.Content.Items.BossLoot
{
    public class NewNebulaMoonBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<NewNebulaMoon>()] > 0)
            {
                player.buffTime[buffIndex] = 18000; // Buff lasts as long as the projectile exists
            }
            else
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }
    }
}
