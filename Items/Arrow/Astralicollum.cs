using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Witcherria.Items.Arrow
{
    public class Astralicollum : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 45;
            Item.width = 38;
            Item.height = 26;
            Item.autoReuse = false;
            Item.knockBack = 3f;
            Item.value = Item.sellPrice(0, 1, 23, 0); ;
            Item.rare = ItemRarityID.Blue;
            Item.noMelee = true;
            Item.shoot = ProjectileID.WoodenArrowFriendly;
            Item.shootSpeed = 22f;
            Item.useAmmo = AmmoID.Arrow;
            Item.DamageType = DamageClass.Ranged;
            Item.useTime = 120;
            Item.useAnimation = 120;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = new SoundStyle($"{nameof(Witcherria)}/Sounds/Astralico")
            {
                Volume = 1.4f,
            };
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.MeteoriteBar, 12)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}