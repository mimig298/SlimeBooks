using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace SlimeBooks.Projectiles
{
    public class BrownSlimeyShower : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.CultistIsResistantTo[Type] = true; // This applies to every projectile with homing
        }

        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.alpha = 255;
            Projectile.penetrate = 2;
            Projectile.extraUpdates = 2;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Magic;
        }

        public override void OnSpawn(IEntitySource source)
        {
            // We'll use ai[1] to store the ID of the target
            // A value of -1 will indicate that the projectile doesn't currently have a target, so we start like that.
            Projectile.ai[1] = -1f;
        }

        public override void AI()
        {
            // Adaptation of aiStyle 12 + homing projectile code

            Projectile.scale -= 0.005f;
            if (Projectile.scale <= 0)
                Projectile.Kill();

            if (Projectile.ai[0] > 3f)
            {
                // Spawn particles
                for (int i = 0; i < 3; i++)
                {
                    Vector2 dustOffset = Projectile.velocity / 3f * i;
                    Dust dust = Dust.NewDustDirect(Projectile.Center - new Vector2(-2, -2), 4, 4, DustID.t_Slime, 0f, 0f, 175, new Color(166, 110, 77, 100), 1.2f);
                    dust.noGravity = true;
                    dust.velocity *= 0.3f;
                    dust.velocity += Projectile.velocity * 0.5f;
                    dust.position -= dustOffset;
                }
                if (Main.rand.NextBool(8))
                {
                    Dust dust = Dust.NewDustDirect(Projectile.Center - new Vector2(-6, -6), 12, 12, DustID.t_Slime, 0f, 0f, 175, new Color(166, 110, 77, 100), 0.75f);
                    dust.velocity *= 0.5f;
                    dust.velocity += Projectile.velocity * 0.5f;
                }
            }

            if (Projectile.ai[0] > 10f) // Only start homing after a little bit
            {
                // Our target might have become invalid since the last frame, so check if that's the case so that we can find a new one
                if (Projectile.ai[1] != -1f && !Main.npc[(int)Projectile.ai[1]].CanBeChasedBy())
                    Projectile.ai[1] = -1f;

                // Find a new target if we don't have one
                if (Projectile.ai[1] == -1f)
                    Projectile.ai[1] = FindTarget(400f); // Each tile is 16 pixels, so 400 pixels is 25 tiles. Change this to whatever range you prefer

                // It's possible for us to still have no target, so only home in if we have one
                if (Projectile.ai[1] != -1f)
                {
                    NPC target = Main.npc[(int)Projectile.ai[1]];
                    float speed = Projectile.velocity.Length();
                    Projectile.velocity += (target.Center - Projectile.Center).SafeNormalize(Vector2.UnitX) * 2f; // 2 is the acceleration value
                    Projectile.velocity = Projectile.velocity.SafeNormalize(Vector2.UnitX) * speed; // Always go at the same speed
                }
            }

            Projectile.ai[0]++; // Timer
        }

        public int FindTarget(float maxDistance)
        {
            float rangeSquared = maxDistance * maxDistance; // Using a squared distance is faster than using a square root in distance calculations
            NPC closestNPC = null;

            foreach (NPC npc in Main.ActiveNPCs)
            {
                // Check if this NPC can be chased and can be hit
                if (npc.CanBeChasedBy() && Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height))
                {
                    // Additionally, the NPC should also be within the attack range
                    float sqrDistToNPC = Vector2.DistanceSquared(npc.Center, Projectile.Center);
                    if (sqrDistToNPC <= rangeSquared)
                    {
                        closestNPC = npc; // This NPC is now the closest one we found so far
                        rangeSquared = sqrDistToNPC; // Lower our range to the distance to the closest NPC so that we don't pick one that's further away later on
                    }
                }
            }

            // Return the index of the new target, or -1 if we didn't find a valid target
            if (closestNPC != null)
                return closestNPC.whoAmI;
            else return -1;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Slimed, 300);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.Slimed, 600);
        }
    }
}
