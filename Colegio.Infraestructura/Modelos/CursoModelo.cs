namespace Colegio.Infraestructura.Modelos;

public record CursoModelo(int? Id, string Codigo, string Nombre, int Creditos, int DepartamentoId, string? NombreDepartamento);
