using Wax.Domain.Training;

namespace Wax.Domain.Exercises.FieldComputings
{
    /// <summary>
    /// 描述訓練紀錄中計算性欄位的計算方法
    /// </summary>
    public interface IExerciseFieldsComputingPolicy
    {
        /// <summary>
        /// 以指定的訓練紀錄計算值
        /// </summary>
        double Compute(TrainingRecord record);
    }
}
