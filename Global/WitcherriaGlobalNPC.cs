using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Witcherria
{
    public class WitcherriaGlobalNPC : GlobalNPC
    {
        // Humanoids
        public static int[] TownNPCs = {
            NPCID.OldMan, NPCID.Demolitionist, NPCID.Guide, NPCID.Dryad, NPCID.ArmsDealer, NPCID.Nurse, NPCID.Merchant, NPCID.Clothier, NPCID.Wizard, NPCID.BoundWizard, NPCID.Mechanic, NPCID.BoundMechanic, NPCID.SantaClaus, NPCID.DyeTrader, NPCID.PartyGirl, NPCID.Cyborg, NPCID.Painter, NPCID.WitchDoctor, NPCID.Pirate, NPCID.Stylist, NPCID.WebbedStylist, NPCID.TravellingMerchant, NPCID.Angler, NPCID.SleepingAngler, NPCID.TaxCollector, NPCID.BartenderUnconscious, NPCID.DD2Bartender, NPCID.Golfer, NPCID.GolferRescue, NPCID.BestiaryGirl, NPCID.TownCat, NPCID.TownDog, NPCID.TownBunny, NPCID.Princess, NPCID.TownSlimeBlue, NPCID.TownSlimeGreen, NPCID.TownSlimeOld, NPCID.TownSlimePurple, NPCID.TownSlimeRainbow, NPCID.TownSlimeRed, NPCID.TownSlimeYellow, NPCID.TownSlimeCopper, NPCID.BoundTownSlimeYellow, NPCID.BoundTownSlimePurple, NPCID.BoundTownSlimeOld,
        };

        public static int[] Humans = {
            NPCID.PirateDeckhand, NPCID.PirateCorsair, NPCID.PirateDeadeye, NPCID.PirateCrossbower, NPCID.PirateCaptain,
            NPCID.Clown,
            NPCID.CultistArcherBlue, NPCID.CultistArcherWhite, NPCID.CultistDevote, NPCID.CultistBoss, NPCID.CultistBossClone,
            NPCID.Butcher, NPCID.Psycho, NPCID.Fritz,
            NPCID.LostGirl,
            NPCID.Paladin,
        };

        public static int[] Goblins = {
            NPCID.GoblinPeon, NPCID.GoblinThief, NPCID.GoblinWarrior, NPCID.GoblinSorcerer, NPCID.GoblinScout, NPCID.GoblinArcher, NPCID.GoblinTinkerer, NPCID.BoundGoblin, NPCID.GoblinSummoner,
            NPCID.DD2GoblinT1, NPCID.DD2GoblinT2, NPCID.DD2GoblinT3, NPCID.DD2GoblinBomberT1, NPCID.DD2GoblinBomberT2, NPCID.DD2GoblinBomberT3, NPCID.DD2JavelinstT1, NPCID.DD2JavelinstT2, NPCID.DD2JavelinstT3,
        };

        public static int[] Salamanders = {
            NPCID.Salamander, NPCID.Salamander2, NPCID.Salamander3, NPCID.Salamander4, NPCID.Salamander5, NPCID.Salamander6, NPCID.Salamander7, NPCID.Salamander8, NPCID.Salamander9,
        };


        public static int[] Martians = {
            NPCID.BrainScrambler, NPCID.RayGunner, NPCID.MartianOfficer, NPCID.GrayGrunt, NPCID.MartianEngineer, NPCID.GigaZapper, NPCID.ScutlixRider,
        };

        public static int[] Alien = {
            NPCID.StardustWormHead, NPCID.StardustWormBody, NPCID.StardustWormTail, NPCID.StardustCellBig, NPCID.StardustCellSmall, NPCID.StardustJellyfishBig, NPCID.StardustSpiderBig, NPCID.StardustSpiderSmall, NPCID.StardustSoldier, NPCID.SolarCrawltipedeHead, NPCID.SolarCrawltipedeBody, NPCID.SolarCrawltipedeTail, NPCID.SolarDrakomire, NPCID.SolarDrakomireRider, NPCID.SolarSroller, NPCID.SolarCorite, NPCID.SolarSolenian, NPCID.NebulaBrain, NPCID.NebulaHeadcrab, NPCID.LunarTowerVortex, NPCID.NebulaBeast, NPCID.NebulaSoldier, NPCID.VortexRifleman, NPCID.VortexHornetQueen, NPCID.VortexHornet, NPCID.VortexLarva, NPCID.VortexSoldier, NPCID.TargetDummy, NPCID.LunarTowerStardust, NPCID.LunarTowerNebula, NPCID.SolarFlare, NPCID.LunarTowerSolar, NPCID.SolarSpearman, NPCID.SolarGoop, NPCID.HallowBoss,
            NPCID.CultistDragonHead, NPCID.CultistDragonBody1, NPCID.CultistDragonBody2, NPCID.CultistDragonBody3, NPCID.CultistDragonBody4, NPCID.CultistDragonTail, NPCID.DD2EterniaCrystal,
        };

        public static int[] OtherHumanoids = {
            NPCID.SnowFlinx,
            NPCID.ElfArcher,
            NPCID.Gnome,
            NPCID.Lihzahrd, NPCID.LihzahrdCrawler,
            NPCID.DD2KoboldWalkerT2, NPCID.DD2KoboldWalkerT3, NPCID.DD2KoboldFlyerT2, NPCID.DD2KoboldFlyerT3,
        };


        // Animals
        public static int[] Critters = {
            NPCID.Firefly, NPCID.Butterfly, NPCID.Worm, NPCID.LightningBug, NPCID.Snail, NPCID.GlowingSnail, NPCID.Frog, NPCID.Duck, NPCID.Duck2, NPCID.DuckWhite, NPCID.DuckWhite2, NPCID.ScorpionBlack, NPCID.ScorpionBlack, NPCID.Grasshopper, NPCID.GoldBird, NPCID.GoldBunny, NPCID.GoldButterfly, NPCID.GoldFrog, NPCID.GoldGrasshopper, NPCID.GoldMouse, NPCID.GoldWorm, NPCID.EnchantedNightcrawler, NPCID.Grubby, NPCID.Sluggy, NPCID.Buggy, NPCID.SquirrelRed, NPCID.SquirrelGold, NPCID.PartyBunny, NPCID.FairyCritterPink, NPCID.FairyCritterGreen, NPCID.FairyCritterBlue, NPCID.GoldGoldfish, NPCID.GoldGoldfishWalker, NPCID.BlackDragonfly, NPCID.BlueDragonfly, NPCID.GreenDragonfly, NPCID.OrangeDragonfly, NPCID.RedDragonfly, NPCID.YellowDragonfly, NPCID.GoldDragonfly, NPCID.Seagull, NPCID.Seagull2, NPCID.LadyBug, NPCID.GoldLadyBug, NPCID.Maggot, NPCID.Pupfish, NPCID.Grebe, NPCID.Grebe2, NPCID.Rat, NPCID.Owl, NPCID.WaterStrider, NPCID.GoldWaterStrider, NPCID.ExplosiveBunny, NPCID.Dolphin, NPCID.Turtle, NPCID.TurtleJungle, NPCID.SeaTurtle, NPCID.Seahorse, NPCID.GoldSeahorse, NPCID.EmpressButterfly, NPCID.Stinkbug, NPCID.ScarletMacaw, NPCID.BlueMacaw, NPCID.Toucan, NPCID.YellowCockatiel, NPCID.GrayCockatiel,
            NPCID.Bunny, NPCID.Goldfish, NPCID.Bird, NPCID.AnglerFish,  NPCID.Penguin, NPCID.PenguinBlack,
            NPCID.GoldfishWalker, NPCID.BirdBlue, NPCID.BirdRed, NPCID.Squirrel, NPCID.Mouse, NPCID.Raven, NPCID.BunnySlimed, NPCID.BunnyXmas,
            NPCID.GemSquirrelAmethyst, NPCID.GemSquirrelTopaz, NPCID.GemSquirrelSapphire, NPCID.GemSquirrelEmerald, NPCID.GemSquirrelRuby, NPCID.GemSquirrelDiamond, NPCID.GemSquirrelAmber, NPCID.GemBunnyAmethyst, NPCID.GemBunnyTopaz, NPCID.GemBunnySapphire, NPCID.GemBunnyEmerald, NPCID.GemBunnyRuby, NPCID.GemBunnyDiamond, NPCID.GemBunnyAmber, NPCID.HellButterfly, NPCID.Lavafly, NPCID.MagmaSnail, NPCID.Shimmerfly
        };

        public static int[] Bats = {
            NPCID.CaveBat, NPCID.JungleBat, NPCID.GiantBat, NPCID.GiantFlyingFox, NPCID.IceBat, NPCID.Lavabat, NPCID.Hellbat,
        };

        public static int[] OtherAnimals = {
            NPCID.Parrot, NPCID.Vulture,
            NPCID.Piranha, NPCID.Shark, NPCID.Crab, NPCID.Arapaima, NPCID.SeaSnail, NPCID.Squid, NPCID.FlyingFish, NPCID.BloodFeeder,
            NPCID.GreenJellyfish, NPCID.BlueJellyfish, NPCID.PinkJellyfish, NPCID.BloodJelly,
            NPCID.GiantTortoise, NPCID.IceTortoise,
            NPCID.Wolf,
            NPCID.Bee, NPCID.BeeSmall, NPCID.QueenBee,
            NPCID.CochinealBeetle, NPCID.CyanBeetle, NPCID.LacBeetle,
            NPCID.Crawdad, NPCID.Crawdad2,
            NPCID.GiantShelly, NPCID.GiantShelly2,
            NPCID.SandShark,

            NPCID.BigHornetStingy, NPCID.LittleHornetStingy, NPCID.BigHornetSpikey, NPCID.LittleHornetSpikey, NPCID.BigHornetLeafy, NPCID.LittleHornetLeafy, NPCID.BigHornetHoney, NPCID.LittleHornetHoney, NPCID.BigHornetFatty, NPCID.LittleHornetFatty, NPCID.GiantMossHornet, NPCID.BigMossHornet, NPCID.LittleMossHornet, NPCID.TinyMossHornet, NPCID.BigStinger, NPCID.LittleStinger,
            NPCID.HornetFatty, NPCID.HornetHoney, NPCID.HornetLeafy, NPCID.HornetSpikey, NPCID.HornetStingy,
            NPCID.MossHornet,

            NPCID.BlackRecluse, NPCID.WallCreeper, NPCID.WallCreeperWall, NPCID.JungleCreeper, NPCID.JungleCreeperWall, NPCID.BlackRecluseWall, NPCID.BloodCrawler, NPCID.BloodCrawlerWall,
            NPCID.Derpling, NPCID.Moth, 
            NPCID.GiantWalkingAntlion, NPCID.GiantFlyingAntlion, NPCID.Antlion, NPCID.WalkingAntlion, NPCID.FlyingAntlion, NPCID.LarvaeAntlion,

            NPCID.DesertScorpionWalk, NPCID.DesertScorpionWall,

            NPCID.DiggerHead, NPCID.DiggerBody, NPCID.DiggerTail, NPCID.GiantWormHead, NPCID.GiantWormBody, NPCID.GiantWormTail,
            NPCID.DuneSplicerHead, NPCID.DuneSplicerBody, NPCID.DuneSplicerBody, NPCID.TombCrawlerHead, NPCID.TombCrawlerBody, NPCID.TombCrawlerTail,
            NPCID.WyvernHead, NPCID.WyvernLegs, NPCID.WyvernBody, NPCID.WyvernBody2, NPCID.WyvernBody3, NPCID.WyvernTail,

            NPCID.Scutlix,
            NPCID.Unicorn,
        };

        // Any Damage
        public static int[] Mechanical = {
            NPCID.Retinazer, NPCID.Spazmatism, NPCID.SkeletronPrime, NPCID.PrimeCannon, NPCID.PrimeSaw, NPCID.PrimeVice, NPCID.PrimeLaser, NPCID.TheDestroyer, NPCID.TheDestroyerBody, NPCID.TheDestroyerTail, NPCID.Probe,
            NPCID.Golem, NPCID.GolemHead, NPCID.GolemFistLeft, NPCID.GolemFistRight, NPCID.GolemHeadFree,
            NPCID.ElfCopter, NPCID.SantaNK1,
            NPCID.Nutcracker, NPCID.NutcrackerSpinning,
            NPCID.MartianTurret, NPCID.MartianDrone, NPCID.ForceBubble, NPCID.MartianSaucer, NPCID.MartianSaucerTurret, NPCID.MartianSaucerCannon, NPCID.MartianSaucerCore, NPCID.MartianProbe, NPCID.MartianWalker,
            NPCID.ChatteringTeethBomb, NPCID.DeadlySphere,
            NPCID.PirateShip, NPCID.PirateShipCannon,
            NPCID.CultistTablet,
            NPCID.TargetDummy,
        };

        public static int[] Slimes = {
            NPCID.MotherSlime, NPCID.BlueSlime, NPCID.Slimeling, NPCID.Slimer2, NPCID.GreenSlime, NPCID.Pinky, NPCID.BabySlime, NPCID.BlackSlime, NPCID.PurpleSlime, NPCID.RedSlime, NPCID.YellowSlime, NPCID.JungleSlime, NPCID.BigCrimslime, NPCID.LittleCrimslime, NPCID.LavaSlime, NPCID.DungeonSlime, NPCID.CorruptSlime, NPCID.Slimer, NPCID.ToxicSludge, NPCID.IceSlime, NPCID.Crimslime, NPCID.SpikedIceSlime, NPCID.SpikedJungleSlime, NPCID.UmbrellaSlime, NPCID.RainbowSlime, NPCID.SlimeMasked, NPCID.HoppinJack, NPCID.SlimeRibbonWhite, NPCID.SlimeRibbonYellow, NPCID.SlimeRibbonGreen, NPCID.SlimeRibbonRed, NPCID.SlimeSpiked, NPCID.SandSlime,
            NPCID.QueenSlimeBoss, NPCID.QueenSlimeMinionBlue, NPCID.QueenSlimeMinionPink, NPCID.QueenSlimeMinionPurple,
            NPCID.GoldenSlime, NPCID.ShimmerSlime, NPCID.KingSlime,
            NPCID.Gastropod,
        };

        // Yrden
        public static int[] Ghosts = {
            NPCID.Pixie, NPCID.Wraith, NPCID.Reaper, NPCID.DungeonSpirit, NPCID.FloatyGross, NPCID.Ghost, 
            NPCID.PresentMimic, NPCID.Mimic, NPCID.BigMimicCorruption, NPCID.BigMimicCrimson, NPCID.BigMimicHallow, NPCID.BigMimicJungle, NPCID.IceMimic,
            NPCID.ChaosElemental, NPCID.IlluminantSlime, NPCID.IlluminantBat, 
            NPCID.AncientCultistSquidhead, NPCID.AncientLight, NPCID.AncientDoom,
        };

        public static int[] Possesed = {
            NPCID.PossessedArmor,
            NPCID.CursedHammer, NPCID.EnchantedSword, NPCID.CrimsonAxe,
        };


        public static int[] VulnerableSilver = { };
        public static int[] VulnerableSteel = { };
        public static int[] VulnerableAll = { };
        public static int[] VulnerableYrden = { };


        public override void SetStaticDefaults()
        {
            VulnerableSteel = VulnerableSteel.Concat(TownNPCs).ToArray();
            VulnerableSteel = VulnerableSteel.Concat(Humans).ToArray();
            VulnerableSteel = VulnerableSteel.Concat(Goblins).ToArray();
            VulnerableSteel = VulnerableSteel.Concat(Salamanders).ToArray();
            VulnerableSteel = VulnerableSteel.Concat(Martians).ToArray();
            VulnerableSteel = VulnerableSteel.Concat(OtherHumanoids).ToArray();
            VulnerableSteel = VulnerableSteel.Concat(Critters).ToArray();
            VulnerableSteel = VulnerableSteel.Concat(Humans).ToArray();

            VulnerableAll = VulnerableAll.Concat(Slimes).ToArray();
            VulnerableAll = VulnerableAll.Concat(Mechanical).ToArray();

            VulnerableYrden = VulnerableYrden.Concat(Ghosts).ToArray();
            VulnerableYrden = VulnerableYrden.Concat(Possesed).ToArray();
        }

        public bool IsVulnerableToSteel(NPC npc)
        {
            return VulnerableSteel.Contains(npc.type);
        }

        public bool IsVulnerableToSilver(NPC npc)
        {
            return VulnerableSilver.Contains(npc.type) || (!VulnerableSteel.Contains(npc.type) && !VulnerableAll.Contains(npc.type) && npc.type <= 687);
        }

        public bool IsVulnerableToAll(NPC npc)
        {
            return VulnerableAll.Contains(npc.type) || (!VulnerableSteel.Contains(npc.type) && !VulnerableSilver.Contains(npc.type) && npc.type > 687);
        }

        public bool IsVulnerableToYrden(NPC npc)
        {
            return VulnerableYrden.Contains(npc.type);
        }


        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }

        public override void ResetEffects(NPC npc)
        {

        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {

        }

        public override void PostAI(NPC npc)
        {

        }

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {

        }

        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref NPC.HitModifiers modifiers)
        {
            if (WitcherriaGlobalItem.isSilver.ContainsKey(item.type))
            {
                ModifySilverHit(player, npc, ref modifiers, WitcherriaGlobalItem.isSilver[item.type]);
            }

            if (npc.HasBuff<Buffs.YrdenDebuff>() && IsVulnerableToYrden(npc))
            {
                ModifyYrdenHit(player, npc, ref modifiers);
            }

            if (npc.HasBuff<Buffs.IgniDebuff>())
            {
                ModifyIgniHit(player, npc, ref modifiers);
            }
        }

        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            Player player = Main.player[projectile.owner];
            if (WitcherriaGlobalProjectile.isSilver.ContainsKey(projectile.type))
            {
                ModifySilverHit(player, npc, ref modifiers, WitcherriaGlobalProjectile.isSilver[projectile.type]);
            }

            if (npc.HasBuff<Buffs.YrdenDebuff>() && IsVulnerableToYrden(npc))
            {
                ModifyYrdenHit(player, npc, ref modifiers);
            }

            if (npc.HasBuff<Buffs.IgniDebuff>())
            {
                ModifyIgniHit(player, npc, ref modifiers);
            }
        }

        private void ModifyIgniHit(Player player, NPC npc, ref NPC.HitModifiers modifiers)
        {
            modifiers.ScalingArmorPenetration += 0.5f;
        }

        private void ModifyYrdenHit(Player player, NPC npc, ref NPC.HitModifiers modifiers)
        {
            modifiers.SetCrit();
            modifiers.FinalDamage *= 1.25f;
        }

        private void ModifySilverHit(Player player, NPC npc, ref NPC.HitModifiers modifiers, bool usedSilver)
        {
            if (IsVulnerableToAll(npc))
            {
            }
            else if (!usedSilver && IsVulnerableToSteel(npc))
            {
            }
            else if (usedSilver && IsVulnerableToSilver(npc))
            {
            }
            else
            {
                if (npc.HasBuff<Buffs.YrdenDebuff>())
                {
                    modifiers.FinalDamage *= 0.5f;
                }
                else
                {
                    modifiers.FinalDamage *= 0.25f;
                }
            }
        }

        public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (false && npc.HasBuff<Buffs.YrdenDebuff>())
            {
                float scale = 1f + 0.25f * (float)Math.Sin(Main.timeForVisualEffects / 10f);
                float rotation = 0.25f * (float)Math.Cos(Main.timeForVisualEffects / 40f);

                Asset<Texture2D> texture = Request<Texture2D>("Witcherria/Extra/YrdenSign");
                spriteBatch.Draw
                (
                    texture.Value,
                    new Vector2
                    (
                        npc.position.X - screenPos.X + npc.width / 2,
                        npc.position.Y - screenPos.Y - 16
                    ),
                    new Rectangle(0, 0, texture.Width(), texture.Height()),
                    Color.White,
                    rotation,
                    texture.Size() * 0.5f,
                    scale * 0.75f,
                    SpriteEffects.None,
                    0f
                );
            }
            else if (Main.LocalPlayer.HasBuff<Buffs.WitcherSense>())
            {
                float scale = 0.7f + 0.1f * (float)Math.Sin(Main.timeForVisualEffects / 20f);

                if (IsVulnerableToSilver(npc))
                {
                    Asset<Texture2D> texture = Request<Texture2D>("Witcherria/Extra/VulnerableSilver");
                    spriteBatch.Draw
                    (
                        texture.Value,
                        new Vector2
                        (
                            npc.position.X - screenPos.X + npc.width / 2,
                            npc.position.Y - screenPos.Y - 16
                        ),
                        new Rectangle(0, 0, texture.Width(), texture.Height()),
                        Color.White,
                        0f,
                        texture.Size() * 0.5f,
                        scale,
                        SpriteEffects.None,
                        0f
                    );
                }
                else if (IsVulnerableToSteel(npc))
                {
                    Asset<Texture2D> texture = Request<Texture2D>("Witcherria/Extra/VulnerableSteel");
                    spriteBatch.Draw
                    (
                        texture.Value,
                        new Vector2
                        (
                            npc.position.X - screenPos.X + npc.width / 2,
                            npc.position.Y - screenPos.Y - 16
                        ),
                        new Rectangle(0, 0, texture.Width(), texture.Height()),
                        Color.White,
                        0f,
                        texture.Size() * 0.5f,
                        scale,
                        SpriteEffects.None,
                        0f
                    );
                }
            }
        }
    }
}