using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposEstadosRepository
{
    Task<IEnumerable<TiposEstados>> ShowallAsync();
    Task<TiposEstados?> ShowbyidAsync(int COD_TIPO_ESTADO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposEstados>> SearchAsync(TiposEstadosSearchDto dto);
    Task<WriteResult> InsertAsync(TiposEstadosInsertDto dto);
    Task<WriteResult> UpdateAsync(TiposEstadosUpdateDto dto);
    Task<WriteResult> DeleteAsync(int COD_TIPO_ESTADO);
}

public interface ITiposEstadosService
{
    Task<ApiResponse<IEnumerable<TiposEstados>>> GetAllAsync();
    Task<ApiResponse<TiposEstados>> GetByIdAsync(int COD_TIPO_ESTADO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposEstados>>> SearchAsync(TiposEstadosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposEstadosInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposEstadosUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(int COD_TIPO_ESTADO);
}