using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZavansaCatalogMVC.Models;

public class Product
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Ürün adı zorunludur.")]
    [MaxLength(120)]
    [Display(Name = "Ürün Adı")]
    public string Name { get; set; } = string.Empty;

    [MaxLength(250)]
    [Display(Name = "Kısa Açıklama")]
    public string? ShortDescription { get; set; }

    [Display(Name = "Açıklama")]
    public string? Description { get; set; }

    [Display(Name = "Genişlik")]
    public string? Width { get; set; }

    [Display(Name = "Yükseklik")]
    public string? Height { get; set; }

    [Display(Name = "Malzeme")]
    public string? Material { get; set; }

    [Display(Name = "Ampül Tipi")]
    public string? BulbType { get; set; }

    [Display(Name = "Renkler")]
    public string? Colors { get; set; }

    [Display(Name = "Görsel")]
    public string? ImageUrl { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    [Display(Name = "Fiyat")]
    public decimal? Price { get; set; }

    [Display(Name = "Öne Çıkan")]
    public bool IsFeatured { get; set; }

    [Display(Name = "Aktif")]
    public bool IsActive { get; set; } = true;

    [Display(Name = "Kategori")]
    public int CategoryId { get; set; }

    public Category? Category { get; set; }

    [Display(Name = "Koleksiyon")]
    public int ProductCollectionId { get; set; }

    public ProductCollection? ProductCollection { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
