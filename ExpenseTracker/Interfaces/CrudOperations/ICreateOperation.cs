public interface ICreateOperation<T> where T : IEntity{
    Task<T> CreateEntity(T entityModel);
}