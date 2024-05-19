using System.ComponentModel.DataAnnotations;

namespace TestCoreApp.Areas.Employees.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
        public int Age { get; set; }
        public decimal Salary { get; set; }

    }
}
