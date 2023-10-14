using Microsoft.AspNetCore.Mvc;
using Wax.Domain.Exercises;
using Wax.MVC.Models.Exercises;
using Wax.MVC.Models.Shared;
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

        [HttpGet]
        public IActionResult Index() => View();

        [HttpGet]
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
            return Json(exerciseTags.Select(x => new VuetifySelectItem(x.Name, x)));
        }

        [Route("/Api/ExerciseTagCategory")]
        [HttpGet]
        public IActionResult GetExerciseTagCategorys()
        {
            var exerciseTagCategorys = EnumExtension.ToSelectItems<ExerciseTagCategory>();
            return Json(exerciseTagCategorys);
        }

        [Route("/Api/ExerciseFieldType")]
        [HttpGet]
        public IActionResult GetExerciseFieldTypes()
        {
            var exerciseTagCategorys = EnumExtension.ToSelectItems<ExerciseFieldType>();
            return Json(exerciseTagCategorys);
        }

        [Route("/Api/Exercise")]
        [HttpPost]
        public IActionResult InsertExercise([FromBody]ExerciseDto exercise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Created($"/Api/Exercise/{exercise.Id}", exercise);
        }

        #endregion
    }
}
