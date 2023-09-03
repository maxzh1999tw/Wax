using Wax.Domain.Exercises;
using Wax.Domain.Users;

namespace Wax.Domain.Training
{
    /// <summary>
    /// 單筆訓練紀錄
    /// </summary>
    public class TrainingRecord
    {
        /// <summary>
        /// 訓練項目
        /// </summary>
        public Exercise Exercise { get; init; }

        /// <summary>
        /// 訓練資料
        /// </summary>
        public IEnumerable<TrainingRecordFieldData> FieldDatas { get; init; }
        
        /// <summary>
        /// 訓練時間
        /// </summary>
        public DateTime TrainingTime { get; init; }

        /// <summary>
        /// 訓練用戶
        /// </summary>
        public IUserIdentity TrainingUser { get; init; }

        public TrainingRecord(Exercise exercise, IEnumerable<TrainingRecordFieldData> fieldDatas, DateTime trainingTime, IUserIdentity trainingUser)
        {
            Exercise = exercise;
            FieldDatas = fieldDatas;
            TrainingTime = trainingTime;
            TrainingUser = trainingUser;
        }
    }
}
