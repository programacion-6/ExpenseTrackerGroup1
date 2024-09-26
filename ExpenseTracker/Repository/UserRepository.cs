using Dapper;
using ExpenseTracker.Domain;
using ExpenseTracker.Interfaces;
using ExpenseTracker.Persistence.Database.Interface;

namespace ExpenseTracker.Repository;

public class UserRepository : IUserRepository
{
    private readonly IDbConnectionFactory _dbConnection;

    public UserRepository(IDbConnectionFactory dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<User?> ReadEntity(Guid entityId)
    {
        var sql = "SELECT * FROM Users WHERE Id = @Id";
        using var connection = await _dbConnection.CreateConnectionAsync();
        return await connection.QueryFirstOrDefaultAsync<User>(sql, new { Id = entityId });
    }

    public async Task<List<User>> GetAllEntities()
    {
        var sql = "SELECT * FROM Users";
        using var connection = await _dbConnection.CreateConnectionAsync();
        return (await connection.QueryAsync<User>(sql)).AsList();
    }

    public async Task<bool> UpdateEntity(Guid entityId, User entity)
    {
        var sql = @"UPDATE Users
                    SET Name = @Name,
                        Email = @Email,
                        PasswordHash = @PasswordHash
                    WHERE Id = @Id";
        using var connection = await _dbConnection.CreateConnectionAsync();
        var affectedRows = await connection.ExecuteAsync(sql, entity);
        return affectedRows > 0;
    }

    public async Task<User?> DeleteEntity(Guid entityId)
    {
        var sql = "DELETE FROM Users WHERE Id= @Id";
        using var connection = await _dbConnection.CreateConnectionAsync();
        await connection.ExecuteAsync(sql, new { Id = entityId });
        return null; 
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        var sql = "SELECT * FROM Users WHERE Email = @Email";
        using var connection = await _dbConnection.CreateConnectionAsync();
        return await connection.QueryFirstOrDefaultAsync<User>(sql, new { Email = email });
    }
}
