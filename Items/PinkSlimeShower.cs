using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SlimeBooks.Items
{
    public class PinkSlimeShower : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 40;

            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 19;
            Item.useAnimation = 19;

            Item.DamageType = DamageClass.Magic;
            Item.damage = 34;
            Item.knockBack = 1.4f;
            Item.noMelee = true;
            Item.autoReuse = true;

            Item.value = Item.sellPrice(silver: 90);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item13;

            Item.shoot = ModContent.ProjectileType<Projectiles.PinkSlimeShower>();
            Item.shootSpeed = 9f;
            Item.mana = 7;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<SlimeShower>()
                .AddIngredient(ItemID.PinkGel, 20)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
