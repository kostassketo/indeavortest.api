using System.Collections.Generic;
using Indeavor.Test.Abstractions.Dtos;

namespace Indeavor.Test.Abstractions.Results
{
    public class DepartmentsResult : MetaDataResult
    {
        public IEnumerable<DepartmentDto> Departments { get; set; }
    }
}
