# API de gestión del colegio

Solución en .NET 10 organizada en tres proyectos: `Colegio.Dominio`, `Colegio.Infraestructura` y `Colegio.Api`. Incluye entidades para cursos, departamentos, estudiantes y docentes, repositorios, modelos DTO y contexto de Entity Framework Core. SQLite crea `colegio-local.db` al iniciar y agrega el departamento inicial `General`.

## Iniciar

Desde la carpeta que contiene `Colegio.slnx`:

```powershell
dotnet run --project Colegio.Api --launch-profile http
```

El perfil HTTP abre Swagger automáticamente en [http://localhost:5129/swagger](http://localhost:5129/swagger). La especificación JSON está disponible en [http://localhost:5129/swagger/v1/swagger.json](http://localhost:5129/swagger/v1/swagger.json). La raíz [http://localhost:5129](http://localhost:5129) también redirige a Swagger.

Swagger UI permite consultar los esquemas de entrada y salida y ejecutar las operaciones de la API directamente desde el navegador. La configuración del documento, título y versión se encuentra en `Colegio.Api/Program.cs`; el paquete `Swashbuckle.AspNetCore` proporciona el generador, la ruta JSON y la interfaz Swagger UI.

## Rutas

- `/api/cursos`
- `/api/departamentos`
- `/api/estudiantes`
- `/api/docentes`

Cada recurso ofrece `GET` de lista y detalle, `POST`, `PUT` y `DELETE`. Los campos de entrada están en español. Para crear cursos, utiliza el `departamentoId` de un departamento existente.
