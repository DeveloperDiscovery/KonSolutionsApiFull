using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

/// <summary>Tabla de asignación pura (Personal asignado a un punto de conteo): solo
/// Showall/Showbyid/Showcombox/Search + Insert(=Asignar, upsert)/Delete(=Quitar). Sin Update —
/// ver comentario en el Domain Model.</summary>
public interface IInventariosAlmacenesPuntoTomaPersonalRepository
{
    Task<IEnumerable<InventariosAlmacenesPuntoTomaPersonal>> ShowallAsync(int numInventario, string codAlmacen, byte numPuntoTomaInventario);
    Task<InventariosAlmacenesPuntoTomaPersonal?> ShowbyidAsync(int numInventario, string codAlmacen, byte numPuntoTomaInventario, decimal codEntidadAsignada);
    // OJO: pese al nombre, el SP _SHOWCOMBOX de esta tabla devuelve todas las columnas (igual
    // que Showall), no Id/Texto — se expone con el mismo tipo de retorno que ShowallAsync.
    Task<IEnumerable<InventariosAlmacenesPuntoTomaPersonal>> ShowcomboxAsync(int numInventario, string codAlmacen, byte numPuntoTomaInventario);
    Task<IEnumerable<InventariosAlmacenesPuntoTomaPersonal>> SearchAsync(InventariosAlmacenesPuntoTomaPersonalSearchDto dto);
    /// <summary>Asignar: el propio SP hace UPSERT (UPDATE de auditoría si ya existe, INSERT si no).</summary>
    Task<WriteResult> InsertAsync(InventariosAlmacenesPuntoTomaPersonalInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int numInventario, string codAlmacen, byte numPuntoTomaInventario, decimal codEntidadAsignada);
}

public interface IInventariosAlmacenesPuntoTomaPersonalService
{
    Task<ApiResponse<IEnumerable<InventariosAlmacenesPuntoTomaPersonal>>> GetAllAsync(int numInventario, string codAlmacen, byte numPuntoTomaInventario);
    Task<ApiResponse<InventariosAlmacenesPuntoTomaPersonal>> GetByIdAsync(int numInventario, string codAlmacen, byte numPuntoTomaInventario, decimal codEntidadAsignada);
    Task<ApiResponse<IEnumerable<InventariosAlmacenesPuntoTomaPersonal>>> GetComboboxAsync(int numInventario, string codAlmacen, byte numPuntoTomaInventario);
    Task<ApiResponse<IEnumerable<InventariosAlmacenesPuntoTomaPersonal>>> SearchAsync(InventariosAlmacenesPuntoTomaPersonalSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(InventariosAlmacenesPuntoTomaPersonalInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int numInventario, string codAlmacen, byte numPuntoTomaInventario, decimal codEntidadAsignada);
}
