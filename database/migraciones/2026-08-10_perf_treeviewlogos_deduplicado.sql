/* ============================================================================
   PRIORIDAD 1 — Deduplicar los íconos de TREEVIEWLOGOS

   MEDICIÓN REAL (rol SEG, tomada con 01_medir_iconos.sql):

       Botones      538 filas →  34 íconos distintos → 13,66 MB  (1,39 MB si se deduplica)
       Opciones     186 filas →  71 íconos distintos →  5,44 MB
       Aplicaciones  14 filas →  14 íconos distintos →  0,44 MB
       ------------------------------------------------------------
       TOTAL                                          19,55 MB binario
                                                    ≈ 26 MB en JSON (Base64 +33%)

       Íconos más repetidos:
           NUEVO      × 122 copias de 75,58 KB →  9.145 KB desperdiciados
           MODIFICAR  × 123 copias de 12,98 KB →  1.583 KB
           CONSULTAR  × 124 copias de  6,41 KB →    788 KB
           ELIMINAR   × 120 copias de  6,47 KB →    770 KB

   QUÉ HACE ESTE CAMBIO
   Cada imagen viaja UNA sola vez. El SP pasa de 3 a 4 resultados: los tres
   primeros ya no llevan el blob sino un ICONO_ID (entero, asignado por
   CONTENIDO de la imagen), y el cuarto trae cada imagen distinta una única vez.

   El cliente reconstruye el mapa. Es un cambio ADITIVO: los DTO conservan sus
   campos actuales, solo se agrega ICONO_ID y la lista Iconos.

   RESULTADO ESPERADO: ~19,55 MB → ~3,2 MB binario (≈ 4,3 MB en JSON).
   Sobre los 54,7 s medidos, la mayor parte era transferencia + deserialización
   Base64 en el navegador (que es mono-hilo en WASM).

   REQUIERE los cambios de C# que van junto con este script:
       API : Domain/Models/Seguridad/RolesUsuariosAplicacionesOpcionesBotones.cs
             Infrastructure/Repositories/RolesUsuariosAplicacionesOpcionesBotonesRepository.cs
       Web : KonSolutions.Shared/Models/Menu.cs
             KonSolutions.Web/Services/MenuService.cs
   Si ejecutás este SP SIN desplegar el C# nuevo, los íconos dejan de verse
   (el menú sigue funcionando, solo queda sin imágenes). Desplegar juntos.

   NO PROBADO CONTRA UN SERVIDOR — revisar y ejecutar en SSMS.
   ============================================================================ */

CREATE OR ALTER PROCEDURE [dbo].[PROC_SEGURIDAD_ROLES_USUARIOS_APLICACIONES_OPCIONES_BOTONES_TREEVIEWLOGOS]
        (@COD_ROL_USUARIO COD_ROL_USUARIO)
AS
BEGIN
SET NOCOUNT ON;

    /* ------------------------------------------------------------------
       1) Juntar todos los íconos del rol en una sola tabla temporal, con
          el hash de su contenido. El tipo 'image' no admite comparación
          directa (=, DISTINCT, GROUP BY), por eso se agrupa por HASHBYTES
          sobre VARBINARY(MAX).
       ------------------------------------------------------------------ */
    CREATE TABLE #Origen
    (
        Origen                CHAR(3)        NOT NULL,   -- APP | OPC | BOT
        COD_APLICACION        NVARCHAR(5)    NULL,
        COD_OPCION_APLICACION NVARCHAR(30)   NULL,
        COD_BOTON_OPCION      NVARCHAR(2)    NULL,
        Hash                  VARBINARY(32)  NULL,
        IMG_ICONO             VARBINARY(MAX) NULL
    );

    -- Aplicaciones del rol
    INSERT INTO #Origen (Origen, COD_APLICACION, Hash, IMG_ICONO)
    SELECT  'APP',
            a.[COD_APLICACION],
            CASE WHEN b.[IMG_ICONO] IS NULL THEN NULL
                 ELSE HASHBYTES('SHA2_256', CAST(b.[IMG_ICONO] AS VARBINARY(MAX))) END,
            CAST(b.[IMG_ICONO] AS VARBINARY(MAX))
    FROM    [SEGURIDAD].[ROLES_USUARIO_APLICACIONES]    a
    INNER JOIN [SEGURIDAD].[APLICACIONES]               b ON a.[COD_APLICACION] = b.[COD_APLICACION]
    WHERE   a.[COD_ROL_USUARIO] = @COD_ROL_USUARIO;

    -- Opciones del rol
    INSERT INTO #Origen (Origen, COD_APLICACION, COD_OPCION_APLICACION, Hash, IMG_ICONO)
    SELECT  'OPC',
            a.[COD_APLICACION],
            a.[COD_OPCION_APLICACION],
            CASE WHEN d.[IMG_ICONO] IS NULL THEN NULL
                 ELSE HASHBYTES('SHA2_256', CAST(d.[IMG_ICONO] AS VARBINARY(MAX))) END,
            CAST(d.[IMG_ICONO] AS VARBINARY(MAX))
    FROM    [SEGURIDAD].[ROLES_USUARIOS_APLICACIONES_OPCIONES]  a
    INNER JOIN [SEGURIDAD].[APLICACIONES_OPCIONES]              d ON a.[COD_APLICACION] = d.[COD_APLICACION]
                                                                 AND a.[COD_OPCION_APLICACION] = d.[COD_OPCION_APLICACION]
    WHERE   a.[COD_ROL_USUARIO] = @COD_ROL_USUARIO;

    -- Botones del rol
    INSERT INTO #Origen (Origen, COD_APLICACION, COD_OPCION_APLICACION, COD_BOTON_OPCION, Hash, IMG_ICONO)
    SELECT  'BOT',
            a.[COD_APLICACION],
            a.[COD_OPCION_APLICACION],
            b.[COD_BOTON_OPCION],
            CASE WHEN c.[IMG_ICONO] IS NULL THEN NULL
                 ELSE HASHBYTES('SHA2_256', CAST(c.[IMG_ICONO] AS VARBINARY(MAX))) END,
            CAST(c.[IMG_ICONO] AS VARBINARY(MAX))
    FROM    [SEGURIDAD].[ROLES_USUARIOS_APLICACIONES_OPCIONES]              a
    INNER JOIN [SEGURIDAD].[ROLES_USUARIOS_APLICACIONES_OPCIONES_BOTONES]   b
            ON a.[COD_ROL_USUARIO] = b.[COD_ROL_USUARIO]
           AND a.[COD_APLICACION] = b.[COD_APLICACION]
           AND a.[COD_OPCION_APLICACION] = b.[COD_OPCION_APLICACION]
    INNER JOIN [SEGURIDAD].[APLICACIONES_OPCIONES_BOTONES]                  c
            ON b.[COD_APLICACION] = c.[COD_APLICACION]
           AND b.[COD_OPCION_APLICACION] = c.[COD_OPCION_APLICACION]
           AND b.[COD_BOTON_OPCION] = c.[COD_BOTON_OPCION]
    WHERE   a.[COD_ROL_USUARIO] = @COD_ROL_USUARIO;

    /* ------------------------------------------------------------------
       2) Un id por CONTENIDO. Dos íconos byte a byte iguales comparten id,
          sin importar de qué aplicación/opción/botón vengan.
       ------------------------------------------------------------------ */
    SELECT  Hash,
            ICONO_ID = DENSE_RANK() OVER (ORDER BY Hash)
    INTO    #Ids
    FROM    (SELECT DISTINCT Hash FROM #Origen WHERE Hash IS NOT NULL) h;

    CREATE UNIQUE CLUSTERED INDEX IX_#Ids ON #Ids (Hash);

    /* ------------------------------------------------------------------
       RESULTADO 1/4 — Aplicaciones (sin blob)
       ------------------------------------------------------------------ */
    SELECT  o.[COD_APLICACION],
            i.[ICONO_ID]
    FROM    #Origen o
    LEFT JOIN #Ids  i ON o.Hash = i.Hash
    WHERE   o.Origen = 'APP';

    /* ------------------------------------------------------------------
       RESULTADO 2/4 — Opciones (sin blob)
       ------------------------------------------------------------------ */
    SELECT  o.[COD_APLICACION],
            o.[COD_OPCION_APLICACION],
            i.[ICONO_ID]
    FROM    #Origen o
    LEFT JOIN #Ids  i ON o.Hash = i.Hash
    WHERE   o.Origen = 'OPC'
    ORDER BY o.[COD_APLICACION], o.[COD_OPCION_APLICACION];

    /* ------------------------------------------------------------------
       RESULTADO 3/4 — Botones (sin blob)
       ------------------------------------------------------------------ */
    SELECT  o.[COD_APLICACION],
            o.[COD_OPCION_APLICACION],
            o.[COD_BOTON_OPCION],
            i.[ICONO_ID]
    FROM    #Origen o
    LEFT JOIN #Ids  i ON o.Hash = i.Hash
    WHERE   o.Origen = 'BOT'
    ORDER BY o.[COD_APLICACION], o.[COD_OPCION_APLICACION];

    /* ------------------------------------------------------------------
       RESULTADO 4/4 — Cada imagen distinta, UNA sola vez.
       Acá está todo el ahorro: 738 blobs → ~119 blobs.
       ------------------------------------------------------------------ */
    SELECT  i.[ICONO_ID],
            [IMG_ICONO] = (SELECT TOP 1 o2.IMG_ICONO
                           FROM   #Origen o2
                           WHERE  o2.Hash = i.Hash)
    FROM    #Ids i
    ORDER BY i.[ICONO_ID];

    DROP TABLE #Ids;
    DROP TABLE #Origen;
END;
GO
