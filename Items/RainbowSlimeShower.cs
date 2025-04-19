using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SlimeBooks.Items
{
    public class RainbowSlimeShower : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 40;

            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 19;
            Item.useAnimation = 19;

            Item.DamageType = DamageClass.Magic;
            Item.damage = 55;
            Item.knockBack = 2.3f;
            Item.noMelee = true;
            Item.autoReuse = true;

            Item.value = Item.sellPrice(gold: 2);
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = SoundID.Item13;

            Item.shoot = ModContent.ProjectileType<Projectiles.RainbowSlimeShower>();
            Item.shootSpeed = 10f;
            Item.mana = 10;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<SlimeShower>()
                .AddIngredient(ItemID.RainbowBrick, 30)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
