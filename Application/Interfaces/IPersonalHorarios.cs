using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Nomina;

namespace KonSolutionsApi.Application.Interfaces;

public interface IPersonalHorariosRepository
{
    Task<IEnumerable<PersonalHorarios>> ShowallAsync();
    Task<PersonalHorarios?> ShowbyidAsync(decimal COD_ENTIDAD_PERSONAL, int NUM_SECUENCIA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<PersonalHorarios>> SearchAsync(PersonalHorariosSearchDto dto);
    Task<WriteResult> InsertAsync(PersonalHorariosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(PersonalHorariosUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(decimal COD_ENTIDAD_PERSONAL, int NUM_SECUENCIA);
}

public interface IPersonalHorariosService
{
    Task<ApiResponse<IEnumerable<PersonalHorarios>>> GetAllAsync();
    Task<ApiResponse<PersonalHorarios>> GetByIdAsync(decimal COD_ENTIDAD_PERSONAL, int NUM_SECUENCIA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<PersonalHorarios>>> SearchAsync(PersonalHorariosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(PersonalHorariosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(PersonalHorariosUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(decimal COD_ENTIDAD_PERSONAL, int NUM_SECUENCIA);
}