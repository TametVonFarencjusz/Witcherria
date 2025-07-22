using Terraria;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using System.Linq;
using static Terraria.ModLoader.ModContent;

namespace Witcherria.NPCs
{
    public class MutatedHound : ModNPC
    {
        public override void SetStaticDefaults()
        {
            WitcherriaGlobalNPC.OtherAnimals.Append(NPC.type);
            Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.Wolf];

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            { // Influences how the NPC looks in the Bestiary
                Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.Wolf);
            NPC.width = 90;
            NPC.height = 52;
            NPC.damage = 9;
            NPC.defense = 1;
            NPC.lifeMax = 24;
            NPC.value = 20f;
            NPC.knockBackResist = 1.1f;
            NPC.HitSound = new SoundStyle($"{nameof(Witcherria)}/Sounds/DogHurt2");
            NPC.DeathSound = new SoundStyle($"{nameof(Witcherria)}/Sounds/DogDeath");
            AIType = NPCID.Wolf;
            AnimationType = NPCID.Wolf;
            //banner = NPC.type;
            //bannerItem = ItemType<Items.VoodoWarriorBanner>();
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<Items.Material.DarksteelScrap>(), 1, 1, 2));
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return SpawnCondition.OverworldDay.Chance * 0.007f;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("Gore_Hound1").Type, NPC.scale);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("Gore_Hound2").Type, NPC.scale);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("Gore_Hound3").Type, NPC.scale);
            }
        }
    }
}
