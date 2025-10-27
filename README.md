# Horoscope.Recursiva.Backend

Pasos a seguir para ejecutar y probar el proyecto.

📋 Descripción
Esta es una API desarrollada con .NET 8, diseñada para manejar consultas relacionadas con horóscopos.
Incluye acceso a base de datos mediante Entity Framework Core, validaciones personalizadas y un enfoque modular siguiendo buenas prácticas de arquitectura en capas (API – Application – Infrastructure – Domain).

🚀 Requisitos previos

Antes de ejecutar el proyecto, asegurate de tener instalado lo siguiente:
| Herramienta      | Versión recomendada    | Comando para verificar |
| ------------------------------------------------------------------------------------------------------------------------------------------------ | ---------------------- | ---------------------- |
| [.NET SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)                                                                               | 8.0 o superior         | `dotnet --version`     |
| [SQL Server](https://www.microsoft.com/en-us/sql-server) o [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) | Última versión estable | —                      |
| [Visual Studio 2022](https://visualstudio.microsoft.com/) o [Visual Studio Code](https://code.visualstudio.com/)                                 | —                      | —                      |
| [Git](https://git-scm.com/)                                                                                                                      | —                      | `git --version`        |

📦 Instalación del proyecto
1️⃣ Clonar el repositorio
git clone https://github.com/tu-usuario/tu-repositorio.git
cd tu-repositorio

2️⃣ Restaurar dependencias
Desde la raíz del proyecto (donde está el archivo .sln):
dotnet restore

3️⃣ Configurar la base de datos
Ir al proyecto Horoscope.Api → archivo appsettings.json.
Modificar la cadena de conexión (ConnectionStrings) con tus credenciales locales de SQL Server.
Ejemplo:
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=HoroscopeDb;Trusted_Connection=True;TrustServerCertificate=True;"
}

🧱 Migraciones de Entity Framework

4️⃣ Aplicar migraciones existentes a la base de datos (desde Visual Studio, realizarlo desde la consola del Administrador de paquetes)
⚠️ Asegúrate de tener Horoscope.Infrastructure como proyecto de predeterminado.
Ejecuta el siguiente comando desde la carpeta raíz:
dotnet ef database update --startup-project Horoscope.Api --project Horoscope.Infrastructure
Esto creará la base de datos y aplicará todas las migraciones registradas.

▶️ Ejecución del proyecto
5️⃣ Ejecutar la API
dotnet run --project Horoscope.Api

La API se iniciará y quedará escuchando por defecto en:
https://localhost:5001
http://localhost:5000

📖 Endpoints principales
Una vez en ejecución, puedes probar los endpoints en Swagger UI:
http://localhost:5001/swagger/index.html
