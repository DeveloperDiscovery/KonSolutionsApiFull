using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.Application.Interfaces;

public interface IEspecificacionesTecnicasItemsRepository
{
    Task<IEnumerable<EspecificacionesTecnicasItems>> ShowallAsync();
    Task<EspecificacionesTecnicasItems?> ShowbyidAsync(string COD_ESPECIFICACION_TECNICA, int COD_ITEM_TECNICO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<EspecificacionesTecnicasItems>> SearchAsync(EspecificacionesTecnicasItemsSearchDto dto);
    Task<WriteResult> InsertAsync(EspecificacionesTecnicasItemsInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(EspecificacionesTecnicasItemsUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_ESPECIFICACION_TECNICA, int COD_ITEM_TECNICO);
}

public interface IEspecificacionesTecnicasItemsService
{
    Task<ApiResponse<IEnumerable<EspecificacionesTecnicasItems>>> GetAllAsync();
    Task<ApiResponse<EspecificacionesTecnicasItems>> GetByIdAsync(string COD_ESPECIFICACION_TECNICA, int COD_ITEM_TECNICO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<EspecificacionesTecnicasItems>>> SearchAsync(EspecificacionesTecnicasItemsSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(EspecificacionesTecnicasItemsInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(EspecificacionesTecnicasItemsUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_ESPECIFICACION_TECNICA, int COD_ITEM_TECNICO);
}