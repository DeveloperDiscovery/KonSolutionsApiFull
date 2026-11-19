using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposDisenoRepository
{
    Task<IEnumerable<TiposDiseno>> ShowallAsync();
    Task<TiposDiseno?> ShowbyidAsync(string COD_TIPO_DISENO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposDiseno>> SearchAsync(TiposDisenoSearchDto dto);
    Task<WriteResult> InsertAsync(TiposDisenoInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_TIPO_DISENO);
}

public interface ITiposDisenoService
{
    Task<ApiResponse<IEnumerable<TiposDiseno>>> GetAllAsync();
    Task<ApiResponse<TiposDiseno>> GetByIdAsync(string COD_TIPO_DISENO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposDiseno>>> SearchAsync(TiposDisenoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposDisenoInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_DISENO);
}
