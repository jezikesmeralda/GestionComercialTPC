USE Comercio;
GO

ALTER TABLE Productos ADD ImagenUrl VARCHAR(500) NULL;
GO

SELECT * FROM Clientes;
SELECT * FROM Productos;
SELECT * FROM Ventas;
SELECT * FROM Usuarios;
SELECT * FROM DetalleCompras;

GO
ALTER TABLE Ventas ADD MedioPago VARCHAR(20) NOT NULL DEFAULT 'Efectivo';
ALTER TABLE Ventas ADD Cuotas INT NOT NULL DEFAULT 1;
ALTER TABLE Ventas ADD Interes DECIMAL(5,2) NOT NULL DEFAULT 0;
ALTER TABLE Ventas ADD TotalConInteres DECIMAL(18,2) NOT NULL DEFAULT 0;

GO
--AÑADI ESTO---
ALTER PROCEDURE sp_AltaVenta
    @IdCliente INT,
    @IdUsuario INT,
    @Total DECIMAL(18,2),
    @MedioPago VARCHAR(20),
    @Cuotas INT,
    @Interes DECIMAL(5,2),
    @TotalConInteres DECIMAL(18,2)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NumeroFactura VARCHAR(20);

    SET @NumeroFactura = 
        'FAC-' + RIGHT('000000' + CAST((SELECT ISNULL(MAX(Id),0)+1 FROM Ventas) AS VARCHAR), 6);


    INSERT INTO Ventas
    (
        IdCliente,
        IdUsuario,
        FechaVenta,
        NumeroFactura,
        Total,
        MedioPago,
        Cuotas,
        Interes,
        TotalConInteres
    )
    VALUES
    (
        @IdCliente,
        @IdUsuario,
        GETDATE(),
        @NumeroFactura,
        @Total,
        @MedioPago,
        @Cuotas,
        @Interes,
        @TotalConInteres
    );


    DECLARE @IdVenta INT = SCOPE_IDENTITY();


    SELECT 
        Id,
        NumeroFactura
    FROM Ventas
    WHERE Id = @IdVenta;
END

GO

ALTER PROCEDURE [dbo].[sp_ObtenerVentaPorId]
    @IdVenta INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        v.Id,
        v.NumeroFactura,
        v.FechaVenta,
        v.Total,
        v.TotalConInteres, 
        v.MedioPago,
        v.Banco,              
        v.UltimosDigitos,    
        v.Cuotas,
        v.Interes,
        c.Id AS IdCliente,
        c.Nombre AS NombreCliente,
        c.Apellido AS ApellidoCliente,
        c.Email AS EmailCliente,
        u.Id AS IdUsuario,
        u.Nombre AS NombreUsuario
    FROM Ventas v
    INNER JOIN Clientes c ON v.IdCliente = c.Id
    INNER JOIN Usuarios u ON v.IdUsuario = u.Id
    WHERE v.Id = @IdVenta;

    SELECT
        dv.Id,
        dv.IdProducto,
        p.NombreProducto,
        dv.Cantidad,
        dv.PrecioUnitario,
        dv.Subtotal
    FROM DetalleVentas dv
    INNER JOIN Productos p ON dv.IdProducto = p.Id
    WHERE dv.IdVenta = @IdVenta;
END

GO
ALTER PROCEDURE sp_ListarProductos
AS
BEGIN
    SELECT p.Id, p.NombreProducto, p.Descripcion, p.ImagenUrl,
           p.PrecioCosto, p.PorcentajeGanancia, p.PrecioVenta,
           p.StockActual, p.StockMinimo, p.Activo,
           m.Id AS IdMarca, m.Nombre AS NombreMarca,
           c.Id AS IdCategoria, c.Nombre AS NombreCategoria
    FROM Productos p
    INNER JOIN Marcas m ON p.IdMarca = m.Id
    INNER JOIN Categorias c ON p.IdCategoria = c.Id
    WHERE p.Activo = 1
END
    
GO


---------------------------
ALTER TABLE Ventas
ADD NumeroFactura VARCHAR(20) UNIQUE;


USE Comercio;
GO

ALTER PROCEDURE sp_AltaVenta
    @IdCliente INT,
    @IdUsuario INT,
    @Total DECIMAL(18,2)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Ventas (IdCliente, IdUsuario, FechaVenta, Total)
    VALUES (@IdCliente, @IdUsuario, GETDATE(), @Total);

    DECLARE @IdVenta INT = SCOPE_IDENTITY();
    DECLARE @NumeroFactura VARCHAR(20) = 'FAC-' + CAST(YEAR(GETDATE()) AS VARCHAR(4)) + '-' + RIGHT('000000' + CAST(@IdVenta AS VARCHAR(6)), 6);

    UPDATE Ventas
    SET NumeroFactura = @NumeroFactura
    WHERE Id = @IdVenta;

    SELECT @IdVenta AS Id, @NumeroFactura AS NumeroFactura;
END
GO

ALTER PROCEDURE sp_ObtenerVentaPorId
    @IdVenta INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        v.Id,
        v.NumeroFactura,
        v.FechaVenta,
        v.Total,
        c.Id AS IdCliente,
        c.Nombre AS NombreCliente,
        c.Apellido AS ApellidoCliente,
        c.Email AS EmailCliente,       
        u.Id AS IdUsuario,
        u.Nombre AS NombreUsuario
    FROM Ventas v
    INNER JOIN Clientes c ON v.IdCliente = c.Id
    INNER JOIN Usuarios u ON v.IdUsuario = u.Id
    WHERE v.Id = @IdVenta;

    SELECT
        dv.Id,
        dv.IdProducto,
        p.NombreProducto,
        dv.Cantidad,
        dv.PrecioUnitario,
        dv.Subtotal
    FROM DetalleVentas dv
    INNER JOIN Productos p ON dv.IdProducto = p.Id
    WHERE dv.IdVenta = @IdVenta;
END
GO

ALTER PROCEDURE [dbo].[sp_ObtenerVentaPorId]
    @IdVenta INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        v.Id,
        v.NumeroFactura,
        v.FechaVenta,
        v.Total,
        c.Id AS IdCliente,
        c.Nombre AS NombreCliente,
        c.Apellido AS ApellidoCliente,
        c.Email AS EmailCliente,
        u.Id AS IdUsuario,
        u.Nombre AS NombreUsuario
    FROM Ventas v
    INNER JOIN Clientes c ON v.IdCliente = c.Id
    INNER JOIN Usuarios u ON v.IdUsuario = u.Id
    WHERE v.Id = @IdVenta;

    SELECT
        dv.Id,
        dv.IdProducto,
        p.NombreProducto,
        dv.Cantidad,
        dv.PrecioUnitario,
        dv.Subtotal
    FROM DetalleVentas dv
    INNER JOIN Productos p ON dv.IdProducto = p.Id
    WHERE dv.IdVenta = @IdVenta;
END

GO

ALTER TABLE Usuarios
ADD Email VARCHAR(200) NULL;

GO


ALTER PROCEDURE [dbo].[sp_ListarUsuarios]
AS
BEGIN
    SELECT Id, Nombre, Password, Rol, Activo, Email
    FROM Usuarios
    WHERE Activo = 1
END

GO


ALTER PROCEDURE [dbo].[sp_AltaUsuario]
    @Nombre VARCHAR(100),
    @Password VARCHAR(255),
    @Rol INT,
    @Email VARCHAR(200)
AS
BEGIN
    INSERT INTO Usuarios (Nombre, Password, Rol, Activo, Email)
    VALUES (@Nombre, @Password, @Rol, 1, @Email)
END
GO

ALTER PROCEDURE [dbo].[sp_ModificarUsuario]
    @Id INT,
    @Nombre VARCHAR(100),
    @Password VARCHAR(255),
    @Rol INT,
    @Email VARCHAR(200)
AS
BEGIN
    UPDATE Usuarios
    SET Nombre = @Nombre,
        Password = @Password,
        Rol = @Rol,
        Email = @Email
    WHERE Id = @Id
END
GO

SELECT * FROM Usuarios
SELECT * FROM Clientes

UPDATE Usuarios
SET Email = 'admin@vinoteca.com'
WHERE Id = 1

ALTER PROCEDURE sp_ModificarProducto
    @Id INT,
    @Nombre VARCHAR(150),
    @Descripcion VARCHAR(500) = NULL,
    @ImagenUrl VARCHAR(500) = NULL,
    @IdMarca INT,
    @IdCategoria INT,
    @PrecioCosto DECIMAL(18,2),
    @PorcentajeGanancia DECIMAL(10,2),
    @StockMinimo INT
AS
BEGIN
    DECLARE @PrecioVenta DECIMAL(18,2)
    SET @PrecioVenta = @PrecioCosto * (1 + @PorcentajeGanancia / 100)

    UPDATE Productos
    SET NombreProducto = @Nombre,
        Descripcion = @Descripcion,
        ImagenUrl = @ImagenUrl,
        IdMarca = @IdMarca,
        IdCategoria = @IdCategoria,
        PrecioCosto = @PrecioCosto,
        PorcentajeGanancia = @PorcentajeGanancia,
        PrecioVenta = @PrecioVenta,
        StockMinimo = @StockMinimo
    WHERE Id = @Id
END
GO

------------------------------

ALTER TABLE Ventas ADD 
        
    Banco VARCHAR(100) NULL,
    UltimosDigitos VARCHAR(4) NULL;
   
    
    -----------------------------------------
 ALTER PROCEDURE [dbo].[sp_ObtenerVentaPorId]
    @IdVenta INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        v.Id,
        v.NumeroFactura,
        v.FechaVenta,
        v.Total,
        v.MedioPago,
        v.Banco,              
        v.UltimosDigitos,    
        v.Cuotas,
        v.Interes,
        c.Id AS IdCliente,
        c.Nombre AS NombreCliente,
        c.Apellido AS ApellidoCliente,
        c.Email AS EmailCliente,
        u.Id AS IdUsuario,
        u.Nombre AS NombreUsuario
    FROM Ventas v
    INNER JOIN Clientes c ON v.IdCliente = c.Id
    INNER JOIN Usuarios u ON v.IdUsuario = u.Id
    WHERE v.Id = @IdVenta;

    SELECT
        dv.Id,
        dv.IdProducto,
        p.NombreProducto,
        dv.Cantidad,
        dv.PrecioUnitario,
        dv.Subtotal
    FROM DetalleVentas dv
    INNER JOIN Productos p ON dv.IdProducto = p.Id
    WHERE dv.IdVenta = @IdVenta;
END
GO

--------------------------------
ALTER PROCEDURE sp_AltaVenta 
    @IdCliente INT,
    @IdUsuario INT,
    @Total DECIMAL(18,2),
    @MedioPago VARCHAR(20) = NULL,
    @Banco VARCHAR(100) = NULL,
    @UltimosDigitos VARCHAR(4) = NULL,
    @Cuotas INT = 1,
    @Interes DECIMAL(18,2) = 0,
    @TotalConInteres DECIMAL(18,2) = NULL
AS
BEGIN
    INSERT INTO Ventas 
    (IdCliente, IdUsuario, FechaVenta, Total, MedioPago, Banco, UltimosDigitos, Cuotas, Interes, TotalConInteres)
    VALUES 
    (@IdCliente, @IdUsuario, GETDATE(), @Total, @MedioPago, @Banco, @UltimosDigitos, @Cuotas, @Interes, @TotalConInteres);

    DECLARE @IdVenta INT = SCOPE_IDENTITY();
    DECLARE @NumeroFactura VARCHAR(20) = 'FAC-' + CAST(YEAR(GETDATE()) AS VARCHAR(4)) + '-' + RIGHT('000000' + CAST(@IdVenta AS VARCHAR(6)), 6);

    UPDATE Ventas SET NumeroFactura = @NumeroFactura WHERE Id = @IdVenta;

    SELECT @IdVenta AS Id, @NumeroFactura AS NumeroFactura;
END

ALTER PROCEDURE [dbo].[sp_ObtenerVentaPorId]
    @IdVenta INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        v.Id,
        v.NumeroFactura,
        v.FechaVenta,
        v.Total,
        v.TotalConInteres, 
        v.MedioPago,
        v.Banco,              
        v.UltimosDigitos,    
        v.Cuotas,
        v.Interes,
        c.Id AS IdCliente,
        c.Nombre AS NombreCliente,
        c.Apellido AS ApellidoCliente,
        c.Email AS EmailCliente,
        u.Id AS IdUsuario,
        u.Nombre AS NombreUsuario
    FROM Ventas v
    INNER JOIN Clientes c ON v.IdCliente = c.Id
    INNER JOIN Usuarios u ON v.IdUsuario = u.Id
    WHERE v.Id = @IdVenta;

    SELECT
        dv.Id,
        dv.IdProducto,
        p.NombreProducto,
        dv.Cantidad,
        dv.PrecioUnitario,
        dv.Subtotal
    FROM DetalleVentas dv
    INNER JOIN Productos p ON dv.IdProducto = p.Id
    WHERE dv.IdVenta = @IdVenta;
END