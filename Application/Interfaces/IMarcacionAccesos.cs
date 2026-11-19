using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Safety;

namespace KonSolutionsApi.Application.Interfaces;

public interface IMarcacionAccesosRepository
{
    Task<IEnumerable<MarcacionAccesos>> ShowallAsync();
    Task<MarcacionAccesos?> ShowbyidAsync(string COD_SUCURSAL, string COD_ZONIFICACION, string COD_ACTIVO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<MarcacionAccesos>> SearchAsync(MarcacionAccesosSearchDto dto);
    Task<WriteResult> InsertAsync(MarcacionAccesosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(MarcacionAccesosUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_SUCURSAL, string COD_ZONIFICACION, string COD_ACTIVO);
}

public interface IMarcacionAccesosService
{
    Task<ApiResponse<IEnumerable<MarcacionAccesos>>> GetAllAsync();
    Task<ApiResponse<MarcacionAccesos>> GetByIdAsync(string COD_SUCURSAL, string COD_ZONIFICACION, string COD_ACTIVO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<MarcacionAccesos>>> SearchAsync(MarcacionAccesosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(MarcacionAccesosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(MarcacionAccesosUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_SUCURSAL, string COD_ZONIFICACION, string COD_ACTIVO);
}