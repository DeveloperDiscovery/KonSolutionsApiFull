using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Proyectos;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposDocumentoCompromisoRepository
{
    Task<IEnumerable<TiposDocumentoCompromiso>> ShowallAsync();
    Task<TiposDocumentoCompromiso?> ShowbyidAsync(string cod);
    Task<IEnumerable<TiposDocumentoCompromiso>> SearchAsync(TiposDocumentoCompromisoSearchDto dto);
    Task<WriteResult> InsertAsync(TiposDocumentoCompromisoInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(TiposDocumentoCompromisoUpdateDto dto);
    Task<WriteResult> DeleteAsync(string cod);
}

public interface ITiposDocumentoCompromisoService
{
    Task<ApiResponse<IEnumerable<TiposDocumentoCompromiso>>> GetAllAsync();
    Task<ApiResponse<TiposDocumentoCompromiso>> GetByIdAsync(string cod);
    Task<ApiResponse<IEnumerable<TiposDocumentoCompromiso>>> SearchAsync(TiposDocumentoCompromisoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposDocumentoCompromisoInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposDocumentoCompromisoUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string cod);
}
