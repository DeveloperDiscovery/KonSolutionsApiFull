using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface IPersonalFormacionAcademicaRepository
{
    Task<IEnumerable<PersonalFormacionAcademica>> ShowallAsync();
    Task<PersonalFormacionAcademica?> ShowbyidAsync(decimal COD_ENTIDAD_PERSONAL, decimal COD_ENTIDAD_ACADEMICA, string COD_CARRERA_ACADEMICA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<PersonalFormacionAcademica>> SearchAsync(PersonalFormacionAcademicaSearchDto dto);
    Task<WriteResult> InsertAsync(PersonalFormacionAcademicaInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(PersonalFormacionAcademicaUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(decimal COD_ENTIDAD_PERSONAL, decimal COD_ENTIDAD_ACADEMICA, string COD_CARRERA_ACADEMICA);
}

public interface IPersonalFormacionAcademicaService
{
    Task<ApiResponse<IEnumerable<PersonalFormacionAcademica>>> GetAllAsync();
    Task<ApiResponse<PersonalFormacionAcademica>> GetByIdAsync(decimal COD_ENTIDAD_PERSONAL, decimal COD_ENTIDAD_ACADEMICA, string COD_CARRERA_ACADEMICA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<PersonalFormacionAcademica>>> SearchAsync(PersonalFormacionAcademicaSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(PersonalFormacionAcademicaInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(PersonalFormacionAcademicaUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(decimal COD_ENTIDAD_PERSONAL, decimal COD_ENTIDAD_ACADEMICA, string COD_CARRERA_ACADEMICA);
}