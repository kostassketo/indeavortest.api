using AutoMapper;
using Indeavor.Test.Abstractions.Dtos;
using Indeavor.Test.Model;

namespace Indeavor.Test.Services.Mappings
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, DepartmentDto>();
            CreateMap<DepartmentDto, Department>()
                .ForMember(d => d.Employees, d => d.Ignore());
        }
    }
}
