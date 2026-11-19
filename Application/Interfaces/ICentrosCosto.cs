using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Contabilidad;

namespace KonSolutionsApi.Application.Interfaces;

public interface ICentrosCostoRepository
{
    Task<IEnumerable<CentrosCosto>> ShowallAsync();
    Task<CentrosCosto?> ShowbyidAsync(string COD_CENTRO_COSTO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<CentrosCosto>> SearchAsync(CentrosCostoSearchDto dto);
    Task<WriteResult> InsertAsync(CentrosCostoInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_CENTRO_COSTO);
}

public interface ICentrosCostoService
{
    Task<ApiResponse<IEnumerable<CentrosCosto>>> GetAllAsync();
    Task<ApiResponse<CentrosCosto>> GetByIdAsync(string COD_CENTRO_COSTO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<CentrosCosto>>> SearchAsync(CentrosCostoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(CentrosCostoInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_CENTRO_COSTO);
}