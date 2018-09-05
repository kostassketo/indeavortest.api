using System.Threading.Tasks;
using Indeavor.Test.Abstractions.Repositories;

namespace Indeavor.Test.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IndeavorTestDbContext _context;
        private IEmployeeRepository _employeeRepository;
        private IDepartmentRepository _departmentRepository;

        public IEmployeeRepository EmployeeRepository => _employeeRepository ?? (_employeeRepository = new EmployeeRepository(_context));

        public IDepartmentRepository DepartmentRepository => _departmentRepository ?? (_departmentRepository = new DepartmentRepository(_context));

        public UnitOfWork(IndeavorTestDbContext context)
        {
            _context = context;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
