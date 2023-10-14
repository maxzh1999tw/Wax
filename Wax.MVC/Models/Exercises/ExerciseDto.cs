using System.ComponentModel.DataAnnotations;
using Wax.Domain.Exercises;
using Wax.MVC.Utilities.Attributes;

namespace Wax.MVC.Models.Exercises
{
    public class ExerciseDto
    {
        public int? Id { get; set; }

        [Required]
        public string? Name { get; set; }

        public string? Description { get; set; }

        [ElementRequired]
        public IEnumerable<ExerciseTag>? Tags { get; set; }

        [ElementRequired]
        public IEnumerable<ExerciseFieldSchemaDto>? FieldSchemas { get; set; }
    }
}
