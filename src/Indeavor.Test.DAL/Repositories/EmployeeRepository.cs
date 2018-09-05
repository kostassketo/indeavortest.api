using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Indeavor.Test.Abstractions.Repositories;
using Indeavor.Test.DAL.Extensions;
using Indeavor.Test.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Indeavor.Test.DAL.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IndeavorTestDbContext _indeavorDbContext;

        public EmployeeRepository(IndeavorTestDbContext indeavorDbContext)
        {
            _indeavorDbContext = indeavorDbContext;
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            return await _indeavorDbContext.Employees.FindAsync(id);
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _indeavorDbContext.Employees.ToListAsync();
        }

        public async Task<IEnumerable<Employee>> QueryAsync(Expression<Func<Employee, bool>> predicate)
        {
            return await _indeavorDbContext.Employees.Where(predicate).ToListAsync();
        }

        public async Task<Employee> GetByIdWithDepartmentAsync(int id)
        {
            return await _indeavorDbContext.Employees.Include(x => x.Department).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Employee> GetByENumberWithDepartmentAsync(string eNumber)
        {
            return await _indeavorDbContext.Employees.Include(x => x.Department).FirstOrDefaultAsync(x => x.ENumber == eNumber);
        }

        public async Task<IQueryable<Employee>> GetAllWithDepartmentAsync()
        {
            var employees = await _indeavorDbContext.Employees.Include(x => x.Department).ToListAsync();

            return employees.AsQueryable();
        }

        public async Task<Employee> QuerySingleAsync(Expression<Func<Employee, bool>> predicate)
        {
            return await _indeavorDbContext.Employees.FirstOrDefaultAsync(predicate);
        }

        public async Task<EntityEntry<Employee>> AddOrUpdateAsync(Employee entity)
        {
            var existedEmployee = await _indeavorDbContext.Employees.FirstOrDefaultAsync(
                x => x.Id == entity.Id || x.Email == entity.Email || x.ENumber == entity.ENumber);
            if (existedEmployee != null)
            {
                existedEmployee.Update(entity);
                _indeavorDbContext.Entry(existedEmployee).State = EntityState.Modified;

                return _indeavorDbContext.Entry(existedEmployee);
            }

            return await _indeavorDbContext.Employees.AddAsync(entity);
        }

        public async Task<EntityEntry<Employee>> RemoveAsync(Employee entity)
        {
            if (await _indeavorDbContext.Employees.AnyAsync(x => x.Id == entity.Id))
            {
                return _indeavorDbContext.Employees.Remove(entity);
            }

            return null;
        }
    }
}
