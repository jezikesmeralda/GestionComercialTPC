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