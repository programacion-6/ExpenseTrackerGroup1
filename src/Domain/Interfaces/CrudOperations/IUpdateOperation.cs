public interface IUpdateOperation<T> where T : IEntity
{
    Task<T?> UpdateEntity(Guid entityId, IDto<T> entityDto);
}
