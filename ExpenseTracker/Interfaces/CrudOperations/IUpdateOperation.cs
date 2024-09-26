using ExpenseTracker.Interfaces;

public interface IUpdateOperation<T> where T : IEntity
{
    Task<bool> UpdateEntity(Guid entityId, T entity);
}
