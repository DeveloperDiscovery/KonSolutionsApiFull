using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface IEntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicasRepository
{
    Task<IEnumerable<EntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicas>> ShowallAsync();
    Task<EntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicas?> ShowbyidAsync(decimal COD_ENTIDAD, string NUM_PURCHASE_ORDER, int NUM_SECUENCIA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<EntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicas>> SearchAsync(EntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicasSearchDto dto);
    Task<WriteResult> InsertAsync(EntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicasInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(EntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicasUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(decimal COD_ENTIDAD, string NUM_PURCHASE_ORDER, int NUM_SECUENCIA);
}

public interface IEntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicasService
{
    Task<ApiResponse<IEnumerable<EntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicas>>> GetAllAsync();
    Task<ApiResponse<EntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicas>> GetByIdAsync(decimal COD_ENTIDAD, string NUM_PURCHASE_ORDER, int NUM_SECUENCIA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<EntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicas>>> SearchAsync(EntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicasSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(EntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicasInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(EntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicasUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(decimal COD_ENTIDAD, string NUM_PURCHASE_ORDER, int NUM_SECUENCIA);
}