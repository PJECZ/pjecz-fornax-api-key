using Microsoft.AspNetCore.Mvc;
using pjecz_fornax_api_key.Models;
using Npgsql; // Required for PostgreSQL connection
using Dapper;  // Required for Dapper extension methods

namespace pjecz_fornax_api_key.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OfiDocumentosController : ControllerBase
{
    // Cargar la configuracion
    private readonly IConfiguration _configuration;
    public OfiDocumentosController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    // Obtener un listado de los documentos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<OfiDocumento>>> GetAllDocuments()
    {
        try
        {
            // Cargar la cadena de conexión desde la configuración
            var connectionString = _configuration.GetConnectionString("PostgresConnection");

            // Definir comando SQL para consultar
            var sql = "SELECT * FROM public.\"ofi_documentos\"";

            // Definir el objeto de conexión
            await using var connection = new NpgsqlConnection(connectionString);

            // Consultar
            var documentos = await connection.QueryAsync<OfiDocumento>(sql);

            // Entregar
            return Ok(documentos);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // Obtener un documento por ID
    [HttpGet("{id}")]
    public async Task<ActionResult<OfiDocumento>> GetDocumentById(int id)
    {
        try
        {
            // Cargar la cadena de conexión desde la configuración
            var connectionString = _configuration.GetConnectionString("PostgresConnection");

            // Definir comando SQL para consultar
            var sql = "SELECT * FROM public.\"OfiDocumentos\" WHERE \"Id\" = @Id";

            // Definir el objeto de conexión
            await using var connection = new NpgsqlConnection(connectionString);

            // Consultar
            var documento = await connection.QueryFirstOrDefaultAsync<OfiDocumento>(sql, new { Id = id });

            // Si no se encuentra, retornar NotFound
            if (documento == null)
            {
                return NotFound($"Documento con ID {id} no encontrado.");
            }

            // Entregar
            return Ok(documento);

        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

}
