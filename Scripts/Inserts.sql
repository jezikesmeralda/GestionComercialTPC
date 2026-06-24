
-- Datos de Prueba

USE Comercio;
GO

INSERT INTO Usuarios (Nombre, Password, Rol, Activo)
VALUES ('admin', '1234', 1, 1)

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


INSERT INTO Proveedores (Nombre, Telefono, Email) 
VALUES ('Distribuidora Central S.A.', '1145678901', 'ventas@distribuidoracentral.com');

INSERT INTO Proveedores (Nombre, Telefono, Email) 
VALUES ('Logística y Abasto Norte', '03414890233', 'contacto@abastonorte.com.ar');

INSERT INTO Proveedores (Nombre, Telefono, Email) 
VALUES ('TecnoMayorista SRL', '2614234556', 'info@tecnomayoristasrl.com');

INSERT INTO Proveedores (Nombre, Telefono, Email) 
VALUES ('Global Alimentos Córdoba', '03514785522', 'proveedores@globalalimentos.com');

INSERT INTO Proveedores (Nombre, Telefono, Email) 
VALUES ('Insumos Industriales Sur', '2914552211', 'comercial@insumosur.com');



-------------------

INSERT INTO Clientes (Nombre, Apellido, Dni, Telefono, Email, Direccion) 
VALUES ('Juan Carlos', 'Pérez', '35123456', '1154321098', 'juan.perez@email.com', 'Av. Rivadavia 1540, CABA');

INSERT INTO Clientes (Nombre, Apellido, Dni, Telefono, Email, Direccion) 
VALUES ('María Laura', 'Gómez', '38456789', '03414556677', 'marialaura.gomez@email.com', 'Calle Belgrano 432, Rosario');

INSERT INTO Clientes (Nombre, Apellido, Dni, Telefono, Email, Direccion) 
VALUES ('Lucas', 'Rodríguez', '40987654', '03514889900', 'lucas.rod@email.com', 'Av. Colón 1200, Córdoba');

INSERT INTO Clientes (Nombre, Apellido, Dni, Telefono, Email, Direccion) 
VALUES ('Sofía Belén', 'Fernández', '33456123', '2614332211', 'sofia.f@email.com', 'San Martín 789, Mendoza');

INSERT INTO Clientes (Nombre, Apellido, Dni, Telefono, Email, Direccion) 
VALUES ('Diego Armando', 'Maradona', '10101010', '1110101010', 'eldiego@email.com', 'Segurola y Habana 4310, CABA');