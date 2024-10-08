public interface IIncomeRepository : 
    ICreateOperation<Income>, 
    IReadOperation<Income>, 
    IUpdateOperation<Income>, 
    IDeleteOperation<Income>,
    IGetAllOperation<Income>
{
Task<IEnumerable<Income>> GetIncomesByUserId(Guid userId);
}
