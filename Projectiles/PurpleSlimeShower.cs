using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;

namespace SlimeBooks.Projectiles
{
    public class PurpleSlimeShower : ModProjectile
    {
        public override string Texture => "SlimeBooks/Projectiles/SlimeShower";

        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.alpha = 255;
            Projectile.extraUpdates = 2;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Magic;
        }

        public override void AI()
        {
            // Adaptation of aiStyle 12

            Projectile.scale -= 0.005f;
            if (Projectile.scale <= 0)
                Projectile.Kill();

            if (Projectile.ai[0] > 3f)
            {
                Projectile.velocity.Y += 0.11f;
                for (int i = 0; i < 3; i++)
                {
                    Vector2 dustOffset = Projectile.velocity / 3f * i;
                    Dust dust = Dust.NewDustDirect(Projectile.Center - new Vector2(-2, -2), 4, 4, DustID.t_Slime, 0f, 0f, 175, new Color(100, 70, 190, 170), 1.3f);
                    dust.noGravity = true;
                    dust.velocity *= 0.3f;
                    dust.velocity += Projectile.velocity * 0.5f;
                    dust.position -= dustOffset;
                }

                if (Main.rand.NextBool(8))
                {
                    Dust dust = Dust.NewDustDirect(Projectile.Center - new Vector2(-6, -6), 12, 12, DustID.t_Slime, 0f, 0f, 175, new Color(100, 70, 190, 100), 0.8f);
                    dust.velocity *= 0.5f;
                    dust.velocity += Projectile.velocity * 0.5f;
                }
            }
            else
                Projectile.ai[0]++;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Slimed, 300);
            SoundEngine.PlaySound(SoundID.Item8, Projectile.position);
            if (Projectile.owner == Main.myPlayer && damageDone > 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    Vector2 newVelocity = new(3, 0);
                    int newDamage = Projectile.damage / 2;
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, newVelocity.RotatedBy(MathHelper.PiOver2 * i), ModContent.ProjectileType<PurpleShadow>(), newDamage, 1f, Projectile.owner);
                }
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.Slimed, 600);
        }
    }
}
