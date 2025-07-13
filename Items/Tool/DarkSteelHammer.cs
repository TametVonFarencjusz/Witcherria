using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Witcherria.Items.Tool
{
    public class DarkSteelHammer: ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = Item.sellPrice(0, 0, 44, 0);
            Item.rare = ItemRarityID.Green;
            Item.DamageType = DamageClass.Melee;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.useAnimation = 13;
            Item.useTime = 13;
            Item.useTurn = true;
            Item.scale = 1f;
            Item.autoReuse = true;
            Item.damage = 12;
            Item.knockBack = 6;
            Item.hammer = 55;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient<Items.Material.DarksteelScrap>(6)
            .AddRecipeGroup(RecipeGroupID.Wood, 6)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}