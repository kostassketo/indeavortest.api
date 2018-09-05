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
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly IndeavorTestDbContext _indeavorDbContext;

        public DepartmentRepository(IndeavorTestDbContext indeavorDbContext)
        {
            _indeavorDbContext = indeavorDbContext;
        }

        public async Task<Department> GetByIdAsync(int id)
        {
            return await _indeavorDbContext.Departments.FindAsync(id);
        }

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await _indeavorDbContext.Departments.ToListAsync();
        }

        public async Task<IEnumerable<Department>> QueryAsync(Expression<Func<Department, bool>> predicate)
        {
            return await _indeavorDbContext.Departments.Where(predicate).ToListAsync();
        }

        public async Task<Department> QuerySingleAsync(Expression<Func<Department, bool>> predicate)
        {
            return await _indeavorDbContext.Departments.FirstOrDefaultAsync(predicate);
        }

        public async Task<EntityEntry<Department>> AddOrUpdateAsync(Department entity)
        {
            var existedDepartment = await _indeavorDbContext.Departments.FirstOrDefaultAsync(
                x => x.Id == entity.Id || x.Code == entity.Code);
            if (existedDepartment != null)
            {
                existedDepartment.Update(entity);
                _indeavorDbContext.Entry(existedDepartment).State = EntityState.Modified;

                return _indeavorDbContext.Entry(existedDepartment);
            }

            return await _indeavorDbContext.Departments.AddAsync(entity);
        }

        public async Task<EntityEntry<Department>> RemoveAsync(Department entity)
        {
            if (await _indeavorDbContext.Departments.AnyAsync(x => x.Id == entity.Id))
            {
                return _indeavorDbContext.Departments.Remove(entity);
            }

            return null;
        }
    }
}
