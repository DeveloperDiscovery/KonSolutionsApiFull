using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposSolicitudComercialAreasCargosRepository
{
    Task<IEnumerable<TiposSolicitudComercialAreasCargos>> ShowallAsync(string codTipoSolicitudComercial, int codArea);
    Task<TiposSolicitudComercialAreasCargos?> ShowbyidAsync(string codTipoSolicitudComercial, int codArea, int codCargo);
    Task<IEnumerable<TiposSolicitudComercialAreasCargos>> SearchAsync(TiposSolicitudComercialAreasCargosSearchDto dto);
    Task<WriteResult> InsertAsync(TiposSolicitudComercialAreasCargosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string codTipoSolicitudComercial, int codArea, int codCargo);
    Task<TiposSolicitudComercialAreasCargosLoadCombosResult> LoadcombosAsync();
}

public interface ITiposSolicitudComercialAreasCargosService
{
    Task<ApiResponse<IEnumerable<TiposSolicitudComercialAreasCargos>>> GetAllAsync(string codTipoSolicitudComercial, int codArea);
    Task<ApiResponse<TiposSolicitudComercialAreasCargos>> GetByIdAsync(string codTipoSolicitudComercial, int codArea, int codCargo);
    Task<ApiResponse<IEnumerable<TiposSolicitudComercialAreasCargos>>> SearchAsync(TiposSolicitudComercialAreasCargosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposSolicitudComercialAreasCargosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string codTipoSolicitudComercial, int codArea, int codCargo);
    Task<ApiResponse<TiposSolicitudComercialAreasCargosLoadCombosResult>> LoadcombosAsync();
}
