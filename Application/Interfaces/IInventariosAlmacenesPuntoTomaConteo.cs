using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

public interface IInventariosAlmacenesPuntoTomaConteoRepository
{
    Task<IEnumerable<InventariosAlmacenesPuntoTomaConteo>> ShowallAsync(int numInventario, string codAlmacen, byte numPuntoTomaInventario);
    Task<InventariosAlmacenesPuntoTomaConteo?> ShowbyidAsync(int numInventario, string codAlmacen, byte numPuntoTomaInventario, int numSecuencia);
    // OJO: pese al nombre, el SP _SHOWCOMBOX de esta tabla devuelve todas las columnas (igual
    // que Showall), no Id/Texto — se expone con el mismo tipo de retorno que ShowallAsync.
    Task<IEnumerable<InventariosAlmacenesPuntoTomaConteo>> ShowcomboxAsync(int numInventario, string codAlmacen, byte numPuntoTomaInventario);
    Task<IEnumerable<InventariosAlmacenesPuntoTomaConteo>> SearchAsync(InventariosAlmacenesPuntoTomaConteoSearchDto dto);
    Task<WriteResult> InsertAsync(InventariosAlmacenesPuntoTomaConteoInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(InventariosAlmacenesPuntoTomaConteoUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int numInventario, string codAlmacen, byte numPuntoTomaInventario, int numSecuencia);
    Task<InventariosAlmacenesPuntoTomaReadRfidCodebarResult?> ReadRfidCodebarAsync(string? codStickerUbicacion, string? codStickerRfid);
}

public interface IInventariosAlmacenesPuntoTomaConteoService
{
    Task<ApiResponse<IEnumerable<InventariosAlmacenesPuntoTomaConteo>>> GetAllAsync(int numInventario, string codAlmacen, byte numPuntoTomaInventario);
    Task<ApiResponse<InventariosAlmacenesPuntoTomaConteo>> GetByIdAsync(int numInventario, string codAlmacen, byte numPuntoTomaInventario, int numSecuencia);
    Task<ApiResponse<IEnumerable<InventariosAlmacenesPuntoTomaConteo>>> GetComboboxAsync(int numInventario, string codAlmacen, byte numPuntoTomaInventario);
    Task<ApiResponse<IEnumerable<InventariosAlmacenesPuntoTomaConteo>>> SearchAsync(InventariosAlmacenesPuntoTomaConteoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(InventariosAlmacenesPuntoTomaConteoInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(InventariosAlmacenesPuntoTomaConteoUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int numInventario, string codAlmacen, byte numPuntoTomaInventario, int numSecuencia);
    Task<ApiResponse<InventariosAlmacenesPuntoTomaReadRfidCodebarResult>> ReadRfidCodebarAsync(string? codStickerUbicacion, string? codStickerRfid);
}
