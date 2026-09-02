# IyzicoDemo

Ürün, kategori, sepet ve sipariş yönetimi sunan; ödemeleri **iyzico sandbox** üzerinden alan örnek e-ticaret uygulaması. Backend ASP.NET Core Web API, kullanıcı arayüzü ise React + Vite ile geliştirilmiştir.

## Özellikler

- Kullanıcı kaydı, giriş ve çıkış işlemleri (ASP.NET Core Identity)
- Yönetim panelinde ürün, kategori ve sipariş yönetimi
- Misafir sepeti oluşturma ve sepet kalemi silme
- iyzico ile karttan sandbox ödeme alma
- Başarılı ödemede sipariş, sipariş kalemleri ve ödeme kaydı oluşturma; stok düşme
- Swagger üzerinden API keşfi ve test imkânı

## Teknolojiler

| Katman | Teknolojiler |
| --- | --- |
| Backend | .NET 10, ASP.NET Core, Entity Framework Core, ASP.NET Core Identity |
| Veritabanı | SQL Server |
| Ödeme | `Iyzipay` SDK (iyzico sandbox) |
| Frontend | React 19, Vite, React Router, Material UI, Axios |

## Proje yapısı

```text
IyzicoDemo/
├── IyzicoDemo/              # ASP.NET Core API
│   ├── Controllers/          # Hesap, sepet, ürün, sipariş ve ödeme uçları
│   ├── Entity/               # EF Core varlıkları
│   ├── Services/             # İş kuralları ve iyzico ödeme servisi
│   ├── Migrations/           # Veritabanı migration'ları
│   └── wwwroot/images/       # Yüklenen ürün görselleri
├── iyzico-react-ui/          # React + Vite istemcisi
└── IyzicoDemo.sln
```

## Gereksinimler

- .NET SDK 10
- Node.js 20 veya üzeri
- SQL Server (LocalDB, Express veya tam sürüm)
- iyzico sandbox hesabı ve API anahtarları

## Kurulum

1. Depoyu klonlayın ve proje dizinine geçin.

   ```bash
   git clone <repo-adresi>
   cd IyzicoDemo
   ```

2. Backend yapılandırmasını hazırlayın. `IyzicoDemo/appsettings.json` içinde bulunan bağlantı dizesini kendi SQL Server örneğinize göre ayarlayın. Gerçek anahtarları kaynak kontrolüne koymayın; aşağıdaki komutlarla user secrets kullanabilirsiniz:

   ```bash
   cd IyzicoDemo
   dotnet user-secrets init
   dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost;Database=IyzicoDemoDb;Trusted_Connection=True;TrustServerCertificate=True"
   dotnet user-secrets set "Stripe:ApiKey" "sandbox-api-key"
   dotnet user-secrets set "Stripe:SecretKey" "sandbox-secret-key"
   dotnet user-secrets set "Stripe:BaseUrl" "https://sandbox-api.iyzipay.com"
   ```

   > Kodda iyzico ayar bölümü tarihsel adlandırma nedeniyle `Stripe` olarak geçmektedir; burada `ApiKey`, `SecretKey` ve `BaseUrl` iyzico için kullanılır.

3. Veritabanını oluşturun ve migration'ları uygulayın.

   ```bash
   dotnet ef database update
   ```

4. API'yi başlatın.

   ```bash
   dotnet run
   ```

   Geliştirme ortamında Swagger arayüzü açılır. Çalışan adresi `Properties/launchSettings.json` dosyasından öğrenebilirsiniz.

5. Ayrı bir terminalde arayüzü başlatın.

   ```bash
   cd ../iyzico-react-ui
   npm install
   npm run dev
   ```

   Vite uygulaması varsayılan olarak `http://localhost:5173` üzerinde çalışır; `/api` ve `/images` istekleri backend'e yönlendirilir.

## Varsayılan yönetici hesabı

Uygulama başlangıcında aşağıdaki yönetici hesabı yoksa otomatik oluşturulur:

| Alan | Değer |
| --- | --- |
| Kullanıcı adı | `admin` |
| E-posta | `admin@example.com` |
| Parola | `Admin123!` |

Bu bilgiler yalnızca geliştirme içindir. Canlı ortamda başlangıç parolasını değiştirin veya seed işlemini güvenli bir dağıtım sürecine taşıyın.

## Başlıca API uçları

| Alan | Uçlar |
| --- | --- |
| Hesap | `POST /api/account/register`, `POST /api/account/login`, `POST /api/account/logout` |
| Ürün ve kategori | `GET /api/product/{id}`, `GET /api/product/get-all-product`, `POST /api/product/create-product`, `PUT /api/product/{id}`, `DELETE /api/product/{id}` |
| Sepet | `POST /api/cart/create`, `DELETE /api/cart/delete-cart-item/{id}` |
| Sipariş | `GET /api/order` |
| Ödeme | `POST /api/payment/checkout/success` |

Ödeme isteği müşteri bilgileri ile kart bilgilerini içerir; yalnızca iyzico'nun sandbox test kartlarıyla test edilmelidir.

## Geliştirme komutları

```bash
# Backend
cd IyzicoDemo
dotnet build
dotnet run

# Frontend
cd iyzico-react-ui
npm run lint
npm run build
npm run preview
```
