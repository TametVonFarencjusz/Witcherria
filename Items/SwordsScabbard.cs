using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Witcherria.Players;

namespace Witcherria.Items
{
    public class SwordsScabbard : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 38;
            Item.value = Item.sellPrice(0, 0, 10, 0);
            Item.rare = ItemRarityID.White;
            Item.accessory = true;
        }

        public override void UpdateVisibleAccessory(Player player, bool hideVisual)
        {
            if (!hideVisual)
            {
                player.GetModPlayer<SignPlayer>().swordsScabbard = true;
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.Leather, 3)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}