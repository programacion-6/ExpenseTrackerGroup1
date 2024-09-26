namespace ExpenseTracker.Interfaces;

public interface IGoalRepository : ICreateOperation<Goal>, IReadOperation<Goal>, IUpdateOperation<Goal>
{
    Task<IEnumerable<Goal>> GetGoalsByUserId(Guid userId);
}