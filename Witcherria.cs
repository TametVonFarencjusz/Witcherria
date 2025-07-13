using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Witcherria
{
    // Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
    public class Witcherria : Mod
    {
        public override void Load()
        {
            if (!Main.dedServ)
            {

                if (Main.netMode != NetmodeID.Server)
                {
                    Ref<Effect> screenRef = new Ref<Effect>(ModContent.Request<Effect>("Witcherria/Effects/WitcherSenseShader", AssetRequestMode.ImmediateLoad).Value);
                    Filters.Scene["WitcherSenseShader"] = new Filter(new ScreenShaderData(screenRef, "WitcherSenseShader"), EffectPriority.Medium);
                    Filters.Scene["WitcherSenseShader"].Load();
                }
            }
        }
    }
}
