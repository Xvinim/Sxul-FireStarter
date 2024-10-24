using Sxul.Assets.BossBars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Sxul.Config;

namespace Sxul.Content.Boss.Reworks
{
    internal class CustomBossBar : GlobalNPC
    {
        public bool notOverrided = false;

        public override bool InstancePerEntity => true;

        public override void SetDefaults(NPC entity)
        {
            if (entity.boss)
            {
                if (SxulConfig.Instance.EnableCustomBossBar)
                {
                    if (notOverrided == false)
                    {
                        entity.BossBar = ModContent.GetInstance<NewVanillaBossBarTexture>();
                    }
                }
            }
        }
    }
}
