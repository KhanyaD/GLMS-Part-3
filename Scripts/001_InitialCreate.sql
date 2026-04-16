CREATE TABLE Clients (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(150) NOT NULL,
    ContactDetails NVARCHAR(150) NOT NULL,
    Region NVARCHAR(100) NOT NULL
);

CREATE TABLE Contracts (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ClientId INT NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    Status INT NOT NULL,
    ServiceLevel NVARCHAR(100) NOT NULL,
    SignedAgreementFileName NVARCHAR(255) NULL,
    SignedAgreementPath NVARCHAR(500) NULL,
    CONSTRAINT FK_Contracts_Clients FOREIGN KEY (ClientId) REFERENCES Clients(Id)
);

CREATE TABLE ServiceRequests (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ContractId INT NOT NULL,
    Description NVARCHAR(500) NOT NULL,
    CostUsd DECIMAL(18,2) NOT NULL,
    LocalCostZar DECIMAL(18,2) NOT NULL,
    ExchangeRateUsed DECIMAL(18,6) NOT NULL,
    Status INT NOT NULL,
    CreatedAtUtc DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    CONSTRAINT FK_ServiceRequests_Contracts FOREIGN KEY (ContractId) REFERENCES Contracts(Id)
);
