using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

public interface IInventariosAlmacenesPuntoTomaEquiposAsignadosRepository
{
    Task<IEnumerable<InventariosAlmacenesPuntoTomaEquiposAsignados>> ShowallAsync(int numInventario, string codAlmacen, byte numPuntoTomaInventario);
    Task<InventariosAlmacenesPuntoTomaEquiposAsignados?> ShowbyidAsync(int numInventario, string codAlmacen, byte numPuntoTomaInventario, string codProductoEquipo);
    // OJO: pese al nombre, el SP _SHOWCOMBOX de esta tabla devuelve todas las columnas (igual
    // que Showall), no Id/Texto — se expone con el mismo tipo de retorno que ShowallAsync.
    Task<IEnumerable<InventariosAlmacenesPuntoTomaEquiposAsignados>> ShowcomboxAsync(int numInventario, string codAlmacen, byte numPuntoTomaInventario);
    Task<IEnumerable<InventariosAlmacenesPuntoTomaEquiposAsignados>> SearchAsync(InventariosAlmacenesPuntoTomaEquiposAsignadosSearchDto dto);
    Task<WriteResult> InsertAsync(InventariosAlmacenesPuntoTomaEquiposAsignadosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(InventariosAlmacenesPuntoTomaEquiposAsignadosUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int numInventario, string codAlmacen, byte numPuntoTomaInventario, string codProductoEquipo);
}

public interface IInventariosAlmacenesPuntoTomaEquiposAsignadosService
{
    Task<ApiResponse<IEnumerable<InventariosAlmacenesPuntoTomaEquiposAsignados>>> GetAllAsync(int numInventario, string codAlmacen, byte numPuntoTomaInventario);
    Task<ApiResponse<InventariosAlmacenesPuntoTomaEquiposAsignados>> GetByIdAsync(int numInventario, string codAlmacen, byte numPuntoTomaInventario, string codProductoEquipo);
    Task<ApiResponse<IEnumerable<InventariosAlmacenesPuntoTomaEquiposAsignados>>> GetComboboxAsync(int numInventario, string codAlmacen, byte numPuntoTomaInventario);
    Task<ApiResponse<IEnumerable<InventariosAlmacenesPuntoTomaEquiposAsignados>>> SearchAsync(InventariosAlmacenesPuntoTomaEquiposAsignadosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(InventariosAlmacenesPuntoTomaEquiposAsignadosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(InventariosAlmacenesPuntoTomaEquiposAsignadosUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int numInventario, string codAlmacen, byte numPuntoTomaInventario, string codProductoEquipo);
}
