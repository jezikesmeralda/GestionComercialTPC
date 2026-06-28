USE Comercio;
GO

ALTER TABLE Productos ADD ImagenUrl VARCHAR(500) NULL;
GO

SELECT * FROM Clientes;
SELECT * FROM Productos;
SELECT * FROM Ventas;
SELECT * FROM Usuarios;

GO
--AÑADI ESTO---
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
        c.Email AS EmailCliente,        -- 👈 agregado
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

