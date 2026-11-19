using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Costos;

namespace KonSolutionsApi.Application.Interfaces;

public interface ICostosCentroCostosProcesosRepository
{
    Task<IEnumerable<CostosCentroCostosProcesos>> ShowallAsync();
    Task<CostosCentroCostosProcesos?> ShowbyidAsync(string COD_CENTRO_COSTO, string COD_PROCESO, string COD_SUB_PROCESO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<CostosCentroCostosProcesos>> SearchAsync(CostosCentroCostosProcesosSearchDto dto);
    Task<WriteResult> InsertAsync(CostosCentroCostosProcesosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(CostosCentroCostosProcesosUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_CENTRO_COSTO, string COD_PROCESO, string COD_SUB_PROCESO);
}

public interface ICostosCentroCostosProcesosService
{
    Task<ApiResponse<IEnumerable<CostosCentroCostosProcesos>>> GetAllAsync();
    Task<ApiResponse<CostosCentroCostosProcesos>> GetByIdAsync(string COD_CENTRO_COSTO, string COD_PROCESO, string COD_SUB_PROCESO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<CostosCentroCostosProcesos>>> SearchAsync(CostosCentroCostosProcesosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(CostosCentroCostosProcesosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(CostosCentroCostosProcesosUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_CENTRO_COSTO, string COD_PROCESO, string COD_SUB_PROCESO);
}