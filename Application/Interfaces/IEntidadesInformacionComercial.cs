using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface IEntidadesInformacionComercialRepository
{
    Task<IEnumerable<EntidadesInformacionComercial>> ShowallAsync(decimal codEntidad, string codClaseOperacion);
    Task<EntidadesInformacionComercial?> ShowbyidAsync(decimal codEntidad, string codClaseOperacion, string codTipoOperacion);
    Task<IEnumerable<EntidadesInformacionComercial>> SearchAsync(EntidadesInformacionComercialSearchDto dto);
    Task<WriteResult> InsertAsync(EntidadesInformacionComercialInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(EntidadesInformacionComercialUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(decimal codEntidad, string codClaseOperacion, string codTipoOperacion);
}

public interface IEntidadesInformacionComercialService
{
    Task<ApiResponse<IEnumerable<EntidadesInformacionComercial>>> GetAllAsync(decimal codEntidad, string codClaseOperacion);
    Task<ApiResponse<EntidadesInformacionComercial>> GetByIdAsync(decimal codEntidad, string codClaseOperacion, string codTipoOperacion);
    Task<ApiResponse<IEnumerable<EntidadesInformacionComercial>>> SearchAsync(EntidadesInformacionComercialSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(EntidadesInformacionComercialInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(EntidadesInformacionComercialUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(decimal codEntidad, string codClaseOperacion, string codTipoOperacion);
}
