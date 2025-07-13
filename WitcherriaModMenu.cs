using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ID;
using Terraria.ModLoader;

namespace Witcherria
{
    public class WitcherriaModMenu : ModMenu
    {
        private const string menuPath = "Witcherria/Extra"; // Creates a constant variable representing the texture path, so we don't have to write it out multiple times

        public override Asset<Texture2D> Logo => ModContent.Request<Texture2D>($"{menuPath}/Menu_Logo");//base.Logo;

        //public override Asset<Texture2D> SunTexture => ModContent.Request<Texture2D>($"{menuPath}/Menu_Sun");

        //public override Asset<Texture2D> MoonTexture => ModContent.Request<Texture2D>($"{menuPath}/Menu_Moon");

        public override int Music => MusicID.OtherworldlyUnderworld;

        public override string DisplayName => "Witcherria";

        public override bool PreDrawLogo(SpriteBatch spriteBatch, ref Vector2 logoDrawCenter, ref float logoRotation, ref float logoScale, ref Color drawColor)
        {
            logoScale *= 1.5f;
            return true;
        }
    }
}
