using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Witcherria.Items.Arrow
{
    public class FelineCrossbow : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 20;
            Item.width = 38;
            Item.height = 26;
            Item.autoReuse = false;
            Item.knockBack = 1.2f;
            Item.value = Item.sellPrice(0, 0, 23, 0); ;
            Item.rare = ItemRarityID.Blue;
            Item.noMelee = true;
            Item.shoot = ProjectileID.WoodenArrowFriendly;
            Item.shootSpeed = 7.4f;
            Item.useAmmo = AmmoID.Arrow;
            Item.DamageType = DamageClass.Ranged;
            Item.useTime = 99;
            Item.useAnimation = 99;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = new SoundStyle($"{nameof(Witcherria)}/Sounds/CrossbowPoor")
            {
                Volume = 1f,
            };
            Item.scale = 1f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient<Items.Material.DarksteelScrap>(5)
            .AddRecipeGroup(RecipeGroupID.Wood, 12)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}