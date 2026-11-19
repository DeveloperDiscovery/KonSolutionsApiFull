using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface IPersonalContratosRepository
{
    Task<IEnumerable<PersonalContratos>> ShowallAsync();
    Task<PersonalContratos?> ShowbyidAsync(int NUM_CONTRATO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<PersonalContratos>> SearchAsync(PersonalContratosSearchDto dto);
    Task<WriteResult> InsertAsync(PersonalContratosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(PersonalContratosUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_CONTRATO);
}

public interface IPersonalContratosService
{
    Task<ApiResponse<IEnumerable<PersonalContratos>>> GetAllAsync();
    Task<ApiResponse<PersonalContratos>> GetByIdAsync(int NUM_CONTRATO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<PersonalContratos>>> SearchAsync(PersonalContratosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(PersonalContratosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(PersonalContratosUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_CONTRATO);
}