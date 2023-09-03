using Wax.Domain.Training;
using Wax.Domain.Utilities;

namespace Wax.Domain.Exercises.FieldComputings
{
    /// <summary>
    /// 將特定欄位相乘的計算方法
    /// </summary>
    public class MultiplyFieldsComputingPolicy : IExerciseFieldsComputingPolicy
    {
        /// <summary>
        /// 欲相乘的欄位描述
        /// </summary>
        public IEnumerable<ExerciseFieldSchema> FieldSchemasToMultiply { get; init; }

        public MultiplyFieldsComputingPolicy(IEnumerable<ExerciseFieldSchema> fieldSchemasToMultiply)
        {
            if(!fieldSchemasToMultiply.All(x => MathUtility.IsNumericType(x.DataType)))
            {
                throw new ArgumentException("只能輸入數值類型的欄位描述");
            }

            FieldSchemasToMultiply = fieldSchemasToMultiply;
        }

        /// <summary>
        /// 以指定的訓練紀錄計算值
        /// </summary>
        public double Compute(TrainingRecord record)
        {
            double score = 0;

            foreach (var fieldSchema in FieldSchemasToMultiply)
            {
                var fieldData = record.FieldDatas.First(x => x.Name == fieldSchema.Name);
                if (fieldData.Value != null)
                {
                    score = Math.Max(score, 1) * (double)fieldData.Value;
                }
            }

            return score;
        }
    }
}
