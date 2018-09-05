using AutoMapper;
using Indeavor.Test.Abstractions.Dtos;
using Indeavor.Test.Model;

namespace Indeavor.Test.Services.Mappings
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeDto, Employee>();
        }
    }
}
