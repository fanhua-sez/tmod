using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace testsword.Content.Projectiles
{
    public class testAnmo:ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height=16; 
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = 3;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.aiStyle = 1;
            //Projectile.damage = 20;
            Projectile.alpha = 0;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.alpha = 0;
            Projectile.extraUpdates = 1;
            AIType = ProjectileID.Bullet; // 使用传统子弹的ai
        }
    }
}
