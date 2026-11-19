namespace KonSolutionsApi.Common;

public static class SP
{
    public static class SCHEMAActivos
    {
        public static class Activos
        {
            public const string Showall = "PROC_ACTIVOS_ACTIVOS_SHOWALL";
            public const string Showbyid = "PROC_ACTIVOS_ACTIVOS_SHOWBYID";
            public const string Showcombox = "PROC_ACTIVOS_ACTIVOS_SHOWCOMBOX";
            public const string Search = "PROC_ACTIVOS_ACTIVOS_SEARCH";
            public const string Insert = "PROC_ACTIVOS_ACTIVOS_INSERT";
            public const string Update = "PROC_ACTIVOS_ACTIVOS_UPDATE";
            public const string Delete = "PROC_ACTIVOS_ACTIVOS_DELETE";
        }
        public static class ActivosEntidades
        {
            public const string Showall = "PROC_ACTIVOS_ACTIVOS_ENTIDADES_SHOWALL";
            public const string Showbyid = "PROC_ACTIVOS_ACTIVOS_ENTIDADES_SHOWBYID";
            public const string Showcombox = "PROC_ACTIVOS_ACTIVOS_ENTIDADES_SHOWCOMBOX";
            public const string Search = "PROC_ACTIVOS_ACTIVOS_ENTIDADES_SEARCH";
            public const string Insert = "PROC_ACTIVOS_ACTIVOS_ENTIDADES_INSERT";
            public const string Update = "PROC_ACTIVOS_ACTIVOS_ENTIDADES_UPDATE";
            public const string Delete = "PROC_ACTIVOS_ACTIVOS_ENTIDADES_DELETE";
        }
        public static class ActivosEspecificacionesTecnicas
        {
            public const string Showall = "PROC_ACTIVOS_ACTIVOS_ESPECIFICACIONES_TECNICAS_SHOWALL";
            public const string Showbyid = "PROC_ACTIVOS_ACTIVOS_ESPECIFICACIONES_TECNICAS_SHOWBYID";
            public const string Showcombox = "PROC_ACTIVOS_ACTIVOS_ESPECIFICACIONES_TECNICAS_SHOWCOMBOX";
            public const string Search = "PROC_ACTIVOS_ACTIVOS_ESPECIFICACIONES_TECNICAS_SEARCH";
            public const string Insert = "PROC_ACTIVOS_ACTIVOS_ESPECIFICACIONES_TECNICAS_INSERT";
            public const string Update = "PROC_ACTIVOS_ACTIVOS_ESPECIFICACIONES_TECNICAS_UPDATE";
            public const string Delete = "PROC_ACTIVOS_ACTIVOS_ESPECIFICACIONES_TECNICAS_DELETE";
        }
        public static class ActivosValorizacion
        {
            public const string Showall = "PROC_ACTIVOS_ACTIVOS_VALORIZACION_SHOWALL";
            public const string Showbyid = "PROC_ACTIVOS_ACTIVOS_VALORIZACION_SHOWBYID";
            public const string Showcombox = "PROC_ACTIVOS_ACTIVOS_VALORIZACION_SHOWCOMBOX";
            public const string Search = "PROC_ACTIVOS_ACTIVOS_VALORIZACION_SEARCH";
            public const string Insert = "PROC_ACTIVOS_ACTIVOS_VALORIZACION_INSERT";
            public const string Update = "PROC_ACTIVOS_ACTIVOS_VALORIZACION_UPDATE";
            public const string Delete = "PROC_ACTIVOS_ACTIVOS_VALORIZACION_DELETE";
        }
        public static class ProcesosLineasEspecificacionesTecnicas
        {
            public const string Showall = "PROC_ACTIVOS_PROCESOS_LINEAS_ESPECIFICACIONES_TECNICAS_SHOWALL";
            public const string Showbyid = "PROC_ACTIVOS_PROCESOS_LINEAS_ESPECIFICACIONES_TECNICAS_SHOWBYID";
            public const string Showcombox = "PROC_ACTIVOS_PROCESOS_LINEAS_ESPECIFICACIONES_TECNICAS_SHOWCOMBOX";
            public const string Search = "PROC_ACTIVOS_PROCESOS_LINEAS_ESPECIFICACIONES_TECNICAS_SEARCH";
            public const string Insert = "PROC_ACTIVOS_PROCESOS_LINEAS_ESPECIFICACIONES_TECNICAS_INSERT";
            public const string Update = "PROC_ACTIVOS_PROCESOS_LINEAS_ESPECIFICACIONES_TECNICAS_UPDATE";
            public const string Delete = "PROC_ACTIVOS_PROCESOS_LINEAS_ESPECIFICACIONES_TECNICAS_DELETE";
        }
        public static class TiposActivo
        {
            public const string Showall = "PROC_ACTIVOS_TIPOS_ACTIVO_SHOWALL";
            public const string Showbyid = "PROC_ACTIVOS_TIPOS_ACTIVO_SHOWBYID";
            public const string Showcombox = "PROC_ACTIVOS_TIPOS_ACTIVO_SHOWCOMBOX";
            public const string Search = "PROC_ACTIVOS_TIPOS_ACTIVO_SEARCH";
            public const string Insert = "PROC_ACTIVOS_TIPOS_ACTIVO_INSERT";
            public const string Update = "PROC_ACTIVOS_TIPOS_ACTIVO_UPDATE";
            public const string Delete = "PROC_ACTIVOS_TIPOS_ACTIVO_DELETE";
        }
    }
    public static class SCHEMAAdministracion
    {
        public static class Areas
        {
            public const string Showall = "PROC_ADMINISTRACION_AREAS_SHOWALL";
            public const string Showbyid = "PROC_ADMINISTRACION_AREAS_SHOWBYID";
            public const string Showcombox = "PROC_ADMINISTRACION_AREAS_SHOWCOMBOX";
            public const string Search = "PROC_ADMINISTRACION_AREAS_SEARCH";
            public const string Insert = "PROC_ADMINISTRACION_AREAS_INSERT";
            public const string Update = "PROC_ADMINISTRACION_AREAS_UPDATE";
            public const string Delete = "PROC_ADMINISTRACION_AREAS_DELETE";
            public const string Loadcombos = "PROC_ADMINISTRACION_AREAS_LOADCOMBOS";
            // Cargos activos de un área específica — usado para poblar el combo "Cargo" al
            // asignar cargos a un tipo de solicitud comercial (COMERCIAL.TIPOS_SOLICITUD_COMERCIAL_AREAS_CARGOS).
            public const string TreeviewCargos = "PROC_ADMINISTRACION_AREAS_CARGOS_TREEVIEW";
        }
        public static class Cargos
        {
            public const string Showall = "PROC_ADMINISTRACION_CARGOS_SHOWALL";
            public const string Showbyid = "PROC_ADMINISTRACION_CARGOS_SHOWBYID";
            public const string Showcombox = "PROC_ADMINISTRACION_CARGOS_SHOWCOMBOX";
            public const string Search = "PROC_ADMINISTRACION_CARGOS_SEARCH";
            public const string Insert = "PROC_ADMINISTRACION_CARGOS_INSERT";
            public const string Update = "PROC_ADMINISTRACION_CARGOS_UPDATE";
            public const string Delete = "PROC_ADMINISTRACION_CARGOS_DELETE";
        }
        public static class TiposCargoAdministrativo
        {
            public const string Showall = "PROC_ADMINISTRACION_TIPOS_CARGO_ADMINISTRATIVO_SHOWALL";
            public const string Showbyid = "PROC_ADMINISTRACION_TIPOS_CARGO_ADMINISTRATIVO_SHOWBYID";
            public const string Showcombox = "PROC_ADMINISTRACION_TIPOS_CARGO_ADMINISTRATIVO_SHOWCOMBOX";
            public const string Search = "PROC_ADMINISTRACION_TIPOS_CARGO_ADMINISTRATIVO_SEARCH";
            public const string Insert = "PROC_ADMINISTRACION_TIPOS_CARGO_ADMINISTRATIVO_INSERT";
            public const string Update = "PROC_ADMINISTRACION_TIPOS_CARGO_ADMINISTRATIVO_UPDATE";
            public const string Delete = "PROC_ADMINISTRACION_TIPOS_CARGO_ADMINISTRATIVO_DELETE";
        }
        // Tabla puente: entidades (personas) asignadas a un cargo dentro de un área
        // (ADMINISTRACION.AREAS_CARGOS_ENTIDADES).
        public static class AreasCargosEntidades
        {
            public const string Showall = "PROC_ADMINISTRACION_AREAS_CARGOS_ENTIDADES_SHOWALL";
            public const string Showbyid = "PROC_ADMINISTRACION_AREAS_CARGOS_ENTIDADES_SHOWBYID";
            public const string Showcombox = "PROC_ADMINISTRACION_AREAS_CARGOS_ENTIDADES_SHOWCOMBOX";
            public const string Search = "PROC_ADMINISTRACION_AREAS_CARGOS_ENTIDADES_SEARCH";
            public const string Insert = "PROC_ADMINISTRACION_AREAS_CARGOS_ENTIDADES_INSERT";
            public const string Delete = "PROC_ADMINISTRACION_AREAS_CARGOS_ENTIDADES_DELETE";
            // Árbol completo Área > Cargo > Entidad asignada, sin parámetros.
            public const string Treeview = "PROC_ADMINISTRACION_AREAS_CARGOS_ENTIDADES_TREEVIEW";
            // Las personas de un CENTRO DE COSTO, alcanzadas por su área. Vive en esta clase y
            // no en una propia porque es un solo procedimiento, pero ojo con el prefijo: es
            // AREAS_CENTROS_COSTO_ENTIDADES, no AREAS_CARGOS_ENTIDADES como el resto.
            public const string EntidadesPorCentroCosto = "PROC_ADMINISTRACION_AREAS_CENTROS_COSTO_ENTIDADES_SHOWALL";
            // Personas asignadas al Área enlazada a una opción de menú puntual — recibe
            // @COD_APLICACION/@COD_OPCION_APLICACION directo y resuelve el Área internamente
            // (join contra ESTANDAR.APLICACIONES_OPCIONES_AREAS). Usado para el combo de
            // Asesores Comerciales en EntidadesDialog.razor.
            public const string PorOpcion = "PROC_ADMINISTRACION_AREAS_CARGOS_ENTIDADES";
        }
    }
    public static class SCHEMAAprobaciones
    {
        public static class Aprobaciones
        {
            public const string Showall = "PROC_APROBACIONES_APROBACIONES_SHOWALL";
            public const string Showbyid = "PROC_APROBACIONES_APROBACIONES_SHOWBYID";
            public const string Showcombox = "PROC_APROBACIONES_APROBACIONES_SHOWCOMBOX";
            public const string Search = "PROC_APROBACIONES_APROBACIONES_SEARCH";
            public const string Insert = "PROC_APROBACIONES_APROBACIONES_INSERT";
            public const string Update = "PROC_APROBACIONES_APROBACIONES_UPDATE";
            public const string Delete = "PROC_APROBACIONES_APROBACIONES_DELETE";
        }
        public static class AprobacionesDetalle
        {
            public const string Showall = "PROC_APROBACIONES_APROBACIONES_DETALLE_SHOWALL";
            public const string Showbyid = "PROC_APROBACIONES_APROBACIONES_DETALLE_SHOWBYID";
            public const string Showcombox = "PROC_APROBACIONES_APROBACIONES_DETALLE_SHOWCOMBOX";
            public const string Search = "PROC_APROBACIONES_APROBACIONES_DETALLE_SEARCH";
            public const string Insert = "PROC_APROBACIONES_APROBACIONES_DETALLE_INSERT";
            public const string Update = "PROC_APROBACIONES_APROBACIONES_DETALLE_UPDATE";
            public const string Delete = "PROC_APROBACIONES_APROBACIONES_DETALLE_DELETE";
        }
        public static class TiposAprobacion
        {
            public const string Showall = "PROC_APROBACIONES_TIPOS_APROBACION_SHOWALL";
            public const string Showbyid = "PROC_APROBACIONES_TIPOS_APROBACION_SHOWBYID";
            public const string Showcombox = "PROC_APROBACIONES_TIPOS_APROBACION_SHOWCOMBOX";
            public const string Search = "PROC_APROBACIONES_TIPOS_APROBACION_SEARCH";
            public const string Insert = "PROC_APROBACIONES_TIPOS_APROBACION_INSERT";
            public const string Update = "PROC_APROBACIONES_TIPOS_APROBACION_UPDATE";
            public const string Delete = "PROC_APROBACIONES_TIPOS_APROBACION_DELETE";
        }
    }
    public static class SCHEMAComercial
    {

        // ── Purchase orders del cliente (COMERCIAL.ENTIDADES_PURCHARSE_ORDERS) ──────────
        // OJO: el nombre de la tabla tiene el typo "PURCHARSE" y así están creados los SP.
        // Las constantes lo respetan porque tienen que coincidir con la base; las clases de
        // C# van con la ortografía correcta.
        public static class EntidadesPurchaseOrders
        {
            public const string Showall    = "PROC_COMERCIAL_ENTIDADES_PURCHARSE_ORDERS_SHOWALL";
            public const string Showbyid   = "PROC_COMERCIAL_ENTIDADES_PURCHARSE_ORDERS_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_ENTIDADES_PURCHARSE_ORDERS_SHOWCOMBOX";
            public const string Search     = "PROC_COMERCIAL_ENTIDADES_PURCHARSE_ORDERS_SEARCH";
            public const string Insert     = "PROC_COMERCIAL_ENTIDADES_PURCHARSE_ORDERS_INSERT";
            public const string Update     = "PROC_COMERCIAL_ENTIDADES_PURCHARSE_ORDERS_UPDATE";
            public const string Delete     = "PROC_COMERCIAL_ENTIDADES_PURCHARSE_ORDERS_DELETE";
        }
        public static class EntidadesPurchaseOrdersDespacho
        {
            public const string Showall    = "PROC_COMERCIAL_ENTIDADES_PURCHARSE_ORDERS_ESPECIFICACIONES_DESPACHO_SHOWALL";
            public const string Showbyid   = "PROC_COMERCIAL_ENTIDADES_PURCHARSE_ORDERS_ESPECIFICACIONES_DESPACHO_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_ENTIDADES_PURCHARSE_ORDERS_ESPECIFICACIONES_DESPACHO_SHOWCOMBOX";
            public const string Search     = "PROC_COMERCIAL_ENTIDADES_PURCHARSE_ORDERS_ESPECIFICACIONES_DESPACHO_SEARCH";
            public const string Insert     = "PROC_COMERCIAL_ENTIDADES_PURCHARSE_ORDERS_ESPECIFICACIONES_DESPACHO_INSERT";
            public const string Update     = "PROC_COMERCIAL_ENTIDADES_PURCHARSE_ORDERS_ESPECIFICACIONES_DESPACHO_UPDATE";
            public const string Delete     = "PROC_COMERCIAL_ENTIDADES_PURCHARSE_ORDERS_ESPECIFICACIONES_DESPACHO_DELETE";
        }
        public static class EntidadesPurchaseOrdersEspecificacionTecnica
        {
            // Estos tres nombres están CORTADOS en la base: _SHOWAL (sin la L final),
            // _SHOWBY (sin ID) y _SHOWCO (sin MBOX). No es un error de tipeo de acá: así los
            // creó la herramienta que los generó, y ninguno llega a los 128 caracteres que
            // permite SQL Server. Ver el LEEME: conviene renombrarlos, y cuando se haga hay
            // que actualizar estas tres líneas.
            public const string Showall    = "PROC_COMERCIAL_ENTIDADES_PURCHARSE_ORDERS_ESPECIFICACIONES_DESPACHO_ESPECIFICACIONES_TECNICAS_SHOWAL";
            public const string Showbyid   = "PROC_COMERCIAL_ENTIDADES_PURCHARSE_ORDERS_ESPECIFICACIONES_DESPACHO_ESPECIFICACIONES_TECNICAS_SHOWBY";
            public const string Showcombox = "PROC_COMERCIAL_ENTIDADES_PURCHARSE_ORDERS_ESPECIFICACIONES_DESPACHO_ESPECIFICACIONES_TECNICAS_SHOWCO";
            public const string Search     = "PROC_COMERCIAL_ENTIDADES_PURCHARSE_ORDERS_ESPECIFICACIONES_DESPACHO_ESPECIFICACIONES_TECNICAS_SEARCH";
            public const string Insert     = "PROC_COMERCIAL_ENTIDADES_PURCHARSE_ORDERS_ESPECIFICACIONES_DESPACHO_ESPECIFICACIONES_TECNICAS_INSERT";
            public const string Update     = "PROC_COMERCIAL_ENTIDADES_PURCHARSE_ORDERS_ESPECIFICACIONES_DESPACHO_ESPECIFICACIONES_TECNICAS_UPDATE";
            public const string Delete     = "PROC_COMERCIAL_ENTIDADES_PURCHARSE_ORDERS_ESPECIFICACIONES_DESPACHO_ESPECIFICACIONES_TECNICAS_DELETE";
        }
        public static class EntidadesUnidadesComerciales
        {
            public const string Showall    = "PROC_COMERCIAL_ENTIDADES_UNIDADES_COMERCIALES_SHOWALL";
            public const string Showbyid   = "PROC_COMERCIAL_ENTIDADES_UNIDADES_COMERCIALES_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_ENTIDADES_UNIDADES_COMERCIALES_SHOWCOMBOX";
            public const string Search     = "PROC_COMERCIAL_ENTIDADES_UNIDADES_COMERCIALES_SEARCH";
            public const string Insert     = "PROC_COMERCIAL_ENTIDADES_UNIDADES_COMERCIALES_INSERT";
            public const string Update     = "PROC_COMERCIAL_ENTIDADES_UNIDADES_COMERCIALES_UPDATE";
            public const string Delete     = "PROC_COMERCIAL_ENTIDADES_UNIDADES_COMERCIALES_DELETE";
        }
        public static class AutorizacionesPedidosComerciales
        {
            public const string Showall = "PROC_COMERCIAL_AUTORIZACIONES_PEDIDOS_COMERCIALES_SHOWALL";
            public const string Showbyid = "PROC_COMERCIAL_AUTORIZACIONES_PEDIDOS_COMERCIALES_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_AUTORIZACIONES_PEDIDOS_COMERCIALES_SHOWCOMBOX";
            public const string Search = "PROC_COMERCIAL_AUTORIZACIONES_PEDIDOS_COMERCIALES_SEARCH";
            public const string Insert = "PROC_COMERCIAL_AUTORIZACIONES_PEDIDOS_COMERCIALES_INSERT";
            public const string Update = "PROC_COMERCIAL_AUTORIZACIONES_PEDIDOS_COMERCIALES_UPDATE";
            public const string Delete = "PROC_COMERCIAL_AUTORIZACIONES_PEDIDOS_COMERCIALES_DELETE";
        }
        public static class EntidadesInformacionComercial
        {
            public const string Showall = "PROC_COMERCIAL_ENTIDADES_INFORMACION_COMERCIAL_SHOWALL";
            public const string Showbyid = "PROC_COMERCIAL_ENTIDADES_INFORMACION_COMERCIAL_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_ENTIDADES_INFORMACION_COMERCIAL_SHOWCOMBOX";
            public const string Search = "PROC_COMERCIAL_ENTIDADES_INFORMACION_COMERCIAL_SEARCH";
            public const string Insert = "PROC_COMERCIAL_ENTIDADES_INFORMACION_COMERCIAL_INSERT";
            public const string Update = "PROC_COMERCIAL_ENTIDADES_INFORMACION_COMERCIAL_UPDATE";
            public const string Delete = "PROC_COMERCIAL_ENTIDADES_INFORMACION_COMERCIAL_DELETE";
        }

        public static class EntidadesAsesoresComerciales
        {
            public const string Showall = "PROC_COMERCIAL_ENTIDADES_ASESORES_COMERCIALES_SHOWALL";
            public const string Showbyid = "PROC_COMERCIAL_ENTIDADES_ASESORES_COMERCIALES_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_ENTIDADES_ASESORES_COMERCIALES_SHOWCOMBOX";
            public const string Search = "PROC_COMERCIAL_ENTIDADES_ASESORES_COMERCIALES_SEARCH";
            public const string Insert = "PROC_COMERCIAL_ENTIDADES_ASESORES_COMERCIALES_INSERT";
            public const string Delete = "PROC_COMERCIAL_ENTIDADES_ASESORES_COMERCIALES_DELETE";
        }
        public static class CanalesVenta
        {
            public const string Showall = "PROC_COMERCIAL_CANALES_VENTA_SHOWALL";
            public const string Showbyid = "PROC_COMERCIAL_CANALES_VENTA_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_CANALES_VENTA_SHOWCOMBOX";
            public const string Search = "PROC_COMERCIAL_CANALES_VENTA_SEARCH";
            public const string Insert = "PROC_COMERCIAL_CANALES_VENTA_INSERT";
            public const string Update = "PROC_COMERCIAL_CANALES_VENTA_UPDATE";
            public const string Delete = "PROC_COMERCIAL_CANALES_VENTA_DELETE";
        }
        public static class CatalogosPrecios
        {
            public const string Showall = "PROC_COMERCIAL_CATALOGOS_PRECIOS_SHOWALL";
            public const string Showbyid = "PROC_COMERCIAL_CATALOGOS_PRECIOS_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_CATALOGOS_PRECIOS_SHOWCOMBOX";
            public const string Search = "PROC_COMERCIAL_CATALOGOS_PRECIOS_SEARCH";
            public const string Insert = "PROC_COMERCIAL_CATALOGOS_PRECIOS_INSERT";
            public const string Update = "PROC_COMERCIAL_CATALOGOS_PRECIOS_UPDATE";
            public const string Delete = "PROC_COMERCIAL_CATALOGOS_PRECIOS_DELETE";
        }
        public static class CatalogosPreciosDetalle
        {
            public const string Showall = "PROC_COMERCIAL_CATALOGOS_PRECIOS_DETALLE_SHOWALL";
            public const string Showbyid = "PROC_COMERCIAL_CATALOGOS_PRECIOS_DETALLE_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_CATALOGOS_PRECIOS_DETALLE_SHOWCOMBOX";
            public const string Search = "PROC_COMERCIAL_CATALOGOS_PRECIOS_DETALLE_SEARCH";
            public const string Insert = "PROC_COMERCIAL_CATALOGOS_PRECIOS_DETALLE_INSERT";
            public const string Update = "PROC_COMERCIAL_CATALOGOS_PRECIOS_DETALLE_UPDATE";
            public const string Delete = "PROC_COMERCIAL_CATALOGOS_PRECIOS_DETALLE_DELETE";
        }
        public static class CatalogosPreciosDetalleCombo
        {
            public const string Showall = "PROC_COMERCIAL_CATALOGOS_PRECIOS_DETALLE_COMBO_SHOWALL";
            public const string Showbyid = "PROC_COMERCIAL_CATALOGOS_PRECIOS_DETALLE_COMBO_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_CATALOGOS_PRECIOS_DETALLE_COMBO_SHOWCOMBOX";
            public const string Search = "PROC_COMERCIAL_CATALOGOS_PRECIOS_DETALLE_COMBO_SEARCH";
            public const string Insert = "PROC_COMERCIAL_CATALOGOS_PRECIOS_DETALLE_COMBO_INSERT";
            public const string Update = "PROC_COMERCIAL_CATALOGOS_PRECIOS_DETALLE_COMBO_UPDATE";
            public const string Delete = "PROC_COMERCIAL_CATALOGOS_PRECIOS_DETALLE_COMBO_DELETE";
        }
        public static class CondicionesPago
        {
            public const string Showall = "PROC_COMERCIAL_CONDICIONES_PAGO_SHOWALL";
            public const string Showbyid = "PROC_COMERCIAL_CONDICIONES_PAGO_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_CONDICIONES_PAGO_SHOWCOMBOX";
            public const string Search = "PROC_COMERCIAL_CONDICIONES_PAGO_SEARCH";
            public const string Insert = "PROC_COMERCIAL_CONDICIONES_PAGO_INSERT";
            public const string Update = "PROC_COMERCIAL_CONDICIONES_PAGO_UPDATE";
            public const string Delete = "PROC_COMERCIAL_CONDICIONES_PAGO_DELETE";
        }
        public static class ConfiguracionPedidosComerciales
        {
            public const string Showall = "PROC_COMERCIAL_CONFIGURACION_PEDIDOS_COMERCIALES_SHOWALL";
            public const string Showbyid = "PROC_COMERCIAL_CONFIGURACION_PEDIDOS_COMERCIALES_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_CONFIGURACION_PEDIDOS_COMERCIALES_SHOWCOMBOX";
            public const string Search = "PROC_COMERCIAL_CONFIGURACION_PEDIDOS_COMERCIALES_SEARCH";
            public const string Insert = "PROC_COMERCIAL_CONFIGURACION_PEDIDOS_COMERCIALES_INSERT";
            public const string Update = "PROC_COMERCIAL_CONFIGURACION_PEDIDOS_COMERCIALES_UPDATE";
            public const string Delete = "PROC_COMERCIAL_CONFIGURACION_PEDIDOS_COMERCIALES_DELETE";
        }
        public static class EntidadesDirecciones
        {
            public const string Showall = "PROC_COMERCIAL_ENTIDADES_DIRECCIONES_SHOWALL";
            public const string Showbyid = "PROC_COMERCIAL_ENTIDADES_DIRECCIONES_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_ENTIDADES_DIRECCIONES_SHOWCOMBOX";
            public const string Search = "PROC_COMERCIAL_ENTIDADES_DIRECCIONES_SEARCH";
            public const string Insert = "PROC_COMERCIAL_ENTIDADES_DIRECCIONES_INSERT";
            public const string Update = "PROC_COMERCIAL_ENTIDADES_DIRECCIONES_UPDATE";
            public const string Delete = "PROC_COMERCIAL_ENTIDADES_DIRECCIONES_DELETE";
            // Bundlea TiposDireccion + TiposZona (Rrhh) + TiposVia + Estado en un solo
            // viaje (ver scratchpad/loadcombos_agenteE.sql).
            public const string Loadcombos = "PROC_COMERCIAL_ENTIDADES_DIRECCIONES_LOADCOMBOS";
        }
        // Vínculo del pedido comercial con los requerimientos de stock que lo originan.
        // Sólo las tres operaciones que usa la ficha: listar, vincular y desvincular.
        // Comparativo de compras: los requerimientos elegibles y la comparación en sí.
        public static class PedidosComercialesComparativo
        {
            public const string RequerimientosConPedidos = "PROC_COMERCIAL_REQUERIMIENTOS_CON_PEDIDOS";
            public const string Comparativo              = "PROC_COMERCIAL_PEDIDOS_COMERCIALES_COMPARATIVO";
        }
        public static class PedidosComercialesEstados
        {
            public const string Showall = "PROC_COMERCIAL_PEDIDOS_COMERCIALES_ESTADOS_SHOWALL";
        }
        public static class PedidosComercialesRequerimientosStock
        {
            public const string Showall = "PROC_COMERCIAL_PEDIDOS_COMERCIALES_REQUERIMIENTOS_STOCK_SHOWALL";
            public const string Insert  = "PROC_COMERCIAL_PEDIDOS_COMERCIALES_REQUERIMIENTOS_STOCK_INSERT";
            public const string Delete  = "PROC_COMERCIAL_PEDIDOS_COMERCIALES_REQUERIMIENTOS_STOCK_DELETE";
        }
        public static class PedidosComercialesDetalleRequerimientosStockDetalle
        {
            public const string Showall = "PROC_COMERCIAL_PEDIDOS_COMERCIALES_DETALLE_REQUERIMIENTOS_STOCK_DETALLE_SHOWALL";
            public const string Insert  = "PROC_COMERCIAL_PEDIDOS_COMERCIALES_DETALLE_REQUERIMIENTOS_STOCK_DETALLE_INSERT";
            public const string Delete  = "PROC_COMERCIAL_PEDIDOS_COMERCIALES_DETALLE_REQUERIMIENTOS_STOCK_DETALLE_DELETE";
        }
        public static class EntidadesProductos
        {
            public const string Showall = "PROC_COMERCIAL_ENTIDADES_PRODUCTOS_SHOWALL";
            public const string Showbyid = "PROC_COMERCIAL_ENTIDADES_PRODUCTOS_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_ENTIDADES_PRODUCTOS_SHOWCOMBOX";
            public const string Search = "PROC_COMERCIAL_ENTIDADES_PRODUCTOS_SEARCH";
            public const string Insert = "PROC_COMERCIAL_ENTIDADES_PRODUCTOS_INSERT";
            public const string Update = "PROC_COMERCIAL_ENTIDADES_PRODUCTOS_UPDATE";
            public const string Delete = "PROC_COMERCIAL_ENTIDADES_PRODUCTOS_DELETE";
        }
        public static class EntidadesProductosTiposPrecios
        {
            public const string Showall = "PROC_COMERCIAL_ENTIDADES_PRODUCTOS_TIPOS_PRECIOS_SHOWALL";
            public const string Showbyid = "PROC_COMERCIAL_ENTIDADES_PRODUCTOS_TIPOS_PRECIOS_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_ENTIDADES_PRODUCTOS_TIPOS_PRECIOS_SHOWCOMBOX";
            public const string Search = "PROC_COMERCIAL_ENTIDADES_PRODUCTOS_TIPOS_PRECIOS_SEARCH";
            public const string Insert = "PROC_COMERCIAL_ENTIDADES_PRODUCTOS_TIPOS_PRECIOS_INSERT";
            public const string Update = "PROC_COMERCIAL_ENTIDADES_PRODUCTOS_TIPOS_PRECIOS_UPDATE";
            public const string Delete = "PROC_COMERCIAL_ENTIDADES_PRODUCTOS_TIPOS_PRECIOS_DELETE";
        }
        public static class EntidadesPurcharseOrders
        {
            public const string Showall = "PROC_COMERCIAL_ENTIDADES_PURCHARSE_ORDERS_SHOWALL";
            public const string Showbyid = "PROC_COMERCIAL_ENTIDADES_PURCHARSE_ORDERS_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_ENTIDADES_PURCHARSE_ORDERS_SHOWCOMBOX";
            public const string Search = "PROC_COMERCIAL_ENTIDADES_PURCHARSE_ORDERS_SEARCH";
            public const string Insert = "PROC_COMERCIAL_ENTIDADES_PURCHARSE_ORDERS_INSERT";
            public const string Update = "PROC_COMERCIAL_ENTIDADES_PURCHARSE_ORDERS_UPDATE";
            public const string Delete = "PROC_COMERCIAL_ENTIDADES_PURCHARSE_ORDERS_DELETE";
        }
        public static class EntidadesPurcharseOrdersEspecificacionesDespacho
        {
            public const string Showall = "PROC_COMERCIAL_ENTIDADES_PURCHARSE_ORDERS_ESPECIFICACIONES_DESPACHO_SHOWALL";
            public const string Showbyid = "PROC_COMERCIAL_ENTIDADES_PURCHARSE_ORDERS_ESPECIFICACIONES_DESPACHO_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_ENTIDADES_PURCHARSE_ORDERS_ESPECIFICACIONES_DESPACHO_SHOWCOMBOX";
            public const string Search = "PROC_COMERCIAL_ENTIDADES_PURCHARSE_ORDERS_ESPECIFICACIONES_DESPACHO_SEARCH";
            public const string Insert = "PROC_COMERCIAL_ENTIDADES_PURCHARSE_ORDERS_ESPECIFICACIONES_DESPACHO_INSERT";
            public const string Update = "PROC_COMERCIAL_ENTIDADES_PURCHARSE_ORDERS_ESPECIFICACIONES_DESPACHO_UPDATE";
            public const string Delete = "PROC_COMERCIAL_ENTIDADES_PURCHARSE_ORDERS_ESPECIFICACIONES_DESPACHO_DELETE";
        }
        public static class EntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicas
        {
            public const string Showall = "PROC_COMERCIAL_ENTIDADES_PURCHARSE_ORDERS_ESPECIFICACIONES_DESPACHO_ESPECIFICACIONES_TECNICAS_SHOWAL";
            public const string Showbyid = "PROC_COMERCIAL_ENTIDADES_PURCHARSE_ORDERS_ESPECIFICACIONES_DESPACHO_ESPECIFICACIONES_TECNICAS_SHOWBY";
            public const string Showcombox = "PROC_COMERCIAL_ENTIDADES_PURCHARSE_ORDERS_ESPECIFICACIONES_DESPACHO_ESPECIFICACIONES_TECNICAS_SHOWCO";
            public const string Search = "PROC_COMERCIAL_ENTIDADES_PURCHARSE_ORDERS_ESPECIFICACIONES_DESPACHO_ESPECIFICACIONES_TECNICAS_SEARCH";
            public const string Insert = "PROC_COMERCIAL_ENTIDADES_PURCHARSE_ORDERS_ESPECIFICACIONES_DESPACHO_ESPECIFICACIONES_TECNICAS_INSERT";
            public const string Update = "PROC_COMERCIAL_ENTIDADES_PURCHARSE_ORDERS_ESPECIFICACIONES_DESPACHO_ESPECIFICACIONES_TECNICAS_UPDATE";
            public const string Delete = "PROC_COMERCIAL_ENTIDADES_PURCHARSE_ORDERS_ESPECIFICACIONES_DESPACHO_ESPECIFICACIONES_TECNICAS_DELETE";
        }
        public static class EntidadesTemporadas
        {
            public const string Showall = "PROC_COMERCIAL_ENTIDADES_TEMPORADAS_SHOWALL";
            public const string Showbyid = "PROC_COMERCIAL_ENTIDADES_TEMPORADAS_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_ENTIDADES_TEMPORADAS_SHOWCOMBOX";
            public const string Search = "PROC_COMERCIAL_ENTIDADES_TEMPORADAS_SEARCH";
            public const string Insert = "PROC_COMERCIAL_ENTIDADES_TEMPORADAS_INSERT";
            public const string Delete = "PROC_COMERCIAL_ENTIDADES_TEMPORADAS_DELETE";
            public const string Loadcombos = "PROC_COMERCIAL_ENTIDADES_TEMPORADAS_LOADCOMBOS";
        }
        public static class EntidadesTemporadasColecciones
        {
            public const string Showall = "PROC_COMERCIAL_ENTIDADES_TEMPORADAS_COLECCIONES_SHOWALL";
            public const string Showbyid = "PROC_COMERCIAL_ENTIDADES_TEMPORADAS_COLECCIONES_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_ENTIDADES_TEMPORADAS_COLECCIONES_SHOWCOMBOX";
            public const string Search = "PROC_COMERCIAL_ENTIDADES_TEMPORADAS_COLECCIONES_SEARCH";
            public const string Insert = "PROC_COMERCIAL_ENTIDADES_TEMPORADAS_COLECCIONES_INSERT";
            public const string Delete = "PROC_COMERCIAL_ENTIDADES_TEMPORADAS_COLECCIONES_DELETE";
        }
        public static class EntidadesTiposMedioContacto
        {
            public const string Showall = "PROC_COMERCIAL_ENTIDADES_TIPOS_MEDIO_CONTACTO_SHOWALL";
            public const string Showbyid = "PROC_COMERCIAL_ENTIDADES_TIPOS_MEDIO_CONTACTO_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_ENTIDADES_TIPOS_MEDIO_CONTACTO_SHOWCOMBOX";
            public const string Search = "PROC_COMERCIAL_ENTIDADES_TIPOS_MEDIO_CONTACTO_SEARCH";
            public const string Insert = "PROC_COMERCIAL_ENTIDADES_TIPOS_MEDIO_CONTACTO_INSERT";
            public const string Delete = "PROC_COMERCIAL_ENTIDADES_TIPOS_MEDIO_CONTACTO_DELETE";
        }
        public static class FasesIncoterm
        {
            public const string Showall = "PROC_COMERCIAL_FASES_INCOTERM_SHOWALL";
            public const string Showbyid = "PROC_COMERCIAL_FASES_INCOTERM_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_FASES_INCOTERM_SHOWCOMBOX";
            public const string Search = "PROC_COMERCIAL_FASES_INCOTERM_SEARCH";
            public const string Insert = "PROC_COMERCIAL_FASES_INCOTERM_INSERT";
            public const string Update = "PROC_COMERCIAL_FASES_INCOTERM_UPDATE";
            public const string Delete = "PROC_COMERCIAL_FASES_INCOTERM_DELETE";
        }
        public static class FormasPago
        {
            public const string Showall = "PROC_COMERCIAL_FORMAS_PAGO_SHOWALL";
            public const string Showbyid = "PROC_COMERCIAL_FORMAS_PAGO_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_FORMAS_PAGO_SHOWCOMBOX";
            public const string Search = "PROC_COMERCIAL_FORMAS_PAGO_SEARCH";
            public const string Insert = "PROC_COMERCIAL_FORMAS_PAGO_INSERT";
            public const string Update = "PROC_COMERCIAL_FORMAS_PAGO_UPDATE";
            public const string Delete = "PROC_COMERCIAL_FORMAS_PAGO_DELETE";
        }
        public static class Incoterms
        {
            public const string Showall = "PROC_COMERCIAL_INCOTERMS_SHOWALL";
            public const string Showbyid = "PROC_COMERCIAL_INCOTERMS_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_INCOTERMS_SHOWCOMBOX";
            public const string Search = "PROC_COMERCIAL_INCOTERMS_SEARCH";
            public const string Insert = "PROC_COMERCIAL_INCOTERMS_INSERT";
            public const string Update = "PROC_COMERCIAL_INCOTERMS_UPDATE";
            public const string Delete = "PROC_COMERCIAL_INCOTERMS_DELETE";
        }
        public static class IncotermsFases
        {
            public const string Showall = "PROC_COMERCIAL_INCOTERMS_FASES_SHOWALL";
            public const string Showbyid = "PROC_COMERCIAL_INCOTERMS_FASES_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_INCOTERMS_FASES_SHOWCOMBOX";
            public const string Search = "PROC_COMERCIAL_INCOTERMS_FASES_SEARCH";
            public const string Insert = "PROC_COMERCIAL_INCOTERMS_FASES_INSERT";
            public const string Update = "PROC_COMERCIAL_INCOTERMS_FASES_UPDATE";
            public const string Delete = "PROC_COMERCIAL_INCOTERMS_FASES_DELETE";
        }
        public static class IncotermsTiposTransporte
        {
            public const string Showall = "PROC_COMERCIAL_INCOTERMS_TIPOS_TRANSPORTE_SHOWALL";
            public const string Showbyid = "PROC_COMERCIAL_INCOTERMS_TIPOS_TRANSPORTE_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_INCOTERMS_TIPOS_TRANSPORTE_SHOWCOMBOX";
            public const string Search = "PROC_COMERCIAL_INCOTERMS_TIPOS_TRANSPORTE_SEARCH";
            public const string Insert = "PROC_COMERCIAL_INCOTERMS_TIPOS_TRANSPORTE_INSERT";
            public const string Delete = "PROC_COMERCIAL_INCOTERMS_TIPOS_TRANSPORTE_DELETE";
        }
        public static class PedidosComerciales
        {
            public const string Showall = "PROC_COMERCIAL_PEDIDOS_COMERCIALES_SHOWALL";
            public const string Showbyid = "PROC_COMERCIAL_PEDIDOS_COMERCIALES_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_PEDIDOS_COMERCIALES_SHOWCOMBOX";
            public const string Search = "PROC_COMERCIAL_PEDIDOS_COMERCIALES_SEARCH";
            public const string Insert = "PROC_COMERCIAL_PEDIDOS_COMERCIALES_INSERT";
            public const string Update = "PROC_COMERCIAL_PEDIDOS_COMERCIALES_UPDATE";
            public const string Delete = "PROC_COMERCIAL_PEDIDOS_COMERCIALES_DELETE";
            public const string Loadcombos = "PROC_COMERCIAL_PEDIDOS_COMERCIALES_LOADCOMBOS";
            public const string Atencion = "PROC_COMERCIAL_PEDIDOS_COMERCIALES_ATENCION";
        }
        public static class PedidosComercialesCalculo
        {
            public const string Showall = "PROC_COMERCIAL_PEDIDOS_COMERCIALES_CALCULO_SHOWALL";
            public const string Showbyid = "PROC_COMERCIAL_PEDIDOS_COMERCIALES_CALCULO_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_PEDIDOS_COMERCIALES_CALCULO_SHOWCOMBOX";
            public const string Search = "PROC_COMERCIAL_PEDIDOS_COMERCIALES_CALCULO_SEARCH";
            public const string Insert = "PROC_COMERCIAL_PEDIDOS_COMERCIALES_CALCULO_INSERT";
            public const string Update = "PROC_COMERCIAL_PEDIDOS_COMERCIALES_CALCULO_UPDATE";
            public const string Delete = "PROC_COMERCIAL_PEDIDOS_COMERCIALES_CALCULO_DELETE";
            /// <summary>Recalcula y REESCRIBE PEDIDOS_COMERCIALES_CALCULO, y de paso actualiza
            /// VAL_TOTAL de la cabecera. Ojo: no es una lectura, escribe.</summary>
            public const string Calcula = "PROC_COMERCIAL_PEDIDOS_COMERCIALES_CALCULA";
            /// <summary>Convierte el pedido elegido en el comparativo (Proforma) en un pedido
            /// de la clase/tipo "sincronizada" (Orden de Compra): copia cabecera, detalle y
            /// vínculos con requerimientos a un pedido NUEVO, y marca el original como
            /// convertido y los pedidos rivales (de los mismos requerimientos) como rechazados.
            /// No es una lectura, escribe (y crea un pedido nuevo).</summary>
            public const string Sincroniza = "PROC_COMERCIAL_PEDIDO_COMERCIAL_SINCRONIZA";
            /// <summary>Deshace SINCRONIZA. Recibe la clave del pedido SINCRONIZADO (la Orden
            /// de Compra), no la de la proforma.</summary>
            public const string Revertir = "PROC_COMERCIAL_PEDIDO_COMERCIAL_REVERTIR";
            /// <summary>Lo mismo, pero a partir de la PROFORMA: ubica la orden que salió de ella y
            /// llama a PROC_COMERCIAL_PEDIDO_COMERCIAL_REVERTIR.</summary>
            public const string ProformaRevertir = "PROC_COMERCIAL_PEDIDO_COMERCIAL_PROFORMA_REVERTIR";
        }
        public static class PedidosComercialesDetalle
        {
            /// <summary>Graba en la línea del pedido el lote y la ubicación que el usuario
            /// eligió en la pantalla del movimiento. NO es el _UPDATE de la línea: ese
            /// recalcula importes, dispara CALCULA y rechaza las líneas que vienen de una
            /// proforma. Este toca dos columnas y nada más.</summary>
            public const string UpdateOtros = "PROC_COMERCIAL_PEDIDOS_COMERCIALES_DETALLE_UPDATEOTROS";
            public const string Showall = "PROC_COMERCIAL_PEDIDOS_COMERCIALES_DETALLE_SHOWALL";
            public const string Showbyid = "PROC_COMERCIAL_PEDIDOS_COMERCIALES_DETALLE_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_PEDIDOS_COMERCIALES_DETALLE_SHOWCOMBOX";
            public const string Search = "PROC_COMERCIAL_PEDIDOS_COMERCIALES_DETALLE_SEARCH";
            public const string Insert = "PROC_COMERCIAL_PEDIDOS_COMERCIALES_DETALLE_INSERT";
            public const string Update = "PROC_COMERCIAL_PEDIDOS_COMERCIALES_DETALLE_UPDATE";
            public const string Delete = "PROC_COMERCIAL_PEDIDOS_COMERCIALES_DETALLE_DELETE";
        }
        public static class PedidosComercialesEspecificacionesTecnicasBase
        {
            public const string Showall = "PROC_COMERCIAL_PEDIDOS_COMERCIALES_ESPECIFICACIONES_TECNICAS_BASE_SHOWALL";
            public const string Showbyid = "PROC_COMERCIAL_PEDIDOS_COMERCIALES_ESPECIFICACIONES_TECNICAS_BASE_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_PEDIDOS_COMERCIALES_ESPECIFICACIONES_TECNICAS_BASE_SHOWCOMBOX";
            public const string Search = "PROC_COMERCIAL_PEDIDOS_COMERCIALES_ESPECIFICACIONES_TECNICAS_BASE_SEARCH";
            public const string Insert = "PROC_COMERCIAL_PEDIDOS_COMERCIALES_ESPECIFICACIONES_TECNICAS_BASE_INSERT";
            public const string Delete = "PROC_COMERCIAL_PEDIDOS_COMERCIALES_ESPECIFICACIONES_TECNICAS_BASE_DELETE";
        }
        public static class PedidosComercialesMovimientosAlmacen
        {
            public const string Showall = "PROC_COMERCIAL_PEDIDOS_COMERCIALES_MOVIMIENTOS_ALMACEN_SHOWALL";
            public const string Showbyid = "PROC_COMERCIAL_PEDIDOS_COMERCIALES_MOVIMIENTOS_ALMACEN_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_PEDIDOS_COMERCIALES_MOVIMIENTOS_ALMACEN_SHOWCOMBOX";
            public const string Search = "PROC_COMERCIAL_PEDIDOS_COMERCIALES_MOVIMIENTOS_ALMACEN_SEARCH";
            public const string Insert = "PROC_COMERCIAL_PEDIDOS_COMERCIALES_MOVIMIENTOS_ALMACEN_INSERT";
            public const string Delete = "PROC_COMERCIAL_PEDIDOS_COMERCIALES_MOVIMIENTOS_ALMACEN_DELETE";
        }
        public static class PedidosComercialesSecuencias
        {
            public const string Showall = "PROC_COMERCIAL_PEDIDOS_COMERCIALES_SECUENCIAS_SHOWALL";
            public const string Showbyid = "PROC_COMERCIAL_PEDIDOS_COMERCIALES_SECUENCIAS_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_PEDIDOS_COMERCIALES_SECUENCIAS_SHOWCOMBOX";
            public const string Search = "PROC_COMERCIAL_PEDIDOS_COMERCIALES_SECUENCIAS_SEARCH";
            public const string Insert = "PROC_COMERCIAL_PEDIDOS_COMERCIALES_SECUENCIAS_INSERT";
            public const string Update = "PROC_COMERCIAL_PEDIDOS_COMERCIALES_SECUENCIAS_UPDATE";
            public const string Delete = "PROC_COMERCIAL_PEDIDOS_COMERCIALES_SECUENCIAS_DELETE";
        }
        public static class PedidosComercialesSecuenciasEspecificacionesTecnicas
        {
            public const string Showall = "PROC_COMERCIAL_PEDIDOS_COMERCIALES_SECUENCIAS_ESPECIFICACIONES_TECNICAS_SHOWALL";
            public const string Showbyid = "PROC_COMERCIAL_PEDIDOS_COMERCIALES_SECUENCIAS_ESPECIFICACIONES_TECNICAS_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_PEDIDOS_COMERCIALES_SECUENCIAS_ESPECIFICACIONES_TECNICAS_SHOWCOMBOX";
            public const string Search = "PROC_COMERCIAL_PEDIDOS_COMERCIALES_SECUENCIAS_ESPECIFICACIONES_TECNICAS_SEARCH";
            public const string Insert = "PROC_COMERCIAL_PEDIDOS_COMERCIALES_SECUENCIAS_ESPECIFICACIONES_TECNICAS_INSERT";
            public const string Update = "PROC_COMERCIAL_PEDIDOS_COMERCIALES_SECUENCIAS_ESPECIFICACIONES_TECNICAS_UPDATE";
            public const string Delete = "PROC_COMERCIAL_PEDIDOS_COMERCIALES_SECUENCIAS_ESPECIFICACIONES_TECNICAS_DELETE";
        }
        public static class PedidosEspecificacionesDespacho
        {
            public const string Showall = "PROC_COMERCIAL_PEDIDOS_ESPECIFICACIONES_DESPACHO__SHOWALL";
            public const string Showbyid = "PROC_COMERCIAL_PEDIDOS_ESPECIFICACIONES_DESPACHO__SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_PEDIDOS_ESPECIFICACIONES_DESPACHO__SHOWCOMBOX";
            public const string Search = "PROC_COMERCIAL_PEDIDOS_ESPECIFICACIONES_DESPACHO__SEARCH";
            public const string Insert = "PROC_COMERCIAL_PEDIDOS_ESPECIFICACIONES_DESPACHO__INSERT";
            public const string Delete = "PROC_COMERCIAL_PEDIDOS_ESPECIFICACIONES_DESPACHO__DELETE";
        }
        public static class PedidosEspecificacionesDespachoEspecificacionesTecnicas
        {
            public const string Showall = "PROC_COMERCIAL_PEDIDOS_ESPECIFICACIONES_DESPACHO_ESPECIFICACIONES_TECNICAS_SHOWALL";
            public const string Showbyid = "PROC_COMERCIAL_PEDIDOS_ESPECIFICACIONES_DESPACHO_ESPECIFICACIONES_TECNICAS_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_PEDIDOS_ESPECIFICACIONES_DESPACHO_ESPECIFICACIONES_TECNICAS_SHOWCOMBOX";
            public const string Search = "PROC_COMERCIAL_PEDIDOS_ESPECIFICACIONES_DESPACHO_ESPECIFICACIONES_TECNICAS_SEARCH";
            public const string Insert = "PROC_COMERCIAL_PEDIDOS_ESPECIFICACIONES_DESPACHO_ESPECIFICACIONES_TECNICAS_INSERT";
            public const string Update = "PROC_COMERCIAL_PEDIDOS_ESPECIFICACIONES_DESPACHO_ESPECIFICACIONES_TECNICAS_UPDATE";
            public const string Delete = "PROC_COMERCIAL_PEDIDOS_ESPECIFICACIONES_DESPACHO_ESPECIFICACIONES_TECNICAS_DELETE";
        }
        public static class ProductosTiposPrecio
        {
            public const string Showall = "PROC_COMERCIAL_PRODUCTOS_TIPOS_PRECIO_SHOWALL";
            public const string Showbyid = "PROC_COMERCIAL_PRODUCTOS_TIPOS_PRECIO_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_PRODUCTOS_TIPOS_PRECIO_SHOWCOMBOX";
            public const string Search = "PROC_COMERCIAL_PRODUCTOS_TIPOS_PRECIO_SEARCH";
            public const string Insert = "PROC_COMERCIAL_PRODUCTOS_TIPOS_PRECIO_INSERT";
            public const string Update = "PROC_COMERCIAL_PRODUCTOS_TIPOS_PRECIO_UPDATE";
            public const string Delete = "PROC_COMERCIAL_PRODUCTOS_TIPOS_PRECIO_DELETE";
        }
        public static class ReglasCombo
        {
            public const string Showall = "PROC_COMERCIAL_REGLAS_COMBO_SHOWALL";
            public const string Showbyid = "PROC_COMERCIAL_REGLAS_COMBO_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_REGLAS_COMBO_SHOWCOMBOX";
            public const string Search = "PROC_COMERCIAL_REGLAS_COMBO_SEARCH";
            public const string Insert = "PROC_COMERCIAL_REGLAS_COMBO_INSERT";
            public const string Update = "PROC_COMERCIAL_REGLAS_COMBO_UPDATE";
            public const string Delete = "PROC_COMERCIAL_REGLAS_COMBO_DELETE";
        }
        public static class ReglasComboSubCanales
        {
            public const string Showall = "PROC_COMERCIAL_REGLAS_COMBO_SUB_CANALES_SHOWALL";
            public const string Showbyid = "PROC_COMERCIAL_REGLAS_COMBO_SUB_CANALES_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_REGLAS_COMBO_SUB_CANALES_SHOWCOMBOX";
            public const string Search = "PROC_COMERCIAL_REGLAS_COMBO_SUB_CANALES_SEARCH";
            public const string Insert = "PROC_COMERCIAL_REGLAS_COMBO_SUB_CANALES_INSERT";
            public const string Update = "PROC_COMERCIAL_REGLAS_COMBO_SUB_CANALES_UPDATE";
            public const string Delete = "PROC_COMERCIAL_REGLAS_COMBO_SUB_CANALES_DELETE";
        }
        public static class SubCanalesVenta
        {
            public const string Showall = "PROC_COMERCIAL_SUB_CANALES_VENTA_SHOWALL";
            public const string Showbyid = "PROC_COMERCIAL_SUB_CANALES_VENTA_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_SUB_CANALES_VENTA_SHOWCOMBOX";
            public const string Search = "PROC_COMERCIAL_SUB_CANALES_VENTA_SEARCH";
            public const string Insert = "PROC_COMERCIAL_SUB_CANALES_VENTA_INSERT";
            public const string Update = "PROC_COMERCIAL_SUB_CANALES_VENTA_UPDATE";
            public const string Delete = "PROC_COMERCIAL_SUB_CANALES_VENTA_DELETE";
        }
        public static class SubCanalesVentaProductosFijos
        {
            public const string Showall = "PROC_COMERCIAL_SUB_CANALES_VENTA_PRODUCTOS_FIJOS_SHOWALL";
            public const string Showbyid = "PROC_COMERCIAL_SUB_CANALES_VENTA_PRODUCTOS_FIJOS_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_SUB_CANALES_VENTA_PRODUCTOS_FIJOS_SHOWCOMBOX";
            public const string Search = "PROC_COMERCIAL_SUB_CANALES_VENTA_PRODUCTOS_FIJOS_SEARCH";
            public const string Insert = "PROC_COMERCIAL_SUB_CANALES_VENTA_PRODUCTOS_FIJOS_INSERT";
            public const string Update = "PROC_COMERCIAL_SUB_CANALES_VENTA_PRODUCTOS_FIJOS_UPDATE";
            public const string Delete = "PROC_COMERCIAL_SUB_CANALES_VENTA_PRODUCTOS_FIJOS_DELETE";
        }
        public static class TiposCatalogoPrecios
        {
            public const string Showall = "PROC_COMERCIAL_TIPOS_CATALOGO_PRECIOS_SHOWALL";
            public const string Showbyid = "PROC_COMERCIAL_TIPOS_CATALOGO_PRECIOS_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_TIPOS_CATALOGO_PRECIOS_SHOWCOMBOX";
            public const string Search = "PROC_COMERCIAL_TIPOS_CATALOGO_PRECIOS_SEARCH";
            public const string Insert = "PROC_COMERCIAL_TIPOS_CATALOGO_PRECIOS_INSERT";
            public const string Update = "PROC_COMERCIAL_TIPOS_CATALOGO_PRECIOS_UPDATE";
            public const string Delete = "PROC_COMERCIAL_TIPOS_CATALOGO_PRECIOS_DELETE";
        }
        public static class TiposDescuento
        {
            public const string Showall = "PROC_COMERCIAL_TIPOS_DESCUENTO_SHOWALL";
            public const string Showbyid = "PROC_COMERCIAL_TIPOS_DESCUENTO_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_TIPOS_DESCUENTO_SHOWCOMBOX";
            public const string Search = "PROC_COMERCIAL_TIPOS_DESCUENTO_SEARCH";
            public const string Insert = "PROC_COMERCIAL_TIPOS_DESCUENTO_INSERT";
            public const string Update = "PROC_COMERCIAL_TIPOS_DESCUENTO_UPDATE";
            public const string Delete = "PROC_COMERCIAL_TIPOS_DESCUENTO_DELETE";
        }
        public static class TiposDireccion
        {
            public const string Showall = "PROC_COMERCIAL_TIPOS_DIRECCION_SHOWALL";
            public const string Showbyid = "PROC_COMERCIAL_TIPOS_DIRECCION_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_TIPOS_DIRECCION_SHOWCOMBOX";
            public const string Search = "PROC_COMERCIAL_TIPOS_DIRECCION_SEARCH";
            public const string Insert = "PROC_COMERCIAL_TIPOS_DIRECCION_INSERT";
            public const string Update = "PROC_COMERCIAL_TIPOS_DIRECCION_UPDATE";
            public const string Delete = "PROC_COMERCIAL_TIPOS_DIRECCION_DELETE";
        }
        public static class TiposImpuesto
        {
            public const string Showall = "PROC_COMERCIAL_TIPOS_IMPUESTO_SHOWALL";
            public const string Showbyid = "PROC_COMERCIAL_TIPOS_IMPUESTO_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_TIPOS_IMPUESTO_SHOWCOMBOX";
            public const string Search = "PROC_COMERCIAL_TIPOS_IMPUESTO_SEARCH";
            public const string Insert = "PROC_COMERCIAL_TIPOS_IMPUESTO_INSERT";
            public const string Update = "PROC_COMERCIAL_TIPOS_IMPUESTO_UPDATE";
            public const string Delete = "PROC_COMERCIAL_TIPOS_IMPUESTO_DELETE";
        }
        public static class TiposImpuestoValores
        {
            public const string Showall = "PROC_COMERCIAL_TIPOS_IMPUESTO_VALORES_SHOWALL";
            public const string Showbyid = "PROC_COMERCIAL_TIPOS_IMPUESTO_VALORES_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_TIPOS_IMPUESTO_VALORES_SHOWCOMBOX";
            public const string Search = "PROC_COMERCIAL_TIPOS_IMPUESTO_VALORES_SEARCH";
            public const string Insert = "PROC_COMERCIAL_TIPOS_IMPUESTO_VALORES_INSERT";
            public const string Update = "PROC_COMERCIAL_TIPOS_IMPUESTO_VALORES_UPDATE";
            public const string Delete = "PROC_COMERCIAL_TIPOS_IMPUESTO_VALORES_DELETE";
        }
        public static class TiposMedioContacto
        {
            public const string Showall = "PROC_COMERCIAL_TIPOS_MEDIO_CONTACTO_SHOWALL";
            public const string Showbyid = "PROC_COMERCIAL_TIPOS_MEDIO_CONTACTO_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_TIPOS_MEDIO_CONTACTO_SHOWCOMBOX";
            public const string Search = "PROC_COMERCIAL_TIPOS_MEDIO_CONTACTO_SEARCH";
            public const string Insert = "PROC_COMERCIAL_TIPOS_MEDIO_CONTACTO_INSERT";
            public const string Update = "PROC_COMERCIAL_TIPOS_MEDIO_CONTACTO_UPDATE";
            public const string Delete = "PROC_COMERCIAL_TIPOS_MEDIO_CONTACTO_DELETE";
        }
        public static class TiposOperacionCalculo
        {
            public const string Showall = "PROC_COMERCIAL_TIPOS_OPERACION_CALCULO_SHOWALL";
            public const string Showbyid = "PROC_COMERCIAL_TIPOS_OPERACION_CALCULO_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_TIPOS_OPERACION_CALCULO_SHOWCOMBOX";
            public const string Search = "PROC_COMERCIAL_TIPOS_OPERACION_CALCULO_SEARCH";
            public const string Insert = "PROC_COMERCIAL_TIPOS_OPERACION_CALCULO_INSERT";
            public const string Update = "PROC_COMERCIAL_TIPOS_OPERACION_CALCULO_UPDATE";
            public const string Delete = "PROC_COMERCIAL_TIPOS_OPERACION_CALCULO_DELETE";
            public const string Copy = "PROC_COMERCIAL_TIPOS_OPERACION_COPY";
        }
        public static class TiposOperacionCalculoAfectados
        {
            public const string Showall = "PROC_COMERCIAL_TIPOS_OPERACION_CALCULO_AFECTADOS_SHOWALL";
            public const string Showbyid = "PROC_COMERCIAL_TIPOS_OPERACION_CALCULO_AFECTADOS_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_TIPOS_OPERACION_CALCULO_AFECTADOS_SHOWCOMBOX";
            public const string Search = "PROC_COMERCIAL_TIPOS_OPERACION_CALCULO_AFECTADOS_SEARCH";
            public const string Insert = "PROC_COMERCIAL_TIPOS_OPERACION_CALCULO_AFECTADOS_INSERT";
            public const string Delete = "PROC_COMERCIAL_TIPOS_OPERACION_CALCULO_AFECTADOS_DELETE";
        }
        public static class TiposOperacionTiposComprobantePago
        {
            public const string Showall = "PROC_COMERCIAL_TIPOS_OPERACION_TIPOS_COMPROBANTE_PAGO_SHOWALL";
            public const string Showbyid = "PROC_COMERCIAL_TIPOS_OPERACION_TIPOS_COMPROBANTE_PAGO_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_TIPOS_OPERACION_TIPOS_COMPROBANTE_PAGO_SHOWCOMBOX";
            public const string Search = "PROC_COMERCIAL_TIPOS_OPERACION_TIPOS_COMPROBANTE_PAGO_SEARCH";
            public const string Insert = "PROC_COMERCIAL_TIPOS_OPERACION_TIPOS_COMPROBANTE_PAGO_INSERT";
            public const string Delete = "PROC_COMERCIAL_TIPOS_OPERACION_TIPOS_COMPROBANTE_PAGO_DELETE";
        }
        public static class TiposPedidoComercial
        {
            public const string Showall = "PROC_COMERCIAL_TIPOS_PEDIDO_COMERCIAL_SHOWALL";
            public const string Showbyid = "PROC_COMERCIAL_TIPOS_PEDIDO_COMERCIAL_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_TIPOS_PEDIDO_COMERCIAL_SHOWCOMBOX";
            public const string Search = "PROC_COMERCIAL_TIPOS_PEDIDO_COMERCIAL_SEARCH";
            public const string Insert = "PROC_COMERCIAL_TIPOS_PEDIDO_COMERCIAL_INSERT";
            public const string Update = "PROC_COMERCIAL_TIPOS_PEDIDO_COMERCIAL_UPDATE";
            public const string Delete = "PROC_COMERCIAL_TIPOS_PEDIDO_COMERCIAL_DELETE";
            public const string Loadcombos = "PROC_COMERCIAL_TIPOS_PEDIDO_COMERCIAL_LOADCOMBOS";
        }
        public static class TiposPos
        {
            public const string Showall = "PROC_COMERCIAL_TIPOS_POS_SHOWALL";
            public const string Showbyid = "PROC_COMERCIAL_TIPOS_POS_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_TIPOS_POS_SHOWCOMBOX";
            public const string Search = "PROC_COMERCIAL_TIPOS_POS_SEARCH";
            public const string Insert = "PROC_COMERCIAL_TIPOS_POS_INSERT";
            public const string Update = "PROC_COMERCIAL_TIPOS_POS_UPDATE";
            public const string Delete = "PROC_COMERCIAL_TIPOS_POS_DELETE";
        }
        public static class TiposPrecio
        {
            public const string Showall = "PROC_COMERCIAL_TIPOS_PRECIO_SHOWALL";
            public const string Showbyid = "PROC_COMERCIAL_TIPOS_PRECIO_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_TIPOS_PRECIO_SHOWCOMBOX";
            public const string Search = "PROC_COMERCIAL_TIPOS_PRECIO_SEARCH";
            public const string Insert = "PROC_COMERCIAL_TIPOS_PRECIO_INSERT";
            public const string Update = "PROC_COMERCIAL_TIPOS_PRECIO_UPDATE";
            public const string Delete = "PROC_COMERCIAL_TIPOS_PRECIO_DELETE";
        }
        public static class TiposTransporte
        {
            public const string Showall = "PROC_COMERCIAL_TIPOS_TRANSPORTE_SHOWALL";
            public const string Showbyid = "PROC_COMERCIAL_TIPOS_TRANSPORTE_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_TIPOS_TRANSPORTE_SHOWCOMBOX";
            public const string Search = "PROC_COMERCIAL_TIPOS_TRANSPORTE_SEARCH";
            public const string Insert = "PROC_COMERCIAL_TIPOS_TRANSPORTE_INSERT";
            public const string Update = "PROC_COMERCIAL_TIPOS_TRANSPORTE_UPDATE";
            public const string Delete = "PROC_COMERCIAL_TIPOS_TRANSPORTE_DELETE";
        }
        public static class TiposUnidadComercial
        {
            public const string Showall = "PROC_COMERCIAL_TIPOS_UNIDAD_COMERCIAL_SHOWALL";
            public const string Showbyid = "PROC_COMERCIAL_TIPOS_UNIDAD_COMERCIAL_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_TIPOS_UNIDAD_COMERCIAL_SHOWCOMBOX";
            public const string Search = "PROC_COMERCIAL_TIPOS_UNIDAD_COMERCIAL_SEARCH";
            public const string Insert = "PROC_COMERCIAL_TIPOS_UNIDAD_COMERCIAL_INSERT";
            public const string Update = "PROC_COMERCIAL_TIPOS_UNIDAD_COMERCIAL_UPDATE";
            public const string Delete = "PROC_COMERCIAL_TIPOS_UNIDAD_COMERCIAL_DELETE";
        }
        public static class TiposUnidadNegocio
        {
            public const string Showall = "PROC_COMERCIAL_TIPOS_UNIDAD_NEGOCIO_SHOWALL";
            public const string Showbyid = "PROC_COMERCIAL_TIPOS_UNIDAD_NEGOCIO_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_TIPOS_UNIDAD_NEGOCIO_SHOWCOMBOX";
            public const string Search = "PROC_COMERCIAL_TIPOS_UNIDAD_NEGOCIO_SEARCH";
            public const string Insert = "PROC_COMERCIAL_TIPOS_UNIDAD_NEGOCIO_INSERT";
            public const string Update = "PROC_COMERCIAL_TIPOS_UNIDAD_NEGOCIO_UPDATE";
            public const string Delete = "PROC_COMERCIAL_TIPOS_UNIDAD_NEGOCIO_DELETE";
        }
        public static class TiposValorCalculo
        {
            public const string Showall = "PROC_COMERCIAL_TIPOS_VALOR_CALCULO_SHOWALL";
            public const string Showbyid = "PROC_COMERCIAL_TIPOS_VALOR_CALCULO_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_TIPOS_VALOR_CALCULO_SHOWCOMBOX";
            public const string Search = "PROC_COMERCIAL_TIPOS_VALOR_CALCULO_SEARCH";
            public const string Insert = "PROC_COMERCIAL_TIPOS_VALOR_CALCULO_INSERT";
            public const string Update = "PROC_COMERCIAL_TIPOS_VALOR_CALCULO_UPDATE";
            public const string Delete = "PROC_COMERCIAL_TIPOS_VALOR_CALCULO_DELETE";
        }
        public static class UnidadesComerciales
        {
            public const string Showall = "PROC_COMERCIAL_UNIDADES_COMERCIALES_SHOWALL";
            public const string Showbyid = "PROC_COMERCIAL_UNIDADES_COMERCIALES_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_UNIDADES_COMERCIALES_SHOWCOMBOX";
            public const string Search = "PROC_COMERCIAL_UNIDADES_COMERCIALES_SEARCH";
            public const string Insert = "PROC_COMERCIAL_UNIDADES_COMERCIALES_INSERT";
            public const string Update = "PROC_COMERCIAL_UNIDADES_COMERCIALES_UPDATE";
            public const string Delete = "PROC_COMERCIAL_UNIDADES_COMERCIALES_DELETE";
        }
        public static class UnidadesNegocio
        {
            public const string Showall = "PROC_COMERCIAL_UNIDADES_NEGOCIO_SHOWALL";
            public const string Showbyid = "PROC_COMERCIAL_UNIDADES_NEGOCIO_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_UNIDADES_NEGOCIO_SHOWCOMBOX";
            public const string Search = "PROC_COMERCIAL_UNIDADES_NEGOCIO_SEARCH";
            public const string Insert = "PROC_COMERCIAL_UNIDADES_NEGOCIO_INSERT";
            public const string Update = "PROC_COMERCIAL_UNIDADES_NEGOCIO_UPDATE";
            public const string Delete = "PROC_COMERCIAL_UNIDADES_NEGOCIO_DELETE";
        }
        public static class SolicitudesComerciales
        {
            public const string Showall = "PROC_COMERCIAL_SOLICITUDES_COMERCIALES_SHOWALL";
            public const string Showbyid = "PROC_COMERCIAL_SOLICITUDES_COMERCIALES_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_SOLICITUDES_COMERCIALES_SHOWCOMBOX";
            public const string Search = "PROC_COMERCIAL_SOLICITUDES_COMERCIALES_SEARCH";
            public const string Insert = "PROC_COMERCIAL_SOLICITUDES_COMERCIALES_INSERT";
            public const string Delete = "PROC_COMERCIAL_SOLICITUDES_COMERCIALES_DELETE";
            public const string AsignarResponsable = "PROC_COMERCIAL_SOLICITUDES_COMERCIALES_ASIGNARRESPONSABLE";
            public const string ReasignaAsesor = "PROC_COMERCIAL_SOLICITUDES_COMERCIALES_REASIGNAASESOR";
            public const string Loadcombos = "PROC_COMERCIAL_SOLICITUDES_LOADCOMBOS";
        }
        public static class SolicitudesComercialesNotificaciones
        {
            public const string Showall = "PROC_COMERCIAL_SOLICITUDES_COMERCIALES_NOTIFICACIONES_SHOWALL";
            public const string Showbyid = "PROC_COMERCIAL_SOLICITUDES_COMERCIALES_NOTIFICACIONES_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_SOLICITUDES_COMERCIALES_NOTIFICACIONES_SHOWCOMBOX";
            public const string Search = "PROC_COMERCIAL_SOLICITUDES_COMERCIALES_NOTIFICACIONES_SEARCH";
            public const string Insert = "PROC_COMERCIAL_SOLICITUDES_COMERCIALES_NOTIFICACIONES_INSERT";
            public const string Delete = "PROC_COMERCIAL_SOLICITUDES_COMERCIALES_NOTIFICACIONES_DELETE";
        }
        public static class SolicitudesComercialesMemorias
        {
            public const string Showall = "PROC_COMERCIAL_SOLICITUDES_COMERCIALES_MEMORIAS_SHOWALL";
            public const string Showbyid = "PROC_COMERCIAL_SOLICITUDES_COMERCIALES_MEMORIAS_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_SOLICITUDES_COMERCIALES_MEMORIAS_SHOWCOMBOX";
            public const string Search = "PROC_COMERCIAL_SOLICITUDES_COMERCIALES_MEMORIAS_SEARCH";
            public const string Insert = "PROC_COMERCIAL_SOLICITUDES_COMERCIALES_MEMORIAS_INSERT";
            public const string Delete = "PROC_COMERCIAL_SOLICITUDES_COMERCIALES_MEMORIAS_DELETE";
        }
        public static class SolicitudesComercialesCliente
        {
            public const string Showall = "PROC_COMERCIAL_SOLICITUDES_COMERCIALES_CLIENTE_SHOWALL";
            public const string Search = "PROC_COMERCIAL_SOLICITUDES_COMERCIALES_CLIENTE_SEARCH";
        }
        public static class TiposSolicitudComercial
        {
            public const string Showall = "PROC_COMERCIAL_TIPOS_SOLICITUD_COMERCIAL_SHOWALL";
            public const string Showbyid = "PROC_COMERCIAL_TIPOS_SOLICITUD_COMERCIAL_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_TIPOS_SOLICITUD_COMERCIAL_SHOWCOMBOX";
            public const string Insert = "PROC_COMERCIAL_TIPOS_SOLICITUD_COMERCIAL_INSERT";
            public const string Delete = "PROC_COMERCIAL_TIPOS_SOLICITUD_COMERCIAL_DELETE";
        }
        // Sub-módulo hijo de TiposSolicitudComercial: áreas asignadas a cada tipo de
        // solicitud comercial (tabla puente, PK compuesta COD_TIPO_SOLICITUD_COMERICAL+COD_AREA).
        public static class TiposSolicitudComercialAreas
        {
            public const string Showall = "PROC_COMERCIAL_TIPOS_SOLICITUD_COMERCIAL_AREAS_SHOWALL";
            public const string Showbyid = "PROC_COMERCIAL_TIPOS_SOLICITUD_COMERCIAL_AREAS_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_TIPOS_SOLICITUD_COMERCIAL_AREAS_SHOWCOMBOX";
            public const string Search = "PROC_COMERCIAL_TIPOS_SOLICITUD_COMERCIAL_AREAS_SEARCH";
            public const string Insert = "PROC_COMERCIAL_TIPOS_SOLICITUD_COMERCIAL_AREAS_INSERT";
            public const string Delete = "PROC_COMERCIAL_TIPOS_SOLICITUD_COMERCIAL_AREAS_DELETE";
        }
        // Nieto: cargos por área participante de cada tipo de solicitud comercial
        // (PK compuesta COD_TIPO_SOLICITUD_COMERICAL+COD_AREA+COD_CARGO).
        public static class TiposSolicitudComercialAreasCargos
        {
            public const string Showall = "PROC_COMERCIAL_TIPOS_SOLICITUD_COMERCIAL_AREAS_CARGOS_SHOWALL";
            public const string Showbyid = "PROC_COMERCIAL_TIPOS_SOLICITUD_COMERCIAL_AREAS_CARGOS_SHOWBYID";
            public const string Showcombox = "PROC_COMERCIAL_TIPOS_SOLICITUD_COMERCIAL_AREAS_CARGOS_SHOWCOMBOX";
            public const string Search = "PROC_COMERCIAL_TIPOS_SOLICITUD_COMERCIAL_AREAS_CARGOS_SEARCH";
            public const string Insert = "PROC_COMERCIAL_TIPOS_SOLICITUD_COMERCIAL_AREAS_CARGOS_INSERT";
            public const string Delete = "PROC_COMERCIAL_TIPOS_SOLICITUD_COMERCIAL_AREAS_CARGOS_DELETE";
            // Combos bundleados de TiposSolicitudComercialAreasCargos.razor: Tipos de
            // Solicitud Comercial + Cargos (universo completo) en una sola llamada.
            public const string Loadcombos = "PROC_COMERCIAL_TIPOS_SOLICITUD_COMERCIAL_AREAS_CARGOS_LOADCOMBOS";
        }
    }
    public static class SCHEMAContabilidad
    {
        public static class AsientosContables
        {
            public const string Showall = "PROC_CONTABILIDAD_ASIENTOS_CONTABLES_SHOWALL";
            public const string Showbyid = "PROC_CONTABILIDAD_ASIENTOS_CONTABLES_SHOWBYID";
            public const string Showcombox = "PROC_CONTABILIDAD_ASIENTOS_CONTABLES_SHOWCOMBOX";
            public const string Search = "PROC_CONTABILIDAD_ASIENTOS_CONTABLES_SEARCH";
            public const string Insert = "PROC_CONTABILIDAD_ASIENTOS_CONTABLES_INSERT";
            public const string Update = "PROC_CONTABILIDAD_ASIENTOS_CONTABLES_UPDATE";
            public const string Delete = "PROC_CONTABILIDAD_ASIENTOS_CONTABLES_DELETE";
        }
        public static class CentrosCosto
        {
            public const string Showall = "PROC_CONTABILIDAD_CENTROS_COSTO_SHOWALL";
            public const string Showbyid = "PROC_CONTABILIDAD_CENTROS_COSTO_SHOWBYID";
            public const string Showcombox = "PROC_CONTABILIDAD_CENTROS_COSTO_SHOWCOMBOX";
            public const string Search = "PROC_CONTABILIDAD_CENTROS_COSTO_SEARCH";
            public const string Insert = "PROC_CONTABILIDAD_CENTROS_COSTO_INSERT";
            public const string Delete = "PROC_CONTABILIDAD_CENTROS_COSTO_DELETE";
        }
        public static class ConfiguracionContable
        {
            public const string Showall = "PROC_CONTABILIDAD_CONFIGURACION_CONTABLE_SHOWALL";
            public const string Showbyid = "PROC_CONTABILIDAD_CONFIGURACION_CONTABLE_SHOWBYID";
            public const string Showcombox = "PROC_CONTABILIDAD_CONFIGURACION_CONTABLE_SHOWCOMBOX";
            public const string Search = "PROC_CONTABILIDAD_CONFIGURACION_CONTABLE_SEARCH";
            public const string Insert = "PROC_CONTABILIDAD_CONFIGURACION_CONTABLE_INSERT";
            public const string Update = "PROC_CONTABILIDAD_CONFIGURACION_CONTABLE_UPDATE";
            public const string Delete = "PROC_CONTABILIDAD_CONFIGURACION_CONTABLE_DELETE";
        }
        public static class CuentasContables
        {
            public const string Showall = "PROC_CONTABILIDAD_CUENTAS_CONTABLES_SHOWALL";
            public const string Showbyid = "PROC_CONTABILIDAD_CUENTAS_CONTABLES_SHOWBYID";
            public const string Showcombox = "PROC_CONTABILIDAD_CUENTAS_CONTABLES_SHOWCOMBOX";
            public const string Search = "PROC_CONTABILIDAD_CUENTAS_CONTABLES_SEARCH";
            public const string Insert = "PROC_CONTABILIDAD_CUENTAS_CONTABLES_INSERT";
            public const string Update = "PROC_CONTABILIDAD_CUENTAS_CONTABLES_UPDATE";
            public const string Delete = "PROC_CONTABILIDAD_CUENTAS_CONTABLES_DELETE";
        }
        public static class CuentasNaturaleza
        {
            public const string Showall = "PROC_CONTABILIDAD_CUENTAS_NATURALEZA_SHOWALL";
            public const string Showbyid = "PROC_CONTABILIDAD_CUENTAS_NATURALEZA_SHOWBYID";
            public const string Showcombox = "PROC_CONTABILIDAD_CUENTAS_NATURALEZA_SHOWCOMBOX";
            public const string Search = "PROC_CONTABILIDAD_CUENTAS_NATURALEZA_SEARCH";
            public const string Insert = "PROC_CONTABILIDAD_CUENTAS_NATURALEZA_INSERT";
            public const string Update = "PROC_CONTABILIDAD_CUENTAS_NATURALEZA_UPDATE";
            public const string Delete = "PROC_CONTABILIDAD_CUENTAS_NATURALEZA_DELETE";
        }
        public static class MovimientosContables
        {
            public const string Showall = "PROC_CONTABILIDAD_MOVIMIENTOS_CONTABLES_SHOWALL";
            public const string Showbyid = "PROC_CONTABILIDAD_MOVIMIENTOS_CONTABLES_SHOWBYID";
            public const string Showcombox = "PROC_CONTABILIDAD_MOVIMIENTOS_CONTABLES_SHOWCOMBOX";
            public const string Search = "PROC_CONTABILIDAD_MOVIMIENTOS_CONTABLES_SEARCH";
            public const string Insert = "PROC_CONTABILIDAD_MOVIMIENTOS_CONTABLES_INSERT";
            public const string Update = "PROC_CONTABILIDAD_MOVIMIENTOS_CONTABLES_UPDATE";
            public const string Delete = "PROC_CONTABILIDAD_MOVIMIENTOS_CONTABLES_DELETE";
        }
        public static class MovimientosContablesDetalle
        {
            public const string Showall = "PROC_CONTABILIDAD_MOVIMIENTOS_CONTABLES_DETALLE_SHOWALL";
            public const string Showbyid = "PROC_CONTABILIDAD_MOVIMIENTOS_CONTABLES_DETALLE_SHOWBYID";
            public const string Showcombox = "PROC_CONTABILIDAD_MOVIMIENTOS_CONTABLES_DETALLE_SHOWCOMBOX";
            public const string Search = "PROC_CONTABILIDAD_MOVIMIENTOS_CONTABLES_DETALLE_SEARCH";
            public const string Insert = "PROC_CONTABILIDAD_MOVIMIENTOS_CONTABLES_DETALLE_INSERT";
            public const string Update = "PROC_CONTABILIDAD_MOVIMIENTOS_CONTABLES_DETALLE_UPDATE";
            public const string Delete = "PROC_CONTABILIDAD_MOVIMIENTOS_CONTABLES_DETALLE_DELETE";
        }
        public static class ProcesosContables
        {
            public const string Showall = "PROC_CONTABILIDAD_PROCESOS_CONTABLES_SHOWALL";
            public const string Showbyid = "PROC_CONTABILIDAD_PROCESOS_CONTABLES_SHOWBYID";
            public const string Showcombox = "PROC_CONTABILIDAD_PROCESOS_CONTABLES_SHOWCOMBOX";
            public const string Search = "PROC_CONTABILIDAD_PROCESOS_CONTABLES_SEARCH";
            public const string Insert = "PROC_CONTABILIDAD_PROCESOS_CONTABLES_INSERT";
            public const string Update = "PROC_CONTABILIDAD_PROCESOS_CONTABLES_UPDATE";
            public const string Delete = "PROC_CONTABILIDAD_PROCESOS_CONTABLES_DELETE";
        }
        public static class SaldosContables
        {
            public const string Showall = "PROC_CONTABILIDAD_SALDOS_CONTABLES_SHOWALL";
            public const string Showbyid = "PROC_CONTABILIDAD_SALDOS_CONTABLES_SHOWBYID";
            public const string Showcombox = "PROC_CONTABILIDAD_SALDOS_CONTABLES_SHOWCOMBOX";
            public const string Search = "PROC_CONTABILIDAD_SALDOS_CONTABLES_SEARCH";
            public const string Insert = "PROC_CONTABILIDAD_SALDOS_CONTABLES_INSERT";
            public const string Update = "PROC_CONTABILIDAD_SALDOS_CONTABLES_UPDATE";
            public const string Delete = "PROC_CONTABILIDAD_SALDOS_CONTABLES_DELETE";
        }
        public static class SaldosCuentaCorriente
        {
            public const string Showall = "PROC_CONTABILIDAD_SALDOS_CUENTA_CORRIENTE_SHOWALL";
            public const string Showbyid = "PROC_CONTABILIDAD_SALDOS_CUENTA_CORRIENTE_SHOWBYID";
            public const string Showcombox = "PROC_CONTABILIDAD_SALDOS_CUENTA_CORRIENTE_SHOWCOMBOX";
            public const string Search = "PROC_CONTABILIDAD_SALDOS_CUENTA_CORRIENTE_SEARCH";
            public const string Insert = "PROC_CONTABILIDAD_SALDOS_CUENTA_CORRIENTE_INSERT";
            public const string Update = "PROC_CONTABILIDAD_SALDOS_CUENTA_CORRIENTE_UPDATE";
            public const string Delete = "PROC_CONTABILIDAD_SALDOS_CUENTA_CORRIENTE_DELETE";
        }
        public static class TiposCentroCosto
        {
            public const string Showall = "PROC_CONTABILIDAD_TIPOS_CENTRO_COSTO_SHOWALL";
            public const string Showbyid = "PROC_CONTABILIDAD_TIPOS_CENTRO_COSTO_SHOWBYID";
            public const string Showcombox = "PROC_CONTABILIDAD_TIPOS_CENTRO_COSTO_SHOWCOMBOX";
            public const string Search = "PROC_CONTABILIDAD_TIPOS_CENTRO_COSTO_SEARCH";
            public const string Insert = "PROC_CONTABILIDAD_TIPOS_CENTRO_COSTO_INSERT";
            public const string Update = "PROC_CONTABILIDAD_TIPOS_CENTRO_COSTO_UPDATE";
            public const string Delete = "PROC_CONTABILIDAD_TIPOS_CENTRO_COSTO_DELETE";
        }
        public static class TiposCuentaContable
        {
            public const string Showall = "PROC_CONTABILIDAD_TIPOS_CUENTA_CONTABLE_SHOWALL";
            public const string Showbyid = "PROC_CONTABILIDAD_TIPOS_CUENTA_CONTABLE_SHOWBYID";
            public const string Showcombox = "PROC_CONTABILIDAD_TIPOS_CUENTA_CONTABLE_SHOWCOMBOX";
            public const string Search = "PROC_CONTABILIDAD_TIPOS_CUENTA_CONTABLE_SEARCH";
            public const string Insert = "PROC_CONTABILIDAD_TIPOS_CUENTA_CONTABLE_INSERT";
            public const string Update = "PROC_CONTABILIDAD_TIPOS_CUENTA_CONTABLE_UPDATE";
            public const string Delete = "PROC_CONTABILIDAD_TIPOS_CUENTA_CONTABLE_DELETE";
        }
    }
    public static class SCHEMAControlcalidad
    {
        public static class ClasesDefectosTecnicos
        {
            public const string Showall = "PROC_CONTROLCALIDAD_CLASES_DEFECTOS_TECNICOS_SHOWALL";
            public const string Showbyid = "PROC_CONTROLCALIDAD_CLASES_DEFECTOS_TECNICOS_SHOWBYID";
            public const string Showcombox = "PROC_CONTROLCALIDAD_CLASES_DEFECTOS_TECNICOS_SHOWCOMBOX";
            public const string Search = "PROC_CONTROLCALIDAD_CLASES_DEFECTOS_TECNICOS_SEARCH";
            public const string Insert = "PROC_CONTROLCALIDAD_CLASES_DEFECTOS_TECNICOS_INSERT";
            public const string Update = "PROC_CONTROLCALIDAD_CLASES_DEFECTOS_TECNICOS_UPDATE";
            public const string Delete = "PROC_CONTROLCALIDAD_CLASES_DEFECTOS_TECNICOS_DELETE";
        }
        public static class ControlCalidadMovimientosAlmacen
        {
            public const string Showall = "PROC_CONTROLCALIDAD_CONTROL_CALIDAD_MOVIMIENTOS_ALMACEN_SHOWALL";
            public const string Showbyid = "PROC_CONTROLCALIDAD_CONTROL_CALIDAD_MOVIMIENTOS_ALMACEN_SHOWBYID";
            public const string Showcombox = "PROC_CONTROLCALIDAD_CONTROL_CALIDAD_MOVIMIENTOS_ALMACEN_SHOWCOMBOX";
            public const string Search = "PROC_CONTROLCALIDAD_CONTROL_CALIDAD_MOVIMIENTOS_ALMACEN_SEARCH";
            public const string Insert = "PROC_CONTROLCALIDAD_CONTROL_CALIDAD_MOVIMIENTOS_ALMACEN_INSERT";
            public const string Update = "PROC_CONTROLCALIDAD_CONTROL_CALIDAD_MOVIMIENTOS_ALMACEN_UPDATE";
            public const string Delete = "PROC_CONTROLCALIDAD_CONTROL_CALIDAD_MOVIMIENTOS_ALMACEN_DELETE";
        }
        public static class ControlCalidadMovimientosAlmacenEspecificaciones
        {
            public const string Showall = "PROC_CONTROLCALIDAD_CONTROL_CALIDAD_MOVIMIENTOS_ALMACEN_ESPECIFICACIONES_SHOWALL";
            public const string Showbyid = "PROC_CONTROLCALIDAD_CONTROL_CALIDAD_MOVIMIENTOS_ALMACEN_ESPECIFICACIONES_SHOWBYID";
            public const string Showcombox = "PROC_CONTROLCALIDAD_CONTROL_CALIDAD_MOVIMIENTOS_ALMACEN_ESPECIFICACIONES_SHOWCOMBOX";
            public const string Search = "PROC_CONTROLCALIDAD_CONTROL_CALIDAD_MOVIMIENTOS_ALMACEN_ESPECIFICACIONES_SEARCH";
            public const string Insert = "PROC_CONTROLCALIDAD_CONTROL_CALIDAD_MOVIMIENTOS_ALMACEN_ESPECIFICACIONES_INSERT";
            public const string Update = "PROC_CONTROLCALIDAD_CONTROL_CALIDAD_MOVIMIENTOS_ALMACEN_ESPECIFICACIONES_UPDATE";
            public const string Delete = "PROC_CONTROLCALIDAD_CONTROL_CALIDAD_MOVIMIENTOS_ALMACEN_ESPECIFICACIONES_DELETE";
        }
        public static class ControlCalidadProduccionEspecificaciones
        {
            public const string Showall = "PROC_CONTROLCALIDAD_CONTROL_CALIDAD_PRODUCCION_ESPECIFICACIONES_SHOWALL";
            public const string Showbyid = "PROC_CONTROLCALIDAD_CONTROL_CALIDAD_PRODUCCION_ESPECIFICACIONES_SHOWBYID";
            public const string Showcombox = "PROC_CONTROLCALIDAD_CONTROL_CALIDAD_PRODUCCION_ESPECIFICACIONES_SHOWCOMBOX";
            public const string Search = "PROC_CONTROLCALIDAD_CONTROL_CALIDAD_PRODUCCION_ESPECIFICACIONES_SEARCH";
            public const string Insert = "PROC_CONTROLCALIDAD_CONTROL_CALIDAD_PRODUCCION_ESPECIFICACIONES_INSERT";
            public const string Update = "PROC_CONTROLCALIDAD_CONTROL_CALIDAD_PRODUCCION_ESPECIFICACIONES_UPDATE";
            public const string Delete = "PROC_CONTROLCALIDAD_CONTROL_CALIDAD_PRODUCCION_ESPECIFICACIONES_DELETE";
        }
        public static class TiposDefectosTecnicos
        {
            public const string Showall = "PROC_CONTROLCALIDAD_TIPOS_DEFECTOS_TECNICOS_SHOWALL";
            public const string Showbyid = "PROC_CONTROLCALIDAD_TIPOS_DEFECTOS_TECNICOS_SHOWBYID";
            public const string Showcombox = "PROC_CONTROLCALIDAD_TIPOS_DEFECTOS_TECNICOS_SHOWCOMBOX";
            public const string Search = "PROC_CONTROLCALIDAD_TIPOS_DEFECTOS_TECNICOS_SEARCH";
            public const string Insert = "PROC_CONTROLCALIDAD_TIPOS_DEFECTOS_TECNICOS_INSERT";
            public const string Update = "PROC_CONTROLCALIDAD_TIPOS_DEFECTOS_TECNICOS_UPDATE";
            public const string Delete = "PROC_CONTROLCALIDAD_TIPOS_DEFECTOS_TECNICOS_DELETE";
        }
    }
    public static class SCHEMACostos
    {
        public static class CategoriasComportamientoCosto
        {
            public const string Showall = "PROC_COSTOS_CATEGORIAS_COMPORTAMIENTO_COSTO_SHOWALL";
            public const string Showbyid = "PROC_COSTOS_CATEGORIAS_COMPORTAMIENTO_COSTO_SHOWBYID";
            public const string Showcombox = "PROC_COSTOS_CATEGORIAS_COMPORTAMIENTO_COSTO_SHOWCOMBOX";
            public const string Search = "PROC_COSTOS_CATEGORIAS_COMPORTAMIENTO_COSTO_SEARCH";
            public const string Insert = "PROC_COSTOS_CATEGORIAS_COMPORTAMIENTO_COSTO_INSERT";
            public const string Update = "PROC_COSTOS_CATEGORIAS_COMPORTAMIENTO_COSTO_UPDATE";
            public const string Delete = "PROC_COSTOS_CATEGORIAS_COMPORTAMIENTO_COSTO_DELETE";
        }
        public static class CategoriasFuncionCosto
        {
            public const string Showall = "PROC_COSTOS_CATEGORIAS_FUNCION_COSTO_SHOWALL";
            public const string Showbyid = "PROC_COSTOS_CATEGORIAS_FUNCION_COSTO_SHOWBYID";
            public const string Showcombox = "PROC_COSTOS_CATEGORIAS_FUNCION_COSTO_SHOWCOMBOX";
            public const string Search = "PROC_COSTOS_CATEGORIAS_FUNCION_COSTO_SEARCH";
            public const string Insert = "PROC_COSTOS_CATEGORIAS_FUNCION_COSTO_INSERT";
            public const string Update = "PROC_COSTOS_CATEGORIAS_FUNCION_COSTO_UPDATE";
            public const string Delete = "PROC_COSTOS_CATEGORIAS_FUNCION_COSTO_DELETE";
        }
        public static class ClasesCosto
        {
            public const string Showall = "PROC_COSTOS_CLASES_COSTO_SHOWALL";
            public const string Showbyid = "PROC_COSTOS_CLASES_COSTO_SHOWBYID";
            public const string Showcombox = "PROC_COSTOS_CLASES_COSTO_SHOWCOMBOX";
            public const string Search = "PROC_COSTOS_CLASES_COSTO_SEARCH";
            public const string Insert = "PROC_COSTOS_CLASES_COSTO_INSERT";
            public const string Update = "PROC_COSTOS_CLASES_COSTO_UPDATE";
            public const string Delete = "PROC_COSTOS_CLASES_COSTO_DELETE";
        }
        public static class ClasesElementosCostos
        {
            public const string Showall = "PROC_COSTOS_CLASES_ELEMENTOS_COSTOS_SHOWALL";
            public const string Showbyid = "PROC_COSTOS_CLASES_ELEMENTOS_COSTOS_SHOWBYID";
            public const string Showcombox = "PROC_COSTOS_CLASES_ELEMENTOS_COSTOS_SHOWCOMBOX";
            public const string Search = "PROC_COSTOS_CLASES_ELEMENTOS_COSTOS_SEARCH";
            public const string Insert = "PROC_COSTOS_CLASES_ELEMENTOS_COSTOS_INSERT";
            public const string Update = "PROC_COSTOS_CLASES_ELEMENTOS_COSTOS_UPDATE";
            public const string Delete = "PROC_COSTOS_CLASES_ELEMENTOS_COSTOS_DELETE";
        }
        public static class ClasesProductos
        {
            public const string Showall = "PROC_COSTOS_CLASES_PRODUCTOS_SHOWALL";
            public const string Showbyid = "PROC_COSTOS_CLASES_PRODUCTOS_SHOWBYID";
            public const string Showcombox = "PROC_COSTOS_CLASES_PRODUCTOS_SHOWCOMBOX";
            public const string Search = "PROC_COSTOS_CLASES_PRODUCTOS_SEARCH";
            public const string Insert = "PROC_COSTOS_CLASES_PRODUCTOS_INSERT";
            public const string Update = "PROC_COSTOS_CLASES_PRODUCTOS_UPDATE";
            public const string Delete = "PROC_COSTOS_CLASES_PRODUCTOS_DELETE";
        }
        public static class ClasesProductosAlmacenes
        {
            public const string Showall = "PROC_COSTOS_CLASES_PRODUCTOS_ALMACENES_SHOWALL";
            public const string Showbyid = "PROC_COSTOS_CLASES_PRODUCTOS_ALMACENES_SHOWBYID";
            public const string Showcombox = "PROC_COSTOS_CLASES_PRODUCTOS_ALMACENES_SHOWCOMBOX";
            public const string Search = "PROC_COSTOS_CLASES_PRODUCTOS_ALMACENES_SEARCH";
            public const string Insert = "PROC_COSTOS_CLASES_PRODUCTOS_ALMACENES_INSERT";
            public const string Delete = "PROC_COSTOS_CLASES_PRODUCTOS_ALMACENES_DELETE";
        }
        public static class CostosCentroCostosProcesos
        {
            public const string Showall = "PROC_COSTOS_COSTOS_CENTRO_COSTOS_PROCESOS_SHOWALL";
            public const string Showbyid = "PROC_COSTOS_COSTOS_CENTRO_COSTOS_PROCESOS_SHOWBYID";
            public const string Showcombox = "PROC_COSTOS_COSTOS_CENTRO_COSTOS_PROCESOS_SHOWCOMBOX";
            public const string Search = "PROC_COSTOS_COSTOS_CENTRO_COSTOS_PROCESOS_SEARCH";
            public const string Insert = "PROC_COSTOS_COSTOS_CENTRO_COSTOS_PROCESOS_INSERT";
            public const string Update = "PROC_COSTOS_COSTOS_CENTRO_COSTOS_PROCESOS_UPDATE";
            public const string Delete = "PROC_COSTOS_COSTOS_CENTRO_COSTOS_PROCESOS_DELETE";
        }
        public static class CostosEntidadesTarifado
        {
            public const string Showall = "PROC_COSTOS_COSTOS_ENTIDADES_TARIFADO_SHOWALL";
            public const string Showbyid = "PROC_COSTOS_COSTOS_ENTIDADES_TARIFADO_SHOWBYID";
            public const string Showcombox = "PROC_COSTOS_COSTOS_ENTIDADES_TARIFADO_SHOWCOMBOX";
            public const string Search = "PROC_COSTOS_COSTOS_ENTIDADES_TARIFADO_SEARCH";
            public const string Insert = "PROC_COSTOS_COSTOS_ENTIDADES_TARIFADO_INSERT";
            public const string Update = "PROC_COSTOS_COSTOS_ENTIDADES_TARIFADO_UPDATE";
            public const string Delete = "PROC_COSTOS_COSTOS_ENTIDADES_TARIFADO_DELETE";
        }
        public static class CostosProceso
        {
            public const string Showall = "PROC_COSTOS_COSTOS_PROCESO_SHOWALL";
            public const string Showbyid = "PROC_COSTOS_COSTOS_PROCESO_SHOWBYID";
            public const string Showcombox = "PROC_COSTOS_COSTOS_PROCESO_SHOWCOMBOX";
            public const string Search = "PROC_COSTOS_COSTOS_PROCESO_SEARCH";
            public const string Insert = "PROC_COSTOS_COSTOS_PROCESO_INSERT";
            public const string Update = "PROC_COSTOS_COSTOS_PROCESO_UPDATE";
            public const string Delete = "PROC_COSTOS_COSTOS_PROCESO_DELETE";
        }
        public static class CostosProduccion
        {
            public const string Showall = "PROC_COSTOS_COSTOS_PRODUCCION_SHOWALL";
            public const string Showbyid = "PROC_COSTOS_COSTOS_PRODUCCION_SHOWBYID";
            public const string Showcombox = "PROC_COSTOS_COSTOS_PRODUCCION_SHOWCOMBOX";
            public const string Search = "PROC_COSTOS_COSTOS_PRODUCCION_SEARCH";
            public const string Insert = "PROC_COSTOS_COSTOS_PRODUCCION_INSERT";
            public const string Update = "PROC_COSTOS_COSTOS_PRODUCCION_UPDATE";
            public const string Delete = "PROC_COSTOS_COSTOS_PRODUCCION_DELETE";
        }
        public static class CostosProductos
        {
            public const string Showall = "PROC_COSTOS_COSTOS_PRODUCTOS_SHOWALL";
            public const string Showbyid = "PROC_COSTOS_COSTOS_PRODUCTOS_SHOWBYID";
            public const string Showcombox = "PROC_COSTOS_COSTOS_PRODUCTOS_SHOWCOMBOX";
            public const string Search = "PROC_COSTOS_COSTOS_PRODUCTOS_SEARCH";
            public const string Insert = "PROC_COSTOS_COSTOS_PRODUCTOS_INSERT";
            public const string Update = "PROC_COSTOS_COSTOS_PRODUCTOS_UPDATE";
            public const string Delete = "PROC_COSTOS_COSTOS_PRODUCTOS_DELETE";
        }
        public static class CostoSubElementosCentroCosto
        {
            public const string Showall = "PROC_COSTOS_COSTO_SUB_ELEMENTOS_CENTRO_COSTO_SHOWALL";
            public const string Showbyid = "PROC_COSTOS_COSTO_SUB_ELEMENTOS_CENTRO_COSTO_SHOWBYID";
            public const string Showcombox = "PROC_COSTOS_COSTO_SUB_ELEMENTOS_CENTRO_COSTO_SHOWCOMBOX";
            public const string Search = "PROC_COSTOS_COSTO_SUB_ELEMENTOS_CENTRO_COSTO_SEARCH";
            public const string Insert = "PROC_COSTOS_COSTO_SUB_ELEMENTOS_CENTRO_COSTO_INSERT";
            public const string Update = "PROC_COSTOS_COSTO_SUB_ELEMENTOS_CENTRO_COSTO_UPDATE";
            public const string Delete = "PROC_COSTOS_COSTO_SUB_ELEMENTOS_CENTRO_COSTO_DELETE";
        }
        public static class ElementosCosto
        {
            public const string Showall = "PROC_COSTOS_ELEMENTOS_COSTO_SHOWALL";
            public const string Showbyid = "PROC_COSTOS_ELEMENTOS_COSTO_SHOWBYID";
            public const string Showcombox = "PROC_COSTOS_ELEMENTOS_COSTO_SHOWCOMBOX";
            public const string Search = "PROC_COSTOS_ELEMENTOS_COSTO_SEARCH";
            public const string Insert = "PROC_COSTOS_ELEMENTOS_COSTO_INSERT";
            public const string Update = "PROC_COSTOS_ELEMENTOS_COSTO_UPDATE";
            public const string Delete = "PROC_COSTOS_ELEMENTOS_COSTO_DELETE";
        }
        public static class SubElementosCosto
        {
            public const string Showall = "PROC_COSTOS_SUB_ELEMENTOS_COSTO_SHOWALL";
            public const string Showbyid = "PROC_COSTOS_SUB_ELEMENTOS_COSTO_SHOWBYID";
            public const string Showcombox = "PROC_COSTOS_SUB_ELEMENTOS_COSTO_SHOWCOMBOX";
            public const string Search = "PROC_COSTOS_SUB_ELEMENTOS_COSTO_SEARCH";
            public const string Insert = "PROC_COSTOS_SUB_ELEMENTOS_COSTO_INSERT";
            public const string Update = "PROC_COSTOS_SUB_ELEMENTOS_COSTO_UPDATE";
            public const string Delete = "PROC_COSTOS_SUB_ELEMENTOS_COSTO_DELETE";
        }
        public static class TiposCosto
        {
            public const string Showall = "PROC_COSTOS_TIPOS_COSTO_SHOWALL";
            public const string Showbyid = "PROC_COSTOS_TIPOS_COSTO_SHOWBYID";
            public const string Showcombox = "PROC_COSTOS_TIPOS_COSTO_SHOWCOMBOX";
            public const string Search = "PROC_COSTOS_TIPOS_COSTO_SEARCH";
            public const string Insert = "PROC_COSTOS_TIPOS_COSTO_INSERT";
            public const string Update = "PROC_COSTOS_TIPOS_COSTO_UPDATE";
            public const string Delete = "PROC_COSTOS_TIPOS_COSTO_DELETE";
        }
    }
    public static class SCHEMADeveloper
    {
        public static class Mensajes
        {
            public const string Showall = "PROC_DEVELOPER_MENSAJES_SHOWALL";
            public const string Showbyid = "PROC_DEVELOPER_MENSAJES_SHOWBYID";
            public const string Showcombox = "PROC_DEVELOPER_MENSAJES_SHOWCOMBOX";
            public const string Search = "PROC_DEVELOPER_MENSAJES_SEARCH";
            public const string Insert = "PROC_DEVELOPER_MENSAJES_INSERT";
            public const string Update = "PROC_DEVELOPER_MENSAJES_UPDATE";
            public const string Delete = "PROC_DEVELOPER_MENSAJES_DELETE";
        }
        public static class StoresProcedures
        {
            public const string Showall = "PROC_DEVELOPER_STORES_PROCEDURES_SHOWALL";
            public const string Showcombox = "PROC_DEVELOPER_STORES_PROCEDURES_SHOWCOMBOX";
            public const string Search = "PROC_DEVELOPER_STORES_PROCEDURES_SEARCH";
        }
        public static class TablasCodigos
        {
            public const string Showall = "PROC_DEVELOPER_TABLAS_CODIGOS_SHOWALL";
            public const string Showbyid = "PROC_DEVELOPER_TABLAS_CODIGOS_SHOWBYID";
            public const string Showcombox = "PROC_DEVELOPER_TABLAS_CODIGOS_SHOWCOMBOX";
            public const string Search = "PROC_DEVELOPER_TABLAS_CODIGOS_SEARCH";
            public const string Insert = "PROC_DEVELOPER_TABLAS_CODIGOS_INSERT";
            public const string Update = "PROC_DEVELOPER_TABLAS_CODIGOS_UPDATE";
            public const string Delete = "PROC_DEVELOPER_TABLAS_CODIGOS_DELETE";
        }
    }
    public static class SCHEMAEntorno
    {

        public static class AplicacionesOpcionesAprobaciones
        {
            public const string Showall  = "PROC_ENTORNO_APLICACIONES_OPCIONES_APROBACIONES_SHOWALL";
            public const string Showbyid = "PROC_ENTORNO_APLICACIONES_OPCIONES_APROBACIONES_SHOWBYID";
            public const string Search   = "PROC_ENTORNO_APLICACIONES_OPCIONES_APROBACIONES_SEARCH";
            public const string Insert   = "PROC_ENTORNO_APLICACIONES_OPCIONES_APROBACIONES_INSERT";
            public const string Update   = "PROC_ENTORNO_APLICACIONES_OPCIONES_APROBACIONES_UPDATE";
            public const string Delete   = "PROC_ENTORNO_APLICACIONES_OPCIONES_APROBACIONES_DELETE";
            // Conteo de niveles por opcion, para la grilla de opciones (script SQL/03).
            // El SHOWCOMBOX de esta tabla existe pero devuelve las mismas columnas que el
            // SHOWALL, no un par (Id, Texto), asi que no sirve como combo y no se registra.
            public const string ShowallResumen = "PROC_ENTORNO_APLICACIONES_OPCIONES_APROBACIONES_SHOWALL_RESUMEN";
        }
        /// <summary>Lista fija de SP candidatos (SHOWCOMBOX/MODULESHOWALL de Entidades) y sus
        /// parámetros — usado por el generador de sentencias SQL de "Entidades a Configurar"
        /// (campo DES_COMMANDSQL).</summary>
        public static class EntidadesProcedures
        {
            public const string Showall = "PROC_ENTORNO_ENTIDADES_PROCEDURES_SHOWALL";
            public const string ParametersShowall = "PROC_ENTORNO_ENTIDADES_PROCEDURES_PARAMETERS_SHOWALL";
        }
        public static class EntidadesConfigurar
        {
            public const string Showall = "PROC_ENTORNO_ENTIDADES_CONFIGURAR_SHOWALL";
            public const string Showbyid = "PROC_ENTORNO_ENTIDADES_CONFIGURAR_SHOWBYID";
            public const string Showcombox = "PROC_ENTORNO_ENTIDADES_CONFIGURAR_SHOWCOMBOX";
            public const string Search = "PROC_ENTORNO_ENTIDADES_CONFIGURAR_SEARCH";
            public const string Insert = "PROC_ENTORNO_ENTIDADES_CONFIGURAR_INSERT";
            public const string Update = "PROC_ENTORNO_ENTIDADES_CONFIGURAR_UPDATE";
            public const string Delete = "PROC_ENTORNO_ENTIDADES_CONFIGURAR_DELETE";
        }
        public static class DominiosEmpresa
        {
            public const string Showall = "PROC_ENTORNO_DOMINIOS_EMPRESA_SHOWALL";
            public const string Showbyid = "PROC_ENTORNO_DOMINIOS_EMPRESA_SHOWBYID";
            public const string Showcombox = "PROC_ENTORNO_DOMINIOS_EMPRESA_SHOWCOMBOX";
            public const string Insert = "PROC_ENTORNO_DOMINIOS_EMPRESA_INSERT";
            public const string Delete = "PROC_ENTORNO_DOMINIOS_EMPRESA_DELETE";
        }
        public static class EjecutarSentencias
        {
            public const string Ejecutar = "PROC_ENTORNO_EJECUTAR_SENTENCIAS";
        }
        public static class DiccionarioEtiquetas
        {
            public const string Showall = "PROC_ENTORNO_DICCIONARIO_ETIQUETAS_SHOWALL";
            public const string Showbyid = "PROC_ENTORNO_DICCIONARIO_ETIQUETAS_SHOWBYID";
            public const string Showcombox = "PROC_ENTORNO_DICCIONARIO_ETIQUETAS_SHOWCOMBOX";
            public const string Insert = "PROC_ENTORNO_DICCIONARIO_ETIQUETAS_INSERT";
            public const string Update = "PROC_ENTORNO_DICCIONARIO_ETIQUETAS_UPDATE";
            public const string Delete = "PROC_ENTORNO_DICCIONARIO_ETIQUETAS_DELETE";
        }
        // Cómo se lee un producto (manual, código de barras, RFID). Sin SP de búsqueda: el catálogo es corto y la pantalla filtra en memoria.
        public static class TiposLectura
        {
            public const string Showall = "PROC_ENTORNO_TIPOS_LECTURA_SHOWALL";
            public const string Showbyid = "PROC_ENTORNO_TIPOS_LECTURA_SHOWBYID";
            public const string Showcombox = "PROC_ENTORNO_TIPOS_LECTURA_SHOWCOMBOX";
            public const string Insert = "PROC_ENTORNO_TIPOS_LECTURA_INSERT";
            public const string Update = "PROC_ENTORNO_TIPOS_LECTURA_UPDATE";
            public const string Delete = "PROC_ENTORNO_TIPOS_LECTURA_DELETE";
        }
        public static class TiposNotificacion
        {
            public const string Showall = "PROC_ENTORNO_TIPOS_NOTIFICACION_SHOWALL";
            public const string Showbyid = "PROC_ENTORNO_TIPOS_NOTIFICACION_SHOWBYID";
            public const string Showcombox = "PROC_ENTORNO_TIPOS_NOTIFICACION_SHOWCOMBOX";
            public const string Search = "PROC_ENTORNO_TIPOS_NOTIFICACION_SEARCH";
            public const string Insert = "PROC_ENTORNO_TIPOS_NOTIFICACION_INSERT";
            public const string Delete = "PROC_ENTORNO_TIPOS_NOTIFICACION_DELETE";
        }
        public static class TiposNotificacionAcciones
        {
            public const string Showall = "PROC_ENTORNO_TIPOS_NOTIFICACION_ACCIONES_SHOWALL";
            public const string Showbyid = "PROC_ENTORNO_TIPOS_NOTIFICACION_ACCIONES_SHOWBYID";
            public const string Showcombox = "PROC_ENTORNO_TIPOS_NOTIFICACION_ACCIONES_SHOWCOMBOX";
            public const string Search = "PROC_ENTORNO_TIPOS_NOTIFICACION_ACCIONES_SEARCH";
            public const string Insert = "PROC_ENTORNO_TIPOS_NOTIFICACION_ACCIONES_INSERT";
            public const string Delete = "PROC_ENTORNO_TIPOS_NOTIFICACION_ACCIONES_DELETE";
        }
        public static class NotificacionesFlujo
        {
            public const string Showall          = "PROC_ENTORNO_NOTIFICACIONES_FLUJO_SHOWALL";
            public const string UsuarioPendiente = "PROC_ENTORNO_NOTIFICACIONES_FLUJO_USUARIO_PENDIENTE";
        }
        public static class PostIts
        {
            public const string Showall = "PROC_ENTORNO_POSTITS_SHOWALL";
            public const string Showbyid = "PROC_ENTORNO_POSTITS_SHOWBYID";
            public const string Insert = "PROC_ENTORNO_POSTITS_INSERT";
            public const string Delete = "PROC_ENTORNO_POSTITS_DELETE";
        }
        public static class Servidores
        {
            public const string Showall = "PROC_ENTORNO_SERVIDORES_SHOWALL";
            public const string Showbyid = "PROC_ENTORNO_SERVIDORES_SHOWBYID";
            public const string Showcombox = "PROC_ENTORNO_SERVIDORES_SHOWCOMBOX";
            public const string Search = "PROC_ENTORNO_SERVIDORES_SEARCH";
            public const string Insert = "PROC_ENTORNO_SERVIDORES_INSERT";
            public const string Delete = "PROC_ENTORNO_SERVIDORES_DELETE";
        }
        public static class PathFiles
        {
            public const string Showall = "PROC_ENTORNO_PATHFILES_SHOWALL";
            public const string Showbyid = "PROC_ENTORNO_PATHFILES_SHOWBYID";
            public const string Showcombox = "PROC_ENTORNO_PATHFILES_SHOWCOMBOX";
            public const string Search = "PROC_ENTORNO_PATHFILES_SEARCH";
            public const string Insert = "PROC_ENTORNO_PATHFILES_INSERT";
            public const string Delete = "PROC_ENTORNO_PATHFILES_DELETE";
        }
        public static class NotificacionesAprobaciones
        {
            public const string Showbyid = "PROC_NOTIFICACIONES_APROBACIONES_SHOWBYID";
            /// <summary>Aprobar, rechazar u observar: el procedimiento valida al aprobador vigente y ejecuta el comando guardado de esa acción.</summary>
            public const string Accion = "PROC_NOTIFICACIONES_APROBACIONES_ACCION";
        }
        public static class Autorizaciones
        {
            public const string Showall = "PROC_ENTORNO_AUTORIZACIONES_SHOWALL";
            public const string Showbyid = "PROC_ENTORNO_AUTORIZACIONES_SHOWBYID";
            public const string Showcombox = "PROC_ENTORNO_AUTORIZACIONES_SHOWCOMBOX";
            public const string Search = "PROC_ENTORNO_AUTORIZACIONES_SEARCH";
            public const string Insert = "PROC_ENTORNO_AUTORIZACIONES_INSERT";
            public const string Update = "PROC_ENTORNO_AUTORIZACIONES_UPDATE";
            public const string Delete = "PROC_ENTORNO_AUTORIZACIONES_DELETE";
        }
        public static class AutorizacionesEventos
        {
            public const string Showall = "PROC_ENTORNO_AUTORIZACIONES_EVENTOS_SHOWALL";
            public const string Showbyid = "PROC_ENTORNO_AUTORIZACIONES_EVENTOS_SHOWBYID";
            public const string Showcombox = "PROC_ENTORNO_AUTORIZACIONES_EVENTOS_SHOWCOMBOX";
            public const string Search = "PROC_ENTORNO_AUTORIZACIONES_EVENTOS_SEARCH";
            public const string Insert = "PROC_ENTORNO_AUTORIZACIONES_EVENTOS_INSERT";
            public const string Delete = "PROC_ENTORNO_AUTORIZACIONES_EVENTOS_DELETE";
        }
        public static class FlujosProceso
        {
            public const string Showall = "PROC_ENTORNO_FLUJOS_PROCESO_SHOWALL";
            public const string Showbyid = "PROC_ENTORNO_FLUJOS_PROCESO_SHOWBYID";
            public const string Showcombox = "PROC_ENTORNO_FLUJOS_PROCESO_SHOWCOMBOX";
            public const string Search = "PROC_ENTORNO_FLUJOS_PROCESO_SEARCH";
            public const string Insert = "PROC_ENTORNO_FLUJOS_PROCESO_INSERT";
            public const string Update = "PROC_ENTORNO_FLUJOS_PROCESO_UPDATE";
            public const string Delete = "PROC_ENTORNO_FLUJOS_PROCESO_DELETE";
        }
        public static class FlujosProcesoDocumentacion
        {
            public const string Showall = "PROC_ENTORNO_FLUJOS_PROCESO_DOCUMENTACION_SHOWALL";
            public const string Showbyid = "PROC_ENTORNO_FLUJOS_PROCESO_DOCUMENTACION_SHOWBYID";
            public const string Showcombox = "PROC_ENTORNO_FLUJOS_PROCESO_DOCUMENTACION_SHOWCOMBOX";
            public const string Search = "PROC_ENTORNO_FLUJOS_PROCESO_DOCUMENTACION_SEARCH";
            public const string Insert = "PROC_ENTORNO_FLUJOS_PROCESO_DOCUMENTACION_INSERT";
            public const string Update = "PROC_ENTORNO_FLUJOS_PROCESO_DOCUMENTACION_UPDATE";
            public const string Delete = "PROC_ENTORNO_FLUJOS_PROCESO_DOCUMENTACION_DELETE";
        }
        public static class FlujosProcesoEtapas
        {
            public const string Showall = "PROC_ENTORNO_FLUJOS_PROCESO_ETAPAS_SHOWALL";
            public const string Showbyid = "PROC_ENTORNO_FLUJOS_PROCESO_ETAPAS_SHOWBYID";
            public const string Showcombox = "PROC_ENTORNO_FLUJOS_PROCESO_ETAPAS_SHOWCOMBOX";
            public const string Search = "PROC_ENTORNO_FLUJOS_PROCESO_ETAPAS_SEARCH";
            public const string Insert = "PROC_ENTORNO_FLUJOS_PROCESO_ETAPAS_INSERT";
            public const string Update = "PROC_ENTORNO_FLUJOS_PROCESO_ETAPAS_UPDATE";
            public const string Delete = "PROC_ENTORNO_FLUJOS_PROCESO_ETAPAS_DELETE";
        }
        public static class FlujosProcesoEtapasDocumentacion
        {
            public const string Showall = "PROC_ENTORNO_FLUJOS_PROCESO_ETAPAS_DOCUMENTACION_SHOWALL";
            public const string Showbyid = "PROC_ENTORNO_FLUJOS_PROCESO_ETAPAS_DOCUMENTACION_SHOWBYID";
            public const string Showcombox = "PROC_ENTORNO_FLUJOS_PROCESO_ETAPAS_DOCUMENTACION_SHOWCOMBOX";
            public const string Search = "PROC_ENTORNO_FLUJOS_PROCESO_ETAPAS_DOCUMENTACION_SEARCH";
            public const string Insert = "PROC_ENTORNO_FLUJOS_PROCESO_ETAPAS_DOCUMENTACION_INSERT";
            public const string Update = "PROC_ENTORNO_FLUJOS_PROCESO_ETAPAS_DOCUMENTACION_UPDATE";
            public const string Delete = "PROC_ENTORNO_FLUJOS_PROCESO_ETAPAS_DOCUMENTACION_DELETE";
        }
        public static class FlujosProcesoEtapasNotificados
        {
            public const string Showall = "PROC_ENTORNO_FLUJOS_PROCESO_ETAPAS_NOTIFICADOS_SHOWALL";
            public const string Showbyid = "PROC_ENTORNO_FLUJOS_PROCESO_ETAPAS_NOTIFICADOS_SHOWBYID";
            public const string Showcombox = "PROC_ENTORNO_FLUJOS_PROCESO_ETAPAS_NOTIFICADOS_SHOWCOMBOX";
            public const string Search = "PROC_ENTORNO_FLUJOS_PROCESO_ETAPAS_NOTIFICADOS_SEARCH";
            public const string Insert = "PROC_ENTORNO_FLUJOS_PROCESO_ETAPAS_NOTIFICADOS_INSERT";
            public const string Update = "PROC_ENTORNO_FLUJOS_PROCESO_ETAPAS_NOTIFICADOS_UPDATE";
            public const string Delete = "PROC_ENTORNO_FLUJOS_PROCESO_ETAPAS_NOTIFICADOS_DELETE";
        }
        public static class FlujosProcesoMemoria
        {
            public const string Showall = "PROC_ENTORNO_FLUJOS_PROCESO_MEMORIA_SHOWALL";
            public const string Showbyid = "PROC_ENTORNO_FLUJOS_PROCESO_MEMORIA_SHOWBYID";
            public const string Showcombox = "PROC_ENTORNO_FLUJOS_PROCESO_MEMORIA_SHOWCOMBOX";
            public const string Search = "PROC_ENTORNO_FLUJOS_PROCESO_MEMORIA_SEARCH";
            public const string Insert = "PROC_ENTORNO_FLUJOS_PROCESO_MEMORIA_INSERT";
            public const string Update = "PROC_ENTORNO_FLUJOS_PROCESO_MEMORIA_UPDATE";
            public const string Delete = "PROC_ENTORNO_FLUJOS_PROCESO_MEMORIA_DELETE";
        }
        public static class TiposAutorizacion
        {
            public const string Showall = "PROC_ENTORNO_TIPOS_AUTORIZACION_SHOWALL";
            public const string Showbyid = "PROC_ENTORNO_TIPOS_AUTORIZACION_SHOWBYID";
            public const string Showcombox = "PROC_ENTORNO_TIPOS_AUTORIZACION_SHOWCOMBOX";
            public const string Search = "PROC_ENTORNO_TIPOS_AUTORIZACION_SEARCH";
            public const string Insert = "PROC_ENTORNO_TIPOS_AUTORIZACION_INSERT";
            public const string Update = "PROC_ENTORNO_TIPOS_AUTORIZACION_UPDATE";
            public const string Delete = "PROC_ENTORNO_TIPOS_AUTORIZACION_DELETE";
        }
        public static class TiposFlujosProceso
        {
            public const string Showall = "PROC_ENTORNO_TIPOS_FLUJOS_PROCESO_SHOWALL";
            public const string Showbyid = "PROC_ENTORNO_TIPOS_FLUJOS_PROCESO_SHOWBYID";
            public const string Showcombox = "PROC_ENTORNO_TIPOS_FLUJOS_PROCESO_SHOWCOMBOX";
            public const string Search = "PROC_ENTORNO_TIPOS_FLUJOS_PROCESO_SEARCH";
            public const string Insert = "PROC_ENTORNO_TIPOS_FLUJOS_PROCESO_INSERT";
            public const string Update = "PROC_ENTORNO_TIPOS_FLUJOS_PROCESO_UPDATE";
            public const string Delete = "PROC_ENTORNO_TIPOS_FLUJOS_PROCESO_DELETE";
        }
        public static class UsuariosAutorizadores
        {
            public const string Showall = "PROC_ENTORNO_USUARIOS_AUTORIZADORES_SHOWALL";
            public const string Showbyid = "PROC_ENTORNO_USUARIOS_AUTORIZADORES_SHOWBYID";
            public const string Showcombox = "PROC_ENTORNO_USUARIOS_AUTORIZADORES_SHOWCOMBOX";
            public const string Search = "PROC_ENTORNO_USUARIOS_AUTORIZADORES_SEARCH";
            public const string Insert = "PROC_ENTORNO_USUARIOS_AUTORIZADORES_INSERT";
            public const string Update = "PROC_ENTORNO_USUARIOS_AUTORIZADORES_UPDATE";
            public const string Delete = "PROC_ENTORNO_USUARIOS_AUTORIZADORES_DELETE";
        }
        public static class BancoColores
        {
            public const string Showall = "PROC_ENTORNO_BANCO_COLORES_SHOWALL";
            public const string Showbyid = "PROC_ENTORNO_BANCO_COLORES_SHOWBYID";
            public const string Showcombox = "PROC_ENTORNO_BANCO_COLORES_SHOWCOMBOX";
            public const string Search = "PROC_ENTORNO_BANCO_COLORES_SEARCH";
            public const string Insert = "PROC_ENTORNO_BANCO_COLORES_INSERT";
            public const string Delete = "PROC_ENTORNO_BANCO_COLORES_DELETE";
        }
    }
    public static class SCHEMAEstandar
    {
        public static class Feriados
        {
            public const string Showall = "PROC_ESTANDAR_FERIADOS_SHOWALL";
            public const string Showbyid = "PROC_ESTANDAR_FERIADOS_SHOWBYID";
            public const string Search = "PROC_ESTANDAR_FERIADOS_SEARCH";
            public const string Insert = "PROC_ESTANDAR_FERIADOS_INSERT";
            public const string Update = "PROC_ESTANDAR_FERIADOS_UPDATE";
            public const string Delete = "PROC_ESTANDAR_FERIADOS_DELETE";
        }
        public static class EstructuraProducto
        {
            public const string Showall = "PROC_ESTANDAR_ESTRUCTURA_PRODUCTO_SHOWALL";
            public const string Showbyid = "PROC_ESTANDAR_ESTRUCTURA_PRODUCTO_SHOWBYID";
            public const string Showcombox = "PROC_ESTANDAR_ESTRUCTURA_PRODUCTO_SHOWCOMBOX";
            public const string Search = "PROC_ESTANDAR_ESTRUCTURA_PRODUCTO_SEARCH";
            public const string Insert = "PROC_ESTANDAR_ESTRUCTURA_PRODUCTO_INSERT";
            public const string Update = "PROC_ESTANDAR_ESTRUCTURA_PRODUCTO_UPDATE";
            public const string Delete = "PROC_ESTANDAR_ESTRUCTURA_PRODUCTO_DELETE";
        }
        public static class AplicacionesOpcionesAreas
        {
            public const string Showall = "PROC_ESTANDAR_APLICACIONES_OPCIONES_AREAS_SHOWALL";
            public const string Showbyid = "PROC_ESTANDAR_APLICACIONES_OPCIONES_AREAS_SHOWBYID";
            public const string Search = "PROC_ESTANDAR_APLICACIONES_OPCIONES_AREAS_SEARCH";
            public const string Insert = "PROC_ESTANDAR_APLICACIONES_OPCIONES_AREAS_INSERT";
            public const string Delete = "PROC_ESTANDAR_APLICACIONES_OPCIONES_AREAS_DELETE";
        }
        public static class EstructuraRfid
        {
            public const string Showall = "PROC_ESTANDAR_ESTRUCTURA_RFID_SHOWALL";
            public const string Showbyid = "PROC_ESTANDAR_ESTRUCTURA_RFID_SHOWBYID";
            public const string Showcombox = "PROC_ESTANDAR_ESTRUCTURA_RFID_SHOWCOMBOX";
            public const string Search = "PROC_ESTANDAR_ESTRUCTURA_RFID_SEARCH";
            public const string Insert = "PROC_ESTANDAR_ESTRUCTURA_RFID_INSERT";
            public const string Update = "PROC_ESTANDAR_ESTRUCTURA_RFID_UPDATE";
            public const string Delete = "PROC_ESTANDAR_ESTRUCTURA_RFID_DELETE";
        }
        public static class EstructuraCodebar
        {
            public const string Showall = "PROC_ESTANDAR_ESTRUCTURA_CODEBAR_SHOWALL";
            public const string Showbyid = "PROC_ESTANDAR_ESTRUCTURA_CODEBAR_SHOWBYID";
            public const string Showcombox = "PROC_ESTANDAR_ESTRUCTURA_CODEBAR_SHOWCOMBOX";
            public const string Insert = "PROC_ESTANDAR_ESTRUCTURA_CODEBAR_INSERT";
            public const string Update = "PROC_ESTANDAR_ESTRUCTURA_CODEBAR_UPDATE";
            public const string Delete = "PROC_ESTANDAR_ESTRUCTURA_CODEBAR_DELETE";
        }
        public static class TiposConfiguracion
        {
            public const string Showall = "PROC_ESTANDAR_TIPOS_CONFIGURACION_SHOWALL";
            public const string Showbyid = "PROC_ESTANDAR_TIPOS_CONFIGURACION_SHOWBYID";
            public const string Showcombox = "PROC_ESTANDAR_TIPOS_CONFIGURACION_SHOWCOMBOX";
            public const string Insert = "PROC_ESTANDAR_TIPOS_CONFIGURACION_INSERT";
            public const string Update = "PROC_ESTANDAR_TIPOS_CONFIGURACION_UPDATE";
            public const string Delete = "PROC_ESTANDAR_TIPOS_CONFIGURACION_DELETE";
        }
        public static class ConfiguracionesProductos
        {
            public const string Showall = "PROC_ESTANDAR_CONFIGURACIONES_PRODUCTOS_SHOWALL";
            public const string Showbyid = "PROC_ESTANDAR_CONFIGURACIONES_PRODUCTOS_SHOWBYID";
            public const string Showcombox = "PROC_ESTANDAR_CONFIGURACIONES_PRODUCTOS_SHOWCOMBOX";
            public const string Search = "PROC_ESTANDAR_CONFIGURACIONES_PRODUCTOS_SEARCH";
            public const string Insert = "PROC_ESTANDAR_CONFIGURACIONES_PRODUCTOS_INSERT";
            public const string Update = "PROC_ESTANDAR_CONFIGURACIONES_PRODUCTOS_UPDATE";
            public const string Delete = "PROC_ESTANDAR_CONFIGURACIONES_PRODUCTOS_DELETE";
        }
        public static class EntidadesUsuariosSugeridos
        {
            public const string Comerciales = "PROC_ESTANDAR_ENTIDADES_USUARIOS_COMERCIALES";
            public const string Dependientes = "PROC_ESTANDAR_ENTIDADES_DEPENDIENTES";
            // Variante por entidad: los dependientes de UNA entidad puntual, para el combo
            // de Vendedor del Pedido Comercial.
            public const string DependientesShowcombox = "PROC_ESTANDAR_ENTIDADES_DEPENDIENTES_SHOWCOMBOX";
        }
        public static class TiposUso
        {
            public const string Showall = "PROC_ESTANDAR_TIPOS_USO_SHOWALL";
            public const string Showbyid = "PROC_ESTANDAR_TIPOS_USO_SHOWBYID";
            public const string Showcombox = "PROC_ESTANDAR_TIPOS_USO_SHOWCOMBOX";
            public const string Insert = "PROC_ESTANDAR_TIPOS_USO_INSERT";
            public const string Delete = "PROC_ESTANDAR_TIPOS_USO_DELETE";
        }
        public static class TablasTiposEstado
        {
            public const string Showall = "PROC_ESTANDAR_TABLAS_TIPOS_ESTADO_SHOWALL";
            public const string Showbyid = "PROC_ESTANDAR_TABLAS_TIPOS_ESTADO_SHOWBYID";
            public const string Showcombox = "PROC_ESTANDAR_TABLAS_TIPOS_ESTADO_SHOWCOMBOX";
            public const string Search = "PROC_ESTANDAR_TABLAS_TIPOS_ESTADO_SEARCH";
            public const string Insert = "PROC_ESTANDAR_TABLAS_TIPOS_ESTADO_INSERT";
            public const string Delete = "PROC_ESTANDAR_TABLAS_TIPOS_ESTADO_DELETE";
        }
        public static class ClasesOperacion
        {
            public const string Showall = "PROC_ESTANDAR_CLASES_OPERACION_SHOWALL";
            public const string Showbyid = "PROC_ESTANDAR_CLASES_OPERACION_SHOWBYID";
            public const string Showcombox = "PROC_ESTANDAR_CLASES_OPERACION_SHOWCOMBOX";
            public const string Search = "PROC_ESTANDAR_CLASES_OPERACION_SEARCH";
            public const string Insert = "PROC_ESTANDAR_CLASES_OPERACION_INSERT";
            public const string Update = "PROC_ESTANDAR_CLASES_OPERACION_UPDATE";
            public const string Delete = "PROC_ESTANDAR_CLASES_OPERACION_DELETE";
            // SP ya existente en BD (antes huérfano: sin constante ni wiring) — devuelve
            // TiposEntidades + TiposEstados + TiposMotivos + Estado (vía
            // PROC_ESTANDAR_ESTADOS_SHOWCOMBOX @COD_TIPO_ESTADO_CLASES_OPERACION) en un
            // solo viaje. Ver scratchpad/loadcombos_agenteE.sql para el detalle.
            public const string Loadcombos = "PROC_ESTANDAR_CLASES_OPERACION_LOADCOMBOS";
        }
        public static class TiposMenuOpciones
        {
            public const string Showall = "PROC_ESTANDAR_TIPOS_MENU_OPCIONES_SHOWALL";
            public const string Showbyid = "PROC_ESTANDAR_TIPOS_MENU_OPCIONES_SHOWBYID";
            public const string Showcombox = "PROC_ESTANDAR_TIPOS_MENU_OPCIONES_SHOWCOMBOX";
            public const string Search = "PROC_ESTANDAR_TIPOS_MENU_OPCIONES_SEARCH";
            public const string Insert = "PROC_ESTANDAR_TIPOS_MENU_OPCIONES_INSERT";
            public const string Delete = "PROC_ESTANDAR_TIPOS_MENU_OPCIONES_DELETE";
        }
        public static class ConfiguracionEmisionElectronica
        {
            /// <summary>Devuelve la fila única CON las columnas cifradas: sólo para la API.</summary>
            public const string Showbyid = "PROC_ESTANDAR_CONFIGURACION_EMISION_ELECTRONICA_SHOWBYID";
            public const string Update = "PROC_ESTANDAR_CONFIGURACION_EMISION_ELECTRONICA_UPDATE";
        }
        public static class ConfiguracionGeneral
        {
            public const string Showall = "PROC_ESTANDAR_CONFIGURACION_GENERAL_SHOWALL";
            public const string Showbyid = "PROC_ESTANDAR_CONFIGURACION_GENERAL_SHOWBYID";
            public const string Showcombox = "PROC_ESTANDAR_CONFIGURACION_GENERAL_SHOWCOMBOX";
            public const string Search = "PROC_ESTANDAR_CONFIGURACION_GENERAL_SEARCH";
            public const string Insert = "PROC_ESTANDAR_CONFIGURACION_GENERAL_INSERT";
            public const string Delete = "PROC_ESTANDAR_CONFIGURACION_GENERAL_DELETE";
            /// <summary>Consolida en un solo viaje los combos independientes que la pantalla
            /// "Configuración General" pedía uno por uno (Monedas + Tipos de Configuración +
            /// Grupos de Producto) — el SP solo hace EXEC de los 3 _SHOWCOMBOX existentes,
            /// en ese orden, y Dapper los lee como 3 result sets con QueryMultipleAsync.</summary>
            public const string Loadcombos = "PROC_ESTANDAR_CONFIGURACION_GENERAL_LOADCOMBOS";
        }
        public static class ConfiguracionUsuario
        {
            public const string Showall = "PROC_ESTANDAR_CONFIGURACION_USUARIO_SHOWALL";
            public const string Showbyid = "PROC_ESTANDAR_CONFIGURACION_USUARIO_SHOWBYID";
            public const string Showcombox = "PROC_ESTANDAR_CONFIGURACION_USUARIO_SHOWCOMBOX";
            public const string Search = "PROC_ESTANDAR_CONFIGURACION_USUARIO_SEARCH";
            public const string Insert = "PROC_ESTANDAR_CONFIGURACION_USUARIO_INSERT";
            public const string Delete = "PROC_ESTANDAR_CONFIGURACION_USUARIO_DELETE";
        }
        public static class Entidades
        {
            public const string Showall = "PROC_ESTANDAR_ENTIDADES_SHOWALL";
            public const string Showbyid = "PROC_ESTANDAR_ENTIDADES_SHOWBYID";
            public const string Showcombox = "PROC_ESTANDAR_ENTIDADES_SHOWCOMBOX";
            public const string Search = "PROC_ESTANDAR_ENTIDADES_SEARCH";
            public const string Insert = "PROC_ESTANDAR_ENTIDADES_INSERT";
            public const string Delete = "PROC_ESTANDAR_ENTIDADES_DELETE";
            public const string Paradepender = "PROC_ESTANDAR_ENTIDADES_PARADEPENDER";
            public const string Loadcombos = "PROC_ESTANDAR_ENTIDADES_LOADCOMBOS";
            // Experimento: logos de TODAS las entidades en un endpoint aparte del SHOWALL
            // principal — así la grilla (texto) carga instantánea y las imágenes llegan en
            // un segundo pedido HTTP, en paralelo, sin bloquear el primer render.
            public const string Showimages = "PROC_ESTANDAR_ENTIDADES_SHOWIMAGES";
            // Personas activas (COD_TIPO_ENTIDAD='PER', dependientes de la empresa) que
            // todavía NO están asignadas a ningún área/cargo en ADMINISTRACION.AREAS_CARGOS_ENTIDADES.
            public const string EntidadesDisponibles = "PROC_ESTANDAR_ENTIDADES_ENTIDADES_DISPONIBLES";
            // Variantes filtradas por ESTANDAR.ENTIDADES_TIPOS_ENTIDADES — usadas por
            // pantallas de Entidades reutilizadas con un tipo fijo (ej. "CLI" = Clientes,
            // ver DES_PARAMETRO en SEGURIDAD.APLICACIONES_OPCIONES).
            public const string ShowallPorTipo = "PROC_ESTANDAR_ENTIDADES_TIPOS_ENTIDAD_MODULESHOWALL";
            public const string SearchPorTipo = "PROC_ESTANDAR_ENTIDADES_TIPOS_ENTIDAD_MODULESEARCH";
            // Encadenado: entidades de un COD_TIPO_ENTIDAD que además dependen de otra entidad
            // elegida (ej. "Entidad conductora" dependiente de la "Entidad propietaria" en
            // TransporteDocumentoDialog.razor) — el frontend ya llamaba a este endpoint
            // (ApiRoutes.Estandar.EntidadesDependientes) pero nunca se había creado ni el SP
            // ni la acción del controller, así que el pedido caía en GetById({COD_ENTIDAD})
            // con "dependientes" como valor, y tiraba 400.
            public const string ShowallPorTipoDependiente = "PROC_ESTANDAR_ENTIDADES_TIPOS_ENTIDAD_DEPENDIENTES_MODULESHOWALL";
        }
        public static class EntidadesDocumentosIdentidad
        {
            public const string Showall = "PROC_ESTANDAR_ENTIDADES_DOCUMENTOS_IDENTIDAD_SHOWALL";
            public const string Showbyid = "PROC_ESTANDAR_ENTIDADES_DOCUMENTOS_IDENTIDAD_SHOWBYID";
            public const string Showcombox = "PROC_ESTANDAR_ENTIDADES_DOCUMENTOS_IDENTIDAD_SHOWCOMBOX";
            public const string Search = "PROC_ESTANDAR_ENTIDADES_DOCUMENTOS_IDENTIDAD_SEARCH";
            public const string Insert = "PROC_ESTANDAR_ENTIDADES_DOCUMENTOS_IDENTIDAD_INSERT";
            public const string Update = "PROC_ESTANDAR_ENTIDADES_DOCUMENTOS_IDENTIDAD_UPDATE";
            public const string Delete = "PROC_ESTANDAR_ENTIDADES_DOCUMENTOS_IDENTIDAD_DELETE";
        }
        public static class EntidadesTiposEntidades
        {
            public const string Showall = "PROC_ESTANDAR_ENTIDADES_TIPOS_ENTIDADES_SHOWALL";
            public const string Showbyid = "PROC_ESTANDAR_ENTIDADES_TIPOS_ENTIDADES_SHOWBYID";
            public const string Showcombox = "PROC_ESTANDAR_ENTIDADES_TIPOS_ENTIDADES_SHOWCOMBOX";
            public const string Search = "PROC_ESTANDAR_ENTIDADES_TIPOS_ENTIDADES_SEARCH";
            public const string Insert = "PROC_ESTANDAR_ENTIDADES_TIPOS_ENTIDADES_INSERT";
            public const string Delete = "PROC_ESTANDAR_ENTIDADES_TIPOS_ENTIDADES_DELETE";
            public const string Showwselect = "PROC_ESTANDAR_ENTIDADES_TIPOS_ENTIDADES_SHOWWSELECT";
            public const string Showcomboxid = "PROC_ESTANDAR_ENTIDADES_TIPOS_ENTIDADES_SHOWCOMBOXID";
        }
        public static class Estados
        {
            public const string FromEstadosEstandar = "PROC_ESTANDAR_ESTADOS_FROM_ESTADOS_ESTANDAR_INSERT";
            public const string Showall = "PROC_ESTANDAR_ESTADOS_SHOWALL";
            public const string Showbyid = "PROC_ESTANDAR_ESTADOS_SHOWBYID";
            public const string Showcombox = "PROC_ESTANDAR_ESTADOS_SHOWCOMBOX";
            public const string Search = "PROC_ESTANDAR_ESTADOS_SEARCH";
            public const string Insert = "PROC_ESTANDAR_ESTADOS_INSERT";
            public const string Delete = "PROC_ESTANDAR_ESTADOS_DELETE";
        }
        public static class EstadosEstandar
        {
            public const string Showall = "PROC_ESTANDAR_ESTADOS_ESTANDAR_SHOWALL";
            public const string Showbyid = "PROC_ESTANDAR_ESTADOS_ESTANDAR_SHOWBYID";
            public const string Showcombox = "PROC_ESTANDAR_ESTADOS_ESTANDAR_SHOWCOMBOX";
            public const string Search = "PROC_ESTANDAR_ESTADOS_ESTANDAR_SEARCH";
            public const string Insert = "PROC_ESTANDAR_ESTADOS_ESTANDAR_INSERT";
            public const string Update = "PROC_ESTANDAR_ESTADOS_ESTANDAR_UPDATE";
            public const string Delete = "PROC_ESTANDAR_ESTADOS_ESTANDAR_DELETE";
        }
        public static class FamiliasProducto
        {
            public const string Showall = "PROC_ESTANDAR_FAMILIAS_PRODUCTO_SHOWALL";
            public const string Showbyid = "PROC_ESTANDAR_FAMILIAS_PRODUCTO_SHOWBYID";
            public const string Showcombox = "PROC_ESTANDAR_FAMILIAS_PRODUCTO_SHOWCOMBOX";
            public const string Search = "PROC_ESTANDAR_FAMILIAS_PRODUCTO_SEARCH";
            public const string Insert = "PROC_ESTANDAR_FAMILIAS_PRODUCTO_INSERT";
            public const string Delete = "PROC_ESTANDAR_FAMILIAS_PRODUCTO_DELETE";
            public const string Loadcombos = "PROC_ESTANDAR_FAMILIAS_PRODUCTO_LOADCOMBOS";
        }
        public static class GruposProducto
        {
            public const string Showall = "PROC_ESTANDAR_GRUPOS_PRODUCTO_SHOWALL";
            public const string Showbyid = "PROC_ESTANDAR_GRUPOS_PRODUCTO_SHOWBYID";
            public const string Showcombox = "PROC_ESTANDAR_GRUPOS_PRODUCTO_SHOWCOMBOX";
            public const string Search = "PROC_ESTANDAR_GRUPOS_PRODUCTO_SEARCH";
            public const string Insert = "PROC_ESTANDAR_GRUPOS_PRODUCTO_INSERT";
            public const string Delete = "PROC_ESTANDAR_GRUPOS_PRODUCTO_DELETE";
            public const string Loadcombos = "PROC_ESTANDAR_GRUPOS_PRODUCTO_LOADCOMBOS";
        }
        public static class Monedas
        {
            public const string Showall = "PROC_ESTANDAR_MONEDAS_SHOWALL";
            public const string Showbyid = "PROC_ESTANDAR_MONEDAS_SHOWBYID";
            public const string Showcombox = "PROC_ESTANDAR_MONEDAS_SHOWCOMBOX";
            public const string Search = "PROC_ESTANDAR_MONEDAS_SEARCH";
            public const string Insert = "PROC_ESTANDAR_MONEDAS_INSERT";
            public const string Delete = "PROC_ESTANDAR_MONEDAS_DELETE";
        }
        public static class MonedasTipoCambio
        {
            public const string Showall = "PROC_ESTANDAR_MONEDAS_TIPO_CAMBIO_SHOWALL";
            public const string Showbyid = "PROC_ESTANDAR_MONEDAS_TIPO_CAMBIO_SHOWBYID";
            public const string Showcombox = "PROC_ESTANDAR_MONEDAS_TIPO_CAMBIO_SHOWCOMBOX";
            public const string Search = "PROC_ESTANDAR_MONEDAS_TIPO_CAMBIO_SEARCH";
            public const string Insert = "PROC_ESTANDAR_MONEDAS_TIPO_CAMBIO_INSERT";
            public const string Update = "PROC_ESTANDAR_MONEDAS_TIPO_CAMBIO_UPDATE";
            public const string Delete = "PROC_ESTANDAR_MONEDAS_TIPO_CAMBIO_DELETE";
            /// <summary>Dado moneda (+ fecha opcional), resuelve el tipo de cambio contra la
            /// pivot configurada. Devuelve 1 si la moneda ES la pivot.</summary>
            public const string Obtener = "PROC_ESTANDAR_MONEDAS_TIPO_CAMBIO_OBTENER";
        }
        public static class Motivos
        {
            public const string Showall = "PROC_ESTANDAR_MOTIVOS_SHOWALL";
            public const string Showbyid = "PROC_ESTANDAR_MOTIVOS_SHOWBYID";
            public const string Showcombox = "PROC_ESTANDAR_MOTIVOS_SHOWCOMBOX";
            public const string Search = "PROC_ESTANDAR_MOTIVOS_SEARCH";
            public const string Insert = "PROC_ESTANDAR_MOTIVOS_INSERT";
            public const string Update = "PROC_ESTANDAR_MOTIVOS_UPDATE";
            public const string Delete = "PROC_ESTANDAR_MOTIVOS_DELETE";
        }
        public static class Paises
        {
            public const string Showall    = "PROC_ESTANDAR_PAISES_SHOWALL";
            public const string Showbyid   = "PROC_ESTANDAR_PAISES_SHOWBYID";
            public const string Showcombox = "PROC_ESTANDAR_PAISES_SHOWCOMBOX";
            public const string Search     = "PROC_ESTANDAR_PAISES_SEARCH";
            public const string Insert     = "PROC_ESTANDAR_PAISES_INSERT";
            public const string Update     = "PROC_ESTANDAR_PAISES_UPDATE";
            public const string Delete     = "PROC_ESTANDAR_PAISES_DELETE";
            public const string UploadJson = "PROC_ESTANDAR_PAISES_UPLOAD_JSON";
        }
        public static class Periodos
        {
            public const string Showall = "PROC_ESTANDAR_PERIODOS_SHOWALL";
            public const string Showbyid = "PROC_ESTANDAR_PERIODOS_SHOWBYID";
            public const string Showcombox = "PROC_ESTANDAR_PERIODOS_SHOWCOMBOX";
            public const string Search = "PROC_ESTANDAR_PERIODOS_SEARCH";
            public const string Insert = "PROC_ESTANDAR_PERIODOS_INSERT";
            public const string Update = "PROC_ESTANDAR_PERIODOS_UPDATE";
            public const string Delete = "PROC_ESTANDAR_PERIODOS_DELETE";
        }
        public static class Productos
        {
            public const string Showall = "PROC_ESTANDAR_PRODUCTOS_SHOWALL";
            public const string Showbyid = "PROC_ESTANDAR_PRODUCTOS_SHOWBYID";
            public const string Showcombox = "PROC_ESTANDAR_PRODUCTOS_SHOWCOMBOX";
            public const string Search = "PROC_ESTANDAR_PRODUCTOS_SEARCH";
            public const string Insert = "PROC_ESTANDAR_PRODUCTOS_INSERT";
            public const string Update = "PROC_ESTANDAR_PRODUCTOS_UPDATE";
            public const string Delete = "PROC_ESTANDAR_PRODUCTOS_DELETE";
            public const string Loadcombos = "PROC_ESTANDAR_PRODUCTOS_LOAD_COMBOS";
            public const string ActualizarDescripcionDetalle = "PROC_ESTANDAR_PRODUCTOS_DESCRIPCION_DETALLE_UPDATE";
            // Panel lateral de filtro por Especificaciones Tecnicas.
            public const string EspecificacionesArbol   = "PROC_ESTANDAR_PRODUCTOS_ESPECIFICACIONES_ARBOL";
            public const string FiltrarEspecificaciones = "PROC_ESTANDAR_PRODUCTOS_FILTRAR_ESPECIFICACIONES";
        }
        public static class ProductosEspecificacionesTecnicas
        {
            public const string Showall = "PROC_ESTANDAR_PRODUCTOS_ESPECIFICACIONES_TECNICAS_SHOWALL";
            public const string Showbyid = "PROC_ESTANDAR_PRODUCTOS_ESPECIFICACIONES_TECNICAS_SHOWBYID";
            public const string Showcombox = "PROC_ESTANDAR_PRODUCTOS_ESPECIFICACIONES_TECNICAS_SHOWCOMBOX";
            public const string Search = "PROC_ESTANDAR_PRODUCTOS_ESPECIFICACIONES_TECNICAS_SEARCH";
            public const string Insert = "PROC_ESTANDAR_PRODUCTOS_ESPECIFICACIONES_TECNICAS_INSERT";
            public const string Delete = "PROC_ESTANDAR_PRODUCTOS_ESPECIFICACIONES_TECNICAS_DELETE";
        }
        public static class Sexos
        {
            public const string Showall = "PROC_ESTANDAR_SEXOS_SHOWALL";
            public const string Showbyid = "PROC_ESTANDAR_SEXOS_SHOWBYID";
            public const string Showcombox = "PROC_ESTANDAR_SEXOS_SHOWCOMBOX";
            public const string Search = "PROC_ESTANDAR_SEXOS_SEARCH";
            public const string Insert = "PROC_ESTANDAR_SEXOS_INSERT";
            public const string Update = "PROC_ESTANDAR_SEXOS_UPDATE";
            public const string Delete = "PROC_ESTANDAR_SEXOS_DELETE";
        }
        // Sin SEARCH — el dump de PROC_ESTANDAR_TIPOS_PERSONAS_ solo trae 5 SPs (sin _SEARCH).
        public static class TiposPersonas
        {
            public const string Showall = "PROC_ESTANDAR_TIPOS_PERSONAS_SHOWALL";
            public const string Showbyid = "PROC_ESTANDAR_TIPOS_PERSONAS_SHOWBYID";
            public const string Showcombox = "PROC_ESTANDAR_TIPOS_PERSONAS_SHOWCOMBOX";
            public const string Insert = "PROC_ESTANDAR_TIPOS_PERSONAS_INSERT";
            public const string Delete = "PROC_ESTANDAR_TIPOS_PERSONAS_DELETE";
        }
        // Sin SEARCH — el dump de PROC_ESTANDAR_ANOS_ solo trae 5 SPs (sin _SEARCH).
        public static class Anos
        {
            public const string Showall = "PROC_ESTANDAR_ANOS_SHOWALL";
            public const string Showbyid = "PROC_ESTANDAR_ANOS_SHOWBYID";
            public const string Showcombox = "PROC_ESTANDAR_ANOS_SHOWCOMBOX";
            public const string Insert = "PROC_ESTANDAR_ANOS_INSERT";
            public const string Delete = "PROC_ESTANDAR_ANOS_DELETE";
        }
        // Sin SEARCH — el dump de PROC_ESTANDAR_MESES_ solo trae 5 SPs (sin _SEARCH).
        public static class Meses
        {
            public const string Showall = "PROC_ESTANDAR_MESES_SHOWALL";
            public const string Showbyid = "PROC_ESTANDAR_MESES_SHOWBYID";
            public const string Showcombox = "PROC_ESTANDAR_MESES_SHOWCOMBOX";
            public const string Insert = "PROC_ESTANDAR_MESES_INSERT";
            public const string Delete = "PROC_ESTANDAR_MESES_DELETE";
        }
        public static class SubFamiliasProducto
        {
            public const string Showall = "PROC_ESTANDAR_SUB_FAMILIAS_PRODUCTO_SHOWALL";
            public const string Showbyid = "PROC_ESTANDAR_SUB_FAMILIAS_PRODUCTO_SHOWBYID";
            public const string Showcombox = "PROC_ESTANDAR_SUB_FAMILIAS_PRODUCTO_SHOWCOMBOX";
            public const string Search = "PROC_ESTANDAR_SUB_FAMILIAS_PRODUCTO_SEARCH";
            public const string Insert = "PROC_ESTANDAR_SUB_FAMILIAS_PRODUCTO_INSERT";
            public const string Delete = "PROC_ESTANDAR_SUB_FAMILIAS_PRODUCTO_DELETE";
            public const string Loadcombos = "PROC_ESTANDAR_SUB_FAMILIAS_PRODUCTO_LOADCOMBOS";
        }
        public static class Sucursales
        {
            public const string Showall = "PROC_ESTANDAR_SUCURSALES_SHOWALL";
            public const string Showbyid = "PROC_ESTANDAR_SUCURSALES_SHOWBYID";
            public const string Showcombox = "PROC_ESTANDAR_SUCURSALES_SHOWCOMBOX";
            public const string Search = "PROC_ESTANDAR_SUCURSALES_SEARCH";
            public const string Insert = "PROC_ESTANDAR_SUCURSALES_INSERT";
            public const string Update = "PROC_ESTANDAR_SUCURSALES_UPDATE";
            public const string Delete = "PROC_ESTANDAR_SUCURSALES_DELETE";
            public const string Loadcombos = "PROC_ESTANDAR_SUCURSALES_LOADCOMBOS";
        }
        public static class SucursalesSerieComprobante
        {
            public const string Showall = "PROC_ESTANDAR_SUCURSALES_SERIE_COMPROBANTE_SHOWALL";
            public const string Showbyid = "PROC_ESTANDAR_SUCURSALES_SERIE_COMPROBANTE_SHOWBYID";
            public const string Showcombox = "PROC_ESTANDAR_SUCURSALES_SERIE_COMPROBANTE_SHOWCOMBOX";
            public const string Search = "PROC_ESTANDAR_SUCURSALES_SERIE_COMPROBANTE_SEARCH";
            public const string Insert = "PROC_ESTANDAR_SUCURSALES_SERIE_COMPROBANTE_INSERT";
            public const string Update = "PROC_ESTANDAR_SUCURSALES_SERIE_COMPROBANTE_UPDATE";
            public const string Delete = "PROC_ESTANDAR_SUCURSALES_SERIE_COMPROBANTE_DELETE";
        }
        public static class SucursalesSerieComprobantesAplicadas
        {
            public const string Showall = "PROC_ESTANDAR_SUCURSALES_SERIE_COMPROBANTES_APLICADAS_SHOWALL";
            public const string Showbyid = "PROC_ESTANDAR_SUCURSALES_SERIE_COMPROBANTES_APLICADAS_SHOWBYID";
            public const string Showcombox = "PROC_ESTANDAR_SUCURSALES_SERIE_COMPROBANTES_APLICADAS_SHOWCOMBOX";
            public const string Search = "PROC_ESTANDAR_SUCURSALES_SERIE_COMPROBANTES_APLICADAS_SEARCH";
            public const string Insert = "PROC_ESTANDAR_SUCURSALES_SERIE_COMPROBANTES_APLICADAS_INSERT";
            public const string Update = "PROC_ESTANDAR_SUCURSALES_SERIE_COMPROBANTES_APLICADAS_UPDATE";
            public const string Delete = "PROC_ESTANDAR_SUCURSALES_SERIE_COMPROBANTES_APLICADAS_DELETE";
        }
        public static class SucursalesTiposComprobantePago
        {
            public const string Showall = "PROC_ESTANDAR_SUCURSALES_TIPOS_COMPROBANTE_PAGO_SHOWALL";
            public const string Showbyid = "PROC_ESTANDAR_SUCURSALES_TIPOS_COMPROBANTE_PAGO_SHOWBYID";
            public const string Showcombox = "PROC_ESTANDAR_SUCURSALES_TIPOS_COMPROBANTE_PAGO_SHOWCOMBOX";
            public const string Search = "PROC_ESTANDAR_SUCURSALES_TIPOS_COMPROBANTE_PAGO_SEARCH";
            public const string Insert = "PROC_ESTANDAR_SUCURSALES_TIPOS_COMPROBANTE_PAGO_INSERT";
            public const string Delete = "PROC_ESTANDAR_SUCURSALES_TIPOS_COMPROBANTE_PAGO_DELETE";
        }
        public static class TiposComprobantesPago
        {
            public const string Showall = "PROC_ESTANDAR_TIPOS_COMPROBANTES_PAGO_SHOWALL";
            public const string Showbyid = "PROC_ESTANDAR_TIPOS_COMPROBANTES_PAGO_SHOWBYID";
            public const string Showcombox = "PROC_ESTANDAR_TIPOS_COMPROBANTES_PAGO_SHOWCOMBOX";
            public const string Search = "PROC_ESTANDAR_TIPOS_COMPROBANTES_PAGO_SEARCH";
            public const string Insert = "PROC_ESTANDAR_TIPOS_COMPROBANTES_PAGO_INSERT";
            public const string Update = "PROC_ESTANDAR_TIPOS_COMPROBANTES_PAGO_UPDATE";
            public const string Delete = "PROC_ESTANDAR_TIPOS_COMPROBANTES_PAGO_DELETE";
        }
        public static class TiposDocumentoIdentidad
        {
            public const string Showall = "PROC_ESTANDAR_TIPOS_DOCUMENTO_IDENTIDAD_SHOWALL";
            public const string Showbyid = "PROC_ESTANDAR_TIPOS_DOCUMENTO_IDENTIDAD_SHOWBYID";
            public const string Showcombox = "PROC_ESTANDAR_TIPOS_DOCUMENTO_IDENTIDAD_SHOWCOMBOX";
            public const string Search = "PROC_ESTANDAR_TIPOS_DOCUMENTO_IDENTIDAD_SEARCH";
            public const string Insert = "PROC_ESTANDAR_TIPOS_DOCUMENTO_IDENTIDAD_INSERT";
            public const string Update = "PROC_ESTANDAR_TIPOS_DOCUMENTO_IDENTIDAD_UPDATE";
            public const string Delete = "PROC_ESTANDAR_TIPOS_DOCUMENTO_IDENTIDAD_DELETE";
        }
        public static class TiposEmpresa
        {
            public const string Showall = "PROC_ESTANDAR_TIPOS_EMPRESA_SHOWALL";
            public const string Showbyid = "PROC_ESTANDAR_TIPOS_EMPRESA_SHOWBYID";
            public const string Showcombox = "PROC_ESTANDAR_TIPOS_EMPRESA_SHOWCOMBOX";
            public const string Search = "PROC_ESTANDAR_TIPOS_EMPRESA_SEARCH";
            public const string Insert = "PROC_ESTANDAR_TIPOS_EMPRESA_INSERT";
            public const string Update = "PROC_ESTANDAR_TIPOS_EMPRESA_UPDATE";
            public const string Delete = "PROC_ESTANDAR_TIPOS_EMPRESA_DELETE";
        }
        public static class TiposEntidades
        {
            public const string Showall = "PROC_ESTANDAR_TIPOS_ENTIDADES_SHOWALL";
            public const string Showbyid = "PROC_ESTANDAR_TIPOS_ENTIDADES_SHOWBYID";
            public const string Showcombox = "PROC_ESTANDAR_TIPOS_ENTIDADES_SHOWCOMBOX";
            public const string Search = "PROC_ESTANDAR_TIPOS_ENTIDADES_SEARCH";
            public const string Insert = "PROC_ESTANDAR_TIPOS_ENTIDADES_INSERT";
            public const string Update = "PROC_ESTANDAR_TIPOS_ENTIDADES_UPDATE";
            public const string Delete = "PROC_ESTANDAR_TIPOS_ENTIDADES_DELETE";
        }
        public static class TiposEstados
        {
            public const string Showall = "PROC_ESTANDAR_TIPOS_ESTADOS_SHOWALL";
            public const string Showbyid = "PROC_ESTANDAR_TIPOS_ESTADOS_SHOWBYID";
            public const string Showcombox = "PROC_ESTANDAR_TIPOS_ESTADOS_SHOWCOMBOX";
            public const string Search = "PROC_ESTANDAR_TIPOS_ESTADOS_SEARCH";
            public const string Insert = "PROC_ESTANDAR_TIPOS_ESTADOS_INSERT";
            public const string Update = "PROC_ESTANDAR_TIPOS_ESTADOS_UPDATE";
            public const string Delete = "PROC_ESTANDAR_TIPOS_ESTADOS_DELETE";
        }
        public static class TiposMotivos
        {
            public const string Showall = "PROC_ESTANDAR_TIPOS_MOTIVOS_SHOWALL";
            public const string Showbyid = "PROC_ESTANDAR_TIPOS_MOTIVOS_SHOWBYID";
            public const string Showcombox = "PROC_ESTANDAR_TIPOS_MOTIVOS_SHOWCOMBOX";
            public const string Search = "PROC_ESTANDAR_TIPOS_MOTIVOS_SEARCH";
            public const string Insert = "PROC_ESTANDAR_TIPOS_MOTIVOS_INSERT";
            public const string Update = "PROC_ESTANDAR_TIPOS_MOTIVOS_UPDATE";
            public const string Delete = "PROC_ESTANDAR_TIPOS_MOTIVOS_DELETE";
        }
        public static class TiposOperacion
        {
            public const string Showall = "PROC_ESTANDAR_TIPOS_OPERACION_SHOWALL";
            public const string Showbyid = "PROC_ESTANDAR_TIPOS_OPERACION_SHOWBYID";
            public const string Showcombox = "PROC_ESTANDAR_TIPOS_OPERACION_SHOWCOMBOX";
            public const string Search = "PROC_ESTANDAR_TIPOS_OPERACION_SEARCH";
            public const string Insert = "PROC_ESTANDAR_TIPOS_OPERACION_INSERT";
            public const string Update = "PROC_ESTANDAR_TIPOS_OPERACION_UPDATE";
            public const string Delete = "PROC_ESTANDAR_TIPOS_OPERACION_DELETE";
            public const string Loadcombos = "PROC_ESTANDAR_TIPOS_OPERACION_LOADCOMBOS";
        }
        public static class TiposPeriodo
        {
            public const string Showall = "PROC_ESTANDAR_TIPOS_PERIODO_SHOWALL";
            public const string Showbyid = "PROC_ESTANDAR_TIPOS_PERIODO_SHOWBYID";
            public const string Showcombox = "PROC_ESTANDAR_TIPOS_PERIODO_SHOWCOMBOX";
            public const string Search = "PROC_ESTANDAR_TIPOS_PERIODO_SEARCH";
            public const string Insert = "PROC_ESTANDAR_TIPOS_PERIODO_INSERT";
            public const string Update = "PROC_ESTANDAR_TIPOS_PERIODO_UPDATE";
            public const string Delete = "PROC_ESTANDAR_TIPOS_PERIODO_DELETE";
        }
        public static class TiposProducto
        {
            public const string Showall = "PROC_ESTANDAR_TIPOS_PRODUCTO_SHOWALL";
            public const string Showbyid = "PROC_ESTANDAR_TIPOS_PRODUCTO_SHOWBYID";
            public const string Showcombox = "PROC_ESTANDAR_TIPOS_PRODUCTO_SHOWCOMBOX";
            public const string Search = "PROC_ESTANDAR_TIPOS_PRODUCTO_SEARCH";
            public const string Insert = "PROC_ESTANDAR_TIPOS_PRODUCTO_INSERT";
            public const string Delete = "PROC_ESTANDAR_TIPOS_PRODUCTO_DELETE";
        }
        public static class TiposSucursales
        {
            public const string Showall = "PROC_ESTANDAR_TIPOS_SUCURSALES_SHOWALL";
            public const string Showbyid = "PROC_ESTANDAR_TIPOS_SUCURSALES_SHOWBYID";
            public const string Showcombox = "PROC_ESTANDAR_TIPOS_SUCURSALES_SHOWCOMBOX";
            public const string Search = "PROC_ESTANDAR_TIPOS_SUCURSALES_SEARCH";
            public const string Insert = "PROC_ESTANDAR_TIPOS_SUCURSALES_INSERT";
            public const string Update = "PROC_ESTANDAR_TIPOS_SUCURSALES_UPDATE";
            public const string Delete = "PROC_ESTANDAR_TIPOS_SUCURSALES_DELETE";
        }
        public static class TiposUnidadMedida
        {
            public const string Showall    = "PROC_ESTANDAR_TIPOS_UNIDAD_MEDIDA_SHOWALL";
            public const string Showbyid   = "PROC_ESTANDAR_TIPOS_UNIDAD_MEDIDA_SHOWBYID";
            public const string Showcombox = "PROC_ESTANDAR_TIPOS_UNIDAD_MEDIDA_SHOWCOMBOX";
            public const string Search     = "PROC_ESTANDAR_TIPOS_UNIDAD_MEDIDA_SEARCH";
            public const string Insert     = "PROC_ESTANDAR_TIPOS_UNIDAD_MEDIDA_INSERT";
            public const string Update     = "PROC_ESTANDAR_TIPOS_UNIDAD_MEDIDA_UPDATE";
            public const string Delete     = "PROC_ESTANDAR_TIPOS_UNIDAD_MEDIDA_DELETE";
            public const string UploadJson = "PROC_ESTANDAR_TIPOS_UNIDAD_MEDIDA_UPLOAD_JSON";
        }
        public static class TiposVia
        {
            public const string Showall = "PROC_ESTANDAR_TIPOS_VIA_SHOWALL";
            public const string Showbyid = "PROC_ESTANDAR_TIPOS_VIA_SHOWBYID";
            public const string Showcombox = "PROC_ESTANDAR_TIPOS_VIA_SHOWCOMBOX";
            public const string Search = "PROC_ESTANDAR_TIPOS_VIA_SEARCH";
            public const string Insert = "PROC_ESTANDAR_TIPOS_VIA_INSERT";
            public const string Update = "PROC_ESTANDAR_TIPOS_VIA_UPDATE";
            public const string Delete = "PROC_ESTANDAR_TIPOS_VIA_DELETE";
        }
        public static class EtiquetasBusquedaAreas
        {
            public const string Showall = "PROC_ESTANDAR_ETIQUETAS_BUSQUEDA_AREAS_SHOWALL";
            public const string Showbyid = "PROC_ESTANDAR_ETIQUETAS_BUSQUEDA_AREAS_SHOWBYID";
            public const string Showcombox = "PROC_ESTANDAR_ETIQUETAS_BUSQUEDA_AREAS_SHOWCOMBOX";
            public const string Insert = "PROC_ESTANDAR_ETIQUETAS_BUSQUEDA_AREAS_INSERT";
            public const string Delete = "PROC_ESTANDAR_ETIQUETAS_BUSQUEDA_AREAS_DELETE";
        }
        public static class TurnosTrabajo
        {
            public const string Showall = "PROC_ESTANDAR_TURNOS_TRABAJO_SHOWALL";
            public const string Showbyid = "PROC_ESTANDAR_TURNOS_TRABAJO_SHOWBYID";
            public const string Showcombox = "PROC_ESTANDAR_TURNOS_TRABAJO_SHOWCOMBOX";
            public const string Search = "PROC_ESTANDAR_TURNOS_TRABAJO_SEARCH";
            public const string Insert = "PROC_ESTANDAR_TURNOS_TRABAJO_INSERT";
            public const string Update = "PROC_ESTANDAR_TURNOS_TRABAJO_UPDATE";
            public const string Delete = "PROC_ESTANDAR_TURNOS_TRABAJO_DELETE";
        }
        public static class Ubigeos
        {
            public const string Showall    = "PROC_ESTANDAR_UBIGEOS_SHOWALL";
            public const string Showbyid   = "PROC_ESTANDAR_UBIGEOS_SHOWBYID";
            public const string Showcombox = "PROC_ESTANDAR_UBIGEOS_SHOWCOMBOX";
            public const string Search     = "PROC_ESTANDAR_UBIGEOS_SEARCH";
            public const string Insert     = "PROC_ESTANDAR_UBIGEOS_INSERT";
            public const string Update     = "PROC_ESTANDAR_UBIGEOS_UPDATE";
            public const string Delete     = "PROC_ESTANDAR_UBIGEOS_DELETE";
            public const string UploadJson = "PROC_ESTANDAR_UBIGEOS_UPLOAD_JSON";
        }
        public static class UnidadesMedida
        {
            public const string Showall    = "PROC_ESTANDAR_UNIDADES_MEDIDA_SHOWALL";
            public const string Showbyid   = "PROC_ESTANDAR_UNIDADES_MEDIDA_SHOWBYID";
            public const string Showcombox = "PROC_ESTANDAR_UNIDADES_MEDIDA_SHOWCOMBOX";
            public const string Search     = "PROC_ESTANDAR_UNIDADES_MEDIDA_SEARCH";
            public const string Insert     = "PROC_ESTANDAR_UNIDADES_MEDIDA_INSERT";
            public const string Update     = "PROC_ESTANDAR_UNIDADES_MEDIDA_UPDATE";
            public const string Delete     = "PROC_ESTANDAR_UNIDADES_MEDIDA_DELETE";
            public const string UploadJson = "PROC_ESTANDAR_UNIDADES_MEDIDA_UPLOAD_JSON";
        }
        public static class UnidadesMedidaEquivalencias
        {
            public const string Showall    = "PROC_ESTANDAR_UNIDADES_MEDIDA_EQUIVALENCIAS_SHOWALL";
            public const string Showbyid   = "PROC_ESTANDAR_UNIDADES_MEDIDA_EQUIVALENCIAS_SHOWBYID";
            public const string Showcombox = "PROC_ESTANDAR_UNIDADES_MEDIDA_EQUIVALENCIAS_SHOWCOMBOX";
            public const string Search     = "PROC_ESTANDAR_UNIDADES_MEDIDA_EQUIVALENCIAS_SEARCH";
            public const string ShowByTipo     = "PROC_ESTANDAR_UNIDADES_MEDIDA_EQUIVALENCIAS_SHOWBYTIPO";
            public const string Upsert         = "PROC_ESTANDAR_UNIDADES_MEDIDA_EQUIVALENCIAS_UPSERT";
            public const string CargarEstandar = "PROC_ESTANDAR_UNIDADES_MEDIDA_EQUIVALENCIAS_CARGAR_ESTANDAR";
            public const string Insert         = "PROC_ESTANDAR_UNIDADES_MEDIDA_EQUIVALENCIAS_INSERT";
            public const string Update         = "PROC_ESTANDAR_UNIDADES_MEDIDA_EQUIVALENCIAS_UPDATE";
            public const string Delete         = "PROC_ESTANDAR_UNIDADES_MEDIDA_EQUIVALENCIAS_DELETE";
        }
    }
    public static class SCHEMAFinanzas
    {
        public static class Bancos
        {
            public const string Showall = "PROC_FINANZAS_BANCOS_SHOWALL";
            public const string Showbyid = "PROC_FINANZAS_BANCOS_SHOWBYID";
            public const string Showcombox = "PROC_FINANZAS_BANCOS_SHOWCOMBOX";
            public const string Search = "PROC_FINANZAS_BANCOS_SEARCH";
            public const string Insert = "PROC_FINANZAS_BANCOS_INSERT";
            public const string Update = "PROC_FINANZAS_BANCOS_UPDATE";
            public const string Delete = "PROC_FINANZAS_BANCOS_DELETE";
        }
        public static class BancosCuentasBancarias
        {
            public const string Showall = "PROC_FINANZAS_BANCOS_CUENTAS_BANCARIAS_SHOWALL";
            public const string Showbyid = "PROC_FINANZAS_BANCOS_CUENTAS_BANCARIAS_SHOWBYID";
            public const string Showcombox = "PROC_FINANZAS_BANCOS_CUENTAS_BANCARIAS_SHOWCOMBOX";
            public const string Search = "PROC_FINANZAS_BANCOS_CUENTAS_BANCARIAS_SEARCH";
            public const string Insert = "PROC_FINANZAS_BANCOS_CUENTAS_BANCARIAS_INSERT";
            public const string Update = "PROC_FINANZAS_BANCOS_CUENTAS_BANCARIAS_UPDATE";
            public const string Delete = "PROC_FINANZAS_BANCOS_CUENTAS_BANCARIAS_DELETE";
        }
        public static class Cajas
        {
            public const string Showall = "PROC_FINANZAS_CAJAS_SHOWALL";
            public const string Showbyid = "PROC_FINANZAS_CAJAS_SHOWBYID";
            public const string Showcombox = "PROC_FINANZAS_CAJAS_SHOWCOMBOX";
            public const string Search = "PROC_FINANZAS_CAJAS_SEARCH";
            public const string Insert = "PROC_FINANZAS_CAJAS_INSERT";
            public const string Update = "PROC_FINANZAS_CAJAS_UPDATE";
            public const string Delete = "PROC_FINANZAS_CAJAS_DELETE";
        }
        public static class CajasSaldos
        {
            public const string Showall = "PROC_FINANZAS_CAJAS_SALDOS_SHOWALL";
            public const string Showbyid = "PROC_FINANZAS_CAJAS_SALDOS_SHOWBYID";
            public const string Showcombox = "PROC_FINANZAS_CAJAS_SALDOS_SHOWCOMBOX";
            public const string Search = "PROC_FINANZAS_CAJAS_SALDOS_SEARCH";
            public const string Insert = "PROC_FINANZAS_CAJAS_SALDOS_INSERT";
            public const string Update = "PROC_FINANZAS_CAJAS_SALDOS_UPDATE";
            public const string Delete = "PROC_FINANZAS_CAJAS_SALDOS_DELETE";
        }
        public static class ComprobantesPago
        {
            public const string Showall = "PROC_FINANZAS_COMPROBANTES_PAGO_SHOWALL";
            public const string Showbyid = "PROC_FINANZAS_COMPROBANTES_PAGO_SHOWBYID";
            public const string Showcombox = "PROC_FINANZAS_COMPROBANTES_PAGO_SHOWCOMBOX";
            public const string Search = "PROC_FINANZAS_COMPROBANTES_PAGO_SEARCH";
            public const string Insert = "PROC_FINANZAS_COMPROBANTES_PAGO_INSERT";
            public const string Update = "PROC_FINANZAS_COMPROBANTES_PAGO_UPDATE";
            public const string Delete = "PROC_FINANZAS_COMPROBANTES_PAGO_DELETE";
        }
        public static class ComprobantesPagoAplicados
        {
            public const string Showall = "PROC_FINANZAS_COMPROBANTES_PAGO_APLICADOS_SHOWALL";
            public const string Showbyid = "PROC_FINANZAS_COMPROBANTES_PAGO_APLICADOS_SHOWBYID";
            public const string Showcombox = "PROC_FINANZAS_COMPROBANTES_PAGO_APLICADOS_SHOWCOMBOX";
            public const string Search = "PROC_FINANZAS_COMPROBANTES_PAGO_APLICADOS_SEARCH";
            public const string Insert = "PROC_FINANZAS_COMPROBANTES_PAGO_APLICADOS_INSERT";
            public const string Delete = "PROC_FINANZAS_COMPROBANTES_PAGO_APLICADOS_DELETE";
        }
        public static class ComprobantesPagoAsociados
        {
            public const string Showall = "PROC_FINANZAS_COMPROBANTES_PAGO_ASOCIADOS_SHOWALL";
            public const string Showbyid = "PROC_FINANZAS_COMPROBANTES_PAGO_ASOCIADOS_SHOWBYID";
            public const string Showcombox = "PROC_FINANZAS_COMPROBANTES_PAGO_ASOCIADOS_SHOWCOMBOX";
            public const string Search = "PROC_FINANZAS_COMPROBANTES_PAGO_ASOCIADOS_SEARCH";
            public const string Insert = "PROC_FINANZAS_COMPROBANTES_PAGO_ASOCIADOS_INSERT";
            public const string Delete = "PROC_FINANZAS_COMPROBANTES_PAGO_ASOCIADOS_DELETE";
        }
        public static class ComprobantesPagoCalculo
        {
            public const string Showall = "PROC_FINANZAS_COMPROBANTES_PAGO_CALCULO_SHOWALL";
            public const string Showbyid = "PROC_FINANZAS_COMPROBANTES_PAGO_CALCULO_SHOWBYID";
            public const string Showcombox = "PROC_FINANZAS_COMPROBANTES_PAGO_CALCULO_SHOWCOMBOX";
            public const string Search = "PROC_FINANZAS_COMPROBANTES_PAGO_CALCULO_SEARCH";
            public const string Insert = "PROC_FINANZAS_COMPROBANTES_PAGO_CALCULO_INSERT";
            public const string Update = "PROC_FINANZAS_COMPROBANTES_PAGO_CALCULO_UPDATE";
            public const string Delete = "PROC_FINANZAS_COMPROBANTES_PAGO_CALCULO_DELETE";
        }
        public static class ComprobantesPagoCronograma
        {
            public const string Showall = "PROC_FINANZAS_COMPROBANTES_PAGO_CRONOGRAMA_SHOWALL";
            public const string Showbyid = "PROC_FINANZAS_COMPROBANTES_PAGO_CRONOGRAMA_SHOWBYID";
            public const string Showcombox = "PROC_FINANZAS_COMPROBANTES_PAGO_CRONOGRAMA_SHOWCOMBOX";
            public const string Search = "PROC_FINANZAS_COMPROBANTES_PAGO_CRONOGRAMA_SEARCH";
            public const string Insert = "PROC_FINANZAS_COMPROBANTES_PAGO_CRONOGRAMA_INSERT";
            public const string Update = "PROC_FINANZAS_COMPROBANTES_PAGO_CRONOGRAMA_UPDATE";
            public const string Delete = "PROC_FINANZAS_COMPROBANTES_PAGO_CRONOGRAMA_DELETE";
        }
        public static class ComprobantesPagoCronogramaCancelacion
        {
            public const string Showall = "PROC_FINANZAS_COMPROBANTES_PAGO_CRONOGRAMA_CANCELACION_SHOWALL";
            public const string Showbyid = "PROC_FINANZAS_COMPROBANTES_PAGO_CRONOGRAMA_CANCELACION_SHOWBYID";
            public const string Showcombox = "PROC_FINANZAS_COMPROBANTES_PAGO_CRONOGRAMA_CANCELACION_SHOWCOMBOX";
            public const string Search = "PROC_FINANZAS_COMPROBANTES_PAGO_CRONOGRAMA_CANCELACION_SEARCH";
            public const string Insert = "PROC_FINANZAS_COMPROBANTES_PAGO_CRONOGRAMA_CANCELACION_INSERT";
            public const string Update = "PROC_FINANZAS_COMPROBANTES_PAGO_CRONOGRAMA_CANCELACION_UPDATE";
            public const string Delete = "PROC_FINANZAS_COMPROBANTES_PAGO_CRONOGRAMA_CANCELACION_DELETE";
        }
        public static class ComprobantesPagoDetalle
        {
            public const string Showall = "PROC_FINANZAS_COMPROBANTES_PAGO_DETALLE_SHOWALL";
            public const string Showbyid = "PROC_FINANZAS_COMPROBANTES_PAGO_DETALLE_SHOWBYID";
            public const string Showcombox = "PROC_FINANZAS_COMPROBANTES_PAGO_DETALLE_SHOWCOMBOX";
            public const string Search = "PROC_FINANZAS_COMPROBANTES_PAGO_DETALLE_SEARCH";
            public const string Insert = "PROC_FINANZAS_COMPROBANTES_PAGO_DETALLE_INSERT";
            public const string Update = "PROC_FINANZAS_COMPROBANTES_PAGO_DETALLE_UPDATE";
            public const string Delete = "PROC_FINANZAS_COMPROBANTES_PAGO_DETALLE_DELETE";
        }
        public static class ComprobantesPagoMovimientosAlmacen
        {
            public const string Showall = "PROC_FINANZAS_COMPROBANTES_PAGO_MOVIMIENTOS_ALMACEN_SHOWALL";
            public const string Showbyid = "PROC_FINANZAS_COMPROBANTES_PAGO_MOVIMIENTOS_ALMACEN_SHOWBYID";
            public const string Showcombox = "PROC_FINANZAS_COMPROBANTES_PAGO_MOVIMIENTOS_ALMACEN_SHOWCOMBOX";
            public const string Search = "PROC_FINANZAS_COMPROBANTES_PAGO_MOVIMIENTOS_ALMACEN_SEARCH";
            public const string Insert = "PROC_FINANZAS_COMPROBANTES_PAGO_MOVIMIENTOS_ALMACEN_INSERT";
            public const string Delete = "PROC_FINANZAS_COMPROBANTES_PAGO_MOVIMIENTOS_ALMACEN_DELETE";
        }
        public static class ConceptosFinancieros
        {
            public const string Showall = "PROC_FINANZAS_CONCEPTOS_FINANCIEROS_SHOWALL";
            public const string Showbyid = "PROC_FINANZAS_CONCEPTOS_FINANCIEROS_SHOWBYID";
            public const string Showcombox = "PROC_FINANZAS_CONCEPTOS_FINANCIEROS_SHOWCOMBOX";
            public const string Search = "PROC_FINANZAS_CONCEPTOS_FINANCIEROS_SEARCH";
            public const string Insert = "PROC_FINANZAS_CONCEPTOS_FINANCIEROS_INSERT";
            public const string Update = "PROC_FINANZAS_CONCEPTOS_FINANCIEROS_UPDATE";
            public const string Delete = "PROC_FINANZAS_CONCEPTOS_FINANCIEROS_DELETE";
        }
        public static class CuentaCorriente
        {
            public const string Showall = "PROC_FINANZAS_CUENTA_CORRIENTE_SHOWALL";
            public const string Showbyid = "PROC_FINANZAS_CUENTA_CORRIENTE_SHOWBYID";
            public const string Showcombox = "PROC_FINANZAS_CUENTA_CORRIENTE_SHOWCOMBOX";
            public const string Search = "PROC_FINANZAS_CUENTA_CORRIENTE_SEARCH";
            public const string Insert = "PROC_FINANZAS_CUENTA_CORRIENTE_INSERT";
            public const string Update = "PROC_FINANZAS_CUENTA_CORRIENTE_UPDATE";
            public const string Delete = "PROC_FINANZAS_CUENTA_CORRIENTE_DELETE";
        }
        public static class EntidadesCuentasBancarias
        {
            public const string Showall = "PROC_FINANZAS_ENTIDADES_CUENTAS_BANCARIAS_SHOWALL";
            public const string Showbyid = "PROC_FINANZAS_ENTIDADES_CUENTAS_BANCARIAS_SHOWBYID";
            public const string Showcombox = "PROC_FINANZAS_ENTIDADES_CUENTAS_BANCARIAS_SHOWCOMBOX";
            public const string Search = "PROC_FINANZAS_ENTIDADES_CUENTAS_BANCARIAS_SEARCH";
            public const string Insert = "PROC_FINANZAS_ENTIDADES_CUENTAS_BANCARIAS_INSERT";
            public const string Update = "PROC_FINANZAS_ENTIDADES_CUENTAS_BANCARIAS_UPDATE";
            public const string Delete = "PROC_FINANZAS_ENTIDADES_CUENTAS_BANCARIAS_DELETE";
        }
        public static class OperacionesCajaBancos
        {
            public const string Showall = "PROC_FINANZAS_OPERACIONES_CAJA_BANCOS_SHOWALL";
            public const string Showbyid = "PROC_FINANZAS_OPERACIONES_CAJA_BANCOS_SHOWBYID";
            public const string Showcombox = "PROC_FINANZAS_OPERACIONES_CAJA_BANCOS_SHOWCOMBOX";
            public const string Search = "PROC_FINANZAS_OPERACIONES_CAJA_BANCOS_SEARCH";
            public const string Insert = "PROC_FINANZAS_OPERACIONES_CAJA_BANCOS_INSERT";
            public const string Update = "PROC_FINANZAS_OPERACIONES_CAJA_BANCOS_UPDATE";
            public const string Delete = "PROC_FINANZAS_OPERACIONES_CAJA_BANCOS_DELETE";
        }
        public static class OperacionesCajaBancosDetalle
        {
            public const string Showall = "PROC_FINANZAS_OPERACIONES_CAJA_BANCOS_DETALLE_SHOWALL";
            public const string Showbyid = "PROC_FINANZAS_OPERACIONES_CAJA_BANCOS_DETALLE_SHOWBYID";
            public const string Showcombox = "PROC_FINANZAS_OPERACIONES_CAJA_BANCOS_DETALLE_SHOWCOMBOX";
            public const string Search = "PROC_FINANZAS_OPERACIONES_CAJA_BANCOS_DETALLE_SEARCH";
            public const string Insert = "PROC_FINANZAS_OPERACIONES_CAJA_BANCOS_DETALLE_INSERT";
            public const string Update = "PROC_FINANZAS_OPERACIONES_CAJA_BANCOS_DETALLE_UPDATE";
            public const string Delete = "PROC_FINANZAS_OPERACIONES_CAJA_BANCOS_DETALLE_DELETE";
        }
        public static class OperacionesCajaBancosDetalleCalculo
        {
            public const string Showall = "PROC_FINANZAS_OPERACIONES_CAJA_BANCOS_DETALLE_CALCULO_SHOWALL";
            public const string Showbyid = "PROC_FINANZAS_OPERACIONES_CAJA_BANCOS_DETALLE_CALCULO_SHOWBYID";
            public const string Showcombox = "PROC_FINANZAS_OPERACIONES_CAJA_BANCOS_DETALLE_CALCULO_SHOWCOMBOX";
            public const string Search = "PROC_FINANZAS_OPERACIONES_CAJA_BANCOS_DETALLE_CALCULO_SEARCH";
            public const string Insert = "PROC_FINANZAS_OPERACIONES_CAJA_BANCOS_DETALLE_CALCULO_INSERT";
            public const string Update = "PROC_FINANZAS_OPERACIONES_CAJA_BANCOS_DETALLE_CALCULO_UPDATE";
            public const string Delete = "PROC_FINANZAS_OPERACIONES_CAJA_BANCOS_DETALLE_CALCULO_DELETE";
        }
        public static class RendicionCuentas
        {
            public const string Showall = "PROC_FINANZAS_RENDICION_CUENTAS_SHOWALL";
            public const string Showbyid = "PROC_FINANZAS_RENDICION_CUENTAS_SHOWBYID";
            public const string Showcombox = "PROC_FINANZAS_RENDICION_CUENTAS_SHOWCOMBOX";
            public const string Search = "PROC_FINANZAS_RENDICION_CUENTAS_SEARCH";
            public const string Insert = "PROC_FINANZAS_RENDICION_CUENTAS_INSERT";
            public const string Update = "PROC_FINANZAS_RENDICION_CUENTAS_UPDATE";
            public const string Delete = "PROC_FINANZAS_RENDICION_CUENTAS_DELETE";
        }
        public static class RendicionCuentasDetalle
        {
            public const string Showall = "PROC_FINANZAS_RENDICION_CUENTAS_DETALLE_SHOWALL";
            public const string Showbyid = "PROC_FINANZAS_RENDICION_CUENTAS_DETALLE_SHOWBYID";
            public const string Showcombox = "PROC_FINANZAS_RENDICION_CUENTAS_DETALLE_SHOWCOMBOX";
            public const string Search = "PROC_FINANZAS_RENDICION_CUENTAS_DETALLE_SEARCH";
            public const string Insert = "PROC_FINANZAS_RENDICION_CUENTAS_DETALLE_INSERT";
            public const string Update = "PROC_FINANZAS_RENDICION_CUENTAS_DETALLE_UPDATE";
            public const string Delete = "PROC_FINANZAS_RENDICION_CUENTAS_DETALLE_DELETE";
        }
        public static class TiposConceptosFinancieros
        {
            public const string Showall = "PROC_FINANZAS_TIPOS_CONCEPTOS_FINANCIEROS_SHOWALL";
            public const string Showbyid = "PROC_FINANZAS_TIPOS_CONCEPTOS_FINANCIEROS_SHOWBYID";
            public const string Showcombox = "PROC_FINANZAS_TIPOS_CONCEPTOS_FINANCIEROS_SHOWCOMBOX";
            public const string Search = "PROC_FINANZAS_TIPOS_CONCEPTOS_FINANCIEROS_SEARCH";
            public const string Insert = "PROC_FINANZAS_TIPOS_CONCEPTOS_FINANCIEROS_INSERT";
            public const string Update = "PROC_FINANZAS_TIPOS_CONCEPTOS_FINANCIEROS_UPDATE";
            public const string Delete = "PROC_FINANZAS_TIPOS_CONCEPTOS_FINANCIEROS_DELETE";
        }
        public static class TiposCuentasBancarias
        {
            public const string Showall = "PROC_FINANZAS_TIPOS_CUENTAS_BANCARIAS_SHOWALL";
            public const string Showbyid = "PROC_FINANZAS_TIPOS_CUENTAS_BANCARIAS_SHOWBYID";
            public const string Showcombox = "PROC_FINANZAS_TIPOS_CUENTAS_BANCARIAS_SHOWCOMBOX";
            public const string Search = "PROC_FINANZAS_TIPOS_CUENTAS_BANCARIAS_SEARCH";
            public const string Insert = "PROC_FINANZAS_TIPOS_CUENTAS_BANCARIAS_INSERT";
            public const string Update = "PROC_FINANZAS_TIPOS_CUENTAS_BANCARIAS_UPDATE";
            public const string Delete = "PROC_FINANZAS_TIPOS_CUENTAS_BANCARIAS_DELETE";
        }
        /// <summary>Registro de Compras (script 39): comprobantes de pago de proveedores validados contra sus órdenes.</summary>
        public static class RegistroCompras
        {
            public const string Search = "PROC_FINANZAS_REGISTRO_COMPRAS_SEARCH";
            public const string Showbyid = "PROC_FINANZAS_REGISTRO_COMPRAS_SHOWBYID";
            public const string TiposOperacion = "PROC_FINANZAS_REGISTRO_COMPRAS_TIPOS_OPERACION_SHOWCOMBOX";
            public const string Loadcombos = "PROC_FINANZAS_REGISTRO_COMPRAS_LOADCOMBOS";
            public const string DetalleSearch = "PROC_FINANZAS_REGISTRO_COMPRAS_DETALLE_SEARCH";
            public const string PedidosSearch = "PROC_FINANZAS_REGISTRO_COMPRAS_PEDIDOS_SEARCH";
            public const string PedidosDisponibles = "PROC_FINANZAS_REGISTRO_COMPRAS_PEDIDOS_DISPONIBLES";
            public const string PedidosDetalleDisponibles = "PROC_FINANZAS_REGISTRO_COMPRAS_PEDIDOS_DETALLE_DISPONIBLES";
            public const string PedidosAsociar = "PROC_FINANZAS_REGISTRO_COMPRAS_PEDIDOS_ASOCIAR";
            public const string PedidosDesasociar = "PROC_FINANZAS_REGISTRO_COMPRAS_PEDIDOS_DESASOCIAR";
            // Notas de credito/debito por item (script 85)
            public const string IncidenciasOrigenes = "PROC_FINANZAS_REGISTRO_COMPRAS_INCIDENCIAS_ORIGENES";
            public const string IncidenciasDisponibles = "PROC_FINANZAS_REGISTRO_COMPRAS_INCIDENCIAS_DISPONIBLES";
            public const string IncidenciasShowall = "PROC_FINANZAS_REGISTRO_COMPRAS_INCIDENCIAS_SHOWALL";
            public const string IncidenciasGuardar = "PROC_FINANZAS_REGISTRO_COMPRAS_INCIDENCIAS_GUARDAR";
            public const string IncidenciasAnulacionTotal = "PROC_FINANZAS_REGISTRO_COMPRAS_INCIDENCIAS_ANULACION_TOTAL";
            public const string IncidenciasAdicionar = "PROC_FINANZAS_REGISTRO_COMPRAS_INCIDENCIAS_ADICIONAR";
            public const string IncidenciasGuiasDisponibles = "PROC_FINANZAS_REGISTRO_COMPRAS_INCIDENCIAS_GUIAS_DISPONIBLES";
            public const string IncidenciasGuias = "PROC_FINANZAS_REGISTRO_COMPRAS_INCIDENCIAS_GUIAS";
            public const string IncidenciasGuiaAsociar = "PROC_FINANZAS_REGISTRO_COMPRAS_INCIDENCIAS_GUIA_ASOCIAR";
            public const string IncidenciasLineaGuia = "PROC_FINANZAS_REGISTRO_COMPRAS_INCIDENCIAS_LINEA_GUIA";
            public const string IncidenciasGuiaQuitar = "PROC_FINANZAS_REGISTRO_COMPRAS_INCIDENCIAS_GUIA_QUITAR";
            // Anticipos: facturas de anticipo (FLG_ANTICIPO) que se aplican por su monto total a la factura.
            public const string AnticiposDisponibles = "PROC_FINANZAS_REGISTRO_COMPRAS_ANTICIPOS_DISPONIBLES";
            public const string Anticipos            = "PROC_FINANZAS_REGISTRO_COMPRAS_ANTICIPOS";
            public const string AnticipoAsociar      = "PROC_FINANZAS_REGISTRO_COMPRAS_ANTICIPO_ASOCIAR";
            public const string AnticipoQuitar       = "PROC_FINANZAS_REGISTRO_COMPRAS_ANTICIPO_QUITAR";
            public const string Autovincular = "PROC_FINANZAS_REGISTRO_COMPRAS_AUTOVINCULAR";
            public const string ConciliacionSearch = "PROC_FINANZAS_REGISTRO_COMPRAS_CONCILIACION_SEARCH";
            public const string Validar = "PROC_FINANZAS_REGISTRO_COMPRAS_VALIDAR";
            public const string EnviarAprobacion = "PROC_FINANZAS_REGISTRO_COMPRAS_ENVIAR_APROBACION";
            public const string Aprobar = "PROC_FINANZAS_REGISTRO_COMPRAS_APROBAR";
            public const string Contabilizar = "PROC_FINANZAS_REGISTRO_COMPRAS_CONTABILIZAR";
            public const string Anular = "PROC_FINANZAS_REGISTRO_COMPRAS_ANULAR";
            public const string XmlShowbyid = "PROC_FINANZAS_REGISTRO_COMPRAS_XML_SHOWBYID";
            public const string ImportarXml = "PROC_FINANZAS_REGISTRO_COMPRAS_IMPORTAR_XML";
        }
    }
    public static class SCHEMAIngenieria
    {
        public static class ClasesDesarrollo
        {
            public const string Showall = "PROC_INGENIERIA_CLASES_DESARROLLO_SHOWALL";
            public const string Showbyid = "PROC_INGENIERIA_CLASES_DESARROLLO_SHOWBYID";
            public const string Showcombox = "PROC_INGENIERIA_CLASES_DESARROLLO_SHOWCOMBOX";
            public const string Insert = "PROC_INGENIERIA_CLASES_DESARROLLO_INSERT";
            public const string Delete = "PROC_INGENIERIA_CLASES_DESARROLLO_DELETE";
        }
        public static class TiposDesarrollo
        {
            public const string Showall = "PROC_INGENIERIA_TIPOS_DESARROLLO_SHOWALL";
            public const string Showbyid = "PROC_INGENIERIA_TIPOS_DESARROLLO_SHOWBYID";
            public const string Showcombox = "PROC_INGENIERIA_TIPOS_DESARROLLO_SHOWCOMBOX";
            public const string Search = "PROC_INGENIERIA_TIPOS_DESARROLLO_SEARCH";
            public const string Insert = "PROC_INGENIERIA_TIPOS_DESARROLLO_INSERT";
            public const string Delete = "PROC_INGENIERIA_TIPOS_DESARROLLO_DELETE";
        }
        public static class ClasesElementoFormula
        {
            public const string Showall = "PROC_INGENIERIA_CLASES_ELEMENTO_FORMULA_SHOWALL";
            public const string Showbyid = "PROC_INGENIERIA_CLASES_ELEMENTO_FORMULA_SHOWBYID";
            public const string Showcombox = "PROC_INGENIERIA_CLASES_ELEMENTO_FORMULA_SHOWCOMBOX";
            public const string Search = "PROC_INGENIERIA_CLASES_ELEMENTO_FORMULA_SEARCH";
            public const string Insert = "PROC_INGENIERIA_CLASES_ELEMENTO_FORMULA_INSERT";
            public const string Update = "PROC_INGENIERIA_CLASES_ELEMENTO_FORMULA_UPDATE";
            public const string Delete = "PROC_INGENIERIA_CLASES_ELEMENTO_FORMULA_DELETE";
        }
        public static class ComponentesProducto
        {
            public const string Showall = "PROC_INGENIERIA_COMPONENTES_PRODUCTO_SHOWALL";
            public const string Showbyid = "PROC_INGENIERIA_COMPONENTES_PRODUCTO_SHOWBYID";
            public const string Showcombox = "PROC_INGENIERIA_COMPONENTES_PRODUCTO_SHOWCOMBOX";
            public const string Search = "PROC_INGENIERIA_COMPONENTES_PRODUCTO_SEARCH";
            public const string Insert = "PROC_INGENIERIA_COMPONENTES_PRODUCTO_INSERT";
            public const string Update = "PROC_INGENIERIA_COMPONENTES_PRODUCTO_UPDATE";
            public const string Delete = "PROC_INGENIERIA_COMPONENTES_PRODUCTO_DELETE";
        }
        public static class Disenos
        {
            public const string Showall = "PROC_INGENIERIA_DISENOS_SHOWALL";
            public const string Showbyid = "PROC_INGENIERIA_DISENOS_SHOWBYID";
            public const string Showcombox = "PROC_INGENIERIA_DISENOS_SHOWCOMBOX";
            public const string Search = "PROC_INGENIERIA_DISENOS_SEARCH";
            public const string Insert = "PROC_INGENIERIA_DISENOS_INSERT";
            public const string Delete = "PROC_INGENIERIA_DISENOS_DELETE";
            public const string Loadcombos = "PROC_INGENIERIA_DISENOS_LOADCOMBOS";
        }
        public static class DisenosMemorias
        {
            public const string Showall = "PROC_INGENIERIA_DISENOS_MEMORIAS_SHOWALL";
            public const string Showbyid = "PROC_INGENIERIA_DISENOS_MEMORIAS_SHOWBYID";
            public const string Showcombox = "PROC_INGENIERIA_DISENOS_MEMORIAS_SHOWCOMBOX";
            public const string Search = "PROC_INGENIERIA_DISENOS_MEMORIAS_SEARCH";
            public const string Insert = "PROC_INGENIERIA_DISENOS_MEMORIAS_INSERT";
            public const string Delete = "PROC_INGENIERIA_DISENOS_MEMORIAS_DELETE";
        }
        public static class DisenosVersionesMemorias
        {
            public const string Showall = "PROC_INGENIERIA_DISENOS_VERSIONES_MEMORIAS_SHOWALL";
            public const string Showbyid = "PROC_INGENIERIA_DISENOS_VERSIONES_MEMORIAS_SHOWBYID";
            public const string Showcombox = "PROC_INGENIERIA_DISENOS_VERSIONES_MEMORIAS_SHOWCOMBOX";
            public const string Search = "PROC_INGENIERIA_DISENOS_VERSIONES_MEMORIAS_SEARCH";
            public const string Insert = "PROC_INGENIERIA_DISENOS_VERSIONES_MEMORIAS_INSERT";
            public const string Delete = "PROC_INGENIERIA_DISENOS_VERSIONES_MEMORIAS_DELETE";
        }
        public static class DisenosProducto
        {
            public const string Showall = "PROC_INGENIERIA_DISENOS_PRODUCTO_SHOWALL";
            public const string Showbyid = "PROC_INGENIERIA_DISENOS_PRODUCTO_SHOWBYID";
            public const string Showcombox = "PROC_INGENIERIA_DISENOS_PRODUCTO_SHOWCOMBOX";
            public const string Search = "PROC_INGENIERIA_DISENOS_PRODUCTO_SEARCH";
            public const string Insert = "PROC_INGENIERIA_DISENOS_PRODUCTO_INSERT";
            public const string Update = "PROC_INGENIERIA_DISENOS_PRODUCTO_UPDATE";
            public const string Delete = "PROC_INGENIERIA_DISENOS_PRODUCTO_DELETE";
        }
        public static class DisenosSectores
        {
            public const string Showall = "PROC_INGENIERIA_DISENOS_SECTORES_SHOWALL";
            public const string Showbyid = "PROC_INGENIERIA_DISENOS_SECTORES_SHOWBYID";
            public const string Showcombox = "PROC_INGENIERIA_DISENOS_SECTORES_SHOWCOMBOX";
            public const string Search = "PROC_INGENIERIA_DISENOS_SECTORES_SEARCH";
            public const string Insert = "PROC_INGENIERIA_DISENOS_SECTORES_INSERT";
            public const string Update = "PROC_INGENIERIA_DISENOS_SECTORES_UPDATE";
            public const string Delete = "PROC_INGENIERIA_DISENOS_SECTORES_DELETE";
        }
        public static class DisenosVersion
        {
            public const string Showall = "PROC_INGENIERIA_DISENOS_VERSION_SHOWALL";
            public const string Showbyid = "PROC_INGENIERIA_DISENOS_VERSION_SHOWBYID";
            public const string Showcombox = "PROC_INGENIERIA_DISENOS_VERSION_SHOWCOMBOX";
            public const string Search = "PROC_INGENIERIA_DISENOS_VERSION_SEARCH";
            public const string Insert = "PROC_INGENIERIA_DISENOS_VERSION_INSERT";
            public const string Delete = "PROC_INGENIERIA_DISENOS_VERSION_DELETE";
            // Nombre real del SP tiene "VERSIONES" (plural) aunque la tabla/clase es
            // singular DISENOS_VERSION — se respeta tal cual está creado en la BD.
            public const string Loadcombos = "PROC_INGENIERIA_DISENOS_VERSIONES_LOADCOMBOS";
        }
        public static class DisenosVersionEspecificacionesTecnicas
        {
            public const string Showall = "PROC_INGENIERIA_DISENOS_VERSION_ESPECIFICACIONES_TECNICAS_SHOWALL";
            public const string Showbyid = "PROC_INGENIERIA_DISENOS_VERSION_ESPECIFICACIONES_TECNICAS_SHOWBYID";
            public const string Showcombox = "PROC_INGENIERIA_DISENOS_VERSION_ESPECIFICACIONES_TECNICAS_SHOWCOMBOX";
            public const string Search = "PROC_INGENIERIA_DISENOS_VERSION_ESPECIFICACIONES_TECNICAS_SEARCH";
            public const string Insert = "PROC_INGENIERIA_DISENOS_VERSION_ESPECIFICACIONES_TECNICAS_INSERT";
            public const string Update = "PROC_INGENIERIA_DISENOS_VERSION_ESPECIFICACIONES_TECNICAS_UPDATE";
            public const string Delete = "PROC_INGENIERIA_DISENOS_VERSION_ESPECIFICACIONES_TECNICAS_DELETE";
        }
        public static class DisenosVersionSectorComponente
        {
            public const string Showall = "PROC_INGENIERIA_DISENOS_VERSION_SECTOR_COMPONENTE_SHOWALL";
            public const string Showbyid = "PROC_INGENIERIA_DISENOS_VERSION_SECTOR_COMPONENTE_SHOWBYID";
            public const string Showcombox = "PROC_INGENIERIA_DISENOS_VERSION_SECTOR_COMPONENTE_SHOWCOMBOX";
            public const string Search = "PROC_INGENIERIA_DISENOS_VERSION_SECTOR_COMPONENTE_SEARCH";
            public const string Insert = "PROC_INGENIERIA_DISENOS_VERSION_SECTOR_COMPONENTE_INSERT";
            public const string Update = "PROC_INGENIERIA_DISENOS_VERSION_SECTOR_COMPONENTE_UPDATE";
            public const string Delete = "PROC_INGENIERIA_DISENOS_VERSION_SECTOR_COMPONENTE_DELETE";
        }
        public static class DisenosVersionSectorComponenteProducto
        {
            public const string Showall = "PROC_INGENIERIA_DISENOS_VERSION_SECTOR_COMPONENTE_PRODUCTO_SHOWALL";
            public const string Showbyid = "PROC_INGENIERIA_DISENOS_VERSION_SECTOR_COMPONENTE_PRODUCTO_SHOWBYID";
            public const string Showcombox = "PROC_INGENIERIA_DISENOS_VERSION_SECTOR_COMPONENTE_PRODUCTO_SHOWCOMBOX";
            public const string Search = "PROC_INGENIERIA_DISENOS_VERSION_SECTOR_COMPONENTE_PRODUCTO_SEARCH";
            public const string Insert = "PROC_INGENIERIA_DISENOS_VERSION_SECTOR_COMPONENTE_PRODUCTO_INSERT";
            public const string Update = "PROC_INGENIERIA_DISENOS_VERSION_SECTOR_COMPONENTE_PRODUCTO_UPDATE";
            public const string Delete = "PROC_INGENIERIA_DISENOS_VERSION_SECTOR_COMPONENTE_PRODUCTO_DELETE";
        }
        public static class DisenosVersionSectorComponenteProductoFormula
        {
            public const string Showall = "PROC_INGENIERIA_DISENOS_VERSION_SECTOR_COMPONENTE_PRODUCTO_FORMULA_SHOWALL";
            public const string Showbyid = "PROC_INGENIERIA_DISENOS_VERSION_SECTOR_COMPONENTE_PRODUCTO_FORMULA_SHOWBYID";
            public const string Showcombox = "PROC_INGENIERIA_DISENOS_VERSION_SECTOR_COMPONENTE_PRODUCTO_FORMULA_SHOWCOMBOX";
            public const string Search = "PROC_INGENIERIA_DISENOS_VERSION_SECTOR_COMPONENTE_PRODUCTO_FORMULA_SEARCH";
            public const string Insert = "PROC_INGENIERIA_DISENOS_VERSION_SECTOR_COMPONENTE_PRODUCTO_FORMULA_INSERT";
            public const string Update = "PROC_INGENIERIA_DISENOS_VERSION_SECTOR_COMPONENTE_PRODUCTO_FORMULA_UPDATE";
            public const string Delete = "PROC_INGENIERIA_DISENOS_VERSION_SECTOR_COMPONENTE_PRODUCTO_FORMULA_DELETE";
        }
        public static class EspecificacionesTecnicas
        {
            public const string Showall = "PROC_INGENIERIA_ESPECIFICACIONES_TECNICAS_SHOWALL";
            public const string Showbyid = "PROC_INGENIERIA_ESPECIFICACIONES_TECNICAS_SHOWBYID";
            public const string Showcombox = "PROC_INGENIERIA_ESPECIFICACIONES_TECNICAS_SHOWCOMBOX";
            public const string Search = "PROC_INGENIERIA_ESPECIFICACIONES_TECNICAS_SEARCH";
            public const string Insert = "PROC_INGENIERIA_ESPECIFICACIONES_TECNICAS_INSERT";
            public const string Delete = "PROC_INGENIERIA_ESPECIFICACIONES_TECNICAS_DELETE";
            public const string Loadcombos = "PROC_INGENIERIA_ESPECIFICACIONES_TECNICAS_LOADCOMBOS";
            public const string Treelist = "PROC_INGENIERIA_ESPECIFICACIONES_TECNICAS_TREELIST";
        }
        public static class EspecificacionesTecnicasItems
        {
            public const string Showall = "PROC_INGENIERIA_ESPECIFICACIONES_ITEMS_TECNICOS_SHOWALL";
            public const string Showbyid = "PROC_INGENIERIA_ESPECIFICACIONES_ITEMS_TECNICOS_SHOWBYID";
            public const string Showcombox = "PROC_INGENIERIA_ESPECIFICACIONES_ITEMS_TECNICOS_SHOWCOMBOX";
            public const string Search = "PROC_INGENIERIA_ESPECIFICACIONES_ITEMS_TECNICOS_SEARCH";
            public const string Insert = "PROC_INGENIERIA_ESPECIFICACIONES_ITEMS_TECNICOS_INSERT";
            public const string Update = "PROC_INGENIERIA_ESPECIFICACIONES_ITEMS_TECNICOS_UPDATE";
            public const string Delete = "PROC_INGENIERIA_ESPECIFICACIONES_ITEMS_TECNICOS_DELETE";
            public const string TreelistPorEspecificacion = "PROC_INGENIERIA_ESPECIFICACIONES_ITEMS_TECNICOS_TREELIST";
            // Nombre real del SP tal como lo creó Pedro (con los typos "ESPECFICACIONES" y
            // "COMBOXDEPENDEINTES") — no corregir, tiene que calzar exacto con SQL Server.
            public const string ComboxDependientes = "PROC_INGENIERIA_ESPECFICACIONES_ITEMS_TECNICOS_COMBOXDEPENDEINTES";
        }
        public static class EstructuraEspecificacionTecnica
        {
            public const string Showall = "PROC_INGENIERIA_ESTRUCTURA_ESPECIFICACION_TECNICA_SHOWALL";
            public const string Showbyid = "PROC_INGENIERIA_ESTRUCTURA_ESPECIFICACION_TECNICA_SHOWBYID";
            public const string Showcombox = "PROC_INGENIERIA_ESTRUCTURA_ESPECIFICACION_TECNICA_SHOWCOMBOX";
            public const string Search = "PROC_INGENIERIA_ESTRUCTURA_ESPECIFICACION_TECNICA_SEARCH";
            public const string Insert = "PROC_INGENIERIA_ESTRUCTURA_ESPECIFICACION_TECNICA_INSERT";
            public const string Delete = "PROC_INGENIERIA_ESTRUCTURA_ESPECIFICACION_TECNICA_DELETE";
        }
        public static class GruposFamiliasSubFamiliasEspecificacionTecnica
        {
            public const string Showall = "PROC_INGENIERIA_GRUPOS_FAMILIAS_SUB_FAMILIAS_ESPECIFICACION_TECNICA_SHOWALL";
            public const string Showbyid = "PROC_INGENIERIA_GRUPOS_FAMILIAS_SUB_FAMILIAS_ESPECIFICACION_TECNICA_SHOWBYID";
            public const string Showcombox = "PROC_INGENIERIA_GRUPOS_FAMILIAS_SUB_FAMILIAS_ESPECIFICACION_TECNICA_SHOWCOMBOX";
            public const string Search = "PROC_INGENIERIA_GRUPOS_FAMILIAS_SUB_FAMILIAS_ESPECIFICACION_TECNICA_SEARCH";
            public const string Insert = "PROC_INGENIERIA_GRUPOS_FAMILIAS_SUB_FAMILIAS_ESPECIFICACION_TECNICA_INSERT";
            public const string Delete = "PROC_INGENIERIA_GRUPOS_FAMILIAS_SUB_FAMILIAS_ESPECIFICACION_TECNICA_DELETE";
            public const string Treelist = "PROC_INGENIERIA_GRUPOS_FAMILIAS_SUB_FAMILIAS_ESPECIFICACION_TECNICA_TREELIST";
            // Combos bundleados de GruposEspecificacionTecnicaDialog.razor: Tipos de Registro
            // de Datos + Grupos de Especificación Técnica + Estado en una sola llamada.
            public const string Loadcombos = "PROC_INGENIERIA_GRUPOS_FAMILIAS_SUB_FAMILIAS_ESPECIFICACION_TECNICA_LOADCOMBOS";
        }
        public static class GruposFamiliasSubFamiliasGruposEspecificacionTecnica
        {
            public const string Showall = "PROC_INGENIERIA_GRUPOS_FAMILIAS_SUB_FAMILIAS_GRUPOS_ESPECIFICACION_TECNICA_SHOWALL";
            public const string Showbyid = "PROC_INGENIERIA_GRUPOS_FAMILIAS_SUB_FAMILIAS_GRUPOS_ESPECIFICACION_TECNICA_SHOWBYID";
            public const string Showcombox = "PROC_INGENIERIA_GRUPOS_FAMILIAS_SUB_FAMILIAS_GRUPOS_ESPECIFICACION_TECNICA_SHOWCOMBOX";
            public const string Search = "PROC_INGENIERIA_GRUPOS_FAMILIAS_SUB_FAMILIAS_GRUPOS_ESPECIFICACION_TECNICA_SEARCH";
            public const string Insert = "PROC_INGENIERIA_GRUPOS_FAMILIAS_SUB_FAMILIAS_GRUPOS_ESPECIFICACION_TECNICA_INSERT";
            public const string Delete = "PROC_INGENIERIA_GRUPOS_FAMILIAS_SUB_FAMILIAS_GRUPOS_ESPECIFICACION_TECNICA_DELETE";
        }
        public static class GruposFamiliasSubFamiliasReglasCodificacion
        {
            public const string Showall = "PROC_INGENIERIA_GRUPOS_FAMILIAS_SUB_FAMILIAS_REGLAS_CODIFICACION_SHOWALL";
            public const string Showbyid = "PROC_INGENIERIA_GRUPOS_FAMILIAS_SUB_FAMILIAS_REGLAS_CODIFICACION_SHOWBYID";
            public const string Showcombox = "PROC_INGENIERIA_GRUPOS_FAMILIAS_SUB_FAMILIAS_REGLAS_CODIFICACION_SHOWCOMBOX";
            public const string Search = "PROC_INGENIERIA_GRUPOS_FAMILIAS_SUB_FAMILIAS_REGLAS_CODIFICACION_SEARCH";
            public const string Insert = "PROC_INGENIERIA_GRUPOS_FAMILIAS_SUB_FAMILIAS_REGLAS_CODIFICACION_INSERT";
            public const string Update = "PROC_INGENIERIA_GRUPOS_FAMILIAS_SUB_FAMILIAS_REGLAS_CODIFICACION_UPDATE";
            public const string Delete = "PROC_INGENIERIA_GRUPOS_FAMILIAS_SUB_FAMILIAS_REGLAS_CODIFICACION_DELETE";
        }
        public static class PedidosComercialesDetalleEspecificacionesTecnicas
        {
            public const string Showall = "PROC_INGENIERIA_PEDIDOS_COMERCIALES_DETALLE_ESPECIFICACIONES_TECNICAS_SHOWALL";
            public const string Showbyid = "PROC_INGENIERIA_PEDIDOS_COMERCIALES_DETALLE_ESPECIFICACIONES_TECNICAS_SHOWBYID";
            public const string Showcombox = "PROC_INGENIERIA_PEDIDOS_COMERCIALES_DETALLE_ESPECIFICACIONES_TECNICAS_SHOWCOMBOX";
            public const string Search = "PROC_INGENIERIA_PEDIDOS_COMERCIALES_DETALLE_ESPECIFICACIONES_TECNICAS_SEARCH";
            public const string Insert = "PROC_INGENIERIA_PEDIDOS_COMERCIALES_DETALLE_ESPECIFICACIONES_TECNICAS_INSERT";
            public const string Update = "PROC_INGENIERIA_PEDIDOS_COMERCIALES_DETALLE_ESPECIFICACIONES_TECNICAS_UPDATE";
            public const string Delete = "PROC_INGENIERIA_PEDIDOS_COMERCIALES_DETALLE_ESPECIFICACIONES_TECNICAS_DELETE";
        }
        public static class ProduccionRutas
        {
            public const string Showall = "PROC_INGENIERIA_PRODUCCION_RUTAS_SHOWALL";
            public const string Showbyid = "PROC_INGENIERIA_PRODUCCION_RUTAS_SHOWBYID";
            public const string Showcombox = "PROC_INGENIERIA_PRODUCCION_RUTAS_SHOWCOMBOX";
            public const string Search = "PROC_INGENIERIA_PRODUCCION_RUTAS_SEARCH";
            public const string Insert = "PROC_INGENIERIA_PRODUCCION_RUTAS_INSERT";
            public const string Delete = "PROC_INGENIERIA_PRODUCCION_RUTAS_DELETE";
        }
        public static class ProduccionRutasOperacionesEstructura
        {
            public const string Showall = "PROC_INGENIERIA_PRODUCCION_RUTAS_OPERACIONES_ESTRUCTURA_SHOWALL";
            public const string Showbyid = "PROC_INGENIERIA_PRODUCCION_RUTAS_OPERACIONES_ESTRUCTURA_SHOWBYID";
            public const string Showcombox = "PROC_INGENIERIA_PRODUCCION_RUTAS_OPERACIONES_ESTRUCTURA_SHOWCOMBOX";
            public const string Search = "PROC_INGENIERIA_PRODUCCION_RUTAS_OPERACIONES_ESTRUCTURA_SEARCH";
            public const string Insert = "PROC_INGENIERIA_PRODUCCION_RUTAS_OPERACIONES_ESTRUCTURA_INSERT";
            public const string Update = "PROC_INGENIERIA_PRODUCCION_RUTAS_OPERACIONES_ESTRUCTURA_UPDATE";
            public const string Delete = "PROC_INGENIERIA_PRODUCCION_RUTAS_OPERACIONES_ESTRUCTURA_DELETE";
        }
        public static class TablasEspecificacionesTecnicas
        {
            public const string Showall = "PROC_INGENIERIA_TABLAS_ESPECIFICACIONES_TECNICAS_SHOWALL";
            public const string Showbyid = "PROC_INGENIERIA_TABLAS_ESPECIFICACIONES_TECNICAS_SHOWBYID";
            public const string Showcombox = "PROC_INGENIERIA_TABLAS_ESPECIFICACIONES_TECNICAS_SHOWCOMBOX";
            public const string Search = "PROC_INGENIERIA_TABLAS_ESPECIFICACIONES_TECNICAS_SEARCH";
            public const string Insert = "PROC_INGENIERIA_TABLAS_ESPECIFICACIONES_TECNICAS_INSERT";
            public const string Update = "PROC_INGENIERIA_TABLAS_ESPECIFICACIONES_TECNICAS_UPDATE";
            public const string Delete = "PROC_INGENIERIA_TABLAS_ESPECIFICACIONES_TECNICAS_DELETE";
        }
        public static class TiposDiseno
        {
            public const string Showall = "PROC_INGENIERIA_TIPOS_DISENO_SHOWALL";
            public const string Showbyid = "PROC_INGENIERIA_TIPOS_DISENO_SHOWBYID";
            public const string Showcombox = "PROC_INGENIERIA_TIPOS_DISENO_SHOWCOMBOX";
            public const string Search = "PROC_INGENIERIA_TIPOS_DISENO_SEARCH";
            public const string Insert = "PROC_INGENIERIA_TIPOS_DISENO_INSERT";
            public const string Delete = "PROC_INGENIERIA_TIPOS_DISENO_DELETE";
        }
        public static class TiposElementoFormula
        {
            public const string Showall = "PROC_INGENIERIA_TIPOS_ELEMENTO_FORMULA_SHOWALL";
            public const string Showbyid = "PROC_INGENIERIA_TIPOS_ELEMENTO_FORMULA_SHOWBYID";
            public const string Showcombox = "PROC_INGENIERIA_TIPOS_ELEMENTO_FORMULA_SHOWCOMBOX";
            public const string Search = "PROC_INGENIERIA_TIPOS_ELEMENTO_FORMULA_SEARCH";
            public const string Insert = "PROC_INGENIERIA_TIPOS_ELEMENTO_FORMULA_INSERT";
            public const string Update = "PROC_INGENIERIA_TIPOS_ELEMENTO_FORMULA_UPDATE";
            public const string Delete = "PROC_INGENIERIA_TIPOS_ELEMENTO_FORMULA_DELETE";
        }
        public static class TiposEspecificacionTecnica
        {
            public const string Showall = "PROC_INGENIERIA_TIPOS_ESPECIFICACION_TECNICA_SHOWALL";
            public const string Showbyid = "PROC_INGENIERIA_TIPOS_ESPECIFICACION_TECNICA_SHOWBYID";
            public const string Showcombox = "PROC_INGENIERIA_TIPOS_ESPECIFICACION_TECNICA_SHOWCOMBOX";
            public const string Search = "PROC_INGENIERIA_TIPOS_ESPECIFICACION_TECNICA_SEARCH";
            public const string Insert = "PROC_INGENIERIA_TIPOS_ESPECIFICACION_TECNICA_INSERT";
            public const string Update = "PROC_INGENIERIA_TIPOS_ESPECIFICACION_TECNICA_UPDATE";
            public const string Delete = "PROC_INGENIERIA_TIPOS_ESPECIFICACION_TECNICA_DELETE";
        }
        public static class TiposRegistroDatos
        {
            public const string Showall = "PROC_INGENIERIA_TIPOS_REGISTRO_DATOS_SHOWALL";
            public const string Showbyid = "PROC_INGENIERIA_TIPOS_REGISTRO_DATOS_SHOWBYID";
            public const string Showcombox = "PROC_INGENIERIA_TIPOS_REGISTRO_DATOS_SHOWCOMBOX";
            public const string Search = "PROC_INGENIERIA_TIPOS_REGISTRO_DATOS_SEARCH";
            public const string Insert = "PROC_INGENIERIA_TIPOS_REGISTRO_DATOS_INSERT";
            public const string Delete = "PROC_INGENIERIA_TIPOS_REGISTRO_DATOS_DELETE";
        }
        public static class TiposMerma
        {
            public const string Showall = "PROC_INGENIERIA_TIPOS_MERMA_SHOWALL";
            public const string Showbyid = "PROC_INGENIERIA_TIPOS_MERMA_SHOWBYID";
            public const string Showcombox = "PROC_INGENIERIA_TIPOS_MERMA_SHOWCOMBOX";
            public const string Search = "PROC_INGENIERIA_TIPOS_MERMA_SEARCH";
            public const string Insert = "PROC_INGENIERIA_TIPOS_MERMA_INSERT";
            public const string Update = "PROC_INGENIERIA_TIPOS_MERMA_UPDATE";
            public const string Delete = "PROC_INGENIERIA_TIPOS_MERMA_DELETE";
        }
        public static class TiposRutaProduccion
        {
            public const string Showall = "PROC_INGENIERIA_TIPOS_RUTA_PRODUCCION_SHOWALL";
            public const string Showbyid = "PROC_INGENIERIA_TIPOS_RUTA_PRODUCCION_SHOWBYID";
            public const string Showcombox = "PROC_INGENIERIA_TIPOS_RUTA_PRODUCCION_SHOWCOMBOX";
            public const string Search = "PROC_INGENIERIA_TIPOS_RUTA_PRODUCCION_SEARCH";
            public const string Insert = "PROC_INGENIERIA_TIPOS_RUTA_PRODUCCION_INSERT";
            public const string Update = "PROC_INGENIERIA_TIPOS_RUTA_PRODUCCION_UPDATE";
            public const string Delete = "PROC_INGENIERIA_TIPOS_RUTA_PRODUCCION_DELETE";
        }
    }
    public static class SCHEMALogistica
    {
        public static class InventariosAlmacenesUbicaciones
        {
            public const string Showall = "PROC_LOGISTICA_INVENTARIOS_ALMACENES_UBICACIONES_SHOWALL";
            public const string Showbyid = "PROC_LOGISTICA_INVENTARIOS_ALMACENES_UBICACIONES_SHOWBYID";
            public const string Showcombox = "PROC_LOGISTICA_INVENTARIOS_ALMACENES_UBICACIONES_SHOWCOMBOX";
            public const string Search = "PROC_LOGISTICA_INVENTARIOS_ALMACENES_UBICACIONES_SEARCH";
            public const string Insert = "PROC_LOGISTICA_INVENTARIOS_ALMACENES_UBICACIONES_INSERT";
            // Sin Update: el SP _INSERT hace upsert internamente (tabla de asignación pura).
            public const string Delete = "PROC_LOGISTICA_INVENTARIOS_ALMACENES_UBICACIONES_DELETE";
        }
        public static class InventariosAlmacenesImagen
        {
            public const string Showall = "PROC_LOGISTICA_INVENTARIOS_ALMACENES_IMAGEN_SHOWALL";
            public const string Showbyid = "PROC_LOGISTICA_INVENTARIOS_ALMACENES_IMAGEN_SHOWBYID";
            public const string Showcombox = "PROC_LOGISTICA_INVENTARIOS_ALMACENES_IMAGEN_SHOWCOMBOX";
            public const string Search = "PROC_LOGISTICA_INVENTARIOS_ALMACENES_IMAGEN_SEARCH";
            public const string Insert = "PROC_LOGISTICA_INVENTARIOS_ALMACENES_IMAGEN_INSERT";
            public const string Update = "PROC_LOGISTICA_INVENTARIOS_ALMACENES_IMAGEN_UPDATE";
            public const string Delete = "PROC_LOGISTICA_INVENTARIOS_ALMACENES_IMAGEN_DELETE";
        }
        public static class Almacenes
        {
            public const string Showall = "PROC_LOGISTICA_ALMACENES_SHOWALL";
            public const string Showbyid = "PROC_LOGISTICA_ALMACENES_SHOWBYID";
            public const string Showcombox = "PROC_LOGISTICA_ALMACENES_SHOWCOMBOX";
            public const string Search = "PROC_LOGISTICA_ALMACENES_SEARCH";
            public const string Insert = "PROC_LOGISTICA_ALMACENES_INSERT";
            public const string Delete = "PROC_LOGISTICA_ALMACENES_DELETE";
            // TiposAlmacen + Sucursales + Procesos en un solo viaje, para AlmacenesDialog.razor.
            public const string Loadcombos = "PROC_LOGISTICA_ALMACENES_LOADCOMBOS";
        }
        public static class AlmacenesTiposMovimiento
        {
            public const string Showall = "PROC_LOGISTICA_ALMACENES_TIPOS_MOVIMIENTO_SHOWALL";
            public const string Showbyid = "PROC_LOGISTICA_ALMACENES_TIPOS_MOVIMIENTO_SHOWBYID";
            public const string Showcombox = "PROC_LOGISTICA_ALMACENES_TIPOS_MOVIMIENTO_SHOWCOMBOX";
            public const string Search = "PROC_LOGISTICA_ALMACENES_TIPOS_MOVIMIENTO_SEARCH";
            public const string Insert = "PROC_LOGISTICA_ALMACENES_TIPOS_MOVIMIENTO_INSERT";
            public const string Delete = "PROC_LOGISTICA_ALMACENES_TIPOS_MOVIMIENTO_DELETE";
        }
        public static class AlmacenesUbicacionesAlmacen
        {
            public const string Showall = "PROC_LOGISTICA_ALMACENES_UBICACIONES_ALMACEN_SHOWALL";
            public const string Showbyid = "PROC_LOGISTICA_ALMACENES_UBICACIONES_ALMACEN_SHOWBYID";
            public const string Showcombox = "PROC_LOGISTICA_ALMACENES_UBICACIONES_ALMACEN_SHOWCOMBOX";
            public const string Search = "PROC_LOGISTICA_ALMACENES_UBICACIONES_ALMACEN_SEARCH";
            public const string Insert = "PROC_LOGISTICA_ALMACENES_UBICACIONES_ALMACEN_INSERT";
            public const string Delete = "PROC_LOGISTICA_ALMACENES_UBICACIONES_ALMACEN_DELETE";
        }
        public static class ClasesMovimiento
        {
            public const string Showall = "PROC_LOGISTICA_CLASES_MOVIMIENTO_SHOWALL";
            public const string Showbyid = "PROC_LOGISTICA_CLASES_MOVIMIENTO_SHOWBYID";
            public const string Showcombox = "PROC_LOGISTICA_CLASES_MOVIMIENTO_SHOWCOMBOX";
            public const string Search = "PROC_LOGISTICA_CLASES_MOVIMIENTO_SEARCH";
            public const string Insert = "PROC_LOGISTICA_CLASES_MOVIMIENTO_INSERT";
            public const string Update = "PROC_LOGISTICA_CLASES_MOVIMIENTO_UPDATE";
            public const string Delete = "PROC_LOGISTICA_CLASES_MOVIMIENTO_DELETE";
        }
        public static class Despachos
        {
            public const string Showall = "PROC_LOGISTICA_DESPACHOS_SHOWALL";
            public const string Showbyid = "PROC_LOGISTICA_DESPACHOS_SHOWBYID";
            public const string Showcombox = "PROC_LOGISTICA_DESPACHOS_SHOWCOMBOX";
            public const string Search = "PROC_LOGISTICA_DESPACHOS_SEARCH";
            public const string Insert = "PROC_LOGISTICA_DESPACHOS_INSERT";
            public const string Update = "PROC_LOGISTICA_DESPACHOS_UPDATE";
            public const string Delete = "PROC_LOGISTICA_DESPACHOS_DELETE";
        }
        public static class DespachosRuta
        {
            public const string Showall = "PROC_LOGISTICA_DESPACHOS_RUTA_SHOWALL";
            public const string Showbyid = "PROC_LOGISTICA_DESPACHOS_RUTA_SHOWBYID";
            public const string Showcombox = "PROC_LOGISTICA_DESPACHOS_RUTA_SHOWCOMBOX";
            public const string Search = "PROC_LOGISTICA_DESPACHOS_RUTA_SEARCH";
            public const string Insert = "PROC_LOGISTICA_DESPACHOS_RUTA_INSERT";
            public const string Update = "PROC_LOGISTICA_DESPACHOS_RUTA_UPDATE";
            public const string Delete = "PROC_LOGISTICA_DESPACHOS_RUTA_DELETE";
        }
        public static class Embalado
        {
            public const string Showall = "PROC_LOGISTICA_EMBALADO_SHOWALL";
            public const string Showbyid = "PROC_LOGISTICA_EMBALADO_SHOWBYID";
            public const string Showcombox = "PROC_LOGISTICA_EMBALADO_SHOWCOMBOX";
            public const string Search = "PROC_LOGISTICA_EMBALADO_SEARCH";
            public const string Insert = "PROC_LOGISTICA_EMBALADO_INSERT";
            public const string Update = "PROC_LOGISTICA_EMBALADO_UPDATE";
            public const string Delete = "PROC_LOGISTICA_EMBALADO_DELETE";
        }
        public static class EmbaladoEmbalajes
        {
            public const string Showall = "PROC_LOGISTICA_EMBALADO_EMBALAJES_SHOWALL";
            public const string Showbyid = "PROC_LOGISTICA_EMBALADO_EMBALAJES_SHOWBYID";
            public const string Showcombox = "PROC_LOGISTICA_EMBALADO_EMBALAJES_SHOWCOMBOX";
            public const string Search = "PROC_LOGISTICA_EMBALADO_EMBALAJES_SEARCH";
            public const string Insert = "PROC_LOGISTICA_EMBALADO_EMBALAJES_INSERT";
            public const string Update = "PROC_LOGISTICA_EMBALADO_EMBALAJES_UPDATE";
            public const string Delete = "PROC_LOGISTICA_EMBALADO_EMBALAJES_DELETE";
        }
        public static class EmbaladoEmbalajeDetalle
        {
            public const string Showall = "PROC_LOGISTICA_EMBALADO_EMBALAJE_DETALLE_SHOWALL";
            public const string Showbyid = "PROC_LOGISTICA_EMBALADO_EMBALAJE_DETALLE_SHOWBYID";
            public const string Showcombox = "PROC_LOGISTICA_EMBALADO_EMBALAJE_DETALLE_SHOWCOMBOX";
            public const string Search = "PROC_LOGISTICA_EMBALADO_EMBALAJE_DETALLE_SEARCH";
            public const string Insert = "PROC_LOGISTICA_EMBALADO_EMBALAJE_DETALLE_INSERT";
            public const string Update = "PROC_LOGISTICA_EMBALADO_EMBALAJE_DETALLE_UPDATE";
            public const string Delete = "PROC_LOGISTICA_EMBALADO_EMBALAJE_DETALLE_DELETE";
        }
        public static class Inventarios
        {
            public const string Showall = "PROC_LOGISTICA_INVENTARIOS_SHOWALL";
            public const string Showbyid = "PROC_LOGISTICA_INVENTARIOS_SHOWBYID";
            public const string Showcombox = "PROC_LOGISTICA_INVENTARIOS_SHOWCOMBOX";
            public const string Search = "PROC_LOGISTICA_INVENTARIOS_SEARCH";
            public const string Insert = "PROC_LOGISTICA_INVENTARIOS_INSERT";
            public const string Update = "PROC_LOGISTICA_INVENTARIOS_UPDATE";
            public const string Delete = "PROC_LOGISTICA_INVENTARIOS_DELETE";

            // ── Procesos de inventario (botones 04 a 08 de la opción) ───────────
            // Los cuatro que existen reciben el mismo trío de parámetros, pero OJO con los
            // nombres: MAKE y REVIERTE los declaran como @COD_USUARIO/@COD_ESTACION, y
            // CERRAR y ABRIR como @COD_USUARIO_REGISTRO/@COD_ESTACION_REGISTRO. Dapper los
            // manda por nombre exacto, así que no son intercambiables.
            public const string RequerimientoMake     = "PROC_LOGISTICA_INVENTARIOS_REQUERIMIENTOMAKE";
            public const string RequerimientoRevierte = "PROC_LOGISTICA_INVENTARIOS_REQUERIMIENTOREVIERTE";
            public const string Cerrar                = "PROC_LOGISTICA_INVENTARIOS_CERRAR";
            public const string Abrir                 = "PROC_LOGISTICA_INVENTARIOS_ABRIR";
            /// <summary>ATENCIÓN: este SP TODAVÍA NO EXISTE en la base. La constante queda
            /// declarada para que el botón 06 no requiera tocar código cuando lo crees;
            /// asume el mismo trío de parámetros que MAKE/REVIERTE, por analogía con
            /// PROC_LOGISTICA_INVENTARIOS_ALMACENES_IMAGEN.</summary>
            public const string Imagen                = "PROC_LOGISTICA_INVENTARIOS_IMAGEN";
        }
        public static class InventariosAlmacenes
        {
            public const string Showall = "PROC_LOGISTICA_INVENTARIOS_ALMACENES_SHOWALL";
            public const string Showbyid = "PROC_LOGISTICA_INVENTARIOS_ALMACENES_SHOWBYID";
            public const string Showcombox = "PROC_LOGISTICA_INVENTARIOS_ALMACENES_SHOWCOMBOX";
            public const string Search = "PROC_LOGISTICA_INVENTARIOS_ALMACENES_SEARCH";
            public const string Insert = "PROC_LOGISTICA_INVENTARIOS_ALMACENES_INSERT";
            public const string Update = "PROC_LOGISTICA_INVENTARIOS_ALMACENES_UPDATE";
            public const string Delete = "PROC_LOGISTICA_INVENTARIOS_ALMACENES_DELETE";
        }
        public static class InventariosAlmacenesProductos
        {
            public const string Showall = "PROC_LOGISTICA_INVENTARIOS_ALMACENES_PRODUCTOS_SHOWALL";
            public const string Showbyid = "PROC_LOGISTICA_INVENTARIOS_ALMACENES_PRODUCTOS_SHOWBYID";
            public const string Showcombox = "PROC_LOGISTICA_INVENTARIOS_ALMACENES_PRODUCTOS_SHOWCOMBOX";
            public const string Search = "PROC_LOGISTICA_INVENTARIOS_ALMACENES_PRODUCTOS_SEARCH";
            public const string Insert = "PROC_LOGISTICA_INVENTARIOS_ALMACENES_PRODUCTOS_INSERT";
            public const string Update = "PROC_LOGISTICA_INVENTARIOS_ALMACENES_PRODUCTOS_UPDATE";
            public const string Delete = "PROC_LOGISTICA_INVENTARIOS_ALMACENES_PRODUCTOS_DELETE";
        }
        public static class InventariosAlmacenesPuntoToma
        {
            public const string Showall = "PROC_LOGISTICA_INVENTARIOS_ALMACENES_PUNTO_TOMA_SHOWALL";
            public const string Showbyid = "PROC_LOGISTICA_INVENTARIOS_ALMACENES_PUNTO_TOMA_SHOWBYID";
            // OJO: pese al nombre, este SP devuelve todas las columnas (igual que Showall), no Id/Texto.
            public const string Showcombox = "PROC_LOGISTICA_INVENTARIOS_ALMACENES_PUNTO_TOMA_SHOWCOMBOX";
            public const string Search = "PROC_LOGISTICA_INVENTARIOS_ALMACENES_PUNTO_TOMA_SEARCH";
            public const string Insert = "PROC_LOGISTICA_INVENTARIOS_ALMACENES_PUNTO_TOMA_INSERT";
            public const string Update = "PROC_LOGISTICA_INVENTARIOS_ALMACENES_PUNTO_TOMA_UPDATE";
            public const string Delete = "PROC_LOGISTICA_INVENTARIOS_ALMACENES_PUNTO_TOMA_DELETE";
        }
        public static class InventariosAlmacenesPuntoTomaConteo
        {
            public const string Showall = "PROC_LOGISTICA_INVENTARIOS_ALMACENES_PUNTO_TOMA_CONTEO_SHOWALL";
            public const string Showbyid = "PROC_LOGISTICA_INVENTARIOS_ALMACENES_PUNTO_TOMA_CONTEO_SHOWBYID";
            // OJO: pese al nombre, este SP devuelve todas las columnas (igual que Showall), no Id/Texto.
            public const string Showcombox = "PROC_LOGISTICA_INVENTARIOS_ALMACENES_PUNTO_TOMA_CONTEO_SHOWCOMBOX";
            public const string Search = "PROC_LOGISTICA_INVENTARIOS_ALMACENES_PUNTO_TOMA_CONTEO_SEARCH";
            public const string Insert = "PROC_LOGISTICA_INVENTARIOS_ALMACENES_PUNTO_TOMA_CONTEO_INSERT";
            public const string Update = "PROC_LOGISTICA_INVENTARIOS_ALMACENES_PUNTO_TOMA_CONTEO_UPDATE";
            public const string Delete = "PROC_LOGISTICA_INVENTARIOS_ALMACENES_PUNTO_TOMA_CONTEO_DELETE";
            public const string Readrfidcodebar = "PROC_LOGISTICA_INVENTARIOS_ALMACENES_PUNTO_TOMA_READRFIDCODEBAR";
        }
        public static class InventariosAlmacenesPuntoTomaEquiposAsignados
        {
            public const string Showall = "PROC_LOGISTICA_INVENTARIOS_ALMACENES_PUNTO_TOMA_EQUIPOS_ASIGNADOS_SHOWALL";
            public const string Showbyid = "PROC_LOGISTICA_INVENTARIOS_ALMACENES_PUNTO_TOMA_EQUIPOS_ASIGNADOS_SHOWBYID";
            // OJO: pese al nombre, este SP devuelve todas las columnas (igual que Showall), no Id/Texto.
            public const string Showcombox = "PROC_LOGISTICA_INVENTARIOS_ALMACENES_PUNTO_TOMA_EQUIPOS_ASIGNADOS_SHOWCOMBOX";
            public const string Search = "PROC_LOGISTICA_INVENTARIOS_ALMACENES_PUNTO_TOMA_EQUIPOS_ASIGNADOS_SEARCH";
            public const string Insert = "PROC_LOGISTICA_INVENTARIOS_ALMACENES_PUNTO_TOMA_EQUIPOS_ASIGNADOS_INSERT";
            public const string Update = "PROC_LOGISTICA_INVENTARIOS_ALMACENES_PUNTO_TOMA_EQUIPOS_ASIGNADOS_UPDATE";
            public const string Delete = "PROC_LOGISTICA_INVENTARIOS_ALMACENES_PUNTO_TOMA_EQUIPOS_ASIGNADOS_DELETE";
        }
        // Tabla de asignación pura (Personal asignado a un punto de conteo): SOLO 6 SPs, sin
        // _UPDATE — el propio _INSERT es un UPSERT (ver comentario en el Domain Model).
        public static class InventariosAlmacenesPuntoTomaPersonal
        {
            public const string Showall = "PROC_LOGISTICA_INVENTARIOS_ALMACENES_PUNTO_TOMA_PERSONAL_SHOWALL";
            public const string Showbyid = "PROC_LOGISTICA_INVENTARIOS_ALMACENES_PUNTO_TOMA_PERSONAL_SHOWBYID";
            // OJO: pese al nombre, este SP devuelve todas las columnas (igual que Showall), no Id/Texto.
            public const string Showcombox = "PROC_LOGISTICA_INVENTARIOS_ALMACENES_PUNTO_TOMA_PERSONAL_SHOWCOMBOX";
            public const string Search = "PROC_LOGISTICA_INVENTARIOS_ALMACENES_PUNTO_TOMA_PERSONAL_SEARCH";
            public const string Insert = "PROC_LOGISTICA_INVENTARIOS_ALMACENES_PUNTO_TOMA_PERSONAL_INSERT";
            public const string Delete = "PROC_LOGISTICA_INVENTARIOS_ALMACENES_PUNTO_TOMA_PERSONAL_DELETE";
        }
        public static class InventariosImagen
        {
            public const string Showall = "PROC_LOGISTICA_INVENTARIOS_IMAGEN_SHOWALL";
            public const string Showbyid = "PROC_LOGISTICA_INVENTARIOS_IMAGEN_SHOWBYID";
            public const string Showcombox = "PROC_LOGISTICA_INVENTARIOS_IMAGEN_SHOWCOMBOX";
            public const string Search = "PROC_LOGISTICA_INVENTARIOS_IMAGEN_SEARCH";
            public const string Insert = "PROC_LOGISTICA_INVENTARIOS_IMAGEN_INSERT";
            public const string Update = "PROC_LOGISTICA_INVENTARIOS_IMAGEN_UPDATE";
            public const string Delete = "PROC_LOGISTICA_INVENTARIOS_IMAGEN_DELETE";
        }
        public static class InventarosAlmacenesInventariadores
        {
            public const string Showall = "PROC_LOGISTICA_INVENTAROS_ALMACENES_INVENTARIADORES_SHOWALL";
            public const string Showbyid = "PROC_LOGISTICA_INVENTAROS_ALMACENES_INVENTARIADORES_SHOWBYID";
            public const string Showcombox = "PROC_LOGISTICA_INVENTAROS_ALMACENES_INVENTARIADORES_SHOWCOMBOX";
            public const string Search = "PROC_LOGISTICA_INVENTAROS_ALMACENES_INVENTARIADORES_SEARCH";
            public const string Insert = "PROC_LOGISTICA_INVENTAROS_ALMACENES_INVENTARIADORES_INSERT";
            public const string Update = "PROC_LOGISTICA_INVENTAROS_ALMACENES_INVENTARIADORES_UPDATE";
            public const string Delete = "PROC_LOGISTICA_INVENTAROS_ALMACENES_INVENTARIADORES_DELETE";
        }
        public static class LotesControl
        {
            public const string Showall = "PROC_LOGISTICA_LOTES_CONTROL_SHOWALL";
            public const string Showbyid = "PROC_LOGISTICA_LOTES_CONTROL_SHOWBYID";
            public const string Showcombox = "PROC_LOGISTICA_LOTES_CONTROL_SHOWCOMBOX";
            public const string Search = "PROC_LOGISTICA_LOTES_CONTROL_SEARCH";
            // Los lotes de un proveedor. Es la pregunta que hace la consulta de existencias.
            public const string ShowEntidades = "PROC_LOGISTICA_LOTES_CONTROL_SHOWENTIDADES";
            public const string Insert = "PROC_LOGISTICA_LOTES_CONTROL_INSERT";
            public const string Update = "PROC_LOGISTICA_LOTES_CONTROL_UPDATE";
            public const string Delete = "PROC_LOGISTICA_LOTES_CONTROL_DELETE";
        }
        public static class LotesControlDetalle
        {
            public const string Showall = "PROC_LOGISTICA_LOTES_CONTROL_DETALLE_SHOWALL";
            public const string Showbyid = "PROC_LOGISTICA_LOTES_CONTROL_DETALLE_SHOWBYID";
            public const string Showcombox = "PROC_LOGISTICA_LOTES_CONTROL_DETALLE_SHOWCOMBOX";
            public const string Search = "PROC_LOGISTICA_LOTES_CONTROL_DETALLE_SEARCH";
            public const string Insert = "PROC_LOGISTICA_LOTES_CONTROL_DETALLE_INSERT";
            public const string Update = "PROC_LOGISTICA_LOTES_CONTROL_DETALLE_UPDATE";
            public const string Delete = "PROC_LOGISTICA_LOTES_CONTROL_DETALLE_DELETE";
        }
        public static class MovimientosAlmacen
        {
            public const string Showall = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_SHOWALL";
            public const string Showbyid = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_SHOWBYID";
            public const string Showcombox = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_SHOWCOMBOX";
            public const string Search = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_SEARCH";
            public const string Insert = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_INSERT";
            public const string Update = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_UPDATE";
            public const string Delete = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_DELETE";
            public const string Loadcombos = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_LOAD_COMBOS";
            // "Agregar al detalle": bajan lineas de un documento de origen al detalle del
            // movimiento. Van en un solo procedimiento por origen porque escriben en dos o
            // tres tablas y tienen que ir en la misma transaccion.
            // Numero de comprobante al IMPRIMIR: consume el correlativo de la sucursal y lo
            // graba en el movimiento. Idempotente — si ya tiene numero, devuelve el mismo.
            public const string NumeroImpresion = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_NUMERO_IMPRESION";
            // Botón TRANSFERIR: pasa el movimiento a su estado "enviado" (alias ENV).
            public const string Transferir = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_TRANSFERIR";
            // Devolucion de compra: asociar guias de compra del mismo proveedor y elegir items/cantidades a devolver.
            public const string DevolucionCompraGuiasDisponibles = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_DEVOLUCION_COMPRA_GUIAS_DISPONIBLES";
            public const string DevolucionCompraGuias            = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_DEVOLUCION_COMPRA_GUIAS";
            public const string DevolucionCompraLineas           = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_DEVOLUCION_COMPRA_LINEAS";
            public const string DevolucionCompraAdicionar        = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_DEVOLUCION_COMPRA_ADICIONAR";
            public const string DevolucionCompraQuitarGuia       = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_DEVOLUCION_COMPRA_QUITAR_GUIA";
            public const string AgregarDesdeReserva        = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_AGREGAR_DESDE_RESERVA";
            public const string AgregarDesdeRequerimiento  = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_AGREGAR_DESDE_REQUERIMIENTO";
            public const string AgregarDesdePedidoComercial= "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_AGREGAR_DESDE_PEDIDO_COMERCIAL";
            // Lectura de etiquetas (RFID / codigo de barras): una lectura completa en una transaccion, y la
            // revision de lo que falta leer en un movimiento cuyo tipo exige lectura.
            public const string Leer           = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_LEER";
            public const string LecturasValidar = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_LECTURAS_VALIDAR";
        }
        // Las etiquetas RFID leidas en cada linea del movimiento (tercer nivel del detalle).
        public static class MovimientosAlmacenDetalleConteoRfid
        {
            public const string Showall = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_DETALLE_CONTEO_RFID_SHOWALL";
            public const string Showbyid = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_DETALLE_CONTEO_RFID_SHOWBYID";
            public const string Showcombox = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_DETALLE_CONTEO_RFID_SHOWCOMBOX";
            public const string Search = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_DETALLE_CONTEO_RFID_SEARCH";
            public const string Insert = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_DETALLE_CONTEO_RFID_INSERT";
            public const string Update = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_DETALLE_CONTEO_RFID_UPDATE";
            public const string Delete = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_DETALLE_CONTEO_RFID_DELETE";
        }
        public static class MovimientosAlmacenDetalle
        {
            public const string Showall = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_DETALLE_SHOWALL";
            public const string Showbyid = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_DETALLE_SHOWBYID";
            public const string Showcombox = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_DETALLE_SHOWCOMBOX";
            public const string Search = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_DETALLE_SEARCH";
            public const string Insert = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_DETALLE_INSERT";
            public const string Update = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_DETALLE_UPDATE";
            public const string Delete = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_DETALLE_DELETE";
            // Alta manual del detalle: el stock con disponible (salidas) y los combos de la línea.
            public const string StockDisponible = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_DETALLE_STOCK_DISPONIBLE";
            public const string LoadCombos = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_DETALLE_LOAD_COMBOS";
        }
        public static class MovimientosAlmacenTransporte
        {
            public const string Showall = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_TRANSPORTE_SHOWALL";
            public const string Showbyid = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_TRANSPORTE_SHOWBYID";
            public const string Showcombox = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_TRANSPORTE_SHOWCOMBOX";
            public const string Search = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_TRANSPORTE_SEARCH";
            public const string Insert = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_TRANSPORTE_INSERT";
            public const string Update = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_TRANSPORTE_UPDATE";
            public const string Delete = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_TRANSPORTE_DELETE";
        }
        public static class MovimientosAlmacenRequerimientosStock
        {
            public const string Showall = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_REQUERIMIENTOS_STOCK_SHOWALL";
            public const string Showbyid = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_REQUERIMIENTOS_STOCK_SHOWBYID";
            public const string Showcombox = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_REQUERIMIENTOS_STOCK_SHOWCOMBOX";
            public const string Search = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_REQUERIMIENTOS_STOCK_SEARCH";
            public const string Insert = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_REQUERIMIENTOS_STOCK_INSERT";
            public const string Delete = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_REQUERIMIENTOS_STOCK_DELETE";
        }
        public static class MovimientosAlmacenReservasStock
        {
            public const string Showall = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_RESERVAS_STOCK_SHOWALL";
            public const string Showbyid = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_RESERVAS_STOCK_SHOWBYID";
            public const string Showcombox = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_RESERVAS_STOCK_SHOWCOMBOX";
            public const string Search = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_RESERVAS_STOCK_SEARCH";
            public const string Insert = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_RESERVAS_STOCK_INSERT";
            public const string Delete = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_RESERVAS_STOCK_DELETE";
        }
        public static class MovimientosAlmacenReservasStockDetalle
        {
            public const string Showall = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_RESERVAS_STOCK_DETALLE_SHOWALL";
            public const string Showbyid = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_RESERVAS_STOCK_DETALLE_SHOWBYID";
            public const string Showcombox = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_RESERVAS_STOCK_DETALLE_SHOWCOMBOX";
            public const string Search = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_RESERVAS_STOCK_DETALLE_SEARCH";
            public const string Insert = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_RESERVAS_STOCK_DETALLE_INSERT";
            public const string Delete = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_RESERVAS_STOCK_DETALLE_DELETE";
        }
        public static class MovimientosAlmacenPedidosComerciales
        {
            public const string Showall = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_PEDIDOS_COMERCIALES_SHOWALL";
            public const string Showbyid = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_PEDIDOS_COMERCIALES_SHOWBYID";
            public const string Showcombox = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_PEDIDOS_COMERCIALES_SHOWCOMBOX";
            public const string Search = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_PEDIDOS_COMERCIALES_SEARCH";
            public const string Insert = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_PEDIDOS_COMERCIALES_INSERT";
            public const string Delete = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_PEDIDOS_COMERCIALES_DELETE";
        }
        /// <summary>Las cuatro consultas de la pestaña "Requerimientos" del movimiento.
        /// OJO con los nombres: estos SP NO llevan el prefijo PROC_LOGISTICA_ como el resto
        /// del esquema, van con PROC_ a secas. Están así en la base.</summary>
        public static class MovimientosAlmacenRequerimientos
        {
            public const string Disponibles        = "PROC_MOVIMIENTOS_ALMACEN_REQUERIMIENTO_STOCK_DISPONIBLES";
            public const string DisponiblesDetalle = "PROC_MOVIMIENTOS_ALMACEN_REQUERIMIENTO_STOCK_DETALLE_DISPONIBLES";
            public const string Asignados          = "PROC_MOVIMIENTOS_ALMACEN_REQUERIMIENTO_STOCK_ASIGNADOS";
            public const string AsignadosDetalle   = "PROC_MOVIMIENTOS_ALMACEN_REQUERIMIENTO_STOCK_DETALLE_ASIGNADOS";
            // 57: los dos pasan a recibir la cadena P<req>S<sec>C<cantidad>_... igual que
            // los de pedidos comerciales. Antes tomaban un requerimiento entero.
            public const string Asigna            = "PROC_MOVIMIENTOS_ALMACEN_REQUERIMIENTO_STOCK_ASIGNA";
            public const string Desasigna         = "PROC_MOVIMIENTOS_ALMACEN_REQUERIMIENTO_STOCK_DESASIGNA";
        }

        /// <summary>Las cuatro consultas de la pestaña "Órdenes de Compra" del movimiento:
        /// lo que se puede asignar (DISPONIBLES) y lo que ya está asignado (ASIGNADOS),
        /// cada uno con su cabecera y su detalle.</summary>
        public static class MovimientosAlmacenOrdenesCompra
        {
            public const string Disponibles        = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_PEDIDOS_COMERCIALES_DISPONIBLES";
            public const string DisponiblesDetalle = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_PEDIDOS_COMERCIALES_DETALLE_DISPONIBLES";
            public const string Asignados          = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_PEDIDOS_COMERCIALES_ASIGNADOS";
            public const string AsignadosDetalle   = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_PEDIDOS_COMERCIALES_DETALLE_ASIGNADOS";

            /// <summary>Asignar y quitar líneas de orden de compra al movimiento.
            ///
            /// OJO CON EL NOMBRE: estos dos NO llevan el prefijo LOGISTICA que sí tienen los
            /// cuatro de arriba. Están así en la base —los verifiqué en el dump v24— y
            /// "corregirlos" acá haría que no se encuentre el procedimiento.
            ///
            /// Los dos reciben la selección como UNA cadena con formato propio:
            /// P&lt;pedido&gt;S&lt;sec&gt;S&lt;sec&gt;_P&lt;pedido&gt;S&lt;sec&gt;. La arma
            /// ListaPedidoSecuencia, en un solo lugar, porque un separador equivocado NO da
            /// error: FNCN_SEPARAR_ID_SECUENCIA usa TRY_CONVERT y devuelve una lista vacía,
            /// con lo cual el botón no haría nada y sin decir por qué.</summary>
            public const string Asigna    = "PROC_MOVIMIENTOS_ALMACEN_PEDIDOS_COMERCIALES_ASIGNA";
            public const string Desasigna = "PROC_MOVIMIENTOS_ALMACEN_PEDIDOS_COMERCIALES_DESASIGNA";
        }
        public static class MovimientosAlmacenPedidosComercialesDetalle
        {
            public const string Showall = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_PEDIDOS_COMERCIALES_DETALLE_SHOWALL";
            public const string Showbyid = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_PEDIDOS_COMERCIALES_DETALLE_SHOWBYID";
            public const string Showcombox = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_PEDIDOS_COMERCIALES_DETALLE_SHOWCOMBOX";
            public const string Search = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_PEDIDOS_COMERCIALES_DETALLE_SEARCH";
            public const string Insert = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_PEDIDOS_COMERCIALES_DETALLE_INSERT";
            public const string Delete = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_PEDIDOS_COMERCIALES_DETALLE_DELETE";
        }
        public static class PedidosComercialesCronogramaEntregas
        {
            public const string Showall = "PROC_LOGISTICA_PEDIDOS_COMERCIALES_CRONOGRAMA_ENTREGAS_SHOWALL";
            public const string Showbyid = "PROC_LOGISTICA_PEDIDOS_COMERCIALES_CRONOGRAMA_ENTREGAS_SHOWBYID";
            public const string Showcombox = "PROC_LOGISTICA_PEDIDOS_COMERCIALES_CRONOGRAMA_ENTREGAS_SHOWCOMBOX";
            public const string Search = "PROC_LOGISTICA_PEDIDOS_COMERCIALES_CRONOGRAMA_ENTREGAS_SEARCH";
            public const string Insert = "PROC_LOGISTICA_PEDIDOS_COMERCIALES_CRONOGRAMA_ENTREGAS_INSERT";
            public const string Update = "PROC_LOGISTICA_PEDIDOS_COMERCIALES_CRONOGRAMA_ENTREGAS_UPDATE";
            public const string Delete = "PROC_LOGISTICA_PEDIDOS_COMERCIALES_CRONOGRAMA_ENTREGAS_DELETE";
            /// <summary>Cronograma COMPLETO del pedido, de todas sus líneas, en una sola
            /// consulta. El _SHOWALL de arriba exige @NUM_SECUENCIA y devuelve una línea por
            /// vez: para la pestaña de una orden de diez líneas harían falta diez llamadas.
            /// SP nuevo, se crea con SQL/01_CRONOGRAMA_ENTREGAS_consultas.sql.</summary>
            public const string ShowallPedido = "PROC_LOGISTICA_PEDIDOS_COMERCIALES_CRONOGRAMA_ENTREGAS_SHOWALL_PEDIDO";
        }
        public static class ProductosAlmacenes
        {
            public const string Showall = "PROC_LOGISTICA_PRODUCTOS_ALMACENES_SHOWALL";
            /// <summary>Misma consulta que Showall pero entrando por almacén en vez de por
            /// producto — para la pestaña que lista todos los productos configurados en un
            /// almacén. El SHOWALL original no sirve: filtra por @COD_PRODUCTO obligatorio.</summary>
            public const string ShowallPorAlmacen = "PROC_LOGISTICA_PRODUCTOS_ALMACENES_SHOWALL_ALMACEN";
            /// <summary>Pone CAN_STOCK_REQUERIR en 0 para lo que todavía NO tiene requerimiento
            /// generado. Los dos parámetros son opcionales: con almacén y producto limpia esa
            /// fila; con uno solo, todo ese almacén (o ese producto); con los dos en NULL no
            /// hace nada, porque el SP no contempla ese caso.</summary>
            /// <summary>Igual que Showall pero solo lo que tiene algo pendiente de pedir
            /// (CAN_STOCK_REQUERIR > 0) — alimenta la pestaña "A Requerir", de solo lectura.</summary>
            public const string Requerir = "PROC_LOGISTICA_PRODUCTOS_ALMACENES_REQUERIR";
            public const string Reset = "PROC_LOGISTICA_PRODUCTOS_ALMACENES_RESET";
            /// <summary>Anula el (los) requerimiento(s) generado(s) en la misma corrida que
            /// el número recibido — botón "Revertir". Solo anula los que siguen en estado
            /// REGISTRADO.</summary>
            public const string Reversion = "PROC_LOGISTICA_PRODUCTOS_ALMACENES_REVERSION";
            /// <summary>Genera los requerimientos de compra para las listas de almacenes y
            /// productos recibidas (códigos separados por "_", igual criterio que el
            /// comparativo de compras).</summary>
            public const string Requerimiento = "PROC_LOGISTICA_PRODUCTOS_ALMACENES_REQUERIMIENTO";
            /// <summary>Recalcula CAN_STOCK_REQUERIR contra el stock real (stock de
            /// seguridad): mínimo menos existencias, por almacén o por producto.</summary>
            public const string Cantidades = "PROC_LOGISTICA_PRODUCTOS_ALMACENES_CANTIDADES";
            public const string Showbyid = "PROC_LOGISTICA_PRODUCTOS_ALMACENES_SHOWBYID";
            public const string Showcombox = "PROC_LOGISTICA_PRODUCTOS_ALMACENES_SHOWCOMBOX";
            public const string Search = "PROC_LOGISTICA_PRODUCTOS_ALMACENES_SEARCH";
            public const string Insert = "PROC_LOGISTICA_PRODUCTOS_ALMACENES_INSERT";
            public const string Update = "PROC_LOGISTICA_PRODUCTOS_ALMACENES_UPDATE";
            public const string Delete = "PROC_LOGISTICA_PRODUCTOS_ALMACENES_DELETE";
        }
        public static class RequerimientosStock
        {
            public const string Showall = "PROC_LOGISTICA_REQUERIMIENTOS_STOCK_SHOWALL";
            public const string Showbyid = "PROC_LOGISTICA_REQUERIMIENTOS_STOCK_SHOWBYID";
            public const string Showcombox = "PROC_LOGISTICA_REQUERIMIENTOS_STOCK_SHOWCOMBOX";
            public const string Search = "PROC_LOGISTICA_REQUERIMIENTOS_STOCK_SEARCH";
            public const string Insert = "PROC_LOGISTICA_REQUERIMIENTOS_STOCK_INSERT";
            public const string Update = "PROC_LOGISTICA_REQUERIMIENTOS_STOCK_UPDATE";
            public const string Delete = "PROC_LOGISTICA_REQUERIMIENTOS_STOCK_DELETE";
            // Tipos + Centros de costo + Entidades + Lotes + Productos en un solo viaje,
            // para RequerimientoDocumento.razor.
            public const string Loadcombos = "PROC_LOGISTICA_REQUERIMIENTOS_STOCK_LOADCOMBOS";
            // Requerimientos de compra registrados, para elegirlos desde el pedido comercial.
            public const string Registrados = "PROC_LOGISTICA_REQUERIMIENTOS_STOCK_REGISTRADOS";
            // Botones "Genera Reservas y Req. Compra" / "Revierte Reservas y Req. Compra"
            // del documento (COD_BOTON_OPCION "04"/"05").
            public const string Makereserva = "PROC_LOGISTICA_REQUERIMIENTOS_STOCK_MAKERESERVA";
            public const string Reversereserva = "PROC_LOGISTICA_REQUERIMIENTOS_STOCK_REVERSERESERVA";
        }
        public static class RequerimientosStockDetalle
        {
            public const string Showall = "PROC_LOGISTICA_REQUERIMIENTOS_STOCK_DETALLE_SHOWALL";
            public const string Showbyid = "PROC_LOGISTICA_REQUERIMIENTOS_STOCK_DETALLE_SHOWBYID";
            public const string Showcombox = "PROC_LOGISTICA_REQUERIMIENTOS_STOCK_DETALLE_SHOWCOMBOX";
            public const string Search = "PROC_LOGISTICA_REQUERIMIENTOS_STOCK_DETALLE_SEARCH";
            public const string Insert = "PROC_LOGISTICA_REQUERIMIENTOS_STOCK_DETALLE_INSERT";
            public const string Update = "PROC_LOGISTICA_REQUERIMIENTOS_STOCK_DETALLE_UPDATE";
            public const string Delete = "PROC_LOGISTICA_REQUERIMIENTOS_STOCK_DETALLE_DELETE";
            public const string Registrados = "PROC_LOGISTICA_REQUERIMIENTOS_STOCK_DETALLE_REGISTRADOS";
        }
        public static class ReservasStock
        {
            public const string Showall = "PROC_LOGISTICA_RESERVAS_STOCK_SHOWALL";
            public const string Showbyid = "PROC_LOGISTICA_RESERVAS_STOCK_SHOWBYID";
            public const string Showcombox = "PROC_LOGISTICA_RESERVAS_STOCK_SHOWCOMBOX";
            public const string Search = "PROC_LOGISTICA_RESERVAS_STOCK_SEARCH";
            public const string Insert = "PROC_LOGISTICA_RESERVAS_STOCK_INSERT";
            public const string Update = "PROC_LOGISTICA_RESERVAS_STOCK_UPDATE";
            public const string Delete = "PROC_LOGISTICA_RESERVAS_STOCK_DELETE";
        }
        public static class ReservasStockDetalle
        {
            public const string Showall = "PROC_LOGISTICA_RESERVAS_STOCK_DETALLE_SHOWALL";
            public const string Showbyid = "PROC_LOGISTICA_RESERVAS_STOCK_DETALLE_SHOWBYID";
            public const string Showcombox = "PROC_LOGISTICA_RESERVAS_STOCK_DETALLE_SHOWCOMBOX";
            public const string Search = "PROC_LOGISTICA_RESERVAS_STOCK_DETALLE_SEARCH";
            public const string Insert = "PROC_LOGISTICA_RESERVAS_STOCK_DETALLE_INSERT";
            public const string Update = "PROC_LOGISTICA_RESERVAS_STOCK_DETALLE_UPDATE";
            public const string Delete = "PROC_LOGISTICA_RESERVAS_STOCK_DETALLE_DELETE";
        }
        public static class StocksAlmacenUbicacion
        {
            public const string Showall = "PROC_LOGISTICA_STOCKS_ALMACEN_UBICACION_SHOWALL";
            public const string Showbyid = "PROC_LOGISTICA_STOCKS_ALMACEN_UBICACION_SHOWBYID";
            public const string Showcombox = "PROC_LOGISTICA_STOCKS_ALMACEN_UBICACION_SHOWCOMBOX";
            public const string Search = "PROC_LOGISTICA_STOCKS_ALMACEN_UBICACION_SEARCH";
            // Consulta con todos los JOIN resueltos y filtros opcionales, para la pantalla
            // de existencias. No hace altas ni bajas: sólo lee.
            public const string SearchDetail = "PROC_LOGISTICA_STOCKS_ALMACEN_UBICACION_SEARCHDETAIL";
            // Los cinco combos que no dependen de nada, en una sola ida a la base.
            public const string LoadCombos = "PROC_STOCKS_ALMACEN_UBICACION_LOADCOMBOS";
            // Lo que dice una etiqueta y cómo está su stock (no graba nada).
            public const string Etiqueta = "PROC_LOGISTICA_STOCKS_ALMACEN_UBICACION_ETIQUETA";
            // Kardex de una línea de stock: sus movimientos con ingreso, salida y saldo.
            public const string Kardex = "PROC_LOGISTICA_STOCKS_ALMACEN_UBICACION_KARDEX";
            public const string Insert = "PROC_LOGISTICA_STOCKS_ALMACEN_UBICACION_INSERT";
            public const string Update = "PROC_LOGISTICA_STOCKS_ALMACEN_UBICACION_UPDATE";
            public const string Delete = "PROC_LOGISTICA_STOCKS_ALMACEN_UBICACION_DELETE";
        }
        // Traslados entre ubicaciones de un mismo almacén (pantalla Transferencias entre Ubicaciones).
        public static class TrasladoUbicaciones
        {
            public const string Tipos = "PROC_LOGISTICA_TRASLADO_UBICACIONES_TIPOS";
            public const string Mapa = "PROC_LOGISTICA_TRASLADO_UBICACIONES_MAPA";
            public const string Stock = "PROC_LOGISTICA_TRASLADO_UBICACIONES_STOCK";
            public const string Registra = "PROC_LOGISTICA_TRASLADO_UBICACIONES_REGISTRA";
            public const string Historial = "PROC_LOGISTICA_TRASLADO_UBICACIONES_HISTORIAL";
        }
        // Confirmación de recepción de transferencias (pantalla Confirmación de Recepción de Transferencias).
        public static class ConfirmacionRecepcion
        {
            public const string Pendientes = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_SHOWPORRECIBIR";
            public const string Detalle = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_SHOWPORRECIBIR_DETALLE";
            public const string MotivosRechazo = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_RECHAZO_MOTIVOS";
            public const string Confirmar = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_CONFIRMAR_RECEPCION";
            public const string Rechazar = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_RECHAZAR_RECEPCION";
            public const string LecturaContexto = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_RECEPCION_LECTURA_CONTEXTO";
            public const string Leer = "PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_RECEPCION_LEER";
        }
        // Mapa de trazabilidad de requerimientos: un nodo, sus vecinos (atrás / adelante) y la
        // entidad que se muestra en el panel. Sólo lectura.
        public static class Trazabilidad
        {
            public const string Nodo = "PROC_LOGISTICA_TRAZABILIDAD_NODO";
            public const string Vecinos = "PROC_LOGISTICA_TRAZABILIDAD_VECINOS";
            public const string Entidad = "PROC_LOGISTICA_TRAZABILIDAD_ENTIDAD";
            // El documento de partida: requerimiento, reserva, proforma, orden de compra o movimiento.
            public const string Buscar = "PROC_LOGISTICA_TRAZABILIDAD_BUSCAR";
            // Las franjas del mapa: requerimientos, reservas, una por clase de operación y almacenes.
            public const string Franjas = "PROC_LOGISTICA_TRAZABILIDAD_FRANJAS";
        }
        // Solo 5 SP: esta tabla no tiene _SEARCH.
        public static class ContenedoresAlmacenaje
        {
            public const string Showall = "PROC_LOGISTICA_CONTENEDORES_ALMACENAJE_SHOWALL";
            public const string Showbyid = "PROC_LOGISTICA_CONTENEDORES_ALMACENAJE_SHOWBYID";
            public const string Showcombox = "PROC_LOGISTICA_CONTENEDORES_ALMACENAJE_SHOWCOMBOX";
            public const string Search = "PROC_LOGISTICA_CONTENEDORES_ALMACENAJE_SEARCH";
            public const string Insert = "PROC_LOGISTICA_CONTENEDORES_ALMACENAJE_INSERT";
            public const string Delete = "PROC_LOGISTICA_CONTENEDORES_ALMACENAJE_DELETE";
            // TiposUbicacion + Sucursales + Almacenes en un solo viaje, para
            // ContenedoresAlmacenajeDialog.razor.
            public const string Loadcombos = "PROC_LOGISTICA_CONTENEDORES_ALMACENAJE_LOADCOMBOS";
        }
        public static class ContenedoresAlmacenajeCasilleros
        {
            public const string Showall = "PROC_LOGISTICA_CONTENEDORES_ALMACENAJE_CASILLEROS_SHOWALL";
            public const string Showbyid = "PROC_LOGISTICA_CONTENEDORES_ALMACENAJE_CASILLEROS_SHOWBYID";
            public const string Showcombox = "PROC_LOGISTICA_CONTENEDORES_ALMACENAJE_CASILLEROS_SHOWCOMBOX";
            public const string Search = "PROC_LOGISTICA_CONTENEDORES_ALMACENAJE_CASILLEROS_SEARCH";
            public const string Insert = "PROC_LOGISTICA_CONTENEDORES_ALMACENAJE_CASILLEROS_INSERT";
            public const string Delete = "PROC_LOGISTICA_CONTENEDORES_ALMACENAJE_CASILLEROS_DELETE";
            /// <summary>Generador de la matriz completa de casilleros + sus ubicaciones.</summary>
            public const string CreateTall = "PROC_LOGISTICA_CONTENEDORES_ALMACENAJE_CASILLEROS_CREATETALL";
        }
        // Cierre de almacenes por fecha. _INSERT cierra uno o varios almacenes a la vez (XML) y _REABRIR no borra: deja el motivo.
        public static class AlmacenesCierre
        {
            public const string Showall = "PROC_LOGISTICA_ALMACENES_CIERRE_SHOWALL";
            public const string Showbyid = "PROC_LOGISTICA_ALMACENES_CIERRE_SHOWBYID";
            public const string Search = "PROC_LOGISTICA_ALMACENES_CIERRE_SEARCH";
            public const string Consultar = "PROC_LOGISTICA_ALMACENES_CIERRE_CONSULTAR";
            public const string Insert = "PROC_LOGISTICA_ALMACENES_CIERRE_INSERT";
            public const string Update = "PROC_LOGISTICA_ALMACENES_CIERRE_UPDATE";
            public const string Reabrir = "PROC_LOGISTICA_ALMACENES_CIERRE_REABRIR";
            public const string Delete = "PROC_LOGISTICA_ALMACENES_CIERRE_DELETE";
        }
        public static class TiposUbicacion
        {
            public const string Showall = "PROC_LOGISTICA_TIPOS_UBICACION_SHOWALL";
            public const string Showbyid = "PROC_LOGISTICA_TIPOS_UBICACION_SHOWBYID";
            public const string Showcombox = "PROC_LOGISTICA_TIPOS_UBICACION_SHOWCOMBOX";
            public const string Insert = "PROC_LOGISTICA_TIPOS_UBICACION_INSERT";
            public const string Delete = "PROC_LOGISTICA_TIPOS_UBICACION_DELETE";
        }
        public static class TiposAlmacen
        {
            public const string Showall = "PROC_LOGISTICA_TIPOS_ALMACEN_SHOWALL";
            public const string Showbyid = "PROC_LOGISTICA_TIPOS_ALMACEN_SHOWBYID";
            public const string Showcombox = "PROC_LOGISTICA_TIPOS_ALMACEN_SHOWCOMBOX";
            public const string Search = "PROC_LOGISTICA_TIPOS_ALMACEN_SEARCH";
            public const string Insert = "PROC_LOGISTICA_TIPOS_ALMACEN_INSERT";
            // Módulo piloto de la separación del upsert: acá _INSERT sólo da de alta y
            // _UPDATE sólo modifica. En el resto de los módulos, _INSERT sigue siendo upsert
            // y no existe _UPDATE — los dos estilos conviven mientras dure la migración.
            public const string Update = "PROC_LOGISTICA_TIPOS_ALMACEN_UPDATE";
            public const string Delete = "PROC_LOGISTICA_TIPOS_ALMACEN_DELETE";
        }
        public static class TiposEmbalado
        {
            public const string Showall = "PROC_LOGISTICA_TIPOS_EMBALADO_SHOWALL";
            public const string Showbyid = "PROC_LOGISTICA_TIPOS_EMBALADO_SHOWBYID";
            public const string Showcombox = "PROC_LOGISTICA_TIPOS_EMBALADO_SHOWCOMBOX";
            public const string Search = "PROC_LOGISTICA_TIPOS_EMBALADO_SEARCH";
            public const string Insert = "PROC_LOGISTICA_TIPOS_EMBALADO_INSERT";
            public const string Update = "PROC_LOGISTICA_TIPOS_EMBALADO_UPDATE";
            public const string Delete = "PROC_LOGISTICA_TIPOS_EMBALADO_DELETE";
        }
        public static class TiposEmbalaje
        {
            public const string Showall = "PROC_LOGISTICA_TIPOS_EMBALAJE_SHOWALL";
            public const string Showbyid = "PROC_LOGISTICA_TIPOS_EMBALAJE_SHOWBYID";
            public const string Showcombox = "PROC_LOGISTICA_TIPOS_EMBALAJE_SHOWCOMBOX";
            public const string Search = "PROC_LOGISTICA_TIPOS_EMBALAJE_SEARCH";
            public const string Insert = "PROC_LOGISTICA_TIPOS_EMBALAJE_INSERT";
            public const string Update = "PROC_LOGISTICA_TIPOS_EMBALAJE_UPDATE";
            public const string Delete = "PROC_LOGISTICA_TIPOS_EMBALAJE_DELETE";
        }
        /// <summary>LOGISTICA.FLUJOS_APROBACION — maestro del flujo.</summary>
        public static class FlujosAprobacion
        {
            public const string Showall = "PROC_LOGISTICA_FLUJOS_APROBACION_SHOWALL";
            public const string Showbyid = "PROC_LOGISTICA_FLUJOS_APROBACION_SHOWBYID";
            public const string Showcombox = "PROC_LOGISTICA_FLUJOS_APROBACION_SHOWCOMBOX";
            public const string Search = "PROC_LOGISTICA_FLUJOS_APROBACION_SEARCH";
            public const string Insert = "PROC_LOGISTICA_FLUJOS_APROBACION_INSERT";
            public const string Update = "PROC_LOGISTICA_FLUJOS_APROBACION_UPDATE";
            public const string Delete = "PROC_LOGISTICA_FLUJOS_APROBACION_DELETE";
        }

        /// <summary>LOGISTICA.FLUJOS_APROBACION_AREA_CARGO — niveles de aprobación del flujo.
        /// Showall/Search exigen además del flujo el @COD_AREA: la pantalla elige primero el
        /// área y recién entonces lista sus cargos.</summary>
        public static class FlujosAprobacionAreaCargo
        {
            public const string Showall = "PROC_LOGISTICA_FLUJOS_APROBACION_AREA_CARGO_SHOWALL";
            public const string Showbyid = "PROC_LOGISTICA_FLUJOS_APROBACION_AREA_CARGO_SHOWBYID";
            public const string Showcombox = "PROC_LOGISTICA_FLUJOS_APROBACION_AREA_CARGO_SHOWCOMBOX";
            public const string Search = "PROC_LOGISTICA_FLUJOS_APROBACION_AREA_CARGO_SEARCH";
            public const string Insert = "PROC_LOGISTICA_FLUJOS_APROBACION_AREA_CARGO_INSERT";
            public const string Update = "PROC_LOGISTICA_FLUJOS_APROBACION_AREA_CARGO_UPDATE";
            public const string Delete = "PROC_LOGISTICA_FLUJOS_APROBACION_AREA_CARGO_DELETE";
            /// <summary>Copia los niveles de un flujo a otro. @FLG_REEMPLAZAR: 0 = solo agrega
            /// los que falten, 1 = deja el destino idéntico al origen.</summary>
            public const string Copy = "PROC_LOGISTICA_FLUJOS_APROBACION_AREA_CARGO_COPY";
        }

        /// <summary>LOGISTICA.FLUJOS_APROBACION_TIPOS_OPERACION — tipos de operación que
        /// disparan el flujo. Tabla de ASIGNACIÓN: el _INSERT es upsert y no hay _UPDATE.
        /// Showall/Search exigen además del flujo el @COD_CLASE_OPERACION.</summary>
        public static class FlujosAprobacionTiposOperacion
        {
            public const string Showall = "PROC_LOGISTICA_FLUJOS_APROBACION_TIPOS_OPERACION_SHOWALL";
            public const string Showbyid = "PROC_LOGISTICA_FLUJOS_APROBACION_TIPOS_OPERACION_SHOWBYID";
            public const string Showcombox = "PROC_LOGISTICA_FLUJOS_APROBACION_TIPOS_OPERACION_SHOWCOMBOX";
            public const string Search = "PROC_LOGISTICA_FLUJOS_APROBACION_TIPOS_OPERACION_SEARCH";
            public const string Insert = "PROC_LOGISTICA_FLUJOS_APROBACION_TIPOS_OPERACION_INSERT";
            public const string Delete = "PROC_LOGISTICA_FLUJOS_APROBACION_TIPOS_OPERACION_DELETE";
        }

        public static class TiposInventario
        {
            public const string Showall = "PROC_LOGISTICA_TIPOS_INVENTARIO_SHOWALL";
            public const string Showbyid = "PROC_LOGISTICA_TIPOS_INVENTARIO_SHOWBYID";
            public const string Showcombox = "PROC_LOGISTICA_TIPOS_INVENTARIO_SHOWCOMBOX";
            public const string Search = "PROC_LOGISTICA_TIPOS_INVENTARIO_SEARCH";
            public const string Insert = "PROC_LOGISTICA_TIPOS_INVENTARIO_INSERT";
            public const string Update = "PROC_LOGISTICA_TIPOS_INVENTARIO_UPDATE";
            public const string Delete = "PROC_LOGISTICA_TIPOS_INVENTARIO_DELETE";
        }
        public static class TiposLoteControl
        {
            public const string Showall = "PROC_LOGISTICA_TIPOS_LOTE_CONTROL_SHOWALL";
            public const string Showbyid = "PROC_LOGISTICA_TIPOS_LOTE_CONTROL_SHOWBYID";
            public const string Showcombox = "PROC_LOGISTICA_TIPOS_LOTE_CONTROL_SHOWCOMBOX";
            public const string Search = "PROC_LOGISTICA_TIPOS_LOTE_CONTROL_SEARCH";
            public const string Insert = "PROC_LOGISTICA_TIPOS_LOTE_CONTROL_INSERT";
            public const string Update = "PROC_LOGISTICA_TIPOS_LOTE_CONTROL_UPDATE";
            public const string Delete = "PROC_LOGISTICA_TIPOS_LOTE_CONTROL_DELETE";
        }
        public static class TiposMovimiento
        {
            public const string Showall = "PROC_LOGISTICA_TIPOS_MOVIMIENTO_SHOWALL";
            public const string Showbyid = "PROC_LOGISTICA_TIPOS_MOVIMIENTO_SHOWBYID";
            public const string Showcombox = "PROC_LOGISTICA_TIPOS_MOVIMIENTO_SHOWCOMBOX";
            public const string Search = "PROC_LOGISTICA_TIPOS_MOVIMIENTO_SEARCH";
            public const string Insert = "PROC_LOGISTICA_TIPOS_MOVIMIENTO_INSERT";
            // El SP existe en la base desde siempre; lo que faltaba era nombrarlo aca y
            // tener una ruta que lo llamara. Ver el comentario del controlador.
            public const string Update = "PROC_LOGISTICA_TIPOS_MOVIMIENTO_UPDATE";
            public const string Delete = "PROC_LOGISTICA_TIPOS_MOVIMIENTO_DELETE";
            // Bundlea TiposEntidades + TiposMotivos + TiposEstados + Estado en un solo
            // viaje (ver scratchpad/loadcombos_agenteE.sql). CargarMotivos() (cascada real
            // por COD_TIPO_MOTIVO_DESTINO) queda fuera, no se toca.
            public const string Loadcombos = "PROC_LOGISTICA_TIPOS_MOVIMIENTO_LOADCOMBOS";
        }
        /// <summary>Tipos de comprobante de pago asociados a un tipo de movimiento.
        /// Los nombres van tal cual están en la base: la tabla y sus SPs dicen
        /// "COMPROBATE" sin la N. Corregirlo acá sería no encontrar el SP.</summary>
        public static class TiposMovimientoTiposComprobantePago
        {
            public const string Showall = "PROC_LOGISTICA_TIPOS_MOVIMIENTO_TIPOS_COMPROBATE_PAGO_SHOWALL";
            public const string Showbyid = "PROC_LOGISTICA_TIPOS_MOVIMIENTO_TIPOS_COMPROBATE_PAGO_SHOWBYID";
            public const string Showcombox = "PROC_LOGISTICA_TIPOS_MOVIMIENTO_TIPOS_COMPROBATE_PAGO_SHOWCOMBOX";
            public const string Search = "PROC_LOGISTICA_TIPOS_MOVIMIENTO_TIPOS_COMPROBATE_PAGO_SEARCH";
            public const string Insert = "PROC_LOGISTICA_TIPOS_MOVIMIENTO_TIPOS_COMPROBATE_PAGO_INSERT";
            public const string Update = "PROC_LOGISTICA_TIPOS_MOVIMIENTO_TIPOS_COMPROBATE_PAGO_UPDATE";
            public const string Delete = "PROC_LOGISTICA_TIPOS_MOVIMIENTO_TIPOS_COMPROBATE_PAGO_DELETE";
        }
        /// <summary>Accesos directos de tipo de movimiento por usuario: qué botones-atajo
        /// muestra cada usuario arriba de la cabecera del documento de movimiento. Sin
        /// UPDATE: un acceso directo se agrega o se quita, no se modifica.</summary>
        public static class UsuariosTiposMovimientoDirectAccess
        {
            public const string Showall = "PROC_LOGISTICA_USUARIOS_TIPOS_MOVIMIENTO_DIRECT_ACCESS_SHOWALL";
            public const string Showbyid = "PROC_LOGISTICA_USUARIOS_TIPOS_MOVIMIENTO_DIRECT_ACCESS_SHOWBYID";
            public const string Showcombox = "PROC_LOGISTICA_USUARIOS_TIPOS_MOVIMIENTO_DIRECT_ACCESS_SHOWCOMBOX";
            public const string Search = "PROC_LOGISTICA_USUARIOS_TIPOS_MOVIMIENTO_DIRECT_ACCESS_SEARCH";
            public const string Insert = "PROC_LOGISTICA_USUARIOS_TIPOS_MOVIMIENTO_DIRECT_ACCESS_INSERT";
            public const string Delete = "PROC_LOGISTICA_USUARIOS_TIPOS_MOVIMIENTO_DIRECT_ACCESS_DELETE";
        }
        public static class TiposRequerimiento
        {
            public const string Showall = "PROC_LOGISTICA_TIPOS_REQUERIMIENTO_SHOWALL";
            public const string Showbyid = "PROC_LOGISTICA_TIPOS_REQUERIMIENTO_SHOWBYID";
            public const string Showcombox = "PROC_LOGISTICA_TIPOS_REQUERIMIENTO_SHOWCOMBOX";
            public const string Search = "PROC_LOGISTICA_TIPOS_REQUERIMIENTO_SEARCH";
            public const string Insert = "PROC_LOGISTICA_TIPOS_REQUERIMIENTO_INSERT";
            public const string Update = "PROC_LOGISTICA_TIPOS_REQUERIMIENTO_UPDATE";
            public const string Delete = "PROC_LOGISTICA_TIPOS_REQUERIMIENTO_DELETE";
        }
        public static class TiposReservaStock
        {
            public const string Showall = "PROC_LOGISTICA_TIPOS_RESERVA_STOCK_SHOWALL";
            public const string Showbyid = "PROC_LOGISTICA_TIPOS_RESERVA_STOCK_SHOWBYID";
            public const string Showcombox = "PROC_LOGISTICA_TIPOS_RESERVA_STOCK_SHOWCOMBOX";
            public const string Search = "PROC_LOGISTICA_TIPOS_RESERVA_STOCK_SEARCH";
            public const string Insert = "PROC_LOGISTICA_TIPOS_RESERVA_STOCK_INSERT";
            public const string Update = "PROC_LOGISTICA_TIPOS_RESERVA_STOCK_UPDATE";
            public const string Delete = "PROC_LOGISTICA_TIPOS_RESERVA_STOCK_DELETE";
        }
        public static class TiposTransporte
        {
            public const string Showall = "PROC_LOGISTICA_TIPOS_TRANSPORTE_SHOWALL";
            public const string Showbyid = "PROC_LOGISTICA_TIPOS_TRANSPORTE_SHOWBYID";
            public const string Showcombox = "PROC_LOGISTICA_TIPOS_TRANSPORTE_SHOWCOMBOX";
            public const string Search = "PROC_LOGISTICA_TIPOS_TRANSPORTE_SEARCH";
            public const string Insert = "PROC_LOGISTICA_TIPOS_TRANSPORTE_INSERT";
            public const string Update = "PROC_LOGISTICA_TIPOS_TRANSPORTE_UPDATE";
            public const string Delete = "PROC_LOGISTICA_TIPOS_TRANSPORTE_DELETE";
        }
        public static class GuiasRemisionSeries
        {
            public const string Showall = "PROC_LOGISTICA_GUIAS_REMISION_SERIES_SHOWALL";
            public const string Showbyid = "PROC_LOGISTICA_GUIAS_REMISION_SERIES_SHOWBYID";
            public const string Showcombox = "PROC_LOGISTICA_GUIAS_REMISION_SERIES_SHOWCOMBOX";
            public const string Insert = "PROC_LOGISTICA_GUIAS_REMISION_SERIES_INSERT";
            public const string Update = "PROC_LOGISTICA_GUIAS_REMISION_SERIES_UPDATE";
            public const string Delete = "PROC_LOGISTICA_GUIAS_REMISION_SERIES_DELETE";
            /// <summary>Las series con las que se puede emitir la guía de un movimiento (función de tabla).</summary>
            public const string FuncionSeriesParaMovimiento = "dbo.FNC_LOGISTICA_GUIA_SERIES_PARA_MOVIMIENTO";
        }
        public static class GuiasRemision
        {
            public const string Showall = "PROC_LOGISTICA_GUIAS_REMISION_SHOWALL";
            public const string Showbyid = "PROC_LOGISTICA_GUIAS_REMISION_SHOWBYID";
            public const string Showbymovimiento = "PROC_LOGISTICA_GUIAS_REMISION_SHOWBYMOVIMIENTO";
            public const string Xml = "PROC_LOGISTICA_GUIAS_REMISION_XML";
            public const string Cdr = "PROC_LOGISTICA_GUIAS_REMISION_CDR";
            public const string EnviosShowall = "PROC_LOGISTICA_GUIAS_REMISION_ENVIOS_SHOWALL";
            public const string Crear = "PROC_LOGISTICA_GUIAS_REMISION_CREAR";
            public const string RegistrarXml = "PROC_LOGISTICA_GUIAS_REMISION_REGISTRAR_XML";
            public const string RegistrarEnvio = "PROC_LOGISTICA_GUIAS_REMISION_REGISTRAR_ENVIO";
            public const string RegistrarRespuesta = "PROC_LOGISTICA_GUIAS_REMISION_REGISTRAR_RESPUESTA";
            public const string Reintentar = "PROC_LOGISTICA_GUIAS_REMISION_REINTENTAR";
            public const string OpcionesShowbyid = "PROC_LOGISTICA_GUIAS_REMISION_OPCIONES_SHOWBYID";
            public const string OpcionesGuardar = "PROC_LOGISTICA_GUIAS_REMISION_OPCIONES_GUARDAR";
            public const string FuncionBloqueaMovimiento = "dbo.FNC_LOGISTICA_GUIA_BLOQUEA_MOVIMIENTO";
            public const string CatalogosRevisar = "PROC_LOGISTICA_GUIAS_REMISION_CATALOGOS_REVISAR";
            /// <summary>El procedimiento de reporte de movimientos: de él salen los datos de la guía.</summary>
            public const string ReporteMovimientos = "PROC_LOGISTICA_REPORTE_MOVIMIENTOS";
        }
        public static class Transportes
        {
            public const string Showall = "PROC_LOGISTICA_TRANSPORTES_SHOWALL";
            public const string Showbyid = "PROC_LOGISTICA_TRANSPORTES_SHOWBYID";
            public const string Showcombox = "PROC_LOGISTICA_TRANSPORTES_SHOWCOMBOX";
            public const string Search = "PROC_LOGISTICA_TRANSPORTES_SEARCH";
            public const string Insert = "PROC_LOGISTICA_TRANSPORTES_INSERT";
            public const string Update = "PROC_LOGISTICA_TRANSPORTES_UPDATE";
            public const string Delete = "PROC_LOGISTICA_TRANSPORTES_DELETE";
        }
        public static class TransportesEspecificacionesTecnicas
        {
            public const string Showall = "PROC_LOGISTICA_TRANSPORTES_ESPECIFICACIONES_TECNICAS_SHOWALL";
            public const string Showbyid = "PROC_LOGISTICA_TRANSPORTES_ESPECIFICACIONES_TECNICAS_SHOWBYID";
            public const string Showcombox = "PROC_LOGISTICA_TRANSPORTES_ESPECIFICACIONES_TECNICAS_SHOWCOMBOX";
            public const string Search = "PROC_LOGISTICA_TRANSPORTES_ESPECIFICACIONES_TECNICAS_SEARCH";
            public const string Insert = "PROC_LOGISTICA_TRANSPORTES_ESPECIFICACIONES_TECNICAS_INSERT";
            public const string Update = "PROC_LOGISTICA_TRANSPORTES_ESPECIFICACIONES_TECNICAS_UPDATE";
            public const string Delete = "PROC_LOGISTICA_TRANSPORTES_ESPECIFICACIONES_TECNICAS_DELETE";
        }
    }
    public static class SCHEMAMantenimiento
    {
        public static class MantenimientoIndustrial
        {
            public const string Showall = "PROC_MANTENIMIENTO_MANTENIMIENTO_INDUSTRIAL_SHOWALL";
            public const string Showbyid = "PROC_MANTENIMIENTO_MANTENIMIENTO_INDUSTRIAL_SHOWBYID";
            public const string Showcombox = "PROC_MANTENIMIENTO_MANTENIMIENTO_INDUSTRIAL_SHOWCOMBOX";
            public const string Search = "PROC_MANTENIMIENTO_MANTENIMIENTO_INDUSTRIAL_SEARCH";
            public const string Insert = "PROC_MANTENIMIENTO_MANTENIMIENTO_INDUSTRIAL_INSERT";
            public const string Update = "PROC_MANTENIMIENTO_MANTENIMIENTO_INDUSTRIAL_UPDATE";
            public const string Delete = "PROC_MANTENIMIENTO_MANTENIMIENTO_INDUSTRIAL_DELETE";
        }
        public static class MantenimientoIndustrialDetalle
        {
            public const string Showall = "PROC_MANTENIMIENTO_MANTENIMIENTO_INDUSTRIAL_DETALLE_SHOWALL";
            public const string Showbyid = "PROC_MANTENIMIENTO_MANTENIMIENTO_INDUSTRIAL_DETALLE_SHOWBYID";
            public const string Showcombox = "PROC_MANTENIMIENTO_MANTENIMIENTO_INDUSTRIAL_DETALLE_SHOWCOMBOX";
            public const string Search = "PROC_MANTENIMIENTO_MANTENIMIENTO_INDUSTRIAL_DETALLE_SEARCH";
            public const string Insert = "PROC_MANTENIMIENTO_MANTENIMIENTO_INDUSTRIAL_DETALLE_INSERT";
            public const string Update = "PROC_MANTENIMIENTO_MANTENIMIENTO_INDUSTRIAL_DETALLE_UPDATE";
            public const string Delete = "PROC_MANTENIMIENTO_MANTENIMIENTO_INDUSTRIAL_DETALLE_DELETE";
        }
        public static class MantenimientoIndustrialDetalleEntidades
        {
            public const string Showall = "PROC_MANTENIMIENTO_MANTENIMIENTO_INDUSTRIAL_DETALLE_ENTIDADES_SHOWALL";
            public const string Showbyid = "PROC_MANTENIMIENTO_MANTENIMIENTO_INDUSTRIAL_DETALLE_ENTIDADES_SHOWBYID";
            public const string Showcombox = "PROC_MANTENIMIENTO_MANTENIMIENTO_INDUSTRIAL_DETALLE_ENTIDADES_SHOWCOMBOX";
            public const string Search = "PROC_MANTENIMIENTO_MANTENIMIENTO_INDUSTRIAL_DETALLE_ENTIDADES_SEARCH";
            public const string Insert = "PROC_MANTENIMIENTO_MANTENIMIENTO_INDUSTRIAL_DETALLE_ENTIDADES_INSERT";
            public const string Delete = "PROC_MANTENIMIENTO_MANTENIMIENTO_INDUSTRIAL_DETALLE_ENTIDADES_DELETE";
        }
        public static class MantenimientoIndustrialDetalleEspecificacionesTecnicas
        {
            public const string Showall = "PROC_MANTENIMIENTO_MANTENIMIENTO_INDUSTRIAL_DETALLE_ESPECIFICACIONES_TECNICAS_SHOWALL";
            public const string Showbyid = "PROC_MANTENIMIENTO_MANTENIMIENTO_INDUSTRIAL_DETALLE_ESPECIFICACIONES_TECNICAS_SHOWBYID";
            public const string Showcombox = "PROC_MANTENIMIENTO_MANTENIMIENTO_INDUSTRIAL_DETALLE_ESPECIFICACIONES_TECNICAS_SHOWCOMBOX";
            public const string Search = "PROC_MANTENIMIENTO_MANTENIMIENTO_INDUSTRIAL_DETALLE_ESPECIFICACIONES_TECNICAS_SEARCH";
            public const string Insert = "PROC_MANTENIMIENTO_MANTENIMIENTO_INDUSTRIAL_DETALLE_ESPECIFICACIONES_TECNICAS_INSERT";
            public const string Update = "PROC_MANTENIMIENTO_MANTENIMIENTO_INDUSTRIAL_DETALLE_ESPECIFICACIONES_TECNICAS_UPDATE";
            public const string Delete = "PROC_MANTENIMIENTO_MANTENIMIENTO_INDUSTRIAL_DETALLE_ESPECIFICACIONES_TECNICAS_DELETE";
        }
        public static class MantenimientoIndustrialDetalleRequerimientos
        {
            public const string Showall = "PROC_MANTENIMIENTO_MANTENIMIENTO_INDUSTRIAL_DETALLE_REQUERIMIENTOS_SHOWALL";
            public const string Showbyid = "PROC_MANTENIMIENTO_MANTENIMIENTO_INDUSTRIAL_DETALLE_REQUERIMIENTOS_SHOWBYID";
            public const string Showcombox = "PROC_MANTENIMIENTO_MANTENIMIENTO_INDUSTRIAL_DETALLE_REQUERIMIENTOS_SHOWCOMBOX";
            public const string Search = "PROC_MANTENIMIENTO_MANTENIMIENTO_INDUSTRIAL_DETALLE_REQUERIMIENTOS_SEARCH";
            public const string Insert = "PROC_MANTENIMIENTO_MANTENIMIENTO_INDUSTRIAL_DETALLE_REQUERIMIENTOS_INSERT";
            public const string Update = "PROC_MANTENIMIENTO_MANTENIMIENTO_INDUSTRIAL_DETALLE_REQUERIMIENTOS_UPDATE";
            public const string Delete = "PROC_MANTENIMIENTO_MANTENIMIENTO_INDUSTRIAL_DETALLE_REQUERIMIENTOS_DELETE";
        }
        public static class TiposMantenimientoIndustrial
        {
            public const string Showall = "PROC_MANTENIMIENTO_TIPOS_MANTENIMIENTO_INDUSTRIAL_SHOWALL";
            public const string Showbyid = "PROC_MANTENIMIENTO_TIPOS_MANTENIMIENTO_INDUSTRIAL_SHOWBYID";
            public const string Showcombox = "PROC_MANTENIMIENTO_TIPOS_MANTENIMIENTO_INDUSTRIAL_SHOWCOMBOX";
            public const string Search = "PROC_MANTENIMIENTO_TIPOS_MANTENIMIENTO_INDUSTRIAL_SEARCH";
            public const string Insert = "PROC_MANTENIMIENTO_TIPOS_MANTENIMIENTO_INDUSTRIAL_INSERT";
            public const string Update = "PROC_MANTENIMIENTO_TIPOS_MANTENIMIENTO_INDUSTRIAL_UPDATE";
            public const string Delete = "PROC_MANTENIMIENTO_TIPOS_MANTENIMIENTO_INDUSTRIAL_DELETE";
        }
    }
    public static class SCHEMANetworking
    {
        public static class CategoriasComision
        {
            public const string Showall = "PROC_NETWORKING_CATEGORIAS_COMISION_SHOWALL";
            public const string Showbyid = "PROC_NETWORKING_CATEGORIAS_COMISION_SHOWBYID";
            public const string Showcombox = "PROC_NETWORKING_CATEGORIAS_COMISION_SHOWCOMBOX";
            public const string Search = "PROC_NETWORKING_CATEGORIAS_COMISION_SEARCH";
            public const string Insert = "PROC_NETWORKING_CATEGORIAS_COMISION_INSERT";
            public const string Update = "PROC_NETWORKING_CATEGORIAS_COMISION_UPDATE";
            public const string Delete = "PROC_NETWORKING_CATEGORIAS_COMISION_DELETE";
        }
        public static class CategoriasComisionHistorico
        {
            public const string Showall = "PROC_NETWORKING_CATEGORIAS_COMISION_HISTORICO_SHOWALL";
            public const string Showbyid = "PROC_NETWORKING_CATEGORIAS_COMISION_HISTORICO_SHOWBYID";
            public const string Showcombox = "PROC_NETWORKING_CATEGORIAS_COMISION_HISTORICO_SHOWCOMBOX";
            public const string Search = "PROC_NETWORKING_CATEGORIAS_COMISION_HISTORICO_SEARCH";
            public const string Insert = "PROC_NETWORKING_CATEGORIAS_COMISION_HISTORICO_INSERT";
            public const string Update = "PROC_NETWORKING_CATEGORIAS_COMISION_HISTORICO_UPDATE";
            public const string Delete = "PROC_NETWORKING_CATEGORIAS_COMISION_HISTORICO_DELETE";
        }
        public static class Configuracion
        {
            public const string Showall = "PROC_NETWORKING_CONFIGURACION_SHOWALL";
            public const string Showbyid = "PROC_NETWORKING_CONFIGURACION_SHOWBYID";
            public const string Showcombox = "PROC_NETWORKING_CONFIGURACION_SHOWCOMBOX";
            public const string Search = "PROC_NETWORKING_CONFIGURACION_SEARCH";
            public const string Insert = "PROC_NETWORKING_CONFIGURACION_INSERT";
            public const string Update = "PROC_NETWORKING_CONFIGURACION_UPDATE";
            public const string Delete = "PROC_NETWORKING_CONFIGURACION_DELETE";
        }
        public static class GruposNetworking
        {
            public const string Showall = "PROC_NETWORKING_GRUPOS_NETWORKING_SHOWALL";
            public const string Showbyid = "PROC_NETWORKING_GRUPOS_NETWORKING_SHOWBYID";
            public const string Showcombox = "PROC_NETWORKING_GRUPOS_NETWORKING_SHOWCOMBOX";
            public const string Search = "PROC_NETWORKING_GRUPOS_NETWORKING_SEARCH";
            public const string Insert = "PROC_NETWORKING_GRUPOS_NETWORKING_INSERT";
            public const string Update = "PROC_NETWORKING_GRUPOS_NETWORKING_UPDATE";
            public const string Delete = "PROC_NETWORKING_GRUPOS_NETWORKING_DELETE";
        }
        public static class GruposNetworkingAsesoresComeriales
        {
            public const string Showall = "PROC_NETWORKING_GRUPOS_NETWORKING_ASESORES_COMERIALES_SHOWALL";
            public const string Showbyid = "PROC_NETWORKING_GRUPOS_NETWORKING_ASESORES_COMERIALES_SHOWBYID";
            public const string Showcombox = "PROC_NETWORKING_GRUPOS_NETWORKING_ASESORES_COMERIALES_SHOWCOMBOX";
            public const string Search = "PROC_NETWORKING_GRUPOS_NETWORKING_ASESORES_COMERIALES_SEARCH";
            public const string Insert = "PROC_NETWORKING_GRUPOS_NETWORKING_ASESORES_COMERIALES_INSERT";
            public const string Update = "PROC_NETWORKING_GRUPOS_NETWORKING_ASESORES_COMERIALES_UPDATE";
            public const string Delete = "PROC_NETWORKING_GRUPOS_NETWORKING_ASESORES_COMERIALES_DELETE";
        }
        public static class LiquidacionesComisionVenta
        {
            public const string Showall = "PROC_NETWORKING_LIQUIDACIONES_COMISION_VENTA_SHOWALL";
            public const string Showbyid = "PROC_NETWORKING_LIQUIDACIONES_COMISION_VENTA_SHOWBYID";
            public const string Showcombox = "PROC_NETWORKING_LIQUIDACIONES_COMISION_VENTA_SHOWCOMBOX";
            public const string Search = "PROC_NETWORKING_LIQUIDACIONES_COMISION_VENTA_SEARCH";
            public const string Insert = "PROC_NETWORKING_LIQUIDACIONES_COMISION_VENTA_INSERT";
            public const string Update = "PROC_NETWORKING_LIQUIDACIONES_COMISION_VENTA_UPDATE";
            public const string Delete = "PROC_NETWORKING_LIQUIDACIONES_COMISION_VENTA_DELETE";
        }
        public static class LiquidacionesComisionVentaDetalle
        {
            public const string Showall = "PROC_NETWORKING_LIQUIDACIONES_COMISION_VENTA_DETALLE_SHOWALL";
            public const string Showbyid = "PROC_NETWORKING_LIQUIDACIONES_COMISION_VENTA_DETALLE_SHOWBYID";
            public const string Showcombox = "PROC_NETWORKING_LIQUIDACIONES_COMISION_VENTA_DETALLE_SHOWCOMBOX";
            public const string Search = "PROC_NETWORKING_LIQUIDACIONES_COMISION_VENTA_DETALLE_SEARCH";
            public const string Insert = "PROC_NETWORKING_LIQUIDACIONES_COMISION_VENTA_DETALLE_INSERT";
            public const string Update = "PROC_NETWORKING_LIQUIDACIONES_COMISION_VENTA_DETALLE_UPDATE";
            public const string Delete = "PROC_NETWORKING_LIQUIDACIONES_COMISION_VENTA_DETALLE_DELETE";
        }
        public static class TiposCategoriaComision
        {
            public const string Showall = "PROC_NETWORKING_TIPOS_CATEGORIA_COMISION_SHOWALL";
            public const string Showbyid = "PROC_NETWORKING_TIPOS_CATEGORIA_COMISION_SHOWBYID";
            public const string Showcombox = "PROC_NETWORKING_TIPOS_CATEGORIA_COMISION_SHOWCOMBOX";
            public const string Search = "PROC_NETWORKING_TIPOS_CATEGORIA_COMISION_SEARCH";
            public const string Insert = "PROC_NETWORKING_TIPOS_CATEGORIA_COMISION_INSERT";
            public const string Update = "PROC_NETWORKING_TIPOS_CATEGORIA_COMISION_UPDATE";
            public const string Delete = "PROC_NETWORKING_TIPOS_CATEGORIA_COMISION_DELETE";
        }
    }
    public static class SCHEMANomina
    {
        public static class Afps
        {
            public const string Showall = "PROC_NOMINA_AFPS_SHOWALL";
            public const string Showbyid = "PROC_NOMINA_AFPS_SHOWBYID";
            public const string Showcombox = "PROC_NOMINA_AFPS_SHOWCOMBOX";
            public const string Search = "PROC_NOMINA_AFPS_SEARCH";
            public const string Insert = "PROC_NOMINA_AFPS_INSERT";
            public const string Update = "PROC_NOMINA_AFPS_UPDATE";
            public const string Delete = "PROC_NOMINA_AFPS_DELETE";
        }
        public static class BeneficiosLaborales
        {
            public const string Showall = "PROC_NOMINA_BENEFICIOS_LABORALES_SHOWALL";
            public const string Showbyid = "PROC_NOMINA_BENEFICIOS_LABORALES_SHOWBYID";
            public const string Showcombox = "PROC_NOMINA_BENEFICIOS_LABORALES_SHOWCOMBOX";
            public const string Search = "PROC_NOMINA_BENEFICIOS_LABORALES_SEARCH";
            public const string Insert = "PROC_NOMINA_BENEFICIOS_LABORALES_INSERT";
            public const string Update = "PROC_NOMINA_BENEFICIOS_LABORALES_UPDATE";
            public const string Delete = "PROC_NOMINA_BENEFICIOS_LABORALES_DELETE";
        }
        public static class ConceptosRemunerativos
        {
            public const string Showall = "PROC_NOMINA_CONCEPTOS_REMUNERATIVOS_SHOWALL";
            public const string Showbyid = "PROC_NOMINA_CONCEPTOS_REMUNERATIVOS_SHOWBYID";
            public const string Showcombox = "PROC_NOMINA_CONCEPTOS_REMUNERATIVOS_SHOWCOMBOX";
            public const string Search = "PROC_NOMINA_CONCEPTOS_REMUNERATIVOS_SEARCH";
            public const string Insert = "PROC_NOMINA_CONCEPTOS_REMUNERATIVOS_INSERT";
            public const string Update = "PROC_NOMINA_CONCEPTOS_REMUNERATIVOS_UPDATE";
            public const string Delete = "PROC_NOMINA_CONCEPTOS_REMUNERATIVOS_DELETE";
        }
        public static class Horarios
        {
            public const string Showall = "PROC_NOMINA_HORARIOS_SHOWALL";
            public const string Showbyid = "PROC_NOMINA_HORARIOS_SHOWBYID";
            public const string Showcombox = "PROC_NOMINA_HORARIOS_SHOWCOMBOX";
            public const string Search = "PROC_NOMINA_HORARIOS_SEARCH";
            public const string Insert = "PROC_NOMINA_HORARIOS_INSERT";
            public const string Update = "PROC_NOMINA_HORARIOS_UPDATE";
            public const string Delete = "PROC_NOMINA_HORARIOS_DELETE";
        }
        public static class HorariosDetalle
        {
            public const string Showall = "PROC_NOMINA_HORARIOS_DETALLE_SHOWALL";
            public const string Showbyid = "PROC_NOMINA_HORARIOS_DETALLE_SHOWBYID";
            public const string Showcombox = "PROC_NOMINA_HORARIOS_DETALLE_SHOWCOMBOX";
            public const string Search = "PROC_NOMINA_HORARIOS_DETALLE_SEARCH";
            public const string Insert = "PROC_NOMINA_HORARIOS_DETALLE_INSERT";
            public const string Update = "PROC_NOMINA_HORARIOS_DETALLE_UPDATE";
            public const string Delete = "PROC_NOMINA_HORARIOS_DETALLE_DELETE";
        }
        public static class PersonalHorarios
        {
            public const string Showall = "PROC_NOMINA_PERSONAL_HORARIOS_SHOWALL";
            public const string Showbyid = "PROC_NOMINA_PERSONAL_HORARIOS_SHOWBYID";
            public const string Showcombox = "PROC_NOMINA_PERSONAL_HORARIOS_SHOWCOMBOX";
            public const string Search = "PROC_NOMINA_PERSONAL_HORARIOS_SEARCH";
            public const string Insert = "PROC_NOMINA_PERSONAL_HORARIOS_INSERT";
            public const string Update = "PROC_NOMINA_PERSONAL_HORARIOS_UPDATE";
            public const string Delete = "PROC_NOMINA_PERSONAL_HORARIOS_DELETE";
        }
        public static class PersonalMarcaciones
        {
            public const string Showall = "PROC_NOMINA_PERSONAL_MARCACIONES_SHOWALL";
            public const string Showbyid = "PROC_NOMINA_PERSONAL_MARCACIONES_SHOWBYID";
            public const string Showcombox = "PROC_NOMINA_PERSONAL_MARCACIONES_SHOWCOMBOX";
            public const string Search = "PROC_NOMINA_PERSONAL_MARCACIONES_SEARCH";
            public const string Insert = "PROC_NOMINA_PERSONAL_MARCACIONES_INSERT";
            public const string Update = "PROC_NOMINA_PERSONAL_MARCACIONES_UPDATE";
            public const string Delete = "PROC_NOMINA_PERSONAL_MARCACIONES_DELETE";
        }
        public static class PersonalTiposPersonal
        {
            public const string Showall = "PROC_NOMINA_PERSONAL_TIPOS_PERSONAL_SHOWALL";
            public const string Showbyid = "PROC_NOMINA_PERSONAL_TIPOS_PERSONAL_SHOWBYID";
            public const string Showcombox = "PROC_NOMINA_PERSONAL_TIPOS_PERSONAL_SHOWCOMBOX";
            public const string Search = "PROC_NOMINA_PERSONAL_TIPOS_PERSONAL_SEARCH";
            public const string Insert = "PROC_NOMINA_PERSONAL_TIPOS_PERSONAL_INSERT";
            public const string Update = "PROC_NOMINA_PERSONAL_TIPOS_PERSONAL_UPDATE";
            public const string Delete = "PROC_NOMINA_PERSONAL_TIPOS_PERSONAL_DELETE";
        }
        public static class PlanillasRemuneracionesDetalle
        {
            public const string Showall = "PROC_NOMINA_PLANILLAS_REMUNERACIONES_DETALLE_SHOWALL";
            public const string Showbyid = "PROC_NOMINA_PLANILLAS_REMUNERACIONES_DETALLE_SHOWBYID";
            public const string Showcombox = "PROC_NOMINA_PLANILLAS_REMUNERACIONES_DETALLE_SHOWCOMBOX";
            public const string Search = "PROC_NOMINA_PLANILLAS_REMUNERACIONES_DETALLE_SEARCH";
            public const string Insert = "PROC_NOMINA_PLANILLAS_REMUNERACIONES_DETALLE_INSERT";
            public const string Update = "PROC_NOMINA_PLANILLAS_REMUNERACIONES_DETALLE_UPDATE";
            public const string Delete = "PROC_NOMINA_PLANILLAS_REMUNERACIONES_DETALLE_DELETE";
        }
        public static class PlanillasRemuneracionesFormulas
        {
            public const string Showall = "PROC_NOMINA_PLANILLAS_REMUNERACIONES_FORMULAS_SHOWALL";
            public const string Showbyid = "PROC_NOMINA_PLANILLAS_REMUNERACIONES_FORMULAS_SHOWBYID";
            public const string Showcombox = "PROC_NOMINA_PLANILLAS_REMUNERACIONES_FORMULAS_SHOWCOMBOX";
            public const string Search = "PROC_NOMINA_PLANILLAS_REMUNERACIONES_FORMULAS_SEARCH";
            public const string Insert = "PROC_NOMINA_PLANILLAS_REMUNERACIONES_FORMULAS_INSERT";
            public const string Update = "PROC_NOMINA_PLANILLAS_REMUNERACIONES_FORMULAS_UPDATE";
            public const string Delete = "PROC_NOMINA_PLANILLAS_REMUNERACIONES_FORMULAS_DELETE";
        }
        public static class PlanillasRemuneracionesPersonal
        {
            public const string Showall = "PROC_NOMINA_PLANILLAS_REMUNERACIONES_PERSONAL_SHOWALL";
            public const string Showbyid = "PROC_NOMINA_PLANILLAS_REMUNERACIONES_PERSONAL_SHOWBYID";
            public const string Showcombox = "PROC_NOMINA_PLANILLAS_REMUNERACIONES_PERSONAL_SHOWCOMBOX";
            public const string Search = "PROC_NOMINA_PLANILLAS_REMUNERACIONES_PERSONAL_SEARCH";
            public const string Insert = "PROC_NOMINA_PLANILLAS_REMUNERACIONES_PERSONAL_INSERT";
            public const string Update = "PROC_NOMINA_PLANILLAS_REMUNERACIONES_PERSONAL_UPDATE";
            public const string Delete = "PROC_NOMINA_PLANILLAS_REMUNERACIONES_PERSONAL_DELETE";
        }
        public static class PlanillaRemuneraciones
        {
            public const string Showall = "PROC_NOMINA_PLANILLA_REMUNERACIONES_SHOWALL";
            public const string Showbyid = "PROC_NOMINA_PLANILLA_REMUNERACIONES_SHOWBYID";
            public const string Showcombox = "PROC_NOMINA_PLANILLA_REMUNERACIONES_SHOWCOMBOX";
            public const string Search = "PROC_NOMINA_PLANILLA_REMUNERACIONES_SEARCH";
            public const string Insert = "PROC_NOMINA_PLANILLA_REMUNERACIONES_INSERT";
            public const string Update = "PROC_NOMINA_PLANILLA_REMUNERACIONES_UPDATE";
            public const string Delete = "PROC_NOMINA_PLANILLA_REMUNERACIONES_DELETE";
        }
        public static class ProgramacionVacaciones
        {
            public const string Showall = "PROC_NOMINA_PROGRAMACION_VACACIONES_SHOWALL";
            public const string Showbyid = "PROC_NOMINA_PROGRAMACION_VACACIONES_SHOWBYID";
            public const string Showcombox = "PROC_NOMINA_PROGRAMACION_VACACIONES_SHOWCOMBOX";
            public const string Search = "PROC_NOMINA_PROGRAMACION_VACACIONES_SEARCH";
            public const string Insert = "PROC_NOMINA_PROGRAMACION_VACACIONES_INSERT";
            public const string Update = "PROC_NOMINA_PROGRAMACION_VACACIONES_UPDATE";
            public const string Delete = "PROC_NOMINA_PROGRAMACION_VACACIONES_DELETE";
        }
        public static class TiposConceptoRemunerativo
        {
            public const string Showall = "PROC_NOMINA_TIPOS_CONCEPTO_REMUNERATIVO_SHOWALL";
            public const string Showbyid = "PROC_NOMINA_TIPOS_CONCEPTO_REMUNERATIVO_SHOWBYID";
            public const string Showcombox = "PROC_NOMINA_TIPOS_CONCEPTO_REMUNERATIVO_SHOWCOMBOX";
            public const string Search = "PROC_NOMINA_TIPOS_CONCEPTO_REMUNERATIVO_SEARCH";
            public const string Insert = "PROC_NOMINA_TIPOS_CONCEPTO_REMUNERATIVO_INSERT";
            public const string Update = "PROC_NOMINA_TIPOS_CONCEPTO_REMUNERATIVO_UPDATE";
            public const string Delete = "PROC_NOMINA_TIPOS_CONCEPTO_REMUNERATIVO_DELETE";
        }
        public static class TiposCuentaCorriente
        {
            public const string Showall = "PROC_NOMINA_TIPOS_CUENTA_CORRIENTE_SHOWALL";
            public const string Showbyid = "PROC_NOMINA_TIPOS_CUENTA_CORRIENTE_SHOWBYID";
            public const string Showcombox = "PROC_NOMINA_TIPOS_CUENTA_CORRIENTE_SHOWCOMBOX";
            public const string Search = "PROC_NOMINA_TIPOS_CUENTA_CORRIENTE_SEARCH";
            public const string Insert = "PROC_NOMINA_TIPOS_CUENTA_CORRIENTE_INSERT";
            public const string Update = "PROC_NOMINA_TIPOS_CUENTA_CORRIENTE_UPDATE";
            public const string Delete = "PROC_NOMINA_TIPOS_CUENTA_CORRIENTE_DELETE";
        }
        public static class TiposHorario
        {
            public const string Showall = "PROC_NOMINA_TIPOS_HORARIO_SHOWALL";
            public const string Showbyid = "PROC_NOMINA_TIPOS_HORARIO_SHOWBYID";
            public const string Showcombox = "PROC_NOMINA_TIPOS_HORARIO_SHOWCOMBOX";
            public const string Search = "PROC_NOMINA_TIPOS_HORARIO_SEARCH";
            public const string Insert = "PROC_NOMINA_TIPOS_HORARIO_INSERT";
            public const string Update = "PROC_NOMINA_TIPOS_HORARIO_UPDATE";
            public const string Delete = "PROC_NOMINA_TIPOS_HORARIO_DELETE";
        }
        public static class TiposPlanilla
        {
            public const string Showall = "PROC_NOMINA_TIPOS_PLANILLA_SHOWALL";
            public const string Showbyid = "PROC_NOMINA_TIPOS_PLANILLA_SHOWBYID";
            public const string Showcombox = "PROC_NOMINA_TIPOS_PLANILLA_SHOWCOMBOX";
            public const string Search = "PROC_NOMINA_TIPOS_PLANILLA_SEARCH";
            public const string Insert = "PROC_NOMINA_TIPOS_PLANILLA_INSERT";
            public const string Update = "PROC_NOMINA_TIPOS_PLANILLA_UPDATE";
            public const string Delete = "PROC_NOMINA_TIPOS_PLANILLA_DELETE";
        }
        public static class TiposPlanillaConceptosRemunerativos
        {
            public const string Showall = "PROC_NOMINA_TIPOS_PLANILLA_CONCEPTOS_REMUNERATIVOS_SHOWALL";
            public const string Showbyid = "PROC_NOMINA_TIPOS_PLANILLA_CONCEPTOS_REMUNERATIVOS_SHOWBYID";
            public const string Showcombox = "PROC_NOMINA_TIPOS_PLANILLA_CONCEPTOS_REMUNERATIVOS_SHOWCOMBOX";
            public const string Search = "PROC_NOMINA_TIPOS_PLANILLA_CONCEPTOS_REMUNERATIVOS_SEARCH";
            public const string Insert = "PROC_NOMINA_TIPOS_PLANILLA_CONCEPTOS_REMUNERATIVOS_INSERT";
            public const string Update = "PROC_NOMINA_TIPOS_PLANILLA_CONCEPTOS_REMUNERATIVOS_UPDATE";
            public const string Delete = "PROC_NOMINA_TIPOS_PLANILLA_CONCEPTOS_REMUNERATIVOS_DELETE";
        }
        public static class TiposPlanillaPeriodos
        {
            public const string Showall = "PROC_NOMINA_TIPOS_PLANILLA_PERIODOS_SHOWALL";
            public const string Showbyid = "PROC_NOMINA_TIPOS_PLANILLA_PERIODOS_SHOWBYID";
            public const string Showcombox = "PROC_NOMINA_TIPOS_PLANILLA_PERIODOS_SHOWCOMBOX";
            public const string Search = "PROC_NOMINA_TIPOS_PLANILLA_PERIODOS_SEARCH";
            public const string Insert = "PROC_NOMINA_TIPOS_PLANILLA_PERIODOS_INSERT";
            public const string Delete = "PROC_NOMINA_TIPOS_PLANILLA_PERIODOS_DELETE";
        }
        public static class UtilidadesAnuales
        {
            public const string Showall = "PROC_NOMINA_UTILIDADES_ANUALES_SHOWALL";
            public const string Showbyid = "PROC_NOMINA_UTILIDADES_ANUALES_SHOWBYID";
            public const string Showcombox = "PROC_NOMINA_UTILIDADES_ANUALES_SHOWCOMBOX";
            public const string Search = "PROC_NOMINA_UTILIDADES_ANUALES_SEARCH";
            public const string Insert = "PROC_NOMINA_UTILIDADES_ANUALES_INSERT";
            public const string Update = "PROC_NOMINA_UTILIDADES_ANUALES_UPDATE";
            public const string Delete = "PROC_NOMINA_UTILIDADES_ANUALES_DELETE";
        }
    }
    public static class SCHEMAPlaneamiento
    {
        public static class ClasesOperacionProduccion
        {
            public const string Showall = "PROC_PLANEAMIENTO_CLASES_OPERACION_PRODUCCION_SHOWALL";
            public const string Showbyid = "PROC_PLANEAMIENTO_CLASES_OPERACION_PRODUCCION_SHOWBYID";
            public const string Showcombox = "PROC_PLANEAMIENTO_CLASES_OPERACION_PRODUCCION_SHOWCOMBOX";
            public const string Search = "PROC_PLANEAMIENTO_CLASES_OPERACION_PRODUCCION_SEARCH";
            public const string Insert = "PROC_PLANEAMIENTO_CLASES_OPERACION_PRODUCCION_INSERT";
            public const string Update = "PROC_PLANEAMIENTO_CLASES_OPERACION_PRODUCCION_UPDATE";
            public const string Delete = "PROC_PLANEAMIENTO_CLASES_OPERACION_PRODUCCION_DELETE";
        }
        public static class GruposPlaneamiento
        {
            public const string Showall = "PROC_PLANEAMIENTO_GRUPOS_PLANEAMIENTO_SHOWALL";
            public const string Showbyid = "PROC_PLANEAMIENTO_GRUPOS_PLANEAMIENTO_SHOWBYID";
            public const string Showcombox = "PROC_PLANEAMIENTO_GRUPOS_PLANEAMIENTO_SHOWCOMBOX";
            public const string Search = "PROC_PLANEAMIENTO_GRUPOS_PLANEAMIENTO_SEARCH";
            public const string Insert = "PROC_PLANEAMIENTO_GRUPOS_PLANEAMIENTO_INSERT";
            public const string Update = "PROC_PLANEAMIENTO_GRUPOS_PLANEAMIENTO_UPDATE";
            public const string Delete = "PROC_PLANEAMIENTO_GRUPOS_PLANEAMIENTO_DELETE";
        }
        public static class GruposPlaneamientosPedidos
        {
            public const string Showall = "PROC_PLANEAMIENTO_GRUPOS_PLANEAMIENTOS_PEDIDOS_SHOWALL";
            public const string Showbyid = "PROC_PLANEAMIENTO_GRUPOS_PLANEAMIENTOS_PEDIDOS_SHOWBYID";
            public const string Showcombox = "PROC_PLANEAMIENTO_GRUPOS_PLANEAMIENTOS_PEDIDOS_SHOWCOMBOX";
            public const string Search = "PROC_PLANEAMIENTO_GRUPOS_PLANEAMIENTOS_PEDIDOS_SEARCH";
            public const string Insert = "PROC_PLANEAMIENTO_GRUPOS_PLANEAMIENTOS_PEDIDOS_INSERT";
            public const string Delete = "PROC_PLANEAMIENTO_GRUPOS_PLANEAMIENTOS_PEDIDOS_DELETE";
        }
        public static class GruposPlaneamientosPedidosDetalle
        {
            public const string Showall = "PROC_PLANEAMIENTO_GRUPOS_PLANEAMIENTOS_PEDIDOS_DETALLE_SHOWALL";
            public const string Showbyid = "PROC_PLANEAMIENTO_GRUPOS_PLANEAMIENTOS_PEDIDOS_DETALLE_SHOWBYID";
            public const string Showcombox = "PROC_PLANEAMIENTO_GRUPOS_PLANEAMIENTOS_PEDIDOS_DETALLE_SHOWCOMBOX";
            public const string Search = "PROC_PLANEAMIENTO_GRUPOS_PLANEAMIENTOS_PEDIDOS_DETALLE_SEARCH";
            public const string Insert = "PROC_PLANEAMIENTO_GRUPOS_PLANEAMIENTOS_PEDIDOS_DETALLE_INSERT";
            public const string Update = "PROC_PLANEAMIENTO_GRUPOS_PLANEAMIENTOS_PEDIDOS_DETALLE_UPDATE";
            public const string Delete = "PROC_PLANEAMIENTO_GRUPOS_PLANEAMIENTOS_PEDIDOS_DETALLE_DELETE";
        }
        public static class GruposRequerimiento
        {
            public const string Showall = "PROC_PLANEAMIENTO_GRUPOS_REQUERIMIENTO_SHOWALL";
            public const string Showbyid = "PROC_PLANEAMIENTO_GRUPOS_REQUERIMIENTO_SHOWBYID";
            public const string Showcombox = "PROC_PLANEAMIENTO_GRUPOS_REQUERIMIENTO_SHOWCOMBOX";
            public const string Search = "PROC_PLANEAMIENTO_GRUPOS_REQUERIMIENTO_SEARCH";
            public const string Insert = "PROC_PLANEAMIENTO_GRUPOS_REQUERIMIENTO_INSERT";
            public const string Update = "PROC_PLANEAMIENTO_GRUPOS_REQUERIMIENTO_UPDATE";
            public const string Delete = "PROC_PLANEAMIENTO_GRUPOS_REQUERIMIENTO_DELETE";
        }
        public static class GruposRequerimientoPedidos
        {
            public const string Showall = "PROC_PLANEAMIENTO_GRUPOS_REQUERIMIENTO_PEDIDOS_SHOWALL";
            public const string Showbyid = "PROC_PLANEAMIENTO_GRUPOS_REQUERIMIENTO_PEDIDOS_SHOWBYID";
            public const string Showcombox = "PROC_PLANEAMIENTO_GRUPOS_REQUERIMIENTO_PEDIDOS_SHOWCOMBOX";
            public const string Search = "PROC_PLANEAMIENTO_GRUPOS_REQUERIMIENTO_PEDIDOS_SEARCH";
            public const string Insert = "PROC_PLANEAMIENTO_GRUPOS_REQUERIMIENTO_PEDIDOS_INSERT";
            public const string Update = "PROC_PLANEAMIENTO_GRUPOS_REQUERIMIENTO_PEDIDOS_UPDATE";
            public const string Delete = "PROC_PLANEAMIENTO_GRUPOS_REQUERIMIENTO_PEDIDOS_DELETE";
        }
        public static class OrdenProduccion
        {
            public const string Showall = "PROC_PLANEAMIENTO_ORDEN_PRODUCCION_SHOWALL";
            public const string Showbyid = "PROC_PLANEAMIENTO_ORDEN_PRODUCCION_SHOWBYID";
            public const string Showcombox = "PROC_PLANEAMIENTO_ORDEN_PRODUCCION_SHOWCOMBOX";
            public const string Search = "PROC_PLANEAMIENTO_ORDEN_PRODUCCION_SEARCH";
            public const string Insert = "PROC_PLANEAMIENTO_ORDEN_PRODUCCION_INSERT";
            public const string Update = "PROC_PLANEAMIENTO_ORDEN_PRODUCCION_UPDATE";
            public const string Delete = "PROC_PLANEAMIENTO_ORDEN_PRODUCCION_DELETE";
        }
        public static class OrdenProduccionItems
        {
            public const string Showall = "PROC_PLANEAMIENTO_ORDEN_PRODUCCION_ITEMS_SHOWALL";
            public const string Showbyid = "PROC_PLANEAMIENTO_ORDEN_PRODUCCION_ITEMS_SHOWBYID";
            public const string Showcombox = "PROC_PLANEAMIENTO_ORDEN_PRODUCCION_ITEMS_SHOWCOMBOX";
            public const string Search = "PROC_PLANEAMIENTO_ORDEN_PRODUCCION_ITEMS_SEARCH";
            public const string Insert = "PROC_PLANEAMIENTO_ORDEN_PRODUCCION_ITEMS_INSERT";
            public const string Update = "PROC_PLANEAMIENTO_ORDEN_PRODUCCION_ITEMS_UPDATE";
            public const string Delete = "PROC_PLANEAMIENTO_ORDEN_PRODUCCION_ITEMS_DELETE";
        }
        public static class OrdenProduccionUnidad
        {
            public const string Showall = "PROC_PLANEAMIENTO_ORDEN_PRODUCCION_UNIDAD_SHOWALL";
            public const string Showbyid = "PROC_PLANEAMIENTO_ORDEN_PRODUCCION_UNIDAD_SHOWBYID";
            public const string Showcombox = "PROC_PLANEAMIENTO_ORDEN_PRODUCCION_UNIDAD_SHOWCOMBOX";
            public const string Search = "PROC_PLANEAMIENTO_ORDEN_PRODUCCION_UNIDAD_SEARCH";
            public const string Insert = "PROC_PLANEAMIENTO_ORDEN_PRODUCCION_UNIDAD_INSERT";
            public const string Update = "PROC_PLANEAMIENTO_ORDEN_PRODUCCION_UNIDAD_UPDATE";
            public const string Delete = "PROC_PLANEAMIENTO_ORDEN_PRODUCCION_UNIDAD_DELETE";
        }
        public static class PlaneamientoConfiguracion
        {
            public const string Showall = "PROC_PLANEAMIENTO_PLANEAMIENTO_CONFIGURACION_SHOWALL";
            public const string Showbyid = "PROC_PLANEAMIENTO_PLANEAMIENTO_CONFIGURACION_SHOWBYID";
            public const string Showcombox = "PROC_PLANEAMIENTO_PLANEAMIENTO_CONFIGURACION_SHOWCOMBOX";
            public const string Search = "PROC_PLANEAMIENTO_PLANEAMIENTO_CONFIGURACION_SEARCH";
            public const string Insert = "PROC_PLANEAMIENTO_PLANEAMIENTO_CONFIGURACION_INSERT";
            public const string Update = "PROC_PLANEAMIENTO_PLANEAMIENTO_CONFIGURACION_UPDATE";
            public const string Delete = "PROC_PLANEAMIENTO_PLANEAMIENTO_CONFIGURACION_DELETE";
        }
        public static class Programacion
        {
            public const string Showall = "PROC_PLANEAMIENTO_PROGRAMACION_SHOWALL";
            public const string Showbyid = "PROC_PLANEAMIENTO_PROGRAMACION_SHOWBYID";
            public const string Showcombox = "PROC_PLANEAMIENTO_PROGRAMACION_SHOWCOMBOX";
            public const string Search = "PROC_PLANEAMIENTO_PROGRAMACION_SEARCH";
            public const string Insert = "PROC_PLANEAMIENTO_PROGRAMACION_INSERT";
            public const string Update = "PROC_PLANEAMIENTO_PROGRAMACION_UPDATE";
            public const string Delete = "PROC_PLANEAMIENTO_PROGRAMACION_DELETE";
        }
        public static class ProgramacionLineasProduccion
        {
            public const string Showall = "PROC_PLANEAMIENTO_PROGRAMACION_LINEAS_PRODUCCION_SHOWALL";
            public const string Showbyid = "PROC_PLANEAMIENTO_PROGRAMACION_LINEAS_PRODUCCION_SHOWBYID";
            public const string Showcombox = "PROC_PLANEAMIENTO_PROGRAMACION_LINEAS_PRODUCCION_SHOWCOMBOX";
            public const string Search = "PROC_PLANEAMIENTO_PROGRAMACION_LINEAS_PRODUCCION_SEARCH";
            public const string Insert = "PROC_PLANEAMIENTO_PROGRAMACION_LINEAS_PRODUCCION_INSERT";
            public const string Update = "PROC_PLANEAMIENTO_PROGRAMACION_LINEAS_PRODUCCION_UPDATE";
            public const string Delete = "PROC_PLANEAMIENTO_PROGRAMACION_LINEAS_PRODUCCION_DELETE";
        }
        public static class ProgramacionVersion
        {
            public const string Showall = "PROC_PLANEAMIENTO_PROGRAMACION_VERSION_SHOWALL";
            public const string Showbyid = "PROC_PLANEAMIENTO_PROGRAMACION_VERSION_SHOWBYID";
            public const string Showcombox = "PROC_PLANEAMIENTO_PROGRAMACION_VERSION_SHOWCOMBOX";
            public const string Search = "PROC_PLANEAMIENTO_PROGRAMACION_VERSION_SEARCH";
            public const string Insert = "PROC_PLANEAMIENTO_PROGRAMACION_VERSION_INSERT";
            public const string Update = "PROC_PLANEAMIENTO_PROGRAMACION_VERSION_UPDATE";
            public const string Delete = "PROC_PLANEAMIENTO_PROGRAMACION_VERSION_DELETE";
        }
        public static class ProgramacionVersionDetalle
        {
            public const string Showall = "PROC_PLANEAMIENTO_PROGRAMACION_VERSION_DETALLE_SHOWALL";
            public const string Showbyid = "PROC_PLANEAMIENTO_PROGRAMACION_VERSION_DETALLE_SHOWBYID";
            public const string Showcombox = "PROC_PLANEAMIENTO_PROGRAMACION_VERSION_DETALLE_SHOWCOMBOX";
            public const string Search = "PROC_PLANEAMIENTO_PROGRAMACION_VERSION_DETALLE_SEARCH";
            public const string Insert = "PROC_PLANEAMIENTO_PROGRAMACION_VERSION_DETALLE_INSERT";
            public const string Update = "PROC_PLANEAMIENTO_PROGRAMACION_VERSION_DETALLE_UPDATE";
            public const string Delete = "PROC_PLANEAMIENTO_PROGRAMACION_VERSION_DETALLE_DELETE";
        }
        public static class TiposOperacionProduccion
        {
            public const string Showall = "PROC_PLANEAMIENTO_TIPOS_OPERACION_PRODUCCION_SHOWALL";
            public const string Showbyid = "PROC_PLANEAMIENTO_TIPOS_OPERACION_PRODUCCION_SHOWBYID";
            public const string Showcombox = "PROC_PLANEAMIENTO_TIPOS_OPERACION_PRODUCCION_SHOWCOMBOX";
            public const string Search = "PROC_PLANEAMIENTO_TIPOS_OPERACION_PRODUCCION_SEARCH";
            public const string Insert = "PROC_PLANEAMIENTO_TIPOS_OPERACION_PRODUCCION_INSERT";
            public const string Update = "PROC_PLANEAMIENTO_TIPOS_OPERACION_PRODUCCION_UPDATE";
            public const string Delete = "PROC_PLANEAMIENTO_TIPOS_OPERACION_PRODUCCION_DELETE";
        }
        public static class TiposOrdenProduccion
        {
            public const string Showall = "PROC_PLANEAMIENTO_TIPOS_ORDEN_PRODUCCION_SHOWALL";
            public const string Showbyid = "PROC_PLANEAMIENTO_TIPOS_ORDEN_PRODUCCION_SHOWBYID";
            public const string Showcombox = "PROC_PLANEAMIENTO_TIPOS_ORDEN_PRODUCCION_SHOWCOMBOX";
            public const string Search = "PROC_PLANEAMIENTO_TIPOS_ORDEN_PRODUCCION_SEARCH";
            public const string Insert = "PROC_PLANEAMIENTO_TIPOS_ORDEN_PRODUCCION_INSERT";
            public const string Update = "PROC_PLANEAMIENTO_TIPOS_ORDEN_PRODUCCION_UPDATE";
            public const string Delete = "PROC_PLANEAMIENTO_TIPOS_ORDEN_PRODUCCION_DELETE";
        }
        public static class TiposProgramacion
        {
            public const string Showall = "PROC_PLANEAMIENTO_TIPOS_PROGRAMACION_SHOWALL";
            public const string Showbyid = "PROC_PLANEAMIENTO_TIPOS_PROGRAMACION_SHOWBYID";
            public const string Showcombox = "PROC_PLANEAMIENTO_TIPOS_PROGRAMACION_SHOWCOMBOX";
            public const string Search = "PROC_PLANEAMIENTO_TIPOS_PROGRAMACION_SEARCH";
            public const string Insert = "PROC_PLANEAMIENTO_TIPOS_PROGRAMACION_INSERT";
            public const string Update = "PROC_PLANEAMIENTO_TIPOS_PROGRAMACION_UPDATE";
            public const string Delete = "PROC_PLANEAMIENTO_TIPOS_PROGRAMACION_DELETE";
        }
    }
    public static class SCHEMAProduccion
    {
        public static class CategoriasOperacionProduccion
        {
            public const string Showall = "PROC_PRODUCCION_CATEGORIAS_OPERACION_PRODUCCION_SHOWALL";
            public const string Showbyid = "PROC_PRODUCCION_CATEGORIAS_OPERACION_PRODUCCION_SHOWBYID";
            public const string Showcombox = "PROC_PRODUCCION_CATEGORIAS_OPERACION_PRODUCCION_SHOWCOMBOX";
            public const string Search = "PROC_PRODUCCION_CATEGORIAS_OPERACION_PRODUCCION_SEARCH";
            public const string Insert = "PROC_PRODUCCION_CATEGORIAS_OPERACION_PRODUCCION_INSERT";
            public const string Update = "PROC_PRODUCCION_CATEGORIAS_OPERACION_PRODUCCION_UPDATE";
            public const string Delete = "PROC_PRODUCCION_CATEGORIAS_OPERACION_PRODUCCION_DELETE";
        }
        public static class ClasesProceso
        {
            public const string Showall = "PROC_PRODUCCION_CLASES_PROCESO_SHOWALL";
            public const string Showbyid = "PROC_PRODUCCION_CLASES_PROCESO_SHOWBYID";
            public const string Showcombox = "PROC_PRODUCCION_CLASES_PROCESO_SHOWCOMBOX";
            public const string Search = "PROC_PRODUCCION_CLASES_PROCESO_SEARCH";
            public const string Insert = "PROC_PRODUCCION_CLASES_PROCESO_INSERT";
            public const string Update = "PROC_PRODUCCION_CLASES_PROCESO_UPDATE";
            public const string Delete = "PROC_PRODUCCION_CLASES_PROCESO_DELETE";
        }
        public static class ContenedoresProduccion
        {
            public const string Showall = "PROC_PRODUCCION_CONTENEDORES_PRODUCCION_SHOWALL";
            public const string Showbyid = "PROC_PRODUCCION_CONTENEDORES_PRODUCCION_SHOWBYID";
            public const string Showcombox = "PROC_PRODUCCION_CONTENEDORES_PRODUCCION_SHOWCOMBOX";
            public const string Search = "PROC_PRODUCCION_CONTENEDORES_PRODUCCION_SEARCH";
            public const string Insert = "PROC_PRODUCCION_CONTENEDORES_PRODUCCION_INSERT";
            public const string Update = "PROC_PRODUCCION_CONTENEDORES_PRODUCCION_UPDATE";
            public const string Delete = "PROC_PRODUCCION_CONTENEDORES_PRODUCCION_DELETE";
        }
        public static class ControlCalidadProduccion
        {
            public const string Showall = "PROC_PRODUCCION_CONTROL_CALIDAD_PRODUCCION_SHOWALL";
            public const string Showbyid = "PROC_PRODUCCION_CONTROL_CALIDAD_PRODUCCION_SHOWBYID";
            public const string Showcombox = "PROC_PRODUCCION_CONTROL_CALIDAD_PRODUCCION_SHOWCOMBOX";
            public const string Search = "PROC_PRODUCCION_CONTROL_CALIDAD_PRODUCCION_SEARCH";
            public const string Insert = "PROC_PRODUCCION_CONTROL_CALIDAD_PRODUCCION_INSERT";
            public const string Update = "PROC_PRODUCCION_CONTROL_CALIDAD_PRODUCCION_UPDATE";
            public const string Delete = "PROC_PRODUCCION_CONTROL_CALIDAD_PRODUCCION_DELETE";
        }
        public static class OrdenesProduccionUnidadesPedidosComerciales
        {
            public const string Showall = "PROC_PRODUCCION_ORDENES_PRODUCCION_UNIDADES_PEDIDOS_COMERCIALES_SHOWALL";
            public const string Showbyid = "PROC_PRODUCCION_ORDENES_PRODUCCION_UNIDADES_PEDIDOS_COMERCIALES_SHOWBYID";
            public const string Showcombox = "PROC_PRODUCCION_ORDENES_PRODUCCION_UNIDADES_PEDIDOS_COMERCIALES_SHOWCOMBOX";
            public const string Search = "PROC_PRODUCCION_ORDENES_PRODUCCION_UNIDADES_PEDIDOS_COMERCIALES_SEARCH";
            public const string Insert = "PROC_PRODUCCION_ORDENES_PRODUCCION_UNIDADES_PEDIDOS_COMERCIALES_INSERT";
            public const string Update = "PROC_PRODUCCION_ORDENES_PRODUCCION_UNIDADES_PEDIDOS_COMERCIALES_UPDATE";
            public const string Delete = "PROC_PRODUCCION_ORDENES_PRODUCCION_UNIDADES_PEDIDOS_COMERCIALES_DELETE";
        }
        public static class OrdenProduccionUnidadEspecificaciones
        {
            public const string Showall = "PROC_PRODUCCION_ORDEN_PRODUCCION_UNIDAD_ESPECIFICACIONES_SHOWALL";
            public const string Showbyid = "PROC_PRODUCCION_ORDEN_PRODUCCION_UNIDAD_ESPECIFICACIONES_SHOWBYID";
            public const string Showcombox = "PROC_PRODUCCION_ORDEN_PRODUCCION_UNIDAD_ESPECIFICACIONES_SHOWCOMBOX";
            public const string Search = "PROC_PRODUCCION_ORDEN_PRODUCCION_UNIDAD_ESPECIFICACIONES_SEARCH";
            public const string Insert = "PROC_PRODUCCION_ORDEN_PRODUCCION_UNIDAD_ESPECIFICACIONES_INSERT";
            public const string Update = "PROC_PRODUCCION_ORDEN_PRODUCCION_UNIDAD_ESPECIFICACIONES_UPDATE";
            public const string Delete = "PROC_PRODUCCION_ORDEN_PRODUCCION_UNIDAD_ESPECIFICACIONES_DELETE";
        }
        public static class Procesos
        {
            public const string Showall = "PROC_PRODUCCION_PROCESOS_SHOWALL";
            public const string Showbyid = "PROC_PRODUCCION_PROCESOS_SHOWBYID";
            public const string Showcombox = "PROC_PRODUCCION_PROCESOS_SHOWCOMBOX";
            public const string Search = "PROC_PRODUCCION_PROCESOS_SEARCH";
            public const string Insert = "PROC_PRODUCCION_PROCESOS_INSERT";
            public const string Update = "PROC_PRODUCCION_PROCESOS_UPDATE";
            public const string Delete = "PROC_PRODUCCION_PROCESOS_DELETE";
            public const string Loadcombos = "PROC_PRODUCCION_PROCESOS_LOADCOMBOS";
        }
        public static class ProcesosLineas
        {
            public const string Showall = "PROC_PRODUCCION_PROCESOS_LINEAS_SHOWALL";
            public const string Showbyid = "PROC_PRODUCCION_PROCESOS_LINEAS_SHOWBYID";
            public const string Showcombox = "PROC_PRODUCCION_PROCESOS_LINEAS_SHOWCOMBOX";
            public const string Search = "PROC_PRODUCCION_PROCESOS_LINEAS_SEARCH";
            public const string Insert = "PROC_PRODUCCION_PROCESOS_LINEAS_INSERT";
            public const string Update = "PROC_PRODUCCION_PROCESOS_LINEAS_UPDATE";
            public const string Delete = "PROC_PRODUCCION_PROCESOS_LINEAS_DELETE";
        }
        public static class ProcesosLineasActivos
        {
            public const string Showall = "PROC_PRODUCCION_PROCESOS_LINEAS_ACTIVOS_SHOWALL";
            public const string Showbyid = "PROC_PRODUCCION_PROCESOS_LINEAS_ACTIVOS_SHOWBYID";
            public const string Showcombox = "PROC_PRODUCCION_PROCESOS_LINEAS_ACTIVOS_SHOWCOMBOX";
            public const string Search = "PROC_PRODUCCION_PROCESOS_LINEAS_ACTIVOS_SEARCH";
            public const string Insert = "PROC_PRODUCCION_PROCESOS_LINEAS_ACTIVOS_INSERT";
            public const string Update = "PROC_PRODUCCION_PROCESOS_LINEAS_ACTIVOS_UPDATE";
            public const string Delete = "PROC_PRODUCCION_PROCESOS_LINEAS_ACTIVOS_DELETE";
        }
        public static class RegistroProduccion
        {
            public const string Showall = "PROC_PRODUCCION_REGISTRO_PRODUCCION_SHOWALL";
            public const string Showbyid = "PROC_PRODUCCION_REGISTRO_PRODUCCION_SHOWBYID";
            public const string Showcombox = "PROC_PRODUCCION_REGISTRO_PRODUCCION_SHOWCOMBOX";
            public const string Search = "PROC_PRODUCCION_REGISTRO_PRODUCCION_SEARCH";
            public const string Insert = "PROC_PRODUCCION_REGISTRO_PRODUCCION_INSERT";
            public const string Update = "PROC_PRODUCCION_REGISTRO_PRODUCCION_UPDATE";
            public const string Delete = "PROC_PRODUCCION_REGISTRO_PRODUCCION_DELETE";
        }
        public static class RegistroProduccionTiposOperacion
        {
            public const string Showall = "PROC_PRODUCCION_REGISTRO_PRODUCCION_TIPOS_OPERACION_SHOWALL";
            public const string Showbyid = "PROC_PRODUCCION_REGISTRO_PRODUCCION_TIPOS_OPERACION_SHOWBYID";
            public const string Showcombox = "PROC_PRODUCCION_REGISTRO_PRODUCCION_TIPOS_OPERACION_SHOWCOMBOX";
            public const string Search = "PROC_PRODUCCION_REGISTRO_PRODUCCION_TIPOS_OPERACION_SEARCH";
            public const string Insert = "PROC_PRODUCCION_REGISTRO_PRODUCCION_TIPOS_OPERACION_INSERT";
            public const string Update = "PROC_PRODUCCION_REGISTRO_PRODUCCION_TIPOS_OPERACION_UPDATE";
            public const string Delete = "PROC_PRODUCCION_REGISTRO_PRODUCCION_TIPOS_OPERACION_DELETE";
        }
        public static class SubProcesos
        {
            public const string Showall = "PROC_PRODUCCION_SUB_PROCESOS_SHOWALL";
            public const string Showbyid = "PROC_PRODUCCION_SUB_PROCESOS_SHOWBYID";
            public const string Showcombox = "PROC_PRODUCCION_SUB_PROCESOS_SHOWCOMBOX";
            public const string Search = "PROC_PRODUCCION_SUB_PROCESOS_SEARCH";
            public const string Insert = "PROC_PRODUCCION_SUB_PROCESOS_INSERT";
            public const string Update = "PROC_PRODUCCION_SUB_PROCESOS_UPDATE";
            public const string Delete = "PROC_PRODUCCION_SUB_PROCESOS_DELETE";
        }
        public static class TiposLineaProduccion
        {
            public const string Showall = "PROC_PRODUCCION_TIPOS_LINEA_PRODUCCION_SHOWALL";
            public const string Showbyid = "PROC_PRODUCCION_TIPOS_LINEA_PRODUCCION_SHOWBYID";
            public const string Showcombox = "PROC_PRODUCCION_TIPOS_LINEA_PRODUCCION_SHOWCOMBOX";
            public const string Search = "PROC_PRODUCCION_TIPOS_LINEA_PRODUCCION_SEARCH";
            public const string Insert = "PROC_PRODUCCION_TIPOS_LINEA_PRODUCCION_INSERT";
            public const string Update = "PROC_PRODUCCION_TIPOS_LINEA_PRODUCCION_UPDATE";
            public const string Delete = "PROC_PRODUCCION_TIPOS_LINEA_PRODUCCION_DELETE";
        }
    }
    public static class SCHEMAProyectos
    {
        public static class Proyectos
        {
            public const string Showall = "PROC_PROYECTOS_PROYECTOS_SHOWALL";
            public const string Showbyid = "PROC_PROYECTOS_PROYECTOS_SHOWBYID";
            public const string Showcombox = "PROC_PROYECTOS_PROYECTOS_SHOWCOMBOX";
            public const string Search = "PROC_PROYECTOS_PROYECTOS_SEARCH";
            public const string Insert = "PROC_PROYECTOS_PROYECTOS_INSERT";
            public const string Update = "PROC_PROYECTOS_PROYECTOS_UPDATE";
            public const string Delete = "PROC_PROYECTOS_PROYECTOS_DELETE";
        }
        public static class ProyectosArchivos
        {
            public const string Showall = "PROC_PROYECTOS_PROYECTOS_ARCHIVOS_SHOWALL";
            public const string Showbyid = "PROC_PROYECTOS_PROYECTOS_ARCHIVOS_SHOWBYID";
            public const string Showcombox = "PROC_PROYECTOS_PROYECTOS_ARCHIVOS_SHOWCOMBOX";
            public const string Search = "PROC_PROYECTOS_PROYECTOS_ARCHIVOS_SEARCH";
            public const string Insert = "PROC_PROYECTOS_PROYECTOS_ARCHIVOS_INSERT";
            public const string Update = "PROC_PROYECTOS_PROYECTOS_ARCHIVOS_UPDATE";
            public const string Delete = "PROC_PROYECTOS_PROYECTOS_ARCHIVOS_DELETE";
        }
        public static class ProyectosFases
        {
            public const string Showall = "PROC_PROYECTOS_PROYECTOS_FASES_SHOWALL";
            public const string Showbyid = "PROC_PROYECTOS_PROYECTOS_FASES_SHOWBYID";
            public const string Showcombox = "PROC_PROYECTOS_PROYECTOS_FASES_SHOWCOMBOX";
            public const string Search = "PROC_PROYECTOS_PROYECTOS_FASES_SEARCH";
            public const string Insert = "PROC_PROYECTOS_PROYECTOS_FASES_INSERT";
            public const string Update = "PROC_PROYECTOS_PROYECTOS_FASES_UPDATE";
            public const string Delete = "PROC_PROYECTOS_PROYECTOS_FASES_DELETE";
        }
        public static class ProyectosGastos
        {
            public const string Showall = "PROC_PROYECTOS_PROYECTOS_GASTOS_SHOWALL";
            public const string Showbyid = "PROC_PROYECTOS_PROYECTOS_GASTOS_SHOWBYID";
            public const string Showcombox = "PROC_PROYECTOS_PROYECTOS_GASTOS_SHOWCOMBOX";
            public const string Search = "PROC_PROYECTOS_PROYECTOS_GASTOS_SEARCH";
            public const string Insert = "PROC_PROYECTOS_PROYECTOS_GASTOS_INSERT";
            public const string Update = "PROC_PROYECTOS_PROYECTOS_GASTOS_UPDATE";
            public const string Delete = "PROC_PROYECTOS_PROYECTOS_GASTOS_DELETE";
        }
        public static class ProyectosHitos
        {
            public const string Showall = "PROC_PROYECTOS_PROYECTOS_HITOS_SHOWALL";
            public const string Showbyid = "PROC_PROYECTOS_PROYECTOS_HITOS_SHOWBYID";
            public const string Showcombox = "PROC_PROYECTOS_PROYECTOS_HITOS_SHOWCOMBOX";
            public const string Search = "PROC_PROYECTOS_PROYECTOS_HITOS_SEARCH";
            public const string Insert = "PROC_PROYECTOS_PROYECTOS_HITOS_INSERT";
            public const string Update = "PROC_PROYECTOS_PROYECTOS_HITOS_UPDATE";
            public const string Delete = "PROC_PROYECTOS_PROYECTOS_HITOS_DELETE";
        }
        public static class ProyectosImpactoRiesgo
        {
            public const string Showall = "PROC_PROYECTOS_PROYECTOS_IMPACTO_RIESGO_SHOWALL";
            public const string Showbyid = "PROC_PROYECTOS_PROYECTOS_IMPACTO_RIESGO_SHOWBYID";
            public const string Showcombox = "PROC_PROYECTOS_PROYECTOS_IMPACTO_RIESGO_SHOWCOMBOX";
            public const string Search = "PROC_PROYECTOS_PROYECTOS_IMPACTO_RIESGO_SEARCH";
            public const string Insert = "PROC_PROYECTOS_PROYECTOS_IMPACTO_RIESGO_INSERT";
            public const string Update = "PROC_PROYECTOS_PROYECTOS_IMPACTO_RIESGO_UPDATE";
            public const string Delete = "PROC_PROYECTOS_PROYECTOS_IMPACTO_RIESGO_DELETE";
        }
        public static class ProyectosMemoria
        {
            public const string Showall = "PROC_PROYECTOS_PROYECTOS_MEMORIA_SHOWALL";
            public const string Showbyid = "PROC_PROYECTOS_PROYECTOS_MEMORIA_SHOWBYID";
            public const string Showcombox = "PROC_PROYECTOS_PROYECTOS_MEMORIA_SHOWCOMBOX";
            public const string Search = "PROC_PROYECTOS_PROYECTOS_MEMORIA_SEARCH";
            public const string Insert = "PROC_PROYECTOS_PROYECTOS_MEMORIA_INSERT";
            public const string Update = "PROC_PROYECTOS_PROYECTOS_MEMORIA_UPDATE";
            public const string Delete = "PROC_PROYECTOS_PROYECTOS_MEMORIA_DELETE";
        }
        public static class ProyectosPlanesMitigacionRiesgos
        {
            public const string Showall = "PROC_PROYECTOS_PROYECTOS_PLANES_MITIGACION_RIESGOS_SHOWALL";
            public const string Showbyid = "PROC_PROYECTOS_PROYECTOS_PLANES_MITIGACION_RIESGOS_SHOWBYID";
            public const string Showcombox = "PROC_PROYECTOS_PROYECTOS_PLANES_MITIGACION_RIESGOS_SHOWCOMBOX";
            public const string Search = "PROC_PROYECTOS_PROYECTOS_PLANES_MITIGACION_RIESGOS_SEARCH";
            public const string Insert = "PROC_PROYECTOS_PROYECTOS_PLANES_MITIGACION_RIESGOS_INSERT";
            public const string Update = "PROC_PROYECTOS_PROYECTOS_PLANES_MITIGACION_RIESGOS_UPDATE";
            public const string Delete = "PROC_PROYECTOS_PROYECTOS_PLANES_MITIGACION_RIESGOS_DELETE";
        }
        public static class ProyectosProbabilidadRiesgo
        {
            public const string Showall = "PROC_PROYECTOS_PROYECTOS_PROBABILIDAD_RIESGO_SHOWALL";
            public const string Showbyid = "PROC_PROYECTOS_PROYECTOS_PROBABILIDAD_RIESGO_SHOWBYID";
            public const string Showcombox = "PROC_PROYECTOS_PROYECTOS_PROBABILIDAD_RIESGO_SHOWCOMBOX";
            public const string Search = "PROC_PROYECTOS_PROYECTOS_PROBABILIDAD_RIESGO_SEARCH";
            public const string Insert = "PROC_PROYECTOS_PROYECTOS_PROBABILIDAD_RIESGO_INSERT";
            public const string Update = "PROC_PROYECTOS_PROYECTOS_PROBABILIDAD_RIESGO_UPDATE";
            public const string Delete = "PROC_PROYECTOS_PROYECTOS_PROBABILIDAD_RIESGO_DELETE";
        }
        public static class ProyectosRecursoHumanoAsignado
        {
            public const string Showall = "PROC_PROYECTOS_PROYECTOS_RECURSO_HUMANO_ASIGNADO_SHOWALL";
            public const string Showbyid = "PROC_PROYECTOS_PROYECTOS_RECURSO_HUMANO_ASIGNADO_SHOWBYID";
            public const string Showcombox = "PROC_PROYECTOS_PROYECTOS_RECURSO_HUMANO_ASIGNADO_SHOWCOMBOX";
            public const string Search = "PROC_PROYECTOS_PROYECTOS_RECURSO_HUMANO_ASIGNADO_SEARCH";
            public const string Insert = "PROC_PROYECTOS_PROYECTOS_RECURSO_HUMANO_ASIGNADO_INSERT";
            public const string Update = "PROC_PROYECTOS_PROYECTOS_RECURSO_HUMANO_ASIGNADO_UPDATE";
            public const string Delete = "PROC_PROYECTOS_PROYECTOS_RECURSO_HUMANO_ASIGNADO_DELETE";
        }
        public static class ProyectosRiesgos
        {
            public const string Showall = "PROC_PROYECTOS_PROYECTOS_RIESGOS_SHOWALL";
            public const string Showbyid = "PROC_PROYECTOS_PROYECTOS_RIESGOS_SHOWBYID";
            public const string Showcombox = "PROC_PROYECTOS_PROYECTOS_RIESGOS_SHOWCOMBOX";
            public const string Search = "PROC_PROYECTOS_PROYECTOS_RIESGOS_SEARCH";
            public const string Insert = "PROC_PROYECTOS_PROYECTOS_RIESGOS_INSERT";
            public const string Update = "PROC_PROYECTOS_PROYECTOS_RIESGOS_UPDATE";
            public const string Delete = "PROC_PROYECTOS_PROYECTOS_RIESGOS_DELETE";
        }
        public static class ProyectosRoles
        {
            public const string Showall = "PROC_PROYECTOS_PROYECTOS_ROLES_SHOWALL";
            public const string Showbyid = "PROC_PROYECTOS_PROYECTOS_ROLES_SHOWBYID";
            public const string Showcombox = "PROC_PROYECTOS_PROYECTOS_ROLES_SHOWCOMBOX";
            public const string Search = "PROC_PROYECTOS_PROYECTOS_ROLES_SEARCH";
            public const string Insert = "PROC_PROYECTOS_PROYECTOS_ROLES_INSERT";
            public const string Update = "PROC_PROYECTOS_PROYECTOS_ROLES_UPDATE";
            public const string Delete = "PROC_PROYECTOS_PROYECTOS_ROLES_DELETE";
        }
        public static class ProyectosParametros
        {
            public const string Show = "PROC_PROYECTOS_PROYECTOS_PARAMETROS_SHOW";
            public const string Guardar = "PROC_PROYECTOS_PROYECTOS_PARAMETROS_GUARDAR";
        }
        public static class ProyectosMateriales
        {
            public const string Showall = "PROC_PROYECTOS_PROYECTOS_MATERIALES_SHOWALL";
            public const string Showbyid = "PROC_PROYECTOS_PROYECTOS_MATERIALES_SHOWBYID";
            public const string Showcombox = "PROC_PROYECTOS_PROYECTOS_MATERIALES_SHOWCOMBOX";
            public const string Search = "PROC_PROYECTOS_PROYECTOS_MATERIALES_SEARCH";
            public const string Insert = "PROC_PROYECTOS_PROYECTOS_MATERIALES_INSERT";
            public const string Update = "PROC_PROYECTOS_PROYECTOS_MATERIALES_UPDATE";
            public const string Delete = "PROC_PROYECTOS_PROYECTOS_MATERIALES_DELETE";
        }
        public static class ProyectosGastosAdicionales
        {
            public const string Showall = "PROC_PROYECTOS_PROYECTOS_GASTOS_ADICIONALES_SHOWALL";
            public const string Showbyid = "PROC_PROYECTOS_PROYECTOS_GASTOS_ADICIONALES_SHOWBYID";
            public const string Showcombox = "PROC_PROYECTOS_PROYECTOS_GASTOS_ADICIONALES_SHOWCOMBOX";
            public const string Search = "PROC_PROYECTOS_PROYECTOS_GASTOS_ADICIONALES_SEARCH";
            public const string Insert = "PROC_PROYECTOS_PROYECTOS_GASTOS_ADICIONALES_INSERT";
            public const string Update = "PROC_PROYECTOS_PROYECTOS_GASTOS_ADICIONALES_UPDATE";
            public const string Delete = "PROC_PROYECTOS_PROYECTOS_GASTOS_ADICIONALES_DELETE";
        }
        public static class ProyectosSubTareas
        {
            public const string Showall = "PROC_PROYECTOS_PROYECTOS_SUB_TAREAS_SHOWALL";
            public const string Showbyid = "PROC_PROYECTOS_PROYECTOS_SUB_TAREAS_SHOWBYID";
            public const string Showcombox = "PROC_PROYECTOS_PROYECTOS_SUB_TAREAS_SHOWCOMBOX";
            public const string Search = "PROC_PROYECTOS_PROYECTOS_SUB_TAREAS_SEARCH";
            public const string Insert = "PROC_PROYECTOS_PROYECTOS_SUB_TAREAS_INSERT";
            public const string Update = "PROC_PROYECTOS_PROYECTOS_SUB_TAREAS_UPDATE";
            public const string Delete = "PROC_PROYECTOS_PROYECTOS_SUB_TAREAS_DELETE";
        }
        public static class ProyectosTareas
        {
            public const string Showall = "PROC_PROYECTOS_PROYECTOS_TAREAS_SHOWALL";
            public const string Showbyid = "PROC_PROYECTOS_PROYECTOS_TAREAS_SHOWBYID";
            public const string Showcombox = "PROC_PROYECTOS_PROYECTOS_TAREAS_SHOWCOMBOX";
            public const string Search = "PROC_PROYECTOS_PROYECTOS_TAREAS_SEARCH";
            public const string Insert = "PROC_PROYECTOS_PROYECTOS_TAREAS_INSERT";
            public const string Update = "PROC_PROYECTOS_PROYECTOS_TAREAS_UPDATE";
            public const string Delete = "PROC_PROYECTOS_PROYECTOS_TAREAS_DELETE";
        }
        public static class ProyectosTiposEntidad
        {
            public const string Showall = "PROC_PROYECTOS_PROYECTOS_TIPOS_ENTIDAD_SHOWALL";
            public const string Showbyid = "PROC_PROYECTOS_PROYECTOS_TIPOS_ENTIDAD_SHOWBYID";
            public const string Showcombox = "PROC_PROYECTOS_PROYECTOS_TIPOS_ENTIDAD_SHOWCOMBOX";
            public const string Search = "PROC_PROYECTOS_PROYECTOS_TIPOS_ENTIDAD_SEARCH";
            public const string Insert = "PROC_PROYECTOS_PROYECTOS_TIPOS_ENTIDAD_INSERT";
            public const string Delete = "PROC_PROYECTOS_PROYECTOS_TIPOS_ENTIDAD_DELETE";
        }
        public static class ProyectosUnidadesMedidaTareas
        {
            public const string Showall = "PROC_PROYECTOS_PROYECTOS_UNIDADES_MEDIDA_TAREAS_SHOWALL";
            public const string Showbyid = "PROC_PROYECTOS_PROYECTOS_UNIDADES_MEDIDA_TAREAS_SHOWBYID";
            public const string Showcombox = "PROC_PROYECTOS_PROYECTOS_UNIDADES_MEDIDA_TAREAS_SHOWCOMBOX";
            public const string Search = "PROC_PROYECTOS_PROYECTOS_UNIDADES_MEDIDA_TAREAS_SEARCH";
            public const string Insert = "PROC_PROYECTOS_PROYECTOS_UNIDADES_MEDIDA_TAREAS_INSERT";
            public const string Update = "PROC_PROYECTOS_PROYECTOS_UNIDADES_MEDIDA_TAREAS_UPDATE";
            public const string Delete = "PROC_PROYECTOS_PROYECTOS_UNIDADES_MEDIDA_TAREAS_DELETE";
        }
        public static class TiposProyecto
        {
            public const string Showall = "PROC_PROYECTOS_TIPOS_PROYECTO_SHOWALL";
            public const string Showbyid = "PROC_PROYECTOS_TIPOS_PROYECTO_SHOWBYID";
            public const string Showcombox = "PROC_PROYECTOS_TIPOS_PROYECTO_SHOWCOMBOX";
            public const string Search = "PROC_PROYECTOS_TIPOS_PROYECTO_SEARCH";
            public const string Insert = "PROC_PROYECTOS_TIPOS_PROYECTO_INSERT";
            public const string Update = "PROC_PROYECTOS_TIPOS_PROYECTO_UPDATE";
            public const string Delete = "PROC_PROYECTOS_TIPOS_PROYECTO_DELETE";
        }
    }
    // Módulo RFID: emisión de códigos de etiquetas (TID + correlativo), equipos y consulta de etiquetas.
    public static class SCHEMARfid
    {
        public static class Equipos
        {
            public const string Showall = "PROC_RFID_EQUIPOS_SHOWALL";
            public const string Showbyid = "PROC_RFID_EQUIPOS_SHOWBYID";
            public const string Showcombox = "PROC_RFID_EQUIPOS_SHOWCOMBOX";
        }
        public static class Estaciones
        {
            public const string Showcombox = "PROC_RFID_ESTACIONES_SHOWCOMBOX";
        }
        public static class Emisiones
        {
            public const string Showall = "PROC_RFID_EMISIONES_SHOWALL";
            public const string Showbyid = "PROC_RFID_EMISIONES_SHOWBYID";
            public const string Search = "PROC_RFID_EMISIONES_SEARCH";
            public const string DetalleShowall = "PROC_RFID_EMISIONES_DETALLE_SHOWALL";
            /// <summary>Crea la emisión y sus etiquetas pendientes (XML de líneas). Todo o nada.</summary>
            public const string Insert = "PROC_RFID_EMISIONES_INSERT";
            public const string Update = "PROC_RFID_EMISIONES_UPDATE";
            public const string Anular = "PROC_RFID_EMISIONES_ANULAR";
            public const string Delete = "PROC_RFID_EMISIONES_DELETE";
        }
        public static class Etiquetas
        {
            public const string Showall = "PROC_RFID_ETIQUETAS_SHOWALL";
            public const string Search = "PROC_RFID_ETIQUETAS_SEARCH";
            /// <summary>Un chip entrega su TID: recibe el correlativo, queda en ESTANDAR.OBJETOS_RFID y devuelve el código y el EPC.</summary>
            public const string Enrolar = "PROC_RFID_ETIQUETAS_ENROLAR";
            public const string Grabada = "PROC_RFID_ETIQUETAS_GRABADA";
            public const string Anular = "PROC_RFID_ETIQUETAS_ANULAR";
        }
        /// <summary>La cola de impresión: la web pide imprimir y el servicio del agente toma los trabajos (35_TRABAJOS_IMPRESION.sql).</summary>
        public static class TrabajosImpresion
        {
            public const string Search = "PROC_RFID_TRABAJOS_IMPRESION_SEARCH";
            public const string Showbyid = "PROC_RFID_TRABAJOS_IMPRESION_SHOWBYID";
            public const string Solicitar = "PROC_RFID_TRABAJOS_IMPRESION_SOLICITAR";
            public const string Cancelar = "PROC_RFID_TRABAJOS_IMPRESION_CANCELAR";
            public const string Reclamar = "PROC_RFID_TRABAJOS_IMPRESION_RECLAMAR";
            public const string Progreso = "PROC_RFID_TRABAJOS_IMPRESION_PROGRESO";
            public const string Terminar = "PROC_RFID_TRABAJOS_IMPRESION_TERMINAR";
        }
        public static class Origen
        {
            public const string ComprasSearch = "PROC_RFID_ORIGEN_COMPRAS_SEARCH";
            public const string ComprasLineas = "PROC_RFID_ORIGEN_COMPRAS_LINEAS";
            public const string StockSearch = "PROC_RFID_ORIGEN_STOCK_SEARCH";
        }
    }
    public static class SCHEMARrhh
    {
        public static class CargosAdministrativosCompetencias
        {
            public const string Showall = "PROC_RRHH_CARGOS_ADMINISTRATIVOS_COMPETENCIAS_SHOWALL";
            public const string Showbyid = "PROC_RRHH_CARGOS_ADMINISTRATIVOS_COMPETENCIAS_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_CARGOS_ADMINISTRATIVOS_COMPETENCIAS_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_CARGOS_ADMINISTRATIVOS_COMPETENCIAS_SEARCH";
            public const string Insert = "PROC_RRHH_CARGOS_ADMINISTRATIVOS_COMPETENCIAS_INSERT";
            public const string Update = "PROC_RRHH_CARGOS_ADMINISTRATIVOS_COMPETENCIAS_UPDATE";
            public const string Delete = "PROC_RRHH_CARGOS_ADMINISTRATIVOS_COMPETENCIAS_DELETE";
        }
        public static class CargosFunciones
        {
            public const string Showall = "PROC_RRHH_CARGOS_FUNCIONES_SHOWALL";
            public const string Showbyid = "PROC_RRHH_CARGOS_FUNCIONES_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_CARGOS_FUNCIONES_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_CARGOS_FUNCIONES_SEARCH";
            public const string Insert = "PROC_RRHH_CARGOS_FUNCIONES_INSERT";
            public const string Update = "PROC_RRHH_CARGOS_FUNCIONES_UPDATE";
            public const string Delete = "PROC_RRHH_CARGOS_FUNCIONES_DELETE";
        }
        public static class CargosImplementos
        {
            public const string Showall = "PROC_RRHH_CARGOS_IMPLEMENTOS_SHOWALL";
            public const string Showbyid = "PROC_RRHH_CARGOS_IMPLEMENTOS_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_CARGOS_IMPLEMENTOS_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_CARGOS_IMPLEMENTOS_SEARCH";
            public const string Insert = "PROC_RRHH_CARGOS_IMPLEMENTOS_INSERT";
            public const string Update = "PROC_RRHH_CARGOS_IMPLEMENTOS_UPDATE";
            public const string Delete = "PROC_RRHH_CARGOS_IMPLEMENTOS_DELETE";
        }
        public static class CarrerasAcademicas
        {
            public const string Showall = "PROC_RRHH_CARRERAS_ACADEMICAS_SHOWALL";
            public const string Showbyid = "PROC_RRHH_CARRERAS_ACADEMICAS_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_CARRERAS_ACADEMICAS_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_CARRERAS_ACADEMICAS_SEARCH";
            public const string Insert = "PROC_RRHH_CARRERAS_ACADEMICAS_INSERT";
            public const string Update = "PROC_RRHH_CARRERAS_ACADEMICAS_UPDATE";
            public const string Delete = "PROC_RRHH_CARRERAS_ACADEMICAS_DELETE";
        }
        public static class Competencias
        {
            public const string Showall = "PROC_RRHH_COMPETENCIAS_SHOWALL";
            public const string Showbyid = "PROC_RRHH_COMPETENCIAS_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_COMPETENCIAS_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_COMPETENCIAS_SEARCH";
            public const string Insert = "PROC_RRHH_COMPETENCIAS_INSERT";
            public const string Update = "PROC_RRHH_COMPETENCIAS_UPDATE";
            public const string Delete = "PROC_RRHH_COMPETENCIAS_DELETE";
        }
        public static class ConfiguracionRrhhNomina
        {
            public const string Showall = "PROC_RRHH_CONFIGURACION_RRHH_NOMINA_SHOWALL";
            public const string Showbyid = "PROC_RRHH_CONFIGURACION_RRHH_NOMINA_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_CONFIGURACION_RRHH_NOMINA_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_CONFIGURACION_RRHH_NOMINA_SEARCH";
            public const string Insert = "PROC_RRHH_CONFIGURACION_RRHH_NOMINA_INSERT";
            public const string Update = "PROC_RRHH_CONFIGURACION_RRHH_NOMINA_UPDATE";
            public const string Delete = "PROC_RRHH_CONFIGURACION_RRHH_NOMINA_DELETE";
        }
        public static class ContratosModelo
        {
            public const string Showall = "PROC_RRHH_CONTRATOS_MODELO_SHOWALL";
            public const string Showbyid = "PROC_RRHH_CONTRATOS_MODELO_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_CONTRATOS_MODELO_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_CONTRATOS_MODELO_SEARCH";
            public const string Insert = "PROC_RRHH_CONTRATOS_MODELO_INSERT";
            public const string Update = "PROC_RRHH_CONTRATOS_MODELO_UPDATE";
            public const string Delete = "PROC_RRHH_CONTRATOS_MODELO_DELETE";
        }
        public static class DerechoHabientes
        {
            public const string Showall = "PROC_RRHH_DERECHO_HABIENTES_SHOWALL";
            public const string Showbyid = "PROC_RRHH_DERECHO_HABIENTES_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_DERECHO_HABIENTES_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_DERECHO_HABIENTES_SEARCH";
            public const string Insert = "PROC_RRHH_DERECHO_HABIENTES_INSERT";
            public const string Update = "PROC_RRHH_DERECHO_HABIENTES_UPDATE";
            public const string Delete = "PROC_RRHH_DERECHO_HABIENTES_DELETE";
        }
        public static class DocumentosOficiales
        {
            public const string Showall = "PROC_RRHH_DOCUMENTOS_OFICIALES_SHOWALL";
            public const string Showbyid = "PROC_RRHH_DOCUMENTOS_OFICIALES_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_DOCUMENTOS_OFICIALES_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_DOCUMENTOS_OFICIALES_SEARCH";
            public const string Insert = "PROC_RRHH_DOCUMENTOS_OFICIALES_INSERT";
            public const string Update = "PROC_RRHH_DOCUMENTOS_OFICIALES_UPDATE";
            public const string Delete = "PROC_RRHH_DOCUMENTOS_OFICIALES_DELETE";
        }
        public static class DocumentosOficialesPersonal
        {
            public const string Showall = "PROC_RRHH_DOCUMENTOS_OFICIALES_PERSONAL_SHOWALL";
            public const string Showbyid = "PROC_RRHH_DOCUMENTOS_OFICIALES_PERSONAL_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_DOCUMENTOS_OFICIALES_PERSONAL_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_DOCUMENTOS_OFICIALES_PERSONAL_SEARCH";
            public const string Insert = "PROC_RRHH_DOCUMENTOS_OFICIALES_PERSONAL_INSERT";
            public const string Update = "PROC_RRHH_DOCUMENTOS_OFICIALES_PERSONAL_UPDATE";
            public const string Delete = "PROC_RRHH_DOCUMENTOS_OFICIALES_PERSONAL_DELETE";
        }
        public static class Encuestas
        {
            public const string Showall = "PROC_RRHH_ENCUESTAS_SHOWALL";
            public const string Showbyid = "PROC_RRHH_ENCUESTAS_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_ENCUESTAS_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_ENCUESTAS_SEARCH";
            public const string Insert = "PROC_RRHH_ENCUESTAS_INSERT";
            public const string Update = "PROC_RRHH_ENCUESTAS_UPDATE";
            public const string Delete = "PROC_RRHH_ENCUESTAS_DELETE";
        }
        public static class EncuestasAreas
        {
            public const string Showall = "PROC_RRHH_ENCUESTAS_AREAS_SHOWALL";
            public const string Showbyid = "PROC_RRHH_ENCUESTAS_AREAS_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_ENCUESTAS_AREAS_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_ENCUESTAS_AREAS_SEARCH";
            public const string Insert = "PROC_RRHH_ENCUESTAS_AREAS_INSERT";
            public const string Delete = "PROC_RRHH_ENCUESTAS_AREAS_DELETE";
        }
        public static class EncuestasPersonal
        {
            public const string Showall = "PROC_RRHH_ENCUESTAS_PERSONAL_SHOWALL";
            public const string Showbyid = "PROC_RRHH_ENCUESTAS_PERSONAL_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_ENCUESTAS_PERSONAL_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_ENCUESTAS_PERSONAL_SEARCH";
            public const string Insert = "PROC_RRHH_ENCUESTAS_PERSONAL_INSERT";
            public const string Update = "PROC_RRHH_ENCUESTAS_PERSONAL_UPDATE";
            public const string Delete = "PROC_RRHH_ENCUESTAS_PERSONAL_DELETE";
        }
        public static class EncuestasPersonalRespuestas
        {
            public const string Showall = "PROC_RRHH_ENCUESTAS_PERSONAL_RESPUESTAS_SHOWALL";
            public const string Showbyid = "PROC_RRHH_ENCUESTAS_PERSONAL_RESPUESTAS_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_ENCUESTAS_PERSONAL_RESPUESTAS_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_ENCUESTAS_PERSONAL_RESPUESTAS_SEARCH";
            public const string Insert = "PROC_RRHH_ENCUESTAS_PERSONAL_RESPUESTAS_INSERT";
            public const string Update = "PROC_RRHH_ENCUESTAS_PERSONAL_RESPUESTAS_UPDATE";
            public const string Delete = "PROC_RRHH_ENCUESTAS_PERSONAL_RESPUESTAS_DELETE";
        }
        public static class EncuestasPreguntas
        {
            public const string Showall = "PROC_RRHH_ENCUESTAS_PREGUNTAS_SHOWALL";
            public const string Showbyid = "PROC_RRHH_ENCUESTAS_PREGUNTAS_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_ENCUESTAS_PREGUNTAS_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_ENCUESTAS_PREGUNTAS_SEARCH";
            public const string Insert = "PROC_RRHH_ENCUESTAS_PREGUNTAS_INSERT";
            public const string Update = "PROC_RRHH_ENCUESTAS_PREGUNTAS_UPDATE";
            public const string Delete = "PROC_RRHH_ENCUESTAS_PREGUNTAS_DELETE";
        }
        public static class EncuestasPreguntasRespuestas
        {
            public const string Showall = "PROC_RRHH_ENCUESTAS_PREGUNTAS_RESPUESTAS_SHOWALL";
            public const string Showbyid = "PROC_RRHH_ENCUESTAS_PREGUNTAS_RESPUESTAS_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_ENCUESTAS_PREGUNTAS_RESPUESTAS_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_ENCUESTAS_PREGUNTAS_RESPUESTAS_SEARCH";
            public const string Insert = "PROC_RRHH_ENCUESTAS_PREGUNTAS_RESPUESTAS_INSERT";
            public const string Update = "PROC_RRHH_ENCUESTAS_PREGUNTAS_RESPUESTAS_UPDATE";
            public const string Delete = "PROC_RRHH_ENCUESTAS_PREGUNTAS_RESPUESTAS_DELETE";
        }
        public static class EstadosCiviles
        {
            public const string Showall = "PROC_RRHH_ESTADOS_CIVILES_SHOWALL";
            public const string Showbyid = "PROC_RRHH_ESTADOS_CIVILES_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_ESTADOS_CIVILES_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_ESTADOS_CIVILES_SEARCH";
            public const string Insert = "PROC_RRHH_ESTADOS_CIVILES_INSERT";
            public const string Update = "PROC_RRHH_ESTADOS_CIVILES_UPDATE";
            public const string Delete = "PROC_RRHH_ESTADOS_CIVILES_DELETE";
        }
        public static class EvaluacionesDesempeno
        {
            public const string Showall = "PROC_RRHH_EVALUACIONES_DESEMPENO_SHOWALL";
            public const string Showbyid = "PROC_RRHH_EVALUACIONES_DESEMPENO_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_EVALUACIONES_DESEMPENO_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_EVALUACIONES_DESEMPENO_SEARCH";
            public const string Insert = "PROC_RRHH_EVALUACIONES_DESEMPENO_INSERT";
            public const string Update = "PROC_RRHH_EVALUACIONES_DESEMPENO_UPDATE";
            public const string Delete = "PROC_RRHH_EVALUACIONES_DESEMPENO_DELETE";
        }
        public static class EvaluacionesDesempenoCompetencias
        {
            public const string Showall = "PROC_RRHH_EVALUACIONES_DESEMPENO_COMPETENCIAS_SHOWALL";
            public const string Showbyid = "PROC_RRHH_EVALUACIONES_DESEMPENO_COMPETENCIAS_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_EVALUACIONES_DESEMPENO_COMPETENCIAS_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_EVALUACIONES_DESEMPENO_COMPETENCIAS_SEARCH";
            public const string Insert = "PROC_RRHH_EVALUACIONES_DESEMPENO_COMPETENCIAS_INSERT";
            public const string Update = "PROC_RRHH_EVALUACIONES_DESEMPENO_COMPETENCIAS_UPDATE";
            public const string Delete = "PROC_RRHH_EVALUACIONES_DESEMPENO_COMPETENCIAS_DELETE";
        }
        public static class EvaluacionesDesempenoMetricas
        {
            public const string Showall = "PROC_RRHH_EVALUACIONES_DESEMPENO_METRICAS_SHOWALL";
            public const string Showbyid = "PROC_RRHH_EVALUACIONES_DESEMPENO_METRICAS_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_EVALUACIONES_DESEMPENO_METRICAS_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_EVALUACIONES_DESEMPENO_METRICAS_SEARCH";
            public const string Insert = "PROC_RRHH_EVALUACIONES_DESEMPENO_METRICAS_INSERT";
            public const string Update = "PROC_RRHH_EVALUACIONES_DESEMPENO_METRICAS_UPDATE";
            public const string Delete = "PROC_RRHH_EVALUACIONES_DESEMPENO_METRICAS_DELETE";
        }
        public static class EvaluacionesDesempenoPersonal
        {
            public const string Showall = "PROC_RRHH_EVALUACIONES_DESEMPENO_PERSONAL_SHOWALL";
            public const string Showbyid = "PROC_RRHH_EVALUACIONES_DESEMPENO_PERSONAL_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_EVALUACIONES_DESEMPENO_PERSONAL_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_EVALUACIONES_DESEMPENO_PERSONAL_SEARCH";
            public const string Insert = "PROC_RRHH_EVALUACIONES_DESEMPENO_PERSONAL_INSERT";
            public const string Update = "PROC_RRHH_EVALUACIONES_DESEMPENO_PERSONAL_UPDATE";
            public const string Delete = "PROC_RRHH_EVALUACIONES_DESEMPENO_PERSONAL_DELETE";
        }
        public static class EvaluacionesDesempenoPersonalEvaluacion
        {
            public const string Showall = "PROC_RRHH_EVALUACIONES_DESEMPENO_PERSONAL_EVALUACION_SHOWALL";
            public const string Showbyid = "PROC_RRHH_EVALUACIONES_DESEMPENO_PERSONAL_EVALUACION_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_EVALUACIONES_DESEMPENO_PERSONAL_EVALUACION_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_EVALUACIONES_DESEMPENO_PERSONAL_EVALUACION_SEARCH";
            public const string Insert = "PROC_RRHH_EVALUACIONES_DESEMPENO_PERSONAL_EVALUACION_INSERT";
            public const string Update = "PROC_RRHH_EVALUACIONES_DESEMPENO_PERSONAL_EVALUACION_UPDATE";
            public const string Delete = "PROC_RRHH_EVALUACIONES_DESEMPENO_PERSONAL_EVALUACION_DELETE";
        }
        public static class EvaluacionesDesempenoTiposCompetencias
        {
            public const string Showall = "PROC_RRHH_EVALUACIONES_DESEMPENO_TIPOS_COMPETENCIAS_SHOWALL";
            public const string Showbyid = "PROC_RRHH_EVALUACIONES_DESEMPENO_TIPOS_COMPETENCIAS_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_EVALUACIONES_DESEMPENO_TIPOS_COMPETENCIAS_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_EVALUACIONES_DESEMPENO_TIPOS_COMPETENCIAS_SEARCH";
            public const string Insert = "PROC_RRHH_EVALUACIONES_DESEMPENO_TIPOS_COMPETENCIAS_INSERT";
            public const string Update = "PROC_RRHH_EVALUACIONES_DESEMPENO_TIPOS_COMPETENCIAS_UPDATE";
            public const string Delete = "PROC_RRHH_EVALUACIONES_DESEMPENO_TIPOS_COMPETENCIAS_DELETE";
        }
        public static class GradosConfidenciabilidad
        {
            public const string Showall = "PROC_RRHH_GRADOS_CONFIDENCIABILIDAD_SHOWALL";
            public const string Showbyid = "PROC_RRHH_GRADOS_CONFIDENCIABILIDAD_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_GRADOS_CONFIDENCIABILIDAD_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_GRADOS_CONFIDENCIABILIDAD_SEARCH";
            public const string Insert = "PROC_RRHH_GRADOS_CONFIDENCIABILIDAD_INSERT";
            public const string Update = "PROC_RRHH_GRADOS_CONFIDENCIABILIDAD_UPDATE";
            public const string Delete = "PROC_RRHH_GRADOS_CONFIDENCIABILIDAD_DELETE";
        }
        public static class ManualesFunciones
        {
            public const string Showall = "PROC_RRHH_MANUALES_FUNCIONES_SHOWALL";
            public const string Showbyid = "PROC_RRHH_MANUALES_FUNCIONES_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_MANUALES_FUNCIONES_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_MANUALES_FUNCIONES_SEARCH";
            public const string Insert = "PROC_RRHH_MANUALES_FUNCIONES_INSERT";
            public const string Update = "PROC_RRHH_MANUALES_FUNCIONES_UPDATE";
            public const string Delete = "PROC_RRHH_MANUALES_FUNCIONES_DELETE";
        }
        public static class NivelesEvaluacion
        {
            public const string Showall = "PROC_RRHH_NIVELES_EVALUACION_SHOWALL";
            public const string Showbyid = "PROC_RRHH_NIVELES_EVALUACION_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_NIVELES_EVALUACION_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_NIVELES_EVALUACION_SEARCH";
            public const string Insert = "PROC_RRHH_NIVELES_EVALUACION_INSERT";
            public const string Update = "PROC_RRHH_NIVELES_EVALUACION_UPDATE";
            public const string Delete = "PROC_RRHH_NIVELES_EVALUACION_DELETE";
        }
        public static class NueveBoxDesempenoPotencial
        {
            public const string Showall = "PROC_RRHH_NUEVE_BOX_DESEMPENO_POTENCIAL_SHOWALL";
            public const string Showbyid = "PROC_RRHH_NUEVE_BOX_DESEMPENO_POTENCIAL_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_NUEVE_BOX_DESEMPENO_POTENCIAL_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_NUEVE_BOX_DESEMPENO_POTENCIAL_SEARCH";
            public const string Insert = "PROC_RRHH_NUEVE_BOX_DESEMPENO_POTENCIAL_INSERT";
            public const string Update = "PROC_RRHH_NUEVE_BOX_DESEMPENO_POTENCIAL_UPDATE";
            public const string Delete = "PROC_RRHH_NUEVE_BOX_DESEMPENO_POTENCIAL_DELETE";
        }
        public static class Personal
        {
            public const string Showall = "PROC_RRHH_PERSONAL_SHOWALL";
            public const string Showbyid = "PROC_RRHH_PERSONAL_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_PERSONAL_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_PERSONAL_SEARCH";
            public const string Insert = "PROC_RRHH_PERSONAL_INSERT";
            public const string Update = "PROC_RRHH_PERSONAL_UPDATE";
            public const string Delete = "PROC_RRHH_PERSONAL_DELETE";
        }
        public static class PersonalAfp
        {
            public const string Showall = "PROC_RRHH_PERSONAL_AFP_SHOWALL";
            public const string Showbyid = "PROC_RRHH_PERSONAL_AFP_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_PERSONAL_AFP_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_PERSONAL_AFP_SEARCH";
            public const string Insert = "PROC_RRHH_PERSONAL_AFP_INSERT";
            public const string Update = "PROC_RRHH_PERSONAL_AFP_UPDATE";
            public const string Delete = "PROC_RRHH_PERSONAL_AFP_DELETE";
        }
        public static class PersonalAfpConceptosRemunerativos
        {
            public const string Showall = "PROC_RRHH_PERSONAL_AFP_CONCEPTOS_REMUNERATIVOS_SHOWALL";
            public const string Showbyid = "PROC_RRHH_PERSONAL_AFP_CONCEPTOS_REMUNERATIVOS_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_PERSONAL_AFP_CONCEPTOS_REMUNERATIVOS_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_PERSONAL_AFP_CONCEPTOS_REMUNERATIVOS_SEARCH";
            public const string Insert = "PROC_RRHH_PERSONAL_AFP_CONCEPTOS_REMUNERATIVOS_INSERT";
            public const string Update = "PROC_RRHH_PERSONAL_AFP_CONCEPTOS_REMUNERATIVOS_UPDATE";
            public const string Delete = "PROC_RRHH_PERSONAL_AFP_CONCEPTOS_REMUNERATIVOS_DELETE";
        }
        public static class PersonalAsistencia
        {
            public const string Showall = "PROC_RRHH_PERSONAL_ASISTENCIA_SHOWALL";
            public const string Showbyid = "PROC_RRHH_PERSONAL_ASISTENCIA_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_PERSONAL_ASISTENCIA_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_PERSONAL_ASISTENCIA_SEARCH";
            public const string Insert = "PROC_RRHH_PERSONAL_ASISTENCIA_INSERT";
            public const string Update = "PROC_RRHH_PERSONAL_ASISTENCIA_UPDATE";
            public const string Delete = "PROC_RRHH_PERSONAL_ASISTENCIA_DELETE";
        }
        public static class PersonalBeneficiarios
        {
            public const string Showall = "PROC_RRHH_PERSONAL_BENEFICIARIOS_SHOWALL";
            public const string Showbyid = "PROC_RRHH_PERSONAL_BENEFICIARIOS_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_PERSONAL_BENEFICIARIOS_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_PERSONAL_BENEFICIARIOS_SEARCH";
            public const string Insert = "PROC_RRHH_PERSONAL_BENEFICIARIOS_INSERT";
            public const string Update = "PROC_RRHH_PERSONAL_BENEFICIARIOS_UPDATE";
            public const string Delete = "PROC_RRHH_PERSONAL_BENEFICIARIOS_DELETE";
        }
        public static class PersonalCargo
        {
            public const string Showall = "PROC_RRHH_PERSONAL_CARGO_SHOWALL";
            public const string Showbyid = "PROC_RRHH_PERSONAL_CARGO_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_PERSONAL_CARGO_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_PERSONAL_CARGO_SEARCH";
            public const string Insert = "PROC_RRHH_PERSONAL_CARGO_INSERT";
            public const string Update = "PROC_RRHH_PERSONAL_CARGO_UPDATE";
            public const string Delete = "PROC_RRHH_PERSONAL_CARGO_DELETE";
        }
        public static class PersonalCompetencias
        {
            public const string Showall = "PROC_RRHH_PERSONAL_COMPETENCIAS_SHOWALL";
            public const string Showbyid = "PROC_RRHH_PERSONAL_COMPETENCIAS_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_PERSONAL_COMPETENCIAS_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_PERSONAL_COMPETENCIAS_SEARCH";
            public const string Insert = "PROC_RRHH_PERSONAL_COMPETENCIAS_INSERT";
            public const string Update = "PROC_RRHH_PERSONAL_COMPETENCIAS_UPDATE";
            public const string Delete = "PROC_RRHH_PERSONAL_COMPETENCIAS_DELETE";
        }
        public static class PersonalConceptosRemunerativos
        {
            public const string Showall = "PROC_RRHH_PERSONAL_CONCEPTOS_REMUNERATIVOS_SHOWALL";
            public const string Showbyid = "PROC_RRHH_PERSONAL_CONCEPTOS_REMUNERATIVOS_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_PERSONAL_CONCEPTOS_REMUNERATIVOS_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_PERSONAL_CONCEPTOS_REMUNERATIVOS_SEARCH";
            public const string Insert = "PROC_RRHH_PERSONAL_CONCEPTOS_REMUNERATIVOS_INSERT";
            public const string Update = "PROC_RRHH_PERSONAL_CONCEPTOS_REMUNERATIVOS_UPDATE";
            public const string Delete = "PROC_RRHH_PERSONAL_CONCEPTOS_REMUNERATIVOS_DELETE";
        }
        public static class PersonalConceptosRemunerativosHistorico
        {
            public const string Showall = "PROC_RRHH_PERSONAL_CONCEPTOS_REMUNERATIVOS_HISTORICO_SHOWALL";
            public const string Showbyid = "PROC_RRHH_PERSONAL_CONCEPTOS_REMUNERATIVOS_HISTORICO_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_PERSONAL_CONCEPTOS_REMUNERATIVOS_HISTORICO_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_PERSONAL_CONCEPTOS_REMUNERATIVOS_HISTORICO_SEARCH";
            public const string Insert = "PROC_RRHH_PERSONAL_CONCEPTOS_REMUNERATIVOS_HISTORICO_INSERT";
            public const string Update = "PROC_RRHH_PERSONAL_CONCEPTOS_REMUNERATIVOS_HISTORICO_UPDATE";
            public const string Delete = "PROC_RRHH_PERSONAL_CONCEPTOS_REMUNERATIVOS_HISTORICO_DELETE";
        }
        public static class PersonalContactos
        {
            public const string Showall = "PROC_RRHH_PERSONAL_CONTACTOS_SHOWALL";
            public const string Showbyid = "PROC_RRHH_PERSONAL_CONTACTOS_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_PERSONAL_CONTACTOS_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_PERSONAL_CONTACTOS_SEARCH";
            public const string Insert = "PROC_RRHH_PERSONAL_CONTACTOS_INSERT";
            public const string Update = "PROC_RRHH_PERSONAL_CONTACTOS_UPDATE";
            public const string Delete = "PROC_RRHH_PERSONAL_CONTACTOS_DELETE";
        }
        public static class PersonalContratos
        {
            public const string Showall = "PROC_RRHH_PERSONAL_CONTRATOS_SHOWALL";
            public const string Showbyid = "PROC_RRHH_PERSONAL_CONTRATOS_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_PERSONAL_CONTRATOS_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_PERSONAL_CONTRATOS_SEARCH";
            public const string Insert = "PROC_RRHH_PERSONAL_CONTRATOS_INSERT";
            public const string Update = "PROC_RRHH_PERSONAL_CONTRATOS_UPDATE";
            public const string Delete = "PROC_RRHH_PERSONAL_CONTRATOS_DELETE";
        }
        public static class PersonalCuentaCorriente
        {
            public const string Showall = "PROC_RRHH_PERSONAL_CUENTA_CORRIENTE_SHOWALL";
            public const string Showbyid = "PROC_RRHH_PERSONAL_CUENTA_CORRIENTE_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_PERSONAL_CUENTA_CORRIENTE_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_PERSONAL_CUENTA_CORRIENTE_SEARCH";
            public const string Insert = "PROC_RRHH_PERSONAL_CUENTA_CORRIENTE_INSERT";
            public const string Update = "PROC_RRHH_PERSONAL_CUENTA_CORRIENTE_UPDATE";
            public const string Delete = "PROC_RRHH_PERSONAL_CUENTA_CORRIENTE_DELETE";
        }
        public static class PersonalCuentaCorrienteDetalle
        {
            public const string Showall = "PROC_RRHH_PERSONAL_CUENTA_CORRIENTE_DETALLE_SHOWALL";
            public const string Showbyid = "PROC_RRHH_PERSONAL_CUENTA_CORRIENTE_DETALLE_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_PERSONAL_CUENTA_CORRIENTE_DETALLE_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_PERSONAL_CUENTA_CORRIENTE_DETALLE_SEARCH";
            public const string Insert = "PROC_RRHH_PERSONAL_CUENTA_CORRIENTE_DETALLE_INSERT";
            public const string Update = "PROC_RRHH_PERSONAL_CUENTA_CORRIENTE_DETALLE_UPDATE";
            public const string Delete = "PROC_RRHH_PERSONAL_CUENTA_CORRIENTE_DETALLE_DELETE";
        }
        public static class PersonalExperienciaLaboral
        {
            public const string Showall = "PROC_RRHH_PERSONAL_EXPERIENCIA_LABORAL_SHOWALL";
            public const string Showbyid = "PROC_RRHH_PERSONAL_EXPERIENCIA_LABORAL_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_PERSONAL_EXPERIENCIA_LABORAL_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_PERSONAL_EXPERIENCIA_LABORAL_SEARCH";
            public const string Insert = "PROC_RRHH_PERSONAL_EXPERIENCIA_LABORAL_INSERT";
            public const string Update = "PROC_RRHH_PERSONAL_EXPERIENCIA_LABORAL_UPDATE";
            public const string Delete = "PROC_RRHH_PERSONAL_EXPERIENCIA_LABORAL_DELETE";
        }
        public static class PersonalFormacionAcademica
        {
            public const string Showall = "PROC_RRHH_PERSONAL_FORMACION_ACADEMICA_SHOWALL";
            public const string Showbyid = "PROC_RRHH_PERSONAL_FORMACION_ACADEMICA_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_PERSONAL_FORMACION_ACADEMICA_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_PERSONAL_FORMACION_ACADEMICA_SEARCH";
            public const string Insert = "PROC_RRHH_PERSONAL_FORMACION_ACADEMICA_INSERT";
            public const string Update = "PROC_RRHH_PERSONAL_FORMACION_ACADEMICA_UPDATE";
            public const string Delete = "PROC_RRHH_PERSONAL_FORMACION_ACADEMICA_DELETE";
        }
        public static class PersonalImplementos
        {
            public const string Showall = "PROC_RRHH_PERSONAL_IMPLEMENTOS_SHOWALL";
            public const string Showbyid = "PROC_RRHH_PERSONAL_IMPLEMENTOS_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_PERSONAL_IMPLEMENTOS_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_PERSONAL_IMPLEMENTOS_SEARCH";
            public const string Insert = "PROC_RRHH_PERSONAL_IMPLEMENTOS_INSERT";
            public const string Update = "PROC_RRHH_PERSONAL_IMPLEMENTOS_UPDATE";
            public const string Delete = "PROC_RRHH_PERSONAL_IMPLEMENTOS_DELETE";
        }
        public static class ProgramacionEntregaImplementos
        {
            public const string Showall = "PROC_RRHH_PROGRAMACION_ENTREGA_IMPLEMENTOS_SHOWALL";
            public const string Showbyid = "PROC_RRHH_PROGRAMACION_ENTREGA_IMPLEMENTOS_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_PROGRAMACION_ENTREGA_IMPLEMENTOS_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_PROGRAMACION_ENTREGA_IMPLEMENTOS_SEARCH";
            public const string Insert = "PROC_RRHH_PROGRAMACION_ENTREGA_IMPLEMENTOS_INSERT";
            public const string Update = "PROC_RRHH_PROGRAMACION_ENTREGA_IMPLEMENTOS_UPDATE";
            public const string Delete = "PROC_RRHH_PROGRAMACION_ENTREGA_IMPLEMENTOS_DELETE";
        }
        public static class ProgramacionEntregaImplementosPersonal
        {
            public const string Showall = "PROC_RRHH_PROGRAMACION_ENTREGA_IMPLEMENTOS_PERSONAL_SHOWALL";
            public const string Showbyid = "PROC_RRHH_PROGRAMACION_ENTREGA_IMPLEMENTOS_PERSONAL_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_PROGRAMACION_ENTREGA_IMPLEMENTOS_PERSONAL_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_PROGRAMACION_ENTREGA_IMPLEMENTOS_PERSONAL_SEARCH";
            public const string Insert = "PROC_RRHH_PROGRAMACION_ENTREGA_IMPLEMENTOS_PERSONAL_INSERT";
            public const string Update = "PROC_RRHH_PROGRAMACION_ENTREGA_IMPLEMENTOS_PERSONAL_UPDATE";
            public const string Delete = "PROC_RRHH_PROGRAMACION_ENTREGA_IMPLEMENTOS_PERSONAL_DELETE";
        }
        public static class ProgramacionEntregaImplementosPersonalDetalle
        {
            public const string Showall = "PROC_RRHH_PROGRAMACION_ENTREGA_IMPLEMENTOS_PERSONAL_DETALLE_SHOWALL";
            public const string Showbyid = "PROC_RRHH_PROGRAMACION_ENTREGA_IMPLEMENTOS_PERSONAL_DETALLE_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_PROGRAMACION_ENTREGA_IMPLEMENTOS_PERSONAL_DETALLE_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_PROGRAMACION_ENTREGA_IMPLEMENTOS_PERSONAL_DETALLE_SEARCH";
            public const string Insert = "PROC_RRHH_PROGRAMACION_ENTREGA_IMPLEMENTOS_PERSONAL_DETALLE_INSERT";
            public const string Update = "PROC_RRHH_PROGRAMACION_ENTREGA_IMPLEMENTOS_PERSONAL_DETALLE_UPDATE";
            public const string Delete = "PROC_RRHH_PROGRAMACION_ENTREGA_IMPLEMENTOS_PERSONAL_DETALLE_DELETE";
        }
        public static class RankingAcademico
        {
            public const string Showall = "PROC_RRHH_RANKING_ACADEMICO_SHOWALL";
            public const string Showbyid = "PROC_RRHH_RANKING_ACADEMICO_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_RANKING_ACADEMICO_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_RANKING_ACADEMICO_SEARCH";
            public const string Insert = "PROC_RRHH_RANKING_ACADEMICO_INSERT";
            public const string Update = "PROC_RRHH_RANKING_ACADEMICO_UPDATE";
            public const string Delete = "PROC_RRHH_RANKING_ACADEMICO_DELETE";
        }
        public static class RegimenesLaborales
        {
            public const string Showall = "PROC_RRHH_REGIMENES_LABORALES_SHOWALL";
            public const string Showbyid = "PROC_RRHH_REGIMENES_LABORALES_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_REGIMENES_LABORALES_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_REGIMENES_LABORALES_SEARCH";
            public const string Insert = "PROC_RRHH_REGIMENES_LABORALES_INSERT";
            public const string Update = "PROC_RRHH_REGIMENES_LABORALES_UPDATE";
            public const string Delete = "PROC_RRHH_REGIMENES_LABORALES_DELETE";
        }
        public static class RutasArchivos
        {
            public const string Showall = "PROC_RRHH_RUTAS_ARCHIVOS_SHOWALL";
            public const string Showbyid = "PROC_RRHH_RUTAS_ARCHIVOS_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_RUTAS_ARCHIVOS_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_RUTAS_ARCHIVOS_SEARCH";
            public const string Insert = "PROC_RRHH_RUTAS_ARCHIVOS_INSERT";
            public const string Update = "PROC_RRHH_RUTAS_ARCHIVOS_UPDATE";
            public const string Delete = "PROC_RRHH_RUTAS_ARCHIVOS_DELETE";
        }
        public static class SolicitudesPersonal
        {
            public const string Showall = "PROC_RRHH_SOLICITUDES_PERSONAL_SHOWALL";
            public const string Showbyid = "PROC_RRHH_SOLICITUDES_PERSONAL_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_SOLICITUDES_PERSONAL_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_SOLICITUDES_PERSONAL_SEARCH";
            public const string Insert = "PROC_RRHH_SOLICITUDES_PERSONAL_INSERT";
            public const string Update = "PROC_RRHH_SOLICITUDES_PERSONAL_UPDATE";
            public const string Delete = "PROC_RRHH_SOLICITUDES_PERSONAL_DELETE";
        }
        public static class SolicitudesReclutamiento
        {
            public const string Showall = "PROC_RRHH_SOLICITUDES_RECLUTAMIENTO_SHOWALL";
            public const string Showbyid = "PROC_RRHH_SOLICITUDES_RECLUTAMIENTO_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_SOLICITUDES_RECLUTAMIENTO_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_SOLICITUDES_RECLUTAMIENTO_SEARCH";
            public const string Insert = "PROC_RRHH_SOLICITUDES_RECLUTAMIENTO_INSERT";
            public const string Update = "PROC_RRHH_SOLICITUDES_RECLUTAMIENTO_UPDATE";
            public const string Delete = "PROC_RRHH_SOLICITUDES_RECLUTAMIENTO_DELETE";
        }
        public static class SolicitudesReclutamientoCompetencias
        {
            public const string Showall = "PROC_RRHH_SOLICITUDES_RECLUTAMIENTO_COMPETENCIAS_SHOWALL";
            public const string Showbyid = "PROC_RRHH_SOLICITUDES_RECLUTAMIENTO_COMPETENCIAS_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_SOLICITUDES_RECLUTAMIENTO_COMPETENCIAS_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_SOLICITUDES_RECLUTAMIENTO_COMPETENCIAS_SEARCH";
            public const string Insert = "PROC_RRHH_SOLICITUDES_RECLUTAMIENTO_COMPETENCIAS_INSERT";
            public const string Update = "PROC_RRHH_SOLICITUDES_RECLUTAMIENTO_COMPETENCIAS_UPDATE";
            public const string Delete = "PROC_RRHH_SOLICITUDES_RECLUTAMIENTO_COMPETENCIAS_DELETE";
        }
        public static class SolicitudesReclutamientoFunciones
        {
            public const string Showall = "PROC_RRHH_SOLICITUDES_RECLUTAMIENTO_FUNCIONES_SHOWALL";
            public const string Showbyid = "PROC_RRHH_SOLICITUDES_RECLUTAMIENTO_FUNCIONES_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_SOLICITUDES_RECLUTAMIENTO_FUNCIONES_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_SOLICITUDES_RECLUTAMIENTO_FUNCIONES_SEARCH";
            public const string Insert = "PROC_RRHH_SOLICITUDES_RECLUTAMIENTO_FUNCIONES_INSERT";
            public const string Delete = "PROC_RRHH_SOLICITUDES_RECLUTAMIENTO_FUNCIONES_DELETE";
        }
        public static class SolicitudesReclutamientoPostulantes
        {
            public const string Showall = "PROC_RRHH_SOLICITUDES_RECLUTAMIENTO_POSTULANTES_SHOWALL";
            public const string Showbyid = "PROC_RRHH_SOLICITUDES_RECLUTAMIENTO_POSTULANTES_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_SOLICITUDES_RECLUTAMIENTO_POSTULANTES_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_SOLICITUDES_RECLUTAMIENTO_POSTULANTES_SEARCH";
            public const string Insert = "PROC_RRHH_SOLICITUDES_RECLUTAMIENTO_POSTULANTES_INSERT";
            public const string Update = "PROC_RRHH_SOLICITUDES_RECLUTAMIENTO_POSTULANTES_UPDATE";
            public const string Delete = "PROC_RRHH_SOLICITUDES_RECLUTAMIENTO_POSTULANTES_DELETE";
        }
        public static class SolicitudesReclutamientoPostulantesEvaluacionCompetencias
        {
            public const string Showall = "PROC_RRHH_SOLICITUDES_RECLUTAMIENTO_POSTULANTES_EVALUACION_COMPETENCIAS_SHOWALL";
            public const string Showbyid = "PROC_RRHH_SOLICITUDES_RECLUTAMIENTO_POSTULANTES_EVALUACION_COMPETENCIAS_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_SOLICITUDES_RECLUTAMIENTO_POSTULANTES_EVALUACION_COMPETENCIAS_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_SOLICITUDES_RECLUTAMIENTO_POSTULANTES_EVALUACION_COMPETENCIAS_SEARCH";
            public const string Insert = "PROC_RRHH_SOLICITUDES_RECLUTAMIENTO_POSTULANTES_EVALUACION_COMPETENCIAS_INSERT";
            public const string Update = "PROC_RRHH_SOLICITUDES_RECLUTAMIENTO_POSTULANTES_EVALUACION_COMPETENCIAS_UPDATE";
            public const string Delete = "PROC_RRHH_SOLICITUDES_RECLUTAMIENTO_POSTULANTES_EVALUACION_COMPETENCIAS_DELETE";
        }
        public static class SolicitudesReclutamientoPostulantesFormacionAcademica
        {
            public const string Showall = "PROC_RRHH_SOLICITUDES_RECLUTAMIENTO_POSTULANTES_FORMACION_ACADEMICA_SHOWALL";
            public const string Showbyid = "PROC_RRHH_SOLICITUDES_RECLUTAMIENTO_POSTULANTES_FORMACION_ACADEMICA_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_SOLICITUDES_RECLUTAMIENTO_POSTULANTES_FORMACION_ACADEMICA_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_SOLICITUDES_RECLUTAMIENTO_POSTULANTES_FORMACION_ACADEMICA_SEARCH";
            public const string Insert = "PROC_RRHH_SOLICITUDES_RECLUTAMIENTO_POSTULANTES_FORMACION_ACADEMICA_INSERT";
            public const string Update = "PROC_RRHH_SOLICITUDES_RECLUTAMIENTO_POSTULANTES_FORMACION_ACADEMICA_UPDATE";
            public const string Delete = "PROC_RRHH_SOLICITUDES_RECLUTAMIENTO_POSTULANTES_FORMACION_ACADEMICA_DELETE";
        }
        public static class SolicitudesReclutamientoPostulantesTiposEvaluacion
        {
            public const string Showall = "PROC_RRHH_SOLICITUDES_RECLUTAMIENTO_POSTULANTES_TIPOS_EVALUACION_SHOWALL";
            public const string Showbyid = "PROC_RRHH_SOLICITUDES_RECLUTAMIENTO_POSTULANTES_TIPOS_EVALUACION_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_SOLICITUDES_RECLUTAMIENTO_POSTULANTES_TIPOS_EVALUACION_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_SOLICITUDES_RECLUTAMIENTO_POSTULANTES_TIPOS_EVALUACION_SEARCH";
            public const string Insert = "PROC_RRHH_SOLICITUDES_RECLUTAMIENTO_POSTULANTES_TIPOS_EVALUACION_INSERT";
            public const string Update = "PROC_RRHH_SOLICITUDES_RECLUTAMIENTO_POSTULANTES_TIPOS_EVALUACION_UPDATE";
            public const string Delete = "PROC_RRHH_SOLICITUDES_RECLUTAMIENTO_POSTULANTES_TIPOS_EVALUACION_DELETE";
        }
        public static class SolicitudesReclutamientoTiposEvaluacion
        {
            public const string Showall = "PROC_RRHH_SOLICITUDES_RECLUTAMIENTO_TIPOS_EVALUACION_SHOWALL";
            public const string Showbyid = "PROC_RRHH_SOLICITUDES_RECLUTAMIENTO_TIPOS_EVALUACION_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_SOLICITUDES_RECLUTAMIENTO_TIPOS_EVALUACION_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_SOLICITUDES_RECLUTAMIENTO_TIPOS_EVALUACION_SEARCH";
            public const string Insert = "PROC_RRHH_SOLICITUDES_RECLUTAMIENTO_TIPOS_EVALUACION_INSERT";
            public const string Update = "PROC_RRHH_SOLICITUDES_RECLUTAMIENTO_TIPOS_EVALUACION_UPDATE";
            public const string Delete = "PROC_RRHH_SOLICITUDES_RECLUTAMIENTO_TIPOS_EVALUACION_DELETE";
        }
        public static class TiposCarreraAcademica
        {
            public const string Showall = "PROC_RRHH_TIPOS_CARRERA_ACADEMICA_SHOWALL";
            public const string Showbyid = "PROC_RRHH_TIPOS_CARRERA_ACADEMICA_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_TIPOS_CARRERA_ACADEMICA_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_TIPOS_CARRERA_ACADEMICA_SEARCH";
            public const string Insert = "PROC_RRHH_TIPOS_CARRERA_ACADEMICA_INSERT";
            public const string Update = "PROC_RRHH_TIPOS_CARRERA_ACADEMICA_UPDATE";
            public const string Delete = "PROC_RRHH_TIPOS_CARRERA_ACADEMICA_DELETE";
        }
        public static class TiposCompetencia
        {
            public const string Showall = "PROC_RRHH_TIPOS_COMPETENCIA_SHOWALL";
            public const string Showbyid = "PROC_RRHH_TIPOS_COMPETENCIA_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_TIPOS_COMPETENCIA_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_TIPOS_COMPETENCIA_SEARCH";
            public const string Insert = "PROC_RRHH_TIPOS_COMPETENCIA_INSERT";
            public const string Update = "PROC_RRHH_TIPOS_COMPETENCIA_UPDATE";
            public const string Delete = "PROC_RRHH_TIPOS_COMPETENCIA_DELETE";
        }
        public static class TiposContrato
        {
            public const string Showall = "PROC_RRHH_TIPOS_CONTRATO_SHOWALL";
            public const string Showbyid = "PROC_RRHH_TIPOS_CONTRATO_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_TIPOS_CONTRATO_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_TIPOS_CONTRATO_SEARCH";
            public const string Insert = "PROC_RRHH_TIPOS_CONTRATO_INSERT";
            public const string Update = "PROC_RRHH_TIPOS_CONTRATO_UPDATE";
            public const string Delete = "PROC_RRHH_TIPOS_CONTRATO_DELETE";
        }
        public static class TiposContratoBeneficiosLaborales
        {
            public const string Showall = "PROC_RRHH_TIPOS_CONTRATO_BENEFICIOS_LABORALES_SHOWALL";
            public const string Showbyid = "PROC_RRHH_TIPOS_CONTRATO_BENEFICIOS_LABORALES_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_TIPOS_CONTRATO_BENEFICIOS_LABORALES_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_TIPOS_CONTRATO_BENEFICIOS_LABORALES_SEARCH";
            public const string Insert = "PROC_RRHH_TIPOS_CONTRATO_BENEFICIOS_LABORALES_INSERT";
            public const string Delete = "PROC_RRHH_TIPOS_CONTRATO_BENEFICIOS_LABORALES_DELETE";
        }
        public static class TiposDocumentoOficial
        {
            public const string Showall = "PROC_RRHH_TIPOS_DOCUMENTO_OFICIAL_SHOWALL";
            public const string Showbyid = "PROC_RRHH_TIPOS_DOCUMENTO_OFICIAL_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_TIPOS_DOCUMENTO_OFICIAL_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_TIPOS_DOCUMENTO_OFICIAL_SEARCH";
            public const string Insert = "PROC_RRHH_TIPOS_DOCUMENTO_OFICIAL_INSERT";
            public const string Update = "PROC_RRHH_TIPOS_DOCUMENTO_OFICIAL_UPDATE";
            public const string Delete = "PROC_RRHH_TIPOS_DOCUMENTO_OFICIAL_DELETE";
        }
        public static class TiposDocumentoOficialFormatos
        {
            public const string Showall = "PROC_RRHH_TIPOS_DOCUMENTO_OFICIAL_FORMATOS_SHOWALL";
            public const string Showbyid = "PROC_RRHH_TIPOS_DOCUMENTO_OFICIAL_FORMATOS_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_TIPOS_DOCUMENTO_OFICIAL_FORMATOS_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_TIPOS_DOCUMENTO_OFICIAL_FORMATOS_SEARCH";
            public const string Insert = "PROC_RRHH_TIPOS_DOCUMENTO_OFICIAL_FORMATOS_INSERT";
            public const string Update = "PROC_RRHH_TIPOS_DOCUMENTO_OFICIAL_FORMATOS_UPDATE";
            public const string Delete = "PROC_RRHH_TIPOS_DOCUMENTO_OFICIAL_FORMATOS_DELETE";
        }
        public static class TiposEvaluacion
        {
            public const string Showall = "PROC_RRHH_TIPOS_EVALUACION_SHOWALL";
            public const string Showbyid = "PROC_RRHH_TIPOS_EVALUACION_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_TIPOS_EVALUACION_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_TIPOS_EVALUACION_SEARCH";
            public const string Insert = "PROC_RRHH_TIPOS_EVALUACION_INSERT";
            public const string Update = "PROC_RRHH_TIPOS_EVALUACION_UPDATE";
            public const string Delete = "PROC_RRHH_TIPOS_EVALUACION_DELETE";
        }
        public static class TiposImplementoLaboral
        {
            public const string Showall = "PROC_RRHH_TIPOS_IMPLEMENTO_LABORAL_SHOWALL";
            public const string Showbyid = "PROC_RRHH_TIPOS_IMPLEMENTO_LABORAL_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_TIPOS_IMPLEMENTO_LABORAL_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_TIPOS_IMPLEMENTO_LABORAL_SEARCH";
            public const string Insert = "PROC_RRHH_TIPOS_IMPLEMENTO_LABORAL_INSERT";
            public const string Update = "PROC_RRHH_TIPOS_IMPLEMENTO_LABORAL_UPDATE";
            public const string Delete = "PROC_RRHH_TIPOS_IMPLEMENTO_LABORAL_DELETE";
        }
        public static class TiposParentesco
        {
            public const string Showall = "PROC_RRHH_TIPOS_PARENTESCO_SHOWALL";
            public const string Showbyid = "PROC_RRHH_TIPOS_PARENTESCO_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_TIPOS_PARENTESCO_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_TIPOS_PARENTESCO_SEARCH";
            public const string Insert = "PROC_RRHH_TIPOS_PARENTESCO_INSERT";
            public const string Update = "PROC_RRHH_TIPOS_PARENTESCO_UPDATE";
            public const string Delete = "PROC_RRHH_TIPOS_PARENTESCO_DELETE";
        }
        public static class TiposPersonal
        {
            public const string Showall = "PROC_RRHH_TIPOS_PERSONAL_SHOWALL";
            public const string Showbyid = "PROC_RRHH_TIPOS_PERSONAL_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_TIPOS_PERSONAL_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_TIPOS_PERSONAL_SEARCH";
            public const string Insert = "PROC_RRHH_TIPOS_PERSONAL_INSERT";
            public const string Update = "PROC_RRHH_TIPOS_PERSONAL_UPDATE";
            public const string Delete = "PROC_RRHH_TIPOS_PERSONAL_DELETE";
        }
        public static class TiposZona
        {
            public const string Showall = "PROC_RRHH_TIPOS_ZONA_SHOWALL";
            public const string Showbyid = "PROC_RRHH_TIPOS_ZONA_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_TIPOS_ZONA_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_TIPOS_ZONA_SEARCH";
            public const string Insert = "PROC_RRHH_TIPOS_ZONA_INSERT";
            public const string Update = "PROC_RRHH_TIPOS_ZONA_UPDATE";
            public const string Delete = "PROC_RRHH_TIPOS_ZONA_DELETE";
        }
        public static class TitulosAcademicos
        {
            public const string Showall = "PROC_RRHH_TITULOS_ACADEMICOS_SHOWALL";
            public const string Showbyid = "PROC_RRHH_TITULOS_ACADEMICOS_SHOWBYID";
            public const string Showcombox = "PROC_RRHH_TITULOS_ACADEMICOS_SHOWCOMBOX";
            public const string Search = "PROC_RRHH_TITULOS_ACADEMICOS_SEARCH";
            public const string Insert = "PROC_RRHH_TITULOS_ACADEMICOS_INSERT";
            public const string Update = "PROC_RRHH_TITULOS_ACADEMICOS_UPDATE";
            public const string Delete = "PROC_RRHH_TITULOS_ACADEMICOS_DELETE";
        }
    }
    public static class SCHEMASafety
    {
        public static class Accesos
        {
            public const string Showall = "PROC_SAFETY_ACCESOS_SHOWALL";
            public const string Showbyid = "PROC_SAFETY_ACCESOS_SHOWBYID";
            public const string Showcombox = "PROC_SAFETY_ACCESOS_SHOWCOMBOX";
            public const string Search = "PROC_SAFETY_ACCESOS_SEARCH";
            public const string Insert = "PROC_SAFETY_ACCESOS_INSERT";
            public const string Update = "PROC_SAFETY_ACCESOS_UPDATE";
            public const string Delete = "PROC_SAFETY_ACCESOS_DELETE";
        }
        public static class AccesosRegistro
        {
            public const string Showall = "PROC_SAFETY_ACCESOS_REGISTRO_SHOWALL";
            public const string Showbyid = "PROC_SAFETY_ACCESOS_REGISTRO_SHOWBYID";
            public const string Showcombox = "PROC_SAFETY_ACCESOS_REGISTRO_SHOWCOMBOX";
            public const string Search = "PROC_SAFETY_ACCESOS_REGISTRO_SEARCH";
            public const string Insert = "PROC_SAFETY_ACCESOS_REGISTRO_INSERT";
            public const string Update = "PROC_SAFETY_ACCESOS_REGISTRO_UPDATE";
            public const string Delete = "PROC_SAFETY_ACCESOS_REGISTRO_DELETE";
        }
        public static class ActivosTag
        {
            public const string Showall = "PROC_SAFETY_ACTIVOS_TAG_SHOWALL";
            public const string Showbyid = "PROC_SAFETY_ACTIVOS_TAG_SHOWBYID";
            public const string Showcombox = "PROC_SAFETY_ACTIVOS_TAG_SHOWCOMBOX";
            public const string Search = "PROC_SAFETY_ACTIVOS_TAG_SEARCH";
            public const string Insert = "PROC_SAFETY_ACTIVOS_TAG_INSERT";
            public const string Update = "PROC_SAFETY_ACTIVOS_TAG_UPDATE";
            public const string Delete = "PROC_SAFETY_ACTIVOS_TAG_DELETE";
        }
        public static class ConfiguracionSafety
        {
            public const string Showall = "PROC_SAFETY_CONFIGURACION_SAFETY_SHOWALL";
            public const string Showbyid = "PROC_SAFETY_CONFIGURACION_SAFETY_SHOWBYID";
            public const string Showcombox = "PROC_SAFETY_CONFIGURACION_SAFETY_SHOWCOMBOX";
            public const string Search = "PROC_SAFETY_CONFIGURACION_SAFETY_SEARCH";
            public const string Insert = "PROC_SAFETY_CONFIGURACION_SAFETY_INSERT";
            public const string Update = "PROC_SAFETY_CONFIGURACION_SAFETY_UPDATE";
            public const string Delete = "PROC_SAFETY_CONFIGURACION_SAFETY_DELETE";
        }
        public static class MarcacionAccesos
        {
            public const string Showall = "PROC_SAFETY_MARCACION_ACCESOS_SHOWALL";
            public const string Showbyid = "PROC_SAFETY_MARCACION_ACCESOS_SHOWBYID";
            public const string Showcombox = "PROC_SAFETY_MARCACION_ACCESOS_SHOWCOMBOX";
            public const string Search = "PROC_SAFETY_MARCACION_ACCESOS_SEARCH";
            public const string Insert = "PROC_SAFETY_MARCACION_ACCESOS_INSERT";
            public const string Update = "PROC_SAFETY_MARCACION_ACCESOS_UPDATE";
            public const string Delete = "PROC_SAFETY_MARCACION_ACCESOS_DELETE";
        }
        public static class MarcasAccesos
        {
            public const string Showall = "PROC_SAFETY_MARCAS_ACCESOS_SHOWALL";
            public const string Showbyid = "PROC_SAFETY_MARCAS_ACCESOS_SHOWBYID";
            public const string Showcombox = "PROC_SAFETY_MARCAS_ACCESOS_SHOWCOMBOX";
            public const string Search = "PROC_SAFETY_MARCAS_ACCESOS_SEARCH";
            public const string Insert = "PROC_SAFETY_MARCAS_ACCESOS_INSERT";
            public const string Update = "PROC_SAFETY_MARCAS_ACCESOS_UPDATE";
            public const string Delete = "PROC_SAFETY_MARCAS_ACCESOS_DELETE";
        }
        public static class ProductosTags
        {
            public const string Showall = "PROC_SAFETY_PRODUCTOS_TAGS_SHOWALL";
            public const string Showbyid = "PROC_SAFETY_PRODUCTOS_TAGS_SHOWBYID";
            public const string Showcombox = "PROC_SAFETY_PRODUCTOS_TAGS_SHOWCOMBOX";
            public const string Search = "PROC_SAFETY_PRODUCTOS_TAGS_SEARCH";
            public const string Insert = "PROC_SAFETY_PRODUCTOS_TAGS_INSERT";
            public const string Update = "PROC_SAFETY_PRODUCTOS_TAGS_UPDATE";
            public const string Delete = "PROC_SAFETY_PRODUCTOS_TAGS_DELETE";
        }
        public static class TiposTag
        {
            public const string Showall = "PROC_SAFETY_TIPOS_TAG_SHOWALL";
            public const string Showbyid = "PROC_SAFETY_TIPOS_TAG_SHOWBYID";
            public const string Showcombox = "PROC_SAFETY_TIPOS_TAG_SHOWCOMBOX";
            public const string Search = "PROC_SAFETY_TIPOS_TAG_SEARCH";
            public const string Insert = "PROC_SAFETY_TIPOS_TAG_INSERT";
            public const string Update = "PROC_SAFETY_TIPOS_TAG_UPDATE";
            public const string Delete = "PROC_SAFETY_TIPOS_TAG_DELETE";
        }
    }
    public static class SCHEMASeguridad
    {
        public static class Favoritos
        {
            public const string Insert = "PROC_SEGURIDAD_USUARIOS_APLICACIONES_OPCIONES_FAVORITOS_INSERT";
            public const string Delete = "PROC_SEGURIDAD_USUARIOS_APLICACIONES_OPCIONES_FAVORITOS_DELETE";
            public const string Showall = "PROC_SEGURIDAD_USUARIOS_APLICACIONES_OPCIONES_FAVORITOS_SHOWALL";
            // Experimento: íconos de los favoritos del Workspace en un SP aparte del texto —
            // mismo patrón que Entidades/OpcionesAplicaciones/menús Tile-Lateral-Circular.
            public const string ShowallLogos = "PROC_SEGURIDAD_USUARIOS_APLICACIONES_OPCIONES_FAVORITOS_SHOWALLLOGOS";
        }
        public static class Aplicaciones
        {
            public const string Showall = "PROC_SEGURIDAD_APLICACIONES_SHOWALL";
            public const string Showbyid = "PROC_SEGURIDAD_APLICACIONES_SHOWBYID";
            public const string Showcombox = "PROC_SEGURIDAD_APLICACIONES_SHOWCOMBOX";
            public const string Search = "PROC_SEGURIDAD_APLICACIONES_SEARCH";
            public const string Insert = "PROC_SEGURIDAD_APLICACIONES_INSERT";
            public const string Update = "PROC_SEGURIDAD_APLICACIONES_UPDATE";
            public const string Delete = "PROC_SEGURIDAD_APLICACIONES_DELETE";
        }
        public static class AplicacionesOpcionesBotones
        {
            public const string Showall = "PROC_SEGURIDAD_APLICACIONES_OPCIONES_BOTONES_SHOWALL";
            public const string Showbyid = "PROC_SEGURIDAD_APLICACIONES_OPCIONES_BOTONES_SHOWBYID";
            public const string Showcombox = "PROC_SEGURIDAD_APLICACIONES_OPCIONES_BOTONES_SHOWCOMBOX";
            public const string Search = "PROC_SEGURIDAD_APLICACIONES_OPCIONES_BOTONES_SEARCH";
            public const string Insert = "PROC_SEGURIDAD_APLICACIONES_OPCIONES_BOTONES_INSERT";
            public const string Delete = "PROC_SEGURIDAD_APLICACIONES_OPCIONES_BOTONES_DELETE";
            // Botón estándar + Estado (SEGURIDAD.APLICACIONES_OPCIONES_BOTONES) en un solo viaje.
            public const string Loadcombos = "PROC_SEGURIDAD_APLICACIONES_OPCIONES_BOTONES_LOADCOMBOS";
        }
        public static class AplicacionesOpcionesTreeview
        {
            public const string Showall = "PROC_SEGURIDAD_APLICACIONES_OPCINOES_TREVIEW";
            // Experimento: íconos de TODO el árbol en un SP/endpoint aparte del Showall
            // (texto) — mismo patrón que Entidades/images.
            public const string ShowLogos = "PROC_SEGURIDAD_APLICACIONES_OPCINOES_TREVIEWLOGOS";
        }
        public static class AplicacionesOpciones
        {
            public const string Showall = "PROC_SEGURIDAD_APLICACIONES_OPCIONES_SHOWALL";
            public const string Showbyid = "PROC_SEGURIDAD_APLICACIONES_OPCIONES_SHOWBYID";
            public const string Showcombox = "PROC_SEGURIDAD_APLICACIONES_OPCIONES_SHOWCOMBOX";
            public const string Search = "PROC_SEGURIDAD_APLICACIONES_OPCIONES_SEARCH";
            public const string Insert = "PROC_SEGURIDAD_APLICACIONES_OPCIONES_INSERT";
            public const string Delete = "PROC_SEGURIDAD_APLICACIONES_OPCIONES_DELETE";
            public const string Move = "PROC_SEGURIDAD_OPCIONES_MOVE";
            // Tipo de opción + Aplicación + Estado (SEGURIDAD.APLICACIONES_OPCIONES) en un solo viaje.
            public const string Loadcombos = "PROC_SEGURIDAD_APLICACIONES_OPCIONES_LOADCOMBOS";
        }
        public static class BotonesEstandar
        {
            public const string Showall = "PROC_SEGURIDAD_BOTONES_ESTANDAR_SHOWALL";
            public const string Showbyid = "PROC_SEGURIDAD_BOTONES_ESTANDAR_SHOWBYID";
            public const string Showcombox = "PROC_SEGURIDAD_BOTONES_ESTANDAR_SHOWCOMBOX";
            public const string Search = "PROC_SEGURIDAD_BOTONES_ESTANDAR_SEARCH";
            public const string Insert = "PROC_SEGURIDAD_BOTONES_ESTANDAR_INSERT";
            public const string Update = "PROC_SEGURIDAD_BOTONES_ESTANDAR_UPDATE";
            public const string Delete = "PROC_SEGURIDAD_BOTONES_ESTANDAR_DELETE";
        }
        public static class ComplejidadPassword
        {
            public const string Showall = "PROC_SEGURIDAD_COMPLEJIDAD_PASSWORD_SHOWALL";
            public const string Showbyid = "PROC_SEGURIDAD_COMPLEJIDAD_PASSWORD_SHOWBYID";
            public const string Showcombox = "PROC_SEGURIDAD_COMPLEJIDAD_PASSWORD_SHOWCOMBOX";
            public const string Search = "PROC_SEGURIDAD_COMPLEJIDAD_PASSWORD_SEARCH";
            public const string Insert = "PROC_SEGURIDAD_COMPLEJIDAD_PASSWORD_INSERT";
            public const string Update = "PROC_SEGURIDAD_COMPLEJIDAD_PASSWORD_UPDATE";
            public const string Delete = "PROC_SEGURIDAD_COMPLEJIDAD_PASSWORD_DELETE";
        }
        public static class PerfilesUsuario
        {
            public const string Showall = "PROC_SEGURIDAD_PERFILES_USUARIO_SHOWALL";
            public const string Showbyid = "PROC_SEGURIDAD_PERFILES_USUARIO_SHOWBYID";
            public const string Showcombox = "PROC_SEGURIDAD_PERFILES_USUARIO_SHOWCOMBOX";
            public const string Search = "PROC_SEGURIDAD_PERFILES_USUARIO_SEARCH";
            public const string Insert = "PROC_SEGURIDAD_PERFILES_USUARIO_INSERT";
            public const string Update = "PROC_SEGURIDAD_PERFILES_USUARIO_UPDATE";
            public const string Delete = "PROC_SEGURIDAD_PERFILES_USUARIO_DELETE";
            // Combos bundleados de PerfilDialog.razor: Complejidad de contraseña + Estado
            // en una sola llamada.
            public const string Loadcombos = "PROC_SEGURIDAD_PERFILES_USUARIO_LOADCOMBOS";
        }
        public static class RolesUsuarioAplicaciones
        {
            public const string Showall = "PROC_SEGURIDAD_ROLES_USUARIO_APLICACIONES_SHOWALL";
            public const string Showbyid = "PROC_SEGURIDAD_ROLES_USUARIO_APLICACIONES_SHOWBYID";
            public const string Showcombox = "PROC_SEGURIDAD_ROLES_USUARIO_APLICACIONES_SHOWCOMBOX";
            public const string Search = "PROC_SEGURIDAD_ROLES_USUARIO_APLICACIONES_SEARCH";
            public const string Insert = "PROC_SEGURIDAD_ROLES_USUARIO_APLICACIONES_INSERT";
            public const string Update = "PROC_SEGURIDAD_ROLES_USUARIO_APLICACIONES_UPDATE";
            public const string Delete = "PROC_SEGURIDAD_ROLES_USUARIO_APLICACIONES_DELETE";
        }
        public static class RolesUsuario
        {
            public const string Showall = "PROC_SEGURIDAD_ROLES_USUARIO_SHOWALL";
            public const string Showbyid = "PROC_SEGURIDAD_ROLES_USUARIO_SHOWBYID";
            public const string Showcombox = "PROC_SEGURIDAD_ROLES_USUARIO_SHOWCOMBOX";
            public const string Search = "PROC_SEGURIDAD_ROLES_USUARIO_SEARCH";
            public const string Insert = "PROC_SEGURIDAD_ROLES_USUARIO_INSERT";
            public const string Update = "PROC_SEGURIDAD_ROLES_USUARIO_UPDATE";
            public const string Delete = "PROC_SEGURIDAD_ROLES_USUARIO_DELETE";
        }
        /// <summary>Autorización de la API. No es un CRUD: son las tres piezas que usa el
        /// guardián de permisos en cada request.</summary>
        public static class PermisosApi
        {
            public const string ShowallPorRol = "PROC_SEGURIDAD_PERMISOS_ROL_SHOWALL";
            public const string AuditoriaInsert = "PROC_SEGURIDAD_PERMISOS_AUDITORIA_INSERT";
            // Desempata el upsert: dice si la clave del POST ya existe.
            public const string ExisteRegistro = "PROC_SEGURIDAD_EXISTE_REGISTRO";
            // 67: hermano del anterior. Misma resolución de tabla+PK por DES_OBJETO+JSON,
            // pero devuelve COD_TIPO_ESTADO/COD_ESTADO en vez de EXISTS.
            public const string EstadoActual = "PROC_SEGURIDAD_ESTADO_ACTUAL";
        }
        // ── Hijos de SEGURIDAD.ROLES_USUARIO ────────────────────────────────────
        // Los cuatro traen además un SP _DISPONIBLES: es la base la que sabe qué le falta al
        // rol (y ya descarta lo anulado), así que la pantalla no resta listas a mano.
        public static class RolesUsuarioAlmacenes
        {
            public const string Showall = "PROC_SEGURIDAD_ROLES_USUARIO_ALMACENES_SHOWALL";
            public const string Showbyid = "PROC_SEGURIDAD_ROLES_USUARIO_ALMACENES_SHOWBYID";
            public const string Showcombox = "PROC_SEGURIDAD_ROLES_USUARIO_ALMACENES_SHOWCOMBOX";
            public const string Search = "PROC_SEGURIDAD_ROLES_USUARIO_ALMACENES_SEARCH";
            public const string Insert = "PROC_SEGURIDAD_ROLES_USUARIO_ALMACENES_INSERT";
            public const string Delete = "PROC_SEGURIDAD_ROLES_USUARIO_ALMACENES_DELETE";
            public const string Disponibles = "PROC_SEGURIDAD_ROLES_USUARIO_ALMACENES_DISPONIBLES";
        }
        public static class RolesUsuarioAlmacenesTiposMovimiento
        {
            public const string Showall = "PROC_SEGURIDAD_ROLES_USUARIO_ALMACENES_TIPOS_MOVIMIENTO_SHOWALL";
            public const string Showbyid = "PROC_SEGURIDAD_ROLES_USUARIO_ALMACENES_TIPOS_MOVIMIENTO_SHOWBYID";
            public const string Showcombox = "PROC_SEGURIDAD_ROLES_USUARIO_ALMACENES_TIPOS_MOVIMIENTO_SHOWCOMBOX";
            public const string Search = "PROC_SEGURIDAD_ROLES_USUARIO_ALMACENES_TIPOS_MOVIMIENTO_SEARCH";
            public const string Insert = "PROC_SEGURIDAD_ROLES_USUARIO_ALMACENES_TIPOS_MOVIMIENTO_INSERT";
            public const string Delete = "PROC_SEGURIDAD_ROLES_USUARIO_ALMACENES_TIPOS_MOVIMIENTO_DELETE";
            public const string Disponibles = "PROC_SEGURIDAD_ROLES_USUARIO_ALMACENES_TIPOS_MOVIMIENTO_DISPONIBLES";
        }
        public static class RolesUsuarioTiposOperacion
        {
            public const string Showall = "PROC_SEGURIDAD_ROLES_USUARIO_TIPOS_OPERACION_SHOWALL";
            public const string Showbyid = "PROC_SEGURIDAD_ROLES_USUARIO_TIPOS_OPERACION_SHOWBYID";
            public const string Showcombox = "PROC_SEGURIDAD_ROLES_USUARIO_TIPOS_OPERACION_SHOWCOMBOX";
            public const string Search = "PROC_SEGURIDAD_ROLES_USUARIO_TIPOS_OPERACION_SEARCH";
            public const string Insert = "PROC_SEGURIDAD_ROLES_USUARIO_TIPOS_OPERACION_INSERT";
            public const string Delete = "PROC_SEGURIDAD_ROLES_USUARIO_TIPOS_OPERACION_DELETE";
            public const string Disponibles = "PROC_SEGURIDAD_ROLES_USUARIO_TIPOS_OPERACION_DISPONIBLES";
        }
        public static class RolesUsuarioAsientosContables
        {
            public const string Showall = "PROC_SEGURIDAD_ROLES_USUARIO_ASIENTOS_CONTABLES_SHOWALL";
            public const string Showbyid = "PROC_SEGURIDAD_ROLES_USUARIO_ASIENTOS_CONTABLES_SHOWBYID";
            public const string Showcombox = "PROC_SEGURIDAD_ROLES_USUARIO_ASIENTOS_CONTABLES_SHOWCOMBOX";
            public const string Search = "PROC_SEGURIDAD_ROLES_USUARIO_ASIENTOS_CONTABLES_SEARCH";
            public const string Insert = "PROC_SEGURIDAD_ROLES_USUARIO_ASIENTOS_CONTABLES_INSERT";
            public const string Delete = "PROC_SEGURIDAD_ROLES_USUARIO_ASIENTOS_CONTABLES_DELETE";
            public const string Disponibles = "PROC_SEGURIDAD_ROLES_USUARIO_ASIENTOS_CONTABLES_DISPONIBLES";
        }
        public static class TiposOpciones
        {
            public const string Showall = "PROC_SEGURIDAD_TIPOS_OPCIONES_SHOWALL";
            public const string Showbyid = "PROC_SEGURIDAD_TIPOS_OPCIONES_SHOWBYID";
            public const string Showcombox = "PROC_SEGURIDAD_TIPOS_OPCIONES_SHOWCOMBOX";
            public const string Search = "PROC_SEGURIDAD_TIPOS_OPCIONES_SEARCH";
            public const string Insert = "PROC_SEGURIDAD_TIPOS_OPCIONES_INSERT";
            public const string Update = "PROC_SEGURIDAD_TIPOS_OPCIONES_UPDATE";
            public const string Delete = "PROC_SEGURIDAD_TIPOS_OPCIONES_DELETE";
        }
        public static class RolesUsuariosAplicacionesOpciones
        {
            public const string Showall = "PROC_SEGURIDAD_ROLES_USUARIOS_APLICACIONES_OPCIONES_SHOWALL";
            public const string Showbyid = "PROC_SEGURIDAD_ROLES_USUARIOS_APLICACIONES_OPCIONES_SHOWBYID";
            public const string Showcombox = "PROC_SEGURIDAD_ROLES_USUARIOS_APLICACIONES_OPCIONES_SHOWCOMBOX";
            public const string Search = "PROC_SEGURIDAD_ROLES_USUARIOS_APLICACIONES_OPCIONES_SEARCH";
            public const string Insert = "PROC_SEGURIDAD_ROLES_USUARIOS_APLICACIONES_OPCIONES_INSERT";
            public const string Update = "PROC_SEGURIDAD_ROLES_USUARIOS_APLICACIONES_OPCIONES_UPDATE";
            public const string Delete = "PROC_SEGURIDAD_ROLES_USUARIOS_APLICACIONES_OPCIONES_DELETE";
            public const string TreeviewAsignadas  = "PROC_SEGURIDAD_ROLES_USUARIOS_APLICACIONES_OPCIONES_ASIGNADAS_TREEVIEW";
            public const string TreeviewDisponibles = "PROC_SEGURIDAD_ROLES_USUARIOS_APLICACIONES_OPCIONES_DISPONIBLES_TREEVIEW";
        }
        public static class RolesUsuariosAplicacionesOpcionesBotones
        {
            public const string Showall = "PROC_SEGURIDAD_ROLES_USUARIOS_APLICACIONES_OPCIONES_BOTONES_SHOWALL";
            public const string Showbyid = "PROC_SEGURIDAD_ROLES_USUARIOS_APLICACIONES_OPCIONES_BOTONES_SHOWBYID";
            public const string Showcombox = "PROC_SEGURIDAD_ROLES_USUARIOS_APLICACIONES_OPCIONES_BOTONES_SHOWCOMBOX";
            public const string Search = "PROC_SEGURIDAD_ROLES_USUARIOS_APLICACIONES_OPCIONES_BOTONES_SEARCH";
            public const string Insert = "PROC_SEGURIDAD_ROLES_USUARIOS_APLICACIONES_OPCIONES_BOTONES_INSERT";
            public const string Update = "PROC_SEGURIDAD_ROLES_USUARIOS_APLICACIONES_OPCIONES_BOTONES_UPDATE";
            public const string Delete = "PROC_SEGURIDAD_ROLES_USUARIOS_APLICACIONES_OPCIONES_BOTONES_DELETE";
            // Trae TODOS los botones de TODAS las opciones del rol de un usuario en una sola llamada
            // (sin parámetros de aplicación/opción) — usado al login para precargar permisos.
            public const string Treeview = "PROC_SEGURIDAD_ROLES_USUARIOS_APLICACIONES_OPCIONES_BOTONES_TREEVIEW";
            // Experimento: íconos de TODO el árbol (apps/opciones/botones) del rol en un SP
            // aparte del Treeview de texto — mismo patrón que Entidades/OpcionesAplicaciones.
            public const string TreeviewLogos = "PROC_SEGURIDAD_ROLES_USUARIOS_APLICACIONES_OPCIONES_BOTONES_TREEVIEWLOGOS";
        }
        public static class RolesUsuarioUsuarios
        {
            public const string Showall = "PROC_SEGURIDAD_ROLES_USUARIO_USUARIOS_SHOWALL";
            public const string Showbyid = "PROC_SEGURIDAD_ROLES_USUARIO_USUARIOS_SHOWBYID";
            public const string Showbyusuario = "PROC_SEGURIDAD_ROLES_USUARIO_USUARIOS_SHOWBYUSUARIO";
            public const string Showcombox = "PROC_SEGURIDAD_ROLES_USUARIO_USUARIOS_SHOWCOMBOX";
            public const string Search = "PROC_SEGURIDAD_ROLES_USUARIO_USUARIOS_SEARCH";
            public const string Insert = "PROC_SEGURIDAD_ROLES_USUARIO_USUARIOS_INSERT";
            public const string Delete = "PROC_SEGURIDAD_ROLES_USUARIO_USUARIOS_DELETE";
        }
        public static class Usuarios
        {
            public const string Showall = "PROC_SEGURIDAD_USUARIOS_SHOWALL";
            public const string Showbyid = "PROC_SEGURIDAD_USUARIOS_SHOWBYID";
            public const string Showcombox = "PROC_SEGURIDAD_USUARIOS_SHOWCOMBOX";
            public const string Search = "PROC_SEGURIDAD_USUARIOS_SEARCH";
            public const string Insert = "PROC_SEGURIDAD_USUARIOS_INSERT";
            public const string Delete = "PROC_SEGURIDAD_USUARIOS_DELETE";
            public const string Estados = "PROC_SEGURIDAD_USUARIOS_ESTADOS";
            public const string Password = "PROC_SEGURIDAD_USUARIOS_PASSWORD";
            public const string PasswordReset = "PROC_SEGURIDAD_USUARIOS_PASSWORDRESET";
            public const string Resetintentos = "PROC_SEGURIDAD_USUARIOS_RESETINTENTOS";
            public const string Validalogin = "PROC_SEGURIDAD_USUARIOS_VALIDALOGIN";
            // Perfil + Dominios de correo + Estado (SEGURIDAD.USUARIOS) en un solo viaje.
            public const string Loadcombos = "PROC_SEGURIDAD_USUARIOS_LOADCOMBOS";
        }
        /// <summary>Biometría facial: SEGURIDAD.USUARIOS_BIOMETRIA_FACIAL.
        /// No hay Update — una muestra no se corrige, se reemplaza el juego completo.</summary>
        public static class UsuariosBiometriaFacial
        {
            public const string Showall = "PROC_SEGURIDAD_USUARIOS_BIOMETRIA_FACIAL_SHOWALL";
            public const string Insert  = "PROC_SEGURIDAD_USUARIOS_BIOMETRIA_FACIAL_INSERT";
            public const string Delete  = "PROC_SEGURIDAD_USUARIOS_BIOMETRIA_FACIAL_DELETE";
            public const string Estado  = "PROC_SEGURIDAD_USUARIOS_BIOMETRIA_FACIAL_ESTADO";
            public const string Estados = "PROC_SEGURIDAD_USUARIOS_BIOMETRIA_FACIAL_ESTADOS";
        }
        public static class UsuariosAlmacenes
        {
            public const string Showall = "PROC_SEGURIDAD_USUARIOS_ALMACENES_SHOWALL";
            public const string Showbyid = "PROC_SEGURIDAD_USUARIOS_ALMACENES_SHOWBYID";
            public const string Showcombox = "PROC_SEGURIDAD_USUARIOS_ALMACENES_SHOWCOMBOX";
            public const string Search = "PROC_SEGURIDAD_USUARIOS_ALMACENES_SEARCH";
            public const string Insert = "PROC_SEGURIDAD_USUARIOS_ALMACENES_INSERT";
            public const string Delete = "PROC_SEGURIDAD_USUARIOS_ALMACENES_DELETE";
        }
        public static class UsuariosAlmacenesTiposMovimiento
        {
            public const string Showall = "PROC_SEGURIDAD_USUARIOS_ALMACENES_TIPOS_MOVIMIENTO_SHOWALL";
            public const string Showbyid = "PROC_SEGURIDAD_USUARIOS_ALMACENES_TIPOS_MOVIMIENTO_SHOWBYID";
            public const string Showcombox = "PROC_SEGURIDAD_USUARIOS_ALMACENES_TIPOS_MOVIMIENTO_SHOWCOMBOX";
            public const string Search = "PROC_SEGURIDAD_USUARIOS_ALMACENES_TIPOS_MOVIMIENTO_SEARCH";
            public const string Insert = "PROC_SEGURIDAD_USUARIOS_ALMACENES_TIPOS_MOVIMIENTO_INSERT";
            public const string Delete = "PROC_SEGURIDAD_USUARIOS_ALMACENES_TIPOS_MOVIMIENTO_DELETE";
        }
        public static class UsuariosAsientosContables
        {
            public const string Showall = "PROC_SEGURIDAD_USUARIOS_ASIENTOS_CONTABLES_SHOWALL";
            public const string Showbyid = "PROC_SEGURIDAD_USUARIOS_ASIENTOS_CONTABLES_SHOWBYID";
            public const string Showcombox = "PROC_SEGURIDAD_USUARIOS_ASIENTOS_CONTABLES_SHOWCOMBOX";
            public const string Search = "PROC_SEGURIDAD_USUARIOS_ASIENTOS_CONTABLES_SEARCH";
            public const string Insert = "PROC_SEGURIDAD_USUARIOS_ASIENTOS_CONTABLES_INSERT";
            public const string Delete = "PROC_SEGURIDAD_USUARIOS_ASIENTOS_CONTABLES_DELETE";
        }
        public static class UsuariosSucursalesCalendario
        {
            public const string Showall = "PROC_SEGURIDAD_USUARIOS_SUCURSALES_CALENDARIO_SHOWALL";
            public const string Showbyid = "PROC_SEGURIDAD_USUARIOS_SUCURSALES_CALENDARIO_SHOWBYID";
            public const string Showcombox = "PROC_SEGURIDAD_USUARIOS_SUCURSALES_CALENDARIO_SHOWCOMBOX";
            public const string Search = "PROC_SEGURIDAD_USUARIOS_SUCURSALES_CALENDARIO_SEARCH";
            public const string Insert = "PROC_SEGURIDAD_USUARIOS_SUCURSALES_CALENDARIO_INSERT";
            public const string Update = "PROC_SEGURIDAD_USUARIOS_SUCURSALES_CALENDARIO_UPDATE";
            public const string Delete = "PROC_SEGURIDAD_USUARIOS_SUCURSALES_CALENDARIO_DELETE";
        }
        public static class UsuariosTiposOperacion
        {
            public const string Showall = "PROC_SEGURIDAD_USUARIOS_TIPOS_OPERACION_SHOWALL";
            public const string Showbyid = "PROC_SEGURIDAD_USUARIOS_TIPOS_OPERACION_SHOWBYID";
            public const string Showcombox = "PROC_SEGURIDAD_USUARIOS_TIPOS_OPERACION_SHOWCOMBOX";
            public const string Search = "PROC_SEGURIDAD_USUARIOS_TIPOS_OPERACION_SEARCH";
            public const string Insert = "PROC_SEGURIDAD_USUARIOS_TIPOS_OPERACION_INSERT";
            public const string Delete = "PROC_SEGURIDAD_USUARIOS_TIPOS_OPERACION_DELETE";
        }
    }
    public static class SCHEMASolicitudes
    {
        public static class Prioridades
        {
            public const string Showall = "PROC_SOLICITUDES_PRIORIDADES_SHOWALL";
            public const string Showbyid = "PROC_SOLICITUDES_PRIORIDADES_SHOWBYID";
            public const string Showcombox = "PROC_SOLICITUDES_PRIORIDADES_SHOWCOMBOX";
            public const string Search = "PROC_SOLICITUDES_PRIORIDADES_SEARCH";
            public const string Insert = "PROC_SOLICITUDES_PRIORIDADES_INSERT";
            public const string Update = "PROC_SOLICITUDES_PRIORIDADES_UPDATE";
            public const string Delete = "PROC_SOLICITUDES_PRIORIDADES_DELETE";
        }
        public static class Solicitudes
        {
            public const string Showall = "PROC_SOLICITUDES_SOLICITUDES_SHOWALL";
            public const string Showbyid = "PROC_SOLICITUDES_SOLICITUDES_SHOWBYID";
            public const string Showcombox = "PROC_SOLICITUDES_SOLICITUDES_SHOWCOMBOX";
            public const string Search = "PROC_SOLICITUDES_SOLICITUDES_SEARCH";
            public const string Insert = "PROC_SOLICITUDES_SOLICITUDES_INSERT";
            public const string Update = "PROC_SOLICITUDES_SOLICITUDES_UPDATE";
            public const string Delete = "PROC_SOLICITUDES_SOLICITUDES_DELETE";
        }
        public static class TiposSolicitud
        {
            public const string Showall = "PROC_SOLICITUDES_TIPOS_SOLICITUD_SHOWALL";
            public const string Showbyid = "PROC_SOLICITUDES_TIPOS_SOLICITUD_SHOWBYID";
            public const string Showcombox = "PROC_SOLICITUDES_TIPOS_SOLICITUD_SHOWCOMBOX";
            public const string Search = "PROC_SOLICITUDES_TIPOS_SOLICITUD_SEARCH";
            public const string Insert = "PROC_SOLICITUDES_TIPOS_SOLICITUD_INSERT";
            public const string Update = "PROC_SOLICITUDES_TIPOS_SOLICITUD_UPDATE";
            public const string Delete = "PROC_SOLICITUDES_TIPOS_SOLICITUD_DELETE";
        }
    }
    public static class SCHEMASoporte
    {
        public static class Tareas
        {
            public const string Showall = "PROC_SOPORTE_TAREAS_SHOWALL";
            public const string Showbyid = "PROC_SOPORTE_TAREAS_SHOWBYID";
            public const string Showcombox = "PROC_SOPORTE_TAREAS_SHOWCOMBOX";
            public const string Search = "PROC_SOPORTE_TAREAS_SEARCH";
            public const string Insert = "PROC_SOPORTE_TAREAS_INSERT";
            public const string Update = "PROC_SOPORTE_TAREAS_UPDATE";
            public const string Delete = "PROC_SOPORTE_TAREAS_DELETE";
        }
        public static class TicketsAtencion
        {
            public const string Showall = "PROC_SOPORTE_TICKETS_ATENCION_SHOWALL";
            public const string Showbyid = "PROC_SOPORTE_TICKETS_ATENCION_SHOWBYID";
            public const string Showcombox = "PROC_SOPORTE_TICKETS_ATENCION_SHOWCOMBOX";
            public const string Search = "PROC_SOPORTE_TICKETS_ATENCION_SEARCH";
            public const string Insert = "PROC_SOPORTE_TICKETS_ATENCION_INSERT";
            public const string Update = "PROC_SOPORTE_TICKETS_ATENCION_UPDATE";
            public const string Delete = "PROC_SOPORTE_TICKETS_ATENCION_DELETE";
        }
        public static class TicketsAtencionDetalle
        {
            public const string Showall = "PROC_SOPORTE_TICKETS_ATENCION_DETALLE_SHOWALL";
            public const string Showbyid = "PROC_SOPORTE_TICKETS_ATENCION_DETALLE_SHOWBYID";
            public const string Showcombox = "PROC_SOPORTE_TICKETS_ATENCION_DETALLE_SHOWCOMBOX";
            public const string Search = "PROC_SOPORTE_TICKETS_ATENCION_DETALLE_SEARCH";
            public const string Insert = "PROC_SOPORTE_TICKETS_ATENCION_DETALLE_INSERT";
            public const string Update = "PROC_SOPORTE_TICKETS_ATENCION_DETALLE_UPDATE";
            public const string Delete = "PROC_SOPORTE_TICKETS_ATENCION_DETALLE_DELETE";
        }
        public static class TicketsAtencionDetallePasos
        {
            public const string Showall = "PROC_SOPORTE_TICKETS_ATENCION_DETALLE_PASOS_SHOWALL";
            public const string Showbyid = "PROC_SOPORTE_TICKETS_ATENCION_DETALLE_PASOS_SHOWBYID";
            public const string Showcombox = "PROC_SOPORTE_TICKETS_ATENCION_DETALLE_PASOS_SHOWCOMBOX";
            public const string Search = "PROC_SOPORTE_TICKETS_ATENCION_DETALLE_PASOS_SEARCH";
            public const string Insert = "PROC_SOPORTE_TICKETS_ATENCION_DETALLE_PASOS_INSERT";
            public const string Update = "PROC_SOPORTE_TICKETS_ATENCION_DETALLE_PASOS_UPDATE";
            public const string Delete = "PROC_SOPORTE_TICKETS_ATENCION_DETALLE_PASOS_DELETE";
        }
    }
    public static class SCHEMASunat
    {
        public static class CubsoSunat
        {
            public const string Showall = "PROC_SUNAT_CUBSO_SUNAT_SHOWALL";
            public const string Showbyid = "PROC_SUNAT_CUBSO_SUNAT_SHOWBYID";
            public const string Showcombox = "PROC_SUNAT_CUBSO_SUNAT_SHOWCOMBOX";
            public const string Search = "PROC_SUNAT_CUBSO_SUNAT_SEARCH";
            public const string Insert = "PROC_SUNAT_CUBSO_SUNAT_INSERT";
            public const string Update = "PROC_SUNAT_CUBSO_SUNAT_UPDATE";
            public const string Delete = "PROC_SUNAT_CUBSO_SUNAT_DELETE";
            public const string UploadJson = "PROC_SUNAT_CUBSO_SUNAT_UPLOAD_JSON";
        }
        public static class RegimenesTributarios
        {
            public const string Showall = "PROC_SUNAT_REGIMENES_TRIBUTARIOS_SHOWALL";
            public const string Showbyid = "PROC_SUNAT_REGIMENES_TRIBUTARIOS_SHOWBYID";
            public const string Showcombox = "PROC_SUNAT_REGIMENES_TRIBUTARIOS_SHOWCOMBOX";
            public const string Search = "PROC_SUNAT_REGIMENES_TRIBUTARIOS_SEARCH";
            public const string Insert = "PROC_SUNAT_REGIMENES_TRIBUTARIOS_INSERT";
            public const string Update = "PROC_SUNAT_REGIMENES_TRIBUTARIOS_UPDATE";
            public const string Delete = "PROC_SUNAT_REGIMENES_TRIBUTARIOS_DELETE";
            public const string Loadcombos = "PROC_SUNAT_REGIMENES_TRIBUTARIOS_LOADCOMBOS";
        }
    }
    public static class SCHEMAZonificacion
    {
        public static class ActivosZonificacion
        {
            public const string Showall = "PROC_ZONIFICACION_ACTIVOS_ZONIFICACION_SHOWALL";
            public const string Showbyid = "PROC_ZONIFICACION_ACTIVOS_ZONIFICACION_SHOWBYID";
            public const string Showcombox = "PROC_ZONIFICACION_ACTIVOS_ZONIFICACION_SHOWCOMBOX";
            public const string Search = "PROC_ZONIFICACION_ACTIVOS_ZONIFICACION_SEARCH";
            public const string Insert = "PROC_ZONIFICACION_ACTIVOS_ZONIFICACION_INSERT";
            public const string Update = "PROC_ZONIFICACION_ACTIVOS_ZONIFICACION_UPDATE";
            public const string Delete = "PROC_ZONIFICACION_ACTIVOS_ZONIFICACION_DELETE";
        }
        public static class TiposZonificacion
        {
            public const string Showall = "PROC_ZONIFICACION_TIPOS_ZONIFICACION_SHOWALL";
            public const string Showbyid = "PROC_ZONIFICACION_TIPOS_ZONIFICACION_SHOWBYID";
            public const string Showcombox = "PROC_ZONIFICACION_TIPOS_ZONIFICACION_SHOWCOMBOX";
            public const string Search = "PROC_ZONIFICACION_TIPOS_ZONIFICACION_SEARCH";
            public const string Insert = "PROC_ZONIFICACION_TIPOS_ZONIFICACION_INSERT";
            public const string Delete = "PROC_ZONIFICACION_TIPOS_ZONIFICACION_DELETE";
        }
        public static class Zonificacion
        {
            public const string Showall = "PROC_ZONIFICACION_ZONIFICACION_SHOWALL";
            public const string Showbyid = "PROC_ZONIFICACION_ZONIFICACION_SHOWBYID";
            public const string Showcombox = "PROC_ZONIFICACION_ZONIFICACION_SHOWCOMBOX";
            public const string Search = "PROC_ZONIFICACION_ZONIFICACION_SEARCH";
            public const string Insert = "PROC_ZONIFICACION_ZONIFICACION_INSERT";
            public const string Delete = "PROC_ZONIFICACION_ZONIFICACION_DELETE";
            // Combos bundleados de ZonificacionDialog.razor: Tipos de Zonificación + Zonas
            // padre de la sucursal + Estado en una sola llamada.
            public const string Loadcombos = "PROC_ZONIFICACION_ZONIFICACION_LOADCOMBOS";
        }
    }

    public static class SCHEMAExterno
    {
        public static class Snippets
        {
            public const string Showall     = "PROC_EXTERNO_SNIPPETS_SHOWALL";
            public const string Showbyid    = "PROC_EXTERNO_SNIPPETS_SHOWBYID";
            public const string Showcombox  = "PROC_EXTERNO_SNIPPETS_SHOWCOMBOX";
            public const string Search      = "PROC_EXTERNO_SNIPPETS_SEARCH";
            public const string Insert      = "PROC_EXTERNO_SNIPPETS_INSERT";
            public const string Delete      = "PROC_EXTERNO_SNIPPETS_DELETE";
            public const string SearchBoton = "PROC_EXTERNO_SNIPPETS_SEARCHBOTON";
        }
    }

    // Reporteador (esquema REPORTES): fuentes de datos, diseños, asignación a botones y ejecución.
    // Se crean con SQL/02_REPORTES_PROCEDIMIENTOS.sql.
    public static class SCHEMAReportes
    {
        public static class Catalogo
        {
            public const string Procedimientos = "PROC_REPORTES_PROCEDIMIENTOS_SHOWCOMBOX";
            public const string ParametrosSp   = "PROC_REPORTES_PROCEDIMIENTOS_PARAMETROS";
            public const string Etiquetas      = "PROC_REPORTES_ETIQUETAS";
        }
        public static class Fuentes
        {
            public const string Showall  = "PROC_REPORTES_FUENTES_SHOWALL";
            public const string Showbyid = "PROC_REPORTES_FUENTES_SHOWBYID";
            public const string Grabar   = "PROC_REPORTES_FUENTES_GRABAR";
            public const string Delete   = "PROC_REPORTES_FUENTES_DELETE";
            // Las columnas nuevas que devuelve el SP de la fuente (al generar un reporte)
            public const string ColumnasAgregar = "PROC_REPORTES_FUENTES_COLUMNAS_AGREGAR";
        }
        public static class Disenos
        {
            public const string Showall         = "PROC_REPORTES_DISENOS_SHOWALL";
            public const string Showbyid        = "PROC_REPORTES_DISENOS_SHOWBYID";
            public const string Grabar          = "PROC_REPORTES_DISENOS_GRABAR";
            public const string Delete          = "PROC_REPORTES_DISENOS_DELETE";
            public const string PlantillaGrabar = "PROC_REPORTES_DISENOS_PLANTILLA_GRABAR";
            public const string PlantillaShow   = "PROC_REPORTES_DISENOS_PLANTILLA_SHOW";
            // Diseño libre: grupos, bandas y objetos del lienzo
            public const string LienzoShowbyid  = "PROC_REPORTES_DISENOS_LIENZO_SHOWBYID";
            public const string LienzoGrabar    = "PROC_REPORTES_DISENOS_LIENZO_GRABAR";
        }
        public static class Botones
        {
            public const string Showall  = "PROC_REPORTES_OPCIONES_BOTONES_SHOWALL";
            public const string Showbyid = "PROC_REPORTES_OPCIONES_BOTONES_SHOWBYID";
            public const string Grabar   = "PROC_REPORTES_OPCIONES_BOTONES_GRABAR";
            public const string Delete   = "PROC_REPORTES_OPCIONES_BOTONES_DELETE";
            public const string Rutas    = "PROC_REPORTES_RUTAS";
        }
        public static class Ejecucion
        {
            public const string OpcionBotones = "PROC_REPORTES_OPCION_BOTONES";
            public const string Definicion    = "PROC_REPORTES_DEFINICION";
            public const string Empresa       = "PROC_REPORTES_EMPRESA";
        }
    }
}
