using EntityFrameWork.Data;
using EntityFrameWork.Models;
using EntityFrameWork.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameWork.Services
{
    public class CategoryService : ICategoryService
    {

        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Category>> GetAll()
        {
           return await _context.Categories.Where(m => !m.SoftDelete).ToListAsync();
        }
    }
}
