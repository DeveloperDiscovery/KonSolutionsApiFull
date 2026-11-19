using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.Application.Interfaces;

public interface IDisenosSectoresRepository
{
    Task<IEnumerable<DisenosSectores>> ShowallAsync();
    Task<DisenosSectores?> ShowbyidAsync(string COD_DISENO, int NUM_VERSION, string COD_SECTOR_PRODUCTO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<DisenosSectores>> SearchAsync(DisenosSectoresSearchDto dto);
    Task<WriteResult> InsertAsync(DisenosSectoresInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(DisenosSectoresUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_DISENO, int NUM_VERSION, string COD_SECTOR_PRODUCTO);
}

public interface IDisenosSectoresService
{
    Task<ApiResponse<IEnumerable<DisenosSectores>>> GetAllAsync();
    Task<ApiResponse<DisenosSectores>> GetByIdAsync(string COD_DISENO, int NUM_VERSION, string COD_SECTOR_PRODUCTO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<DisenosSectores>>> SearchAsync(DisenosSectoresSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(DisenosSectoresInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(DisenosSectoresUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_DISENO, int NUM_VERSION, string COD_SECTOR_PRODUCTO);
}