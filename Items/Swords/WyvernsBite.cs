using Terraria;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;

namespace Witcherria.Items.Swords
{
    public class WyvernsBite : WitcherSword
    {
        public override void SetStaticDefaults()
        {
            WitcherriaGlobalItem.isSilver[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = Item.sellPrice(0, 0, 2, 10);
            Item.rare = ItemRarityID.White;
            Item.damage = 17;
            Item.DamageType = DamageClass.Melee;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 3.5f;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.SilverBulletSparkle,
                new ParticleOrchestraSettings { PositionInWorld = Main.rand.NextVector2FromRectangle(target.Hitbox) },
                player.whoAmI);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.TungstenBar, 7)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}