# ZavansaCatalogMVC

ZavansaCatalogMVC, **Zavansa Aydınlatma ve Dekor** markası için hazırlanmış ASP.NET Core MVC tabanlı bir ürün katalog sitesidir. Proje bir öğrenci Web Programlama ödevi kapsamında; MVC yapısı, Razor Views, Entity Framework Core, SQLite veritabanı, admin girişi ve CRUD işlemlerini göstermek amacıyla geliştirilmiştir.

## Proje Amacı

Bu uygulama avize, aplik, masa lambası, abajur ve çocuk odası aydınlatma ürünlerinin katalog mantığıyla listelenmesini sağlar. Proje tam bir e-ticaret sistemi değildir.

## Kullanılan Teknolojiler

- C#
- ASP.NET Core MVC
- Razor Views
- Entity Framework Core
- SQLite
- Bootstrap 5
- Cookie tabanlı admin kimlik doğrulama

## Veritabanı Tabloları

- Products
- Categories
- ProductCollections
- ContactMessages
- AdminUsers
- SiteSettings

## Public Sayfalar

- Ana Sayfa
- Hakkımızda
- Ürünler
- Ürün Detay
- Koleksiyonlar
- Koleksiyon Detay
- İletişim
- PDF Katalog

## Admin Panel Sayfaları

Admin giriş adresi:

```bash
/Admin/Account/Login
```

Admin panelde bulunan sayfalar:

- Dashboard
- Ürünler
- Kategoriler
- Koleksiyonlar
- Mesajlar
- Site Ayarları

## CRUD Özellikleri

- Ürün ekleme, düzenleme, silme ve detay görüntüleme
- Ürün görseli yükleme
- Kategori ekleme, düzenleme, silme ve detay görüntüleme
- Koleksiyon ekleme, düzenleme, silme ve detay görüntüleme
- İletişim mesajlarını listeleme, detay görüntüleme, okundu/cevaplandı işaretleme ve silme
- Site ayarlarını düzenleme

## Kurulum ve Çalıştırma

Projeyi çalıştırmak için repository kök dizininde şu komutları kullanın:

```bash
dotnet restore
dotnet build
dotnet run
```

İlk çalıştırmada SQLite veritabanı `zavansa_catalog.db` adıyla oluşturulur ve örnek kategori, koleksiyon, ürün, site ayarı ve admin kullanıcısı eklenir.

## Admin Demo Girişi

```text
username: admin
password: Admin123!
```

Demo admin şifresi veritabanında düz metin olarak tutulmaz; `PasswordHasher<AdminUser>` ile hashlenerek kaydedilir.

## Katalog PDF

Repository içindeki `docs/zavansa-katalog-2026-subat.pdf` dosyası `wwwroot/catalog/zavansa-katalog.pdf` konumuna kopyalanmıştır. Public katalog sayfası bu dosyayı açma ve indirme bağlantısı sunar.

## Notlar

- Bu proje bir ürün katalog sitesidir, e-ticaret sitesi değildir.
- Sepet, ödeme, kargo ve sipariş yönetimi bulunmaz.
- Ürün fiyatları varsayılan olarak boş bırakılmıştır.
- Ürün görselleri admin panelinden daha sonra yüklenebilir.
- Varsayılan ürünlerde placeholder görsel kullanılır.
