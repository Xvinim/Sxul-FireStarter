using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;

namespace Sxul.Content.Items.BossLoot
{
    public class CircleEffect : ModProjectile
    {
        private const int EffectDuration = 600; // 10 seconds (60 ticks per second)
        private const float Radius = 100f; // Radius of the effect
        private const int DamageAmount = 1000; // Damage dealt to non-boss NPCs

        private Player player;

        public override void SetDefaults()
        {
            Projectile.width = 200; // Adjust size as needed
            Projectile.height = 200;
            Projectile.friendly = true; // Not a friendly projectile
            Projectile.hostile = false; // Not a hostile projectile
            Projectile.penetrate = -1;
            Projectile.timeLeft = EffectDuration;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false; // No collision with tiles
            Projectile.alpha = 0; // Fully visible
        }

        public override void AI()
        {
            // Find the player and update the projectile's position
            player = Main.player[Projectile.owner];
            Projectile.position = player.Center - new Vector2(Projectile.width / 2, Projectile.height / 2);

            // Create green dust around the player in a circle
            CreateDustCircle();

            // Apply damage to enemies within the radius
            foreach (NPC npc in Main.npc)
            {
                if (npc.active && !npc.boss && Vector2.Distance(Projectile.Center, npc.Center) < Radius)
                {
                    // Create HitInfo for the damage application
                    NPC.HitInfo hitInfo = new NPC.HitInfo
                    {
                        Damage = DamageAmount,
                        DamageType = DamageClass.Melee,
                        Knockback = 0,
                        Crit = false,
                        HideCombatText = false,
                        InstantKill = false,
                        HitDirection = 0
                    };

                    npc.StrikeNPC(hitInfo, false, false);
                }
            }
        }

        private void CreateDustCircle()
        {
            int dustCount = 30; // Number of dust particles
            float angleStep = MathHelper.TwoPi / dustCount;
            Color dustColor = Color.Green; // Green color for the dust

            for (int i = 0; i < dustCount; i++)
            {
                float angle = angleStep * i;
                Vector2 dustPosition = Projectile.Center + new Vector2(Radius, 0).RotatedBy(angle);
                Dust dust = Dust.NewDustPerfect(dustPosition, DustID.GreenTorch, null, 100, dustColor, 1.5f);
                dust.noGravity = true;
                dust.velocity = Vector2.Zero;
            }
        }
    }
}
