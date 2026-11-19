using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Nomina;

namespace KonSolutionsApi.Application.Interfaces;

public interface IHorariosRepository
{
    Task<IEnumerable<Horarios>> ShowallAsync();
    Task<Horarios?> ShowbyidAsync(string COD_HORARIO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<Horarios>> SearchAsync(HorariosSearchDto dto);
    Task<WriteResult> InsertAsync(HorariosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(HorariosUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_HORARIO);
}

public interface IHorariosService
{
    Task<ApiResponse<IEnumerable<Horarios>>> GetAllAsync();
    Task<ApiResponse<Horarios>> GetByIdAsync(string COD_HORARIO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<Horarios>>> SearchAsync(HorariosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(HorariosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(HorariosUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_HORARIO);
}