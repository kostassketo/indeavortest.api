using System;
using System.Threading.Tasks;

namespace Indeavor.Test.Abstractions.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository EmployeeRepository { get; }

        IDepartmentRepository DepartmentRepository { get; }

        Task CompleteAsync();
    }
}
