using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Finanzas;

namespace KonSolutionsApi.Application.Interfaces;

public interface IBancosCuentasBancariasRepository
{
    Task<IEnumerable<BancosCuentasBancarias>> ShowallAsync(string codBanco);
    Task<BancosCuentasBancarias?> ShowbyidAsync(string COD_BANCO, string NUM_CUENTA_BANCARIA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<BancosCuentasBancarias>> SearchAsync(BancosCuentasBancariasSearchDto dto);
    Task<WriteResult> InsertAsync(BancosCuentasBancariasInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(BancosCuentasBancariasUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_BANCO, string NUM_CUENTA_BANCARIA);
}

public interface IBancosCuentasBancariasService
{
    Task<ApiResponse<IEnumerable<BancosCuentasBancarias>>> GetAllAsync(string codBanco);
    Task<ApiResponse<BancosCuentasBancarias>> GetByIdAsync(string COD_BANCO, string NUM_CUENTA_BANCARIA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<BancosCuentasBancarias>>> SearchAsync(BancosCuentasBancariasSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(BancosCuentasBancariasInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(BancosCuentasBancariasUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_BANCO, string NUM_CUENTA_BANCARIA);
}