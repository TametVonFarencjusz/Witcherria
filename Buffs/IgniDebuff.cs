using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;


namespace Witcherria.Buffs
{
    public class IgniDebuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
			if (Main.rand.Next(5) == 0)
			{
				for (int index1 = 0; index1 < 3; ++index1)
				{
					int index2 = Dust.NewDust(npc.position, npc.width, npc.height, DustID.Torch, 0.0f, 0.0f, 100, default(Color), 3f);
					Main.dust[index2].noGravity = true;
					Main.dust[index2].velocity *= 8f;
				}
			}
		}
    }
}
