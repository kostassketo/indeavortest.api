using System.Threading.Tasks;
using Indeavor.Test.Abstractions.Dtos;

namespace Indeavor.Test.Abstractions.Services
{
    public interface IEditDepartmentService
    {
        Task<bool> AddOrUpdateAsync(DepartmentDto entity);

        Task<bool> RemoveAsync(int id);
    }
}
