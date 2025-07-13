using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Witcherria.Players;
using static Terraria.ModLoader.ModContent;


namespace Witcherria.Items.Swords
{
    public abstract class WitcherSword : ModItem
    {
        public override bool AltFunctionUse(Player player)
        {
            if (player.GetModPlayer<UIModPlayer>().altUseCooldownQuick > 0 || !Main.LocalPlayer.GetModPlayer<SignPlayer>().wolfMedallion)
            {
                player.GetModPlayer<UIModPlayer>().altUseCooldownQuick = 3;
                return false;
            }
            player.GetModPlayer<UIModPlayer>().altUseCooldownQuick = 3;
            return true;
        }

        public override void HoldItemFrame(Player player)
        {
            base.HoldItemFrame(player);
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                player.GetModPlayer<SignPlayer>().CastSign();
                return false;
            }
            else
            {
                if (Main.LocalPlayer.GetModPlayer<UIModPlayer>().signsShow)
                {
                    if (Main.LocalPlayer.GetModPlayer<UIModPlayer>().signsShowCooldown == -1)
                    {
                        Main.LocalPlayer.GetModPlayer<UIModPlayer>().signsShowCooldown = 20;
                    }
                    return false;
                }
            }
            return base.CanUseItem(player);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (WitcherriaGlobalItem.isSilver.ContainsKey(Item.type))
            {
                int index = tooltips.FindIndex(x => x.Name == "ItemName" && x.Mod == "Terraria");
                if (WitcherriaGlobalItem.isSilver[Item.type])
                {
                    TooltipLine silverTooltip = new TooltipLine(Mod, "Silver", "Silver Sword");
                    silverTooltip.OverrideColor = new Color(200, 200, 255);
                    tooltips.Insert(index + 1, silverTooltip);
                }
                else
                {
                    TooltipLine silverTooltip = new TooltipLine(Mod, "Silver", "Steel Sword");
                    silverTooltip.OverrideColor = new Color(200, 200, 200);
                    tooltips.Insert(index + 1, silverTooltip);
                }    
            }
        }
    }
}