using CafeShop.Core.Entities;

namespace CafeShop.Infrastructure.Data;

public static class DbInitializer
{
    public static void Initialize(CafeShopDbContext context)
    {
        // Ensure database is created
        context.Database.EnsureCreated();

        // Check if there's already data
        if (context.MenuItems.Any())
        {
            return; // DB has been seeded
        }

        var menuItems = new MenuItem[]
        {
            new MenuItem
            {
                Name = "Cà phê sữa đá",
                Description = "Cà phê đậm đà với sữa đặc",
                Price = 25000,
                Category = MenuCategory.Drinks,
                ImageUrl = "https://caphe4.flatsome.vn/wp-content/uploads/2023/11/CA-PHE-SUA-DA-SAI-GON.png",
                IsAvailable = true
            },
            new MenuItem
            {
                Name = "Bánh mì thịt",
                Description = "Bánh mì với nhân thịt, rau củ",
                Price = 30000,
                Category = MenuCategory.Food,
                ImageUrl = "https://cdn.xanhsm.com/2025/01/125f9835-banh-mi-sai-gon-thumb.jpg",
                IsAvailable = true
            },
            new MenuItem
            {
                Name = "Trà đào",
                Description = "Trà tươi với đào tươi",
                Price = 35000,
                Category = MenuCategory.Drinks,
                ImageUrl = "https://horecavn.com/wp-content/uploads/2024/05/huong-dan-cong-thuc-tra-dao-cam-sa-hut-khach-ngon-kho-cuong_20240526180626.jpg",
                IsAvailable = true
            }
        };

        context.MenuItems.AddRange(menuItems);
        context.SaveChanges();
    }
} 