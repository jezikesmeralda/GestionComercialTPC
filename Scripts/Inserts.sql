
-- Datos de Prueba

USE Comercio;
GO

INSERT INTO Marcas (Nombre)
VALUES
('Catena'),
('Rutini'),
('Corona'),
('Quilmes');

INSERT INTO Categorias (Nombre)
VALUES
('Vinos'),
('Cervezas'),
('Destilados');

INSERT INTO Productos
(
NombreProducto,
Descripcion,
IdMarca,
IdCategoria,
PrecioCosto,
PorcentajeGanancia,
PrecioVenta,
StockActual,
StockMinimo
)
VALUES
(
'Corona 710ml',
'Cerveza importada',
3,
2,
1500,
40,
2100,
100,
20
),
(
'Quilmes 1L',
'Cerveza rubia',
4,
2,
1000,
35,
1350,
200,
30
);
GO




-- ALTER TABLE PRODUCTOS



USE Comercio
GO
ALTER TABLE Productos ADD ImagenUrl VARCHAR(300) NULL;
GO
ALTER PROCEDURE sp_ListarProductos
AS
BEGIN
    SELECT p.Id, p.NombreProducto, p.Descripcion, 
           p.PrecioCosto, p.PorcentajeGanancia,
           p.StockActual, p.StockMinimo, p.Activo,
           p.ImagenUrl,
           m.Nombre AS NombreMarca,
           c.Nombre AS NombreCategoria
    FROM Productos p
    INNER JOIN Marcas m ON p.IdMarca = m.Id
    INNER JOIN Categorias c ON p.IdCategoria = c.Id
END