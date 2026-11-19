using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface IReglasComboSubCanalesRepository
{
    Task<IEnumerable<ReglasComboSubCanales>> ShowallAsync();
    Task<ReglasComboSubCanales?> ShowbyidAsync(int NUM_UNIDAD_COMERCIAL, int NUM_CANAL_VENTA, int NUM_SUB_CANAL_VENTA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ReglasComboSubCanales>> SearchAsync(ReglasComboSubCanalesSearchDto dto);
    Task<WriteResult> InsertAsync(ReglasComboSubCanalesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(ReglasComboSubCanalesUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_UNIDAD_COMERCIAL, int NUM_CANAL_VENTA, int NUM_SUB_CANAL_VENTA);
}

public interface IReglasComboSubCanalesService
{
    Task<ApiResponse<IEnumerable<ReglasComboSubCanales>>> GetAllAsync();
    Task<ApiResponse<ReglasComboSubCanales>> GetByIdAsync(int NUM_UNIDAD_COMERCIAL, int NUM_CANAL_VENTA, int NUM_SUB_CANAL_VENTA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ReglasComboSubCanales>>> SearchAsync(ReglasComboSubCanalesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ReglasComboSubCanalesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(ReglasComboSubCanalesUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_UNIDAD_COMERCIAL, int NUM_CANAL_VENTA, int NUM_SUB_CANAL_VENTA);
}