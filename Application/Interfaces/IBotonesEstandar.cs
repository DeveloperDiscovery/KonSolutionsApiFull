using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;

namespace KonSolutionsApi.Application.Interfaces;

public interface IBotonesEstandarRepository
{
    Task<IEnumerable<BotonesEstandar>> ShowallAsync();
    Task<BotonesEstandar?> ShowbyidAsync(string COD_BOTON_OPCION_ESTANDAR);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<BotonesEstandar>> SearchAsync(BotonesEstandarSearchDto dto);
    Task<WriteResult> InsertAsync(BotonesEstandarInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(BotonesEstandarUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_BOTON_OPCION_ESTANDAR);
}

public interface IBotonesEstandarService
{
    Task<ApiResponse<IEnumerable<BotonesEstandar>>> GetAllAsync();
    Task<ApiResponse<BotonesEstandar>> GetByIdAsync(string COD_BOTON_OPCION_ESTANDAR);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<BotonesEstandar>>> SearchAsync(BotonesEstandarSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(BotonesEstandarInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(BotonesEstandarUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_BOTON_OPCION_ESTANDAR);
}