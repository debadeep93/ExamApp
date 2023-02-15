using ExamApp.Context;
using ExamApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExamApp.Services;

public class LanguageService
{
    private readonly MainContext _context;

    public LanguageService(MainContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Language>> GetLanguages()
    {
        return await _context.Languages.ToListAsync();
    }
}