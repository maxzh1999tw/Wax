using System.ComponentModel.DataAnnotations;

namespace Wax.Domain.Exercises
{
    public readonly struct ExerciseTag
    {
        public ExerciseTagCategory Category { get; init; }

        public string Name { get; init; }

        public ExerciseTag(ExerciseTagCategory category, string name)
        {
            Category = category;
            Name = name;
        }

        public ExerciseTag()
        {
            Category = ExerciseTagCategory.None;
            Name = string.Empty;
        }

        #region === 相等比較 ===

        public override bool Equals(object? obj) => Equals(obj as ExerciseTag?);

        public bool Equals(ExerciseTag? e)
        {
            if (e == null)
            {
                return false;
            }
            return Category == e.Value.Category && Name == e.Value.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Category, Name);
        }

        public static bool operator ==(ExerciseTag left, ExerciseTag right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ExerciseTag left, ExerciseTag right)
        {
            return !(left == right);
        }

        #endregion
    }

    public enum ExerciseTagCategory
    {
        /// <summary>
        /// 無
        /// </summary>
        [Display(Name = "無")]
        None,

        /// <summary>
        /// 訓練類別(重訓、有氧、伸展...等等)
        /// </summary>
        [Display(Name = "訓練類別")]
        TrainingType,

        /// <summary>
        /// 身體部位
        /// </summary>
        [Display(Name = "身體部位")]
        BodyPart,

        /// <summary>
        /// 器材種類
        /// </summary>
        [Display(Name = "器材種類")]
        Equipment,
    }
}
