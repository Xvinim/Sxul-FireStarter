using Sxul.Content.Buffs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace Sxul.Systems
{
    internal class BuffHandlerNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public bool hasEntropyDebuff;
        public bool hasEntropyImmunityBuff;

        public override void ResetEffects(NPC npc)
        {
            hasEntropyDebuff = false;
            hasEntropyImmunityBuff = false;
        }

        public override void OnHitByItem(NPC npc, Player player, Item item, NPC.HitInfo hit, int damageDone)
        {
            if (npc.HasBuff(ModContent.BuffType<Entropy>()))
            {
                if (hit.Crit && !hasEntropyImmunityBuff)  // If it's a critical hit
                {
                    // Apply 5x critical damage
                    int extraDamage = (damageDone * 3) - damageDone;  // Calculate the extra damage

                    npc.life -= extraDamage;  // Directly reduce the NPC's health by the extra damage
                    CombatText.NewText(npc.getRect(), CombatText.DamagedFriendly, extraDamage, dramatic: true, dot: false);  // Display the extra damage

                    npc.DelBuff(ModContent.BuffType<Entropy>()); // Remove Entropy debuff
                    npc.AddBuff(ModContent.BuffType<Equilibrium>(), 1800); // 30 seconds (30 * 60)
                }
            }
        }

        public override void OnHitByProjectile(NPC npc, Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
            if (npc.HasBuff(ModContent.BuffType<Entropy>()))
            {
                if (hit.Crit && !hasEntropyImmunityBuff)  // If it's a critical hit
                {
                    // Apply 5x critical damage
                    int extraDamage = (damageDone * 3) - damageDone;  // Calculate the extra damage

                    npc.life -= extraDamage;  // Directly reduce the NPC's health by the extra damage
                    CombatText.NewText(npc.getRect(), CombatText.DamagedFriendly, extraDamage, dramatic: true, dot: false);  // Display the extra damage

                    npc.DelBuff(ModContent.BuffType<Entropy>()); // Remove Entropy debuff
                    npc.AddBuff(ModContent.BuffType<Equilibrium>(), 1800); // 30 seconds (30 * 60)
                }
            }
        }
    }
}
