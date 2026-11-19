using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Finanzas;

namespace KonSolutionsApi.Application.Interfaces;

public interface IEntidadesCuentasBancariasRepository
{
    Task<IEnumerable<EntidadesCuentasBancarias>> ShowallAsync(decimal codEntidad);
    Task<EntidadesCuentasBancarias?> ShowbyidAsync(decimal COD_ENTIDAD_BANCARIA, string COD_BANCO, string NUM_CUENTA_BANCARIA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<EntidadesCuentasBancarias>> SearchAsync(EntidadesCuentasBancariasSearchDto dto);
    Task<WriteResult> InsertAsync(EntidadesCuentasBancariasInsertDto dto);
    Task<WriteResult> UpdateAsync(EntidadesCuentasBancariasUpdateDto dto);
    Task<WriteResult> DeleteAsync(decimal COD_ENTIDAD_BANCARIA, string COD_BANCO, string NUM_CUENTA_BANCARIA);
}

public interface IEntidadesCuentasBancariasService
{
    Task<ApiResponse<IEnumerable<EntidadesCuentasBancarias>>> GetAllAsync(decimal codEntidad);
    Task<ApiResponse<EntidadesCuentasBancarias>> GetByIdAsync(decimal COD_ENTIDAD_BANCARIA, string COD_BANCO, string NUM_CUENTA_BANCARIA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<EntidadesCuentasBancarias>>> SearchAsync(EntidadesCuentasBancariasSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(EntidadesCuentasBancariasInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(EntidadesCuentasBancariasUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(decimal COD_ENTIDAD_BANCARIA, string COD_BANCO, string NUM_CUENTA_BANCARIA);
}