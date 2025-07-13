using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;


namespace Witcherria.Buffs
{
    public class QuenBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
			if (Main.rand.Next(5) == 0)
			{
				for (int index1 = 0; index1 < 3; ++index1)
				{
					int index2 = Dust.NewDust(player.position, player.width, player.height, DustID.OrangeTorch, 0.0f, 0.0f, 100, default(Color), 1.75f);
					Main.dust[index2].noGravity = true;
					Main.dust[index2].velocity *= 4f;
				}
			}
		}
    }
}
