INSERT INTO Users (Id, Name, Email, PasswordHash, CreatedAt) VALUES
    (gen_random_uuid(), 'John Doe', 'john.doe@example.com', 'hashedpassword1', CURRENT_TIMESTAMP),
    (gen_random_uuid(), 'Jane Smith', 'jane.smith@example.com', 'hashedpassword2', CURRENT_TIMESTAMP),
    (gen_random_uuid(), 'Carlos García', 'carlos.garcia@example.com', 'hashedpassword3', CURRENT_TIMESTAMP),
    (gen_random_uuid(), 'Ana Martinez', 'ana.martinez@example.com', 'hashedpassword4', CURRENT_TIMESTAMP),
    (gen_random_uuid(), 'Sara Lopez', 'sara.lopez@example.com', 'hashedpassword5', CURRENT_TIMESTAMP),
    (gen_random_uuid(), 'Luis Rodríguez', 'luis.rodriguez@example.com', 'hashedpassword6', CURRENT_TIMESTAMP),
    (gen_random_uuid(), 'Maria Torres', 'maria.torres@example.com', 'hashedpassword7', CURRENT_TIMESTAMP),
    (gen_random_uuid(), 'David Alvarez', 'david.alvarez@example.com', 'hashedpassword8', CURRENT_TIMESTAMP),
    (gen_random_uuid(), 'Paula Perez', 'paula.perez@example.com', 'hashedpassword9', CURRENT_TIMESTAMP),
    (gen_random_uuid(), 'Miguel Castillo', 'miguel.castillo@example.com', 'hashedpassword10', CURRENT_TIMESTAMP);

INSERT INTO Budget (Id, UserId, BudgetAmount, Month, CreatedAt) VALUES
    (gen_random_uuid(), (SELECT Id FROM Users WHERE Name = 'John Doe'), 1000.00, '2024-09-01', CURRENT_TIMESTAMP),
    (gen_random_uuid(), (SELECT Id FROM Users WHERE Name = 'Jane Smith'), 1500.50, '2024-09-01', CURRENT_TIMESTAMP),
    (gen_random_uuid(), (SELECT Id FROM Users WHERE Name = 'Carlos García'), 2000.00, '2024-09-01', CURRENT_TIMESTAMP),
    (gen_random_uuid(), (SELECT Id FROM Users WHERE Name = 'Ana Martinez'), 1200.75, '2024-08-01', CURRENT_TIMESTAMP),
    (gen_random_uuid(), (SELECT Id FROM Users WHERE Name = 'Sara Lopez'), 800.00, '2024-09-01', CURRENT_TIMESTAMP),
    (gen_random_uuid(), (SELECT Id FROM Users WHERE Name = 'Luis Rodríguez'), 2500.00, '2024-09-01', CURRENT_TIMESTAMP),
    (gen_random_uuid(), (SELECT Id FROM Users WHERE Name = 'Maria Torres'), 1750.50, '2024-07-01', CURRENT_TIMESTAMP),
    (gen_random_uuid(), (SELECT Id FROM Users WHERE Name = 'David Alvarez'), 950.25, '2024-08-01', CURRENT_TIMESTAMP),
    (gen_random_uuid(), (SELECT Id FROM Users WHERE Name = 'Paula Perez'), 1100.00, '2024-06-01', CURRENT_TIMESTAMP),
    (gen_random_uuid(), (SELECT Id FROM Users WHERE Name = 'Miguel Castillo'), 3000.00, '2024-09-01', CURRENT_TIMESTAMP);


INSERT INTO Expense (Id, UserId, Amount, Description, Category, Date, CreatedAt) VALUES
    (gen_random_uuid(), (SELECT Id FROM Users WHERE Name = 'John Doe'), 200.00, 'Groceries', 'Food', '2024-09-12', CURRENT_TIMESTAMP),
    (gen_random_uuid(), (SELECT Id FROM Users WHERE Name = 'Jane Smith'), 150.75, 'Electricity Bill', 'Utilities', '2024-09-10', CURRENT_TIMESTAMP),
    (gen_random_uuid(), (SELECT Id FROM Users WHERE Name = 'Carlos García'), 300.00, 'Rent', 'Housing', '2024-09-01', CURRENT_TIMESTAMP),
    (gen_random_uuid(), (SELECT Id FROM Users WHERE Name = 'Ana Martinez'), 50.00, 'Internet Bill', 'Utilities', '2024-08-22', CURRENT_TIMESTAMP),
    (gen_random_uuid(), (SELECT Id FROM Users WHERE Name = 'Sara Lopez'), 120.00, 'Gym Membership', 'Fitness', '2024-09-05', CURRENT_TIMESTAMP),
    (gen_random_uuid(), (SELECT Id FROM Users WHERE Name = 'Luis Rodríguez'), 500.00, 'Car Repair', 'Transport', '2024-09-07', CURRENT_TIMESTAMP),
    (gen_random_uuid(), (SELECT Id FROM Users WHERE Name = 'Maria Torres'), 45.25, 'Dining Out', 'Food', '2024-07-12', CURRENT_TIMESTAMP),
    (gen_random_uuid(), (SELECT Id FROM Users WHERE Name = 'David Alvarez'), 80.00, 'New Shoes', 'Clothing', '2024-08-15', CURRENT_TIMESTAMP),
    (gen_random_uuid(), (SELECT Id FROM Users WHERE Name = 'Paula Perez'), 220.50, 'Travel Tickets', 'Transport', '2024-06-20', CURRENT_TIMESTAMP),
    (gen_random_uuid(), (SELECT Id FROM Users WHERE Name = 'Miguel Castillo'), 75.00, 'Books', 'Education', '2024-09-14', CURRENT_TIMESTAMP);

INSERT INTO Goal (Id, UserId, GoalAmount, CurrentAmount, Deadline) VALUES
   (gen_random_uuid(), (SELECT Id FROM Users WHERE Name = 'John Doe'), 5000.00, 1000.00, '2025-12-31'),
   (gen_random_uuid(), (SELECT Id FROM Users WHERE Name = 'Jane Smith'), 8000.00, 2500.00, '2026-06-30'),
   (gen_random_uuid(), (SELECT Id FROM Users WHERE Name = 'Carlos García'), 3000.00, 500.00, '2025-09-01'),
   (gen_random_uuid(), (SELECT Id FROM Users WHERE Name = 'Ana Martinez'), 1500.00, 700.00, '2024-12-31'),
   (gen_random_uuid(), (SELECT Id FROM Users WHERE Name = 'Sara Lopez'), 2000.00, 400.00, '2025-03-31'),
   (gen_random_uuid(), (SELECT Id FROM Users WHERE Name = 'Luis Rodríguez'), 10000.00, 5000.00, '2027-12-31'),
   (gen_random_uuid(), (SELECT Id FROM Users WHERE Name = 'Maria Torres'), 6000.00, 3500.00, '2025-06-30'),
   (gen_random_uuid(), (SELECT Id FROM Users WHERE Name = 'David Alvarez'), 4000.00, 1200.00, '2024-11-30'),
   (gen_random_uuid(), (SELECT Id FROM Users WHERE Name = 'Paula Perez'), 2500.00, 500.00, '2024-10-31'),
   (gen_random_uuid(), (SELECT Id FROM Users WHERE Name = 'Miguel Castillo'), 12000.00, 3000.00, '2028-12-31');

INSERT INTO Income (Id, UserId, Amount, Source, Date, CreatedAt) VALUES
    (gen_random_uuid(), (SELECT Id FROM Users WHERE Name = 'John Doe'), 1500.00, 'Freelancing', '2024-09-05', CURRENT_TIMESTAMP),
    (gen_random_uuid(), (SELECT Id FROM Users WHERE Name = 'Jane Smith'), 2500.00, 'Salary', '2024-09-01', CURRENT_TIMESTAMP),
    (gen_random_uuid(), (SELECT Id FROM Users WHERE Name = 'Carlos García'), 1200.00, 'Online Sales', '2024-09-03', CURRENT_TIMESTAMP),
    (gen_random_uuid(), (SELECT Id FROM Users WHERE Name = 'Ana Martinez'), 3000.00, 'Business Income', '2024-08-28', CURRENT_TIMESTAMP),
    (gen_random_uuid(), (SELECT Id FROM Users WHERE Name = 'Sara Lopez'), 400.00, 'Tutoring', '2024-09-08', CURRENT_TIMESTAMP),
    (gen_random_uuid(), (SELECT Id FROM Users WHERE Name = 'Luis Rodríguez'), 1800.00, 'Consulting', '2024-09-02', CURRENT_TIMESTAMP),
    (gen_random_uuid(), (SELECT Id FROM Users WHERE Name = 'Maria Torres'), 750.00, 'Freelancing', '2024-07-20', CURRENT_TIMESTAMP),
    (gen_random_uuid(), (SELECT Id FROM Users WHERE Name = 'David Alvarez'), 2100.00, 'Salary', '2024-08-01', CURRENT_TIMESTAMP),
    (gen_random_uuid(), (SELECT Id FROM Users WHERE Name = 'Paula Perez'), 500.00, 'Part-time Job', '2024-06-25', CURRENT_TIMESTAMP),
    (gen_random_uuid(), (SELECT Id FROM Users WHERE Name = 'Miguel Castillo'), 2200.00, 'Consulting', '2024-09-09', CURRENT_TIMESTAMP);
