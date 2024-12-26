using System.ComponentModel.DataAnnotations;

namespace GrpcCrudExample.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        [RegularExpression(@"^\d{10}$", ErrorMessage = "کد ملی باید 10 رقم باشد.")]
        public required string NationalCode { get; set; }
        public required DateTime BirthDate { get; set; }
    }
}
