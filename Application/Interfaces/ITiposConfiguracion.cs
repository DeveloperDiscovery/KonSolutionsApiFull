using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposConfiguracionRepository
{
    Task<IEnumerable<TiposConfiguracion>> ShowallAsync();
    Task<TiposConfiguracion?> ShowbyidAsync(string COD_TIPO_CONFIGURACION);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<WriteResult> InsertAsync(TiposConfiguracionInsertDto dto);
    Task<WriteResult> UpdateAsync(TiposConfiguracionUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_TIPO_CONFIGURACION);
}

public interface ITiposConfiguracionService
{
    Task<ApiResponse<IEnumerable<TiposConfiguracion>>> GetAllAsync();
    Task<ApiResponse<TiposConfiguracion>> GetByIdAsync(string COD_TIPO_CONFIGURACION);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<WriteResult>> InsertAsync(TiposConfiguracionInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposConfiguracionUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_CONFIGURACION);
}
