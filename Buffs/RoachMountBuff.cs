using Terraria;
using Terraria.ModLoader;

namespace Witcherria.Buffs
{
    public class RoachMountBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.mount.SetMount(ModContent.MountType<Items.Mounts.RoachMount>(), player);
            player.buffTime[buffIndex] = 10;
        }
    }
}
