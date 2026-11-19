using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface IRutasArchivosRepository
{
    Task<IEnumerable<RutasArchivos>> ShowallAsync();
    Task<RutasArchivos?> ShowbyidAsync(int NUM_RUTA_ARCHIVO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<RutasArchivos>> SearchAsync(RutasArchivosSearchDto dto);
    Task<WriteResult> InsertAsync(RutasArchivosInsertDto dto);
    Task<WriteResult> UpdateAsync(RutasArchivosUpdateDto dto);
    Task<WriteResult> DeleteAsync(int NUM_RUTA_ARCHIVO);
}

public interface IRutasArchivosService
{
    Task<ApiResponse<IEnumerable<RutasArchivos>>> GetAllAsync();
    Task<ApiResponse<RutasArchivos>> GetByIdAsync(int NUM_RUTA_ARCHIVO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<RutasArchivos>>> SearchAsync(RutasArchivosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(RutasArchivosInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(RutasArchivosUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_RUTA_ARCHIVO);
}