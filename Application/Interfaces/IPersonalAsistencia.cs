using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface IPersonalAsistenciaRepository
{
    Task<IEnumerable<PersonalAsistencia>> ShowallAsync();
    Task<PersonalAsistencia?> ShowbyidAsync(int NUM_PLANILLA_REMUNERATIVA, decimal COD_ENTIDAD_PERSONAL);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<PersonalAsistencia>> SearchAsync(PersonalAsistenciaSearchDto dto);
    Task<WriteResult> InsertAsync(PersonalAsistenciaInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(PersonalAsistenciaUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_PLANILLA_REMUNERATIVA, decimal COD_ENTIDAD_PERSONAL);
}

public interface IPersonalAsistenciaService
{
    Task<ApiResponse<IEnumerable<PersonalAsistencia>>> GetAllAsync();
    Task<ApiResponse<PersonalAsistencia>> GetByIdAsync(int NUM_PLANILLA_REMUNERATIVA, decimal COD_ENTIDAD_PERSONAL);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<PersonalAsistencia>>> SearchAsync(PersonalAsistenciaSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(PersonalAsistenciaInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(PersonalAsistenciaUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_PLANILLA_REMUNERATIVA, decimal COD_ENTIDAD_PERSONAL);
}