using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface ICargosFuncionesRepository
{
    Task<IEnumerable<CargosFunciones>> ShowallAsync();
    Task<CargosFunciones?> ShowbyidAsync(int COD_CARGO, string COD_FUNCION);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<CargosFunciones>> SearchAsync(CargosFuncionesSearchDto dto);
    Task<WriteResult> InsertAsync(CargosFuncionesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(CargosFuncionesUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int COD_CARGO, string COD_FUNCION);
}

public interface ICargosFuncionesService
{
    Task<ApiResponse<IEnumerable<CargosFunciones>>> GetAllAsync();
    Task<ApiResponse<CargosFunciones>> GetByIdAsync(int COD_CARGO, string COD_FUNCION);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<CargosFunciones>>> SearchAsync(CargosFuncionesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(CargosFuncionesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(CargosFuncionesUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int COD_CARGO, string COD_FUNCION);
}