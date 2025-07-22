using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using System.Linq;
using static Terraria.ModLoader.ModContent;

namespace Witcherria.NPCs
{
    public class ArmedBandit : ModNPC
    {
        public override void SetStaticDefaults()
        {
            WitcherriaGlobalNPC.Humans.Append(Type);
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Skeleton];

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            { // Influences how the NPC looks in the Bestiary
                Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        }
        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.AngryBones);
            NPC.width = 42;
            NPC.height = 60;
            NPC.damage = 10;
            NPC.defense = 2;
            NPC.lifeMax = 55;
            NPC.value = 20f;
            NPC.knockBackResist = 1f;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            AIType = NPCID.Skeleton;
            AnimationType = NPCID.Skeleton;
            //banner = NPC.type;
            //bannerItem = ItemType<Items.VoodoWarriorBanner>();
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<Items.Material.DarksteelScrap>(), 5, 1, 2));
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return SpawnCondition.OverworldDay.Chance * 0.015f;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("Gore_Bandit1").Type, NPC.scale);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("Gore_Bandit2").Type, NPC.scale);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("Gore_Bandit3").Type, NPC.scale);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("Gore_Bandit4").Type, NPC.scale);
            }
        }
    }
}
