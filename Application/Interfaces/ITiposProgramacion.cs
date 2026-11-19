using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Planeamiento;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposProgramacionRepository
{
    Task<IEnumerable<TiposProgramacion>> ShowallAsync();
    Task<TiposProgramacion?> ShowbyidAsync(string COD_TIPO_PROGRAMACION);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposProgramacion>> SearchAsync(TiposProgramacionSearchDto dto);
    Task<WriteResult> InsertAsync(TiposProgramacionInsertDto dto);
    Task<WriteResult> UpdateAsync(TiposProgramacionUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_TIPO_PROGRAMACION);
}

public interface ITiposProgramacionService
{
    Task<ApiResponse<IEnumerable<TiposProgramacion>>> GetAllAsync();
    Task<ApiResponse<TiposProgramacion>> GetByIdAsync(string COD_TIPO_PROGRAMACION);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposProgramacion>>> SearchAsync(TiposProgramacionSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposProgramacionInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposProgramacionUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_PROGRAMACION);
}