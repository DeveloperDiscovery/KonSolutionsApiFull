using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposLecturaRepository
{
    Task<IEnumerable<TiposLectura>> ShowallAsync();
    Task<TiposLectura?> ShowbyidAsync(byte COD_TIPO_LECTURA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<WriteResult> InsertAsync(TiposLecturaInsertDto dto);
    Task<WriteResult> UpdateAsync(TiposLecturaUpdateDto dto);
    Task<WriteResult> DeleteAsync(byte COD_TIPO_LECTURA);
}

public interface ITiposLecturaService
{
    Task<ApiResponse<IEnumerable<TiposLectura>>> GetAllAsync();
    Task<ApiResponse<TiposLectura>> GetByIdAsync(byte COD_TIPO_LECTURA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<WriteResult>> InsertAsync(TiposLecturaInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposLecturaUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(byte COD_TIPO_LECTURA);
}
