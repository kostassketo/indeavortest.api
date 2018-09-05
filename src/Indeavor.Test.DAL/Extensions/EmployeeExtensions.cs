using System;
using Indeavor.Test.Model;

namespace Indeavor.Test.DAL.Extensions
{
    public static class EmployeeExtensions
    {
        public static void Update(this Employee current, Employee updated)
        {
            if (!string.IsNullOrEmpty(updated.FirstName) && updated.FirstName != current.FirstName)
            {
                current.FirstName = updated.FirstName;
            }
            if (!string.IsNullOrEmpty(updated.LastName) && updated.LastName != current.LastName)
            {
                current.LastName = updated.LastName;
            }
            if (!string.IsNullOrEmpty(updated.Email) && updated.Email != current.Email)
            {
                current.Email = updated.Email;
            }
            if (!string.IsNullOrEmpty(updated.Title) && updated.Title != current.Title)
            {
                current.Title = updated.Title;
            }
            if (updated.DateOfBirth > DateTime.MinValue && updated.DateOfBirth != current.DateOfBirth)
            {
                current.DateOfBirth = updated.DateOfBirth;
            }
            if (!string.IsNullOrEmpty(updated.ENumber) && updated.ENumber != current.ENumber)
            {
                current.ENumber = updated.ENumber;
            }
            if (!string.IsNullOrEmpty(updated.Role) && updated.Role != current.Role)
            {
                current.Role = updated.Role;
            }
            if (updated.Gender != Gender.Nothing && updated.Gender != current.Gender)
            {
                current.Gender = updated.Gender;
            }
            if (updated.DepartmentId.HasValue && updated.DepartmentId != current.DepartmentId)
            {
                current.DepartmentId = updated.DepartmentId;
            }
            if (updated.Department != null)
            {
                current.Department = updated.Department;
            }
        }
    }
}
