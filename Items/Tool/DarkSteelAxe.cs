using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Witcherria.Items.Tool
{
    public class DarkSteelAxe : ModItem
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
            Item.useAnimation = 18;
            Item.useTime = 18;
            Item.useTurn = true;
            Item.scale = 1f;
            Item.autoReuse = true;
            Item.damage = 18;
            Item.knockBack = 5.5f;
            Item.axe = 11;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient<Items.Material.DarksteelScrap>(4)
            .AddRecipeGroup(RecipeGroupID.Wood, 10)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}