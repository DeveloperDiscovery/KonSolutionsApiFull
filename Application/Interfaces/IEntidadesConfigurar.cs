using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;

namespace KonSolutionsApi.Application.Interfaces;

public interface IEntidadesConfigurarRepository
{
    /// <summary>El SP también hace el alta automática de columnas COD_ENTIDAD% nuevas antes
    /// de devolver el listado — no es un SELECT puro.</summary>
    Task<IEnumerable<EntidadesConfigurar>> ShowallAsync();
    Task<EntidadesConfigurar?> ShowbyidAsync(string desTable, string desColumn);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(string desTable);
    Task<IEnumerable<EntidadesConfigurar>> SearchAsync(EntidadesConfigurarSearchDto dto);
    Task<WriteResult> InsertAsync(EntidadesConfigurarInsertDto dto);
    Task<WriteResult> UpdateAsync(EntidadesConfigurarUpdateDto dto);
    Task<WriteResult> DeleteAsync(string desTable, string desColumn);
}

public interface IEntidadesConfigurarService
{
    Task<ApiResponse<IEnumerable<EntidadesConfigurar>>> GetAllAsync();
    Task<ApiResponse<EntidadesConfigurar>> GetByIdAsync(string desTable, string desColumn);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(string desTable);
    Task<ApiResponse<IEnumerable<EntidadesConfigurar>>> SearchAsync(EntidadesConfigurarSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(EntidadesConfigurarInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(EntidadesConfigurarUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string desTable, string desColumn);
}
