using ExamApp.Context;
using ExamApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExamApp.Interfaces
{
    public interface IStudentsService
    {
        Task<IEnumerable<Student>> GetAllAsync();

        Task<Student> GetAsync(int id);
        Task AddAsync(Student student); // Spelling error in naming the method.
        Task UpdateAsync(Student student);

        // Missing action to retrieve a single student by the provided id
        Task DeleteAsync(Student student);
    }
}
