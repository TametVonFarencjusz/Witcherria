using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Witcherria.Items.Arrow
{
    public class BonePilePlinker : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 13;
            Item.width = 50;
            Item.height = 20;
            Item.autoReuse = false;
            Item.knockBack = 0.5f;
            Item.value = Item.sellPrice(0, 1, 16, 0); ;
            Item.rare = ItemRarityID.Purple;
            Item.noMelee = true;
            Item.shoot = ProjectileID.WoodenArrowFriendly;
            Item.shootSpeed = 9f;
            Item.useAmmo = AmmoID.Arrow;
            Item.DamageType = DamageClass.Ranged;
            Item.useTime = 66;
            Item.useAnimation = 66;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = new SoundStyle($"{nameof(Witcherria)}/Sounds/CrossbowPlinker")
            {
                Volume = 1.1f,
            };
            Item.scale = 1f;
            Item.scale = 1f;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            //SoundEngine.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/DartShot").WithVolume(.3f).WithPitchVariance(.4f));
            Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(5));
            int num1 = Projectile.NewProjectile(source, position, new Vector2(perturbedSpeed.X, perturbedSpeed.Y), type, damage, knockback, player.whoAmI, 0, 1);
            Main.projectile[num1].noDropItem = true;
            perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(5));
            num1 = Projectile.NewProjectile(source, position, new Vector2(perturbedSpeed.X, perturbedSpeed.Y), type, damage, knockback, player.whoAmI, 0, 1);
            Main.projectile[num1].noDropItem = true;
            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.Bone, 15)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}