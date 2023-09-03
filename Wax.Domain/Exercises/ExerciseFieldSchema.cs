namespace Wax.Domain.Exercises
{
    /// <summary>
    /// 訓練資料欄位定義
    /// </summary>
    public class ExerciseFieldSchema
    {
        /// <summary>
        /// 欄位名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 單位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 是否必填
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// 資料型別
        /// </summary>
        public Type DataType { get; set; }

        public ExerciseFieldSchema(string name, string unit, bool isRequired, Type dataType)
        {
            Name = name;
            Unit = unit;
            IsRequired = isRequired;
            DataType = dataType;
        }

        /// <summary>
        /// 驗證指定的值是否符合規範
        /// </summary>
        public bool ValidateValue(object? value)
        {
            if (value == null)
            {
                return !IsRequired;
            }

            return DataType.IsInstanceOfType(value);
        }
    }
}
