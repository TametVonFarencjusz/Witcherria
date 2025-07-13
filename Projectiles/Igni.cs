using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Witcherria.Projectiles
{
    public class Igni : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
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
            float scale = 3f - Projectile.timeLeft / 30f;
            int dustID = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustType<Dusts.IgniDust>(), 0f, 0f, 100, default, scale);
            Main.dust[dustID].noGravity = true;
            Main.dust[dustID].velocity *= 1.5f - Projectile.timeLeft / 30f / 2f;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!target.buffImmune[BuffID.OnFire])
            {
                target.AddBuff(BuffID.OnFire, 60 * 10, false);
                target.AddBuff(BuffType<Buffs.IgniDebuff>(), 60 * 10, false);
            }
        }
    }
}
