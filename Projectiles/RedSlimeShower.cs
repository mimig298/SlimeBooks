using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SlimeBooks.Projectiles
{
    public class RedSlimeShower : ModProjectile
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
                    Dust dust = Dust.NewDustDirect(Projectile.Center - new Vector2(-2, -2), 4, 4, DustID.t_Slime, 0f, 0f, 175, new Color(200, 15, 15, 170), 1.5f);
                    dust.noGravity = true;
                    dust.velocity *= 0.3f;
                    dust.velocity += Projectile.velocity * 0.5f;
                    dust.position -= dustOffset;
                }

                if (Main.rand.NextBool(8))
                {
                    Dust dust = Dust.NewDustDirect(Projectile.Center - new Vector2(-6, -6), 12, 12, DustID.t_Slime, 0f, 0f, 175, new Color(200, 15, 15, 170), 0.9f);
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
            if (Projectile.owner == Main.myPlayer && !Main.LocalPlayer.moonLeech && damageDone > 0 && target.lifeMax > 5 && Projectile.friendly && !Projectile.hostile && !target.immortal)
            {
                Main.LocalPlayer.lifeSteal -= 2;
                Main.LocalPlayer.Heal(2);
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.Slimed, 600);
        }
    }
}
