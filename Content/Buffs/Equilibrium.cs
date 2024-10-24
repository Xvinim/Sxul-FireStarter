using Sxul.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace Sxul.Content.Buffs
{
    internal class Equilibrium : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = false; // It's not a debuff
            Main.buffNoTimeDisplay[Type] = false; // Show the buff timer
            Main.pvpBuff[Type] = true; // Can apply in PvP
            Main.buffNoSave[Type] = false; // Buff will not be saved between sessions
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<BuffHandlerNPC>().hasEntropyImmunityBuff = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<BuffHandler>().hasEntropyImmunityBuff = true;
        }
    }
}
