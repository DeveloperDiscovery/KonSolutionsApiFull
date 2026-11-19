using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface IEstadosCivilesRepository
{
    Task<IEnumerable<EstadosCiviles>> ShowallAsync();
    Task<EstadosCiviles?> ShowbyidAsync(string COD_ESTADO_CIVIL);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<EstadosCiviles>> SearchAsync(EstadosCivilesSearchDto dto);
    Task<WriteResult> InsertAsync(EstadosCivilesInsertDto dto);
    Task<WriteResult> UpdateAsync(EstadosCivilesUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_ESTADO_CIVIL);
}

public interface IEstadosCivilesService
{
    Task<ApiResponse<IEnumerable<EstadosCiviles>>> GetAllAsync();
    Task<ApiResponse<EstadosCiviles>> GetByIdAsync(string COD_ESTADO_CIVIL);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<EstadosCiviles>>> SearchAsync(EstadosCivilesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(EstadosCivilesInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(EstadosCivilesUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_ESTADO_CIVIL);
}