using Grpc.Core;
using GrpcCrudExample.Models;
using GrpcCrudExample.Repositories;
namespace GrpcCrudExample.Services
{
    public class PersonsService :  GrpcCrudExample.PersonService.PersonServiceBase
    {
        private readonly IPersonRepository _repository;
        private readonly ILogger<PersonsService> _logger;

        public PersonsService(IPersonRepository repository, ILogger<PersonsService> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public override async Task<PersonResponse> CreatePerson(CreatePersonRequest request, ServerCallContext context)
        {
            _logger.LogInformation("CreatePerson called with input: {@Request}", request);

            var person = new Person
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                NationalCode = request.NationalCode,
                BirthDate = DateTime.Parse(request.BirthDate)
            };

            var createdPerson = await _repository.CreatePersonAsync(person);
            _logger.LogInformation("CreatePerson completed with output: {@Response}", createdPerson);

            return MapToPersonResponse(createdPerson);
        }
        public override async Task<PersonResponse> GetPerson(GetPersonRequest request, ServerCallContext context)
        {
            _logger.LogInformation("GetPerson called with input: {@Request}", request);

            var person = await _repository.GetPersonAsync(request.Id);
            if (person == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Person with ID {request.Id} not found"));
            }
            return MapToPersonResponse(person);
        }
        public override async Task<PersonResponse> UpdatePerson(UpdatePersonRequest request, ServerCallContext context)
        {
            _logger.LogInformation("UpdatePerson called with input: {@Request}", request);

            var person = new Person
            {
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                NationalCode = request.NationalCode,
                BirthDate = DateTime.Parse(request.BirthDate)
            };

            var updatedPerson = await _repository.UpdatePersonAsync(person);
            if (updatedPerson == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Person with ID {request.Id} not found"));
            }
            return MapToPersonResponse(updatedPerson);
        }
        public override async Task<DeletePersonResponse> DeletePerson(DeletePersonRequest request, ServerCallContext context)
        {
            _logger.LogInformation("DeletePerson called with input: {@Request}", request);

            var success = await _repository.DeletePersonAsync(request.Id);
            if (!success)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Person with ID {request.Id} not found"));
            }
            return new DeletePersonResponse { Success = true };
        }

        private PersonResponse MapToPersonResponse(Person person)
        {
            return new PersonResponse
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                NationalCode = person.NationalCode,
                BirthDate = person.BirthDate.ToString("yyyy-MM-dd")
            };
        }
        public override async Task<PersonsList> GetAllPerson(Empty request, ServerCallContext context)
        {
            _logger.LogInformation("GetAllPerson called with input: {@Request}", request);

            var persons = await _repository.GetAllPersonAsync();
            var response = new PersonsList();
            response.Persons.AddRange(persons.Select(p => new PersonResponse
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                NationalCode = p.NationalCode,
                BirthDate = p.BirthDate.ToString("yyyy-MM-dd")
            }));
            _logger.LogInformation("ListPersons returned {Count} persons.", persons.Count);

            return response;
        }
    }
}