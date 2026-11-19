using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Sunat;

namespace KonSolutionsApi.Application.Interfaces;

public interface IRegimenesTributariosRepository
{
    Task<IEnumerable<RegimenesTributarios>> ShowallAsync();
    Task<RegimenesTributarios?> ShowbyidAsync(int COD_REGIMEN_TRIBUTARIO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<RegimenesTributarios>> SearchAsync(RegimenesTributariosSearchDto dto);
    Task<WriteResult> InsertAsync(RegimenesTributariosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(RegimenesTributariosUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int COD_REGIMEN_TRIBUTARIO);
    /// <summary>PROC_SUNAT_REGIMENES_TRIBUTARIOS_LOADCOMBOS — tipos de comprobante de pago,
    /// monedas, tipos de impuesto y tipos de valor de cálculo en un solo viaje.</summary>
    Task<RegimenesTributariosLoadCombosResult> LoadcombosAsync();
}

public interface IRegimenesTributariosService
{
    Task<ApiResponse<IEnumerable<RegimenesTributarios>>> GetAllAsync();
    Task<ApiResponse<RegimenesTributarios>> GetByIdAsync(int COD_REGIMEN_TRIBUTARIO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<RegimenesTributarios>>> SearchAsync(RegimenesTributariosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(RegimenesTributariosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(RegimenesTributariosUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int COD_REGIMEN_TRIBUTARIO);
    Task<ApiResponse<RegimenesTributariosLoadCombosResult>> LoadcombosAsync();
}