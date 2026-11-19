using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Planeamiento;

namespace KonSolutionsApi.Application.Interfaces;

public interface IProgramacionLineasProduccionRepository
{
    Task<IEnumerable<ProgramacionLineasProduccion>> ShowallAsync();
    Task<ProgramacionLineasProduccion?> ShowbyidAsync(DateTime FEC_PROGRAMACION, decimal COD_ENTIDAD);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ProgramacionLineasProduccion>> SearchAsync(ProgramacionLineasProduccionSearchDto dto);
    Task<WriteResult> InsertAsync(ProgramacionLineasProduccionInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(ProgramacionLineasProduccionUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(DateTime FEC_PROGRAMACION, decimal COD_ENTIDAD);
}

public interface IProgramacionLineasProduccionService
{
    Task<ApiResponse<IEnumerable<ProgramacionLineasProduccion>>> GetAllAsync();
    Task<ApiResponse<ProgramacionLineasProduccion>> GetByIdAsync(DateTime FEC_PROGRAMACION, decimal COD_ENTIDAD);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ProgramacionLineasProduccion>>> SearchAsync(ProgramacionLineasProduccionSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ProgramacionLineasProduccionInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(ProgramacionLineasProduccionUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(DateTime FEC_PROGRAMACION, decimal COD_ENTIDAD);
}