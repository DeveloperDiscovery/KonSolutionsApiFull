using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposPersonasRepository
{
    Task<IEnumerable<TiposPersonas>> ShowallAsync();
    Task<TiposPersonas?> ShowbyidAsync(byte COD_TIPO_PERSONA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<WriteResult> InsertAsync(TiposPersonasInsertDto dto);
    Task<WriteResult> DeleteAsync(byte COD_TIPO_PERSONA);
}

public interface ITiposPersonasService
{
    Task<ApiResponse<IEnumerable<TiposPersonas>>> GetAllAsync();
    Task<ApiResponse<TiposPersonas>> GetByIdAsync(byte COD_TIPO_PERSONA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<WriteResult>> InsertAsync(TiposPersonasInsertDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(byte COD_TIPO_PERSONA);
}
