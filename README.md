<div align="center">

# 🌍 WktExampleWithGeneric

**Generic Repository deseni ve WKT (Well-Known Text) tabanlı Coğrafi Bilgi Sistemleri (GIS) verilerini işleyen modern C# .NET & React Full-Stack mimari şablonu.**

[![Build Status](https://img.shields.io/badge/build-passing-brightgreen?style=for-the-badge)](https://github.com/UmutGuvenUslu/WktExampleWithGeneric/actions)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg?style=for-the-badge)](LICENSE)
[![.NET](https://img.shields.io/badge/.NET-8.0-purple?style=for-the-badge&logo=dotnet)](https://dotnet.microsoft.com/)
[![React](https://img.shields.io/badge/React-18.x-61DAFB?style=for-the-badge&logo=react&logoColor=black)](https://react.dev/)
[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-brightgreen?style=for-the-badge)](CONTRIBUTING.md)
[![GitHub Stars](https://img.shields.io/github/stars/UmutGuvenUslu/WktExampleWithGeneric?style=for-the-badge&color=blue)](https://github.com/UmutGuvenUslu/WktExampleWithGeneric/stargazers)

<p align="center">
  <a href="#-neden-wktexamplewithgeneric">Neden WktExampleWithGeneric?</a> •
  <a href="#-sistem-mimarisi-ve-katmanlar-architecture-flow">Mimari Akış</a> •
  <a href="#-wkt-mekânsal-veri-işleme-akışı-gis-spatial-flow">WKT İşleme Akışı</a> •
  <a href="#-generic-repository-ve-veri-akışı">Generic Repository Akışı</a> •
  <a href="#-temel-özellikler">Özellikler</a> •
  <a href="#-teknik-altyapı">Teknolojiler</a> •
  <a href="#-kurulum-ve-çalıştırma">Kurulum</a> •
  <a href="#-proje-dizin-yapısı">Dizin Yapısı</a>
</p>

</div>

---

## 🎯 Neden WktExampleWithGeneric?

> **Problem:** Full-stack projelerde mimari tutarlılığı sağlamak, tekrarlayan CRUD operasyonlarını yönetmek ve özellikle harita tabanlı mekânsal (WKT / Point / Polygon / LineString) geometrik verileri Entity Framework üzerinden veritabanına sorunsuz aktarmak karmaşık ve zaman alıcı bir süreçtir.

**Çözüm:** **WktExampleWithGeneric**, kurumsal C# .NET mimarisi üzerine inşa edilmiş, **Generic Repository & Unit of Work** desenlerini mekânsal WKT geometrileriyle birleştiren uçtan uca hazır bir full-stack referans projesidir.

---

## 🧠 Sistem Mimarisi ve Katmanlar (Architecture Flow)

Uygulamanın SoA (Separation of Concerns) prensiplerine göre yapılandırılmış katmanlı veri iletimi:

```mermaid
flowchart TD
    subgraph Frontend_Katmanı["💻 Presentation Layer (React UI)"]
        UI[React Components / Map Views]
        Axios[Axios / REST Client]
    end

    subgraph API_Katmanı["🌐 API Layer (ASP.NET Core)"]
        Ctrl[API Controllers]
        DTO[DTOs & Model Mapping]
        Middleware[Global Exception & Auth Middleware]
    end

    subgraph Business_Katmanı["🧠 Service & Core Layer"]
        Service[Application Services]
        IGeneric[IGenericRepository<T> & IUnitOfWork]
        Entities[Domain Entities / WKT Models]
    end

    subgraph Data_Katmanı["⚙️ Data Access Layer (EF Core)"]
        GenericRepo[GenericRepository<T> Implementasyonu]
        AppCtx[AppDbContext / NetTopologySuite]
    end

    subgraph DB_Katmanı["💾 Database Layer"]
        DB[(PostgreSQL + PostGIS / SQL Server)]
    end

    UI <--> Axios
    Axios <-->|JSON / WKT Geometry| Ctrl
    Ctrl <--> Middleware
    Ctrl <--> DTO
    Ctrl <--> Service
    Service <--> IGeneric
    IGeneric <--> GenericRepo
    GenericRepo <--> AppCtx
    AppCtx <--> DB
```

---

## 🗺️ WKT Mekânsal Veri İşleme Akışı (GIS Spatial Flow)

Haritadan çizilen bir geometrinin (WKT) backend'e aktarılması ve spatial sorgulanma süreci:

```mermaid
flowchart LR
    A[🗺️ Harita Üzerinde Geometri Çizimi: Point/Polygon] --> B[📐 WKT Formatına Dönüştür: POINT 35.33 41.28]
    B --> C[🌐 POST /api/geometries Endpoint'ine Gönder]
    C --> D[⚙️ NetTopologySuite WKTReader ile Geometry Nesnesine Parse Et]
    D --> E[💾 PostGIS / Spatial DB Kolonuna Kaydet]
    E --> F[🔍 Spatial Index ile Hızlı Arama & Filtreleme: ST_Contains]
    F --> G[📦 Sonuçları WKT/GeoJSON Olarak UI'a Geri Döndür]
```

---

## 🔄 Generic Repository ve Veri Akışı

Generic katman üzerinden yürütülen tip güvenli veri okuma/yazma döngüsü:

```mermaid
sequenceDiagram
    autonumber
    actor Client as 💻 Frontend İstemcisi
    participant Ctrl as 🌐 GenericApiController
    participant Serv as 🧠 EntityService
    participant Repo as ⚙️ GenericRepository<T>
    participant Context as 💾 DbContext
    participant DB as 🗄️ Veritabanı

    Client->>Ctrl: GET /api/v1/entities/{id}
    Ctrl->>Serv: GetByIdAsync(id)
    Serv->>Repo: GetByIdAsync(id)
    Repo->>Context: Set<T>().FindAsync(id)
    Context->>DB: SELECT * FROM Table WHERE Id = @id
    DB-->>Context: Tablo Satırı / Geometri Verisi
    Context-->>Repo: Entity<T> Instance
    Repo-->>Serv: Entity<T>
    Serv-->>Ctrl: DTO / Response ViewModel
    Ctrl-->>Client: HTTP 200 (JSON + WKT Data)
```

---

## ✨ Temel Özellikler

* ⚡️ **Hızlı Başlangıç:** Önceden yapılandırılmış katmanlı mimari ile doğrudan iş mantığına odaklanma.
* 🧩 **Generic Repository & Unit of Work:** Kod tekrarını önleyen, tip güvenli generic CRUD mekanizması.
* 📍 **WKT & GIS Entegrasyonu:** Well-Known Text geometrik formatlarını okuma, yazma ve veritabanında saklama desteği.
* 🏗️ **Temiz & Modüler Mimari:** Core, Data, API ve UI katmanlarının birbirinden tam bağımsız ayrımı.
* 🧪 **Test Edilebilir Yapı:** Dependency Injection (DI) ve Interface tabanlı kolay unit/integration test altyapısı.
* 🔄 **Çapraz Platform:** .NET Core gücüyle Linux, macOS ve Windows üzerinde kesintisiz çalışma.

---

## 🛠️ Teknik Altyapı

| Teknoloji | Kullanım Amacı | Temel Avantaj |
| :--- | :--- | :--- |
| **C# / .NET 8** | Backend API & İş Mantığı | Güçlü tip güvenliği, yüksek asenkron performans |
| **ASP.NET Core Web API** | RESTful Uç Noktaları | Hızlı, ölçeklenebilir ve modüler routing altyapısı |
| **Entity Framework Core** | ORM & Veri Erişim | LINQ desteği, Migration yönetimi ve Spatial uzantıları |
| **NetTopologySuite** | CBS / GIS Geometri Motoru | WKT, WKB ve GeoJSON formatlarında gelişmiş mekânsal hesaplama |
| **React** | Kullanıcı Arayüzü (Frontend) | Bileşen tabanlı, modern ve dinamik harita/veri arayüzü |
| **PostgreSQL / MS SQL** | İlişkisel & Mekânsal Veritabanı | Güvenilir ve spatial indeksleme yetenekli veri depolama |

---

## 📂 Proje Dizin Yapısı

```plaintext
WktExampleWithGeneric/
├── 📁 Backend/                                  # C# .NET Çözüm ve Projeleri
│   ├── 📄 WktExampleWithGeneric.sln            # Visual Studio Çözüm Dosyası
│   ├── 📁 WktExampleWithGeneric.Api/           # API Katmanı
│   │   ├── 📁 Controllers/                     # REST Uç Noktaları
│   │   ├── 📁 Models/                          # DTO ve İstek Modelleri
│   │   ├── 📁 Services/                        # Uygulama Servisleri
│   │   ├── 📄 appsettings.json                 # Yapılandırma ve Connection Strings
│   │   └── 📄 Program.cs                       # Dependency Injection ve Middleware Girişi
│   ├── 📁 WktExampleWithGeneric.Core/          # Domain Katmanı
│   │   ├── 📁 Interfaces/                      # IGenericRepository, IUnitOfWork
│   │   └── 📁 Entities/                        # Varlıklar ve WKT Geometri Tipleri
│   └── 📁 WktExampleWithGeneric.Data/          # Veri Erişim Katmanı
│       ├── 📁 Context/                         # AppDbContext ve ModelBuilder
│       ├── 📁 Repositories/                    # GenericRepository implementasyonu
│       └── 📁 Migrations/                      # EF Core Migration Dosyaları
├── 📁 Frontend/                                 # React SPA Kaynak Kodları
│   ├── 📁 public/                              # Statik Dosyalar
│   ├── 📁 src/
│   │   ├── 📁 components/                      # Harita ve Veri Bileşenleri
│   │   ├── 📁 pages/                           # Sayfa Görünümleri
│   │   ├── 📄 App.js
│   │   └── 📄 index.js
│   ├── 📄 package.json
│   └── 📄 README.md
├── 📄 .gitignore
├── 📄 LICENSE
└── 📄 README.md
```

---

## 🚀 Kurulum ve Çalıştırma

### Gereksinimler

* **[.NET SDK 8.0+](https://dotnet.microsoft.com/download)**
* **[Node.js & npm (v18+)](https://nodejs.org/)**
* **[PostgreSQL + PostGIS](https://www.postgresql.org/)** veya **SQL Server**
* **[Git](https://git-scm.com/)**

---

### Kurulum Adımları

1. **Repoyu Klonlayın:**
   ```bash
   git clone [https://github.com/UmutGuvenUslu/WktExampleWithGeneric.git](https://github.com/UmutGuvenUslu/WktExampleWithGeneric.git)
   cd WktExampleWithGeneric
   ```

2. **Backend'i Ayağa Kaldırın:**
   ```bash
   cd Backend
   dotnet restore
   dotnet ef database update --project WktExampleWithGeneric.Data --startup-project WktExampleWithGeneric.Api
   dotnet run --project WktExampleWithGeneric.Api
   ```
   *API varsayılan olarak `https://localhost:7001` veya `http://localhost:5000` adresinde açılır.*

3. **Frontend'i Başlatın:**
   *Yeni bir terminal sekmesi açın:*
   ```bash
   cd Frontend
   npm install
   npm start
   ```
   *Arayüz `http://localhost:3000` adresinde çalışmaya başlayacaktır.*

---

## ⚙️ Ortam Yapılandırması

* **Backend:** Veritabanı bağlantı adresinizi `Backend/WktExampleWithGeneric.Api/appsettings.Development.json` dosyasından ayarlayın:
  ```json
  {
    "ConnectionStrings": {
      "DefaultConnection": "Host=localhost;Database=WktGenericDb;Username=postgres;Password=yourpassword"
    }
  }
  ```
* **Frontend:** API bağlantı URL'sini `Frontend/.env` dosyasında tanımlayabilirsiniz:
  ```env
  REACT_APP_API_BASE_URL=https://localhost:7001/api
  ```

---

## 🤝 Katkıda Bulunma

Projeye katkı sağlamak için:

1. Repoyu Fork'layın (`Fork`)
2. Yeni bir özellik dalı açın (`git checkout -b feature/YeniMekansalModul`)
3. Değişikliklerinizi commit edin (`git commit -m 'feat: Yeni WKT parse mekanizması eklendi'`)
4. Dalınıza push yapın (`git push origin feature/YeniMekansalModul`)
5. Bir **Pull Request** açın

---

<div align="center">

Geliştirici: **[Umut Güven Uslu](https://github.com/UmutGuvenUslu)**

⭐ Projeyi beğendiyseniz yıldız vermeyi unutmayın!

</div>
