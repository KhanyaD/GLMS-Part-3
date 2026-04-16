-- =====================================================
-- GLMS Database Migration Script - InitialCreate
-- Generated: 2025-01-17
-- Description: Creates initial database schema for 
--              Global Logistics Management System
-- =====================================================

USE [GLMS_Database]
GO

-- =====================================================
-- Create Clients Table
-- =====================================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Clients]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Clients] (
        [Id] INT IDENTITY(1,1) NOT NULL,
        [Name] NVARCHAR(150) NOT NULL,
        [ContactDetails] NVARCHAR(150) NOT NULL,
        [Region] NVARCHAR(100) NOT NULL,
        CONSTRAINT [PK_Clients] PRIMARY KEY CLUSTERED ([Id] ASC)
    );
    PRINT 'Table [Clients] created successfully.';
END
ELSE
BEGIN
    PRINT 'Table [Clients] already exists.';
END
GO

-- =====================================================
-- Create Contracts Table
-- =====================================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Contracts]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Contracts] (
        [Id] INT IDENTITY(1,1) NOT NULL,
        [ClientId] INT NOT NULL,
        [StartDate] DATETIME2 NOT NULL,
        [EndDate] DATETIME2 NOT NULL,
        [Status] INT NOT NULL,
        [ServiceLevel] NVARCHAR(100) NOT NULL,
        [SignedAgreementFileName] NVARCHAR(255) NULL,
        [SignedAgreementPath] NVARCHAR(500) NULL,
        CONSTRAINT [PK_Contracts] PRIMARY KEY CLUSTERED ([Id] ASC)
    );
    PRINT 'Table [Contracts] created successfully.';
END
ELSE
BEGIN
    PRINT 'Table [Contracts] already exists.';
END
GO

-- =====================================================
-- Create ServiceRequests Table
-- =====================================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ServiceRequests]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[ServiceRequests] (
        [Id] INT IDENTITY(1,1) NOT NULL,
        [ContractId] INT NOT NULL,
        [Description] NVARCHAR(500) NOT NULL,
        [CostUsd] DECIMAL(18,2) NOT NULL,
        [LocalCostZar] DECIMAL(18,2) NOT NULL,
        [ExchangeRateUsed] DECIMAL(18,6) NOT NULL,
        [Status] INT NOT NULL,
        [CreatedAtUtc] DATETIME2 NOT NULL,
        CONSTRAINT [PK_ServiceRequests] PRIMARY KEY CLUSTERED ([Id] ASC)
    );
    PRINT 'Table [ServiceRequests] created successfully.';
END
ELSE
BEGIN
    PRINT 'Table [ServiceRequests] already exists.';
END
GO

-- =====================================================
-- Create Foreign Key: Contracts -> Clients
-- =====================================================
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Contracts_Clients_ClientId')
BEGIN
    ALTER TABLE [dbo].[Contracts]
    ADD CONSTRAINT [FK_Contracts_Clients_ClientId] 
        FOREIGN KEY ([ClientId]) 
        REFERENCES [dbo].[Clients]([Id])
        ON DELETE NO ACTION;
    PRINT 'Foreign key [FK_Contracts_Clients_ClientId] created successfully.';
END
ELSE
BEGIN
    PRINT 'Foreign key [FK_Contracts_Clients_ClientId] already exists.';
END
GO

-- =====================================================
-- Create Foreign Key: ServiceRequests -> Contracts
-- =====================================================
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_ServiceRequests_Contracts_ContractId')
BEGIN
    ALTER TABLE [dbo].[ServiceRequests]
    ADD CONSTRAINT [FK_ServiceRequests_Contracts_ContractId] 
        FOREIGN KEY ([ContractId]) 
        REFERENCES [dbo].[Contracts]([Id])
        ON DELETE NO ACTION;
    PRINT 'Foreign key [FK_ServiceRequests_Contracts_ContractId] created successfully.';
END
ELSE
BEGIN
    PRINT 'Foreign key [FK_ServiceRequests_Contracts_ContractId] already exists.';
END
GO

-- =====================================================
-- Create Index: IX_Contracts_ClientId
-- =====================================================
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Contracts_ClientId' AND object_id = OBJECT_ID('Contracts'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Contracts_ClientId] 
    ON [dbo].[Contracts] ([ClientId] ASC);
    PRINT 'Index [IX_Contracts_ClientId] created successfully.';
END
ELSE
BEGIN
    PRINT 'Index [IX_Contracts_ClientId] already exists.';
END
GO

-- =====================================================
-- Create Index: IX_ServiceRequests_ContractId
-- =====================================================
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_ServiceRequests_ContractId' AND object_id = OBJECT_ID('ServiceRequests'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_ServiceRequests_ContractId] 
    ON [dbo].[ServiceRequests] ([ContractId] ASC);
    PRINT 'Index [IX_ServiceRequests_ContractId] created successfully.';
END
ELSE
BEGIN
    PRINT 'Index [IX_ServiceRequests_ContractId] already exists.';
END
GO

PRINT '=====================================================';
PRINT 'Database migration completed successfully!';
PRINT 'Tables created: Clients, Contracts, ServiceRequests';
PRINT 'Foreign keys created: 2';
PRINT 'Indexes created: 2';
PRINT '=====================================================';
GO
