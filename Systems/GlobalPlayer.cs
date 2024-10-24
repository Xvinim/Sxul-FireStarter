using Microsoft.Xna.Framework.Content;
using Sxul.Content.Armor.Vanilla.GildedTungsten;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Sxul.Systems
{
    internal class GlobalPlayer : ModPlayer
    {
        /* To set up a dashType in an accessory/armor.. do this Below
        Inside --> UpdateEquip(Player player)
        GlobalPlayer mps = player.GetModPlayer<GlobalPlayer>();
            mps.gildedCopper = true;
            mps.gildedSetDash = true;
            mps.dashType = GlobalPlayer.Dashes.gilded;
         */
        #region Accessories
        public bool hasTheDamnStimShot = false;
        public bool hasTheDamnDevil = false;
        public bool hasTheDamnCursedOil = false;
        public bool hasTheDamnBrainCell = false;
        public bool hasTheDamnBrainofTrueConfusion = false;
        public bool hasTheDamnConfusionMask = false;
        public bool hasTheDamnEvilCell = false;
        public bool hasTheDamnNeauron = false;
        public bool hasTheDamnCursedSeed = false;
        public bool hasTheFuckingCursedRock = false;
        public bool hasTheFuckingHeavenlyCorruption = false;
        public bool hasTheFuckingDecayCrown = false;
        public bool hasTheFuckingEngravedFire = false;
        public bool hasTheFuckingFlameKing = false;
        public bool hasTheFuckingFlameTalisman = false;
        public bool hasTheFuckingHBlaze = false;
        public bool hasTheFuckingHFire = false;
        public bool hasTheFuckingPearlKing = false;
        public bool hasADumbassSoulCharade = false;
        public bool hasADumbassSoulKing = false;
        public bool hasADumbassAngelicColdness = false;
        public bool hasADumbassIceRock = false;
        public bool hasADumbassIceStone = false;
        public bool hasADumbassMossyStone = false;
        public bool hasADumbassPoisonRock = false;
        public bool hasADumbassPosionStone = false;
        public bool hasADumbassPoisonVile = false;
        public bool hasFuckingSoulPoison = false;
        public bool hasFuckingVineClump = false;
        public bool hasFuckingCelestialBodies = false;
        public bool hasFuckingCorruptedCellStone = false;
        public bool hasFuckingEngravedStone = false;
        public bool hasFuckingEvilRock = false;
        public bool hasHeavenlyStone = false;
        public bool hasSoulStone = false;
        public bool hasTrueFlame = false;
        #endregion
        #region Armor
        public bool gildedCopper = false;
        public bool gildedSilver = false;
        public bool gildedTungsten = false;
        public bool etchedGreatHelm = false;
        public bool meleeSilver = false;
        #endregion
        #region Dashes
        public bool gildedSetDash = false;
        public bool gildedHMSetDash = false;
        //Below is not to be messed with, add the dashType types above.
        public bool dashable = false;
        #endregion

        public override void PostUpdate()
        {
            //when new dashes are made do a && and add that dashType.
            if (gildedSetDash)
            {
                dashable = true;
            }
        }

        public enum Dashes
        {
            gilded = 0,
            gildedHM = 1,
            invalid = -1
        }

        public Dashes dashType = Dashes.invalid;

        public const int dashDown = 0;
        public const int dashUp = 1;
        public const int dashRight = 2;
        public const int dashLeft = 3;

        public int dashDuration = 15;
        public int dashCooldown = 45;

        public float dashVelocity = 7.5f;

        public int dashDir = -1;

        public int dashDelay = 0;
        public int dashTimer = 0;

        public override void ResetEffects()
        {
            gildedSetDash = false;
            gildedHMSetDash = false;

            gildedCopper = false;
            gildedSilver = false;
            gildedTungsten = false;
            meleeSilver = false;
            etchedGreatHelm = false;

            if (Player.controlRight &&  Player.releaseRight && Player.doubleTapCardinalTimer[dashRight] < 15)
            {
                dashDir = dashRight;
            }
            else if (Player.controlLeft && Player.releaseLeft && Player.doubleTapCardinalTimer[dashLeft] < 15)
            {
                dashDir = dashLeft;
            }
            else
            {
                dashDir = -1;
            }

            hasTheDamnStimShot = false;
            hasTheDamnDevil = false;
            hasTheDamnCursedOil = false;
            hasTheDamnBrainCell = false;
            hasTheDamnBrainofTrueConfusion = false;
            hasTheDamnConfusionMask = false;
            hasTheDamnEvilCell = false;
            hasTheDamnNeauron = false;
            hasTheDamnCursedSeed = false;
            hasTheFuckingCursedRock = false;
            hasTheFuckingHeavenlyCorruption = false;
            hasTheFuckingDecayCrown = false;
            hasTheFuckingEngravedFire = false;
            hasTheFuckingFlameKing = false;
            hasTheFuckingFlameTalisman = false;
            hasTheFuckingHBlaze = false;
            hasTheFuckingHFire = false;
            hasTheFuckingPearlKing = false;
            hasADumbassSoulCharade = false;
            hasADumbassSoulKing = false;
            hasADumbassAngelicColdness = false;
            hasADumbassIceRock = false;
            hasADumbassIceStone = false;
            hasADumbassMossyStone = false;
            hasADumbassPoisonRock = false;
            hasADumbassPosionStone = false;
            hasADumbassPoisonVile = false;
            hasFuckingSoulPoison = false;
            hasFuckingVineClump = false;
            hasFuckingCelestialBodies = false;
            hasFuckingCorruptedCellStone = false;
            hasFuckingEngravedStone = false;
            hasFuckingEvilRock = false;
            hasHeavenlyStone = false;
            hasSoulStone = false;
            hasTrueFlame = false;

            switch (dashType)
            {
                case Dashes.gilded:
                    dashVelocity = 8f;
                    dashDuration = 10;
                    break;
                case Dashes.gildedHM:
                    dashVelocity = 16f;
                    dashDuration = 25;
                    break;
                case Dashes.invalid:
                    dashVelocity = 0.0f;
                    dashDuration = 0;
                    break;

                default:
                    return;
            }

            dashType = Dashes.invalid;
        }

        public override void PreUpdateMovement()
        {
            // If the player has initiated a dash and dashTimer is active, keep dashing
            if (dashTimer > 0)
            {
                // Apply dash effects and continue the dash
                Player.eocDash = dashTimer;  // Lock the player in the dash animation
                ApplyDashEffects();  // Apply visual effects during dash
                dashTimer--;  // Decrease the dash timer

                // After the dash ends, set the cooldown and reset the dash direction
                if (dashTimer == 0)
                {
                    dashCooldown = dashDelay;  // Start the cooldown after dashing
                    dashDir = -1;  // Reset the dash direction
                }
            }
            else if (dashCooldown > 0)
            {
                // Decrease the cooldown timer if it's active
                dashCooldown--;
            }
            else if (dashDir != -1 && CanUseDash())
            {
                // If the player double-tapped and the dash can be triggered, start the dash
                Microsoft.Xna.Framework.Vector2 newVelocity = Player.velocity;

                // Set the velocity based on the dash direction
                switch (dashDir)
                {
                    case dashLeft:
                        if (Player.velocity.X > -dashVelocity)
                        {
                            newVelocity.X = -dashVelocity;  // Dash to the left
                        }
                        break;
                    case dashRight:
                        if (Player.velocity.X < dashVelocity)
                        {
                            newVelocity.X = dashVelocity;  // Dash to the right
                        }
                        break;
                }

                // Apply the dash velocity to the player
                Player.velocity = newVelocity;

                // Set dash timers
                dashTimer = dashDuration;  // Start dash duration
                dashCooldown = dashDelay;  // Start dash delay (cooldown)

                // Play sound based on dash type
                switch (dashType)
                {
                    case Dashes.gilded:
                        SoundEngine.PlaySound(SoundID.DD2_PhantomPhoenixShot, Player.position);
                        break;
                    case Dashes.gildedHM:
                        SoundEngine.PlaySound(SoundID.DD2_MonkStaffSwing, Player.position);
                        break;
                }
            }
        }

        private void PlayDashSound()
        {
            var player = Player;
            switch (dashType)
            {
                case Dashes.gilded:
                    SoundEngine.PlaySound(SoundID.DD2_PhantomPhoenixShot, player.position);
                    break;
                case Dashes.gildedHM:
                    SoundEngine.PlaySound(SoundID.DD2_MonkStaffSwing, player.position);
                    break;
                default:
                    SoundEngine.PlaySound(SoundID.DD2_BetsyWindAttack, player.position);
                    break;
            }
        }

        private void ApplyDashEffects()
        {
            // Apply dash dust effect
            switch (dashType)
            {
                case Dashes.gilded:
                    int dust = Dust.NewDust(Player.position, Player.width, Player.height, DustID.Cloud, 0, 0, default, default, Main.rand.NextFloat(0.75f, 1.25f));
                    Main.dust[dust].velocity.Y = 0;
                    Main.dust[dust].velocity.X = Player.direction * -1;
                    break;
            }

            Player.eocDash = dashTimer;
        }

        private bool CanUseDash()
        {
            return dashable
                && Player.dashType == 0
                && !Player.setSolar
                && !Player.mount.Active;
        }
    }
}
