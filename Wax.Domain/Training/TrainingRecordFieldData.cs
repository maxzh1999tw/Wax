namespace Wax.Domain.Training
{
    /// <summary>
    /// 訓練資料欄位
    /// </summary>
    public class TrainingRecordFieldData
    {
        /// <summary>
        /// 欄位名稱
        /// </summary>
        public string Name { get; init; }

        /// <summary>
        /// 值
        /// </summary>
        public object? Value { get; init; }

        public TrainingRecordFieldData(string name, object? value)
        {
            Name = name;
            Value = value;
        }
    }
}
