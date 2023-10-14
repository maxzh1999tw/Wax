using System.ComponentModel.DataAnnotations;
using Wax.Domain.Exercises;

namespace Wax.MVC.Models.Exercises
{
    public class ExerciseFieldSchemaDto
    {
        /// <summary>
        /// 欄位名稱
        /// </summary>
        [Required]
        public string? Name { get; set; }

        /// <summary>
        /// 單位
        /// </summary>
        [Required]
        public string? Unit { get; set; }

        /// <summary>
        /// 是否必填
        /// </summary>
        [Required]
        public bool? IsRequired { get; set; }

        /// <summary>
        /// 資料型別
        /// </summary>
        [Required]
        public ExerciseFieldType? DataType { get; set; }
    }
}
