using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Witcherria.Projectiles
{
    public class Axii : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 128;
            Projectile.height = 128;
            Projectile.aiStyle = -1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.timeLeft = 30;
            Projectile.ignoreWater = true;
            Projectile.friendly = true;
            Projectile.extraUpdates = 1;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            float scale = 2f - Projectile.timeLeft / 30f;
            Vector2 vector = new Vector2((30f - Projectile.timeLeft) / 30f * Projectile.width * 0.5f, 0);


            int numberDust = 30;
            for (int i = 0; i < numberDust; i++)
            {
                float rotation = MathHelper.ToRadians(360);
                Vector2 place = vector.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberDust - 1f))); // Item defines the projectile roatation

                int dustID = Dust.NewDust(Projectile.Center + place, 2, 2, DustType<Dusts.AxiiDust>(), 0f, 0f, 100, default, scale);
                Main.dust[dustID].noGravity = true;
                Main.dust[dustID].velocity *= 1.5f - Projectile.timeLeft / 30f / 2f;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Confused, 60 * 10, false);
            if (!target.buffImmune[BuffID.Confused])
            {
                //target.AddBuff(BuffType<Buffs.IgniDebuff>(), 60 * 10, false);
            }
        }
    }
}
