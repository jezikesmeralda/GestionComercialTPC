USE Comercio;
GO

ALTER TABLE Productos ADD ImagenUrl VARCHAR(500) NULL;
GO


SELECT * FROM Productos;
--AÑADI ESTO---
ALTER PROCEDURE sp_ListarProductos
AS
BEGIN
    SELECT
        p.Id,
        p.NombreProducto,
        p.Descripcion,
        p.ImagenUrl,
        p.PrecioCosto,
        p.PorcentajeGanancia,
        p.PrecioVenta,
        p.StockActual,
        p.StockMinimo,
        p.Activo,

        m.Id AS IdMarca,
        m.Nombre AS NombreMarca,

        c.Id AS IdCategoria,
        c.Nombre AS NombreCategoria

    FROM Productos p
    INNER JOIN Marcas m ON p.IdMarca = m.Id
    INNER JOIN Categorias c ON p.IdCategoria = c.Id
END
