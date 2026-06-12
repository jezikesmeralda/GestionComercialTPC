
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

SELECT COUNT(*) FROM Productos WHERE Activo = 1