using Microsoft.AspNetCore.Mvc;
using pjecz_fornax_api_key.Models;
using Npgsql; // Required for PostgreSQL connection
using Dapper;  // Required for Dapper extension methods

namespace pjecz_fornax_api_key.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OfiPlantillasController : ControllerBase
{
    // Cargar la configuracion
    private readonly IConfiguration _configuration;
    public OfiPlantillasController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    // Obtener un listado de las plantillas
    [HttpGet]
    public async Task<ActionResult<IEnumerable<OfiPlantilla>>> GetAllPlantillas()
    {
        try
        {
            // Cargar la cadena de conexión desde la configuración
            var connectionString = _configuration.GetConnectionString("PostgresConnection");

            // Definir comando SQL para consultar
            var sql = "SELECT * FROM public.\"ofi_plantillas\"";

            // Definir el objeto de conexión
            await using var connection = new NpgsqlConnection(connectionString);

            // Consultar
            var plantillas = await connection.QueryAsync<OfiPlantilla>(sql);

            // Entregar
            return Ok(plantillas);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

}
