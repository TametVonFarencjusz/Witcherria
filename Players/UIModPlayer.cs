using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;
using Witcherria.Items.Swords;
using Witcherria.Systems;
using Witcherria.UI.Signs;
using static Terraria.ModLoader.ModContent;

namespace Witcherria.Players
{
    public class UIModPlayer : ModPlayer
    {
        public int altUseCooldownQuick = 0;

        // Signs
        public bool signsShow = false;
        public int signsShowCooldown = -1;
        public bool signsShowUpdate = false;

        public Vector2 signsShowVector = new Vector2(0, 0);


        public override void ResetEffects()
        {
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (WitcherriaKeybindSystem.ShowSignsKeybind.JustPressed)
            {
                ShowSigns();
            }
        }

        public void ShowSigns()
        {
            if (!Main.LocalPlayer.GetModPlayer<UIModPlayer>().signsShow && Main.LocalPlayer.GetModPlayer<SignPlayer>().wolfMedallion)
            {
                Main.LocalPlayer.GetModPlayer<UIModPlayer>().signsShow = true;
                Main.LocalPlayer.GetModPlayer<UIModPlayer>().signsShowUpdate = true;
                GetInstance<SignsSystem>().ShowMyUI();
            }
            else
            {
                Main.LocalPlayer.GetModPlayer<UIModPlayer>().signsShow = false;
            }
        }

        public override void PostUpdate()
        {
            if (Player == Main.LocalPlayer)
            {
                if (altUseCooldownQuick > 0)
                {
                    altUseCooldownQuick--;
                }

                if (signsShowCooldown > 0)
                {
                    signsShowCooldown--;
                }

                if (signsShowCooldown == 0)
                {
                    signsShow = false;
                    GetInstance<SignsSystem>().HideMyUI();
                    signsShowCooldown = -1;
                }

                if (Player.inventory[Player.selectedItem].ModItem is not WitcherSword)
                {
                    signsShow = false;
                    GetInstance<SignsSystem>().HideMyUI();
                }
            }
        }
    }
}
