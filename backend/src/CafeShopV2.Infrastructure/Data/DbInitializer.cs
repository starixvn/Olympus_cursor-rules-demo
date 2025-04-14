using CafeShopV2.Core.Entities;
using CafeShopV2.Core.Enums;

namespace CafeShopV2.Infrastructure.Data;

public static class DbInitializer
{
    public static void Initialize(ApplicationDbContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        // Ensure database is created
        context.Database.EnsureCreated();

        // Check if there's already data
        if (context.Menus.Any())
        {
            return; // DB has been seeded
        }

        var menus = new Menu[]
        {
            // Drinks
            new()
            {
                Name = "Espresso",
                Description = "Strong Italian coffee brewed by forcing hot water through finely ground coffee beans",
                Category = MenuCategory.Drinks,
                Price = 3.99m,
                ImageUrl = "https://cdn.tgdd.vn/Files/2020/04/08/1247674/ca-phe-espresso-cappuccino-hay-macchiato-khac-nhau-nhu-the-nao-202004081936305660.jpg",
                IsAvailable = true
            },
            new()
            {
                Name = "Cappuccino",
                Description = "Espresso topped with foamy milk and a sprinkle of cocoa powder",
                Category = MenuCategory.Drinks,
                Price = 4.99m,
                ImageUrl = "https://cdn.tgdd.vn/Files/2020/04/08/1247674/ca-phe-espresso-cappuccino-hay-macchiato-khac-nhau-nhu-the-nao-202004081936305660.jpg",
                IsAvailable = true
            },
            new()
            {
                Name = "Green Tea Latte",
                Description = "Matcha green tea powder mixed with steamed milk",
                Category = MenuCategory.Drinks,
                Price = 4.49m,
                ImageUrl = "https://cdn.tgdd.vn/Files/2020/04/08/1247674/ca-phe-espresso-cappuccino-hay-macchiato-khac-nhau-nhu-the-nao-202004081936305660.jpg",
                IsAvailable = true
            },

            // Food
            new()
            {
                Name = "Chocolate Croissant",
                Description = "Buttery croissant filled with rich chocolate",
                Category = MenuCategory.Food,
                Price = 3.99m,
                ImageUrl = "https://cdn.tgdd.vn/Files/2020/04/08/1247674/ca-phe-espresso-cappuccino-hay-macchiato-khac-nhau-nhu-the-nao-202004081936305660.jpg",
                IsAvailable = true
            },
            new()
            {
                Name = "Avocado Toast",
                Description = "Sourdough toast topped with mashed avocado, sea salt, and red pepper flakes",
                Category = MenuCategory.Food,
                Price = 8.99m,
                ImageUrl = "https://cdn.tgdd.vn/Files/2020/04/08/1247674/ca-phe-espresso-cappuccino-hay-macchiato-khac-nhau-nhu-the-nao-202004081936305660.jpg",
                IsAvailable = true
            },
            new()
            {
                Name = "Blueberry Muffin",
                Description = "Fresh-baked muffin loaded with juicy blueberries",
                Category = MenuCategory.Food,
                Price = 3.49m,
                ImageUrl = "https://cdn.tgdd.vn/Files/2020/04/08/1247674/ca-phe-espresso-cappuccino-hay-macchiato-khac-nhau-nhu-the-nao-202004081936305660.jpg",
                IsAvailable = true
            }
        };

        context.Menus.AddRange(menus);
        context.SaveChanges();
    }
} 