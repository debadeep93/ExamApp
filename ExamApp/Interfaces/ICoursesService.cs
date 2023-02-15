using ExamApp.Context;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using ExamApp.Domain.Entities;

namespace ExamApp.Interfaces
{
    public interface ICoursesService
    {
        Task<IEnumerable<Course>> GetAllAsync();

        Task<Course> GetAsync(Guid id);
        Task AddAsync(Course student); // Spelling error in naming the method.
        Task UpdateAsync(Course student);

        // Missing action to retrieve a single student by the provided id
        Task DeleteAsync(Course student);
    }
}
