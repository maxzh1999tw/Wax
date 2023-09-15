using Wax.Domain.Utilities.Repositories;

namespace Wax.Domain.Exercises
{
    public interface IExerciseRepository : IRepository<Exercise>
    {
        Task<IEnumerable<Exercise>> GetByTagsAsync(IEnumerable<ExerciseTag> tags);

        Task<IEnumerable<ExerciseTag>> GetAllExerciseTagsAsync();
    }
}
