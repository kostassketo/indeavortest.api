using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Indeavor.Test.Abstractions.Results;
using Indeavor.Test.Model;

namespace Indeavor.Test.Abstractions.Services
{
    public interface IRetrieveEmployeeService
    {
        Task<EmployeeResult> GetByIdAsync(int id);

        Task<EmployeesResult> GetAllAsync();

        Task<EmployeesResult> QueryAsync(Expression<Func<Employee, bool>> predicate);

        Task<EmployeeResult> QuerySingleAsync(Expression<Func<Employee, bool>> predicate);

        Task<EmployeeResult> GetByIdWithDepartmentAsync(int id);

        Task<EmployeeResult> GetByENumberWithDepartmentAsync(string eNumber);

        Task<EmployeesResult> GetAllWithDepartmentAsync();
    }
}
