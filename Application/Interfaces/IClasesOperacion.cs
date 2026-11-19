using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Interfaces;

public interface IClasesOperacionRepository
{
    Task<IEnumerable<ClasesOperacion>> ShowallAsync();
    Task<ClasesOperacion?> ShowbyidAsync(string COD_CLASE_OPERACION);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ClasesOperacion>> SearchAsync(ClasesOperacionSearchDto dto);
    Task<WriteResult> InsertAsync(ClasesOperacionInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(ClasesOperacionUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_CLASE_OPERACION);
    Task<ClasesOperacionLoadCombosResult> LoadcombosAsync(int codTipoEstado);
}

public interface IClasesOperacionService
{
    Task<ApiResponse<IEnumerable<ClasesOperacion>>> GetAllAsync();
    Task<ApiResponse<ClasesOperacion>> GetByIdAsync(string COD_CLASE_OPERACION);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ClasesOperacion>>> SearchAsync(ClasesOperacionSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ClasesOperacionInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(ClasesOperacionUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_CLASE_OPERACION);
    Task<ApiResponse<ClasesOperacionLoadCombosResult>> LoadcombosAsync(int codTipoEstado);
}