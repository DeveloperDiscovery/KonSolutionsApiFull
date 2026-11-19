using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposContratoBeneficiosLaboralesRepository
{
    Task<IEnumerable<TiposContratoBeneficiosLaborales>> ShowallAsync();
    Task<TiposContratoBeneficiosLaborales?> ShowbyidAsync(string COD_TIPO_CONTRATO, string COD_BENEFICIO_LABORAL);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposContratoBeneficiosLaborales>> SearchAsync(TiposContratoBeneficiosLaboralesSearchDto dto);
    Task<WriteResult> InsertAsync(TiposContratoBeneficiosLaboralesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_TIPO_CONTRATO, string COD_BENEFICIO_LABORAL);
}

public interface ITiposContratoBeneficiosLaboralesService
{
    Task<ApiResponse<IEnumerable<TiposContratoBeneficiosLaborales>>> GetAllAsync();
    Task<ApiResponse<TiposContratoBeneficiosLaborales>> GetByIdAsync(string COD_TIPO_CONTRATO, string COD_BENEFICIO_LABORAL);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposContratoBeneficiosLaborales>>> SearchAsync(TiposContratoBeneficiosLaboralesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposContratoBeneficiosLaboralesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_CONTRATO, string COD_BENEFICIO_LABORAL);
}