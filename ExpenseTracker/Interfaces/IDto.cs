namespace ExpenseTracker.Interfaces;

public interface IDto<T>
{
    T GetEntity(T? entity);
}