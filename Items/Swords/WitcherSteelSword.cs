using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Witcherria.Items.Swords
{
    public class WitcherSteelSword : WitcherSword
    {
        public override void SetStaticDefaults()
        {
            WitcherriaGlobalItem.isSilver[Type] = false;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = Item.sellPrice(0, 0, 30, 0);
            Item.rare = ItemRarityID.Blue;
            Item.damage = 62;
            Item.DamageType = DamageClass.Melee;
            Item.useTime = 28;
            Item.useAnimation = 28;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 9;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.IronBar, 14)
            .AddIngredient(ItemID.SoulofNight, 4)
            .AddTile(TileID.Anvils)
            .Register();

            CreateRecipe()
            .AddIngredient(ItemID.LeadBar, 14)
            .AddIngredient(ItemID.SoulofNight, 4)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}