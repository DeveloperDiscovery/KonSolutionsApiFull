using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

public interface IInventariosAlmacenesPuntoTomaRepository
{
    Task<IEnumerable<InventariosAlmacenesPuntoToma>> ShowallAsync(int numInventario, string codAlmacen);
    Task<InventariosAlmacenesPuntoToma?> ShowbyidAsync(int numInventario, string codAlmacen, byte numPuntoTomaInventario);
    // OJO: pese al nombre, el SP _SHOWCOMBOX de esta tabla devuelve todas las columnas (igual
    // que Showall), no Id/Texto — se expone con el mismo tipo de retorno que ShowallAsync.
    Task<IEnumerable<InventariosAlmacenesPuntoToma>> ShowcomboxAsync(int numInventario, string codAlmacen);
    Task<IEnumerable<InventariosAlmacenesPuntoToma>> SearchAsync(InventariosAlmacenesPuntoTomaSearchDto dto);
    Task<WriteResult> InsertAsync(InventariosAlmacenesPuntoTomaInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(InventariosAlmacenesPuntoTomaUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int numInventario, string codAlmacen, byte numPuntoTomaInventario);
}

public interface IInventariosAlmacenesPuntoTomaService
{
    Task<ApiResponse<IEnumerable<InventariosAlmacenesPuntoToma>>> GetAllAsync(int numInventario, string codAlmacen);
    Task<ApiResponse<InventariosAlmacenesPuntoToma>> GetByIdAsync(int numInventario, string codAlmacen, byte numPuntoTomaInventario);
    Task<ApiResponse<IEnumerable<InventariosAlmacenesPuntoToma>>> GetComboboxAsync(int numInventario, string codAlmacen);
    Task<ApiResponse<IEnumerable<InventariosAlmacenesPuntoToma>>> SearchAsync(InventariosAlmacenesPuntoTomaSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(InventariosAlmacenesPuntoTomaInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(InventariosAlmacenesPuntoTomaUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int numInventario, string codAlmacen, byte numPuntoTomaInventario);
}
