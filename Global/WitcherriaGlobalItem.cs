using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Witcherria
{
    public class WitcherriaGlobalItem : GlobalItem
    {
        public static Dictionary<int, bool> isSilver = new Dictionary<int, bool> { };
    }
}
