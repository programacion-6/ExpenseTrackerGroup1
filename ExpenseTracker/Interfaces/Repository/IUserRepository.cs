using ExpenseTracker.Domain;

namespace ExpenseTracker.Interfaces;

public interface IUserRepository : IReadOperation<User>, IGetAllOperation<User>, IUpdateOperation<User>, IDeleteOperation<User>, ICreateOperation<User>
{
    public Task<User?> GetUserByEmail(string email);
}