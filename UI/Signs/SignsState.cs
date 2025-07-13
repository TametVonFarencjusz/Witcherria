using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader.UI;
using Terraria.UI;
using Witcherria.Players;
using static Terraria.ModLoader.ModContent;

namespace Witcherria.UI.Signs
{
    internal class SignsState : UIState
    {
        // For this bar we'll be using a frame texture and then a gradient inside bar, as it's one of the more simpler approaches while still looking decent.
        // Once this is all set up make sure to go and do the required stuff for most UI's in the Mod class.
        private UIElement area;
        private Asset<Texture2D> textureSignAard = Request<Texture2D>("Witcherria/UI/Signs/SignAard");
        private Asset<Texture2D> textureSignIgni = Request<Texture2D>("Witcherria/UI/Signs/SignIgni");
        private Asset<Texture2D> textureSignYrden = Request<Texture2D>("Witcherria/UI/Signs/SignYrden");
        private Asset<Texture2D> textureSignQuen = Request<Texture2D>("Witcherria/UI/Signs/SignQuen");
        private Asset<Texture2D> textureSignAxii = Request<Texture2D>("Witcherria/UI/Signs/SignAxii");
        private Asset<Texture2D> textureSignNone = Request<Texture2D>("Witcherria/UI/Signs/SignNone");
        private Asset<Texture2D> textureSense = Request<Texture2D>("Witcherria/UI/Signs/WitcherSense");
        private Asset<Texture2D> textureRoach = Request<Texture2D>("Witcherria/UI/Signs/Roach");


        private Dictionary<UIImageButton, string> buttons = new Dictionary<UIImageButton, string> { };

        private readonly int SIGN_COUNT = 8;
        private const float CIRCLE_RADIUS = 72f;

        public override void OnInitialize()
        {
            Asset<Texture2D>[] textures = new Asset<Texture2D>[] { textureSignYrden, textureSignQuen, textureSignIgni, textureSignAxii, textureSignAard, textureSense, textureSignNone, textureRoach };
            String[] hoverText = new String[] { "Yrden", "Quen", "Igni", "Axii", "Aard", "Witcher Sense", "None", "Roach" };
            MouseEvent[] SIGNS_EVENTS = new MouseEvent[] {
                SignYrdenButtonClicked,
                SignQuenButtonClicked,
                SignIgniPlayerButtonClicked,
                SignAxiiButtonClicked,
                SignAardButtonClicked,
                WitcherSenseButtonClicked,
                SignNoneButtonClicked,
                RoachButtonClicked,
            };

            // Create a UIElement for all the elements to sit on top of, this simplifies the numbers as nested elements can be positioned relative to the top left corner of this element. 
            // UIElement is invisible and has no padding. You can use a UIPanel if you wish for a background.
            area = new UIElement();
            area.Left.Set(-area.Width.Pixels - 1200, 1f); // Place the resource bar to the left of the hearts.
            area.Top.Set(500, 0f); // Placing it just a bit below the top of the screen.
            area.Width.Set(CIRCLE_RADIUS * 3, 0f); // We will be placing the following 2 UIElements within this 182x60 area.
            area.Height.Set(CIRCLE_RADIUS * 3, 0f);


            for (int i = 0; i < SIGN_COUNT; i++)
            {
                float alpha = (float)(2 * Math.PI * i / SIGN_COUNT);
                float x = (float)(-Math.Cos(alpha) * CIRCLE_RADIUS) + CIRCLE_RADIUS;
                float y = (float)(-Math.Sin(alpha) * CIRCLE_RADIUS) + CIRCLE_RADIUS;

                UIImageButton sign;
                sign = new UIImageButton(textures[i]);
                sign.Left.Set(x, 0f);
                sign.Top.Set(y, 0f);
                sign.Width.Set(40, 0f);
                sign.Height.Set(40, 0f);
                sign.OnLeftClick += new MouseEvent(SIGNS_EVENTS[i]);

                buttons.Add(sign, hoverText[i]);

                area.Append(sign);
            }

            Append(area);
        }

        private void SignAardButtonClicked(UIMouseEvent evt, UIElement listeningElement) => SignChangeUseStyle(SignPlayer.SignType.Aard);
        private void SignIgniPlayerButtonClicked(UIMouseEvent evt, UIElement listeningElement) => SignChangeUseStyle(SignPlayer.SignType.Igni);
        private void SignYrdenButtonClicked(UIMouseEvent evt, UIElement listeningElement) => SignChangeUseStyle(SignPlayer.SignType.Yrden);
        private void SignQuenButtonClicked(UIMouseEvent evt, UIElement listeningElement) => SignChangeUseStyle(SignPlayer.SignType.Quen);
        private void SignAxiiButtonClicked(UIMouseEvent evt, UIElement listeningElement) => SignChangeUseStyle(SignPlayer.SignType.Axii);
        private void SignNoneButtonClicked(UIMouseEvent evt, UIElement listeningElement) => SignChangeUseStyle(SignPlayer.SignType.None);
        private void RoachButtonClicked(UIMouseEvent evt, UIElement listeningElement) => SignChangeUseStyle(SignPlayer.SignType.Roach);

        private void WitcherSenseButtonClicked(UIMouseEvent evt, UIElement listeningElement) => SignChangeUseStyle(SignPlayer.SignType.WitcherSense);

        private void SignChangeUseStyle(SignPlayer.SignType type)
        {
            Player player = Main.LocalPlayer;
            player.GetModPlayer<SignPlayer>().SetSignType(type);

            Main.LocalPlayer.GetModPlayer<UIModPlayer>().signsShowCooldown = 2;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // This prevents drawing if it should not be drawn
            if (!Main.LocalPlayer.GetModPlayer<UIModPlayer>().signsShow || Main.LocalPlayer.GetModPlayer<UIModPlayer>().signsShowUpdate)
            {
                return;
            }

            base.Draw(spriteBatch);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            Main.LocalPlayer.mouseInterface = true;
            foreach (UIImageButton button in buttons.Keys)
            {
                if (button.IsMouseHovering)
                {
                    Main.hoverItemName = buttons[button];
                    //UICommon.TooltipMouseText(buttons[button]);
                }
            }

            base.DrawSelf(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            if (!Main.LocalPlayer.GetModPlayer<UIModPlayer>().signsShowUpdate)
            {
                return;
            }
			Main.LocalPlayer.GetModPlayer<UIModPlayer>().signsShowUpdate = false;

            area.Left.Set(-area.Width.Pixels / 2 + Main.mouseX + (Main.screenPosition.X - Main.LocalPlayer.Center.X) * 0, 0f);
            area.Top.Set(-area.Height.Pixels / 2 + Main.mouseY + (Main.screenPosition.Y - Main.LocalPlayer.Center.Y) * 0, 0f);

            Main.LocalPlayer.GetModPlayer<UIModPlayer>().signsShowVector = new Vector2(Main.mouseX, Main.mouseY);


            //SignPlayer.SignType signUseStyle = Main.LocalPlayer.GetModPlayer<SignPlayer>().GetSignType();
            base.Update(gameTime);
        }
    }
}
