using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Indeavor.Test.Abstractions.Dtos;
using Indeavor.Test.Abstractions.Repositories;
using Indeavor.Test.Abstractions.Results;
using Indeavor.Test.Abstractions.Services;
using Indeavor.Test.Model;

namespace Indeavor.Test.Services
{
    public class EmployeeService : IEditEmployeeService, IRetrieveEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<EmployeeResult> GetByIdAsync(int id)
        {
            var employee = await _unitOfWork.EmployeeRepository.GetByIdAsync(id);
            var employeeDto = _mapper.Map<EmployeeDto>(employee);

            return new EmployeeResult
            {
                Employee = employeeDto,
                Succeeded = employeeDto != null
            };
        }

        public async Task<EmployeesResult> GetAllAsync()
        {
            var employees = await _unitOfWork.EmployeeRepository.GetAllAsync();
            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employees);

            return new EmployeesResult
            {
                Employees = employeesDto,
                Succeeded = employeesDto != null && employeesDto.Any()
            };
        }

        public async Task<EmployeesResult> QueryAsync(Expression<Func<Employee, bool>> predicate)
        {
            var employees = await _unitOfWork.EmployeeRepository.QueryAsync(predicate);
            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employees);

            return new EmployeesResult
            {
                Employees = employeesDto,
                Succeeded = employeesDto != null && employeesDto.Any()
            };
        }

        public async Task<EmployeeResult> QuerySingleAsync(Expression<Func<Employee, bool>> predicate)
        {
            var employee = await _unitOfWork.EmployeeRepository.QuerySingleAsync(predicate);
            var employeeDto = _mapper.Map<EmployeeDto>(employee);

            return new EmployeeResult
            {
                Employee = employeeDto,
                Succeeded = employeeDto != null
            };
        }

        public async Task<EmployeeResult> GetByIdWithDepartmentAsync(int id)
        {
            var employee = await _unitOfWork.EmployeeRepository.GetByIdWithDepartmentAsync(id);
            var employeeDto = _mapper.Map<EmployeeDto>(employee);

            return new EmployeeResult
            {
                Employee = employeeDto,
                Succeeded = employeeDto != null
            };
        }

        public async Task<EmployeeResult> GetByENumberWithDepartmentAsync(string eNumber)
        {
            var employee = await _unitOfWork.EmployeeRepository.GetByENumberWithDepartmentAsync(eNumber);
            var employeeDto = _mapper.Map<EmployeeDto>(employee);

            return new EmployeeResult
            {
                Employee = employeeDto,
                Succeeded = employeeDto != null
            };
        }

        public async Task<EmployeesResult> GetAllWithDepartmentAsync()
        {
            var employees = await _unitOfWork.EmployeeRepository.GetAllWithDepartmentAsync();
            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employees);

            return new EmployeesResult
            {
                Employees = employeesDto,
                Succeeded = employeesDto != null && employeesDto.Any()
            };
        }

        public async Task<bool> AddOrUpdateAsync(EmployeeDto entity)
        {
            var employee = _mapper.Map<Employee>(entity);
            if (entity.DepartmentId.HasValue)
            {
                employee.Department = await _unitOfWork.DepartmentRepository.GetByIdAsync(entity.DepartmentId.Value);
            }
            var entityEntry = await _unitOfWork.EmployeeRepository.AddOrUpdateAsync(employee);
            await _unitOfWork.CompleteAsync();

            return entityEntry.Entity != null;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var employee = await _unitOfWork.EmployeeRepository.GetByIdAsync(id);
            var entityEntry = await _unitOfWork.EmployeeRepository.RemoveAsync(employee);
            await _unitOfWork.CompleteAsync();

            return entityEntry.Entity != null;
        }
    }
}
