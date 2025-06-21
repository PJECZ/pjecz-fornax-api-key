namespace pjecz_fornax_api_key.Models;

public class OfiPlantilla
{
    public int Id { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public bool EstaArchivado { get; set; } = false;
    public string? ContenidoHtml { get; set; }
    public string? ContenidoMd { get; set; }
    public string? ContenidoSftd { get; set; }
    public string Estatus { get; set; } = "A";

}
