using Wax.Domain.Exercises.FieldComputings;
using Wax.Domain.Training;
using Wax.Domain.Users;

namespace Wax.Domain.Exercises
{
    /// <summary>
    /// 訓練項目
    /// </summary>
    public class Exercise
    {
        public int Id { get; set; }

        /// <summary>
        /// 描述性資訊
        /// </summary>
        public ExerciseInfo Info { get; set; }

        /// <summary>
        /// 資料欄位定義
        /// </summary>
        public IEnumerable<ExerciseFieldSchema> FieldSchemas { get; set; }

        /// <summary>
        /// 訓練量計算方式
        /// </summary>
        public IExerciseFieldsComputingPolicy ScoreComputingPolicy { get; set; }

        public Exercise(int id, ExerciseInfo info, IEnumerable<ExerciseFieldSchema> fieldSchemas, IExerciseFieldsComputingPolicy scoreComputingPolicy)
        {
            Id = id;
            Info = info;
            FieldSchemas = fieldSchemas;
            ScoreComputingPolicy = scoreComputingPolicy;
        }

        /// <summary>
        /// 以此訓練項目創建新的訓練紀錄
        /// </summary>
        /// <param name="user">訓練用戶</param>
        /// <param name="fieldDatas">訓練資料</param>
        /// <param name="trainingTime">訓練時間</param>
        /// <returns>新的訓練紀錄</returns>
        /// <exception cref="InvalidOperationException">訓練資料值與定義不符</exception>
        public TrainingRecord CreateTrainingRecord(IUserIdentity user, IEnumerable<TrainingRecordFieldData> fieldDatas, DateTime trainingTime)
        {
            var tempFieldDatas = new List<TrainingRecordFieldData>(FieldSchemas.Count());
            foreach (var fieldSchema in FieldSchemas)
            {
                var fieldData = fieldDatas.FirstOrDefault(x => x.Name == fieldSchema.Name);
                if(fieldData == null)
                {
                    throw new ArgumentException($"訓練資料缺少某些欄位");
                }
                if (!fieldSchema.ValidateValue(fieldData.Value))
                {
                    throw new ArgumentException($"{nameof(TrainingRecordFieldData)}.${nameof(TrainingRecordFieldData.Value)} 的資料內容不符合規範。");
                }

                tempFieldDatas.Add(fieldData);
            }

            return new TrainingRecord(this, fieldDatas, trainingTime, user);
        }
    }
}
