using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Networking;

namespace KonSolutionsApi.Application.Interfaces;

public interface IGruposNetworkingRepository
{
    Task<IEnumerable<GruposNetworking>> ShowallAsync();
    Task<GruposNetworking?> ShowbyidAsync(int NUM_GRUPO_NETWORKING);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<GruposNetworking>> SearchAsync(GruposNetworkingSearchDto dto);
    Task<WriteResult> InsertAsync(GruposNetworkingInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(GruposNetworkingUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_GRUPO_NETWORKING);
}

public interface IGruposNetworkingService
{
    Task<ApiResponse<IEnumerable<GruposNetworking>>> GetAllAsync();
    Task<ApiResponse<GruposNetworking>> GetByIdAsync(int NUM_GRUPO_NETWORKING);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<GruposNetworking>>> SearchAsync(GruposNetworkingSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(GruposNetworkingInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(GruposNetworkingUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_GRUPO_NETWORKING);
}