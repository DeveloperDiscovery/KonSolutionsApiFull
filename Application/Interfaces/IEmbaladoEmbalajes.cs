using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

public interface IEmbaladoEmbalajesRepository
{
    Task<IEnumerable<EmbaladoEmbalajes>> ShowallAsync();
    Task<EmbaladoEmbalajes?> ShowbyidAsync(decimal NUM_EMBALAJE);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<EmbaladoEmbalajes>> SearchAsync(EmbaladoEmbalajesSearchDto dto);
    Task<WriteResult> InsertAsync(EmbaladoEmbalajesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(EmbaladoEmbalajesUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(decimal NUM_EMBALAJE);
}

public interface IEmbaladoEmbalajesService
{
    Task<ApiResponse<IEnumerable<EmbaladoEmbalajes>>> GetAllAsync();
    Task<ApiResponse<EmbaladoEmbalajes>> GetByIdAsync(decimal NUM_EMBALAJE);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<EmbaladoEmbalajes>>> SearchAsync(EmbaladoEmbalajesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(EmbaladoEmbalajesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(EmbaladoEmbalajesUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(decimal NUM_EMBALAJE);
}