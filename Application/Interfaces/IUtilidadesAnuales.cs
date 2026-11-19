using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Nomina;

namespace KonSolutionsApi.Application.Interfaces;

public interface IUtilidadesAnualesRepository
{
    Task<IEnumerable<UtilidadesAnuales>> ShowallAsync();
    Task<UtilidadesAnuales?> ShowbyidAsync(int NUM_ANO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<UtilidadesAnuales>> SearchAsync(UtilidadesAnualesSearchDto dto);
    Task<WriteResult> InsertAsync(UtilidadesAnualesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(UtilidadesAnualesUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_ANO);
}

public interface IUtilidadesAnualesService
{
    Task<ApiResponse<IEnumerable<UtilidadesAnuales>>> GetAllAsync();
    Task<ApiResponse<UtilidadesAnuales>> GetByIdAsync(int NUM_ANO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<UtilidadesAnuales>>> SearchAsync(UtilidadesAnualesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(UtilidadesAnualesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(UtilidadesAnualesUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_ANO);
}