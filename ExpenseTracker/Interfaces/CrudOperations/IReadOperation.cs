public interface IReadOperation<T> where T : IEntity
{
    Task<T?> ReadEntity(Guid entityId);
}