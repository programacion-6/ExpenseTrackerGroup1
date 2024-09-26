namespace ExpenseTracker.Interfaces;

public interface IInDto<T>
{
    T GetEntity(T? entity);
}