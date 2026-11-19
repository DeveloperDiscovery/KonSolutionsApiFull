using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface IEntidadesPurcharseOrdersEspecificacionesDespachoRepository
{
    Task<IEnumerable<EntidadesPurcharseOrdersEspecificacionesDespacho>> ShowallAsync();
    Task<EntidadesPurcharseOrdersEspecificacionesDespacho?> ShowbyidAsync(decimal COD_ENTIDAD, string NUM_PURCHASE_ORDER, int NUM_SECUENCIA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<EntidadesPurcharseOrdersEspecificacionesDespacho>> SearchAsync(EntidadesPurcharseOrdersEspecificacionesDespachoSearchDto dto);
    Task<WriteResult> InsertAsync(EntidadesPurcharseOrdersEspecificacionesDespachoInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(EntidadesPurcharseOrdersEspecificacionesDespachoUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(decimal COD_ENTIDAD, string NUM_PURCHASE_ORDER, int NUM_SECUENCIA);
}

public interface IEntidadesPurcharseOrdersEspecificacionesDespachoService
{
    Task<ApiResponse<IEnumerable<EntidadesPurcharseOrdersEspecificacionesDespacho>>> GetAllAsync();
    Task<ApiResponse<EntidadesPurcharseOrdersEspecificacionesDespacho>> GetByIdAsync(decimal COD_ENTIDAD, string NUM_PURCHASE_ORDER, int NUM_SECUENCIA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<EntidadesPurcharseOrdersEspecificacionesDespacho>>> SearchAsync(EntidadesPurcharseOrdersEspecificacionesDespachoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(EntidadesPurcharseOrdersEspecificacionesDespachoInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(EntidadesPurcharseOrdersEspecificacionesDespachoUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(decimal COD_ENTIDAD, string NUM_PURCHASE_ORDER, int NUM_SECUENCIA);
}