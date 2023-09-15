using Wax.Domain.Exercises;

namespace Wax.Debug.Models
{
    internal class ExerciseRepository : IExerciseRepository
    {
        private int _nextId;
        private readonly List<Exercise> _exercises = new();

        public Task DeleteAsync(int id)
        {
            var removed = _exercises.RemoveAll(x => x.Id == id);
            if (removed < 1)
            {
                throw new KeyNotFoundException();
            }

            return Task.CompletedTask;
        }

        public Task<IEnumerable<Exercise>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<Exercise>>(_exercises);
        }

        public Task<Exercise?> GetByIdAsync(int id)
        {
            return Task.FromResult(_exercises.FirstOrDefault(x => x.Id == id));
        }

        public Task<IEnumerable<Exercise>> GetByTagsAsync(IEnumerable<ExerciseTag> tags)
        {
            return Task.FromResult(_exercises.Where(x => x.Info.Tags.Intersect(tags).Any()));
        }

        public Task<int> InsertAsync(Exercise entity)
        {
            entity.Id = _nextId++;
            _exercises.Add(entity);
            return Task.FromResult(entity.Id);
        }

        public Task UpdateAsync(Exercise entity)
        {
            var index = _exercises.FindIndex(x => x.Id ==  entity.Id);
            if(index < 0)
            {
                throw new KeyNotFoundException();
            }

            _exercises[index] = entity;
            return Task.CompletedTask;
        }

        public Task<IEnumerable<ExerciseTag>> GetAllExerciseTagsAsync()
        {
            var initTags = new ExerciseTag[]
            {
                new(ExerciseTagCategory.BodyPart, "胸"),
                new(ExerciseTagCategory.BodyPart, "背"),
                new(ExerciseTagCategory.BodyPart, "腿"),
                new(ExerciseTagCategory.BodyPart, "肩"),
                new(ExerciseTagCategory.BodyPart, "手"),
            };
            return Task.FromResult( _exercises.SelectMany(x => x.Info.Tags).Union(initTags).Distinct());
        }
    }
}
