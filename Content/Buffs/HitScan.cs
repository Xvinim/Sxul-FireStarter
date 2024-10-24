using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace Sxul.Content.Buffs
{
    internal class HitScan : ModBuff
    {
        private bool phase1 = true;
        private bool phase2 = false;
        private bool phase3 = false;
        private bool phase4 = false;
        private bool phase5 = false;
        private bool phaseF = false;

        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true; // It's a debuff, so it can't be canceled by the player
            Main.buffNoTimeDisplay[Type] = false; // Show the buff timer
            Main.pvpBuff[Type] = false; // Can apply in PvP
            Main.buffNoSave[Type] = false; // Buff will not be saved between sessions
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            if (npc.life > 0) // Check if the NPC is alive
            {
                if (phase1)
                {
                    npc.takenDamageMultiplier += 0.01f;
                    phase2 = true;
                }
                else if (phase2)
                {
                    npc.takenDamageMultiplier += 0.02f;
                    phase3 = true;
                }
                else if(phase3)
                {
                    npc.takenDamageMultiplier += 0.03f;
                    phase4 = true;
                }   
                else if (phase4)
                {
                    npc.takenDamageMultiplier += 0.04f;
                    phase5 = true;
                }
                else if (phase5)
                {
                    npc.takenDamageMultiplier += 0.05f;
                    phaseF = true;
                }

                if (phaseF)
                {
                    npc.takenDamageMultiplier += 0.07f;
                    phase1 = true;
                    phase2 = false;
                    phase3 = false;
                    phase4 = false;
                    phase5 = false;
                    phaseF = false;
                }
            }
        }
    }
}
