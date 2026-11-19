using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface IPersonalCargoRepository
{
    Task<IEnumerable<PersonalCargo>> ShowallAsync();
    Task<PersonalCargo?> ShowbyidAsync(decimal COD_ENTIDAD_PERSONAL, int COD_CARGO, DateTime FEC_INGRESO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<PersonalCargo>> SearchAsync(PersonalCargoSearchDto dto);
    Task<WriteResult> InsertAsync(PersonalCargoInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(PersonalCargoUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(decimal COD_ENTIDAD_PERSONAL, int COD_CARGO, DateTime FEC_INGRESO);
}

public interface IPersonalCargoService
{
    Task<ApiResponse<IEnumerable<PersonalCargo>>> GetAllAsync();
    Task<ApiResponse<PersonalCargo>> GetByIdAsync(decimal COD_ENTIDAD_PERSONAL, int COD_CARGO, DateTime FEC_INGRESO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<PersonalCargo>>> SearchAsync(PersonalCargoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(PersonalCargoInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(PersonalCargoUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(decimal COD_ENTIDAD_PERSONAL, int COD_CARGO, DateTime FEC_INGRESO);
}