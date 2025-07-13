using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Witcherria.Players;
using static Terraria.ModLoader.ModContent;

namespace Witcherria.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class KaerMorhenEyeshadow : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 0, 60, 0);
            Item.rare = ItemRarityID.Blue;
            Item.defense = 5;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<KaerMorhenArmor>() && legs.type == ItemType<KaerMorhenLegs>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Increases power of Witcher Signs";

            player.GetModPlayer<SignPlayer>().bonusMultIgniDamage += 0.5f;
            player.GetModPlayer<SignPlayer>().bonusMultIgniSpeed += 0.25f;

            player.GetModPlayer<SignPlayer>().bonusMultAxiiDamage += 0.25f;

            player.GetModPlayer<SignPlayer>().bonusFlatAardDamage += 10;
            player.GetModPlayer<SignPlayer>().bonusMultAardSpeed += 0.25f;

            player.GetModPlayer<SignPlayer>().bonusFlatQuenTime += 10;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient<Items.Material.DarksteelScrap>(5)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}