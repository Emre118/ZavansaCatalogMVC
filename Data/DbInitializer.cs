using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ZavansaCatalogMVC.Models;

namespace ZavansaCatalogMVC.Data;

public static class DbInitializer
{
    private const string PlaceholderImage = "/images/placeholders/product-placeholder.svg";

    private static readonly IReadOnlyDictionary<string, string> ProductImagePaths = new Dictionary<string, string>
    {
        ["LAVA Beton Avize"] = "/images/products/lava-beton-avize.jpg",
        ["LUMEN Beton Avize"] = "/images/products/lumen-beton-avize.jpg",
        ["ORBIT Beton Avize"] = "/images/products/orbit-beton-avize.jpg",
        ["LAVA 2'li Beton Avize"] = "/images/products/lava-2li-beton-avize.jpg",
        ["LAVA 3'lü Beton Avize"] = "/images/products/lava-3lu-beton-avize.jpg",
        ["LAVA 5'li Beton Avize"] = "/images/products/lava-5li-beton-avize.jpg",
        ["LAVA 3'lü V2 Beton Avize"] = "/images/products/lava-3lu-v2-beton-avize.jpg",
        ["LUMEN 3'lü Beton Avize"] = "/images/products/lumen-3lu-beton-avize.jpg",
        ["HIKARI Avize"] = "/images/products/hikari-avize.jpg",
        ["SATORI Avize"] = "/images/products/satori-avize.jpg",
        ["LINO Avize"] = "/images/products/lino-avize.jpg",
        ["Aniko Avize"] = "/images/products/aniko-avize.jpg",
        ["Oromi Avize"] = "/images/products/oromi-avize.jpg",
        ["Abysell Long Avize"] = "/images/products/abysell-long-avize.jpg",
        ["Abysell Avize"] = "/images/products/abysell-avize.jpg",
        ["Twisted v1 Avize"] = "/images/products/twisted-v1-avize.jpg",
        ["Twisted v2 Avize"] = "/images/products/twisted-v2-avize.jpg",
        ["Vori v1 Avize"] = "/images/products/vori-v1-avize.jpg",
        ["Vori v2 Avize"] = "/images/products/vori-v2-avize.jpg",
        ["Stone Glow Aplik"] = "/images/products/stone-glow-aplik.jpg",
        ["Aqua Masa Lambası"] = "/images/products/aqua-masa-lambasi.jpg",
        ["Void Masa Lambası"] = "/images/products/void-masa-lambasi.jpg",
        ["Arco Abajur Beyaz"] = "/images/products/arco-abajur-beyaz.jpg",
        ["Arco Abajur Kumlu Bej"] = "/images/products/arco-abajur-kumlu-bej.jpg",
        ["Ufo Lamp"] = "/images/products/ufo-lamp.jpg",
        ["Roket Masa/Gece Lambası"] = "/images/products/roket-masa-gece-lambasi.jpg",
        ["Roket 2 Masa/Gece Lamp"] = "/images/products/roket-2-masa-gece-lambasi.jpg"
    };

    private static readonly IReadOnlyDictionary<string, string> CollectionImagePaths = new Dictionary<string, string>
    {
        ["Beton Collection"] = "/images/collections/beton-collection.jpg",
        ["Ori Design Collection"] = "/images/collections/ori-design-collection.jpg",
        ["Monolit Collection"] = "/images/collections/monolit-collection.jpg",
        ["Modern Twisted Collection"] = "/images/collections/modern-twisted-collection.jpg",
        ["Stone Glow"] = "/images/collections/stone-glow.jpg",
        ["Masa Lambaları"] = "/images/collections/masa-lambalari.jpg",
        ["Arco Abajur"] = "/images/collections/arco-abajur.jpg",
        ["Çocuk Odası Aydınlatma"] = "/images/collections/cocuk-odasi-aydinlatma.jpg"
    };

    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

        context.Database.EnsureCreated();

        SeedCategories(context);
        SeedCollections(context);
        SeedProducts(context);
        UpdateSeededImagePaths(context);
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

    private static void UpdateSeededImagePaths(ApplicationDbContext context)
    {
        var hasChanges = false;

        foreach (var product in context.Products)
        {
            if (ProductImagePaths.TryGetValue(product.Name, out var imageUrl) && product.ImageUrl != imageUrl)
            {
                product.ImageUrl = imageUrl;
                hasChanges = true;
            }
        }

        foreach (var collection in context.ProductCollections)
        {
            if (CollectionImagePaths.TryGetValue(collection.Name, out var imageUrl) && collection.ImageUrl != imageUrl)
            {
                collection.ImageUrl = imageUrl;
                hasChanges = true;
            }
        }

        if (hasChanges)
        {
            context.SaveChanges();
        }
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
