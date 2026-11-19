using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Controlcalidad;

namespace KonSolutionsApi.Application.Interfaces;

public interface IClasesDefectosTecnicosRepository
{
    Task<IEnumerable<ClasesDefectosTecnicos>> ShowallAsync();
    Task<ClasesDefectosTecnicos?> ShowbyidAsync(string COD_CLASE_DEFECTO_TECNICO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ClasesDefectosTecnicos>> SearchAsync(ClasesDefectosTecnicosSearchDto dto);
    Task<WriteResult> InsertAsync(ClasesDefectosTecnicosInsertDto dto);
    Task<WriteResult> UpdateAsync(ClasesDefectosTecnicosUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_CLASE_DEFECTO_TECNICO);
}

public interface IClasesDefectosTecnicosService
{
    Task<ApiResponse<IEnumerable<ClasesDefectosTecnicos>>> GetAllAsync();
    Task<ApiResponse<ClasesDefectosTecnicos>> GetByIdAsync(string COD_CLASE_DEFECTO_TECNICO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ClasesDefectosTecnicos>>> SearchAsync(ClasesDefectosTecnicosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ClasesDefectosTecnicosInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(ClasesDefectosTecnicosUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_CLASE_DEFECTO_TECNICO);
}