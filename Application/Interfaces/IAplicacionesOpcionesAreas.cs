using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Interfaces;

public interface IAplicacionesOpcionesAreasRepository
{
    Task<IEnumerable<AplicacionesOpcionesAreas>> ShowallAsync(string COD_APLICACION);
    Task<AplicacionesOpcionesAreas?> ShowbyidAsync(string COD_APLICACION, string COD_OPCION_APLICACION);
    Task<IEnumerable<AplicacionesOpcionesAreas>> SearchAsync(AplicacionesOpcionesAreasSearchDto dto);
    Task<WriteResult> InsertAsync(AplicacionesOpcionesAreasInsertDto dto);
    Task<WriteResult> DeleteAsync(string COD_APLICACION, string COD_OPCION_APLICACION);
}
