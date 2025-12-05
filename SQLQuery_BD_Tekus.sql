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

-- 5. Datos Bases Para Insertar

-- Proveedores
DECLARE @P1Id UNIQUEIDENTIFIER = 'C0877013-1A1F-415D-ABFA-6C261C81F01A';
DECLARE @P2Id UNIQUEIDENTIFIER = '2E8F19A2-C0A5-42E1-A2B3-7D1E8F29A32B';
DECLARE @P3Id UNIQUEIDENTIFIER = '9A1F4C3F-7B2D-4E90-B8A1-3C4D5F6A7B8C';
DECLARE @P4Id UNIQUEIDENTIFIER = '4D6E7C9A-B0C1-4D2F-A4E5-6F7D8C9A0B1C';
DECLARE @P5Id UNIQUEIDENTIFIER = '7F8A9B0C-1D2E-3F4A-5B6C-7D8E9F0A1B2C';
DECLARE @P6Id UNIQUEIDENTIFIER = '5B4C3A2D-1E0F-9C8B-7A6D-5E4F392B1C0F';
DECLARE @P7Id UNIQUEIDENTIFIER = '1A2B3C4D-5E6F-7A8B-9C0D-1E2F3A4B5C6D';
DECLARE @P8Id UNIQUEIDENTIFIER = 'E9F8A7D6-C5B4-A3D2-E1F0-9C8B7A6D5E4F';
DECLARE @P9Id UNIQUEIDENTIFIER = '3B4C5D6E-7F8A-9B0C-1D2E-3F4A5B6C7D8E';
DECLARE @P10Id UNIQUEIDENTIFIER = '8A9B0C1D-2E3F-4A5B-6C7D-8E9F0A1B2C3D';

INSERT INTO Providers (Id, Name, NIT, Email, CustomAttributes, CreatedAt)
VALUES
(@P1Id, 'GlobalTech Solutions', '900500123-1', 'sales@globaltech.com', '{"CódigoSwift": "GTSUSNYC"}', GETDATE()),
(@P2Id, 'Ecológica Andina S.A.S', '800456000-0', 'contact@ecoandina.co', '{}', GETDATE()),
(@P3Id, 'DataSecure Labs', '777121212-9', 'admin@datasecure.net', '{"TipoLicencia": "Enterprise"}', GETDATE()),
(@P4Id, 'Constructora del Sol', '901000100-5', 'proyectos@sol.com.mx', '{}', GETDATE()),
(@P5Id, 'Logística Rápida Ltda.', '830300400-5', 'dispatch@rapidalog.net', '{"CoberturaRegión": "LATAM"}', GETDATE()),
(@P6Id, 'Marketing Digital 360', '80000000-9', 'info@marketing360.com', '{}', GETDATE()),
(@P7Id, 'IT Consulting Hub', '999111222-7', 'help@itchub.com', '{"CertificadoISO": "9001"}', GETDATE()),
(@P8Id, 'Finanzas Claras Corp.', '70000000-4', 'payroll@fclara.com', '{}', GETDATE()),
(@P9Id, 'Recursos Humanos Óptimos', '81100050-3', 'rrhh@optimos.com', '{}', GETDATE()),
(@P10Id, 'Agro Industrias Tierra Viva', '90090090-2', 'agro@tierra.com', '{"LotesProducción": "3"}', GETDATE());

-- Servicios

INSERT INTO Services (Id, Name, HourlyRate, ProviderId, Countries, CreatedAt)
VALUES
(NEWID(), 'Desarrollo de Software', 85.00, 'C0877013-1A1F-415D-ABFA-6C261C81F01A', '["CO", "MX", "PE"]', GETDATE()),
(NEWID(), 'Consultoría en IA', 120.00, 'C0877013-1A1F-415D-ABFA-6C261C81F01A', '["US", "CA"]', GETDATE()),
(NEWID(), 'Auditoría Ambiental', 55.50, '2E8F19A2-C0A5-42E1-A2B3-7D1E8F29A32B', '["CO", "EC"]', GETDATE()),
(NEWID(), 'Gestión de Ciberseguridad', 95.00, '9A1F4C3F-7B2D-4E90-B8A1-3C4D5F6A7B8C', '["US"]', GETDATE()),
(NEWID(), 'Diseño Estructural', 78.00, '4D6E7C9A-B0C1-4D2F-A4E5-6F7D8C9A0B1C', '["MX", "PA"]', GETDATE()),
(NEWID(), 'Transporte Marítimo Express', 150.00, '7F8A9B0C-1D2E-3F4A-5B6C-7D8E9F0A1B2C', '["CO", "BR", "AR"]', GETDATE()),
(NEWID(), 'Campañas en Redes Sociales', 45.00, '5B4C3A2D-1E0F-9C8B-7A6D-5E4F392B1C0F', '["ES", "CO"]', GETDATE()),
(NEWID(), 'Soporte Técnico 24/7', 60.00, '1A2B3C4D-5E6F-7A8B-9C0D-1E2F3A4B5C6D', '["US", "GB", "AU"]', GETDATE()),
(NEWID(), 'Nómina y Contabilidad', 50.00, 'E9F8A7D6-C5B4-A3D2-E1F0-9C8B7A6D5E4F', '["CO"]', GETDATE()),
(NEWID(), 'Reclutamiento Ejecutivo', 72.50, '3B4C5D6E-7F8A-9B0C-1D2E-3F4A5B6C7D8E', '["MX", "CO"]', GETDATE());
