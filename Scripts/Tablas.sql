CREATE DATABASE Comercio;
GO
USE Comercio;
GO


CREATE TABLE Marcas
(
Id INT IDENTITY(1,1) PRIMARY KEY,
Nombre VARCHAR(100) NOT NULL,
Activo BIT NOT NULL DEFAULT 1
);

CREATE TABLE Categorias
(
Id INT IDENTITY(1,1) PRIMARY KEY,
Nombre VARCHAR(100) NOT NULL,
Activo BIT NOT NULL DEFAULT 1
);



CREATE TABLE Productos -- A esta tabla se le agrego el campo IMAGEN
(
Id INT IDENTITY(1,1) PRIMARY KEY,
NombreProducto VARCHAR(150) NOT NULL,
Descripcion VARCHAR(500) NULL,

IdMarca INT NOT NULL,
IdCategoria INT NOT NULL,

PrecioCosto DECIMAL(18,2) NOT NULL,
PorcentajeGanancia DECIMAL(10,2) NOT NULL,
PrecioVenta DECIMAL(18,2) NOT NULL,

StockActual INT NOT NULL DEFAULT 0,
StockMinimo INT NOT NULL DEFAULT 0,

Activo BIT NOT NULL DEFAULT 1,

CONSTRAINT FK_Productos_Marcas
    FOREIGN KEY (IdMarca)
    REFERENCES Marcas(Id),

CONSTRAINT FK_Productos_Categorias
    FOREIGN KEY (IdCategoria)
    REFERENCES Categorias(Id)

);



CREATE TABLE Clientes
(
Id INT IDENTITY(1,1) PRIMARY KEY,


Nombre VARCHAR(100) NOT NULL,
Apellido VARCHAR(100) NOT NULL,

DNI INT NOT NULL UNIQUE,

Email VARCHAR(200) NULL,
Telefono VARCHAR(50) NULL,
Direccion VARCHAR(200) NULL,

Activo BIT NOT NULL DEFAULT 1

);

CREATE TABLE Proveedores
(
Id INT IDENTITY(1,1) PRIMARY KEY,


Nombre VARCHAR(150) NOT NULL,

Email VARCHAR(200) NULL,
Telefono VARCHAR(50) NULL,

Activo BIT NOT NULL DEFAULT 1

);

CREATE TABLE Usuarios
(
    Id INT IDENTITY(1,1) PRIMARY KEY,

    Nombre VARCHAR(100) NOT NULL UNIQUE,
    Password VARCHAR(255) NOT NULL,
    Rol INT NOT NULL, -- 0 = Vendedor, 1 = Administrador

    Activo BIT NOT NULL DEFAULT 1
);


CREATE TABLE Compras
(
    Id INT IDENTITY(1,1) PRIMARY KEY,

    IdProveedor INT NOT NULL,

    FechaCompra DATETIME NOT NULL DEFAULT GETDATE(),

    Total DECIMAL(18,2) NOT NULL,

    CONSTRAINT FK_Compras_Proveedores
        FOREIGN KEY (IdProveedor)
        REFERENCES Proveedores(Id)
);
CREATE TABLE DetalleCompras
(
    Id INT IDENTITY(1,1) PRIMARY KEY,

    IdCompra INT NOT NULL,

    IdProducto INT NOT NULL,

    Cantidad INT NOT NULL,

    PrecioUnitario DECIMAL(18,2) NOT NULL,

    Subtotal DECIMAL(18,2) NOT NULL,

    CONSTRAINT FK_DetalleCompras_Compras
        FOREIGN KEY (IdCompra)
        REFERENCES Compras(Id),

    CONSTRAINT FK_DetalleCompras_Productos
        FOREIGN KEY (IdProducto)
        REFERENCES Productos(Id)
);

---------------------nuevo 
CREATE TABLE Ventas
(
    Id INT IDENTITY(1,1) PRIMARY KEY,

    IdCliente INT NOT NULL,
    IdUsuario INT NOT NULL,

    FechaVenta DATETIME NOT NULL DEFAULT GETDATE(),

    Total DECIMAL(18,2) NOT NULL,

    FOREIGN KEY(IdCliente)
        REFERENCES Clientes(Id),

    FOREIGN KEY(IdUsuario)
        REFERENCES Usuarios(Id)
);

CREATE TABLE DetalleVentas
(
    Id INT IDENTITY(1,1) PRIMARY KEY,

    IdVenta INT NOT NULL,
    IdProducto INT NOT NULL,

    Cantidad INT NOT NULL,

    PrecioUnitario DECIMAL(18,2) NOT NULL,

    Subtotal DECIMAL(18,2) NOT NULL,

    FOREIGN KEY(IdVenta)
        REFERENCES Ventas(Id),

    FOREIGN KEY(IdProducto)
        REFERENCES Productos(Id)
);