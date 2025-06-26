# Docker Deployment Guide

Bu proje için Docker Compose kullanarak tüm servisleri çalıştırabilirsiniz.

## Servisler

### Altyapı Servisleri
- **SQL Server**: Port 1433 (sa kullanıcısı, şifre: sa.++112233)
- **RabbitMQ**: Port 5672 (AMQP), Port 15672 (Management UI)

### Uygulama Servisleri
- **Identity API**: Port 5001
- **Order Management API**: Port 5002
- **Payment System API**: Port 5003
- **Product Catalog API**: Port 5004
- **Shipping API**: Port 5005
- **Stock API**: Port 5006
- **Notification Worker**: Background service (port yok)

## Kullanım

### Tüm servisleri başlatmak için:
```bash
docker-compose up -d
```

### Servisleri durdurmak için:
```bash
docker-compose down
```

### Logları görüntülemek için:
```bash
# Tüm servislerin logları
docker-compose logs -f

# Belirli bir servisin logları
docker-compose logs -f identity-api
```

### Sadece altyapı servislerini başlatmak için:
```bash
docker-compose up -d sqlserver rabbitmq
```

### Rebuild için:
```bash
docker-compose up -d --build
```

## Servis Durumları

Servislerin durumunu kontrol etmek için:
```bash
docker-compose ps
```

## Database Bağlantıları

Her servis kendi veritabanını kullanır:
- IdentityDb
- OrderManagementDb
- PaymentSystemDb
- ProductCatalogDb
- ShippingDb
- StockDb
- NotificationDb

## RabbitMQ Management

RabbitMQ Management UI'ya erişim:
- URL: http://localhost:15672
- Kullanıcı: guest
- Şifre: guest

## Environment Variables

Her servis için aşağıdaki ortam değişkenleri tanımlanmıştır:
- Database connection strings
- RabbitMQ bağlantı bilgileri
- Servisler arası iletişim URL'leri

## Bağımlılıklar

Order Management API diğer tüm API'lere bağımlıdır ve onlar hazır olduktan sonra başlar.
Tüm servisler SQL Server ve RabbitMQ'nun hazır olmasını bekler.

## Troubleshooting

1. **Port çakışması**: Belirtilen portların başka servisler tarafından kullanılmadığından emin olun
2. **Memory**: SQL Server için yeterli bellek (en az 2GB) olduğundan emin olun
3. **Disk alanı**: Docker image'ları için yeterli disk alanı olduğundan emin olun

## Development

Development sırasında tek bir servisi yeniden build etmek için:
```bash
docker-compose up -d --build identity-api
```
