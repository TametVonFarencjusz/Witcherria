using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Witcherria.Players;

namespace Witcherria.Items
{
    [AutoloadEquip(EquipType.Neck)]
    public class WolfMedallion : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 38;
            Item.value = Item.buyPrice(0, 10, 0, 0);
            Item.rare = ItemRarityID.Blue;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<SignPlayer>().wolfMedallion = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.SilverBar, 5)
            .AddIngredient(ItemID.MeteoriteBar, 5)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}