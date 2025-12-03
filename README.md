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

1.Base de datos

1.1 Crear la base de datos

En SQL Server Management Studio:

sql
CREATE DATABASE BookRadarDb;
GO

1.2 Crear la tabla

CREATE TABLE HistorialBusquedas (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Autor NVARCHAR(200) NOT NULL,
    Titulo NVARCHAR(500) NULL,
    AnioPublicacion INT NULL,
    Editorial NVARCHAR(300) NULL,
    FechaBusqueda DATETIME2 NOT NULL DEFAULT SYSDATETIME()
);

1.3 Crear el procedimiento almacenado
CREATE PROCEDURE sp_InsertHistorialBusqueda
    @Autor NVARCHAR(200),
    @Titulo NVARCHAR(500),
    @AnioPublicacion INT = NULL,
    @Editorial NVARCHAR(300) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1
        FROM HistorialBusquedas
        WHERE Autor = @Autor
          AND Titulo = @Titulo
          AND ISNULL(AnioPublicacion, -1) = ISNULL(@AnioPublicacion, -1)
          AND ISNULL(Editorial, '') = ISNULL(@Editorial, '')
          AND FechaBusqueda >= DATEADD(MINUTE, -1, SYSDATETIME())
    )
    BEGIN
        RETURN;
    END

    INSERT INTO HistorialBusquedas (Autor, Titulo, AnioPublicacion, Editorial, FechaBusqueda)
    VALUES (@Autor, @Titulo, @AnioPublicacion, @Editorial, SYSDATETIME());
END;

2. Clonar el repositorio.

git clone https://github.com/rubenalvarez98/BookRadar.git

3. Configurar la conexión a la base de datos desde el proyecto
	
	 En appsettings.json, ajustar el nombre del servidor SQL si es necesario:

	"ConnectionStrings": {
  "DefaultConnection": "Server=TU_SERVIDOR_SQL;Database=BookRadarDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
}



 