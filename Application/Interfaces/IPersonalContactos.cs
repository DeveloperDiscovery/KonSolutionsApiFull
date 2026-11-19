using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface IPersonalContactosRepository
{
    Task<IEnumerable<PersonalContactos>> ShowallAsync();
    Task<PersonalContactos?> ShowbyidAsync(decimal COD_ENTIDAD_PERSONAL, decimal COD_ENTIDAD_CONTACTO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<PersonalContactos>> SearchAsync(PersonalContactosSearchDto dto);
    Task<WriteResult> InsertAsync(PersonalContactosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(PersonalContactosUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(decimal COD_ENTIDAD_PERSONAL, decimal COD_ENTIDAD_CONTACTO);
}

public interface IPersonalContactosService
{
    Task<ApiResponse<IEnumerable<PersonalContactos>>> GetAllAsync();
    Task<ApiResponse<PersonalContactos>> GetByIdAsync(decimal COD_ENTIDAD_PERSONAL, decimal COD_ENTIDAD_CONTACTO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<PersonalContactos>>> SearchAsync(PersonalContactosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(PersonalContactosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(PersonalContactosUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(decimal COD_ENTIDAD_PERSONAL, decimal COD_ENTIDAD_CONTACTO);
}