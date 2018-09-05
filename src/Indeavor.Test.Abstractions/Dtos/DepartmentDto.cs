using System.ComponentModel.DataAnnotations;

namespace Indeavor.Test.Abstractions.Dtos
{
    public class DepartmentDto
    {
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        [MaxLength(100)]
        [Required]
        public string Code { get; set; }
    }
}
