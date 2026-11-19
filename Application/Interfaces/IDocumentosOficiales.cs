using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface IDocumentosOficialesRepository
{
    Task<IEnumerable<DocumentosOficiales>> ShowallAsync();
    Task<DocumentosOficiales?> ShowbyidAsync(int NUM_DOCUMENTO_OFICIAL);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<DocumentosOficiales>> SearchAsync(DocumentosOficialesSearchDto dto);
    Task<WriteResult> InsertAsync(DocumentosOficialesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(DocumentosOficialesUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_DOCUMENTO_OFICIAL);
}

public interface IDocumentosOficialesService
{
    Task<ApiResponse<IEnumerable<DocumentosOficiales>>> GetAllAsync();
    Task<ApiResponse<DocumentosOficiales>> GetByIdAsync(int NUM_DOCUMENTO_OFICIAL);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<DocumentosOficiales>>> SearchAsync(DocumentosOficialesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(DocumentosOficialesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(DocumentosOficialesUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_DOCUMENTO_OFICIAL);
}