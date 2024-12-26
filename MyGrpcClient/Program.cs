using Grpc.Net.Client;
using GrpcCrudExample;

namespace MyGrpcClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // ایجاد کانال gRPC
            var channel = GrpcChannel.ForAddress("http://localhost:3322"); // آدرس سرور
            var clientperson = new PersonService.PersonServiceClient(channel);

            #region Create
            var request = new CreatePersonRequest
            {
                FirstName = "John",
                LastName = "Doe",
                NationalCode = "1234567890",
                BirthDate = "1990-01-01"
            };
            var response = await clientperson.CreatePersonAsync(request);
            #endregion

            #region GetPerson
            var getRequest = new GetPersonRequest { Id = response.Id };
            var responseget = await clientperson.GetPersonAsync(getRequest);
            #endregion

            #region update
            var updateRequest = new UpdatePersonRequest
            {
                Id = responseget.Id,
                FirstName = "Alice Updated",
                LastName = "Smith Updated",
                NationalCode = "1122334455",
                BirthDate = "1995-03-03"
            };
            var responseupdate = await clientperson.UpdatePersonAsync(updateRequest);
            #endregion

            #region  DeletePerson
            var deleteRequest = new DeletePersonRequest { Id = responseupdate.Id };
            var responsedelete = await clientperson.DeletePersonAsync(deleteRequest);
            #endregion

            #region GetAll
            Empty empty=new Empty();
            var responsegetall = await clientperson.GetAllPersonAsync(empty);
            #endregion


            var client = new Greeter.GreeterClient(channel);

            // ارسال درخواست
            var reply = await client.SayHelloAsync(new HelloRequest { Name = "World" });
            Console.WriteLine("Greeting: " + reply.Message);
            Console.ReadLine();

        }
    }
}
