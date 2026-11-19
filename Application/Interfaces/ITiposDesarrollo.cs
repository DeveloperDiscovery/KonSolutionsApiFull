using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposDesarrolloRepository
{
    Task<IEnumerable<TiposDesarrollo>> ShowallAsync(string codClaseDesarrollo);
    Task<TiposDesarrollo?> ShowbyidAsync(string COD_CLASE_DESARROLLO, string COD_TIPO_DESARROLLO);
    Task<IEnumerable<TiposDesarrollo>> ShowcomboxAsync(string codClaseDesarrollo);
    Task<IEnumerable<TiposDesarrollo>> SearchAsync(string codClaseDesarrollo, TiposDesarrolloSearchDto dto);
    Task<WriteResult> InsertAsync(TiposDesarrolloInsertDto dto);
    Task<WriteResult> DeleteAsync(string COD_CLASE_DESARROLLO, string COD_TIPO_DESARROLLO);
}

public interface ITiposDesarrolloService
{
    Task<ApiResponse<IEnumerable<TiposDesarrollo>>> GetAllAsync(string codClaseDesarrollo);
    Task<ApiResponse<TiposDesarrollo>> GetByIdAsync(string COD_CLASE_DESARROLLO, string COD_TIPO_DESARROLLO);
    Task<ApiResponse<IEnumerable<TiposDesarrollo>>> GetComboboxAsync(string codClaseDesarrollo);
    Task<ApiResponse<IEnumerable<TiposDesarrollo>>> SearchAsync(string codClaseDesarrollo, TiposDesarrolloSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposDesarrolloInsertDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_CLASE_DESARROLLO, string COD_TIPO_DESARROLLO);
}
