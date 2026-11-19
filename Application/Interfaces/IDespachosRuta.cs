using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

public interface IDespachosRutaRepository
{
    Task<IEnumerable<DespachosRuta>> ShowallAsync();
    Task<DespachosRuta?> ShowbyidAsync(int NUM_DESPACHO, int NUM_SECUENCIA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<DespachosRuta>> SearchAsync(DespachosRutaSearchDto dto);
    Task<WriteResult> InsertAsync(DespachosRutaInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(DespachosRutaUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_DESPACHO, int NUM_SECUENCIA);
}

public interface IDespachosRutaService
{
    Task<ApiResponse<IEnumerable<DespachosRuta>>> GetAllAsync();
    Task<ApiResponse<DespachosRuta>> GetByIdAsync(int NUM_DESPACHO, int NUM_SECUENCIA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<DespachosRuta>>> SearchAsync(DespachosRutaSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(DespachosRutaInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(DespachosRutaUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_DESPACHO, int NUM_SECUENCIA);
}