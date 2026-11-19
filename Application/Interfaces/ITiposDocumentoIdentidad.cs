using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposDocumentoIdentidadRepository
{
    Task<IEnumerable<TiposDocumentoIdentidad>> ShowallAsync();
    Task<TiposDocumentoIdentidad?> ShowbyidAsync(int COD_TIPO_DOCUMENTO_IDENTIDAD);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposDocumentoIdentidad>> SearchAsync(TiposDocumentoIdentidadSearchDto dto);
    Task<WriteResult> InsertAsync(TiposDocumentoIdentidadInsertDto dto);
    Task<WriteResult> UpdateAsync(TiposDocumentoIdentidadUpdateDto dto);
    Task<WriteResult> DeleteAsync(int COD_TIPO_DOCUMENTO_IDENTIDAD);
}

public interface ITiposDocumentoIdentidadService
{
    Task<ApiResponse<IEnumerable<TiposDocumentoIdentidad>>> GetAllAsync();
    Task<ApiResponse<TiposDocumentoIdentidad>> GetByIdAsync(int COD_TIPO_DOCUMENTO_IDENTIDAD);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposDocumentoIdentidad>>> SearchAsync(TiposDocumentoIdentidadSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposDocumentoIdentidadInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(int id, TiposDocumentoIdentidadInsertDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(int COD_TIPO_DOCUMENTO_IDENTIDAD);
}