using DevSpot.Models;
using DevSpot.Data;
using Microsoft.EntityFrameworkCore;

namespace DevSpot.Repositories;

public class JobPostingRepository : IRepository<JobPosting>
{
    private readonly ApplicationDbContext _context;

    public JobPostingRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<JobPosting>> GetAllAsync()
    {
        return await _context.JobPostings.ToListAsync();
    }

    public async Task<JobPosting> GetByIdAsync(int id)
    {
        var jobPosting = await _context.JobPostings.FindAsync(id);
        if (jobPosting == null)
        {
            throw new KeyNotFoundException($"JobPosting with id {id} not found.");
        }
        return jobPosting;
    }

    public async Task AddAsync(JobPosting entity)
    {
        await _context.JobPostings.AddAsync(entity);
        await _context.SaveChangesAsync();
      
    }

    public async Task UpdateAsync(JobPosting entity)
    {
        _context.JobPostings.Update(entity);
        await _context.SaveChangesAsync();
       
    }

    public async Task DeleteAsync(int id)
    {
        var jobPosting = await _context.JobPostings.FindAsync(id);

        if (jobPosting == null)
        {
            throw new KeyNotFoundException($"JobPosting with id {id} not found.");
        }
        
        _context.JobPostings.Remove(jobPosting);
        await _context.SaveChangesAsync();
    }
}