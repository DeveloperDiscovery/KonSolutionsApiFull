using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposPeriodoRepository
{
    Task<IEnumerable<TiposPeriodo>> ShowallAsync();
    Task<TiposPeriodo?> ShowbyidAsync(string COD_TIPO_PERIODO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposPeriodo>> SearchAsync(TiposPeriodoSearchDto dto);
    Task<WriteResult> InsertAsync(TiposPeriodoInsertDto dto);
    Task<WriteResult> UpdateAsync(TiposPeriodoUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_TIPO_PERIODO);
}

public interface ITiposPeriodoService
{
    Task<ApiResponse<IEnumerable<TiposPeriodo>>> GetAllAsync();
    Task<ApiResponse<TiposPeriodo>> GetByIdAsync(string COD_TIPO_PERIODO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposPeriodo>>> SearchAsync(TiposPeriodoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposPeriodoInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposPeriodoUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_PERIODO);
}