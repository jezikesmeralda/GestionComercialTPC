USE Comercio;
GO

-- Marcas:

CREATE PROCEDURE sp_ListarMarcas
AS
BEGIN
    SELECT Id, Nombre, Activo
    FROM Marcas
    WHERE Activo = 1
END
GO

CREATE PROCEDURE sp_AltaMarca
    @Nombre VARCHAR(100)
AS
BEGIN
    INSERT INTO Marcas (Nombre, Activo)
    VALUES (@Nombre, 1)
END
GO

CREATE PROCEDURE sp_ModificarMarca
    @Id INT,
    @Nombre VARCHAR(100)
AS
BEGIN
    UPDATE Marcas
    SET Nombre = @Nombre
    WHERE Id = @Id
END
GO

CREATE PROCEDURE sp_BajaMarca
    @Id INT
AS
BEGIN
    UPDATE Marcas
    SET Activo = 0
    WHERE Id = @Id
END
GO

-- Categorias: 

CREATE PROCEDURE sp_ListarCategorias
AS
BEGIN
    SELECT Id, Nombre, Activo
    FROM Categorias
    WHERE Activo = 1
END
GO

CREATE PROCEDURE sp_AltaCategoria
    @Nombre VARCHAR(100)
AS
BEGIN
    INSERT INTO Categorias (Nombre, Activo)
    VALUES (@Nombre, 1)
END
GO

CREATE PROCEDURE sp_ModificarCategoria
    @Id INT,
    @Nombre VARCHAR(100)
AS
BEGIN
    UPDATE Categorias
    SET Nombre = @Nombre
    WHERE Id = @Id
END
GO

CREATE PROCEDURE sp_BajaCategoria
    @Id INT
AS
BEGIN
    UPDATE Categorias
    SET Activo = 0
    WHERE Id = @Id
END
GO


-- Productos:


CREATE PROCEDURE sp_ListarProductos
AS
BEGIN
    SELECT p.Id, p.NombreProducto, p.Descripcion, p.ImagenUrl,
           p.PrecioCosto, p.PorcentajeGanancia, p.PrecioVenta,
           p.StockActual, p.StockMinimo, p.Activo,
           m.Nombre AS NombreMarca,
           c.Nombre AS NombreCategoria
    FROM Productos p
    INNER JOIN Marcas m ON p.IdMarca = m.Id
    INNER JOIN Categorias c ON p.IdCategoria = c.Id
END
GO

CREATE PROCEDURE sp_AltaProducto
    @Nombre VARCHAR(150),
    @Descripcion VARCHAR(500) = NULL,
    @ImagenUrl VARCHAR(500) = NULL,
    @IdMarca INT,
    @IdCategoria INT,
    @PrecioCosto DECIMAL(18,2),
    @PorcentajeGanancia DECIMAL(10,2),
    @StockActual INT,
    @StockMinimo INT
AS
BEGIN
    DECLARE @PrecioVenta DECIMAL(18,2)
    SET @PrecioVenta = @PrecioCosto * (1 + @PorcentajeGanancia / 100)

    INSERT INTO Productos (NombreProducto, Descripcion, ImagenUrl, IdMarca, IdCategoria, PrecioCosto, PorcentajeGanancia, PrecioVenta, StockActual, StockMinimo, Activo)
    VALUES (@Nombre, @Descripcion, @ImagenUrl, @IdMarca, @IdCategoria, @PrecioCosto, @PorcentajeGanancia, @PrecioVenta, @StockActual, @StockMinimo, 1)
END
GO

CREATE PROCEDURE sp_ModificarProducto
    @Id INT,
    @Nombre VARCHAR(150),
    @Descripcion VARCHAR(500) = NULL,
    @ImagenUrl VARCHAR(500) = NULL,
    @IdMarca INT,
    @IdCategoria INT,
    @PrecioCosto DECIMAL(18,2),
    @PorcentajeGanancia DECIMAL(10,2),
    @StockActual INT,
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
        StockActual = @StockActual,
        StockMinimo = @StockMinimo
    WHERE Id = @Id
END
GO

CREATE PROCEDURE sp_BajaProducto
    @Id INT
AS
BEGIN
    UPDATE Productos
    SET Activo = 0
    WHERE Id = @Id
END
GO

-- Clientes:

CREATE PROCEDURE sp_ListarClientes
AS
BEGIN
    SELECT Id, Nombre, Apellido, Dni, Email, Telefono, Direccion, Activo
    FROM Clientes
    WHERE Activo = 1
END
GO

CREATE PROCEDURE sp_AltaCliente
    @Nombre VARCHAR(100),
    @Apellido VARCHAR(100),
    @Dni INT,
    @Email VARCHAR(200) = NULL,
    @Telefono VARCHAR(50) = NULL,
    @Direccion VARCHAR(200) = NULL
AS
BEGIN
    INSERT INTO Clientes (Nombre, Apellido, Dni, Email, Telefono, Direccion, Activo)
    VALUES (@Nombre, @Apellido, @Dni, @Email, @Telefono, @Direccion, 1)
END
GO

CREATE PROCEDURE sp_ModificarCliente
    @Id INT,
    @Nombre VARCHAR(100),
    @Apellido VARCHAR(100),
    @Dni INT,
    @Email VARCHAR(200) = NULL,
    @Telefono VARCHAR(50) = NULL,
    @Direccion VARCHAR(200) = NULL
AS
BEGIN
    UPDATE Clientes
    SET Nombre = @Nombre,
        Apellido = @Apellido,
        Dni = @Dni,
        Email = @Email,
        Telefono = @Telefono,
        Direccion = @Direccion
    WHERE Id = @Id
END
GO

CREATE PROCEDURE sp_BajaCliente
    @Id INT
AS
BEGIN
    UPDATE Clientes
    SET Activo = 0
    WHERE Id = @Id
END
GO

-- Proveedores: 

CREATE PROCEDURE sp_ListarProveedores
AS
BEGIN
    SELECT Id, Nombre, Email, Telefono, Activo
    FROM Proveedores
    WHERE Activo = 1
END
GO

CREATE PROCEDURE sp_AltaProveedor
    @Nombre VARCHAR(150),
    @Email VARCHAR(200) = NULL,
    @Telefono VARCHAR(50) = NULL
AS
BEGIN
    INSERT INTO Proveedores (Nombre, Email, Telefono, Activo)
    VALUES (@Nombre, @Email, @Telefono, 1)
END
GO

CREATE PROCEDURE sp_ModificarProveedor
    @Id INT,
    @Nombre VARCHAR(150),
    @Email VARCHAR(200) = NULL,
    @Telefono VARCHAR(50) = NULL
AS
BEGIN
    UPDATE Proveedores
    SET Nombre = @Nombre,
        Email = @Email,
        Telefono = @Telefono
    WHERE Id = @Id
END
GO

CREATE PROCEDURE sp_BajaProveedor
    @Id INT
AS
BEGIN
    UPDATE Proveedores
    SET Activo = 0
    WHERE Id = @Id
END
GO

-- Usuarios:

CREATE PROCEDURE sp_ListarUsuarios
AS
BEGIN
    SELECT Id, Nombre, Password, Rol, Activo
    FROM Usuarios
    WHERE Activo = 1
END
GO

CREATE PROCEDURE sp_AltaUsuario
    @Nombre VARCHAR(100),
    @Password VARCHAR(255),
    @Rol INT
AS
BEGIN
    INSERT INTO Usuarios (Nombre, Password, Rol, Activo)
    VALUES (@Nombre, @Password, @Rol, 1)
END
GO

CREATE PROCEDURE sp_ModificarUsuario
    @Id INT,
    @Nombre VARCHAR(100),
    @Password VARCHAR(255),
    @Rol INT
AS
BEGIN
    UPDATE Usuarios
    SET Nombre = @Nombre,
        Password = @Password,
        Rol = @Rol
    WHERE Id = @Id
END
GO

CREATE PROCEDURE sp_BajaUsuario
    @Id INT
AS
BEGIN
    UPDATE Usuarios
    SET Activo = 0
    WHERE Id = @Id
END
GO

CREATE PROCEDURE sp_ValidarLogin
    @Nombre VARCHAR(100),
    @Password VARCHAR(255)
AS
BEGIN
    SELECT Id, Nombre, Rol
    FROM Usuarios
    WHERE Nombre = @Nombre AND Password = @Password AND Activo = 1
END
GO
--------------------------------------
USE Comercio;
GO

CREATE PROCEDURE sp_AltaCompra
(
    @IdProveedor INT,
    @Total DECIMAL(18,2)
)
AS
BEGIN

    INSERT INTO Compras
    (
        IdProveedor,
        FechaCompra,
        Total
    )
    VALUES
    (
        @IdProveedor,
        GETDATE(),
        @Total
    );

    SELECT SCOPE_IDENTITY();

END

GO;

CREATE PROCEDURE sp_AltaDetalleCompra
(
    @IdCompra INT,
    @IdProducto INT,
    @Cantidad INT,
    @PrecioUnitario DECIMAL(18,2),
    @Subtotal DECIMAL(18,2)
)
AS
BEGIN

    INSERT INTO DetalleCompras
    (
        IdCompra,
        IdProducto,
        Cantidad,
        PrecioUnitario,
        Subtotal
    )
    VALUES
    (
        @IdCompra,
        @IdProducto,
        @Cantidad,
        @PrecioUnitario,
        @Subtotal
    );

END

GO

CREATE PROCEDURE sp_ActualizarStockCompra
(
    @IdProducto INT,
    @Cantidad INT,
    @PrecioCosto DECIMAL(18,2)
)
AS
BEGIN

    UPDATE Productos
    SET
        StockActual = StockActual + @Cantidad,
        PrecioCosto = @PrecioCosto,
        PrecioVenta = @PrecioCosto + (@PrecioCosto * PorcentajeGanancia / 100)
    WHERE Id = @IdProducto;

END
GO;
-----------------nuevo 
CREATE PROCEDURE sp_AltaVenta
(
    @IdCliente INT,
    @IdUsuario INT,
    @Total DECIMAL(18,2)
)
AS
BEGIN

    INSERT INTO Ventas
    (
        IdCliente,
        IdUsuario,
        FechaVenta,
        Total
    )
    VALUES
    (
        @IdCliente,
        @IdUsuario,
        GETDATE(),
        @Total
    );

    SELECT SCOPE_IDENTITY();

END
GO
CREATE PROCEDURE sp_AltaDetalleVenta
(
    @IdVenta INT,
    @IdProducto INT,
    @Cantidad INT,
    @PrecioUnitario DECIMAL(18,2),
    @Subtotal DECIMAL(18,2)
)
AS
BEGIN

    INSERT INTO DetalleVentas
    (
        IdVenta,
        IdProducto,
        Cantidad,
        PrecioUnitario,
        Subtotal
    )
    VALUES
    (
        @IdVenta,
        @IdProducto,
        @Cantidad,
        @PrecioUnitario,
        @Subtotal
    );

END
GO
    CREATE PROCEDURE sp_ListarDetalleCompras
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        dc.Id,
        dc.IdCompra,
        dc.IdProducto,
        p.NombreProducto,
        dc.Cantidad,
        dc.PrecioUnitario,
        dc.Subtotal
    FROM DetalleCompras dc
    INNER JOIN Productos p
        ON dc.IdProducto = p.Id
    ORDER BY dc.Id DESC;
END
GO

CREATE PROCEDURE sp_ListarCompras
AS
BEGIN
    SELECT
        c.Id,
        c.FechaCompra,
        c.Total,
        p.Id AS IdProveedor,
        p.Nombre AS Proveedor
    FROM Compras c
    INNER JOIN Proveedores p
        ON c.IdProveedor = p.Id
    ORDER BY c.FechaCompra DESC
END
GO
CREATE PROCEDURE sp_AltaVenta
    @IdCliente INT,
    @IdUsuario INT,
    @Total DECIMAL(18,2)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Ventas
    (
        IdCliente,
        IdUsuario,
        FechaVenta,
        Total
    )
    VALUES
    (
        @IdCliente,
        @IdUsuario,
        GETDATE(),
        @Total
    );

    SELECT SCOPE_IDENTITY();
END
GO
CREATE PROCEDURE sp_AltaDetalleVenta
    @IdVenta INT,
    @IdProducto INT,
    @Cantidad INT,
    @PrecioUnitario DECIMAL(18,2),
    @Subtotal DECIMAL(18,2)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO DetalleVentas
    (
        IdVenta,
        IdProducto,
        Cantidad,
        PrecioUnitario,
        Subtotal
    )
    VALUES
    (
        @IdVenta,
        @IdProducto,
        @Cantidad,
        @PrecioUnitario,
        @Subtotal
    );
END

GO

---------

CREATE PROCEDURE sp_ObtenerVentaPorId
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

CREATE PROCEDURE sp_ListarVentas
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        v.Id,
        v.NumeroFactura,
        v.FechaVenta,
        v.Total,
        c.Nombre + ' ' + c.Apellido AS NombreCliente,
        u.Nombre AS NombreVendedor
    FROM Ventas v
    INNER JOIN Clientes c ON v.IdCliente = c.Id
    INNER JOIN Usuarios u ON v.IdUsuario = u.Id
    ORDER BY v.FechaVenta DESC
END

GO

CREATE PROCEDURE sp_ReporteProductosMasVendidos
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        p.Id,
        p.NombreProducto,
        SUM(dv.Cantidad)  AS CantidadVendida,
        SUM(dv.Subtotal)  AS MontoTotal
    FROM DetalleVentas dv
    INNER JOIN Productos p ON dv.IdProducto = p.Id
    GROUP BY p.Id, p.NombreProducto
    ORDER BY CantidadVendida DESC;
END
GO

CREATE PROCEDURE sp_ReporteClientes
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        c.Id,
        c.Nombre + ' ' + c.Apellido AS NombreCliente,
        COUNT(v.Id)   AS CantidadCompras,
        SUM(v.Total)  AS MontoTotal
    FROM Ventas v
    INNER JOIN Clientes c ON v.IdCliente = c.Id
    GROUP BY c.Id, c.Nombre, c.Apellido
    ORDER BY MontoTotal DESC;
END
GO

CREATE PROCEDURE sp_ReporteStockBajo
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        p.Id,
        p.NombreProducto,
        p.StockActual,
        p.StockMinimo,
        m.Nombre AS NombreMarca
    FROM Productos p
    INNER JOIN Marcas m ON p.IdMarca = m.Id
    WHERE p.Activo = 1 AND p.StockActual <= p.StockMinimo
    ORDER BY p.StockActual ASC;
END
GO

CREATE PROCEDURE sp_ReporteVendedores
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        u.Id,
        u.Nombre AS NombreVendedor,
        COUNT(v.Id)      AS CantidadVentas,
        SUM(v.Total)     AS MontoTotal
    FROM Ventas v
    INNER JOIN Usuarios u ON v.IdUsuario = u.Id
    GROUP BY u.Id, u.Nombre
    ORDER BY MontoTotal DESC;
END
GO


CREATE PROCEDURE sp_ObtenerCompraPorId
    @IdCompra INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        c.Id,
        c.FechaCompra,
        c.Total,
        p.Id AS IdProveedor,
        p.Nombre AS NombreProveedor
    FROM Compras c
    INNER JOIN Proveedores p ON c.IdProveedor = p.Id
    WHERE c.Id = @IdCompra;

    SELECT
        dc.Id,
        dc.IdProducto,
        pr.NombreProducto,
        dc.Cantidad,
        dc.PrecioUnitario,
        dc.Subtotal
    FROM DetalleCompras dc
    INNER JOIN Productos pr ON dc.IdProducto = pr.Id
    WHERE dc.IdCompra = @IdCompra;
END

GO;

CREATE PROCEDURE sp_ListarClientesInactivos
AS
BEGIN
    SELECT Id, Nombre, Apellido, Dni, Email, Telefono, Direccion, Activo
    FROM Clientes
    WHERE Activo = 0
END
GO

CREATE PROCEDURE sp_ReactivarCliente
    @Id INT
AS
BEGIN
    UPDATE Clientes
    SET Activo = 1
    WHERE Id = @Id
END
GO
CREATE PROCEDURE sp_ListarProveedoresInactivos
AS
BEGIN
    SELECT Id, Nombre, Email, Telefono, Activo
    FROM Proveedores
    WHERE Activo = 0
END
GO

CREATE PROCEDURE sp_ReactivarProveedor
    @Id INT
AS
BEGIN
    UPDATE Proveedores
    SET Activo = 1
    WHERE Id = @Id
END
GO
CREATE PROCEDURE sp_ListarMarcasInactivas
AS
BEGIN
    SELECT Id, Nombre, Activo
    FROM Marcas
    WHERE Activo = 0
END
GO

CREATE PROCEDURE sp_ReactivarMarca
    @Id INT
AS
BEGIN
    UPDATE Marcas
    SET Activo = 1
    WHERE Id = @Id
END
GO

CREATE PROCEDURE sp_ListarCategoriasInactivas
AS
BEGIN
    SELECT Id, Nombre, Activo
    FROM Categorias
    WHERE Activo = 0
END
GO

CREATE PROCEDURE sp_ReactivarCategoria
    @Id INT
AS
BEGIN
    UPDATE Categorias
    SET Activo = 1
    WHERE Id = @Id
END
GO

CREATE PROCEDURE sp_ListarProductosInactivos
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
    WHERE p.Activo = 0
END
GO

CREATE PROCEDURE sp_ReactivarProducto
    @Id INT
AS
BEGIN
    UPDATE Productos
    SET Activo = 1
    WHERE Id = @Id
END
GO
CREATE PROCEDURE sp_ListarUsuariosInactivos
AS
BEGIN
    SELECT Id, Nombre, Password, Rol, Activo, Email
    FROM Usuarios
    WHERE Activo = 0
END
GO

CREATE PROCEDURE sp_ReactivarUsuario
    @Id INT
AS
BEGIN
    UPDATE Usuarios
    SET Activo = 1
    WHERE Id = @Id
END
GO