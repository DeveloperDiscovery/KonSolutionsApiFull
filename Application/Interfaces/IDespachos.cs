using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

public interface IDespachosRepository
{
    Task<IEnumerable<Despachos>> ShowallAsync();
    Task<Despachos?> ShowbyidAsync(int NUM_DESPACHO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<Despachos>> SearchAsync(DespachosSearchDto dto);
    Task<WriteResult> InsertAsync(DespachosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(DespachosUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_DESPACHO);
}

public interface IDespachosService
{
    Task<ApiResponse<IEnumerable<Despachos>>> GetAllAsync();
    Task<ApiResponse<Despachos>> GetByIdAsync(int NUM_DESPACHO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<Despachos>>> SearchAsync(DespachosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(DespachosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(DespachosUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_DESPACHO);
}