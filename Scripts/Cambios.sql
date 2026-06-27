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


---------------------------
ALTER TABLE Ventas
ADD NumeroFactura VARCHAR(20) UNIQUE;


ALTER PROCEDURE sp_AltaVenta
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

   
    DECLARE @IdVenta INT;
    SET @IdVenta = SCOPE_IDENTITY();

    
    UPDATE Ventas
    SET NumeroFactura = CAST(@IdVenta AS VARCHAR(20))
    WHERE Id = @IdVenta;

    
    SELECT @IdVenta;
END
GO