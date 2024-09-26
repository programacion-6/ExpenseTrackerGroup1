namespace ExpenseTracker.Interfaces.Service;

public interface IOutDto<T, R> where T : IOutDto<T, R> where R : IEntity
{
    T GetDto(R entity);
}