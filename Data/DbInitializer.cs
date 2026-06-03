using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ZavansaCatalogMVC.Models;

namespace ZavansaCatalogMVC.Data;

public static class DbInitializer
{
    private const string PlaceholderImage = "/images/placeholders/product-placeholder.svg";

    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

        context.Database.EnsureCreated();

        SeedCategories(context);
        SeedCollections(context);
        SeedProducts(context);
        SeedSettings(context);
        SeedAdmin(context);
    }

    private static void SeedCategories(ApplicationDbContext context)
    {
        if (context.Categories.Any())
        {
            return;
        }

        context.Categories.AddRange(
            new Category { Name = "Avize", Description = "Modern yaşam alanları için dekoratif avize modelleri." },
            new Category { Name = "Aplik", Description = "Duvar yüzeylerine sıcak ve karakterli ışık katan aplikler." },
            new Category { Name = "Masa Lambası", Description = "Çalışma ve dinlenme alanları için işlevsel masa lambaları." },
            new Category { Name = "Abajur", Description = "Soft ışık etkisi sunan modern abajur tasarımları." },
            new Category { Name = "Çocuk Odası Aydınlatma", Description = "Çocuk odaları için renkli ve eğlenceli dekoratif ışıklar." });

        context.SaveChanges();
    }

    private static void SeedCollections(ApplicationDbContext context)
    {
        if (context.ProductCollections.Any())
        {
            return;
        }

        context.ProductCollections.AddRange(
            new ProductCollection { Name = "Beton Collection", Description = "Gerçek beton dokusunun doğallığını modern ve hafif aydınlatma tasarımlarıyla birleştiren koleksiyon.", DisplayOrder = 1, ImageUrl = PlaceholderImage },
            new ProductCollection { Name = "Ori Design Collection", Description = "Japon ve Kore tarzından esinlenen modern avize koleksiyonu.", DisplayOrder = 2, ImageUrl = PlaceholderImage },
            new ProductCollection { Name = "Monolit Collection", Description = "Geometrik formlardan ve özel tasarımlardan oluşan modern koleksiyon.", DisplayOrder = 3, ImageUrl = PlaceholderImage },
            new ProductCollection { Name = "Modern Twisted Collection", Description = "İddialı ve dikkat çekici formlara sahip özel avize koleksiyonu.", DisplayOrder = 4, ImageUrl = PlaceholderImage },
            new ProductCollection { Name = "Stone Glow", Description = "Taş formundan ve doğal dokulardan ilham alan aplik tasarımları.", DisplayOrder = 5, ImageUrl = PlaceholderImage },
            new ProductCollection { Name = "Masa Lambaları", Description = "Dekoratif ve işlevsel masa lambası modelleri.", DisplayOrder = 6, ImageUrl = PlaceholderImage },
            new ProductCollection { Name = "Arco Abajur", Description = "Zarif mermer sütunlardan esinlenen soft ve modern abajur koleksiyonu.", DisplayOrder = 7, ImageUrl = PlaceholderImage },
            new ProductCollection { Name = "Çocuk Odası Aydınlatma", Description = "Çocuk odaları için renkli ve eğlenceli dekoratif aydınlatmalar.", DisplayOrder = 8, ImageUrl = PlaceholderImage });

        context.SaveChanges();
    }

    private static void SeedProducts(ApplicationDbContext context)
    {
        if (context.Products.Any())
        {
            return;
        }

        var categories = context.Categories.AsNoTracking().ToDictionary(category => category.Name, category => category.Id);
        var collections = context.ProductCollections.AsNoTracking().ToDictionary(collection => collection.Name, collection => collection.Id);

        var products = new List<Product>
        {
            CreateProduct("LAVA Beton Avize", "Avize", "Beton Collection", categories, collections, "25cm", "20cm", "PLA", "E27 uyumlu ampül", "Beyaz, Siyah, Antrasit, Gri", true),
            CreateProduct("LUMEN Beton Avize", "Avize", "Beton Collection", categories, collections, "25cm", "26cm", "PLA", "E27 uyumlu ampül", "Beyaz, Siyah, Antrasit, Gri", true),
            CreateProduct("ORBIT Beton Avize", "Avize", "Beton Collection", categories, collections, "25cm", "23cm", "PLA", "E27 uyumlu ampül", "Beyaz, Siyah, Antrasit, Gri", true),
            CreateProduct("LAVA 2'li Beton Avize", "Avize", "Beton Collection", categories, collections, "22cm", "20cm", "PLA", "E27 uyumlu ampül", "Beyaz, Siyah, Antrasit, Gri"),
            CreateProduct("LAVA 3'lü Beton Avize", "Avize", "Beton Collection", categories, collections, "22cm", "20cm", "PLA", "E27 uyumlu ampül", "Beyaz, Siyah, Antrasit, Gri"),
            CreateProduct("LAVA 5'li Beton Avize", "Avize", "Beton Collection", categories, collections, "22cm", "20cm", "PLA", "E27 uyumlu ampül", "Beyaz, Siyah, Antrasit, Gri"),
            CreateProduct("LAVA 3'lü V2 Beton Avize", "Avize", "Beton Collection", categories, collections, "22cm", "20cm", "PLA", "E27 uyumlu ampül", "Beyaz, Siyah, Antrasit, Gri"),
            CreateProduct("LUMEN 3'lü Beton Avize", "Avize", "Beton Collection", categories, collections, "22cm", "23cm", "PLA", "E27 uyumlu ampül", "Beyaz, Siyah, Antrasit, Gri"),
            CreateProduct("HIKARI Avize", "Avize", "Ori Design Collection", categories, collections, "15cm", "20cm", "PETG", "E27 uyumlu ampül", "Beyaz"),
            CreateProduct("SATORI Avize", "Avize", "Ori Design Collection", categories, collections, "15cm", "20cm", "PETG", "E27 uyumlu ampül", "Beyaz"),
            CreateProduct("LINO Avize", "Avize", "Ori Design Collection", categories, collections, "15cm", "20cm", "PETG", "E27 uyumlu ampül", "Beyaz"),
            CreateProduct("Aniko Avize", "Avize", "Monolit Collection", categories, collections, "22cm", "15-20cm", "PETG", "E27 uyumlu ampül", "Beyaz, Bej, Turuncu"),
            CreateProduct("Oromi Avize", "Avize", "Monolit Collection", categories, collections, "22cm", "15-20cm", "PETG", "E27 uyumlu ampül", "Beyaz, Bej"),
            CreateProduct("Abysell Long Avize", "Avize", "Monolit Collection", categories, collections, "22cm", "23cm", "PETG", "E27 uyumlu ampül", "Beyaz"),
            CreateProduct("Abysell Avize", "Avize", "Monolit Collection", categories, collections, "22cm", "15-20cm", "PETG", "E27 uyumlu ampül", "Beyaz, Yeşil"),
            CreateProduct("Twisted v1 Avize", "Avize", "Modern Twisted Collection", categories, collections, "22cm", "15-20cm", "PETG", "E27 uyumlu ampül", "Beyaz, Bej, Turuncu", true),
            CreateProduct("Twisted v2 Avize", "Avize", "Modern Twisted Collection", categories, collections, "22cm", "15-20cm", "PETG", "E27 uyumlu ampül", "Beyaz, Bej, Turuncu"),
            CreateProduct("Vori v1 Avize", "Avize", "Modern Twisted Collection", categories, collections, "15-20cm", "15-20cm", "PETG", "E27 uyumlu ampül", "Beyaz, Bej, Turuncu"),
            CreateProduct("Vori v2 Avize", "Avize", "Modern Twisted Collection", categories, collections, "15-20cm", "15-20cm", "PETG", "E27 uyumlu ampül", "Beyaz, Bej, Turuncu"),
            CreateProduct("Stone Glow Aplik", "Aplik", "Stone Glow", categories, collections, "22cm", null, "PLA", "E27 uyumlu ampül", null),
            CreateProduct("Aqua Masa Lambası", "Masa Lambası", "Masa Lambaları", categories, collections, null, null, null, null, null, false, "Dalgaların ve ışığın estetik birleşimi."),
            CreateProduct("Void Masa Lambası", "Masa Lambası", "Masa Lambaları", categories, collections, null, null, null, null, null, false, "Tek bir ürün, birden çok işlev."),
            CreateProduct("Arco Abajur Beyaz", "Abajur", "Arco Abajur", categories, collections, "25cm", null, "PLA", "LED ışıklandırmalıdır", "Beyaz"),
            CreateProduct("Arco Abajur Kumlu Bej", "Abajur", "Arco Abajur", categories, collections, "25cm", null, "PLA", "LED ışıklandırmalıdır", "Kumlu Bej"),
            CreateProduct("Ufo Lamp", "Çocuk Odası Aydınlatma", "Çocuk Odası Aydınlatma", categories, collections, null, null, "PLA", "LED ile prize takılarak çalışır", "Farklı renkler yapılabilir"),
            CreateProduct("Roket Masa/Gece Lambası", "Çocuk Odası Aydınlatma", "Çocuk Odası Aydınlatma", categories, collections, null, null, "PLA", "LED ile prize takılarak çalışır", "Farklı renkler yapılabilir"),
            CreateProduct("Roket 2 Masa/Gece Lamp", "Çocuk Odası Aydınlatma", "Çocuk Odası Aydınlatma", categories, collections, null, null, "PLA", "LED ile prize takılarak çalışır", "Farklı renkler yapılabilir")
        };

        context.Products.AddRange(products);
        context.SaveChanges();
    }

    private static Product CreateProduct(
        string name,
        string categoryName,
        string collectionName,
        IReadOnlyDictionary<string, int> categories,
        IReadOnlyDictionary<string, int> collections,
        string? width,
        string? height,
        string? material,
        string? bulbType,
        string? colors,
        bool isFeatured = false,
        string? shortDescription = null)
    {
        var description = $"{name}, Zavansa'nın katalog odaklı aydınlatma yaklaşımını yansıtan modern ve dekoratif bir tasarımdır. Yaşam alanlarında yalın, sıcak ve dikkat çekici bir ışık etkisi oluşturmak için tasarlanmıştır.";

        return new Product
        {
            Name = name,
            ShortDescription = shortDescription ?? $"{collectionName} içinde yer alan modern ve hafif aydınlatma tasarımı.",
            Description = description,
            Width = width,
            Height = height,
            Material = material,
            BulbType = bulbType,
            Colors = colors,
            ImageUrl = PlaceholderImage,
            Price = null,
            IsFeatured = isFeatured,
            IsActive = true,
            CategoryId = categories[categoryName],
            ProductCollectionId = collections[collectionName],
            CreatedAt = DateTime.UtcNow
        };
    }

    private static void SeedSettings(ApplicationDbContext context)
    {
        if (context.SiteSettings.Any())
        {
            return;
        }

        context.SiteSettings.Add(new SiteSetting
        {
            BrandName = "Zavansa Aydınlatma ve Dekor",
            Slogan = "Modern yaşam alanları için özgün aydınlatma tasarımları",
            AboutText = "Zavansa, modern yaşam alanları için avize, aplik, masa lambası ve dekoratif aydınlatma ürünleri sunan bir aydınlatma ve dekor markasıdır. Hafif malzemeler, yalın formlar ve katalog tabanlı ürün sunumu ile kullanıcıların ihtiyaçlarına uygun modelleri kolayca incelemesini sağlar.",
            Phone = "+90 555 000 00 00",
            Email = "info@zavansa.local",
            Address = "İstanbul, Türkiye",
            CatalogPdfUrl = "/catalog/zavansa-katalog.pdf"
        });

        context.SaveChanges();
    }

    private static void SeedAdmin(ApplicationDbContext context)
    {
        if (context.AdminUsers.Any())
        {
            return;
        }

        var admin = new AdminUser
        {
            Username = "admin",
            Email = "admin@zavansa.local",
            FullName = "Zavansa Admin",
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        var hasher = new PasswordHasher<AdminUser>();
        admin.PasswordHash = hasher.HashPassword(admin, "Admin123!");

        context.AdminUsers.Add(admin);
        context.SaveChanges();
    }
}
