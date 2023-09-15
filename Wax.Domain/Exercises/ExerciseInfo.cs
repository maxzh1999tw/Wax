namespace Wax.Domain.Exercises
{
    public class ExerciseInfo
    {
        public string Name { get; init; }

        public string? Description { get; init; }

        public IEnumerable<ExerciseTag> Tags { get; init; }

        public ExerciseInfo(string name, string? description, IEnumerable<ExerciseTag> tags) 
        {
            Name = name;
            Description = description;
            Tags = tags;
        }
    }
}
