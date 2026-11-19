using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface ICarrerasAcademicasRepository
{
    Task<IEnumerable<CarrerasAcademicas>> ShowallAsync();
    Task<CarrerasAcademicas?> ShowbyidAsync(string COD_CARRERA_ACADEMICA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<CarrerasAcademicas>> SearchAsync(CarrerasAcademicasSearchDto dto);
    Task<WriteResult> InsertAsync(CarrerasAcademicasInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(CarrerasAcademicasUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_CARRERA_ACADEMICA);
}

public interface ICarrerasAcademicasService
{
    Task<ApiResponse<IEnumerable<CarrerasAcademicas>>> GetAllAsync();
    Task<ApiResponse<CarrerasAcademicas>> GetByIdAsync(string COD_CARRERA_ACADEMICA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<CarrerasAcademicas>>> SearchAsync(CarrerasAcademicasSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(CarrerasAcademicasInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(CarrerasAcademicasUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_CARRERA_ACADEMICA);
}