using System.ComponentModel.DataAnnotations;

namespace ZavansaCatalogMVC.Models;

public class SiteSetting
{
    public int Id { get; set; }

    [Required]
    [Display(Name = "Marka Adı")]
    public string BrandName { get; set; } = "Zavansa Aydınlatma ve Dekor";

    [Required]
    [Display(Name = "Slogan")]
    public string Slogan { get; set; } = "Modern yaşam alanları için özgün aydınlatma tasarımları";

    [Required]
    [Display(Name = "Hakkımızda")]
    public string AboutText { get; set; } = string.Empty;

    [Display(Name = "Telefon")]
    public string? Phone { get; set; }

    [Display(Name = "E-posta")]
    public string? Email { get; set; }

    [Display(Name = "Adres")]
    public string? Address { get; set; }

    [Display(Name = "Instagram")]
    public string? InstagramUrl { get; set; }

    [Display(Name = "WhatsApp")]
    public string? WhatsAppUrl { get; set; }

    [Display(Name = "Katalog PDF")]
    public string? CatalogPdfUrl { get; set; }
}
