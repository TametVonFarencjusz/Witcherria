using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Witcherria.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class KaerMorhenLegs : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 0, 50, 0);
            Item.rare = ItemRarityID.Blue;
            Item.defense = 8;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient<Items.Material.DarksteelScrap>(8)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}