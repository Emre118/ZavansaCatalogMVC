using ZavansaCatalogMVC.Models;

namespace ZavansaCatalogMVC.ViewModels;

public class ProductListViewModel
{
    public IEnumerable<Product> Products { get; set; } = new List<Product>();
    public IEnumerable<Category> Categories { get; set; } = new List<Category>();
    public IEnumerable<ProductCollection> Collections { get; set; } = new List<ProductCollection>();
    public string? Search { get; set; }
    public int? CategoryId { get; set; }
    public int? CollectionId { get; set; }
}
