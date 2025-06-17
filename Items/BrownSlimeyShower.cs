using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SlimeBooks.Items
{
    public class BrownSlimeyShower : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 40;

            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 17;
            Item.useAnimation = 17;

            Item.DamageType = DamageClass.Magic;
            Item.damage = 75;
            Item.knockBack = 1.7f;
            Item.noMelee = true;
            Item.autoReuse = true;

            Item.value = Item.sellPrice(silver: 50);
            Item.rare = ItemRarityID.Lime;
            Item.UseSound = SoundID.Item13;

            Item.shoot = ModContent.ProjectileType<Projectiles.BrownSlimeyShower>();
            Item.shootSpeed = 10f;
            Item.mana = 5;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.DirtBlock, 15)
                .AddIngredient<SlimeShower>()
                .AddIngredient(ItemID.PossessedHatchet)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
