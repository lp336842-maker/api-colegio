namespace Colegio.Infraestructura.Modelos;

public record EstudianteModelo(int? Id, string Nombre, string Apellido, string Correo, DateOnly FechaInscripcion);
