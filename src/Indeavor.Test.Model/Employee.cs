using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Indeavor.Test.Model
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(30)]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(30)]
        [Required]
        public string LastName { get; set; }

        [EmailAddress]
        [MaxLength(50)]
        [Required]
        public string Email { get; set; }

        [MaxLength(10)]
        public string Title { get; set; }

        [MaxLength(50)]
        [Required]
        public string Role { get; set; }

        public DateTime DateOfBirth { get; set; }

        [MaxLength(100)]
        [Required]
        public string ENumber { get; set; }

        [Required]
        public Gender Gender { get; set; }

        public Department Department { get; set; }

        public int? DepartmentId { get; set; }
    }
}
