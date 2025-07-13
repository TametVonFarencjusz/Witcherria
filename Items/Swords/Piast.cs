using Terraria;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;

namespace Witcherria.Items.Swords
{
    public class Piast : WitcherSword
    {
        public override void SetStaticDefaults()
        {
            WitcherriaGlobalItem.isSilver[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = Item.sellPrice(0, 7, 30, 0);
            Item.rare = ItemRarityID.Purple;
            Item.damage = 89;
            Item.DamageType = DamageClass.Melee;
            Item.useTime = 17;
            Item.useAnimation = 17;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 14;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.Excalibur,
                new ParticleOrchestraSettings { PositionInWorld = Main.rand.NextVector2FromRectangle(target.Hitbox) },
                player.whoAmI);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.HallowedBar, 11)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}