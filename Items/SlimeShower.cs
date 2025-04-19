using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SlimeBooks.Items
{
    public class SlimeShower : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 40;

            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 19;
            Item.useAnimation = 19;

            Item.DamageType = DamageClass.Magic;
            Item.damage = 15;
            Item.knockBack = 1.7f;
            Item.noMelee = true;
            Item.autoReuse = true;

            Item.value = Item.sellPrice(silver: 50);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item13;

            Item.shoot = ModContent.ProjectileType<Projectiles.SlimeShower>();
            Item.shootSpeed = 9f;
            Item.mana = 5;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Gel, 30)
                .AddIngredient(ItemID.Book)
                .AddTile(TileID.Solidifier)
                .Register();
        }
    }
}
