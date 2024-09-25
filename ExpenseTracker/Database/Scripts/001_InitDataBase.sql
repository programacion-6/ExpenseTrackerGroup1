CREATE TABLE IF NOT EXISTS Users (
    Id UUID PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Email VARCHAR(255) NOT NULL UNIQUE,
    PasswordHash TEXT NOT NULL,
    CreatedAt TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE IF NOT EXISTS Budget (
    Id UUID PRIMARY KEY,
    UserId UUID NOT NULL REFERENCES Users(Id) ON DELETE CASCADE,
    BudgetAmount DECIMAL(18, 2) NOT NULL CHECK (BudgetAmount >= 0), 
    Month VARCHAR(7) NOT NULL, 
    CONSTRAINT uq_budget UNIQUE (UserId, Month) 
);

CREATE TABLE IF NOT EXISTS Expense (
    Id UUID PRIMARY KEY,
    UserId UUID NOT NULL REFERENCES Users(Id) ON DELETE CASCADE,
    Amount DECIMAL(18, 2) NOT NULL CHECK (Amount >= 0),
    Description TEXT NOT NULL,
    Category VARCHAR(50) NOT NULL,
    Date TIMESTAMP NOT NULL,
    CreatedAt TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE IF NOT EXISTS Goal (
    Id UUID PRIMARY KEY,
    UserId UUID NOT NULL REFERENCES Users(Id) ON DELETE CASCADE,
    GoalAmount DECIMAL(18, 2) NOT NULL CHECK (GoalAmount > 0), 
    CurrentAmount DECIMAL(18, 2) DEFAULT 0 CHECK (CurrentAmount >= 0), 
    Deadline DATE NOT NULL
);

CREATE TABLE IF NOT EXISTS Income (
    Id UUID PRIMARY KEY,
    UserId UUID NOT NULL REFERENCES Users(Id) ON DELETE CASCADE,
    Amount DECIMAL(18, 2) NOT NULL CHECK (Amount > 0), 
    Source VARCHAR(100) NOT NULL,
    Date TIMESTAMP NOT NULL,
    CreatedAt TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);
