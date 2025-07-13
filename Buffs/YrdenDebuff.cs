using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;


namespace Witcherria.Buffs
{
    public class YrdenDebuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
			if (Main.rand.Next(10) == 0)
			{
				for (int index1 = 0; index1 < 2; ++index1)
				{
					int index2 = Dust.NewDust(npc.position, npc.width, npc.height, DustID.CorruptTorch, 0.0f, 0.0f, 100, default(Color), 1.75f);
					Main.dust[index2].noGravity = true;
					Main.dust[index2].velocity *= 4f;
				}
			}
		}
    }
}
