/* =============================================================================
   PROC_COMERCIAL_PEDIDOS_COMERCIALES_UPDATE — versión corregida
   -----------------------------------------------------------------------------
   El CATCH ya lo arreglaste. Lo que faltaba es lo que hacía que el mensaje
   llegara vacío, que es otra cosa:

        DECLARE @DES_ERROR_MOTOR DES_2XL          -- nunca se le asigna nada
        ...
        SET @DES_MENSAJE_ERROR = @DES_MENSAJE_IMPOSIBLE_ELIMINAR_PROVIENE_PROFORMA
                               + SPACE(1) + @DES_ERROR_MOTOR;

   'texto' + NULL = NULL. Entonces @DES_MENSAJE_ERROR queda NULL y
   RAISERROR(NULL, 16, 1) levanta el error 50000 CON EL MENSAJE VACÍO. Eso es lo
   que se ve en el Response: {"errorNumber":50000, "errorMessage":""}. El texto
   largo que aparece en el cartel es el dump de la sentencia que arma el
   repositorio para poder diagnosticar — no es el mensaje del SP, porque mensaje
   no hay.

   @DES_ERROR_MOTOR tiene sentido en los SP de DELETE, donde se llena con
   ERROR_MESSAGE() DENTRO del CATCH para pegarle al mensaje del sistema el detalle
   del motor (la FK que impidió borrar). Acá se usa en una validación de negocio,
   antes de que el motor haya fallado: no hay nada que agregar.

   CAMBIOS
   -------
   1) El mensaje va solo, sin concatenar @DES_ERROR_MOTOR. Se eliminaron esa
      variable y @NUM_ERROR, que tampoco se usaban.

   2) La validación se movió ANTES del BEGIN TRANSACTION. Dentro del TRY el
      RETURN era código muerto: el RAISERROR ya había saltado al CATCH. Y de paso
      no se abre una transacción para descartarla enseguida.

   3) Lo mismo para el control de @@ROWCOUNT = 0 tras el UPDATE: ese RAISERROR
      también salta al CATCH, así que el RETURN que le sigue nunca corría. Se
      dejó el RAISERROR solo (el CATCH hace el rollback y el THROW).

   El CATCH quedó como lo dejaste, que ya está bien.
   ========================================================================== */
ALTER   PROCEDURE [dbo].[PROC_COMERCIAL_PEDIDOS_COMERCIALES_UPDATE]
(
		@COD_CLASE_OPERACION		COD_CLASE_OPERACION,
		@COD_TIPO_OPERACION		COD_TIPO_OPERACION,
		@NUM_PEDIDO_COMERCIAL		NUM_PEDIDO_COMERCIAL,
		@DES_PEDIDO_COMERCIAL		DES_LARGE,
		@COD_TIPO_COMPROBANTE_PAGO		COD_TIPO_COMPROBANTE_PAGO,
		@NUM_DOCUMENTO		NUM_DOCUMENTO,
		@NUM_PROYECTO		NUM_PROYECTO,
		@COD_TIPO_PEDIDO_COMERCIAL		COD_TIPO_PEDIDO_COMERCIAL,
		@FEC_PEDIDO		FEC_DATE,
		@FEC_VECIMIENTO		FEC_DATE,
		@FEC_ENTREGA		FEC_DATE,
		@FEC_ENTREGA_PUBLICA	FEC_DATE,
		@COD_TIPO_ENTIDAD		COD_TIPO_ENTIDADES,
		@COD_ENTIDAD		COD_ENTIDAD,
		@NUM_TEMPORADA		NUM_TEMPORADA,
		@NUM_COLECCION		NUM_COLECCION,
		@COD_MONEDA		COD_MONEDA,
		@FAC_TIPO_CAMBIO		VAL_DECIMAL,
		@NUM_UNIDAD_COMERCIAL		NUM_UNIDAD_COMERCIAL,
		@NUM_SECUENCIA_DIRECCION		NUM_SECUENCIA,
		@COD_CENTRO_COSTO_SOLICITANTE		COD_CENTRO_COSTO,
		@COD_CENTRO_COSTO_DESTINO		COD_CENTRO_COSTO,
		@COD_SUCURSAL		COD_SUCURSAL,
		@COD_FORMA_PAGO		COD_FORMA_PAGO,
		@COD_CONDICION_PAGO		COD_CONDICION_PAGO,
		@COD_TIPO_DESCUENTO		COD_TIPO_DESCUENTO,
		@TAS_DESCUENTO		TAS_PORCENTUAL,
		@VAL_TOTAL		VAL_PRECIO,
		@COD_ENTIDAD_VENDEDOR		COD_ENTIDAD,
		@NUM_PURCHASE_ORDER		NUM_PURCHASE_ORDER,
		@COD_PAIS_DESTINO		COD_PAIS,
		@NUM_LOTE_CONTROL		NUM_LOTE_CONTROL,
		@NUM_RENDICION_CUENTAS		NUM_RENDICION_CUENTAS,
		@COD_TIPO_MOTIVO		COD_TIPO_MOTIVO,
		@COD_MOTIVO		COD_MOTIVO,
		@DES_OBSERVACION		DES_2XL,
		@NUM_PEDIDO_COMERCIAL_RELACIONADO		NUM_PEDIDO_COMERCIAL,
		@NUM_TRACKING		NUM_TRACKING,
		@COD_INCOTERM		COD_INCOTERM,
		@COD_TIPO_ESTADO		COD_TIPO_ESTADO,
		@COD_ESTADO		COD_ESTADO,
		@COD_USUARIO_REGISTRO		COD_USUARIO,
		@COD_ESTACION_REGISTRO		COD_ESTACION
)
AS
SET NOCOUNT ON
SET ANSI_NULLS OFF
SET XACT_ABORT ON
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
SET QUOTED_IDENTIFIER OFF
BEGIN
	DECLARE	@FEC_REGISTRO											FEC_DATE_TIME = GETDATE()
	DECLARE	@DES_MENSAJE_REGISTRO_INEXISTENTE						DES_2XL = dbo.FNCN_MENSAJE_DESCRIPCION('KEY_REGISTRO_INEXISTENTE')
	DECLARE @DES_MENSAJE_IMPOSIBLE_ELIMINAR_PROVIENE_PROFORMA		DES_2XL = 'IMPOSIBLE MODIFICAR, PROVIENE DE UNA PROFORMA'
	DECLARE @DES_MENSAJE_ERROR										DES_2XL	= SPACE(0)

	-----------------------------------------------------------
	-- VALIDACIÓN DE NEGOCIO — fuera de la transacción.
	-- El mensaje va SOLO: concatenarle @DES_ERROR_MOTOR (que acá es NULL) lo
	-- anulaba entero y el usuario recibía un cartel sin texto.
	-----------------------------------------------------------
	IF	EXISTS(SELECT	1
			   FROM		COMERCIAL.PEDIDOS_COMERCIALES A
			   WHERE	A.COD_CLASE_OPERACION							= @COD_CLASE_OPERACION
			   AND		A.COD_TIPO_OPERACION							= @COD_TIPO_OPERACION
			   AND		A.NUM_PEDIDO_COMERCIAL							= @NUM_PEDIDO_COMERCIAL
			   AND		ISNULL(A.NUM_PEDIDO_COMERCIAL_RELACIONADO,'')	!= '')
		BEGIN
		SET	@DES_MENSAJE_ERROR = @DES_MENSAJE_IMPOSIBLE_ELIMINAR_PROVIENE_PROFORMA;
		RAISERROR(@DES_MENSAJE_ERROR, 16, 1);
		RETURN;
	END

    BEGIN TRY
		BEGIN TRANSACTION
        -----------------------------------------------------------
        -- Modificación. NO da de alta: si la clave no existe, es un error.
        -----------------------------------------------------------
        UPDATE	COMERCIAL.PEDIDOS_COMERCIALES
        SET				[DES_PEDIDO_COMERCIAL]	= @DES_PEDIDO_COMERCIAL,
				[COD_TIPO_COMPROBANTE_PAGO]	= @COD_TIPO_COMPROBANTE_PAGO,
				[NUM_DOCUMENTO]	= @NUM_DOCUMENTO,
				[NUM_PROYECTO]	= @NUM_PROYECTO,
				[COD_TIPO_PEDIDO_COMERCIAL]	= @COD_TIPO_PEDIDO_COMERCIAL,
				[FEC_PEDIDO]	= @FEC_PEDIDO,
				[FEC_VECIMIENTO]	= @FEC_VECIMIENTO,
				[FEC_ENTREGA]	= @FEC_ENTREGA,
				[FEC_ENTREGA_PUBLICA]	= @FEC_ENTREGA_PUBLICA,
				[COD_TIPO_ENTIDAD]	= @COD_TIPO_ENTIDAD,
				[COD_ENTIDAD]	= @COD_ENTIDAD,
				[NUM_TEMPORADA]	= @NUM_TEMPORADA,
				[NUM_COLECCION]	= @NUM_COLECCION,
				[COD_MONEDA]	= @COD_MONEDA,
				[FAC_TIPO_CAMBIO]	= @FAC_TIPO_CAMBIO,
				[NUM_UNIDAD_COMERCIAL]	= @NUM_UNIDAD_COMERCIAL,
				[NUM_SECUENCIA_DIRECCION]	= @NUM_SECUENCIA_DIRECCION,
				[COD_CENTRO_COSTO_SOLICITANTE]	= @COD_CENTRO_COSTO_SOLICITANTE,
				[COD_CENTRO_COSTO_DESTINO]	= @COD_CENTRO_COSTO_DESTINO,
				[COD_SUCURSAL]	= @COD_SUCURSAL,
				[COD_FORMA_PAGO]	= @COD_FORMA_PAGO,
				[COD_CONDICION_PAGO]	= @COD_CONDICION_PAGO,
				[COD_TIPO_DESCUENTO]	= @COD_TIPO_DESCUENTO,
				[TAS_DESCUENTO]	= @TAS_DESCUENTO,
				[VAL_TOTAL]	= @VAL_TOTAL,
				[COD_ENTIDAD_VENDEDOR]	= @COD_ENTIDAD_VENDEDOR,
				[NUM_PURCHASE_ORDER]	= @NUM_PURCHASE_ORDER,
				[COD_PAIS_DESTINO]	= @COD_PAIS_DESTINO,
				[NUM_LOTE_CONTROL]	= @NUM_LOTE_CONTROL,
				[NUM_RENDICION_CUENTAS]	= @NUM_RENDICION_CUENTAS,
				[COD_TIPO_MOTIVO]	= @COD_TIPO_MOTIVO,
				[COD_MOTIVO]	= @COD_MOTIVO,
				[DES_OBSERVACION]	= @DES_OBSERVACION,
				[NUM_PEDIDO_COMERCIAL_RELACIONADO]	= @NUM_PEDIDO_COMERCIAL_RELACIONADO,
				[NUM_TRACKING]	= @NUM_TRACKING,
				[COD_INCOTERM]	= @COD_INCOTERM,
				[COD_TIPO_ESTADO]	= @COD_TIPO_ESTADO,
				[COD_ESTADO]	= @COD_ESTADO,
				[COD_USUARIO_ACTUALIZACION]	= @COD_USUARIO_REGISTRO,
				[COD_ESTACION_ACTUALIZACION]	= @COD_ESTACION_REGISTRO,
				[FEC_ACTUALIZACION]	= @FEC_REGISTRO
        WHERE	[COD_CLASE_OPERACION] = @COD_CLASE_OPERACION
		AND		[COD_TIPO_OPERACION] = @COD_TIPO_OPERACION
		AND		[NUM_PEDIDO_COMERCIAL] = @NUM_PEDIDO_COMERCIAL;

		-- Sin RETURN: estando dentro del TRY, el RAISERROR ya salta al CATCH y el
		-- RETURN nunca se ejecutaba. El CATCH hace el rollback y el THROW.
        IF	@@ROWCOUNT = 0
			RAISERROR(@DES_MENSAJE_REGISTRO_INEXISTENTE, 16, 1);

		COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
		-----------------------------------------------------------
		-- SIN el SELECT de columnas de error: ese resultset era lo que leía el
		-- repositorio (QueryFirstOrDefault<WriteResult>), y por eso llegaban
		-- errorNumber/errorProcedure pero el mensaje vacío. Dejando solo el
		-- THROW, la excepción llega como SqlException y la aplicación muestra
		-- el texto real del RAISERROR.
		--
		-- El THROW va FUERA del IF: si la transacción ya no existía, antes no se
		-- lanzaba nada y el error se perdía en silencio.
		-----------------------------------------------------------
		IF	@@TRANCOUNT > 0
			ROLLBACK TRANSACTION;

		THROW;
    END CATCH
END
GO
