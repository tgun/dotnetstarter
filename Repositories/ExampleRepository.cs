using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetstarter.Model;
using dotnetstarter.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace dotnetstarter.Repositories {
    /// <summary>
    /// Repository class, to interact with the database layer and return the data as you expect.
    /// </summary>
    public class ExampleRepository : IExampleRepository {
        private readonly AppDbContext _context;

        public ExampleRepository(AppDbContext context) {
            _context = context;
        }

        public async Task<IEnumerable<ExampleModel>> GetExamplesAsync() {
            return await _context.Examples.ToListAsync();
        }

        public async Task<ExampleModel> CreateExample(ExampleModel resource) {
            EntityEntry<ExampleModel> newItem = await _context.Examples.AddAsync(resource);
            await _context.SaveChangesAsync();
            return newItem.Entity;
        }
    }
}
