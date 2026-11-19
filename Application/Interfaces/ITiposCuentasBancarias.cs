using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Finanzas;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposCuentasBancariasRepository
{
    Task<IEnumerable<TiposCuentasBancarias>> ShowallAsync();
    Task<TiposCuentasBancarias?> ShowbyidAsync(string COD_TIPO_CUENTA_BANCARIA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposCuentasBancarias>> SearchAsync(TiposCuentasBancariasSearchDto dto);
    Task<WriteResult> InsertAsync(TiposCuentasBancariasInsertDto dto);
    Task<WriteResult> UpdateAsync(TiposCuentasBancariasUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_TIPO_CUENTA_BANCARIA);
}

public interface ITiposCuentasBancariasService
{
    Task<ApiResponse<IEnumerable<TiposCuentasBancarias>>> GetAllAsync();
    Task<ApiResponse<TiposCuentasBancarias>> GetByIdAsync(string COD_TIPO_CUENTA_BANCARIA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposCuentasBancarias>>> SearchAsync(TiposCuentasBancariasSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposCuentasBancariasInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposCuentasBancariasUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_CUENTA_BANCARIA);
}