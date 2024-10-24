using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Sxul.Content.Buffs;

namespace Sxul.Systems
{
    internal class BuffHandler : ModPlayer
    {
        public bool hasEntropyDebuff;
        public bool hasEntropyImmunityBuff;

        public override void ResetEffects()
        {
            hasEntropyDebuff = false;
            hasEntropyImmunityBuff = false;
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (hasEntropyDebuff)
            {
                if (hit.Crit && !hasEntropyImmunityBuff)
                {
                    damageDone *= 2; // Apply 5x damage

                    // Remove Entropy debuff and add immunity buff
                    target.DelBuff(ModContent.BuffType<Entropy>());
                    target.AddBuff(ModContent.BuffType<Equilibrium>(), 1800); // 30 seconds immunity (1800 ticks)
                }
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (hasEntropyDebuff)
            {
                if (hit.Crit && !hasEntropyImmunityBuff)
                {
                    damageDone *= 2; // Apply 5x damage

                    // Remove Entropy debuff and add immunity buff
                    target.DelBuff(ModContent.BuffType<Entropy>());
                    target.AddBuff(ModContent.BuffType<Equilibrium>(), 1800); // 30 seconds immunity (1800 ticks)
                }
            }
        }
    }
}
