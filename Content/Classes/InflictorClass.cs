using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Sxul.Content.Classes
{
    public class InflictorClass : DamageClass
    {
        public override LocalizedText DisplayName => base.DisplayName;

        public override StatInheritanceData GetModifierInheritance(DamageClass damageClass)
        {
            if (damageClass == DamageClass.Melee)
            {
                return StatInheritanceData.Full;
            }
            return StatInheritanceData.None;
        }
    }
}
