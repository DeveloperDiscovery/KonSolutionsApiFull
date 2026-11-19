using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposImplementoLaboralRepository
{
    Task<IEnumerable<TiposImplementoLaboral>> ShowallAsync();
    Task<TiposImplementoLaboral?> ShowbyidAsync(string COD_TIPO_IMPLEMENTO_LABORAL);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposImplementoLaboral>> SearchAsync(TiposImplementoLaboralSearchDto dto);
    Task<WriteResult> InsertAsync(TiposImplementoLaboralInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(TiposImplementoLaboralUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_TIPO_IMPLEMENTO_LABORAL);
}

public interface ITiposImplementoLaboralService
{
    Task<ApiResponse<IEnumerable<TiposImplementoLaboral>>> GetAllAsync();
    Task<ApiResponse<TiposImplementoLaboral>> GetByIdAsync(string COD_TIPO_IMPLEMENTO_LABORAL);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposImplementoLaboral>>> SearchAsync(TiposImplementoLaboralSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposImplementoLaboralInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposImplementoLaboralUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_IMPLEMENTO_LABORAL);
}