using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface IEntidadesUnidadesComercialesRepository
{
    Task<IEnumerable<EntidadesUnidadesComerciales>> ShowallAsync(decimal codEntidad);
    Task<EntidadesUnidadesComerciales?> ShowbyidAsync(decimal codEntidad, byte codUnidadComercial);
    Task<WriteResult> InsertAsync(EntidadesUnidadesComercialesInsertDto dto);
    Task<WriteResult> UpdateAsync(EntidadesUnidadesComercialesUpdateDto dto);
    Task<WriteResult> DeleteAsync(decimal codEntidad, byte codUnidadComercial);
}

public interface IEntidadesUnidadesComercialesService
{
    Task<ApiResponse<IEnumerable<EntidadesUnidadesComerciales>>> GetAllAsync(decimal codEntidad);
    Task<ApiResponse<EntidadesUnidadesComerciales>> GetByIdAsync(decimal codEntidad, byte codUnidadComercial);
    Task<ApiResponse<WriteResult>> InsertAsync(EntidadesUnidadesComercialesInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(EntidadesUnidadesComercialesUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(decimal codEntidad, byte codUnidadComercial);
}
