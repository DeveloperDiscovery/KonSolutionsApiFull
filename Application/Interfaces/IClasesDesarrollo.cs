using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.Application.Interfaces;

public interface IClasesDesarrolloRepository
{
    Task<IEnumerable<ClasesDesarrollo>> ShowallAsync();
    Task<ClasesDesarrollo?> ShowbyidAsync(string COD_CLASE_DESARROLLO);
    Task<IEnumerable<ClasesDesarrollo>> ShowcomboxAsync();
    Task<WriteResult> InsertAsync(ClasesDesarrolloInsertDto dto);
    Task<WriteResult> DeleteAsync(string COD_CLASE_DESARROLLO);
}

public interface IClasesDesarrolloService
{
    Task<ApiResponse<IEnumerable<ClasesDesarrollo>>> GetAllAsync();
    Task<ApiResponse<ClasesDesarrollo>> GetByIdAsync(string COD_CLASE_DESARROLLO);
    Task<ApiResponse<IEnumerable<ClasesDesarrollo>>> GetComboboxAsync();
    Task<ApiResponse<WriteResult>> InsertAsync(ClasesDesarrolloInsertDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_CLASE_DESARROLLO);
}
