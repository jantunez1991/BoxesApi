# 📦 Boxes API

API desarrollada para el challenge técnico de ACTIF.  
Permite registrar leads (clientes potenciales) que solicitan turnos para realizar servicios en talleres.  
El sistema valida la estructura del lead y verifica contra una API externa si el `placeId` es válido.

---

## 🚀 Tecnologías utilizadas

- .NET 8
- ASP.NET Core Web API
- FluentValidation
- Swagger (Swashbuckle)
- Inyección de dependencias
- `HttpClientFactory` con autenticación básica
- Logging con `ILogger`

---

## 🔧 Cómo correr el proyecto

1. Cloná el repositorio:
   ```bash
   git clone https://github.com/jantunez1991/BoxesApi.git
   cd BoxesApi
   
✅ Ejemplo de Request

{
    "PlaceId": 2,
    "AppointmentAt": "2025-08-01T10:00:00Z",
    "ServiceType": "cambio_aceite",
    "Contact": {
        "Name": "José Antúnez",
        "Email": "jose@example.com",
        "Phone": "+5491112345678"
    },
    "Vehicle": {
        "Make": "Toyota",
        "Model": "Corolla",
        "Year": 2020,
        "LicensePlate": "ABC123"
    }
}
