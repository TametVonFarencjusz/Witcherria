using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using Witcherria.Items;
using Witcherria.Items.Swords;
using Witcherria.Players;

namespace Witcherria.UI.Vigor
{
    // This custom UI will show whenever the player has Wolf Medallion item and will display the player's Vigor resource amounts that are tracked in SignPlayer
    internal class VigorBar : UIState
    {
        // For this bar we'll be using a frame texture and then a gradient inside bar, as it's one of the more simpler approaches while still looking decent.
        // Once this is all set up make sure to go and do the required stuff for most UI's in the ModSystem class.
        private UIElement area;
        private UIImage barFrame;
        private UIImage iconFrame;

        public override void OnInitialize()
        {
            // Create a UIElement for all the elements to sit on top of, this simplifies the numbers as nested elements can be positioned relative to the top left corner of this element. 
            // UIElement is invisible and has no padding.
            area = new UIElement();
            area.Left.Set(-area.Width.Pixels - 600, 1f); // Place the resource bar to the left of the hearts.
            area.Top.Set(30, 0f); // Placing it just a bit below the top of the screen.
            area.Width.Set(182, 0f);
            area.Height.Set(60, 0f);

            barFrame = new UIImage(ModContent.Request<Texture2D>("Witcherria/UI/Vigor/VigorFrame")); // Frame of our resource bar
            barFrame.Left.Set(0, 0f);
            barFrame.Top.Set(0, 0f);
            barFrame.Width.Set(138, 0f);
            barFrame.Height.Set(34, 0f);

            iconFrame = new UIImage(ModContent.Request<Texture2D>("Witcherria/UI/Signs/Sign")); // Frame of our resource bar
            iconFrame.Left.Set(66, 0f);
            iconFrame.Top.Set(2, 0f);
            iconFrame.Width.Set(138, 0f);
            iconFrame.Height.Set(34, 0f);

            area.Append(barFrame);
            area.Append(iconFrame);
            Append(area);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!Main.LocalPlayer.GetModPlayer<SignPlayer>().wolfMedallion)
            {
                return;
            }

            base.Draw(spriteBatch);
        }

        // Here we draw our UI
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            var modPlayer = Main.LocalPlayer.GetModPlayer<SignPlayer>();
            // Calculate quotient
            float quotient = modPlayer.statVigor / 100f; // Creating a quotient that represents the difference of your currentResource vs your maximumResource, resulting in a float of 0-1f.
            quotient = Utils.Clamp(quotient, 0f, 1f); // Clamping it to 0-1f so it doesn't go over that.

            // Here we get the screen dimensions of the barFrame element, then tweak the resulting rectangle to arrive at a rectangle within the barFrame texture that we will draw the gradient. These values were measured in a drawing program.
            Rectangle hitbox = barFrame.GetInnerDimensions().ToRectangle();
            hitbox.X += 4;
            //hitbox.Width -= 24;
            hitbox.Y += 16;
            //hitbox.Height -= 16;

            int left = hitbox.Left;

            Texture2D textureEmpty = ModContent.Request<Texture2D>("Witcherria/UI/Vigor/VigorProgressEmpty").Value;
            spriteBatch.Draw(textureEmpty, new Vector2(left, hitbox.Y), new Rectangle(0, 0, textureEmpty.Width, textureEmpty.Height), Color.White);

            Texture2D texture = ModContent.Request<Texture2D>("Witcherria/UI/Vigor/VigorProgress").Value;
            spriteBatch.Draw(texture, new Vector2(left, hitbox.Y), new Rectangle(0, 0, (int)(texture.Width * quotient), texture.Height), Color.White);
        }

        public void UpdateSign()
        {
            switch(Main.LocalPlayer.GetModPlayer<SignPlayer>().GetSignType())
            {
                case SignPlayer.SignType.Aard: 
                    iconFrame.SetImage(ModContent.Request<Texture2D>("Witcherria/UI/Signs/SignAard"));
                    break;
                case SignPlayer.SignType.Igni: 
                    iconFrame.SetImage(ModContent.Request<Texture2D>("Witcherria/UI/Signs/SignIgni"));
                    break;
                case SignPlayer.SignType.Yrden: 
                    iconFrame.SetImage(ModContent.Request<Texture2D>("Witcherria/UI/Signs/SignYrden"));
                    break;
                case SignPlayer.SignType.Quen: 
                    iconFrame.SetImage(ModContent.Request<Texture2D>("Witcherria/UI/Signs/SignQuen"));
                    break;
                case SignPlayer.SignType.Axii: 
                    iconFrame.SetImage(ModContent.Request<Texture2D>("Witcherria/UI/Signs/SignAxii"));
                    break;
                case SignPlayer.SignType.WitcherSense: 
                    iconFrame.SetImage(ModContent.Request<Texture2D>("Witcherria/UI/Signs/WitcherSense"));
                    break;
                case SignPlayer.SignType.Roach: 
                    iconFrame.SetImage(ModContent.Request<Texture2D>("Witcherria/UI/Signs/Roach"));
                    break;
                default: 
                    iconFrame.SetImage(ModContent.Request<Texture2D>("Witcherria/UI/Signs/Sign"));
                    break;
            }
        }
    }

    // This class will only be autoloaded/registered if we're not loading on a server
    [Autoload(Side = ModSide.Client)]
    internal class VigorSystem : ModSystem
    {
        private UserInterface VigorBarUserInterface;

        internal VigorBar VigorBar;

        public static LocalizedText VigorText { get; private set; }

        public override void Load()
        {
            VigorBar = new();
            VigorBarUserInterface = new();
            VigorBarUserInterface.SetState(VigorBar);

            string category = "UI";
            VigorText ??= Mod.GetLocalization($"{category}.Vigor");
        }

        public void UpdateSign()
        {
            VigorBar.UpdateSign();
        }

        public override void UpdateUI(GameTime gameTime)
        {
            VigorBarUserInterface?.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int resourceBarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
            if (resourceBarIndex != -1)
            {
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "Witcherria: Vigor Bar",
                    delegate
                    {
                        VigorBarUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}