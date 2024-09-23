public interface IGetAllOperation<T> where T : IEntity
{
    Task<List<T>> GetAllEntities();
}
