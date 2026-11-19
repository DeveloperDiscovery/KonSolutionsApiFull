using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposUsoRepository
{
    Task<IEnumerable<TiposUso>> ShowallAsync();
    Task<TiposUso?> ShowbyidAsync(string COD_TIPO_USO);
    Task<IEnumerable<TiposUso>> ShowcomboxAsync();
    Task<WriteResult> InsertAsync(TiposUsoInsertDto dto);
    Task<WriteResult> DeleteAsync(string COD_TIPO_USO);
}

public interface ITiposUsoService
{
    Task<ApiResponse<IEnumerable<TiposUso>>> GetAllAsync();
    Task<ApiResponse<TiposUso>> GetByIdAsync(string COD_TIPO_USO);
    Task<ApiResponse<IEnumerable<TiposUso>>> GetComboboxAsync();
    Task<ApiResponse<WriteResult>> InsertAsync(TiposUsoInsertDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_USO);
}
