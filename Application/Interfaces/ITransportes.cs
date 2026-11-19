using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITransportesRepository
{
    Task<IEnumerable<Transportes>> ShowallAsync();
    Task<Transportes?> ShowbyidAsync(string COD_TRANSPORTE);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<Transportes>> SearchAsync(TransportesSearchDto dto);
    Task<WriteResult> InsertAsync(TransportesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(TransportesUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_TRANSPORTE);
}

public interface ITransportesService
{
    Task<ApiResponse<IEnumerable<Transportes>>> GetAllAsync();
    Task<ApiResponse<Transportes>> GetByIdAsync(string COD_TRANSPORTE);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<Transportes>>> SearchAsync(TransportesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TransportesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(TransportesUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TRANSPORTE);
}