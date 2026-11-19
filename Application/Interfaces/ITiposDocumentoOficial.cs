using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposDocumentoOficialRepository
{
    Task<IEnumerable<TiposDocumentoOficial>> ShowallAsync();
    Task<TiposDocumentoOficial?> ShowbyidAsync(string COD_TIPO_DOCUMENTO_OFICIAL);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposDocumentoOficial>> SearchAsync(TiposDocumentoOficialSearchDto dto);
    Task<WriteResult> InsertAsync(TiposDocumentoOficialInsertDto dto);
    Task<WriteResult> UpdateAsync(TiposDocumentoOficialUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_TIPO_DOCUMENTO_OFICIAL);
}

public interface ITiposDocumentoOficialService
{
    Task<ApiResponse<IEnumerable<TiposDocumentoOficial>>> GetAllAsync();
    Task<ApiResponse<TiposDocumentoOficial>> GetByIdAsync(string COD_TIPO_DOCUMENTO_OFICIAL);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposDocumentoOficial>>> SearchAsync(TiposDocumentoOficialSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposDocumentoOficialInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposDocumentoOficialUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_DOCUMENTO_OFICIAL);
}