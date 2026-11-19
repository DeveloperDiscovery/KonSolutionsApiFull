using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

public interface IAlmacenesCierreRepository
{
    Task<IEnumerable<AlmacenesCierre>> ShowallAsync();
    Task<AlmacenesCierre?> ShowbyidAsync(int NUM_CIERRE_ALMACEN);
    Task<IEnumerable<AlmacenesCierre>> SearchAsync(AlmacenesCierreSearchDto dto);
    Task<AlmacenesCierreConsulta?> ConsultarAsync(string COD_ALMACEN, DateTime? FEC_MOVIMENTO);
    Task<WriteResult> InsertAsync(AlmacenesCierreInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(AlmacenesCierreUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> ReabrirAsync(int NUM_CIERRE_ALMACEN, AlmacenesCierreReabrirDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_CIERRE_ALMACEN);
}

public interface IAlmacenesCierreService
{
    Task<ApiResponse<IEnumerable<AlmacenesCierre>>> GetAllAsync();
    Task<ApiResponse<AlmacenesCierre>> GetByIdAsync(int NUM_CIERRE_ALMACEN);
    Task<ApiResponse<IEnumerable<AlmacenesCierre>>> SearchAsync(AlmacenesCierreSearchDto dto);
    Task<ApiResponse<AlmacenesCierreConsulta>> ConsultarAsync(string COD_ALMACEN, DateTime? FEC_MOVIMENTO);
    Task<ApiResponse<WriteResult>> InsertAsync(AlmacenesCierreInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(AlmacenesCierreUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> ReabrirAsync(int NUM_CIERRE_ALMACEN, AlmacenesCierreReabrirDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_CIERRE_ALMACEN);
}
