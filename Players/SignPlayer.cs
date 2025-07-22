using Microsoft.Xna.Framework;
using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameInput;
using Witcherria;
using Witcherria.Items.Swords;
using Witcherria.UI.Vigor;
using static Terraria.ModLoader.ModContent;

namespace Witcherria.Players
{
    public class SignPlayer : ModPlayer
    {
        public bool wolfMedallion = false;
        public bool swordsScabbard = false;

        public float statVigor = 100;

        public int signCastTime = 0;
        public Vector2 signCastDirection = Vector2.One;

        public int whistleTime = 0;

        public int witcherSenseTimer = 0;

        // Signs Stats
        private const int baseIgniDamage = 30;
        public float bonusMultIgniDamage = 1f;
        private const float baseIgniSpeed = 8f;
        public float bonusMultIgniSpeed = 1f;

        private const int baseAxiiDamage = 24;
        public float bonusMultAxiiDamage = 1f;

        private const int baseAardDamage = 1;
        public float bonusFlatAardDamage = 0;
        private const float baseAardSpeed = 5f;
        public float bonusMultAardSpeed = 1f;

        private const int baseQuenTime = 10;
        public float bonusFlatQuenTime = 0;


        //Aura Type
        private SignType signType = SignType.None;

        public enum SignType
        {
            None = -1,
            Aard = 0,
            Igni = 1,
            Yrden = 2,
            Quen = 3,
            Axii = 4,
            WitcherSense = 5,
            Roach = 6,
        }

        private Dictionary<SignType, Color> SignDustColor = new Dictionary<SignType, Color>
        {
            { SignType.None, Color.Black },
            { SignType.Aard, Color.Blue },
            { SignType.Igni, Color.Red },
            { SignType.Yrden, Color.Violet },
            { SignType.Quen, Color.Orange },
            { SignType.Axii, Color.Green },
            { SignType.WitcherSense, Color.DarkOrange },
            { SignType.Roach, Color.SkyBlue },
        };

        public void SetSignType(SignType useStyle)
        {
            signType = useStyle;
            GetInstance<VigorSystem>().UpdateSign();
        }

        public SignType GetSignType()
        {
            return signType;
        }

        public override void ResetEffects()
        {
            wolfMedallion = false;
            swordsScabbard = false;

            bonusMultIgniDamage = 1f;
            bonusMultIgniSpeed = 1f;

            bonusMultAxiiDamage = 1f;

            bonusFlatAardDamage = 0;
            bonusMultAardSpeed = 1f;

            bonusFlatQuenTime = 0;
        }

        // Valor
        public override void PreUpdate()
        {
            if (Main.netMode != NetmodeID.Server && Player == Main.LocalPlayer)
            {
                Filters.Scene["WitcherSenseShader"].GetShader().UseIntensity(witcherSenseTimer / 400f);
                if (Player.HasBuff<Buffs.WitcherSense>())
                {
                    Filters.Scene.Activate("WitcherSenseShader");
                    witcherSenseTimer++;
                    if (witcherSenseTimer > 60)
                    {
                        witcherSenseTimer = 60;
                    }
                }
                else
                {
                    if (--witcherSenseTimer < 0)
                    {
                        witcherSenseTimer = 0;
                        Filters.Scene["WitcherSenseShader"].Deactivate();
                    }
                }
            }

            if (statVigor < 100)
            {
                statVigor += 100f / 60f / 15f;
                if (statVigor > 100)
                {
                    statVigor = 100;
                }
            }
        }

        // Starting Item Changes
        public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
        {
            return new Item[] {
                new Item(ItemType<Items.Swords.CopperCoredRapier>()),
                new Item(ItemType<Items.Swords.CopperMesser>()),
                new Item(ItemType<Items.WolfMedallion>()),
            };
        }

        public override void ModifyStartingInventory(IReadOnlyDictionary<string, List<Item>> itemsByMod, bool mediumCoreDeath)
        {
            itemsByMod["Terraria"].RemoveAll(t => t.type == ItemID.CopperShortsword);
        }

        // Remove Witcher Sense
        public override void OnHurt(Player.HurtInfo info)
        {
            if (Player.HasBuff(BuffType<Buffs.WitcherSense>()))
            {
                Player.ClearBuff(BuffType<Buffs.WitcherSense>());
                SoundEngine.PlaySound(new SoundStyle($"{nameof(Witcherria)}/Sounds/SwordPaw"), Player.Center);
            }
        }

        public override void PostUpdate()
        {
            if (Player.HasBuff(BuffType<Buffs.WitcherSense>()))
            {
                if (Player.inventory[Player.selectedItem].ModItem is not WitcherSword)
                {
                    Player.ClearBuff(BuffType<Buffs.WitcherSense>());
                    SoundEngine.PlaySound(new SoundStyle($"{nameof(Witcherria)}/Sounds/SwordPaw"), Player.Center);
                }

                if (Math.Abs(Player.velocity.Y) > 2f)
                {
                    Player.ClearBuff(BuffType<Buffs.WitcherSense>());
                    SoundEngine.PlaySound(new SoundStyle($"{nameof(Witcherria)}/Sounds/SwordPaw"), Player.Center);
                }
            }

            if (whistleTime > 0)
            {
                whistleTime--;

                Player.CompositeArmStretchAmount arm = Player.CompositeArmStretchAmount.Full;
                int progress = Math.Abs(30 - whistleTime);
                if (progress >= 25)
                {
                    arm = Player.CompositeArmStretchAmount.None;
                }
                else if (progress >= 20)
                {
                    arm = Player.CompositeArmStretchAmount.Quarter;
                }
                else
                {
                    arm = Player.CompositeArmStretchAmount.ThreeQuarters;
                }

                Player.SetCompositeArmFront(true, arm, Player.direction * (MathHelper.ToRadians(-40) - MathHelper.ToRadians(90f))); // * Player.direction
            }


            if (signCastTime > 0)
            {
                signCastTime--;
                if (signCastTime == 15)
                {
                    ShootSign();
                }


                Player.CompositeArmStretchAmount arm = Player.CompositeArmStretchAmount.Full;
                int progress = Math.Abs(15 - signCastTime);
                if (progress >= 12)
                {
                    arm = Player.CompositeArmStretchAmount.None;
                }
                /*else if (progress >= 8)
                {
                    arm = Player.CompositeArmStretchAmount.Quarter;
                }*/
                /*else if (progress >= 4)
                {
                    arm = Player.CompositeArmStretchAmount.ThreeQuarters;
                }*/
                else
                {
                    arm = Player.CompositeArmStretchAmount.Quarter;
                }

                for (int i = 0; i < 10; i++)
                {
                    Vector2 pos = Player.GetFrontHandPosition(arm, signCastDirection.ToRotation() - (float)Math.PI / 2); // + signCastDirection * handRange

                    int index2 = Dust.NewDust(pos - new Vector2(4, 4), 8, 8, DustType<Dusts.InstantDust>(), 0.0f, 0.0f, 100, SignDustColor[signType], 2.25f);
                    Main.dust[index2].noGravity = true;
                    Main.dust[index2].velocity *= 0.5f;
                }

                Player.direction = Math.Sign(signCastDirection.X);
                Player.SetCompositeArmFront(true, arm, signCastDirection.ToRotation() - MathHelper.ToRadians(90f)); // * Player.direction
            }
        }

        public override bool CanUseItem(Item item)
        {
            if (signCastTime > 0 || whistleTime > 0)
            {
                return false;
            }
            return base.CanUseItem(item);
        }

        public void CastSign()
        {
            if (GetSignType() != SignType.None)
            {
                if (GetSignType() == SignType.WitcherSense)
                {
                    if (Player.HasBuff(BuffType<Buffs.WitcherSense>()))
                    {
                        Player.ClearBuff(BuffType<Buffs.WitcherSense>());
                        SoundEngine.PlaySound(new SoundStyle($"{nameof(Witcherria)}/Sounds/SwordPaw"), Player.Center);
                    }
                    else
                    {
                        Player.AddBuff(BuffType<Buffs.WitcherSense>(), 60 * 30);
                        SoundEngine.PlaySound(new SoundStyle($"{nameof(Witcherria)}/Sounds/SwordPaw"), Player.Center);
                    }
                }
                else if (GetSignType() == SignType.Roach)
                {
                    if (Main.netMode != NetmodeID.SinglePlayer)
                    {
                        whistleTime = 60;
                        SoundEngine.PlaySound(new SoundStyle($"{nameof(Witcherria)}/Sounds/RoachSound")
                        {
                            Volume = 1.2f,
                        }, Player.Center);
                        Main.LocalPlayer.mount.SetMount(MountType<Items.Mounts.RoachMount>(), Main.LocalPlayer);
                    }
                    else if (Player.mount.Type != MountType<Items.Mounts.RoachMount>() && whistleTime <= 0)
                    {
                        whistleTime = 60;
                        SoundEngine.PlaySound(new SoundStyle($"{nameof(Witcherria)}/Sounds/RoachSound")
                        {
                            Volume = 1.2f,
                        }, Player.Center);

                        int myRoach = -1;
                        for (int i = 0; i < 200; i++)
                        {
                            NPC targetNPC = Main.npc[i];
                            if (targetNPC.active && targetNPC.target == Player.whoAmI && targetNPC.type == NPCType<NPCs.Roach.Roach>())
                            {
                                myRoach = i;
                                break;
                            }
                        }

                        if (myRoach != -1 && Main.npc[myRoach].Center.Distance(Player.Center) >= 1000f)
                        {
                            Main.npc[myRoach].immortal = false;
                            Main.npc[myRoach].townNPC = false;
                            Main.npc[myRoach].StrikeInstantKill();

                            myRoach = -1;
                        }

                        if (myRoach != -1)
                        {
                            Main.npc[myRoach].direction = Math.Sign(Player.Center.X - Main.npc[myRoach].Center.X);
                            Main.npc[myRoach].aiStyle = NPCAIStyleID.Unicorn;
                        }
                        else
                        {
                            SpawnOnPlayer(NPCType<NPCs.Roach.Roach>());
                        }
                    }
                }
                else if (Player.GetModPlayer<SignPlayer>().statVigor >= 100)
                {
                    Player.GetModPlayer<SignPlayer>().statVigor = 0;
                    signCastTime = 30;

                    Vector2 position = Player.Center;
                    Vector2 positionMouse = new Vector2(Main.screenPosition.X + Main.mouseX, Main.screenPosition.Y + Main.mouseY);
                    if (Player.gravDir == -1)
                    {
                        positionMouse = new Vector2(Main.mouseX + Main.screenPosition.X, Main.screenPosition.Y + Main.screenHeight - Main.mouseY);
                    }
                    signCastDirection = (positionMouse - position).SafeNormalize(new Vector2(1f, 0f));
                }
            }
        }

        public void SpawnOnPlayer(int type)
        {
            //FieldInfo NPCSpawnRangeX = typeof(NPC).GetField("spawnRangeX", BindingFlags.NonPublic | BindingFlags.Static);
            //int spawnRangeX = (int)NPCSpawnRangeX.GetValue(null);
            int spawnRangeX = (int)(NPC.safeRangeX / 0.52f * 0.7f);
            //FieldInfo NPCSpawnRangeY = typeof(NPC).GetField("spawnRangeY", BindingFlags.NonPublic | BindingFlags.Static);
            //int spawnRangeY = (int)NPCSpawnRangeX.GetValue(null);
            int spawnRangeY = (int)(NPC.safeRangeY / 0.52f * 0.7f);
            //FieldInfo NPCSafeRangeX = typeof(NPC).GetField("safeRangeX", BindingFlags.NonPublic | BindingFlags.Static);
            //int safeRangeX = (int)NPCSpawnRangeX.GetValue(null);
            int safeRangeX = NPC.safeRangeX;
            //FieldInfo NPCSafeRangeY = typeof(NPC).GetField("safeRangeY", BindingFlags.NonPublic | BindingFlags.Static);
            //int safeRangeY = (int)NPCSpawnRangeX.GetValue(null);
            int safeRangeY = NPC.safeRangeY;
            //FieldInfo NPCSpawnSpaceX = typeof(NPC).GetField("spawnSpaceX", BindingFlags.NonPublic | BindingFlags.Static);
            //int spawnSpaceX = (int)NPCSpawnRangeX.GetValue(null);
            int spawnSpaceX = 3;
            //FieldInfo NPCSpawnSpaceY = typeof(NPC).GetField("spawnSpaceY", BindingFlags.NonPublic | BindingFlags.Static);
            //int spawnSpaceY = (int)NPCSpawnRangeX.GetValue(null);
            int spawnSpaceY = 3;


            bool flag = false;
            int num10 = 0;
            int num11 = 0;
            int num12 = (int)(Player.position.X / 16f) - spawnRangeX * 2;
            int num13 = (int)(Player.position.X / 16f) + spawnRangeX * 2;
            int num14 = (int)(Player.position.Y / 16f) - spawnRangeY * 2;
            int num15 = (int)(Player.position.Y / 16f) + spawnRangeY * 2;
            int num16 = (int)(Player.position.X / 16f) - safeRangeX;
            int num17 = (int)(Player.position.X / 16f) + safeRangeX;
            int num18 = (int)(Player.position.Y / 16f) - safeRangeY;
            int num19 = (int)(Player.position.Y / 16f) + safeRangeY;
            if (num12 < 0)
            {
                num12 = 0;
            }

            if (num13 > Main.maxTilesX)
            {
                num13 = Main.maxTilesX;
            }

            if (num14 < 0)
            {
                num14 = 0;
            }

            if (num15 > Main.maxTilesY)
            {
                num15 = Main.maxTilesY;
            }

            for (int m = 0; m < 1000; m++)
            {
                for (int n = 0; n < 100; n++)
                {
                    int num20 = Main.rand.Next(num12, num13);
                    int num21 = Main.rand.Next(num14, num15);
                    if (!Main.tile[num20, num21].HasUnactuatedTile || !Main.tileSolid[Main.tile[num20, num21].TileType])
                    {
                        /*if ((Main.wallHouse[Main.tile[num20, num21].WallType] && m < 999))
                        {
                            continue;
                        }*/

                        for (int num22 = num21; num22 < Main.maxTilesY; num22++)
                        {
                            if (Main.tile[num20, num22].HasUnactuatedTile && Main.tileSolid[Main.tile[num20, num22].TileType])
                            {
                                if ((num20 < num16 || num20 > num17 || num22 < num18 || num22 > num19 || m == 999) && ((num20 >= num12 && num20 <= num13 && num22 >= num14 && num22 <= num15) || m == 999))
                                {
                                    _ = Main.tile[num20, num22].TileType;
                                    num10 = num20;
                                    num11 = num22;
                                    flag = true;
                                }

                                break;
                            }
                        }

                        if (flag && m < 999)
                        {
                            int num24 = num10 - spawnSpaceX / 2;
                            int num25 = num10 + spawnSpaceX / 2;
                            int num26 = num11 - spawnSpaceY;
                            int num27 = num11;
                            if (num24 < 0)
                            {
                                flag = false;
                            }

                            if (num25 > Main.maxTilesX)
                            {
                                flag = false;
                            }

                            if (num26 < 0)
                            {
                                flag = false;
                            }

                            if (num27 > Main.maxTilesY)
                            {
                                flag = false;
                            }

                            if (flag)
                            {
                                for (int num28 = num24; num28 < num25; num28++)
                                {
                                    for (int num29 = num26; num29 < num27; num29++)
                                    {
                                        if (Main.tile[num28, num29].HasUnactuatedTile && Main.tileSolid[Main.tile[num28, num29].TileType])
                                        {
                                            flag = false;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (flag || flag)
                    {
                        break;
                    }
                }

                if (flag && m < 999)
                {
                    Rectangle rectangle = new Rectangle(num10 * 16, num11 * 16, 16, 16);
                    for (int num30 = 0; num30 < 255; num30++)
                    {
                        if (Main.player[num30].active)
                        {
                            Rectangle rectangle2 = new Rectangle((int)(Main.player[num30].position.X + (float)(Main.player[num30].width / 2) - (float)(NPC.sWidth / 2) - (float)safeRangeX), (int)(Main.player[num30].position.Y + (float)(Main.player[num30].height / 2) - (float)(NPC.sHeight / 2) - (float)safeRangeY), NPC.sWidth + safeRangeX * 2, NPC.sHeight + safeRangeY * 2);
                            if (rectangle.Intersects(rectangle2))
                            {
                                flag = false;
                            }
                        }
                    }
                }

                if (flag)
                {
                    break;
                }
            }

            if (flag)
            {
                int spawnPositionX = num10 * 16 + 8;
                int spawnPositionY = num11 * 16;
                SpawnRoach(spawnPositionX, spawnPositionY, type);
            }
        }

        public void SpawnRoach(int spawnPositionX, int spawnPositionY, int type)
        {
            int num = 200;
            num = NPC.NewNPC(NPC.GetBossSpawnSource(Player.whoAmI), spawnPositionX, spawnPositionY, type, 1);

            if (num == 200)
            {
                return;
            }

            Main.npc[num].target = Player.whoAmI;
            Main.npc[num].timeLeft *= 20;
            string typeName = Main.npc[num].TypeName;
            if (Main.netMode == 2 && num < 200)
            {
                NetMessage.SendData(23, -1, -1, null, num);
            }
        }

        public void ShootSign()
        {
            Vector2 position = Player.Center;

            Vector2 positionMouse = new Vector2(Main.screenPosition.X + Main.mouseX, Main.screenPosition.Y + Main.mouseY);
            if (Player.gravDir == -1)
            {
                positionMouse = new Vector2(Main.mouseX + Main.screenPosition.X, Main.screenPosition.Y + Main.screenHeight - Main.mouseY);
            }

            Vector2 velocity = (positionMouse - position).SafeNormalize(new Vector2(1f, 0f));


            if (GetSignType() == SignType.Aard)
            {
                int aardDamage = (int)(baseAardDamage + bonusFlatAardDamage);
                float aardSpeed = baseAardSpeed * bonusMultAardSpeed;

                position += Vector2.Normalize(velocity) * 10f; //Item defines the distance of the projectiles form the player when the projectile spawns

                int angle = 180;
                int numberProjectiles = angle / 3;
                for (int i = 0; i < numberProjectiles; i++)
                {

                    float rotation = MathHelper.ToRadians(angle);
                    Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1f))) * aardSpeed; ; // Item defines the projectile roatation
                    Projectile.NewProjectile(Player.GetSource_FromAI(), position, perturbedSpeed, ProjectileType<Projectiles.Aard>(), aardDamage, 50f, Player.whoAmI, 0, 0);
                }

                SoundEngine.PlaySound(new SoundStyle($"{nameof(Witcherria)}/Sounds/AardCast")
                {
                    Volume = 1.2f,
                }, Player.Center);
            }
            else if (GetSignType() == SignType.Igni)
            {
                int igniDamage = (int)(baseIgniDamage * bonusMultIgniDamage);
                float igniSpeed = baseIgniSpeed * bonusMultIgniSpeed;

                position += Vector2.Normalize(velocity) * 10f; //Item defines the distance of the projectiles form the player when the projectile spawns
                int numberProjectiles = 15;
                for (int i = 0; i < numberProjectiles; i++)
                {
                    float rotation = MathHelper.ToRadians(30);
                    Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1f))) * igniSpeed; // Item defines the projectile roatation
                    Projectile.NewProjectile(Player.GetSource_FromAI(), position, perturbedSpeed, ProjectileType<Projectiles.Igni>(), igniDamage, 0f, Player.whoAmI, 0, 0);
                }

                SoundEngine.PlaySound(new SoundStyle($"{nameof(Witcherria)}/Sounds/Igni2")
                {
                    Volume = 1.2f,
                }, Player.Center);
            }
            else if (GetSignType() == SignType.Yrden)
            {
                Projectile.NewProjectile(Player.GetSource_FromAI(), positionMouse, Vector2.Zero, ProjectileType<Projectiles.Yrden>(), 0, 0f, Player.whoAmI);

                SoundEngine.PlaySound(new SoundStyle($"{nameof(Witcherria)}/Sounds/YrdenStart")
                {
                    Volume = 1.2f,
                }, Player.Center);
            }
            else if (GetSignType() == SignType.Axii)
            {
                int axiiDamage = (int)(baseAxiiDamage * bonusMultAxiiDamage);

                Projectile.NewProjectile(Player.GetSource_FromAI(), positionMouse, Vector2.Zero, ProjectileType<Projectiles.Axii>(), axiiDamage, 0f, Player.whoAmI);

                SoundEngine.PlaySound(new SoundStyle($"{nameof(Witcherria)}/Sounds/Igni1")
                {
                    Volume = 1.2f,
                }, Player.Center);
            }
            else if (GetSignType() == SignType.Quen)
            {
                int quenTime = (int)(60 * (baseQuenTime + bonusFlatQuenTime));

                Player.AddBuff(BuffType<Buffs.QuenBuff>(), quenTime);

                SoundEngine.PlaySound(new SoundStyle($"{nameof(Witcherria)}/Sounds/QuenStart")
                {
                    Volume = 1.5f,
                }, Player.Center);
            }
        }

        // Sign: Quen
        public override void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers)
        {
            CheckQuen(ref modifiers);
        }

        public override void ModifyHitByProjectile(Projectile proj, ref Player.HurtModifiers modifiers)
        {
            CheckQuen(ref modifiers);
        }

        public void CheckQuen(ref Player.HurtModifiers modifiers)
        {
            if (Player.HasBuff(BuffType<Buffs.QuenBuff>()))
            {
                for (int i = 0; i < 50; ++i)
                {
                    int index2 = Dust.NewDust(Player.position, Player.width, Player.height, DustID.OrangeTorch, 0.0f, 0.0f, 100, default(Color), 2.75f);
                    Main.dust[index2].noGravity = true;
                    Main.dust[index2].velocity *= 7f;
                }
                modifiers.Cancel();
                Player.ClearBuff(BuffType<Buffs.QuenBuff>());
                Player.SetImmuneTimeForAllTypes(2 * 60);

                SoundEngine.PlaySound(new SoundStyle($"{nameof(Witcherria)}/Sounds/QuenEnd")
                {
                    Volume = 2f,
                }, Player.Center);
            }
        }

        public override void PostUpdateRunSpeeds()
        {
            if (Player.HasBuff<Buffs.WitcherSense>())
            {
                // Random Values - Should be set better after Jam
                Player.maxRunSpeed *= 0.1f;
                Player.moveSpeed *= 0.1f;
                Player.accRunSpeed = 2f;
            }
        }


        // Sync
        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            ModPacket packet = Mod.GetPacket();
            packet.Write((byte)Witcherria.MessageType.SignSyncPlayer);
            packet.Write((byte)Player.whoAmI);
            packet.Write((byte)signCastTime);
            packet.Write((byte)signType);
            packet.Send(toWho, fromWho);
        }


        // Called in ValhallaMod.Networking.cs
        public void ReceivePlayerSync(BinaryReader reader)
        {
            signCastTime = reader.ReadByte();
            signType = (SignType)reader.ReadByte();
        }

        public override void CopyClientState(ModPlayer targetCopy)
        {
            SignPlayer clone = (SignPlayer)targetCopy;
            clone.signCastTime = signCastTime;
            clone.signType = signType;
        }

        public override void SendClientChanges(ModPlayer clientPlayer)
        {
            SignPlayer clone = (SignPlayer)clientPlayer;
            if (signCastTime != clone.signCastTime || signType != clone.signType)
            {
                SyncPlayer(toWho: -1, fromWho: Main.myPlayer, newPlayer: false);
            }
        }
    }
}
