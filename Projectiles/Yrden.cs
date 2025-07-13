using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Witcherria.Projectiles
{
    public class Yrden : ModProjectile
    {
        public float Distance => 300f;

        private const float endTime = 240;

        //********** Textures **********//
        private Asset<Texture2D> PixelTexture;
        private Asset<Texture2D> OrbTexture;
        private Asset<Texture2D> SignTexture;
        private Asset<Texture2D> RingTexture;


        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.aiStyle = -1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.timeLeft = 900;
            Projectile.ignoreWater = true;
            Projectile.alpha = 255;
        }

        public override void Load()
        {
            
        }

        public override void AI()
        {
            if (Projectile.alpha > 0)
            {
                Projectile.alpha -= 5;
            }

            ApplyDebuff();
            PlayerDeadCheck();
        }

        public void ApplyDebuff()
        {
            //NPC debuff
            for (int index2 = 0; index2 < 200; ++index2)
            {
                NPC targetNPC = Main.npc[index2];
                if (targetNPC.active && DistanceCheckHexagon(targetNPC, Distance))
                {
                    //Enemy NPC debuff
                    if (!targetNPC.friendly && targetNPC.damage > 0 && !targetNPC.immortal)
                    {
                        targetNPC.AddBuff(BuffID.Slow, 20, false);
                        targetNPC.AddBuff(BuffType<Buffs.YrdenDebuff>(), 20, false);
                    }
                }
            }
        }

        //General Distance Check
        public bool DistanceCheckHexagon(NPC npc, float distance)
        {
            float X = MathHelper.Clamp(Projectile.Center.X, npc.position.X, npc.position.X + npc.width);
            float Y = MathHelper.Clamp(Projectile.Center.Y, npc.position.Y, npc.position.Y + npc.height);
            if (Math.Abs(Y - Projectile.Center.Y) > distance * Math.Sqrt(3f) / 2f)
            {
                return false;
            }
            else if (Math.Abs(Y - Projectile.Center.Y) + Math.Abs(X - Projectile.Center.X) * Math.Sqrt(3f) > distance * Math.Sqrt(3f))
            {
                return false;
            }
            return true;
        }

        //Kill when player is dead
        public void PlayerDeadCheck()
        {
            if (Projectile.active && Projectile.timeLeft > 30)
            {
                if (Main.player[Projectile.owner].dead)
                {
                    Projectile.timeLeft = 30;
                }
            }
        }

        //Draw Circle
        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            Projectile.hide = true;
            overPlayers.Add(index);
            base.DrawBehind(index, behindNPCsAndTiles, behindNPCs, behindProjectiles, overPlayers, overWiresUI);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Color auraColor = new Color(100, 70, 150, 150);


            float ringAlpha = 1.0f;
            float auraAlpha = 0.5f;
            float auraOrbAlpha = 1.0f;
            float auraSignAlpha = 1.0f;

            float allAlpha = (255 - Projectile.alpha) / 255f;
            if (Projectile.timeLeft < endTime)
            {
                float alpha = (endTime - Projectile.timeLeft) / 80f;
                float cos = (float)Math.Cos(alpha * alpha * alpha);
                allAlpha *= cos * cos;
            }

            ringAlpha *= allAlpha;
            auraAlpha *= allAlpha;
            auraOrbAlpha *= allAlpha;
            auraSignAlpha *= allAlpha;


            PixelTexture = Request<Texture2D>("Witcherria/Extra/Pixel");
            OrbTexture = Request<Texture2D>("Witcherria/Projectiles/YrdenOrb");
            SignTexture = Request<Texture2D>("Witcherria/Projectiles/YrdenSign");
            RingTexture = Request<Texture2D>("Witcherria/Projectiles/YrdenAura");

            Color auraColorRing = new Color(auraColor.R, auraColor.G, auraColor.B, 255) * ringAlpha;
            Color auraColorField = new Color(auraColor.R, auraColor.G, auraColor.B, 0) * auraAlpha;
            auraColorField *= .8f + (float)Math.Sin(Projectile.timeLeft / 10) * .2f;
            Color auraColorOrb = new Color(auraColor.R, auraColor.G, auraColor.B, 0) * auraOrbAlpha;
            auraColorOrb *= .8f + (float)Math.Sin(Main.timeForVisualEffects / 10) * .2f;
            Color auraColorSign = new Color(auraColor.R, auraColor.G, auraColor.B, 0) * auraSignAlpha;
            auraColorSign *= 1.0f + (float)Math.Sin(Main.timeForVisualEffects / 10) * .0f;


            //Ring
            float sqrt3 = (float)Math.Sqrt(3);
            float sqrt3by2 = sqrt3 / 2f;
            int iMax = (int)(5 * Distance);
            for (int s = 0; s < 6; s++)
            {
                double angle = 2 * s * Math.PI / 6;

                for (int i = 0; i < iMax; i++)
                {
                    Vector2 pos = new Vector2
                        (
                            Projectile.position.X - Main.screenPosition.X + Projectile.width / 2 + (float)Math.Cos(angle) * Distance,
                            Projectile.position.Y - Main.screenPosition.Y + Projectile.height / 2 + (float)Math.Sin(angle) * Distance
                        );

                    switch (s)
                    {
                        case 0:
                            pos.X += -Distance / 2f * i / iMax;
                            pos.Y += Distance * sqrt3by2 * i / iMax;
                            break;
                        case 1:
                            pos.X += -Distance * i / iMax;
                            pos.Y += 0;
                            break;
                        case 2:
                            pos.X += -Distance / 2f * i / iMax;
                            pos.Y += -Distance * sqrt3by2 * i / iMax;
                            break;
                        case 3:
                            pos.X += Distance / 2f * i / iMax;
                            pos.Y += -Distance * sqrt3by2 * i / iMax;
                            break;
                        case 4:
                            pos.X += Distance * i / iMax;
                            pos.Y += 0;
                            break;
                        case 5:
                            pos.X += Distance / 2f * i / iMax;
                            pos.Y += Distance * sqrt3by2 * i / iMax;
                            break;
                    };
                    
                    Main.spriteBatch.Draw
                    (
                        PixelTexture.Value,
                        pos,
                        new Rectangle(0, 0, PixelTexture.Width(), PixelTexture.Height()),
                        auraColorRing,
                        (float)angle,
                        PixelTexture.Size() * 0.5f,
                        Projectile.scale,
                        SpriteEffects.None,
                        0f
                    );
                }
            }

            //Field
            float distScale = Distance / 300;
            Main.spriteBatch.Draw
            (
                RingTexture.Value,
                new Vector2
                (
                    Projectile.position.X - Main.screenPosition.X + Projectile.width / 2,
                    Projectile.position.Y - Main.screenPosition.Y + Projectile.height / 2
                ),
                new Rectangle(0, 0, RingTexture.Width(), RingTexture.Height()),
                auraColorField,
                0,
                RingTexture.Size() * 0.5f,
                distScale,
                SpriteEffects.None,
                0f
            );

            //Orbs
            const int orbCount = 6;
            for (int i = 0; i < orbCount; i++)
            {
                double angle = 2 * i * Math.PI / orbCount;

                Main.spriteBatch.Draw
                (
                    OrbTexture.Value,
                    new Vector2
                    (
                        Projectile.position.X - Main.screenPosition.X + Projectile.width / 2 + (float)Math.Cos(angle) * Distance,
                        Projectile.position.Y - Main.screenPosition.Y + Projectile.height / 2 + (float)Math.Sin(angle) * Distance
                    ),
                    new Rectangle(0, 0, OrbTexture.Width(), OrbTexture.Height()),
                    auraColorOrb,
                    (float)angle,
                    OrbTexture.Size() * 0.5f,
                    3f,
                    SpriteEffects.None,
                    0f
                );

                Main.spriteBatch.Draw
                (
                    SignTexture.Value,
                    new Vector2
                    (
                        Projectile.position.X - Main.screenPosition.X + Projectile.width / 2 + (float)Math.Cos(angle) * Distance,
                        Projectile.position.Y - Main.screenPosition.Y + Projectile.height / 2 + (float)Math.Sin(angle) * Distance
                    ),
                    new Rectangle(0, 0, SignTexture.Width(), SignTexture.Height()),
                    auraColorSign,
                    0f,
                    SignTexture.Size() * 0.5f,
                    2f,
                    SpriteEffects.None,
                    0f
                );
            }
            return false;
        }
    }
}