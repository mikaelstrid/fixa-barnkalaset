using System.Linq;
using Pixel.FixaBarnkalaset.Core;

namespace Pixel.FixaBarnkalaset.Infrastructure.Persistence.EntityFramework
{
    // https://github.com/rowanmiller/UnicornStore/blob/master/UnicornStore/src/UnicornStore/Models/UnicornStore/UnicornStoreExtensions.cs
    public static class KidsPartiesContextExtensions
    {
        public static void EnsureSeedData(this KidsPartiesContext context)
        {
            if (context.AllMigrationsApplied())
            {
                if (!context.Cities.Any())
                {
                    context.Cities.AddRange(
                        new City { Slug = "halmstad", Name = "Halmstad", Latitude = 56.6706614, Longitude = 12.7579992 },
                        new City { Slug = "goteborg", Name = "Göteborg", Latitude = 57.7018796, Longitude = 11.7536028 },
                        new City { Slug = "malmo", Name = "Malmö", Latitude = 55.5708212, Longitude = 12.9500712 },
                        new City { Slug = "vaxjo", Name = "Växjö", Latitude = 56.8894262, Longitude = 14.7241027 }
                    );
                    context.SaveChanges();
                }

                //if (!context.Products.Any())
                //{
                //    var clothing = context.Categories.Add(new Category { DisplayName = "Clothing" }).Entity;
                //    var mensClothing = context.Categories.Add(new Category { DisplayName = "Mens Clothing", ParentCategory = clothing }).Entity;
                //    var mensShirts = context.Categories.Add(new Category { DisplayName = "Mens Shirts", ParentCategory = mensClothing }).Entity;

                //    var homeAndGarden = context.Categories.Add(new Category { DisplayName = "Home & Garden" }).Entity;
                //    var kitchenAndDining = context.Categories.Add(new Category { DisplayName = "Kitchen & Dining", ParentCategory = homeAndGarden }).Entity;

                //    context.Products.AddRange(
                //        new Product { SKU = "MUG001", DisplayName = "Unicorn Coffee Mug (Blue)", MSRP = 12.95M, CurrentPrice = 12.95M, Category = kitchenAndDining, ImageUrl = "/images/products/CoffeeMug_Blue.png", Description = "Coffee and unicorns... what else could you need! Our flagship unicorn printed on a high quality coffee mug." },
                //        new Product { SKU = "MUG002", DisplayName = "Unicorn Coffee Mug (Green)", MSRP = 12.95M, CurrentPrice = 10.95M, Category = kitchenAndDining, ImageUrl = "/images/products/CoffeeMug_Green.png", Description = "Coffee and unicorns... what else could you need! Our flagship unicorn printed on a high quality coffee mug." },
                //        new Product { SKU = "MUG003", DisplayName = "Unicorn Coffee Mug (Pink)", MSRP = 12.95M, CurrentPrice = 12.95M, Category = kitchenAndDining, ImageUrl = "/images/products/CoffeeMug_Pink.png", Description = "Coffee and unicorns... what else could you need! Our flagship unicorn printed on a high quality coffee mug." },
                //        new Product { SKU = "TEE201", DisplayName = "Unicorn Coffee Mug (White)", MSRP = 12.95M, CurrentPrice = 12.95M, Category = kitchenAndDining, ImageUrl = "/images/products/CoffeeMug_White.png", Description = "Coffee and unicorns... what else could you need! Our flagship unicorn printed on a high quality coffee mug." },
                //        new Product { SKU = "TEE202", DisplayName = "Mens Unicorn Tee (Blue)", MSRP = 19.95M, CurrentPrice = 19.95M, Category = mensShirts, ImageUrl = "/images/products/MensTee_Blue.png", Description = "Share your love of unicorns with the world. Quality cotton t-shirt with a long lasting print." },
                //        new Product { SKU = "TEE203", DisplayName = "Mens Unicorn Tee (Grey)", MSRP = 19.95M, CurrentPrice = 17.95M, Category = mensShirts, ImageUrl = "/images/products/MensTee_Grey.png", Description = "Share your love of unicorns with the world. Quality cotton t-shirt with a long lasting print." },
                //        new Product { SKU = "TEE204", DisplayName = "Mens Unicorn Tee (Red/Black Stripe)", MSRP = 24.95M, CurrentPrice = 19.95M, Category = mensShirts, ImageUrl = "/images/products/MensTee_RedBlackStripe.png", Description = "Share your love of unicorns with the world. Quality cotton t-shirt with a long lasting print." });

                //    context.SaveChanges();
                //}

                //if (!context.WebsiteAds.Any())
                //{
                //    var kitchenAndDining = context.Categories.Single(c => c.DisplayName == "Kitchen & Dining");
                //    var clothing = context.Categories.Single(c => c.DisplayName == "Clothing");

                //    context.WebsiteAds.Add(new WebsiteAd { ImageUrl = "/images/banners/Unicorn.png", TagLine = "Welcome to Unicorn Store", Details = "A series of applications showing EF7 in action", Url = "http://unicornstore.net" });
                //    context.WebsiteAds.Add(new WebsiteAd { ImageUrl = "/images/banners/GitHub.png", TagLine = "Source code available on GitHub", Details = "github.com/rowanmiller/UnicornStore", Url = "http://github.com/rowanmiller/UnicornStore" });
                //    context.WebsiteAds.Add(new WebsiteAd { ImageUrl = "/images/banners/UnicornClicker.png", TagLine = "Possibly the lamest game of the year", Details = "Available in the Windows 10 app store", Url = "https://www.microsoft.com/en-us/store/apps/unicorn-clicker/9nblggh5jzrk" });
                //    context.SaveChanges();
                //}
            }
        }
    }
}
