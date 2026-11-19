using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface IUnidadesComercialesRepository
{
    Task<IEnumerable<UnidadesComerciales>> ShowallAsync();
    Task<UnidadesComerciales?> ShowbyidAsync(int NUM_UNIDAD_COMERCIAL);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<UnidadesComerciales>> SearchAsync(UnidadesComercialesSearchDto dto);
    Task<WriteResult> InsertAsync(UnidadesComercialesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(UnidadesComercialesUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_UNIDAD_COMERCIAL);
}

public interface IUnidadesComercialesService
{
    Task<ApiResponse<IEnumerable<UnidadesComerciales>>> GetAllAsync();
    Task<ApiResponse<UnidadesComerciales>> GetByIdAsync(int NUM_UNIDAD_COMERCIAL);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<UnidadesComerciales>>> SearchAsync(UnidadesComercialesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(UnidadesComercialesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(UnidadesComercialesUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_UNIDAD_COMERCIAL);
}