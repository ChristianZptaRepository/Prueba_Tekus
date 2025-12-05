Proyecto TEKUS S.A.S - Plataforma de Gestión de Proveedores y Servicios

Este proyecto implementa una solución full-stack para la gestión de proveedores, servicios y análisis de datos, siguiendo los principios de la Arquitectura Limpia (Clean Architecture) con DDD/CQRS en el backend y Angular en el frontend.

Requisitos 
Antes de ejecutar el proyecto, debemos de tener instalado los siguientes software's:

Backend: .NET 8 SDK
Base de Datos: SQL Server 2019/2022
Frontend: Node.js (versión LTS) y npm (o yarn/pnpm).
Angular CLI

1. Configuración del Backend (.NET API)
El Backend esta en la carpeta Tekus.API y utiliza EF Core

1.1. Configuración de la Cadena de Conexión
Tenemos que abrir el archivo Tekus.API/appsettings.json.
Buscamos la sección ConnectionStrings y reemplazamos el valor de DefaultConnection con tu cadena de conexión a SQL Server. En Mi Caso Utilize la Autenticacion de Windows,Si estas usando Otro Tipo de Autenticación, Simplemente hay que editar el DefaultConnection

JSON

"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=YOUR_DB_NAME;User Id=YOUR_USER;Password=YOUR_PASSWORD;TrustServerCertificate=True;MultipleActiveResultSets=true;"
},

Nota: En Mi Caso y el proyecto actual lo tiene de esta manera: "Server=(localdb)\\mssqllocaldb;Database=TekusDB;Trusted_Connection=True;"

1.2. Inicialización de la Base de Datos

Luego de que tengamos la conexión a base de datos, podemos ejecutar el Script que encontramos en el proyecto Scripts/SQLQuery_BD_Tekus.sql

Este Script crea la base de datos,y las tablas necesarias con sus correspondientes relaciones. Además inserta 10 datos de prueba por cada tabla

1.3 Ejecución del Proyecto

Una vez tengamos toda la configuracion terminada,podemos limpiar la solución y ejecutarla. Debemos de revisar por si de pronto los puertos cambiaron para modificarlos en el proyecto del FrontEnd
http://localhost:[Puerto]/swagger

Fuentes Utilizadas Para El Desarrollo del BackEnd

- ASP.NET Core (General)	https://learn.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core
- Entity Framework Core	https://learn.microsoft.com/en-us/ef/core/
- CQRS & MediatR	https://github.com/jbogard/MediatR/wiki
- Arquitectura Limpia/DDD	https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-with-aspnet-core/
