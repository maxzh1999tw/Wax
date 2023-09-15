using Wax.Domain.Exercises;
using Wax.Domain.Users;
using Wax.Infra.Memory.Utilities;

namespace Wax.Infra.Memory.Exercises
{
    public class ExerciseRepository : ListRepository<Exercise>, IExerciseRepository
    {
        public ExerciseRepository(IUserIdentity user) : base(user) { }

        public Task<IEnumerable<Exercise>> GetByTagsAsync(IEnumerable<ExerciseTag> tags)
        {
            return Task.FromResult(EntityList.Where(x => x.Info.Tags.Intersect(tags).Any()));
        }

        public Task<IEnumerable<ExerciseTag>> GetAllExerciseTagsAsync()
        {
            var initData = new ExerciseTag[]
            {
                new(ExerciseTagCategory.BodyPart, "胸"),
                new(ExerciseTagCategory.BodyPart, "背"),
                new(ExerciseTagCategory.BodyPart, "腿"),
                new(ExerciseTagCategory.BodyPart, "肩"),
                new(ExerciseTagCategory.BodyPart, "手"),
            };
            return Task.FromResult(EntityList.SelectMany(x => x.Info.Tags).Union(initData).Distinct());
        }
    }
}
