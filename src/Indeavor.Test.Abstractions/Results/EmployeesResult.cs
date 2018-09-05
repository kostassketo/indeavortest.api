using System.Collections.Generic;
using Indeavor.Test.Abstractions.Dtos;

namespace Indeavor.Test.Abstractions.Results
{
    public class EmployeesResult : MetaDataResult
    {
        public IEnumerable<EmployeeDto> Employees { get; set; }
    }
}
