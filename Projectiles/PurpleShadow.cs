using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SlimeBooks.Projectiles
{
    public class PurpleShadow : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.alpha = 255;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Magic;
        }

        public override bool? CanDamage()
        {
            return Projectile.ai[0] > 5;
        }

        public override void AI()
        {
            Projectile.ai[0]++;
            Projectile.velocity += Vector2.Normalize(Projectile.velocity) * 0.1f;

            for (int i = 0; i < 3; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Corruption);
                dust.noGravity = true;
                dust.velocity *= 0.25f;
            }
        }
    }
}
