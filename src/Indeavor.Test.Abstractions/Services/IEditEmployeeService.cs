using System.Threading.Tasks;
using Indeavor.Test.Abstractions.Dtos;

namespace Indeavor.Test.Abstractions.Services
{
    public interface IEditEmployeeService
    {
        Task<bool> AddOrUpdateAsync(EmployeeDto entity);

        Task<bool> RemoveAsync(int id);
    }
}
