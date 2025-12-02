BookRadar  
Aplicación web desarrollada en **ASP.NET Core MVC (.NET 8)** que permite:

- Buscar libros por nombre de **autor** usando la API pública de **Open Library**  
- Mostrar los resultados en una vista Razor  
- Guardar los resultados en una base de datos SQL Server  
- Evitar registros duplicados en un período de 1 minuto  
- Mostrar un historial de búsquedas previas  

Incluye buenas prácticas:  
✔ Entity Framework Core  
✔ Stored Procedures  
✔ Validación en frontend con Razor  
✔ Bootstrap 5 para interfaz moderna  
✔ Arquitectura MVC limpia  

---

Tecnologías utilizadas

- **ASP.NET Core MVC (.NET 8)**
- **Entity Framework Core 8**
- **SQL Server 2019+**
- **Bootstrap 5.3**
- **C# 12**
- **Open Library API**:  
  `https://openlibrary.org/search.json?author={nombreAutor}`

---

Requisitos para ejecutar el proyecto

Antes de comenzar, necesitas:

- ✔ Visual Studio 2022 (versión compatible con .NET 8)  
- ✔ SQL Server o SQL Express  
- ✔ SQL Server Management Studio (SSMS)  
- ✔ .NET 8 SDK  
- ✔ Conexión a Internet (para consumir la API)

---

Base de datos

Crear la base de datos

En SQL Server Management Studio:

sql
CREATE DATABASE BookRadarDb;
GO