using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

// ── Cabecera ────────────────────────────────────────────────────────────────
public interface IEntidadesPurchaseOrdersRepository
{
    Task<IEnumerable<EntidadesPurchaseOrders>> ShowallAsync(decimal codEntidad);
    Task<EntidadesPurchaseOrders?> ShowbyidAsync(decimal codEntidad, string numPurchaseOrder);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(decimal codEntidad);
    Task<IEnumerable<EntidadesPurchaseOrders>> SearchAsync(EntidadesPurchaseOrdersSearchDto dto);
    Task<WriteResult> InsertAsync(EntidadesPurchaseOrdersInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(EntidadesPurchaseOrdersUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(decimal codEntidad, string numPurchaseOrder);
}

public interface IEntidadesPurchaseOrdersService
{
    Task<ApiResponse<IEnumerable<EntidadesPurchaseOrders>>> GetAllAsync(decimal codEntidad);
    Task<ApiResponse<EntidadesPurchaseOrders>> GetByIdAsync(decimal codEntidad, string numPurchaseOrder);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(decimal codEntidad);
    Task<ApiResponse<IEnumerable<EntidadesPurchaseOrders>>> SearchAsync(EntidadesPurchaseOrdersSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(EntidadesPurchaseOrdersInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(EntidadesPurchaseOrdersUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(decimal codEntidad, string numPurchaseOrder);
}


// ── Despacho ────────────────────────────────────────────────────────────────
public interface IEntidadesPurchaseOrdersDespachoRepository
{
    Task<IEnumerable<EntidadesPurchaseOrdersDespacho>> ShowallAsync(decimal codEntidad, string numPurchaseOrder);
    Task<EntidadesPurchaseOrdersDespacho?> ShowbyidAsync(decimal codEntidad, string numPurchaseOrder, int numSecuencia);
    Task<IEnumerable<EntidadesPurchaseOrdersDespacho>> SearchAsync(EntidadesPurchaseOrdersDespachoSearchDto dto);
    Task<WriteResult> InsertAsync(EntidadesPurchaseOrdersDespachoInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(EntidadesPurchaseOrdersDespachoUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(decimal codEntidad, string numPurchaseOrder, int numSecuencia);
}

public interface IEntidadesPurchaseOrdersDespachoService
{
    Task<ApiResponse<IEnumerable<EntidadesPurchaseOrdersDespacho>>> GetAllAsync(decimal codEntidad, string numPurchaseOrder);
    Task<ApiResponse<EntidadesPurchaseOrdersDespacho>> GetByIdAsync(decimal codEntidad, string numPurchaseOrder, int numSecuencia);
    Task<ApiResponse<IEnumerable<EntidadesPurchaseOrdersDespacho>>> SearchAsync(EntidadesPurchaseOrdersDespachoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(EntidadesPurchaseOrdersDespachoInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(EntidadesPurchaseOrdersDespachoUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(decimal codEntidad, string numPurchaseOrder, int numSecuencia);
}


// ── Especificación técnica ──────────────────────────────────────────────────
public interface IEntidadesPurchaseOrdersEspecificacionTecnicaRepository
{
    Task<IEnumerable<EntidadesPurchaseOrdersEspecificacionTecnica>> ShowallAsync(decimal codEntidad, string numPurchaseOrder, int numSecuencia);
    Task<EntidadesPurchaseOrdersEspecificacionTecnica?> ShowbyidAsync(decimal codEntidad, string numPurchaseOrder, int numSecuencia, string codEspecificacion);
    Task<IEnumerable<EntidadesPurchaseOrdersEspecificacionTecnica>> SearchAsync(EntidadesPurchaseOrdersEspecificacionTecnicaSearchDto dto);
    Task<WriteResult> InsertAsync(EntidadesPurchaseOrdersEspecificacionTecnicaInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(EntidadesPurchaseOrdersEspecificacionTecnicaUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(decimal codEntidad, string numPurchaseOrder, int numSecuencia, string codEspecificacion);
}

public interface IEntidadesPurchaseOrdersEspecificacionTecnicaService
{
    Task<ApiResponse<IEnumerable<EntidadesPurchaseOrdersEspecificacionTecnica>>> GetAllAsync(decimal codEntidad, string numPurchaseOrder, int numSecuencia);
    Task<ApiResponse<EntidadesPurchaseOrdersEspecificacionTecnica>> GetByIdAsync(decimal codEntidad, string numPurchaseOrder, int numSecuencia, string codEspecificacion);
    Task<ApiResponse<IEnumerable<EntidadesPurchaseOrdersEspecificacionTecnica>>> SearchAsync(EntidadesPurchaseOrdersEspecificacionTecnicaSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(EntidadesPurchaseOrdersEspecificacionTecnicaInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(EntidadesPurchaseOrdersEspecificacionTecnicaUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(decimal codEntidad, string numPurchaseOrder, int numSecuencia, string codEspecificacion);
}
