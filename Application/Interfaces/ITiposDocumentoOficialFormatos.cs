using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposDocumentoOficialFormatosRepository
{
    Task<IEnumerable<TiposDocumentoOficialFormatos>> ShowallAsync();
    Task<TiposDocumentoOficialFormatos?> ShowbyidAsync(int NUM_FORMATO_DOCUMENTO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposDocumentoOficialFormatos>> SearchAsync(TiposDocumentoOficialFormatosSearchDto dto);
    Task<WriteResult> InsertAsync(TiposDocumentoOficialFormatosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(TiposDocumentoOficialFormatosUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_FORMATO_DOCUMENTO);
}

public interface ITiposDocumentoOficialFormatosService
{
    Task<ApiResponse<IEnumerable<TiposDocumentoOficialFormatos>>> GetAllAsync();
    Task<ApiResponse<TiposDocumentoOficialFormatos>> GetByIdAsync(int NUM_FORMATO_DOCUMENTO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposDocumentoOficialFormatos>>> SearchAsync(TiposDocumentoOficialFormatosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposDocumentoOficialFormatosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposDocumentoOficialFormatosUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_FORMATO_DOCUMENTO);
}