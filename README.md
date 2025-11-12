# 🌌 Horoscope.Recursiva.Backend

API desarrollada en **.NET 8** para gestionar y consultar información relacionada con horóscopos.  
Sigue una arquitectura en capas (**API – Application – Infrastructure – Domain**) y utiliza **Entity Framework Core** para la persistencia de datos, junto con validaciones personalizadas y buenas prácticas de desarrollo modular.

---

## 📋 Descripción

Este proyecto proporciona un servicio backend basado en **ASP.NET Core Web API**, ideal para manejar operaciones relacionadas con horóscopos, como consultas, validaciones y administración de datos.

---

## 🚀 Requisitos previos

Antes de comenzar, asegúrate de tener instaladas las siguientes herramientas:

| Herramienta | Versión recomendada | Comando para verificar |
|--------------|---------------------|------------------------|
| [.NET SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) | 8.0 o superior | `dotnet --version` |
| [SQL Server](https://www.microsoft.com/en-us/sql-server) o [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) | Última versión estable | — |
| [Visual Studio 2022](https://visualstudio.microsoft.com/) o [Visual Studio Code](https://code.visualstudio.com/) | — | — |
| [Git](https://git-scm.com/) | — | `git --version` |

---

## ⚙️ Instalación y configuración

### 1️⃣ Clonar el repositorio

```bash
git clone https://github.com/MauroPolizzi/Horoscope.Recursiva.Backend.git
```

### 2️⃣ Restaurar dependencias

Ejecuta el siguiente comando en la raíz del proyecto (donde se encuentra el archivo `.sln`):

```bash
dotnet restore
```

### 3️⃣ Configurar la base de datos

Edita el archivo **`appsettings.json`** del proyecto **Horoscope.Api** y ajusta la cadena de conexión con tus credenciales locales de SQL Server:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=HoroscopeDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

---

## 🧱 Migraciones de Entity Framework Core

### 4️⃣ Aplicar migraciones existentes

> ⚠️ Asegúrate de que el proyecto **Horoscope.Infrastructure** esté configurado como proyecto predeterminado.

Ejecuta desde la carpeta raíz del proyecto:

```bash
dotnet ef database update --startup-project Horoscope.Api --project Horoscope.Infrastructure
```

Esto creará la base de datos y aplicará todas las migraciones registradas.

---

## ▶️ Ejecución del proyecto

### 5️⃣ Ejecutar la API

```bash
dotnet run --project Horoscope.Api
```

La API se iniciará y quedará escuchando por defecto en alguno de estas url:

- https://localhost:5001  
- http://localhost:5000

---

## 📖 Probar la API

Una vez en ejecución, puedes probar los endpoints desde **Swagger UI**:

👉 [http://localhost:5001/swagger/index.html](http://localhost:5001/swagger/index.html)

---

## 🧩 Estructura del proyecto

```
Horoscope.Recursiva.Backend/
├── Horoscope.Api/             # Capa de presentación (endpoints, controladores, configuración)
├── Horoscope.Application/     # Lógica de aplicación, DTOs, validaciones, casos de uso
├── Horoscope.Domain/          # Entidades de negocio y lógica de dominio
├── Horoscope.Infrastructure/  # Acceso a datos, contexto de EF, repositorios, migraciones
└── README.md
```

---

## 🧠 Tecnologías principales

- **.NET 8 / ASP.NET Core**
- **Entity Framework Core**
- **SQL Server**
- **C# 12**
- **Swagger (Swashbuckle)** para documentación interactiva
- **Arquitectura en capas**

---

## 🤝 Contribuciones

Las contribuciones son bienvenidas.  

Para colaborar:

1. Crea un *fork* del repositorio.  
2. Crea una nueva rama:  
   ```bash
   git checkout -b feature/nueva-funcionalidad
   ```
3. Realiza tus cambios y haz commit.  
4. Abre un *Pull Request* describiendo tu aporte.

---

