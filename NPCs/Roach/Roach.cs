using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;
using static Terraria.ModLoader.ModContent;

namespace Witcherria.NPCs.Roach
{
    [AutoloadHead]
    public class Roach : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Unicorn];
            NPCID.Sets.NoTownNPCHappiness[Type] = true; // Prevents the happiness button
        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.Unicorn);
            NPC.damage = 0;
            NPC.defense = 999_999_999;
            NPC.lifeMax = 100;
            NPC.knockBackResist = 0f;
            NPC.value = 0f;

            NPC.townNPC = true;
            NPC.homeless = true;
            NPC.friendly = true;
            NPC.immortal = true;
            NPC.HitSound = null;
            NPC.DeathSound = null;
            AnimationType = NPCID.Unicorn;

            TownNPCStayingHomeless = true;
        }

        public override void AI()
        {
            Player owner = Main.player[NPC.target];
            if (owner.Center.Distance(NPC.Center) < 128f)
            {
                NPC.aiStyle = -1;
            }

            if (NPC.aiStyle == -1)
            {
                NPC.velocity.X *= 0.9f;
            }

            if (NPC.Center.Distance(owner.Center) >= 3000f)
            {
                NPC.immortal = false;
                NPC.townNPC = false;
                NPC.StrikeInstantKill();
            }
        }

        public override bool CanChat()
        {
            return true;
        }

        public override void ChatBubblePosition(ref Vector2 position, ref SpriteEffects spriteEffects)
        {
            position.X += NPC.direction * 22;
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = "Mount";
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            if (firstButton)
            {
                Main.LocalPlayer.direction = NPC.direction;
                Main.LocalPlayer.position = NPC.position + new Vector2(19f - 8f * NPC.direction, 0f);
                Main.LocalPlayer.mount.SetMount(MountType<Items.Mounts.RoachMount>(), Main.LocalPlayer);

                NPC.immortal = false;
                NPC.townNPC = false;
                NPC.StrikeInstantKill();
            }
        }

        public override string GetChat()
        {
            if (Main.LocalPlayer.HasBuff(BuffID.Tipsy))
            {
                WeightedRandom<string> chat = new WeightedRandom<string>();
                //chat.Add(Language.GetTextValue("Mods.Witcherria.Dialogue.Roach.TipsyDialogue1"));
                //chat.Add(Language.GetTextValue("Mods.Witcherria.Dialogue.Roach.TipsyDialogue2"));
                //chat.Add(Language.GetTextValue("Mods.Witcherria.Dialogue.Roach.TipsyDialogue3"));
                chat.Add("I will always come when you whistle. I'll be there for you!");
                return chat;
            }
            else
            {
                WeightedRandom<string> chat = new WeightedRandom<string>();
                //chat.Add(Language.GetTextValue("Mods.Witcherria.Dialogue.Roach.StandardDialogue1"));
                //chat.Add(Language.GetTextValue("Mods.Witcherria.Dialogue.Roach.StandardDialogue2"));
                //chat.Add(Language.GetTextValue("Mods.Witcherria.Dialogue.Roach.StandardDialogue3"));
                chat.Add("*horse noises*");
                return chat;
            }
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                
            }
        }
    }
}
