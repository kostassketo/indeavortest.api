using System.Linq;
using Indeavor.Test.Model;

namespace Indeavor.Test.DAL.Extensions
{
    public static class DepartmentExtensions
    {
        public static void Update(this Department current, Department updated)
        {
            if (!string.IsNullOrEmpty(updated.Name) && updated.Name != current.Name)
            {
                current.Name = updated.Name;
            }
            if (!string.IsNullOrEmpty(updated.Code) && updated.Code != current.Code)
            {
                current.Code = updated.Code;
            }
            if (updated.Employees != null && updated.Employees.Any())
            {
                current.Employees = updated.Employees;
            }
        }
    }
}
