using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using System.Linq;
using static Terraria.ModLoader.ModContent;

namespace Witcherria.NPCs
{
    public class AlbaArbalest : ModNPC
    {
        public override void SetStaticDefaults()
        {
            WitcherriaGlobalNPC.Humans.Append(NPC.type);
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.PirateCrossbower];

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            { // Influences how the NPC looks in the Bestiary
                Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        }
        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.PirateCrossbower);
            NPC.width = 42;
            NPC.height = 60;
            NPC.damage = 31;
            NPC.defense = 4;
            NPC.lifeMax = 77;
            NPC.value = 50f;
            NPC.knockBackResist = 0.4f;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            AIType = NPCID.CultistArcherWhite;
            AnimationType = NPCID.PirateCrossbower;
        }


        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<Items.Material.DarksteelScrap>(), 1, 2, 5));
            npcLoot.Add(ItemDropRule.Common(ItemID.Hook, 25));
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.hardMode && !NPC.AnyNPCs(Type))
            {
                return SpawnCondition.OverworldNightMonster.Chance * 0.017f;
            }
            return 0;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("Gore_Arbalest1").Type, NPC.scale);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("Gore_Arbalest2").Type, NPC.scale);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("Gore_Arbalest3").Type, NPC.scale);
            }
        }
    }
}
