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
    public class DepartmentService : IEditDepartmentService, IRetrieveDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DepartmentResult> GetByIdAsync(int id)
        {
            var department = await _unitOfWork.DepartmentRepository.GetByIdAsync(id);
            var departmentDto = _mapper.Map<DepartmentDto>(department);

            return new DepartmentResult
            {
                Department = departmentDto,
                Succeeded = departmentDto != null
            };
        }

        public async Task<DepartmentsResult> GetAllAsync()
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            var departmentsDto = _mapper.Map<List<DepartmentDto>>(departments);

            return new DepartmentsResult
            {
                Departments = departmentsDto,
                Succeeded = departmentsDto != null && departmentsDto.Any()
            };
        }

        public async Task<DepartmentsResult> QueryAsync(Expression<Func<Department, bool>> predicate)
        {
            var departments = await _unitOfWork.DepartmentRepository.QueryAsync(predicate);
            var departmentsDto = _mapper.Map<List<DepartmentDto>>(departments);

            return new DepartmentsResult
            {
                Departments = departmentsDto,
                Succeeded = departmentsDto != null && departmentsDto.Any()
            };
        }

        public async Task<DepartmentResult> QuerySingleAsync(Expression<Func<Department, bool>> predicate)
        {
            var department = await _unitOfWork.DepartmentRepository.QuerySingleAsync(predicate);
            var departmentDto = _mapper.Map<DepartmentDto>(department);

            return new DepartmentResult
            {
                Department = departmentDto,
                Succeeded = departmentDto != null
            };
        }

        public async Task<bool> AddOrUpdateAsync(DepartmentDto entity)
        {
            var department = _mapper.Map<Department>(entity);
            var entityEntry = await _unitOfWork.DepartmentRepository.AddOrUpdateAsync(department);
            await _unitOfWork.CompleteAsync();

            return entityEntry.Entity != null;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var department = await _unitOfWork.DepartmentRepository.GetByIdAsync(id);
            var entityEntry = await _unitOfWork.DepartmentRepository.RemoveAsync(department);
            await _unitOfWork.CompleteAsync();

            return entityEntry.Entity != null;
        }
    }
}
