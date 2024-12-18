CREATE DATABASE SistemaAltaGama
GO
USE SistemaAltaGama
GO


/* VENTA */
CREATE TABLE Clientes(
DNICliente INT PRIMARY KEY NOT NULL,
Nombre varchar(50) NOT NULL,
Apellido varchar(50) NOT NULL,
Mail varchar(100) NOT NULL,
Direccion varchar(150) NOT NULL,
BorradoLogico bit
)



CREATE TABLE Productos(
CodigoProducto varchar(14) PRIMARY KEY NOT NULL,
Modelo varchar(50) NOT NULL,
Descripcion varchar(100) NOT NULL,
Marca varchar(50) NOT NULL,
Color varchar(50) NOT NULL,
Precio float NOT NULL,
Stock smallint NOT NULL,
StockMinimo smallint NOT NULL,
StockMaximo smallint NOT NULL,
Almacenamiento smallint NOT NULL,
BorradoLogico bit
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
PrecioVenta float
)
GO

CREATE PROCEDURE ValidarUsuario
    @NombreUsuario varchar(50),
	@DNI int,
	@Email varchar(50)
AS
BEGIN
    SELECT * FROM Usuarios U 
	INNER JOIN Roles R ON U.Rol = R.CodRol
	WHERE NombreUsuario = @NombreUsuario OR DNI = @DNI OR Mail = @Email

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

    INSERT INTO Usuarios VALUES (@DNI, @Nombre, @Apellido, @Mail, @NombreUsuario, @Clave, @Rol, 0 ,1,0)
END
GO



CREATE PROCEDURE ModificarUsuario
    @DNI int,
	@Nombre varchar(50),
	@Apellido varchar(50),
	@Mail varchar(50),
	@NombreUsuario varchar(50),
	@Rol varchar(50),
	@Bloqueo bit
AS
BEGIN
    UPDATE  Usuarios SET Nombre = @Nombre, Apellido = @Apellido, Mail = @Mail, NombreUsuario = @NombreUsuario, Rol = @Rol, Bloqueo = @Bloqueo where DNI = @DNI
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



CREATE PROCEDURE ModificarContFallido
    @NombreUsuario varchar(50),
	@ContClaveIncorrecta smallint
AS
BEGIN
    UPDATE Usuarios SET ContFallidos = @ContClaveIncorrecta where NombreUsuario = @NombreUsuario;
END
GO

/*PROC ALMACENADOS clientes*/
CREATE PROCEDURE RegistrarCliente
    @DNI int,
	@Nombre varchar(50),
	@Apellido varchar(50),
	@Mail varchar(50),
	@Direccion varchar(150)
AS
BEGIN

    INSERT INTO Clientes VALUES (@DNI, @Nombre, @Apellido, @Mail, @Direccion,1)
END
GO

CREATE PROCEDURE EliminarCliente
    @DNI int
AS
BEGIN
    UPDATE Clientes SET BorradoLogico = 0 where DNICliente = @DNI;
END
GO


CREATE PROCEDURE HabilitarCliente
    @DNI int
AS
BEGIN
    UPDATE Clientes SET BorradoLogico = 1 where DNICliente = @DNI;
END
GO


CREATE PROCEDURE ModificarCliente
    @DNI int,
	@Nombre varchar(50),
	@Apellido varchar(50),
	@Mail varchar(50),
	@Direccion varchar(150),
	@Borrado bit
AS
BEGIN
    UPDATE Clientes SET Nombre= @Nombre, Apellido = @Apellido, Mail = @Mail, Direccion = @Direccion, BorradoLogico = @Borrado where DNICliente= @DNI
END
GO

CREATE PROCEDURE VerificarCliente
    @DNI int
AS
BEGIN
    SELECT * FROM Clientes WHERE DNICliente = @DNI;
END
GO


CREATE PROCEDURE TraerUltimoNumTransaccion
AS
BEGIN
SELECT TOP 1 NumeroTransaccion from Facturas WHERE NumeroTransaccion IS NOT NULL ORDER BY NumFactura DESC
END 
GO


CREATE PROCEDURE RegistrarFactura
	@DNICliente int,
    @NumTransaccion int,
	@MontoTotal float,
	@Impuesto float,
    @Fecha varchar(50),
	@MetodoPago varchar(50),
	@MarcaTarjeta varchar(50),
	@CantCuotas smallint,
	@AliasMP varchar(50) NULL,
	@ComentarioAdicional varchar(100) NULL
AS
BEGIN
    INSERT INTO Facturas VALUES (@DNICliente, @NumTransaccion, @MontoTotal, @Impuesto, @Fecha, @MetodoPago, @MarcaTarjeta, @CantCuotas, @AliasMP, @ComentarioAdicional);
	SELECT SCOPE_IDENTITY()
END
GO

CREATE PROCEDURE RegistrarItemFactura
    @NumFactura int,
	@CodigoProducto varchar(14),
	@Cant int,
	@PrecioVenta float
AS
BEGIN
    INSERT INTO Item_Factura VALUES (@NumFactura,@CodigoProducto,@Cant,@PrecioVenta);
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

CREATE PROCEDURE ConsultarStock
	@CodigoProducto varchar(14)
AS
BEGIN
    SELECT Stock from Productos where CodigoProducto = @CodigoProducto
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
	@StockMinimo smallint,
	@StockMaximo smallint,
	@Almacenamiento smallint

AS
BEGIN
    INSERT INTO Productos VALUES (@CodigoProducto,@Modelo,@Descripcion,@Marca,
	@Color,@Precio,@Stock, @StockMinimo, @StockMaximo, @Almacenamiento,1)
END
GO

CREATE PROCEDURE EliminarProducto
	@CodigoProducto varchar(14)
AS
BEGIN
    UPDATE Productos set BorradoLogico = 0 WHERE CodigoProducto = @CodigoProducto;
END
GO

CREATE PROCEDURE HabilitarProducto
	@CodigoProducto varchar(14)
AS
BEGIN
    UPDATE Productos set BorradoLogico = 1 WHERE CodigoProducto = @CodigoProducto;
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
	@StockMinimo smallint,
	@StockMaximo smallint,
	@Almacenamiento smallint,
	@Borrado bit

AS
BEGIN
    UPDATE Productos SET Modelo = @Modelo, Descripcion = @Descripcion, Marca = @Marca, 
	Color = @Color, Precio = @Precio, Stock = @Stock, StockMinimo = @StockMinimo, 
	StockMaximo = @StockMaximo, Almacenamiento = @Almacenamiento, BorradoLogico = @Borrado where CodigoProducto = @CodigoProducto
END
GO


CREATE PROCEDURE TraerUltimas100Facturas
AS
BEGIN
     SELECT TOP 100 * FROM Facturas F INNER JOIN Clientes C ON C.DNICliente = F.DNICliente ORDER BY NumFactura DESC;
END
GO


CREATE PROCEDURE TraerItemFactura
@NumFactura int
AS
BEGIN
	SELECT * FROM Item_Factura I INNER JOIN Productos P ON I.CodigoProducto = P.CodigoProducto WHERE I.NumFactura = @NumFactura;
END
GO

CREATE PROCEDURE ConsultarFacturas
@NumFactura int,
@NumTransaccion int,
@DNICliente int
AS
BEGIN
	SELECT * FROM Facturas F INNER JOIN Clientes C ON C.DNICliente = F.DNICliente
	WHERE NumFactura = @NumFactura OR NumeroTransaccion = @NumTransaccion OR F.DNICliente = @DNICliente  ORDER BY NumFactura DESC;
END
GO



/*COMPOSITE*/


CREATE TABLE Permisos
(
CodPermiso INT PRIMARY KEY IDENTITY(1,1),
Nombre VARCHAR(50),
Tipo bit
)
GO

CREATE TABLE Permiso_Componente
(
CodPermisoComponente INT PRIMARY KEY IDENTITY(1,1),
CodPadre INT  FOREIGN KEY REFERENCES Permisos(CodPermiso),
CodHijo INT FOREIGN KEY REFERENCES Permisos(CodPermiso)
)
GO

CREATE TABLE Roles
(
CodRol INT PRIMARY KEY IDENTITY(1,1),
Nombre VARCHAR(50)
)
GO

CREATE TABLE Rol_Permiso
(
CodRolPermiso INT PRIMARY KEY IDENTITY(1,1),
CodRol INT FOREIGN KEY REFERENCES Roles(CodRol),
CodPermiso INT FOREIGN KEY REFERENCES Permisos(CodPermiso)
)
GO








CREATE PROCEDURE CrearFamilia
    @NombreFamilia varchar(50)
AS
BEGIN
    INSERT INTO Permisos VALUES(@NombreFamilia, 1);
	SELECT SCOPE_IDENTITY()
END
GO

CREATE PROCEDURE ModificarFamilia
 @CodPermiso int,
 @NombreFamilia varchar(50)
AS
BEGIN
    UPDATE Permisos SET Nombre = @NombreFamilia WHERE CodPermiso = @CodPermiso;
END
GO

CREATE PROCEDURE EliminarFamilia
 @CodFamilia int
AS
BEGIN
    DELETE FROM Permisos WHERE CodPermiso = @CodFamilia;
END
GO



CREATE PROCEDURE RegistrarHijosFamilia
    @CodPadre int,
	@CodHijo int
AS
BEGIN

    INSERT INTO Permiso_Componente VALUES(@CodPadre,@CodHijo);
END
GO


CREATE PROCEDURE EliminarHijos
    @CodPadre int
AS
BEGIN

    DELETE FROM Permiso_Componente WHERE CodPadre =@CodPadre;
END
GO



CREATE PROCEDURE TraerListaHijos 
@CodPadre INT
AS
BEGIN
    SELECT PC.CodPadre, PC.CodHijo, P.Nombre, P.Tipo FROM Permiso_Componente AS PC INNER JOIN Permisos AS P ON PC.CodHijo = P.CodPermiso WHERE CodPadre = @CodPadre;
END
GO


CREATE PROCEDURE CrearRol
    @NombreRol varchar(50)
AS
BEGIN
    INSERT INTO Roles VALUES(@NombreRol);
	SELECT SCOPE_IDENTITY()
END
GO

CREATE PROCEDURE RegistrarRolPermiso
    @CodRol int,
	@codPermiso int
AS
BEGIN

    INSERT INTO Rol_Permiso VALUES(@CodRol,@codPermiso);
END
GO

CREATE PROCEDURE EliminarPermisosRol
    @CodRol int
AS
BEGIN
    DELETE FROM Rol_Permiso WHERE CodRol = @CodRol;
END
GO



CREATE PROCEDURE EliminarRol
    @CodRol int
AS
BEGIN
    DELETE FROM Roles WHERE CodRol = @CodRol;
END
GO

CREATE PROCEDURE TraerListaPermisosRol 
@CodRol INT
AS
BEGIN
    SELECT PC.CodRol, PC.CodPermiso, P.Nombre, P.Tipo FROM Rol_Permiso AS PC INNER JOIN Permisos AS P ON PC.CodPermiso = P.CodPermiso WHERE CodRol = @CodRol;
END
GO

CREATE PROCEDURE TraerListaPermisosRolSegunPermiso 
@CodPermiso INT
AS
BEGIN
    SELECT PC.CodRol, PC.CodPermiso, P.Nombre, P.Tipo FROM Rol_Permiso AS PC INNER JOIN Permisos AS P ON PC.CodPermiso = P.CodPermiso WHERE PC.CodPermiso = @CodPermiso;
END
GO


CREATE PROCEDURE ModificarRol
    @CodRol int,
	@Nombre varchar(50)
AS
BEGIN
    UPDATE Roles SET Nombre = @Nombre WHERE CodRol = @CodRol;
END
GO

/*PERMISOS, FAMILIAS, ROLES INSERTS*/
INSERT INTO Permisos VALUES('Admin',0) --1
INSERT INTO Permisos VALUES('Maestros',0) --2
INSERT INTO Permisos VALUES('Usuarios',0) --3
INSERT INTO Permisos VALUES('Ventas',0) --4
INSERT INTO Permisos VALUES('Compras',0) --5
INSERT INTO Permisos VALUES('Reportes',0) --6
INSERT INTO Permisos VALUES('Ayuda',0) --7

INSERT INTO Permisos VALUES('GestionUsuarios', 0); --8
INSERT INTO Permisos VALUES('GestionPerfiles', 0); --9
INSERT INTO Permisos VALUES('Productos', 0); --10
INSERT INTO Permisos VALUES('Clientes', 0); --11
INSERT INTO Permisos VALUES('CambiarClave', 0); --12
INSERT INTO Permisos VALUES('CambiarIdioma', 0); --13
INSERT INTO Permisos VALUES('GenerarFactura', 0); --14
INSERT INTO Permisos VALUES('ReporteVentas', 0); --15
INSERT INTO Permisos VALUES('Eventos', 0); --16
INSERT INTO Permisos VALUES('Respaldos', 0); --17
INSERT INTO Permisos VALUES('ProductosC', 0); --18
INSERT INTO Permisos VALUES('GenerarSolicitudCotizacion', 0); --19
INSERT INTO Permisos VALUES('GenerarOrdenCompra', 0); --20
INSERT INTO Permisos VALUES('CorroborarRecepcion', 0); --21
INSERT INTO Permisos VALUES('Proveedores', 0); --22
INSERT INTO Permisos VALUES('ReporteCompras', 0); --23
INSERT INTO Permisos VALUES('ReporteInteligente', 0); --24



INSERT INTO Permisos VALUES('FamiliaAdmin',1)
Insert into Permiso_Componente VALUES(25, 1)
Insert into Permiso_Componente VALUES(25, 2)
Insert into Permiso_Componente VALUES(25, 3)
Insert into Permiso_Componente VALUES(25, 4)
Insert into Permiso_Componente VALUES(25, 5)
Insert into Permiso_Componente VALUES(25, 6)
Insert into Permiso_Componente VALUES(25, 7)
Insert into Permiso_Componente VALUES(25, 8)
Insert into Permiso_Componente VALUES(25, 9)
Insert into Permiso_Componente VALUES(25, 10)
Insert into Permiso_Componente VALUES(25, 11)
Insert into Permiso_Componente VALUES(25, 12)
Insert into Permiso_Componente VALUES(25, 13)
Insert into Permiso_Componente VALUES(25, 14)
Insert into Permiso_Componente VALUES(25, 15)
Insert into Permiso_Componente VALUES(25, 16)
Insert into Permiso_Componente VALUES(25, 17)
Insert into Permiso_Componente VALUES(25, 18)
Insert into Permiso_Componente VALUES(25, 19)
Insert into Permiso_Componente VALUES(25, 20)
Insert into Permiso_Componente VALUES(25, 21)
Insert into Permiso_Componente VALUES(25, 22)
Insert into Permiso_Componente VALUES(25, 23)
Insert into Permiso_Componente VALUES(25, 24)




INSERT INTO Roles VALUES('Admin')
INSERT INTO Rol_Permiso VALUES (1,25)


INSERT INTO Permisos VALUES('FamiliaVentas',1)
Insert into Permiso_Componente VALUES(26, 2)
Insert into Permiso_Componente VALUES(26, 3)
Insert into Permiso_Componente VALUES(26, 4)
Insert into Permiso_Componente VALUES(26, 6)
Insert into Permiso_Componente VALUES(26, 7)
Insert into Permiso_Componente VALUES(26, 10)
Insert into Permiso_Componente VALUES(26, 11)
Insert into Permiso_Componente VALUES(26, 18)
Insert into Permiso_Componente VALUES(26, 12)
Insert into Permiso_Componente VALUES(26, 13)
Insert into Permiso_Componente VALUES(26, 14)
Insert into Permiso_Componente VALUES(26, 15)
Insert into Permiso_Componente VALUES(26, 24)



INSERT INTO Roles VALUES('Vendedor')
INSERT INTO Rol_Permiso VALUES (2,26)



INSERT INTO Permisos VALUES('FamiliaCompras',1)
Insert into Permiso_Componente VALUES(27, 2)
Insert into Permiso_Componente VALUES(27, 3)
Insert into Permiso_Componente VALUES(27, 5)
Insert into Permiso_Componente VALUES(27, 6)
Insert into Permiso_Componente VALUES(27, 7)
Insert into Permiso_Componente VALUES(27, 22)
Insert into Permiso_Componente VALUES(27, 10)
Insert into Permiso_Componente VALUES(27, 18)
Insert into Permiso_Componente VALUES(27, 12)
Insert into Permiso_Componente VALUES(27, 13)
Insert into Permiso_Componente VALUES(27, 19)
Insert into Permiso_Componente VALUES(27, 20)
Insert into Permiso_Componente VALUES(27, 21)
Insert into Permiso_Componente VALUES(27, 23)
Insert into Permiso_Componente VALUES(27, 24)


INSERT INTO Roles VALUES('Comprador')
INSERT INTO Rol_Permiso VALUES (3,27)


CREATE TABLE Usuarios(
DNI INT PRIMARY KEY NOT NULL,
Nombre varchar(50) NOT NULL,
Apellido varchar(50) NOT NULL,
Mail varchar(100) NOT NULL,
NombreUsuario varchar(50) NOT NULL UNIQUE,
Clave varchar(100) NOT NULL,
Rol INT FOREIGN KEY REFERENCES Roles(CodRol),
Bloqueo bit,
Activo bit,
ContFallidos smallint
);

CREATE TABLE Eventos(
CodEvento BIGINT PRIMARY KEY IDENTITY(1,1),
NombreUsuario varchar(50) FOREIGN KEY REFERENCES Usuarios(NombreUsuario),
Modulo varchar(50),
Evento varchar(100),
Criticiad smallint,
Fecha varchar(11),
Hora varchar(5),
)
GO


CREATE PROCEDURE TraerEventosUltimos3Dias
AS
BEGIN
	SELECT * FROM Eventos WHERE CONVERT(DATE, Fecha, 120) >= DATEADD(DAY, -3, GETDATE());
END
GO

CREATE PROCEDURE RegistrarEvento
    @NombreUsuario varchar(50),
	@Modulo varchar(50),
	@Evento varchar(100),
	@Criticidad smallint,
	@Fecha varchar(11),
	@Hora varchar(5)
AS
BEGIN
    INSERT INTO Eventos VALUES (@NombreUsuario, @Modulo, @Evento, @Criticidad, @Fecha, @Hora)
END
GO


CREATE PROCEDURE FiltrarEvento
    @NombreUsuario varchar(50),
	@Modulo varchar(50),
	@Evento varchar(100),
	@Criticidad varchar(2),
	@FechaInicio varchar(11),
	@FechaFin varchar(11)
AS
BEGIN
    SELECT * FROM Eventos where NombreUsuario LIKE @NombreUsuario + '%' AND Modulo LIKE @Modulo + '%' 
	AND Evento LIKE @Evento + '%' AND Criticiad LIKE @Criticidad + '%' AND (Fecha >= @FechaInicio AND Fecha <= @FechaFin)
END
GO




/*BITACORA CAMBIOS*/

CREATE TABLE Productos_C
(
CodCambio INT PRIMARY KEY IDENTITY(1,1),
CodigoProducto varchar(14) FOREIGN KEY REFERENCES Productos(CodigoProducto),
Fecha varchar(11),
Hora varchar(5),
Modelo varchar(50),
Descripcion varchar(100) NOT NULL,
Marca varchar(50) NOT NULL,
Color varchar(50) NOT NULL,
Precio float NOT NULL,
Stock smallint NOT NULL,
StockMinimo smallint NOT NULL,
StockMaximo smallint NOT NULL,
Almacenamiento smallint NOT NULL,
BorradoLogico bit,
Act bit
)


GO
CREATE TRIGGER TriggerBitacoraCambio ON Productos AFTER INSERT, UPDATE
AS
BEGIN
UPDATE Productos_C SET Productos_C.Act = 0 FROM inserted where Productos_C.CodigoProducto = inserted.CodigoProducto

INSERT INTO Productos_C (CodigoProducto, Fecha, Hora, Modelo, Descripcion, Marca, Color, Precio, Stock, StockMinimo, StockMaximo, Almacenamiento, BorradoLogico, Act)
SELECT CodigoProducto, CONVERT(VARCHAR(11),GETDATE(),120), CONVERT(VARCHAR(5),GETDATE(),114), Modelo, Descripcion, Marca, Color, Precio, Stock, StockMinimo, StockMaximo, Almacenamiento, BorradoLogico, 1 
FROM inserted
END
GO


CREATE PROCEDURE TraerCambiosUltimoMes
AS
BEGIN
	SELECT * FROM Productos_C WHERE CONVERT(DATE, Fecha, 120) >= DATEADD(DAY, -31, GETDATE());
END
GO



CREATE PROCEDURE FiltrarCambios
    @CodProd varchar(50),
	@ModeloProd varchar(50),
	@FechaInicio varchar(11),
	@FechaFin varchar(11)
AS
BEGIN
    SELECT * FROM Productos_C where CodigoProducto LIKE @CodProd+ '%' AND Modelo LIKE @ModeloProd + '%' 
	AND (Fecha >= @FechaInicio AND Fecha <= @FechaFin)
END
GO


/*RESPALDO
CREATE PROCEDURE RealizarBackUp
    @RutaBackUp varchar(260)
AS
BEGIN
    BACKUP DATABASE CarpetaIngSoftware TO DISK= @RutaBackUp
END
GO
*/






/* COMPRA */
CREATE TABLE Proveedores
(
	CUITProveedor varchar(14) PRIMARY KEY NOT NULL,
	Nombre varchar(100),
	RazonSocial varchar(100),
	Email varchar(100),
	NumTelefono varchar(15), 
	Direccion varchar(100),
	Banco varchar(50),
	CBU varchar(100),
	BorradoLogico bit
)


CREATE TABLE SolicitudesCotizacion
(
	NumeroSolicitud INT PRIMARY KEY IDENTITY(1,1),
	Fecha varchar(18),
	Estado varchar(50)
)

CREATE TABLE Item_Solicitud
(
	CodItem_Solicitud INT PRIMARY KEY IDENTITY(1,1),
	NumeroSolicitud INT FOREIGN KEY REFERENCES SolicitudesCotizacion(NumeroSolicitud),
	CodigoProducto varchar(14) FOREIGN KEY REFERENCES Productos(CodigoProducto),
	Cantidad int
)

CREATE TABLE Solicitud_Proveedor
(
	CodSolicitud_Proveedor INT PRIMARY KEY IDENTITY(1,1),
	NumeroSolicitud INT FOREIGN KEY REFERENCES SolicitudesCotizacion(NumeroSolicitud),
	CUITProveedor varchar(14) FOREIGN KEY REFERENCES Proveedores(CUITProveedor)
)




CREATE TABLE OrdenesCompra
(
	NumeroOrdenCompra INT PRIMARY KEY IDENTITY(1,1),
	CUITProveedor varchar(14) FOREIGN KEY REFERENCES Proveedores(CUITProveedor),
	NumeroSolicitud INT FOREIGN KEY REFERENCES SolicitudesCotizacion(NumeroSolicitud),
	FechaRegistro varchar(18),
	FechaEntrega varchar(18),
	Estado varchar(50),
	NumeroTransferencia bigint,
	MetodoPago varchar(50),
	MontoTotal float,
	CantidadTotal int,
	NumeroFactura bigint
)


CREATE TABLE Item_OrdenCompra
(
	CodItem_OrdenCompra INT PRIMARY KEY IDENTITY(1,1),
	NumeroOrdenCompra INT FOREIGN KEY REFERENCES OrdenesCompra(NumeroOrdenCompra),
	CodigoProducto varchar(14) FOREIGN KEY REFERENCES Productos(CodigoProducto),
	CantidadSolicitada int,
	PrecioCompra float,
	CantidadRecibida int,
	NumFacturaRecepcion bigint,
	FechaRecepcion varchar(18)
)





GO


CREATE PROCEDURE TraerProductosBajoStock
AS
BEGIN
	SELECT * FROM Productos 
	WHERE (Stock < StockMinimo OR Stock <= (StockMinimo + 10))
	 AND Stock <= StockMaximo;
END
GO

CREATE PROCEDURE TraerOrdenesPendientes
AS
BEGIN
	SELECT * FROM OrdenesCompra where Estado <> 'Entregada'
END
GO



CREATE PROCEDURE RegistrarSolicitudCotizacion
	@Estado varchar(11),
	@Fecha varchar(18)
AS
BEGIN
    INSERT INTO SolicitudesCotizacion VALUES (@Fecha,@Estado)
	SELECT SCOPE_IDENTITY()
END
GO

CREATE PROCEDURE RegistrarItemSolicitud
	@NumeroSolicitud int,
	@CodigoProducto varchar(14),
	@Cantidad int
AS
BEGIN
    INSERT INTO Item_Solicitud VALUES (@NumeroSolicitud, @CodigoProducto, @Cantidad)
END
GO


CREATE PROCEDURE RegistrarProveedorSolicitud
	@NumeroSolicitud int,
	@CUITProveedor varchar(14)
AS
BEGIN
    INSERT INTO Solicitud_Proveedor VALUES (@NumeroSolicitud, @CUITProveedor)
END
GO


CREATE PROCEDURE TraerProveedoresSolicitud
	@NumeroSolicitud int
AS
BEGIN
   SELECT * FROM Solicitud_Proveedor S INNER JOIN Proveedores P ON S.CUITProveedor = P.CUITProveedor WHERE NumeroSolicitud = @NumeroSolicitud
END
GO

CREATE PROCEDURE TraerItemsSolicitud
	@NumeroSolicitud int
AS
BEGIN
   SELECT * FROM Item_Solicitud S INNER JOIN Productos P ON S.CodigoProducto = P.CodigoProducto WHERE NumeroSolicitud = @NumeroSolicitud
END
GO

CREATE PROCEDURE RegistrarOrdenCompra
	@CUITProveedor varchar(14),
	@NumeroSolicitud int,
	@FechaRegistro varchar(18),
	@FechaEntrega varchar(18),
	@Estado varchar(50),
	@NumeroTransferencia bigint,
	@MetodoPago varchar(50),
	@MontoTotal float,
	@CantidadTotal int,
	@NumeroFactura bigint
AS
BEGIN
    INSERT INTO OrdenesCompra VALUES(@CUITProveedor, @NumeroSolicitud, @FechaRegistro, @FechaEntrega, @Estado, @NumeroTransferencia, @MetodoPago, @MontoTotal, @CantidadTotal, @NumeroFactura)
	SELECT SCOPE_IDENTITY()
END
GO

CREATE PROCEDURE RegistrarItemOrden
	@NumeroOrdenCompra int,
	@CodigoProducto varchar(14),
	@Cantidad int,
	@PrecioCompra float
AS
BEGIN
    INSERT INTO Item_OrdenCompra VALUES (@NumeroOrdenCompra, @CodigoProducto, @Cantidad, @PrecioCompra,0,0,'')
END
GO

CREATE PROCEDURE TraerProveedorOrden
	@NumeroOrdenCompra INT
AS
BEGIN
    SELECT * FROM OrdenesCompra O INNER JOIN Proveedores P ON O.CUITProveedor = P.CUITProveedor
	WHERE NumeroOrdenCompra = @NumeroOrdenCompra
END
GO

CREATE PROCEDURE TraerProductosOrden
	@NumeroOrdenCompra INT
AS
BEGIN
   SELECT * FROM Item_OrdenCompra O INNER JOIN Productos P ON O.CodigoProducto = P.CodigoProducto WHERE NumeroOrdenCompra = @NumeroOrdenCompra
END
GO



CREATE PROCEDURE MarcarOrdenEntregada
	@NumeroOrdenCompra INT,
	@Estado varchar(50),
	@Fecha varchar(18)
AS
BEGIN
	UPDATE OrdenesCompra SET Estado = @Estado, FechaEntrega = @Fecha WHERE NumeroOrdenCompra = @NumeroOrdenCompra
END
GO


CREATE PROCEDURE ModificarEstadoSolicitud
	@NumeroSolicitud INT,
	@Estado varchar(50)
AS
BEGIN
	UPDATE SolicitudesCotizacion SET Estado = @Estado WHERE NumeroSolicitud = @NumeroSolicitud
END
GO

CREATE PROCEDURE ModificarCantidadRecibidaItem
	@NumeroOrdenCompra INT,
	@CodigoProducto varchar(14),
	@CantidadRecibida INT,
	@NumFactura BIGINT,
	@FechaRecepcion varchar(18)
AS
BEGIN
	UPDATE Item_OrdenCompra SET CantidadRecibida = @CantidadRecibida, NumFacturaRecepcion = @NumFactura, FechaRecepcion = @FechaRecepcion
	WHERE NumeroOrdenCompra = @NumeroOrdenCompra AND CodigoProducto = @CodigoProducto
END
GO



CREATE PROCEDURE VerificarProveedor
	@CUITProveedor varchar(14),
	@CBU varchar(100),
	@Email varchar(100)
AS
BEGIN
    SELECT * FROM Proveedores WHERE CUITProveedor = @CUITProveedor OR CBU = @CBU OR Email = @Email
END
GO



CREATE PROCEDURE RegistrarProveedor
	@CUITProveedor varchar(14),
	@Nombre varchar(100),
	@RazonSocial varchar(100),
	@Email varchar(100),
	@NumTelefono varchar(15),
	@CBU varchar(100),
	@Direccion varchar(100),
	@Banco varchar(50)

AS
BEGIN
    INSERT INTO Proveedores VALUES (@CUITProveedor, @Nombre, @RazonSocial, @Email, @NumTelefono, @Direccion, @Banco, @CBU, 1)
END
GO


CREATE PROCEDURE ModificarProveedor
	@CUITProveedor varchar(14),
	@Nombre varchar(100),
	@RazonSocial varchar(100),
	@Email varchar(100),
	@NumTelefono varchar(15),
	@CBU varchar(100),
	@Direccion varchar(100),
	@Banco varchar(50)
AS
BEGIN
    UPDATE Proveedores  SET Nombre = @Nombre, RazonSocial = @RazonSocial,Email=  @Email, NumTelefono= @NumTelefono, CBU= @CBU, Direccion= @Direccion, Banco = @Banco
	where CUITProveedor = @CUITProveedor
END
GO

CREATE PROCEDURE EliminarProveedor
	@CUITProveedor varchar(14)
AS
BEGIN
    UPDATE Proveedores  SET BorradoLogico = 0 where CUITProveedor = @CUITProveedor
END
GO

CREATE PROCEDURE HabilitarProveedor
	@CUITProveedor varchar(14)
AS
BEGIN
    UPDATE Proveedores  SET BorradoLogico = 1 where CUITProveedor = @CUITProveedor
END
GO


CREATE TABLE DigitoVerificador
(
NombreTabla VARCHAR(50) PRIMARY KEY,
DVH VARCHAR(100),
DVV VARCHAR(100)
)
GO


CREATE PROCEDURE PersistirDV
    @NombreTabla VARCHAR(50),
	@DVH VARCHAR(100),
	@DVV VARCHAR(100)
AS
BEGIN
    UPDATE DigitoVerificador SET DVH = @DVH, DVV = @DVV WHERE NombreTabla = @NombreTabla
END
GO



CREATE PROCEDURE ReporteInteligenteStock
AS
BEGIN
    SELECT 
        p.CodigoProducto, p.Modelo, p.Marca,
        p.Stock AS StockActual, p.StockMinimo, p.StockMaximo,
        ISNULL(SUM(f.Cant), 0) AS CantidadVendidaUltimoMes,
        -- Cantidad a reponer
        CASE
            WHEN p.Stock <= p.StockMinimo THEN 
			--Calcula la cantidad m�s grande entre lo que falta para llegar al stock m�nimo y la cantidad vendida en el �ltimo mes con un 10% extra
                CEILING(LEAST(
                        p.StockMaximo - p.Stock, -- No superar el stock m�ximo
                        GREATEST(p.StockMinimo - p.Stock, ISNULL(SUM(f.Cant) * 1.1, 0)) -- Reponer lo necesario
                    ))
            ELSE 0
        END AS CantidadAReponer
    FROM Productos p
    LEFT JOIN Item_Factura f ON p.CodigoProducto = f.CodigoProducto
    LEFT JOIN Facturas fact ON f.NumFactura = fact.NumFactura AND fact.Fecha >= DATEADD(MONTH, -1, GETDATE())
    GROUP BY  p.CodigoProducto, p.Modelo, p.Marca, p.Stock, p.StockMinimo, p.StockMaximo;
END
GO


CREATE PROCEDURE ReporteInteligenteVentasGeneradasPorProd
	@FechaInicio varchar(11),
	@FechaFin varchar(11)
AS
BEGIN
	SELECT P.CodigoProducto, P.Modelo, p.Marca, COUNT(F.NumFactura) AS CantidadVentas, 
	SUM(MontoTotal) AS MontoVendido from Facturas F
	INNER JOIN Item_Factura I ON I.NumFactura = F.NumFactura
	INNER JOIN Productos P ON P.CodigoProducto = I.CodigoProducto
	WHERE CAST(Fecha AS DATE) >= @FechaInicio AND CAST(Fecha AS DATE) <= @FechaFin
	GROUP BY P.CodigoProducto, P.Modelo, P.Marca
	ORDER BY COUNT(*) DESC
END
GO


CREATE PROCEDURE ReporteInteligenteVentasGeneradasPorMarca
	@FechaInicio varchar(11),
	@FechaFin varchar(11)
AS
BEGIN
	SELECT p.Marca, COUNT(F.NumFactura) AS CantidadVentas, 
	SUM(MontoTotal) AS MontoVendido from Facturas F
	INNER JOIN Item_Factura I ON I.NumFactura = F.NumFactura
	INNER JOIN Productos P ON P.CodigoProducto = I.CodigoProducto
	WHERE CAST(Fecha AS DATE) >= @FechaInicio AND CAST(Fecha AS DATE) <= @FechaFin
	GROUP BY P.Marca
	ORDER BY COUNT(*) DESC
END
GO


CREATE PROCEDURE ReporteInteligenteVentasGeneradasPorCliente
	@FechaInicio varchar(11),
	@FechaFin varchar(11)
AS
BEGIN
	SELECT F.DNICliente, C.Nombre, C.Apellido, C.Mail, COUNT(F.NumFactura) AS CantidadVentas, 
	SUM(MontoTotal) AS MontoVendido from Facturas F
	INNER JOIN Clientes C ON F.DNICliente = C.DNICliente
	WHERE CAST(Fecha AS DATE) >= @FechaInicio AND CAST(Fecha AS DATE) <= @FechaFin
	GROUP BY F.DNICliente, C.Nombre, C.Apellido, C.Mail
	ORDER BY COUNT(*) DESC
END
GO




CREATE PROCEDURE ReporteInteligentePrececirVentas
AS
BEGIN
	WITH VentasUltimos7Dias AS (
		SELECT  i.CodigoProducto, SUM(i.Cant) AS CantidadVendidaUltimos7Dias
		FROM  Item_Factura i
		INNER JOIN Facturas f ON i.NumFactura = f.NumFactura
		WHERE CONVERT(datetime, f.Fecha, 120) >= DATEADD(DAY, -7, GETDATE()) --ultimos 7 dias
		GROUP BY i.CodigoProducto
	)
	SELECT 
		p.CodigoProducto, p.Modelo, p.Marca, p.Stock AS StockActual,
		v.CantidadVendidaUltimos7Dias AS PrediccionSemana, --prediccion para la propxima semana
		v.CantidadVendidaUltimos7Dias * 4 AS PrediccionMes --prediccion para el proximo mes
	FROM 
		VentasUltimos7Dias v INNER JOIN  Productos p ON v.CodigoProducto = p.CodigoProducto
	ORDER BY 
		PrediccionMes DESC;
END
GO


CREATE PROCEDURE ReporteInteligentePrececirIngresos
AS
BEGIN
	WITH VentasSemanales AS (
    SELECT 
        DATEPART(YEAR, F.Fecha) AS A�o,
        DATEPART(WEEK, F.Fecha) AS Semana,
        SUM(I.Cant * I.PrecioVenta) AS VentasSemanales
    FROM Facturas F
    INNER JOIN Item_Factura I ON F.NumFactura = I.NumFactura
    GROUP BY DATEPART(YEAR, F.Fecha), DATEPART(WEEK, F.Fecha)
),
VentasMensuales AS (
    SELECT 
        DATEPART(YEAR, F.Fecha) AS A�o,
        DATEPART(MONTH, F.Fecha) AS Mes,
        SUM(I.Cant * I.PrecioVenta) AS VentasMensuales
    FROM Facturas F
    INNER JOIN Item_Factura I ON F.NumFactura = I.NumFactura
    GROUP BY DATEPART(YEAR, F.Fecha), DATEPART(MONTH, F.Fecha)
)
-- Predicci�n basada en el promedio hist�rico
SELECT 
    CONVERT(DATE, DATEADD(WEEK, 1, GETDATE())) AS ProximaSemana, 
    (SELECT AVG(VentasSemanales) FROM VentasSemanales) AS VentasProximasSemana,
    CONVERT(DATE, DATEADD(MONTH, 1, GETDATE())) AS ProximoMes,
    (SELECT AVG(VentasMensuales) FROM VentasMensuales) * 4 AS VentasProximasMes
END
GO





/*CLAVE: clave123*/
INSERT INTO Usuarios VALUES (12345678, 'Admin', 'Admin', 'admin@gmail.com', 'Admin', '5ac0852e770506dcd80f1a36d20ba7878bf82244b836d9324593bd14bc56dcb5', 1, 0, 1,0);
--clave: claveadmin2
INSERT INTO Usuarios VALUES (11111111, 'Admin2', 'Admin2', 'admin2@gmail.com', 'Admin2', '18776097b125be86e05255df301827a91cdd34564b26cb8538f61cf781db5471', 1, 0, 1,0);
--Clave: 41256789Rodriguez
INSERT INTO Usuarios VALUES (41256789, 'Esteban', 'Rodriguez', 'estebanrodriguez@gmail.com', 'Esteban', 'c0f7d327744518249a4db0aee5e4096c8b42e9858e6d9104fd048cf7decd127e', 2, 0, 1,0);
--Clave: 40334227Perez
INSERT INTO Usuarios VALUES (40334227, 'Horacio', 'Perez', 'horacioperez@gmail.com', 'Horacio', 'f350068aca75e6a6fa6b8e898f42b707bd7fe1233518959181e6ca621e1c9f44', 3, 0, 1,0);

INSERT INTO Productos VALUES (123, 'Iphone 15 Pro','Chip A17 Pro, 8GB Ram, OLED 6.1 pulgadas, Camara 48 MP', 'Apple', 'Blanco', 1100, 20, 10, 40, 256,1);
INSERT INTO Productos VALUES (456, 'Samsung S24 Ultra','Chip Octa-Coree, 8GB Ram, Bateria 5000 mAh, Camra 50MP','Samsung', 'Negro', 1300, 26,  10, 40, 512,1);
INSERT INTO Productos VALUES (789012, 'Google Pixel 8', 'Chip Tensor G3, 12GB Ram, OLED 6.2 pulgadas, Camara 50 MP', 'Google', 'Gris', 900, 15, 10, 40, 256, 1);
INSERT INTO Productos VALUES (901234, 'Xiaomi Mi 13 Ultra','Chip Snapdragon 8 Gen 2, 12GB Ram, AMOLED 6.73 pulgadas, Camara 50 MP', 'Xiaomi', 'Verde', 850, 22, 10, 40, 512,1);


INSERT INTO Proveedores VALUES ('30-68537634-9','Proveedor de Celulares S.A.','Proveedor de Celulares Sociedad An�nima', 'provcelular@gmail.com', '1125681425', 'Av. Siempre Viva 742', 'Banco Galicia', '01702046600000087865',1);
INSERT INTO Proveedores VALUES ('30-22588311-5','Proveedor Tech', 'Proveedor Tech S.A', 'provcelular2@gmail.com', '1144333567', 'Alejandro Pereyra 244', 'Banco HSBC', '0720003070000001234567',1);


INSERT INTO Clientes VALUES (34789332, 'Franco', 'Perez', 'francoperez@gmail.com', 'Q6AITKuh4LfnxQ+6o/6LSA==',1);
INSERT INTO Clientes VALUES (29145876, 'Marcos', 'Diaz', 'marcosdiaz@gmail.com', '5ZZgvahyS8Hd8hi9gTZjDQ==',1);
INSERT INTO Clientes VALUES (44978545, 'Luis', 'Hern�ndez', 'luishernandez@gmail.com', 'G83WHQGteUATtYUG2eu3bydWi2qACBS6A19s6UyTTKc=', 1);
INSERT INTO Clientes VALUES (40898122, 'Carla', 'Mart�nez', 'carlamartinez@gmail.com', 'NccLBubi03EOxcaLpcaPRg==', 1);

/*
INSERT INTO Facturas VALUES (29145876, 1, 1331, 231, '2024-10-28 13:20', 'MercadoPago',null,1,'marcos','')
INSERT INTO Item_Factura VALUES (1,123,1,1100)*/


INSERT INTO DigitoVerificador VALUES ('Clientes','','')
INSERT INTO DigitoVerificador VALUES ('Productos','','')
INSERT INTO DigitoVerificador VALUES ('Facturas','','')
INSERT INTO DigitoVerificador VALUES ('Item_Factura','','')
INSERT INTO DigitoVerificador VALUES ('Proveedores','','')
INSERT INTO DigitoVerificador VALUES ('SolicitudesCotizacion','','')
INSERT INTO DigitoVerificador VALUES ('Item_Solicitud','','')
INSERT INTO DigitoVerificador VALUES ('Solicitud_Proveedor','','')
INSERT INTO DigitoVerificador VALUES ('OrdenesCompra','','')
INSERT INTO DigitoVerificador VALUES ('Item_OrdenCompra','','')

insert into Eventos values ('Esteban','Sesiones','Inicio sesi�n',1,'2024-10-28','12:15')
insert into Eventos values ('Esteban','Ventas','Factura generada',2,'2024-10-28','13:20')
insert into Eventos values ('Esteban','Ventas','Impresi�n de factura',2,'2024-10-28','13:23')
insert into Eventos values ('Esteban','Sesiones','Cierre sesi�n',1,'2024-10-28','15:37')
insert into Eventos values ('Admin','Sesiones','Inicio sesi�n',	1,'2024-10-29',	'15:40')
insert into Eventos values ('Admin','Clientes','Cliente creado',1,'2024-10-29',	'15:45')
insert into Eventos values ('Admin','Clientes','Archivo serializado',1,'2024-10-29',	'15:49')
insert into Eventos values ('Admin','Clientes','Archivo deserializado',	1,'2024-10-29',	'15:52')
insert into Eventos values ('Admin','Productos','Producto creado',	1,'2024-10-29',	'16:30')
insert into Eventos values ('Admin','Sesiones','Cierre sesi�n',	1,'2024-10-29',	'19:40')