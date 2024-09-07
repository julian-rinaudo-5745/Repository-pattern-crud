SET DATEFORMAT DMY;
CREATE DATABASE facturacion;
GO
USE facturacion
GO

CREATE TABLE articulos (
	id int primary key identity(1,1),
	nombre varchar(50) not null,
	precio_unitario decimal(10,2) not null
)

CREATE TABLE formas_pago (
	id int primary key identity(1,1),
	nombre varchar(50) not null
)

CREATE TABLE facturas (
	nro_factura int primary key identity(1,1),
	id_forma_pago int not null,
	cliente varchar(50) not null,
	fecha datetime not null

	CONSTRAINT fk_facturas_formas_pago FOREIGN KEY (id_forma_pago)
		REFERENCES formas_pago (id)
)

CREATE TABLE detalles (
	id int primary key identity(1,1),
	nro_factura int not null,
	id_articulo int not null,
	cantidad int not null,
	precio_venta decimal(10,2) not null

	CONSTRAINT fk_detalles_facturas FOREIGN KEY (nro_factura)
		REFERENCES facturas (nro_factura),
	CONSTRAINT fk_detalles_articulos FOREIGN KEY (id_articulo)
		REFERENCES articulos (id)
)
GO

CREATE PROCEDURE SP_OBTENER_ARTICULOS 
AS
BEGIN
	SELECT * FROM articulos;
END
GO

CREATE PROCEDURE SP_OBTENER_ARTICULO
	@id int
AS
BEGIN
	SELECT * FROM articulos where id = @id;
END
GO

CREATE PROCEDURE SP_INSERTAR_ARTICULO
	@nombre varchar(50),
	@precio_unitario decimal(10,2)		
AS
BEGIN
	INSERT INTO articulos (nombre, precio_unitario) 
	VALUES (@nombre, @precio_unitario)
END

GO

CREATE PROCEDURE SP_EDITAR_ARTICULO
    @id INT,
    @nombre VARCHAR(50) = NULL,
    @precio_unitario DECIMAL(10,2) = NULL
AS
BEGIN
    UPDATE articulos
    SET
        nombre = COALESCE(@nombre, nombre), --Si no viene ningun valor, toma el que tiene por defecto.
        precio_unitario = COALESCE(@precio_unitario, precio_unitario)
    WHERE id = @id;
END

GO

CREATE PROCEDURE SP_ELIMINAR_ARTICULO 
	@id INT
AS
BEGIN
	DELETE articulos where id = @id
END

GO

CREATE PROCEDURE SP_OBTENER_FORMAS_PAGO
AS
BEGIN
	SELECT * FROM formas_pago
END
GO

CREATE PROCEDURE SP_OBTENER_FORMA_PAGO
	@id int
AS
BEGIN
	SELECT * FROM formas_pago WHERE id = @id
END
GO

CREATE PROCEDURE SP_INSERTAR_FORMA_PAGO
	@nombre varchar(50)
AS
BEGIN
	INSERT INTO formas_pago (nombre)
	VALUES (@nombre)
END

GO

CREATE PROCEDURE SP_EDITAR_FORMA_PAGO
	@id int,
	@nombre varchar(50)
AS
BEGIN
	UPDATE formas_pago
		SET nombre = @nombre
	WHERE id = @id
END

GO

CREATE PROCEDURE SP_ELIMINAR_FORMA_PAGO
	@id int
AS
BEGIN
	DELETE formas_pago WHERE id = @id
END

GO
CREATE PROCEDURE SP_OBTENER_FACTURAS
AS
BEGIN
	SELECT * FROM facturas;
END

GO
CREATE PROCEDURE SP_CREAR_FACTURA
	@id_forma_pago int,
	@cliente varchar(50),
	@nro_factura int output
AS
BEGIN
	INSERT INTO facturas (id_forma_pago, fecha, cliente)
	VALUES (@id_forma_pago, GETDATE(), @cliente)
	SET @nro_factura = SCOPE_IDENTITY()
END

GO

CREATE PROCEDURE SP_EDITAR_FACTURA
	@nro_factura int,
	@fecha datetime = null,
	@id_forma_pago int = null,
	@cliente varchar(50) = null
AS
BEGIN
	UPDATE facturas
	SET fecha = COALESCE(@fecha,fecha),
		id_forma_pago = COALESCE(@id_forma_pago,id_forma_pago),
		cliente = COALESCE(@cliente,cliente)
	WHERE nro_factura = @nro_factura
END

GO
CREATE PROCEDURE SP_ELIMINAR_FACTURA
	@nro_factura int 
AS
BEGIN
	DELETE facturas WHERE nro_factura = @nro_factura
END
GO

CREATE PROCEDURE SP_CREAR_DETALLE
	@nro_factura int,
	@id_articulo int,
	@precio_venta decimal(10,2),
	@cantidad int 
AS
BEGIN
	INSERT INTO detalles (nro_factura, id_articulo, precio_venta, cantidad)
	VALUES (@nro_factura, @id_articulo, @precio_venta, @cantidad)
END
GO

CREATE PROCEDURE SP_OBTENER_DETALLES_POR_NRO_FACTURA
	@nro_factura int
AS
BEGIN
	SELECT * FROM DETALLES WHERE nro_factura = @nro_factura
END
GO

CREATE PROCEDURE SP_EDITAR_DETALLE
	@id int,
	@nro_factura int,
	@id_articulo int = null,
	@precio_venta decimal(10,2) = null,
	@cantidad int = null
AS
BEGIN
	UPDATE detalles
	SET id_articulo = COALESCE(@id_articulo, id_articulo),
		precio_venta = COALESCE(@precio_venta,precio_venta),
		cantidad = COALESCE(@cantidad,cantidad)
	WHERE id = @id and nro_factura = @nro_factura
END
GO

CREATE PROCEDURE SP_ELIMINAR_DETALLE
	@nro_factura int
AS
BEGIN
	DELETE FROM detalles where nro_factura = @nro_factura
END

