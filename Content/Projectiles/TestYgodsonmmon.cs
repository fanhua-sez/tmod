using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using testsword.Content.Buffs;
using Microsoft.Xna.Framework;

namespace testsword.Content.Projectiles
{
    public class TestYgodsonmmon : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 34;
            Projectile.height = 34;
            Projectile.friendly = true;
            Projectile.minion = true;
            Projectile.minionSlots = 1f;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 18000;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Summon;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            int summonCap = player.maxMinions;

            if (!player.HasBuff(ModContent.BuffType<TestsummonBuff>()))
            {
                Projectile.Kill(); // 玩家没buff召回
                return;
            }

            // 跟随玩家
            Projectile.Center = Vector2.Lerp(Projectile.Center, player.Center + new Vector2(0, -50), 0.1f);

            // 大小、AI模式、伤害根据召唤上限动态变化
            if (summonCap <= 3)
            {
                MeleeCharge(player, summonCap);
            }
            else if (summonCap <= 5)
            {
                RangedAttack(player, summonCap);
            }
            else
            {
                Projectile.scale = 1.5f; // 明显变大
                MeleeCharge(player, summonCap);
                RangedAttack(player, summonCap);
            }
        }


        private void MeleeCharge(Player player, int summonCap)
        {
            NPC target = FindTarget();
            if (target != null)
            {
                Vector2 direction = (target.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * 10f;
                Projectile.velocity = direction;
                Projectile.damage = (int)(summonCap * 20 * player.GetDamage(DamageClass.Summon).ApplyTo(1f));
            }
        }

        private void RangedAttack(Player player, int summonCap)
        {
            NPC target = FindTarget();
            if (target != null && Projectile.ai[0]++ > 60) // 每60帧发射一次
            {
                Vector2 shootDir = (target.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * 12f;
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, shootDir,
                    ModContent.ProjectileType<TestYgodsonmmon>(),
                    (int)(summonCap * 15 * player.GetDamage(DamageClass.Summon).ApplyTo(1f)),
                    0, player.whoAmI);
                Projectile.ai[0] = 0;
            }
        }

        private NPC FindTarget()
        {
            NPC closest = null;
            float distance = 600f;
            foreach (NPC npc in Main.npc)
            {
                if (npc.CanBeChasedBy() && Vector2.Distance(Projectile.Center, npc.Center) < distance)
                {
                    closest = npc;
                    distance = Vector2.Distance(Projectile.Center, npc.Center);
                }
            }
            return closest;
        }
    }
}
