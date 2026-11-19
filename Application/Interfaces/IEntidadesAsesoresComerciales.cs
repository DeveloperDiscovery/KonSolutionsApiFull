using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface IEntidadesAsesoresComercialesRepository
{
    Task<IEnumerable<EntidadesAsesoresComerciales>> ShowallAsync(decimal codEntidadCliente);
    Task<EntidadesAsesoresComerciales?> ShowbyidAsync(decimal codEntidadCliente, decimal codEntidadAsesorComercial);
    Task<IEnumerable<EntidadesAsesoresComerciales>> SearchAsync(EntidadesAsesoresComercialesSearchDto dto);
    Task<WriteResult> InsertAsync(EntidadesAsesoresComercialesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(decimal codEntidadCliente, decimal codEntidadAsesorComercial);
}

public interface IEntidadesAsesoresComercialesService
{
    Task<ApiResponse<IEnumerable<EntidadesAsesoresComerciales>>> GetAllAsync(decimal codEntidadCliente);
    Task<ApiResponse<EntidadesAsesoresComerciales>> GetByIdAsync(decimal codEntidadCliente, decimal codEntidadAsesorComercial);
    Task<ApiResponse<IEnumerable<EntidadesAsesoresComerciales>>> SearchAsync(EntidadesAsesoresComercialesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(EntidadesAsesoresComercialesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(decimal codEntidadCliente, decimal codEntidadAsesorComercial);
}
