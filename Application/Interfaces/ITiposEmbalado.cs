using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposEmbaladoRepository
{
    Task<IEnumerable<TiposEmbalado>> ShowallAsync();
    Task<TiposEmbalado?> ShowbyidAsync(string COD_TIPO_EMBALADO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposEmbalado>> SearchAsync(TiposEmbaladoSearchDto dto);
    Task<WriteResult> InsertAsync(TiposEmbaladoInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(TiposEmbaladoUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_TIPO_EMBALADO);
}

public interface ITiposEmbaladoService
{
    Task<ApiResponse<IEnumerable<TiposEmbalado>>> GetAllAsync();
    Task<ApiResponse<TiposEmbalado>> GetByIdAsync(string COD_TIPO_EMBALADO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposEmbalado>>> SearchAsync(TiposEmbaladoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposEmbaladoInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposEmbaladoUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_EMBALADO);
}