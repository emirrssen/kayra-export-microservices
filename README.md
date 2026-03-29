# KayraExport Microservices

KayraExport Microservices, modern yazılım mimarisi prensipleriyle (Microservices & Clean Architecture) geliştirilmiş kapsamlı bir backend projesidir. Sistem, yüksek ölçeklenebilirlik, esneklik ve performans hedeflenerek tasarlanmıştır.

## 🔗 Kod Deposu Bağlantısı
**GitHub Repository:** [https://github.com/emirrssen/kayra-export-microservices.git](https://github.com/emirrssen/kayra-export-microservices.git)

---

## 🏗️ Mimari ve Design Pattern Kararları (Kod Belgeleri)

Bu projenin temelinde sistemin gevşek bağlı (loosely coupled) ve sürdürülebilir olmasını sağlayan belirgin tasarım desenleri kullanılmıştır:

1. **CQRS (Command Query Responsibility Segregation) & MediatR**
   - *Neden kullanıldı?* Okuma (Query) ve yazma (Command) işlemlerinin birbirinden ayrılarak daha rahat ölçeklenmesini ve karmaşıklığın azalmasını sağlamak amacıyla. Proje içerisindeki tüm business logic (iş kuralları) modülleri birbirinden izole edilmiş Handler sınıfları içerisinde yürütülür.
   
2. **Domain-Driven Design (DDD) & Encapsulation**
   - *Neden kullanıldı?* Entity sınıflarının dışarıdan rastgele değiştirilebilir olmasını (Anemic Domain Model yaklaşımını) engellemek için. Örneğin `Product` entity'si tüm property'lerini kapsüller (`private set`) ve özellikleri sadece içeride dışarıya kapalı kural denetimi sağlayan `Update` gibi encapsulation metotları üzerinden değiştirilebilir.
   
3. **Repository Pattern**
   - *Neden kullanıldı?* Veri erişim soyutlaması (abstraction) sağlayarak veritabanı teknolojisi bağımlılıklarını izole etmek için. Entity Framework Core gibi kütüphaneler Application katmanına (business kurallarına) sızmaz.
   
4. **Event-Driven Architecture & Message Broker (RabbitMQ / Rebus)**
   - *Neden kullanıldı?* Mikroservislerin birbirleriyle asenkron konuşmasını sağlayarak, bir servisin çökmesinin diğerini etkilemesini (Single Point of Failure) önlemek için. Hata fırlayan senaryolar ve log'lar, (Graceful Degradation tolerans mekanizması sayesinde sistem engellenmeden) anında `log-queue` kuyruğuna ateşlenir ve tüketici servis (Log API) tarafından işlenir.
   
5. **Gateway Routing & Rate Limiting (YARP)**
   - *Neden kullanıldı?* Dış dünyadan (İstemciler, mobil uygulamalar, web) gelen dağınık microservice çağrılarını tek bir merkezde orkestre etmek, güvenliği sağlamak ve saniyede sınırlandırılmış istekler (Rate Limiting) uygulayarak DDoS/Abuse saldırılarını engellemek için.
   
6. **Distributed Caching (Redis)**
   - *Neden kullanıldı?* Product gibi sık listelenen ancak anlık değişme olasılığı bulunan verileri yüksek performansta sunmak için. Tüm `Insert`, `Update`, `Delete` işlemlerinde **Cache Invalidation** (önbellek temizleme) deseni uygulanarak verinin güncel kalması sağlanmıştır.

7. **Shared (Building Blocks) Altyapısı ve Soyutlamalar (Abstractions)**
   - *Neden kullanıldı?* Mikroservisler arası kod tekrarlarını (Code Duplication) önlemek ve sisteme kurumsal bir standart dayatmak için. 
   - **MediatR Abstraction & Responses:** `ICommand`, `ICommandHandler`, `IQuery`, `IQueryHandler` gibi kendi arayüzlerimiz ve `CommandHandlerBase` gibi soyut (abstract) şablon sınıflarımız yazılmış; tüm operasyonların ortak bir `BaseResponse` modeli dönmesi garanti altına alınmıştır.
   - **FluentValidation Pipelines:** Mikroservislere gelen isteklerin Controller tarafında `if (!ModelState.IsValid)` kalabalığı yaratmaması için **ValidationBehavior** pipeline yapısı kurgulanmıştır. Hatalı ve eksik istekler Handler'a asla giremeden kesilir (Short-Circuit) ve güvenli bir şekilde `ValidatorException` fırlatarak Global Error Handler'a (HTTP 400 - Bad Request) aktarılır.

---

## 🚀 Kurulum, Çalıştırma ve Dağıtım

Proje; tüm veritabanı, queue ve cache altyapılarıyla birlikte **Docker** Container ortamında tamamen izole çalışmak üzere ("out of the box") yapılandırılmıştır.

### 1- Ön Gereksinimler
- Bilgisayarınızda (veya dağıtım yapacağınız sunucuda) **Docker** ve **Docker Compose** yüklü olmalıdır.

### 2- Kurulum ve Çalıştırma (Development / Run)
Tüm altyapı hizmetlerini (Postgres DB, RabbitMQ, Redis, Seq) ve API C# servislerini aynı anda kaldırmak için:
1. Terminal üzerinden projenin bulunduğu kök dizine (`docker-compose.yml` dosyasının bulunduğu klasöre) gidin.
2. Aşağıdaki komutu çalıştırın:
   ```bash
   docker-compose up -d --build
   ```
3. Docker üzerindeki tüm konteynerlerin sağlıklı durumuna (`Running`) geçmesini bekleyin. Multi-stage build mimarisine sahip Dockerfile dosyaları ile uygulamanız derlenip ayağa kalkacaktır.

### 3- Sistemin Test Edilmesi
Tüm dış istekler Gateway (`5000` portu) üzerinden proxy edilerek aktarılmaktadır. Postman veya benzeri REST istemcilerle uygulamanızı test etmek için temel adres şudur:

- **Erişim Adresi:** `http://localhost:5000`
- **Auth Service:** `http://localhost:5000/api/auth-service/{path}`
- **Product Service:** `http://localhost:5000/api/product-service/{path}`

**Not:** Servisler arası veritabanı ve kuyruk haberleşmelerini yöneten şifreler, portlar ve host isimleri (Örn: `kayra_rabbitmq`) doğrudan `docker-compose.yml` içindeki Environment Variables (çevresel değişkenler) mekanizması ile konteyner içerisine "override" edilerek (ezilerek) dahil edilmiştir. `appsettings.json` dosyalarınıza kesinlikle dokunmanız gerekmemektedir.

### 4- Sunucu Dağıtımı (Deployment)
Projedeki 4 adet microservice (`Auth.API`, `Product.API`, `Log.API`, `Gateway.API`) standart `.NET 9 Multi-Stage Dockerfile` yapısına sahiptir. Saniyeler içerisinde derlenip (publish) küçültülmüş container'lara dönüşür. 

Canlı (Production) ortamlara sunucu dağıtımı yaparken, CI/CD süreçlerinizde (Örn. GitHub Actions) bu mevcut Dockerfile ve Compose dosyaları hiçbir bağımlılık problemi olmadan doğrudan kullanıma hazırdır. Dağıtım yapılacak sunucuda sadece `docker-compose up -d` tetiklenmesi sunucunun %100 güncel versiyonda hizmet vermesini sağlar.
