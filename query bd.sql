CREATE DATABASE CarpetaIngSoftware
GO
USE CarpetaIngSoftware
GO


CREATE TABLE Usuarios(
DNI INT PRIMARY KEY NOT NULL,
Nombre varchar(50) NOT NULL,
Apellido varchar(50) NOT NULL,
Mail varchar(100) NOT NULL,
NombreUsuario varchar(50) NOT NULL,
Clave varchar(100) NOT NULL,
Rol varchar(50) NOT NULL,
Bloqueo bit,
Activo bit
);


CREATE TABLE Clientes(
DNICliente INT PRIMARY KEY NOT NULL,
Nombre varchar(50) NOT NULL,
Apellido varchar(50) NOT NULL,
Mail varchar(100) NOT NULL,
Direccion varchar(150) NOT NULL
)



CREATE TABLE Productos(
CodigoProducto varchar(14) PRIMARY KEY NOT NULL,
Modelo varchar(50) NOT NULL,
Descripcion varchar(100) NOT NULL,
Marca varchar(50) NOT NULL,
Color varchar(50) NOT NULL,
Precio float NOT NULL,
Stock smallint NOT NULL,
Almacenamiento smallint NOT NULL
)

CREATE TABLE Facturas(
NumFactura INT PRIMARY KEY IDENTITY(1,1),
DNICliente INT FOREIGN KEY REFERENCES Clientes(DNICliente),
NumeroTransaccion INT,
MontoTotal float,
Impuesto float,
Fecha varchar(50),
MetodoPago varchar(50),
MarcaTarjeta varchar(50),
NumTarjeta varchar(100), /*Se encripta*/
CantCuotas smallint,
AliasMP varchar(50) NULL,
ComentarioAdicional varchar(100) NULL
)
GO

CREATE TABLE Item_Factura
(
Item INT PRIMARY KEY IDENTITY(1,1),
NumFactura INT FOREIGN KEY REFERENCES Facturas(NumFactura),
CodigoProducto varchar(14) FOREIGN KEY REFERENCES Productos(CodigoProducto),
Cant smallint,
SubTotal float
)
GO

CREATE PROCEDURE VerificarUsuario
    @NombreUsuario varchar(50),
	@DNI int,
	@Email varchar(50)
AS
BEGIN

    SELECT * FROM Usuarios WHERE NombreUsuario = @NombreUsuario OR DNI = @DNI OR Mail = @Email;
END
GO


CREATE PROCEDURE ModificarBloquearUsuario
    @DNI int,
	@Bloqueo bit
AS
BEGIN

    UPDATE Usuarios SET Bloqueo= @Bloqueo WHERE DNI = @DNI;
END
GO

CREATE PROCEDURE RegistrarUsuario
    @DNI int,
	@Nombre varchar(50),
	@Apellido varchar(50),
	@Mail varchar(50),
	@NombreUsuario varchar(50),
	@Clave varchar(100),
	@Rol varchar(50)
AS
BEGIN

    INSERT INTO Usuarios VALUES (@DNI, @Nombre, @Apellido, @Mail, @NombreUsuario, @Clave, @Rol, 0 ,1)
END
GO



CREATE PROCEDURE ModificarUsuario
    @DNI int,
	@Nombre varchar(50),
	@Apellido varchar(50),
	@Mail varchar(50),
	@NombreUsuario varchar(50),
	@Rol varchar(50),
	@Bloqueo bit,
	@UltimoDNI int
AS
BEGIN
    UPDATE  Usuarios SET DNI=@DNI, Nombre = @Nombre, Apellido = @Apellido, Mail = @Mail, NombreUsuario = @NombreUsuario, Rol = @Rol, Bloqueo = @Bloqueo where DNI = @UltimoDNI;
END
GO


CREATE PROCEDURE EliminarUsuario
    @DNI int
AS
BEGIN
    UPDATE Usuarios SET Activo = 0 where DNI = @DNI;
END
GO

CREATE PROCEDURE ActivarUsuario
    @DNI int
AS
BEGIN
    UPDATE Usuarios SET Activo = 1 where DNI = @DNI;
END
GO

CREATE PROCEDURE CambiarClaveUsuario
    @DNI int,
	@Clave varchar(100)
AS
BEGIN
    UPDATE  Usuarios SET Clave = @Clave where DNI = @DNI;
END
GO

CREATE PROCEDURE RegistrarCliente
    @DNI int,
	@Nombre varchar(50),
	@Apellido varchar(50),
	@Mail varchar(50),
	@Direccion varchar(150)
AS
BEGIN

    INSERT INTO Clientes VALUES (@DNI, @Nombre, @Apellido, @Mail, @Direccion)
END
GO

CREATE PROCEDURE EliminarCliente
    @DNI int
AS
BEGIN
    DELETE FROM Clientes WHERE DNICliente = @DNI;
END
GO

CREATE PROCEDURE ModificarCliente
    @DNI int,
	@Nombre varchar(50),
	@Apellido varchar(50),
	@Mail varchar(50),
	@Direccion varchar(150)
AS
BEGIN
    UPDATE Clientes SET Nombre= @Nombre, Apellido = @Apellido, Mail = @Mail, Direccion = @Direccion where DNICliente= @DNI
END
GO


CREATE PROCEDURE TraerUltimoIDFactura
AS
BEGIN
SELECT IDENT_CURRENT( 'Facturas' )  
END 
GO

/*
/*Solo registra la fecha. Luego se le modifica los datos del pago. Devuelve el ID NumFactura generado*/
CREATE PROCEDURE RegistrarFactura
	@DNICliente int,
    @Fecha varchar(50)
AS
BEGIN
    INSERT INTO Facturas VALUES (@DNICliente,null,null,null,@Fecha,null,null,null,null,null, null);
	SELECT SCOPE_IDENTITY() 
END
GO*/
CREATE PROCEDURE RegistrarFactura
	@DNICliente int,
    @NumTransaccion int,
	@MontoTotal float,
	@Impuesto float,
    @Fecha varchar(50),
	@MetodoPago varchar(50),
	@MarcaTarjeta varchar(50),
	@NumTarjeta varchar(100),
	@CantCuotas smallint,
	@AliasMP varchar(50) NULL,
	@ComentarioAdicional varchar(100) NULL
AS
BEGIN
    INSERT INTO Facturas VALUES (@DNICliente, @NumTransaccion, @MontoTotal, @Impuesto, @Fecha, @MetodoPago, @MarcaTarjeta, @NumTarjeta, @CantCuotas, @AliasMP, @ComentarioAdicional);
END
GO

CREATE PROCEDURE RegistrarItemFactura
    @NumFactura int,
	@CodigoProducto varchar(14),
	@Cant int,
	@SubTotal float
AS
BEGIN
    INSERT INTO Item_Factura VALUES (@NumFactura,@CodigoProducto,@Cant,@SubTotal);
END
GO

CREATE PROCEDURE ModificarStock
	@CodigoProducto varchar(14),
	@Stock int
AS
BEGIN
    UPDATE Productos SET Stock = @Stock WHERE CodigoProducto = @CodigoProducto;
END
GO

CREATE PROCEDURE RegistrarProducto
	@CodigoProducto varchar(14),
	@Modelo varchar(50),
	@Descripcion varchar(50),
	@Marca varchar(50),
	@Color varchar(50),
	@Precio float,
	@Stock smallint,
	@Almacenamiento smallint

AS
BEGIN
    INSERT INTO Productos VALUES (@CodigoProducto,@Modelo,@Descripcion,@Marca,@Color,@Precio,@Stock, @Almacenamiento)
END
GO

CREATE PROCEDURE EliminarProducto
	@CodigoProducto varchar(14)
AS
BEGIN
    DELETE FROM Productos WHERE CodigoProducto = @CodigoProducto;
END
GO

CREATE PROCEDURE ModificarProducto
	@CodigoProducto varchar(14),
	@Modelo varchar(50),
	@Descripcion varchar(50),
	@Marca varchar(50),
	@Color varchar(50),
	@Precio float,
	@Stock smallint,
	@Almacenamiento smallint

AS
BEGIN
    UPDATE Productos SET Modelo = @Modelo, Descripcion = @Descripcion, Marca = @Marca, Color = @Color, Precio = @Precio, Stock = @Stock, Almacenamiento = @Almacenamiento where CodigoProducto = @CodigoProducto
END
GO

/*CLAVE clave123*/
INSERT INTO Usuarios VALUES (12345678, 'Admin', 'Admin', 'admin@gmail.com', 'Admin', '5ac0852e770506dcd80f1a36d20ba7878bf82244b836d9324593bd14bc56dcb5', 'Admin', 0, 1);
INSERT INTO Productos VALUES (123, 'Iphone 15 Pro','Chip A17 Pro, 8GB Ram, OLED 6.1 pulgadas, Camara 48 MP', 'Apple', 'Blanco', 1100, 20, 256);
INSERT INTO Productos VALUES (456, 'Samsung S24 Ultra','Chip Octa-Coree, 8GB Ram, Bateria 5000 mAh, Camra 50MP','Samsung', 'Negro', 1300, 26, 512);
INSERT INTO Clientes VALUES (34789332, 'Esteban', 'Rodriguez', 'estebanrodriguez@gmail.com', 'Jose Paz 678');
INSERT INTO Clientes VALUES (29145876, 'Marcos', 'Diaz', 'marcosdiaz@gmail.com', 'Av. Olivos 222');

