using Steamworks;
using System;
using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Microsoft.Xna.Framework;
using XPT.Core.Audio.MP3Sharp.Decoding;
using Terraria;
using Terraria.ID;
using Sxul.Content.Items.Beginning;
using SteelSeries.GameSense;
using Sxul.Content.Items.Accessories.Melee;
using Sxul.Content.Items.Weapons;
using Sxul.Content.Classes;

namespace Sxul
{
    internal class SoulTracker : ModPlayer
    {
        //Soul Stuff
        public int soulCurrent = 0;
        public int soulMax = 0;
        public bool usingSoul = false;

        //Soul Item Uses
        public int crackedSoulUses = 0;
        public int crackedSoulUsed = 0;

        public int blightSoulUses = 0;
        public int blightSoulUsed = 0;

        public int ensnaredSoulUses = 0;
        public int ensnaredSoulUsed = 0;

        public int decaySoulUses = 0;
        public int decaySoulUsed = 0;

        //Mana Stuff
        public bool hasUsedManaCore = false;

        //Races
        public bool isHuman;
        public bool isDryad;
        public bool isLizhard;
        public bool isVampire;
        public bool isElf;
        public bool raceChosen;

        public override void Initialize()
        {
            soulCurrent = 0;
            soulMax = 500;
            Player.statManaMax = 0;
        }

        public override void ResetEffects()
        {
            if (usingSoul)
            {
                Player.statMana = 0;
            }
            else
            {
                soulCurrent = 0;
            }

            if (!hasUsedManaCore)
            {
                Player.statManaMax2 = 0;
            }
        }

        public void useSoul(int amount)
        {
            if (soulCurrent >= amount)
            {
                soulCurrent -= amount;
            }
        }

        public bool CanUseSoul(int amount)
        {
            return soulCurrent >= amount;
        }

        public override void SaveData(TagCompound tag)
        {
            tag["soulCurrent"] = soulCurrent;
            tag["soulMax"] = soulMax;
            tag["usingSoul"] = usingSoul;

            #region souls
            tag["crackedSoulUses"] = crackedSoulUses;
            tag["crackedSoulUsed"] = crackedSoulUsed;

            tag["blightUses"] = blightSoulUses;
            tag["blightUsed"] = blightSoulUsed;

            tag["ensnaredUses"] = ensnaredSoulUses;
            tag["ensnaredUsed"] = ensnaredSoulUsed;

            tag["decayUses"] = decaySoulUses;
            tag["decayUsed"] = decaySoulUsed;
            #endregion
            tag["hasUsedManaCore"] = hasUsedManaCore;

            tag["isHuman"] = isHuman;
            tag["isDryad"] = isDryad;
            tag["isLizhard"] = isLizhard;
            tag["isElf"] = isElf;
            tag["isVampire"] = isVampire;

            tag["hrsi"] = hasRecievedStartingItem;
        }

        public override void LoadData(TagCompound tag)
        {
            soulCurrent = tag.GetInt("soulCurrent");
            soulMax = tag.GetInt("soulMax");
            usingSoul = tag.GetBool("usingSoul");
            #region souls
            crackedSoulUses = tag.GetInt("crackedSoulUses");
            crackedSoulUsed = tag.GetInt("crackedSoulUsed");

            blightSoulUses = tag.GetInt("blightUses");
            blightSoulUsed = tag.GetInt("blightUsed");

            ensnaredSoulUses = tag.GetInt("ensnaredUses");
            ensnaredSoulUsed = tag.GetInt("ensnaredUsed");

            decaySoulUses = tag.GetInt("decayUses");
            decaySoulUsed = tag.GetInt("decayUsed");
            #endregion
            hasUsedManaCore = tag.GetBool("hasUsedManaCore");

            isHuman = tag.GetBool("isHuman");
            isDryad = tag.GetBool("isDryad");
            isLizhard = tag.GetBool("isLizhard");
            isElf = tag.GetBool("isElf");
            isVampire = tag.GetBool("isVampire");

            hasRecievedStartingItem = tag.GetBool("hrsi");
        }

        public override void PostUpdate()
        {
            #region Souls
            //Cracked
            Player.statDefense += crackedSoulUsed;

            //Blighted
            Player.statLifeMax2 += blightSoulUsed * 20;

            //Ensnared
            Player.statManaMax2 += ensnaredSoulUsed * 20;

            //Decaying
            Player.GetCritChance<GenericDamageClass>() += decaySoulUsed * 2;
            #endregion

            if (isVampire && Main.dayTime && Player.ZoneOverworldHeight && Main.ActiveWorldFileData.SeedText == "SxulFire")
            {
                // Check if there are no solid tiles above the player (exposed to sunlight)
                int tileX = (int)(Player.position.X / 16);
                int tileY = (int)(Player.position.Y / 16);
                bool exposedToSunlight = true;

                // Loop through tiles above the player to check for solid blocks
                for (int y = tileY - 1; y >= 0; y--)
                {
                    if (WorldGen.SolidTile(tileX, y))
                    {
                        exposedToSunlight = false;
                        break;
                    }
                }

                // If exposed to sunlight, apply the OnFire! debuff
                if (exposedToSunlight)
                {
                    Player.AddBuff(BuffID.OnFire, 120); // On fire for 2 second, will reapply every tick
                }
            }

            Vector2 dustPosition = Player.Center + new Vector2(0, -40);

            int maxDust = 10;

            Microsoft.Xna.Framework.Color dustColor = soulCurrent > soulMax
                    ? Color.Orange
                    : Color.Purple;

            for (int i = 0; i > maxDust; i++)
            {
                    Dust.NewDust(dustPosition, 0, 0, DustID.RainbowMk2, 0f, 0f, 0, dustColor, 1.5f);
            }

            if (stealTimer > 0)
            {
                stealTimer--;
            }

            if (!Player.HasItem(ModContent.ItemType<CursedOilPot>()))
            {
                cursedOilPot = false;
            }
        }

        public void RaceBuffs(Player player)
        { 
            if (isHuman && raceChosen == false)
            {
                player.manaRegen += 3;
                raceChosen = true;
            }
            else if (isDryad && raceChosen == false)
            {
                player.lifeRegen += 3;
                raceChosen = true;
            }
            else if (isLizhard && raceChosen == false)
            {
                player.statDefense += 3;
                raceChosen = true;
            }
            else if (isVampire && raceChosen == false)
            {
                raceChosen = true;
            }
            else if (isElf && raceChosen == false)
            {
                player.GetDamage(DamageClass.Ranged) += 0.03f;
                raceChosen = true;
            }
            else
            {
                player.moveSpeed += 0.07f;
            }
        }

        public bool hasRecievedStartingItem = false;

        public override void OnEnterWorld()
        {
            if (!hasRecievedStartingItem)
            {
                Player.QuickSpawnItem(null, ModContent.ItemType<HumanRace>());
                hasRecievedStartingItem = true;
            }
        }

        public bool cursedOilPot;
        float debuffChance = 0.10f;

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (isVampire)
            {
                VampSteal(damageDone);
            }

            debuffChance = MathHelper.Clamp(debuffChance, 0f, 1f);

            if (cursedOilPot == true)
            {
                if (Main.rand.NextFloat() < debuffChance)
                {
                    target.AddBuff(BuffID.CursedInferno, 120);
                }
            }
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (isVampire)
            {
                VampSteal(damageDone);
            }
        }

        private float lifeStealPerc = 0.05f;
        private int lifeStealCD = 120;
        private int stealTimer = 120;

        private void VampSteal(int damageDone)
        {
            if(stealTimer <= 0)
            {
                int lifeToHeal = (int)(damageDone * lifeStealPerc);

                int maxStealaHit = 4;
                if (Main.hardMode)
                {
                    maxStealaHit = (int)(Player.statLifeMax * 0.02f);
                }

                if (lifeToHeal > lifeStealPerc)
                {
                    lifeToHeal = maxStealaHit;
                }

                Player.statLife += lifeToHeal;
                Player.HealEffect(lifeToHeal);

                stealTimer = lifeStealCD;
            }
        }
    }
}
