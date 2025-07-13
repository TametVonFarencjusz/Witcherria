using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Witcherria.Projectiles
{
    public class Aard : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.aiStyle = -1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.timeLeft = 30;
            Projectile.ignoreWater = true;
            Projectile.friendly = true;
            Projectile.extraUpdates = 1;
            Projectile.penetrate = -1;
        }

        public override void AI()
        {
            float scale = 2.1f - Projectile.timeLeft / 30f;
            for (int i = 0; i < 2; i++)
            {
                int dustID = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustType<Dusts.AardDust>(), 0f, 0f, 100, default, scale);
                Main.dust[dustID].noGravity = true;
                Main.dust[dustID].velocity *= 1.5f - Projectile.timeLeft / 30f / 2f;
            }
        }
    }
}
