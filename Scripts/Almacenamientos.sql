USE Comercio;


GO
CREATE PROCEDURE sp_ListarMarcas
AS
BEGIN
    SELECT Id, Nombre, Activo
    FROM Marcas
    WHERE Activo = 1
END


GO
CREATE PROCEDURE sp_ListarCategorias
AS
BEGIN
    SELECT Id, Nombre, Activo
    FROM Categorias
    WHERE Activo = 1
END

GO
CREATE PROCEDURE sp_ListarProductos
AS
BEGIN
    SELECT p.Id, p.NombreProducto, p.Descripcion, 
           p.PrecioCosto, p.PorcentajeGanancia, p.PrecioVenta,
           p.StockActual, p.StockMinimo, p.Activo,
           m.Nombre AS NombreMarca,
           c.Nombre AS NombreCategoria
    FROM Productos p
    INNER JOIN Marcas m ON p.IdMarca = m.Id
    INNER JOIN Categorias c ON p.IdCategoria = c.Id
END
GO
CREATE PROCEDURE sp_prueba
AS
BEGIN
    SELECT * from Categorias;
END

