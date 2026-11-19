using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Administracion;

namespace KonSolutionsApi.Application.Interfaces;

public interface ICargosRepository
{
    Task<IEnumerable<Cargos>> ShowallAsync();
    Task<Cargos?> ShowbyidAsync(int COD_CARGO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<Cargos>> SearchAsync(CargosSearchDto dto);
    Task<WriteResult> InsertAsync(CargosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(CargosUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int COD_CARGO);
}

public interface ICargosService
{
    Task<ApiResponse<IEnumerable<Cargos>>> GetAllAsync();
    Task<ApiResponse<Cargos>> GetByIdAsync(int COD_CARGO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<Cargos>>> SearchAsync(CargosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(CargosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(CargosUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int COD_CARGO);
}