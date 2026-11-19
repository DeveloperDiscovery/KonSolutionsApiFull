using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Networking;

namespace KonSolutionsApi.Application.Interfaces;

public interface IGruposNetworkingAsesoresComerialesRepository
{
    Task<IEnumerable<GruposNetworkingAsesoresComeriales>> ShowallAsync();
    Task<GruposNetworkingAsesoresComeriales?> ShowbyidAsync(int NUM_GRUPO_NETWORKING, decimal COD_ENTIDAD_ASESOR_COMERCIAL);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<GruposNetworkingAsesoresComeriales>> SearchAsync(GruposNetworkingAsesoresComerialesSearchDto dto);
    Task<WriteResult> InsertAsync(GruposNetworkingAsesoresComerialesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(GruposNetworkingAsesoresComerialesUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_GRUPO_NETWORKING, decimal COD_ENTIDAD_ASESOR_COMERCIAL);
}

public interface IGruposNetworkingAsesoresComerialesService
{
    Task<ApiResponse<IEnumerable<GruposNetworkingAsesoresComeriales>>> GetAllAsync();
    Task<ApiResponse<GruposNetworkingAsesoresComeriales>> GetByIdAsync(int NUM_GRUPO_NETWORKING, decimal COD_ENTIDAD_ASESOR_COMERCIAL);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<GruposNetworkingAsesoresComeriales>>> SearchAsync(GruposNetworkingAsesoresComerialesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(GruposNetworkingAsesoresComerialesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(GruposNetworkingAsesoresComerialesUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_GRUPO_NETWORKING, decimal COD_ENTIDAD_ASESOR_COMERCIAL);
}