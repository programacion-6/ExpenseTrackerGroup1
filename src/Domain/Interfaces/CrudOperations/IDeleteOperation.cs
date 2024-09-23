public interface IDeleteOperation<T> where T : IEntity
{
    Task<T?> DeleteEntity(Guid entityId);
}
