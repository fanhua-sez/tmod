using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using testsword.Content.Projectiles;

namespace testsword.Content.Items
{
    public class testgun:ModItem
    {

        public override void SetDefaults() {
            Item.damage = 200;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 20;
            Item.height = 20;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 6f;
            Item.value = Item.buyPrice(silver: 1);
            Item.rare = ItemRarityID.Master;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.noUseGraphic = false;
            Item.shoot = ModContent.ProjectileType<Projectiles.testAnmo>();
            Item.shootSpeed = 60;
            Item.crit = 96;



        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            //自瞄
            NPC target = null;
            float distanceMax = 1000f;
            foreach (var npc in Main.npc)
            {
                if (npc.active && !npc.friendly && !npc.dontTakeDamage)
                {
                    float distance = Vector2.Distance(player.Center, npc.Center);
                    if (distance < distanceMax)
                    {
                        distanceMax = distance;
                        target = npc;
                    }
                }
            }

            if (target != null)
            {
                Vector2 direction = target.Center - position;
                direction.Normalize();
                direction *= Item.shootSpeed; // 用枪的射速

                Projectile.NewProjectile(source, position, direction, type, damage, knockback, player.whoAmI);
            }




            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return !Main.rand.NextBool(50, 100);//百分之五十几率不消耗
        }


        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.DirtBlock, 10);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
