using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface IContratosModeloRepository
{
    Task<IEnumerable<ContratosModelo>> ShowallAsync();
    Task<ContratosModelo?> ShowbyidAsync(string COD_TIPO_CONTRATO, int NUM_VERSION);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ContratosModelo>> SearchAsync(ContratosModeloSearchDto dto);
    Task<WriteResult> InsertAsync(ContratosModeloInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(ContratosModeloUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_TIPO_CONTRATO, int NUM_VERSION);
}

public interface IContratosModeloService
{
    Task<ApiResponse<IEnumerable<ContratosModelo>>> GetAllAsync();
    Task<ApiResponse<ContratosModelo>> GetByIdAsync(string COD_TIPO_CONTRATO, int NUM_VERSION);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ContratosModelo>>> SearchAsync(ContratosModeloSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ContratosModeloInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(ContratosModeloUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_CONTRATO, int NUM_VERSION);
}