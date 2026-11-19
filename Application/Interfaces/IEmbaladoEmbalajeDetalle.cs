using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

public interface IEmbaladoEmbalajeDetalleRepository
{
    Task<IEnumerable<EmbaladoEmbalajeDetalle>> ShowallAsync();
    Task<EmbaladoEmbalajeDetalle?> ShowbyidAsync(decimal NUM_EMBALADO, int NUM_SECUENCIA_EMBALAJE);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<EmbaladoEmbalajeDetalle>> SearchAsync(EmbaladoEmbalajeDetalleSearchDto dto);
    Task<WriteResult> InsertAsync(EmbaladoEmbalajeDetalleInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(EmbaladoEmbalajeDetalleUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(decimal NUM_EMBALADO, int NUM_SECUENCIA_EMBALAJE);
}

public interface IEmbaladoEmbalajeDetalleService
{
    Task<ApiResponse<IEnumerable<EmbaladoEmbalajeDetalle>>> GetAllAsync();
    Task<ApiResponse<EmbaladoEmbalajeDetalle>> GetByIdAsync(decimal NUM_EMBALADO, int NUM_SECUENCIA_EMBALAJE);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<EmbaladoEmbalajeDetalle>>> SearchAsync(EmbaladoEmbalajeDetalleSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(EmbaladoEmbalajeDetalleInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(EmbaladoEmbalajeDetalleUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(decimal NUM_EMBALADO, int NUM_SECUENCIA_EMBALAJE);
}