using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Sxul.Systems;
using Terraria.GameContent;

namespace Sxul.Content.Buffs
{
    internal class Entropy : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true; // It's a debuff, so it can't be canceled by the player
            Main.buffNoTimeDisplay[Type] = false; // Show the buff timer
            Main.pvpBuff[Type] = true; // Can apply in PvP
            Main.buffNoSave[Type] = false; // Buff will not be saved between sessions
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<BuffHandlerNPC>().hasEntropyDebuff = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<BuffHandler>().hasEntropyDebuff = true;
        }
    }
}
