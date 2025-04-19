using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SlimeBooks.Items
{
    public class RedSlimeShower : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 38;

            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 22;
            Item.useAnimation = 22;

            Item.DamageType = DamageClass.Magic;
            Item.damage = 23;
            Item.knockBack = 2.3f;
            Item.noMelee = true;
            Item.autoReuse = true;

            Item.value = Item.sellPrice(gold: 1, silver: 20);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item13;

            Item.shoot = ModContent.ProjectileType<Projectiles.RedSlimeShower>();
            Item.shootSpeed = 10f;
            Item.mana = 7;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<SlimeShower>()
                .AddIngredient(ItemID.CrimtaneBar, 8)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
