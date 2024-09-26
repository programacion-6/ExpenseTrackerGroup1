using ExpenseTracker.Domain;

namespace ExpenseTracker.Interfaces;

public interface IExpenseRepository : 
    ICreateOperation<Expense>, 
    IReadOperation<Expense>, 
    IUpdateOperation<Expense>, 
    IDeleteOperation<Expense>, 
    IGetAllOperation<Expense>
{

}