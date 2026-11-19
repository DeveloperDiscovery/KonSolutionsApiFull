using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Interfaces;

public interface IEntidadesDocumentosIdentidadRepository
{
    Task<IEnumerable<EntidadesDocumentosIdentidad>> ShowallAsync(decimal COD_ENTIDAD);
    Task<EntidadesDocumentosIdentidad?> ShowbyidAsync(decimal COD_ENTIDAD, int COD_TIPO_DOCUMENTO_IDENTIDAD);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<EntidadesDocumentosIdentidad>> SearchAsync(EntidadesDocumentosIdentidadSearchDto dto);
    Task<WriteResult> InsertAsync(EntidadesDocumentosIdentidadInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(EntidadesDocumentosIdentidadUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(decimal COD_ENTIDAD, int COD_TIPO_DOCUMENTO_IDENTIDAD);
}

public interface IEntidadesDocumentosIdentidadService
{
    Task<ApiResponse<IEnumerable<EntidadesDocumentosIdentidad>>> GetAllAsync(decimal COD_ENTIDAD);
    Task<ApiResponse<EntidadesDocumentosIdentidad>> GetByIdAsync(decimal COD_ENTIDAD, int COD_TIPO_DOCUMENTO_IDENTIDAD);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<EntidadesDocumentosIdentidad>>> SearchAsync(EntidadesDocumentosIdentidadSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(EntidadesDocumentosIdentidadInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(EntidadesDocumentosIdentidadUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(decimal COD_ENTIDAD, int COD_TIPO_DOCUMENTO_IDENTIDAD);
}