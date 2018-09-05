using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Indeavor.Test.Abstractions.Results;
using Indeavor.Test.Model;

namespace Indeavor.Test.Abstractions.Services
{
    public interface IRetrieveDepartmentService
    {
        Task<DepartmentResult> GetByIdAsync(int id);

        Task<DepartmentsResult> GetAllAsync();

        Task<DepartmentsResult> QueryAsync(Expression<Func<Department, bool>> predicate);

        Task<DepartmentResult> QuerySingleAsync(Expression<Func<Department, bool>> predicate);
    }
}
