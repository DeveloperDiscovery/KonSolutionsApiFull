using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.Application.Interfaces;

public interface IDisenosVersionEspecificacionesTecnicasRepository
{
    Task<IEnumerable<DisenosVersionEspecificacionesTecnicas>> ShowallAsync();
    Task<DisenosVersionEspecificacionesTecnicas?> ShowbyidAsync(string COD_DISENO, int NUM_VERSION, string COD_ESPECIFICACION_TECNICA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<DisenosVersionEspecificacionesTecnicas>> SearchAsync(DisenosVersionEspecificacionesTecnicasSearchDto dto);
    Task<WriteResult> InsertAsync(DisenosVersionEspecificacionesTecnicasInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(DisenosVersionEspecificacionesTecnicasUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_DISENO, int NUM_VERSION, string COD_ESPECIFICACION_TECNICA);
}

public interface IDisenosVersionEspecificacionesTecnicasService
{
    Task<ApiResponse<IEnumerable<DisenosVersionEspecificacionesTecnicas>>> GetAllAsync();
    Task<ApiResponse<DisenosVersionEspecificacionesTecnicas>> GetByIdAsync(string COD_DISENO, int NUM_VERSION, string COD_ESPECIFICACION_TECNICA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<DisenosVersionEspecificacionesTecnicas>>> SearchAsync(DisenosVersionEspecificacionesTecnicasSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(DisenosVersionEspecificacionesTecnicasInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(DisenosVersionEspecificacionesTecnicasUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_DISENO, int NUM_VERSION, string COD_ESPECIFICACION_TECNICA);
}