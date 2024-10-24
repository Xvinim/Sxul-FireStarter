using Microsoft.Xna.Framework;
using Sxul.Content.Buffs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Sxul.Content.Classes
{
    public class Inflictor : ModPlayer
    {
        public float iCrit = 0.0f;
        public float iDamage = 0.0f;

        public float iBaseDebuff = 0.0f;
        public float iPoison = 0;
        public float iFlames = 0;
        public float iCursedFire = 0;
        public float iConfusion = 0;
        public float iFrost = 0;
        public float iEntropy = 0;
        public float iScorch = 0;

        public bool canPoison = false;
        public bool canFlame = false;
        public bool canCurse = false;
        public bool canConfuse = false;
        public bool canFrost = false;
        public bool canEntropy = false;
        public bool canScorch = false;

        public override void ResetEffects()
        {
            iCrit = 0.0f;
            iDamage = 0.0f;

            iBaseDebuff = 0.0f;
            iPoison = 0;
            iFlames = 0;
            iCursedFire = 0;
            iConfusion = 0;
            iFrost = 0;
            iEntropy = 0;
            iScorch = 0;

            canPoison = false;
            canFlame = false;
            canCurse = false;
            canConfuse = false;
            canFrost = false;
            canEntropy = false;
            canScorch = false;
    }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            float damageBoost = 1.0f + iDamage;
            int boostedDamage = (int)(damageDone * damageBoost);

            Player player = Main.LocalPlayer;

            if (player.HeldItem.DamageType == ModContent.GetInstance<InflictorClass>())
            {
                damageDone *= (int)(iCrit);
                // Calculate base debuff chance based on damage done
                float iBaseDebuff = MathHelper.Clamp(damageDone / 100f, 0f, 0.75f); // 0-75% debuff chance based on damage
                float iPoison = MathHelper.Clamp(iBaseDebuff, 0f, 1f);
                float iFlames = MathHelper.Clamp(iBaseDebuff, 0f, 1f);
                float iCursedFire = MathHelper.Clamp(iBaseDebuff, 0f, 1f);
                float iConfusion = MathHelper.Clamp(iBaseDebuff, 0f, 1f);
                float iFrost = MathHelper.Clamp(iBaseDebuff, 0f, 1f);
                float iEntropy = MathHelper.Clamp(iBaseDebuff, 0f, 1f);
                float iScorch = MathHelper.Clamp(iBaseDebuff, 0f, 1f);

                // Only apply debuffs if base debuff chance is above 0
                if (iBaseDebuff > 0.0f)
                {
                    if (canPoison && Main.rand.NextFloat() < iPoison)
                    {
                        target.AddBuff(BuffID.Poisoned, 60 * 5); // Poisoned for 5 seconds
                    }

                    if (canFlame && Main.rand.NextFloat() < iFlames)
                    {
                        target.AddBuff(BuffID.OnFire, 60 * 5); // On Fire! for 5 seconds
                    }

                    if (canCurse && Main.rand.NextFloat() < iCursedFire)
                    {
                        target.AddBuff(BuffID.CursedInferno, 60 * 5); // Cursed Inferno for 5 seconds
                    }

                    if (canConfuse && Main.rand.NextFloat() < iConfusion)
                    {
                        target.AddBuff(BuffID.Confused, 60 * 5); // Confused for 5 seconds
                    }

                    if (canFrost && Main.rand.NextFloat() < iFrost)
                    {
                        target.AddBuff(BuffID.Frostburn, 60 * 5); // Frostburn for 5 seconds
                    }

                    if (canEntropy && Main.rand.NextFloat() < iEntropy)
                    {
                        target.AddBuff(ModContent.BuffType<Entropy>(), 60 * 5); // Custom Entropy debuff for 5 seconds
                    }

                    if (canScorch && Main.rand.NextFloat() < iScorch)
                    {
                        target.AddBuff(ModContent.BuffType<ScorchedWinds>(), 60 * 5); // Custom Scorched Winds debuff for 5 seconds
                    }
                }
            }
        }
    }
}
