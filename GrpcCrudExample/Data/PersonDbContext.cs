using GrpcCrudExample.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace GrpcCrudExample.Data
{
    public class PersonDbContext : DbContext
    {
        public PersonDbContext(DbContextOptions<PersonDbContext> options) : base(options) { }

        public DbSet<Person> Persons { get; set; }
    }
}
