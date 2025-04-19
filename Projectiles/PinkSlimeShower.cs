using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SlimeBooks.Projectiles
{
    public class PinkSlimeShower : ModProjectile
    {
        public override string Texture => "SlimeBooks/Projectiles/SlimeShower";

        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.alpha = 255;
            Projectile.penetrate = 5;
            Projectile.extraUpdates = 2;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = true;
        }

        public override void AI()
        {
            // Adaptation of aiStyle 12

            Projectile.scale -= 0.003f;
            if (Projectile.scale <= 0)
                Projectile.Kill();

            if (Projectile.ai[0] > 3f)
            {
                Projectile.velocity.Y += 0.09f;
                for (int i = 0; i < 3; i++)
                {
                    Vector2 dustOffset = Projectile.velocity / 3f * i;
                    Dust dust = Dust.NewDustDirect(Projectile.Center - new Vector2(-2, -2), 4, 4, DustID.t_Slime, 0f, 0f, 175, new Color(250, 100, 190, 100), 1.2f);
                    dust.noGravity = true;
                    dust.velocity *= 0.3f;
                    dust.velocity += Projectile.velocity * 0.5f;
                    dust.position -= dustOffset;
                }

                if (Main.rand.NextBool(8))
                {
                    Dust dust = Dust.NewDustDirect(Projectile.Center - new Vector2(-6, -6), 12, 12, DustID.t_Slime, 0f, 0f, 175, new Color(250, 100, 190, 100), 0.75f);
                    dust.velocity *= 0.5f;
                    dust.velocity += Projectile.velocity * 0.5f;
                }
            }
            else
                Projectile.ai[0]++;
        }

        private void Bounce(Vector2 oldVelocity)
        {
            Projectile.scale -= 0.07f;
            Projectile.penetrate--;
            if (Projectile.scale <= 0 || Projectile.penetrate <= 0)
                Projectile.Kill();
            else
            {
                if (Math.Abs(Projectile.velocity.X - oldVelocity.X) > float.Epsilon)
                    Projectile.velocity.X = oldVelocity.X * -0.7f;
                if (Math.Abs(Projectile.velocity.Y - oldVelocity.Y) > float.Epsilon)
                    Projectile.velocity.Y = oldVelocity.Y * -0.7f;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Bounce(oldVelocity);
            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Vector2 oldVelocity = Projectile.velocity;
            Projectile.velocity = Vector2.Zero;
            Bounce(oldVelocity);
            target.AddBuff(BuffID.Slimed, 300);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.Slimed, 600);
        }
    }
}
