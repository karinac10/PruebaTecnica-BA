CREATE DATABASE DBAPI

USE DBAPI

--------------------CREACIÓN DE TABLAS DE BD--------------------
CREATE TABLE CLIENTE(
	id int primary key identity(1,1),
	nombre varchar(50),
	apellido varchar(50)
)

CREATE TABLE TARJETACREDITO(
	id_tarjeta int primary key identity(1,1),
	id_cliente int,
	numero_tarjeta varchar(16) unique, 
	saldo_actual decimal(10,2),
	saldo_disponible decimal(10,2),
	limite_credito decimal(10,2),
	interes_bonificable decimal(5,2),
	CONSTRAINT FK_IDCLIENTE FOREIGN KEY (id_cliente) REFERENCES CLIENTE(id)
)

CREATE TABLE MOVIMIENTO(
	id_movimiento int primary key identity(1,1),
	id_tarjeta int,
	fecha date,
	descripcion varchar(255),
	monto decimal(10,2),
	tipo varchar(50),
	CONSTRAINT FK_TARJETA FOREIGN KEY (id_tarjeta) REFERENCES TARJETACREDITO(id_tarjeta)
)

--------------------INSERT DATOS --------------------
INSERT INTO CLIENTE(nombre, apellido) VALUES ('David', 'Castro')
INSERT INTO CLIENTE(nombre, apellido) VALUES ('José', 'Martínez')


INSERT INTO TARJETACREDITO(id_cliente, numero_tarjeta, limite_credito) VALUES (1, '5454404413332345', 2000)
INSERT INTO TARJETACREDITO(id_cliente, numero_tarjeta, limite_credito) VALUES (2, '5656351652476543', 2000)

INSERT INTO MOVIMIENTO(id_tarjeta, fecha, descripcion, monto, tipo) VALUES (1, '2024-01-16', 'Super Selectos', 34.76, 'Compra')
INSERT INTO MOVIMIENTO(id_tarjeta, fecha, descripcion, monto, tipo) VALUES (1, '2024-01-14', 'Pago de TC', 12.43, 'Pago')


SELECT * FROM CLIENTE
SELECT * FROM TARJETACREDITO

--------------------PROCEDIMIENTOS--------------------

CREATE PROCEDURE SP_LISTADO_TARJETA_CREDITO
AS
BEGIN
	SELECT
		TC.id_tarjeta,
		TC.numero_tarjeta,
		TC.id_cliente,
		C.nombre,
		C.apellido
	FROM TARJETACREDITO TC
	INNER JOIN CLIENTE C ON TC.id_cliente = C.id;
END

EXEC SP_LISTADO_TARJETA_CREDITO

CREATE PROCEDURE SP_GUARDAR_MOVIMIENTO(
@idTarjeta int,
@fechaMovimiento date,
@descripcion varchar(255),
@monto decimal(10,2),
@tipoMovimiento varchar(50) --Este parámetro indica si es "Compra" o "Pago"
)
AS
BEGIN
	BEGIN TRY
		--Verifica si es una compra o pago y realiza la operación
		IF @tipoMovimiento = 'Compra'
		BEGIN
			INSERT INTO MOVIMIENTO(fecha, descripcion, monto, tipo)
			VALUES (@fechaMovimiento, @descripcion, @monto, 'Compra')
		END
		ELSE IF @tipoMovimiento = 'Pago'
		BEGIN 
			INSERT INTO MOVIMIENTO(fecha, descripcion, monto, tipo)
			VALUES (@fechaMovimiento, @descripcion, @monto, 'Pago')
		END 
	END TRY 
	BEGIN CATCH
		SELECT ERROR_MESSAGE() AS ErrorMessage;
	END CATCH
END

SELECT * FROM MOVIMIENTO

CREATE PROCEDURE SP_EDITAR_MOVIMIENTO(
@idMovimiento int,
@fechaMovimiento date null,
@descripcion varchar(255) null,
@monto decimal(10,2) null
)
AS
BEGIN
	UPDATE MOVIMIENTO SET
	fecha = ISNULL(@fechaMovimiento, fecha),
	descripcion = ISNULL(@descripcion,descripcion),
	monto = ISNULL(@monto, monto)
	WHERE id_movimiento = @idMovimiento
END 

CREATE PROCEDURE SP_ELIMINAR_MOVIMIENTO(
@idMovimiento int
)
AS 
BEGIN
	DELETE FROM MOVIMIENTO WHERE id_movimiento = @idMovimiento
END

CREATE PROCEDURE SP_ESTADO_CUENTA
AS
BEGIN
	SELECT
		M.id_movimiento,
		M.id_tarjeta,
		M.fecha,
		M.descripcion,
		M.monto,
		M.tipo
	FROM MOVIMIENTO M
	INNER JOIN TARJETACREDITO TC ON TC.id_tarjeta = M.id_tarjeta;
END

EXEC SP_ESTADO_CUENTA
