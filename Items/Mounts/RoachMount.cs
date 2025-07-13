using System.Linq;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;

namespace Witcherria.Items.Mounts
{
    public class RoachMount : ModMount
    {
        public override void SetStaticDefaults()
        {
            MountData.spawnDust = 3;
            MountData.buff = BuffType<Buffs.RoachMountBuff>();
            MountData.heightBoost = 34;
            MountData.flightTimeMax = 0;
            MountData.fallDamage = 0.5f;
            MountData.runSpeed = 3f;
            MountData.dashSpeed = 9f;
            MountData.acceleration = 0.25f;
            MountData.jumpHeight = 6;
            MountData.jumpSpeed = 7.01f;
            MountData.totalFrames = 16;
            int[] array = new int[MountData.totalFrames];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = 28;
            }
            array[3] += 2;
            array[4] += 2;
            array[7] += 2;
            array[8] += 2;
            array[12] += 2;
            array[13] += 2;
            array[15] += 4;
            MountData.playerYOffsets = array;
            MountData.xOffset = 5;
            MountData.bodyFrame = 3;
            MountData.yOffset = 1;
            MountData.playerHeadOffset = 34;
            MountData.standingFrameCount = 1;
            MountData.standingFrameDelay = 12;
            MountData.standingFrameStart = 0;
            MountData.runningFrameCount = 7;
            MountData.runningFrameDelay = 15;
            MountData.runningFrameStart = 1;
            MountData.dashingFrameCount = 6;
            MountData.dashingFrameDelay = 40;
            MountData.dashingFrameStart = 9;
            MountData.flyingFrameCount = 6;
            MountData.flyingFrameDelay = 6;
            MountData.flyingFrameStart = 1;
            MountData.inAirFrameCount = 1;
            MountData.inAirFrameDelay = 12;
            MountData.inAirFrameStart = 15;
            MountData.idleFrameCount = 0;
            MountData.idleFrameDelay = 0;
            MountData.idleFrameStart = 0;
            MountData.idleFrameLoop = false;
            MountData.swimFrameCount = MountData.inAirFrameCount;
            MountData.swimFrameDelay = MountData.inAirFrameDelay;
            MountData.swimFrameStart = MountData.inAirFrameStart;
            if (Main.netMode != NetmodeID.Server)
            {
                //MountData.backTexture = texture;
                //MountData.backTextureExtra = Asset.Empty;
                //MountData.frontTexture = Asset.Empty;
                //MountData.frontTextureExtra = Asset.Empty;
                MountData.textureWidth = MountData.backTexture.Width();
                MountData.textureHeight = MountData.backTexture.Height();
            }
        }
    }
}
