using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface IDocumentosOficialesPersonalRepository
{
    Task<IEnumerable<DocumentosOficialesPersonal>> ShowallAsync();
    Task<DocumentosOficialesPersonal?> ShowbyidAsync(int NUM_DOCUMENTO_OFICIAL, decimal COD_ENTIDAD_PERSONAL);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<DocumentosOficialesPersonal>> SearchAsync(DocumentosOficialesPersonalSearchDto dto);
    Task<WriteResult> InsertAsync(DocumentosOficialesPersonalInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(DocumentosOficialesPersonalUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_DOCUMENTO_OFICIAL, decimal COD_ENTIDAD_PERSONAL);
}

public interface IDocumentosOficialesPersonalService
{
    Task<ApiResponse<IEnumerable<DocumentosOficialesPersonal>>> GetAllAsync();
    Task<ApiResponse<DocumentosOficialesPersonal>> GetByIdAsync(int NUM_DOCUMENTO_OFICIAL, decimal COD_ENTIDAD_PERSONAL);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<DocumentosOficialesPersonal>>> SearchAsync(DocumentosOficialesPersonalSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(DocumentosOficialesPersonalInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(DocumentosOficialesPersonalUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_DOCUMENTO_OFICIAL, decimal COD_ENTIDAD_PERSONAL);
}