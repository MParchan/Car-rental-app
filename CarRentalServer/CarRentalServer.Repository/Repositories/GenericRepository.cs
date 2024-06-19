using CarRentalServer.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalServer.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public async Task AddAsync(T obj)
        {
            await _context.Set<T>().AddAsync(obj);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(T obj)
        {
            _context.Set<T>().Update(obj);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(T obj)
        {
            _context.Set<T>().Remove(obj);
            await _context.SaveChangesAsync();
        }
    }
}
