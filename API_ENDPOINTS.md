# KayraExport Microservices - API Test Endpoints

Bu doküman, sistem Docker üzerinden (veya localde) çalışırken **API Gateway (PORT: 5000)** üzerinden mikroservislere gönderilebilecek örnek `HTTP` isteklerini içermektedir. Postman veya benzeri bir REST Client ile bu yapıları kullanarak test işlemlerini kolayca gerçekleştirebilirsiniz.

---

## 🛍️ Product Servisi İstekleri

Ürün (Product) servisi Gateway içerisinde `/api/product-service/` rotasıyla aktarılmaktadır. Redis Cache ve RabbitMQ Event altyapısını doğrudan tetikler.

### 1- Tüm Ürünleri Listele (GET)
**URL:** `GET http://localhost:5000/api/product-service/products`
*Cache (Redis) entegrasyonuyla desteklenmekte olan liste çekme işlemi.*

### 2- Yeni Ürün Ekle (POST)
**URL:** `POST http://localhost:5000/api/product-service/products`
**Headers:** `Content-Type: application/json`
**Body (JSON):**
```json
{
  "name": "Kablosuz Kulaklık",
  "description": "Gürültü engelleyici özellikli yeni nesil kulaklık",
  "price": 2450.99,
  "stockQuantity": 150
}
```

### 3- Ürün Güncelle (PUT)
**URL:** `PUT http://localhost:5000/api/product-service/products/1`
**Headers:** `Content-Type: application/json`
**Authorization:** `Bearer Token (JWT accessToken)`
**Body (JSON):**
```json
{
  "name": "Kablosuz Kulaklık Pro",
  "description": "Gürültü engelleyici özellikli yeni nesil kulaklık (Pro versiyon)",
  "price": 3150.00,
  "stockQuantity": 120
}
```

### 4- Ürün Sil (DELETE)
**URL:** `DELETE http://localhost:5000/api/product-service/products/1`
*(Silinecek ürünün ID numarası doğrudan rota /products/{id} üzerinden iletilmelidir)*

---

## 🛡️ Auth Servisi İstekleri

Kullanıcı kaydı ve JWT Yetkilendirme (Authentication) servisine Gateway üzerinden `/api/auth-service/` rotasıyla erişilir.

### 1- Kullanıcı Kaydı / Register (POST)
**URL:** `POST http://localhost:5000/api/auth-service/auth/register`
**Headers:** `Content-Type: application/json`
**Body (JSON):**
```json
{
  "firstName": "Emir",
  "lastName": "Şen",
  "email": "emir@kayraexport.com",
  "password": "GüçlüSifre123!"
}
```

### 2- Kullanıcı Girişi / Login (POST)
**URL:** `POST http://localhost:5000/api/auth-service/auth/login`
**Headers:** `Content-Type: application/json`
**Body (JSON):**
```json
{
  "email": "emir@kayraexport.com",
  "password": "GüçlüSifre123!"
}
```

---

> **🔑 JWT (Bearer Token) Kullanımı İpucu:**
> Login isteğinden başarıyla dönen `accessToken` değerini, dışarıya kapalı olan (örneğin Ürün Ekleme/Güncelleme uç noktasına ait olan) istekleri Postman ile gönderirken **Authorization** sekmesine geçip **Type: Bearer Token** alanına yapıştırmalısınız. Gateway `5000` üzerinden gelen token'ı şeffaf ve güvenli bir şekilde arkadaki Product servisine iletecektir. 
