using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface IManualesFuncionesRepository
{
    Task<IEnumerable<ManualesFunciones>> ShowallAsync();
    Task<ManualesFunciones?> ShowbyidAsync(int NUM_MANUAL_FUNCIONES);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ManualesFunciones>> SearchAsync(ManualesFuncionesSearchDto dto);
    Task<WriteResult> InsertAsync(ManualesFuncionesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(ManualesFuncionesUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_MANUAL_FUNCIONES);
}

public interface IManualesFuncionesService
{
    Task<ApiResponse<IEnumerable<ManualesFunciones>>> GetAllAsync();
    Task<ApiResponse<ManualesFunciones>> GetByIdAsync(int NUM_MANUAL_FUNCIONES);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ManualesFunciones>>> SearchAsync(ManualesFuncionesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ManualesFuncionesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(ManualesFuncionesUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_MANUAL_FUNCIONES);
}