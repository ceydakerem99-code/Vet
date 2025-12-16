# 🐾 VETERİNER KLİNİK YÖNETİM SİSTEMİ
## Proje Sunumu

---

# 📋 SLAYT 1 - Proje Tanıtımı

## Veteriner Klinik Yönetim Sistemi

**Proje Adı:** VeterinerProjectApp  
**Platform:** Windows Forms (.NET 8)  
**Dil:** C#  
**Veritabanı:** JSON / SQLite

### Projenin Amacı
Veteriner kliniklerinin günlük işlemlerini dijitalleştiren, hayvan kayıtlarını ve randevuları yöneten kapsamlı bir masaüstü uygulaması.

---

# 📋 SLAYT 2 - Kullanıcı Rolleri

## 3 Farklı Kullanıcı Tipi

| 👨‍⚕️ Klinik Yöneticisi | 👤 Pet Kullanıcısı | 🐕 Patili Koruyucu |
|------------------------|-------------------|-------------------|
| Tüm yetkiler | Kendi hayvanları | Sokak hayvanları |
| Randevu onaylama | Randevu alma | Hayvan getirme |
| İşlem kaydetme | Geçmiş görme | Tedavi takibi |
| Rapor oluşturma | - | - |

---

# 📋 SLAYT 3 - Nesne Yönelimli Programlama (OOP)

## Kullanılan OOP Prensipleri

### ✅ Encapsulation (Kapsülleme)
- Private alanlar, public property'ler
- Şifre bilgisi korumalı

### ✅ Inheritance (Kalıtım)
```
KullaniciBase (abstract)
├── VeterinerAdmin
├── HayvanSahibi
└── SokakHayvaniSorumlusu
```

### ✅ Polymorphism (Çok Biçimlilik)
- Override metodlar (AnaSayfaGetir, YetkileriAyarla)

### ✅ Abstraction (Soyutlama)
- Interface'ler (IKullanici, IHayvan)
- Abstract sınıflar

---

# 📋 SLAYT 4 - Class Yapısı

## Kullanıcı Hiyerarşisi
```
KullaniciBase (abstract)
    │
    ├── VeterinerAdmin
    │       └── DiplomaNo, UzmanlikAlani
    │
    ├── HayvanSahibi
    │       └── Adres, HayvanSayisi
    │
    └── SokakHayvaniSorumlusu
            └── SorumluOlduguBolge
```

## Hayvan Hiyerarşisi
```
HayvanBase (abstract)
    │
    ├── EvcilHayvan
    │       └── SahipId, ChipNumarasi
    │
    └── SokakHayvani
            └── BulunduguBolge, TedaviOnayliMi
```

---

# 📋 SLAYT 5 - Interface Yapısı

## 4 Adet Interface

| Interface | Açıklama |
|-----------|----------|
| `IKullanici` | Kullanıcı işlemleri |
| `IHayvan` | Hayvan işlemleri |
| `IMuayene` | Muayene işlemleri |
| `ITedavi` | Tedavi işlemleri |

```csharp
public interface IKullanici
{
    bool GirisYap(string email, string sifre);
    void CikisYap();
    bool YetkiKontrol(string islemAdi);
}
```

---

# 📋 SLAYT 6 - Proje Yapısı

## Klasör Organizasyonu

```
📁 VeterinerProjectApp/
├── 📂 Models/      → 12 veri modeli
├── 📂 Services/    → 4 servis sınıfı
├── 📂 Interfaces/  → 4 interface
├── 📂 Enums/       → 4 enum
└── 📄 Forms        → 15+ Windows Form
```

### Dosya Sayıları
- **Toplam Form:** 15+
- **Model Sınıfı:** 12
- **Servis:** 4
- **Interface:** 4
- **Enum:** 4

---

# 📋 SLAYT 7 - Temel Özellikler

## ✅ Uygulama Özellikleri

1. **Kullanıcı Yönetimi**
   - Giriş / Kayıt sistemi
   - Rol bazlı yetkilendirme

2. **Hayvan Kayıt**
   - Evcil ve sokak hayvanı kaydı
   - Aşı ve kısırlık takibi

3. **Randevu Sistemi**
   - Geliş sebebi seçimi
   - Onay/Red mekanizması

4. **İşlem Kayıt**
   - Muayene kaydetme
   - Tedavi planı

5. **Nöbetçi Klinik**
   - Gün bazlı arama

---

# 📋 SLAYT 8 - Teknik Detaylar

## Kullanılan Teknolojiler

| Teknoloji | Kullanım |
|-----------|----------|
| C# | Ana programlama dili |
| .NET 8 | Framework |
| Windows Forms | UI |
| JSON | Veri kaydetme |
| SQLite | Veritabanı |

## Design Patterns
- **Singleton:** VeriYoneticisi, OturumYoneticisi
- **Factory:** Kullanıcı oluşturma

---

# 📋 SLAYT 9 - Ekran Görüntüleri

## Ana Ekranlar

| Ekran | Açıklama |
|-------|----------|
| Giriş | Kullanıcı login |
| Ana Menü | Role göre yönlendirme |
| Randevu Al | Geliş sebebi ve tarih seçimi |
| Admin Panel | Randevu onaylama |
| Hasta Görüntüle | Hayvan bilgileri |

---

# 📋 SLAYT 10 - Sonuç

## Proje Kazanımları

✅ Nesne Yönelimli Programlama uygulaması  
✅ Kalıtım ve Interface kullanımı  
✅ Windows Forms ile UI geliştirme  
✅ Veri yönetimi (JSON/SQLite)  
✅ Rol bazlı yetkilendirme  
✅ SOLID prensipleri  

---

# 🙏 TEŞEKKÜRLER

## Sorularınız?

**Hazırlayan:** Ceyda Kerem  
**Proje:** Veteriner Klinik Yönetim Sistemi  
**Tarih:** Aralık 2024
