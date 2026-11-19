using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface ICargosImplementosRepository
{
    Task<IEnumerable<CargosImplementos>> ShowallAsync();
    Task<CargosImplementos?> ShowbyidAsync(int COD_CARGO, string COD_PRODUCTO_IMPLEMENTO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<CargosImplementos>> SearchAsync(CargosImplementosSearchDto dto);
    Task<WriteResult> InsertAsync(CargosImplementosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(CargosImplementosUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int COD_CARGO, string COD_PRODUCTO_IMPLEMENTO);
}

public interface ICargosImplementosService
{
    Task<ApiResponse<IEnumerable<CargosImplementos>>> GetAllAsync();
    Task<ApiResponse<CargosImplementos>> GetByIdAsync(int COD_CARGO, string COD_PRODUCTO_IMPLEMENTO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<CargosImplementos>>> SearchAsync(CargosImplementosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(CargosImplementosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(CargosImplementosUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int COD_CARGO, string COD_PRODUCTO_IMPLEMENTO);
}