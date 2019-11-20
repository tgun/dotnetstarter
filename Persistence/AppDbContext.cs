using System;
using dotnetstarter.Model;
using Microsoft.EntityFrameworkCore;

namespace dotnetstarter.Persistence {
    /// <summary>
    /// Defines the layout of data with a database, tables, seed data, etc.
    /// </summary>
    public class AppDbContext : DbContext {
        public DbSet<ExampleModel> Examples { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {

        }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);

            builder.Entity<ExampleModel>().HasKey(p => p.Id);

            builder.Entity<ExampleModel>().HasData(
                new ExampleModel { Id = Guid.NewGuid(), Description = "First Example Item", Name = "First", SomeSecret = "Secret sauce"},
                new ExampleModel { Id = Guid.NewGuid(), Name = "Second", Description = "Second Example Item", SomeSecret = "Super secret sauce"}
            );

        }
    }

}
