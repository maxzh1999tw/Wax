using Microsoft.AspNetCore.Mvc;
using Wax.Domain.Exercises;
using Wax.MVC.Utilities.Extensions;

namespace Wax.MVC.Controllers
{
    public class ExerciseController : Controller
    {
        private IExerciseRepository _exerciseRepository;

        public ExerciseController(IExerciseRepository exerciseRepository)
        {
            _exerciseRepository = exerciseRepository;
        }

        #region === View ===

        public IActionResult Index() => View();

        public IActionResult Create() => View();

        #endregion

        #region === Exercise Api ===

        [Route("/Api/Exercise")]
        [HttpGet]
        public async Task<IActionResult> GetExercises()
        {
            var exercises = await _exerciseRepository.GetAllAsync();
            return Json(exercises);
        }

        #endregion

        #region === ExerciseTag Api ===

        [Route("/Api/ExerciseTag")]
        [HttpGet]
        public async Task<IActionResult> GetExerciseTags()
        {
            var exerciseTags = await _exerciseRepository.GetAllExerciseTagsAsync();
            return Json(exerciseTags);
        }

        [Route("/Api/ExerciseTagCategory")]
        [HttpGet]
        public IActionResult GetExerciseTagCategorys()
        {
            var exerciseTagCategorys = EnumExtension.ToListItems<ExerciseTagCategory>();
            return Json(exerciseTagCategorys);
        }

        #endregion
    }
}
