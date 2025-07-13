using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Witcherria.Items.Swords
{
    public class Iris : WitcherSword
    {
        public override void SetStaticDefaults()
        {
            WitcherriaGlobalItem.isSilver[Type] = false;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = Item.sellPrice(0, 0, 66, 33);
            Item.rare = ItemRarityID.White;
            Item.damage = 44;
            Item.DamageType = DamageClass.Melee;
            Item.useTime = 55;
            Item.useAnimation = 55;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 10;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.GoldBar, 18)
            .AddTile(TileID.Anvils)
            .Register();

            CreateRecipe()
            .AddIngredient(ItemID.PlatinumBar, 18)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}