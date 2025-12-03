-- 1. Creamos la BD (si no existe)
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'TekusDb')
BEGIN
    CREATE DATABASE TekusDb;
END
GO

USE TekusDb;
GO

-- 2. Tabla de Proveedores (Providers)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Providers')
BEGIN
    CREATE TABLE Providers (
        Id UNIQUEIDENTIFIER PRIMARY KEY,
        Nit NVARCHAR(20) NOT NULL,
        Name NVARCHAR(150) NOT NULL,
        Email NVARCHAR(100) NOT NULL,
        CustomAttributes NVARCHAR(MAX) NULL, 
        CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE()
    );
    CREATE UNIQUE INDEX IX_Providers_Nit ON Providers(Nit);
END
GO

-- 3. Tabla de Servicios (Services)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Services')
BEGIN
    CREATE TABLE Services (
        Id UNIQUEIDENTIFIER PRIMARY KEY,
        Name NVARCHAR(150) NOT NULL,
        HourlyRate DECIMAL(18,2) NOT NULL,
        ProviderId UNIQUEIDENTIFIER NOT NULL,
        Countries NVARCHAR(MAX) NULL,
        CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),

        -- Relación Foreign Key
        CONSTRAINT FK_Services_Providers_ProviderId 
            FOREIGN KEY (ProviderId) 
            REFERENCES Providers(Id) 
            ON DELETE NO ACTION
    );
    CREATE INDEX IX_Services_ProviderId ON Services(ProviderId);
END
GO

-- 4. asegurar que sea un JSON válido
ALTER TABLE Providers
    ADD CONSTRAINT CK_Providers_CustomAttributes_IsJson 
    CHECK (CustomAttributes IS NULL OR ISJSON(CustomAttributes) = 1);

ALTER TABLE Services
    ADD CONSTRAINT CK_Services_Countries_IsJson 
    CHECK (Countries IS NULL OR ISJSON(Countries) = 1);
GO
