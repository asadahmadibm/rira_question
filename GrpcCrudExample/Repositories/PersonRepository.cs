using GrpcCrudExample.Data;
using GrpcCrudExample.Models;
using Microsoft.EntityFrameworkCore;

namespace GrpcCrudExample.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly PersonDbContext _context;

        public PersonRepository(PersonDbContext context)
        {
            _context = context;
        }

        public async Task<Person> CreatePersonAsync(Person person)
        {
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();
            return person;
        }

        public async Task<Person> GetPersonAsync(int id)
        {
            return await _context.Persons.FindAsync(id);
        }

        public async Task<Person> UpdatePersonAsync(Person person)
        {
            _context.Entry(person).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return person;
        }

        public async Task<bool> DeletePersonAsync(int id)
        {
            var person = await _context.Persons.FindAsync(id);
            if (person == null)
                return false;

            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Person>> GetAllPersonAsync()
        {
            var persons = await _context.Persons.ToListAsync();
            return persons;
        }
    }

    public interface IPersonRepository
    {
        Task<Person> CreatePersonAsync(Person person);
        Task<Person> GetPersonAsync(int id);
        Task<Person> UpdatePersonAsync(Person person);
        Task<bool> DeletePersonAsync(int id);
        Task<List<Person>> GetAllPersonAsync();
    }
}