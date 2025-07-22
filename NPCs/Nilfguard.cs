using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using System.Linq;
using static Terraria.ModLoader.ModContent;

namespace Witcherria.NPCs
{
    public class Nilfguard : ModNPC
    {
        public override void SetStaticDefaults()
        {
            WitcherriaGlobalNPC.Humans.Append(Type);
            Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.Skeleton];

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            { // Influences how the NPC looks in the Bestiary
                Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.GoblinThief);
            NPC.width = 42;
            NPC.height = 60;
            NPC.damage = 10;
            NPC.defense = 7;
            NPC.lifeMax = 157;
            NPC.value = 60f;
            NPC.knockBackResist = 0.4f;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath15;
            AIType = NPCID.Skeleton;
            AnimationType = NPCID.Skeleton;
        }


        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<Items.Material.DarksteelScrap>(), 1, 1, 4));
            npcLoot.Add(ItemDropRule.Common(ItemType<Items.Swords.FelineDarksteelSword>(), 15, 1, 1));
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return SpawnCondition.OverworldNightMonster.Chance * 0.007f;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("Gore_Guard1").Type, NPC.scale);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("Gore_Guard2").Type, NPC.scale);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("Gore_Guard3").Type, NPC.scale);
            }
        }
    }
}
