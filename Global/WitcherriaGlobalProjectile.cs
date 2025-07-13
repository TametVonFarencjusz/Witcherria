using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Witcherria.NPCs;
using static Terraria.ModLoader.ModContent;

namespace Witcherria
{
    public class WitcherriaGlobalProjectile : GlobalProjectile
    {
        public static Dictionary<int, bool> isSilver = new Dictionary<int, bool> { };


        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }
    }
}