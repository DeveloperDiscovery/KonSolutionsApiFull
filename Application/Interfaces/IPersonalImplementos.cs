using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface IPersonalImplementosRepository
{
    Task<IEnumerable<PersonalImplementos>> ShowallAsync();
    Task<PersonalImplementos?> ShowbyidAsync(decimal COD_ENTIDAD_PERSONAL, string COD_PRODUCTO_IMPLEMENTO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<PersonalImplementos>> SearchAsync(PersonalImplementosSearchDto dto);
    Task<WriteResult> InsertAsync(PersonalImplementosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(PersonalImplementosUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(decimal COD_ENTIDAD_PERSONAL, string COD_PRODUCTO_IMPLEMENTO);
}

public interface IPersonalImplementosService
{
    Task<ApiResponse<IEnumerable<PersonalImplementos>>> GetAllAsync();
    Task<ApiResponse<PersonalImplementos>> GetByIdAsync(decimal COD_ENTIDAD_PERSONAL, string COD_PRODUCTO_IMPLEMENTO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<PersonalImplementos>>> SearchAsync(PersonalImplementosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(PersonalImplementosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(PersonalImplementosUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(decimal COD_ENTIDAD_PERSONAL, string COD_PRODUCTO_IMPLEMENTO);
}