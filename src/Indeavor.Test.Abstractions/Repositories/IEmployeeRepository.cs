using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Indeavor.Test.Model;

namespace Indeavor.Test.Abstractions.Repositories
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<Employee> GetByIdWithDepartmentAsync(int id);

        Task<Employee> GetByENumberWithDepartmentAsync(string eNumber);

        Task<IQueryable<Employee>> GetAllWithDepartmentAsync();

        Task<Employee> QuerySingleAsync(Expression<Func<Employee, bool>> predicate);
    }
}
