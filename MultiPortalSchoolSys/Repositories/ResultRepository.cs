using Microsoft.EntityFrameworkCore;
using MultiPortalSchoolSys.Data;
using MultiPortalSchoolSys.Models;
using MultiPortalSchoolSys.Repositories.Interfaces;

namespace MultiPortalSchoolSys.Repositories;

// PHASE A FIX: This file previously contained an illegal duplicate of the base
// Repository<T> class. That has been completely removed. This file now contains
// ONLY the ResultRepository, which inherits from the one true base in Repository.cs.
public class ResultRepository : Repository<StudentResult>, IResultRepository
{
    public ResultRepository(ApplicationDbContext context) : base(context) { }

    /// <summary>
    /// All results for one student in one term, with Subject details loaded.
    /// PHASE D FIX: Parameter is 'term' (int 1/2/3), not a foreign key 'termId'.
    /// </summary>
    public async Task<IEnumerable<StudentResult>> GetByStudentAndTermAsync(int studentId, int term)
        => await _context.StudentResults
            .Include(r => r.Subject)
            .Where(r => r.StudentId == studentId && r.Term == term)
            .OrderBy(r => r.Subject!.Name)
            .ToListAsync();

    /// <summary>
    /// Full result sheet for a class in a term. Joins through Subject.ClassId
    /// because StudentResult does not directly store a ClassRoomId.
    /// Includes Student → User for name display on the result sheet.
    /// </summary>
    public async Task<IEnumerable<StudentResult>> GetClassResultsAsync(int classRoomId, int term)
        => await _context.StudentResults
            .Include(r => r.Student)
                .ThenInclude(s => s!.User)
            .Include(r => r.Subject)
            .Where(r => r.Subject!.ClassId == classRoomId && r.Term == term)
            .OrderBy(r => r.Student!.User!.LastName)
            .ToListAsync();

    /// <summary>
    /// Student/Parent portal query — ONLY returns published results.
    /// PHASE D FIX: Now implementable because StudentResult.IsPublished was added.
    /// Business rule: This is the ONLY result query the Student/Parent portals may call.
    /// </summary>
    public async Task<IEnumerable<StudentResult>> GetPublishedAsync(int classRoomId, int term)
        => await _context.StudentResults
            .Include(r => r.Student)
                .ThenInclude(s => s!.User)
            .Include(r => r.Subject)
            .Where(r => r.Subject!.ClassId == classRoomId &&
                        r.Term == term &&
                        r.IsPublished == true)
            .OrderBy(r => r.Student!.User!.LastName)
            .ToListAsync();
}