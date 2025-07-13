using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Witcherria.Players
{
    public class BackSwordsPlayerDrawLayer : PlayerDrawLayer
    {
        private Asset<Texture2D> backpackItemTexture;

        // Returning true in this property makes this layer appear on the minimap player head icon.
        public override bool IsHeadLayer => true;

        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            return drawInfo.drawPlayer.GetModPlayer<SignPlayer>().swordsScabbard;
        }

        // This layer will be a 'child' of the head layer, and draw before (beneath) it.
        // If the Head layer is hidden, this layer will also be hidden.
        // If the Head layer is moved, this layer will move with it.
        public override Position GetDefaultPosition() => new BeforeParent(PlayerDrawLayers.Backpacks);
        // If you want to make a layer which isn't a child of another layer, use `new Between(Layer1, Layer2)` to specify the position.
        // If you want to make a 'mobile' layer which can render in different locations depending on the drawInfo, use a `Multiple` position.

        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            DrawBackpack(ref drawInfo);
        }

        public bool HasMetalSword(Player player, bool silver)
        {
            for (int i = 0; i < 58; i++)
            {
                if (player.inventory[i].stack > 0 && player.inventory[i].ModItem is Items.Swords.WitcherSword && WitcherriaGlobalItem.isSilver.ContainsKey(player.inventory[i].type) && WitcherriaGlobalItem.isSilver[player.inventory[i].type] == silver)
                {
                    return true;
                }
            }
            return false;
        }

        public void DrawBackpack(ref PlayerDrawSet drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;
            SignPlayer signPlayer = drawPlayer.GetModPlayer<SignPlayer>();

            Rectangle? sourceRect = null;
            int countFrames = 1;
            int frame = 0;


            bool hasSilver = HasMetalSword(drawPlayer, true);
            bool hasSteel = HasMetalSword(drawPlayer, false);

            if (WitcherriaGlobalItem.isSilver.ContainsKey(drawPlayer.HeldItem.type))
            {
                if (WitcherriaGlobalItem.isSilver[drawPlayer.HeldItem.type])
                {
                    if (hasSteel)
                    {
                        backpackItemTexture = Request<Texture2D>("Witcherria/Extra/BackSwords_OnlySteel");
                    }
                    else
                    {
                        backpackItemTexture = Request<Texture2D>("Witcherria/Extra/BackSwords_Empty");
                    }
                }
                else
                {
                    if (hasSilver)
                    {
                        backpackItemTexture = Request<Texture2D>("Witcherria/Extra/BackSwords_OnlySilver");
                    }
                    else
                    {
                        backpackItemTexture = Request<Texture2D>("Witcherria/Extra/BackSwords_Empty");
                    }
                }
            }
            else
            {
                if (hasSilver && hasSteel)
                {
                    backpackItemTexture = Request<Texture2D>("Witcherria/Extra/BackSwords_Full");
                }
                else if(hasSilver)
                {
                    backpackItemTexture = Request<Texture2D>("Witcherria/Extra/BackSwords_OnlySilver");
                }
                else if(hasSteel)
                {
                    backpackItemTexture = Request<Texture2D>("Witcherria/Extra/BackSwords_OnlySteel");
                }
                else
                {
                    backpackItemTexture = Request<Texture2D>("Witcherria/Extra/BackSwords_Empty");
                }
            }
            int height = backpackItemTexture.Height() / countFrames;
            int width = backpackItemTexture.Width();
            sourceRect = new Rectangle(0, height * frame, width, height);
            Vector2 origin = new Vector2(width / 2, height / 2);

            if (drawPlayer.dead || drawPlayer.invis)
            {
                return;
            }

            if (Main.gameMenu)
            {
                return;
            }

            int drawX = (int)(drawPlayer.Center.X - Main.screenPosition.X) - 13 * drawPlayer.direction;
            int drawY = (int)(drawPlayer.Center.Y + drawPlayer.gfxOffY - Main.screenPosition.Y) - 6;

            Vector2 center = new Vector2(drawX, drawY);
            Vector2 drawPos = center;

            Color color = Lighting.GetColor((int)drawPlayer.Center.X / 16, (int)drawPlayer.Center.Y / 16);

            color = drawPlayer.GetImmuneAlphaPure(color, drawInfo.shadow);
            color = drawPlayer.GetImmuneAlphaPure(color, 1f - drawPlayer.stealth);

            DrawData drawData = new DrawData(
                backpackItemTexture.Value, // The texture to render.
                drawPos, // Position to render at.
                sourceRect, // Source rectangle.
                color, // Color.
                0f, // Rotation.
                origin, // Origin. Uses the texture's center.
                1f, // Scale.
                drawPlayer.direction == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, // SpriteEffects.
                0 // 'Layer'. This is always 0 in Terraria.
            );
            //drawData.shader = PlayerDrawLayers.Backpacks.;
            drawInfo.DrawDataCache.Add(drawData);
        }
    }
}