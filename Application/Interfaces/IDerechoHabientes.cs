using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface IDerechoHabientesRepository
{
    Task<IEnumerable<DerechoHabientes>> ShowallAsync();
    Task<DerechoHabientes?> ShowbyidAsync(decimal COD_ENTIDAD_PERSONAL, decimal COD_DERECHO_HABIENTE);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<DerechoHabientes>> SearchAsync(DerechoHabientesSearchDto dto);
    Task<WriteResult> InsertAsync(DerechoHabientesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(DerechoHabientesUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(decimal COD_ENTIDAD_PERSONAL, decimal COD_DERECHO_HABIENTE);
}

public interface IDerechoHabientesService
{
    Task<ApiResponse<IEnumerable<DerechoHabientes>>> GetAllAsync();
    Task<ApiResponse<DerechoHabientes>> GetByIdAsync(decimal COD_ENTIDAD_PERSONAL, decimal COD_DERECHO_HABIENTE);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<DerechoHabientes>>> SearchAsync(DerechoHabientesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(DerechoHabientesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(DerechoHabientesUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(decimal COD_ENTIDAD_PERSONAL, decimal COD_DERECHO_HABIENTE);
}