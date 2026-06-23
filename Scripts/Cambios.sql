USE Comercio;
GO

ALTER TABLE Productos ADD ImagenUrl VARCHAR(500) NULL;
GO

SELECT * FROM Clientes;
SELECT * FROM Productos;
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
--NUEVO--
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
----------Nuevo 
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