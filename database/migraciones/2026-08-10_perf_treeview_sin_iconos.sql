/* ============================================================================
   PRIORIDAD 2 — Que PROC_..._TREEVIEW deje de traer los íconos

   POR QUÉ
   El SP devuelve hoy dos columnas de imagen que el frontend descarta apenas
   las recibe (MenuService.FetchBotonesAsync):

       foreach (var a in data.Aplicaciones) a.IMG_ICONO_APLICACION = null;
       foreach (var o in data.Opciones)     o.IMG_ICONO_OPCION     = null;

   El propio comentario del código ya advierte que eso solo evita que ocupen
   memoria del navegador, pero NO evita que los bytes viajen desde SQL Server
   → API → red → navegador. Este cambio corta ese tráfico en el origen.

   Los íconos siguen llegando igual, por el SP aparte
   PROC_..._TREEVIEWLOGOS, que es justamente para eso.

   SEGURIDAD DEL CAMBIO
   Se verificó que este SP lo consume UN solo lugar en todo el sistema:
   MenuService (ruta /seguridad/roles-usuarios-aplicaciones-opciones-botones/treeview),
   y ese consumidor ya pone los íconos en null. La pantalla de administración
   de opciones usa OTRO SP distinto (AplicacionesOpcionesTreeview), que no se
   toca acá.

   Dapper mapea por nombre de columna: si la columna deja de venir, la
   propiedad del DTO simplemente queda en null. No hay que tocar C#.

   NO PROBADO CONTRA UN SERVIDOR — revisar y ejecutar en SSMS.
   Lo único que cambia respecto del original son las 2 columnas de ícono
   quitadas (y la coma de la línea anterior en cada SELECT).
   ============================================================================ */

CREATE OR ALTER PROCEDURE [dbo].[PROC_SEGURIDAD_ROLES_USUARIOS_APLICACIONES_OPCIONES_BOTONES_TREEVIEW]
        (@COD_ROL_USUARIO COD_ROL_USUARIO)
AS
BEGIN
SET NOCOUNT ON;

-- 1) APLICACIONES  (se quitó b.[IMG_ICONO] AS [IMG_ICONO_APLICACION])
SELECT  a.[COD_APLICACION],
        b.[DES_APLICACION],
        b.[DES_BACKCOLOR]               AS [DES_BACKCOLOR_APLICACION],
        b.[DES_FORECOLOR]               AS [DES_FORECOLOR_APLICACION]
FROM    [SEGURIDAD].[ROLES_USUARIO_APLICACIONES]        a
INNER JOIN [SEGURIDAD].[APLICACIONES]                   b ON a.[COD_APLICACION]= b.[COD_APLICACION]
WHERE   a.[COD_ROL_USUARIO] = @COD_ROL_USUARIO

-- 2) OPCIONES  (se quitó d.[IMG_ICONO] AS [IMG_ICONO_OPCION])
SELECT  a.[COD_APLICACION],
        d.[COD_TIPO_OPCION_APLICACION],
        f.[DES_TIPO_OPCION_APLICACION],
        d.[COD_OPCION_APLICACION_PADRE],
        a.[COD_OPCION_APLICACION],
        d.[DES_OPCION_APLICACION],
        d.[DES_FORMULARIO],
        d.[DES_PARAMETRO],
        d.[FLG_TECLADO_VIRTUAL],
        d.[DES_BACKCOLOR]               AS [DES_BACKCOLOR_OPCION],
        d.[DES_FORECOLOR]               AS [DES_FORECOLOR_OPCION]
FROM    [SEGURIDAD].[ROLES_USUARIOS_APLICACIONES_OPCIONES]              a
INNER JOIN [SEGURIDAD].[APLICACIONES_OPCIONES]                          d ON a.[COD_APLICACION]=d.[COD_APLICACION] AND a.[COD_OPCION_APLICACION]=d.[COD_OPCION_APLICACION]
INNER JOIN [SEGURIDAD].[APLICACIONES]                                   e ON a.[COD_APLICACION]=e.[COD_APLICACION]
INNER JOIN [SEGURIDAD].[TIPOS_OPCIONES]                                 f ON d.[COD_TIPO_OPCION_APLICACION]=f.[COD_TIPO_OPCION_APLICACION]
WHERE   a.[COD_ROL_USUARIO] = @COD_ROL_USUARIO
ORDER BY A.COD_APLICACION, A.COD_OPCION_APLICACION

-- 3) BOTONES  (sin cambios — este resultado nunca trajo íconos)
SELECT  a.[COD_APLICACION],
        d.[COD_OPCION_APLICACION_PADRE],
        a.[COD_OPCION_APLICACION],
        b.[COD_BOTON_OPCION],
        c.[DES_BOTON_OPCION],
        c.[DES_TOOLTIPTEXT],
        c.[DES_ALIAS],
        d.[FLG_TECLADO_VIRTUAL],
        b.[FLG_HABILITADO],
        c.[FLG_HEADER],
        c.[FLG_GRILLA],
        c.[FLG_ACCION_PUT],
        c.[FLG_ACCION_POST],
        c.[FLG_ACCION_DELETE],
        c.[FLG_ACCION_GET],
        c.[DES_METODO]
FROM    [SEGURIDAD].[ROLES_USUARIOS_APLICACIONES_OPCIONES]              a
LEFT  JOIN [SEGURIDAD].[ROLES_USUARIOS_APLICACIONES_OPCIONES_BOTONES]   b ON a.[COD_ROL_USUARIO]=b.[COD_ROL_USUARIO] AND a.[COD_APLICACION]=b.[COD_APLICACION] AND a.[COD_OPCION_APLICACION]=b.[COD_OPCION_APLICACION]
LEFT  JOIN [SEGURIDAD].[APLICACIONES_OPCIONES_BOTONES]                  c ON b.[COD_APLICACION]=c.[COD_APLICACION] AND b.[COD_OPCION_APLICACION]=c.[COD_OPCION_APLICACION] AND b.[COD_BOTON_OPCION]=c.[COD_BOTON_OPCION]
INNER JOIN [SEGURIDAD].[APLICACIONES_OPCIONES]                          d ON a.[COD_APLICACION]=d.[COD_APLICACION] AND a.[COD_OPCION_APLICACION]=d.[COD_OPCION_APLICACION]
INNER JOIN [SEGURIDAD].[APLICACIONES]                                   e ON a.[COD_APLICACION]=e.[COD_APLICACION]
INNER JOIN [SEGURIDAD].[TIPOS_OPCIONES]                                 f ON d.[COD_TIPO_OPCION_APLICACION]=f.[COD_TIPO_OPCION_APLICACION]
WHERE   a.[COD_ROL_USUARIO] = @COD_ROL_USUARIO
ORDER BY A.COD_APLICACION, A.COD_OPCION_APLICACION
END;
GO
