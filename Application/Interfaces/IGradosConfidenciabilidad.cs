using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface IGradosConfidenciabilidadRepository
{
    Task<IEnumerable<GradosConfidenciabilidad>> ShowallAsync();
    Task<GradosConfidenciabilidad?> ShowbyidAsync(int COD_GRADO_CONFIDENCIABILIDAD);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<GradosConfidenciabilidad>> SearchAsync(GradosConfidenciabilidadSearchDto dto);
    Task<WriteResult> InsertAsync(GradosConfidenciabilidadInsertDto dto);
    Task<WriteResult> UpdateAsync(GradosConfidenciabilidadUpdateDto dto);
    Task<WriteResult> DeleteAsync(int COD_GRADO_CONFIDENCIABILIDAD);
}

public interface IGradosConfidenciabilidadService
{
    Task<ApiResponse<IEnumerable<GradosConfidenciabilidad>>> GetAllAsync();
    Task<ApiResponse<GradosConfidenciabilidad>> GetByIdAsync(int COD_GRADO_CONFIDENCIABILIDAD);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<GradosConfidenciabilidad>>> SearchAsync(GradosConfidenciabilidadSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(GradosConfidenciabilidadInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(GradosConfidenciabilidadUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(int COD_GRADO_CONFIDENCIABILIDAD);
}