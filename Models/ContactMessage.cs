using System.ComponentModel.DataAnnotations;

namespace ZavansaCatalogMVC.Models;

public class ContactMessage
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Ad soyad zorunludur.")]
    [MaxLength(100)]
    [Display(Name = "Ad Soyad")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "E-posta zorunludur.")]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
    [MaxLength(120)]
    [Display(Name = "E-posta")]
    public string Email { get; set; } = string.Empty;

    [MaxLength(30)]
    [Display(Name = "Telefon")]
    public string? Phone { get; set; }

    [MaxLength(150)]
    [Display(Name = "Konu")]
    public string? Subject { get; set; }

    [Required(ErrorMessage = "Mesaj zorunludur.")]
    [MaxLength(1000)]
    [Display(Name = "Mesaj")]
    public string Message { get; set; } = string.Empty;

    [MaxLength(150)]
    [Display(Name = "İlgilenilen Ürün")]
    public string? InterestedProductName { get; set; }

    public int? ProductId { get; set; }

    public Product? Product { get; set; }

    [Display(Name = "Durum")]
    public MessageStatus Status { get; set; } = MessageStatus.New;

    [Display(Name = "Gönderim Tarihi")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
