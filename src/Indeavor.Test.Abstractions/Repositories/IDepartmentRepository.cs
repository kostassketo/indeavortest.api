using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Indeavor.Test.Model;

namespace Indeavor.Test.Abstractions.Repositories
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        Task<Department> QuerySingleAsync(Expression<Func<Department, bool>> predicate);
    }
}
