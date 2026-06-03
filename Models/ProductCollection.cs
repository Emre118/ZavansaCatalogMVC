using System.ComponentModel.DataAnnotations;

namespace ZavansaCatalogMVC.Models;

public class ProductCollection
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Koleksiyon adı zorunludur.")]
    [MaxLength(100)]
    [Display(Name = "Koleksiyon Adı")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Açıklama")]
    public string? Description { get; set; }

    [Display(Name = "Görsel")]
    public string? ImageUrl { get; set; }

    [Display(Name = "Sıralama")]
    public int DisplayOrder { get; set; }

    [Display(Name = "Aktif")]
    public bool IsActive { get; set; } = true;

    public ICollection<Product> Products { get; set; } = new List<Product>();
}
