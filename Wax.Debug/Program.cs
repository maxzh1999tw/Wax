using Wax.Debug.Models;
using Wax.Domain.Exercises;

namespace Wax.Debug
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            IExerciseRepository exerciseRepository = new ExerciseRepository();

            var newExercise1 = new Exercise(
                new ExerciseInfo(
                    "胸推", 
                    "推推", 
                    new ExerciseTag[]
                    {
                        new(ExerciseTagCategory.BodyPart, "胸"),
                        new(ExerciseTagCategory.TrainingType, "重訓"),
                    }),
                new ExerciseFieldSchema[]
                {
                    new("次數", "下", true, typeof(int))
                });

            var newExercise2 = new Exercise(
                new ExerciseInfo(
                    "腿推",
                    "推推",
                    new ExerciseTag[]
                    {
                        new(ExerciseTagCategory.BodyPart, "腿"),
                        new(ExerciseTagCategory.TrainingType, "重訓"),
                    }),
                new ExerciseFieldSchema[]
                {
                    new("次數", "下", true, typeof(int))
                });

            await exerciseRepository.InsertAsync(newExercise1);
            var allExercise = await exerciseRepository.GetAllAsync();
            var filteredExercises = await exerciseRepository.GetByTagsAsync(new ExerciseTag[] { new(ExerciseTagCategory.BodyPart, "胸") });
            var exerciseTags = (await exerciseRepository.GetAllExerciseTagsAsync()).ToArray();
        }
    }
}