using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Safety;

namespace KonSolutionsApi.Application.Interfaces;

public interface IAccesosRepository
{
    Task<IEnumerable<Accesos>> ShowallAsync();
    Task<Accesos?> ShowbyidAsync(string COD_SUCURSAL, string COD_ZONIFICACION, decimal COD_ENTIDAD);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<Accesos>> SearchAsync(AccesosSearchDto dto);
    Task<WriteResult> InsertAsync(AccesosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(AccesosUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_SUCURSAL, string COD_ZONIFICACION, decimal COD_ENTIDAD);
}

public interface IAccesosService
{
    Task<ApiResponse<IEnumerable<Accesos>>> GetAllAsync();
    Task<ApiResponse<Accesos>> GetByIdAsync(string COD_SUCURSAL, string COD_ZONIFICACION, decimal COD_ENTIDAD);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<Accesos>>> SearchAsync(AccesosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(AccesosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(AccesosUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_SUCURSAL, string COD_ZONIFICACION, decimal COD_ENTIDAD);
}