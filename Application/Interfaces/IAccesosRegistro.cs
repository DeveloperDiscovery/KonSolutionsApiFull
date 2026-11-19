using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Safety;

namespace KonSolutionsApi.Application.Interfaces;

public interface IAccesosRegistroRepository
{
    Task<IEnumerable<AccesosRegistro>> ShowallAsync();
    Task<AccesosRegistro?> ShowbyidAsync(string COD_SUCURSAL, string COD_ZONIFICACION, decimal COD_ENTIDAD);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<AccesosRegistro>> SearchAsync(AccesosRegistroSearchDto dto);
    Task<WriteResult> InsertAsync(AccesosRegistroInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(AccesosRegistroUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_SUCURSAL, string COD_ZONIFICACION, decimal COD_ENTIDAD);
}

public interface IAccesosRegistroService
{
    Task<ApiResponse<IEnumerable<AccesosRegistro>>> GetAllAsync();
    Task<ApiResponse<AccesosRegistro>> GetByIdAsync(string COD_SUCURSAL, string COD_ZONIFICACION, decimal COD_ENTIDAD);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<AccesosRegistro>>> SearchAsync(AccesosRegistroSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(AccesosRegistroInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(AccesosRegistroUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_SUCURSAL, string COD_ZONIFICACION, decimal COD_ENTIDAD);
}